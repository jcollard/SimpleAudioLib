namespace CaptainCoder.SimpleAudioLib;
internal class AudioReference {

    private readonly AudioAdapter wo;
    // public string URL { get; }

    internal AudioReference(AudioAdapter wo) {
        this.wo = wo;
    }

    internal void Play() => this.wo.Play();
    internal bool IsDone => this.wo.IsDone;
    internal void Stop() => this.wo.Stop();

    internal void Dispose() => this.wo.Dispose();
}