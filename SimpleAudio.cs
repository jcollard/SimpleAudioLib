using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using NAudio.Wave;

namespace CaptainCoder.SimpleAudioLib;
public static class SimpleAudio
{
    private static readonly ConcurrentDictionary<string, AudioReference> audio = new();
    private static Thread? checker;
    private static int NextID = 0;
    private static Thread MainThread;

    static SimpleAudio()
    {
        MainThread = Thread.CurrentThread;
        checker = new Thread(CheckDispose);
        checker.Start();
    }

    private static void CheckDispose()
    {
        while (true && MainThread.IsAlive)
        {
            var toRemove = audio.Keys.Where(k => audio[k].IsDone);
            foreach (var key in toRemove)
            {
                audio[key].Dispose();
                audio.TryRemove(key, out AudioReference _);
            }
            Thread.Sleep(100);
        }
        StopAllAudio();
    }

    /// <summary>
    /// Given a URL, attempts to download and play it as an audio file.
    /// </summary>
    /// <param name="url"></param>
    public static void PlayURL(string url)
    {
        WasapiOut wo = new WasapiOut();
        wo.Init(new MediaFoundationReader(url));
        AudioReference reference = new AudioReference(new WasapiOutAdapter(wo));
        reference.Play();
        audio[$"{NextID++}"] = reference;
    }

    /// <summary>
    /// Given a file path, attempts to load and play it as an audio file.
    /// </summary>
    /// <param name="path"></param>
    public static void PlayFile(string path)
    {
        AudioFileReader audioFile = new AudioFileReader(path);
        WaveOutEvent outputDevice = new WaveOutEvent();
        outputDevice.Init(audioFile);
        AudioReference reference = new (new WaveOutEventAdapter(outputDevice));
        reference.Play();
        audio[$"{NextID++}"] = reference;
    }

    /// <summary>
    /// Stops all Audio that is currently playing.
    /// </summary>
    public static void StopAllAudio() => audio.Values.ForEach(a => a.Stop());

    /// <summary>
    /// Waits for all audio that is currently playing to stop.
    /// </summary>
    public static void WaitForAllAudio()
    {
        while (!audio.IsEmpty) Thread.Sleep(100);
    }

}
