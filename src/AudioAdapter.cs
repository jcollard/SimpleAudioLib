using ManagedBass;
namespace CaptainCoder.SimpleAudioLib;

public interface AudioAdapter
{
    internal void Play();
    internal bool IsDone {get;}
    internal void Stop();
    internal void Dispose();
}

internal class BassAudioAdapter : AudioAdapter
{

    private readonly int bassHandle;
    public string ResourceName { get; private set; }
    private bool _isStopped = false;

    internal BassAudioAdapter(int bassHandle, string resourceName)
    {
        this.bassHandle = bassHandle;
        this.ResourceName = resourceName;
    }
    bool AudioAdapter.IsDone => _isStopped || Bass.ChannelGetPosition(bassHandle) >= Bass.ChannelGetLength(bassHandle);

    void AudioAdapter.Dispose() => Bass.ChannelStop(bassHandle);

    void AudioAdapter.Play() 
    {
        if (Bass.ChannelPlay(bassHandle)) return;
        Console.Error.WriteLine($"An error occurred while attempting to play: {ResourceName}");
    }

    void AudioAdapter.Stop()
    {
        Bass.ChannelStop(bassHandle);
        _isStopped = true;
        
    }
}