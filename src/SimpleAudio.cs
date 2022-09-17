using System.Collections.Concurrent;
using ManagedBass;

namespace CaptainCoder.SimpleAudioLib;
public static class SimpleAudio
{
    private static readonly ConcurrentDictionary<string, AudioReference> audio = new();
    private static Thread? checker;
    private static int NextID = 0;
    private static Thread MainThread;

    static SimpleAudio()
    {
        if (!Bass.Init()) {
            Console.Error.WriteLine("Could not initialize Audio Device.");
        }
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
        Bass.Free();
    }

    public static void PlayURL(string URL)
    {
        AudioAdapter adapter = new BassAudioAdapter(Bass.CreateStream(URL, 0, BassFlags.Default, null), URL);
        AudioReference reference = new (adapter);
        reference.Play();
        audio[$"{NextID++}"] = reference;
    }

    /// <summary>
    /// Given a file path, attempts to load and play it as an audio file.
    /// </summary>
    /// <param name="path"></param>
    public static void PlayFile(string path)
    {
        AudioAdapter adapter = new BassAudioAdapter(Bass.CreateStream(path), path);
        AudioReference reference = new (adapter);
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
