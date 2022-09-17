using NAudio.Wave;

namespace CaptainCoder.SimpleAudioLib;

public interface AudioAdapter
{
    internal void Play();
    internal bool IsDone {get;}
    internal void Stop();
    internal void Dispose();
}

internal class WasapiOutAdapter : AudioAdapter
{
    private readonly WasapiOut wo;
    internal WasapiOutAdapter(WasapiOut wo)
    {
        this.wo = wo;
    }

    bool AudioAdapter.IsDone => this.wo.PlaybackState == PlaybackState.Stopped;

    void AudioAdapter.Dispose() => this.wo.Dispose();

    void AudioAdapter.Play() => this.wo.Play();

    void AudioAdapter.Stop() => this.wo.Stop();
}

internal class WaveOutEventAdapter : AudioAdapter
{
    private readonly WaveOutEvent wo;
    internal WaveOutEventAdapter(WaveOutEvent wo)
    {
        this.wo = wo;
    }

    bool AudioAdapter.IsDone => this.wo.PlaybackState == PlaybackState.Stopped;

    void AudioAdapter.Dispose() => this.wo.Dispose();

    void AudioAdapter.Play() => this.wo.Play();

    void AudioAdapter.Stop() => this.wo.Stop();
}