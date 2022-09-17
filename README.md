# Simple Audio Lib

A simple Audio Library for playing audio in Console Projects. This 
library is built on top of [ManagedBass](https://github.com/ManagedBass/ManagedBass). It starts
a background thread which manages the disposal of audio automagically. 

## Install

You can add as a NuGet package:

```
dotnet add package CaptainCoder.SimpleAudio
```

## Playing Local Files

```csharp
using CaptainCoder.SimpleAudioLib;
SimpleAudio.PlayFile("Victory.mp3");
// Doesn't block, you can keep doing things.
```

## Playing Remote Files

```csharp
using CaptainCoder.SimpleAudioLib;
SimpleAudio.PlayURL("https://github.com/jcollard/SimpleAudioLib/blob/main/audio/Victory.mp3?raw=true");
// Doesn't block, you can keep doing things.
```

## Waiting for All Audio to Finish

If the main thread exits, the audio will be disposed of automatically. To
wait for audio to finish before continuing run:

```
using CaptainCoder.SimpleAudioLib;
SimpleAudio.PlayURL("https://github.com/jcollard/SimpleAudioLib/blob/main/audio/Victory.mp3?raw=true");
SimpleAudio.WaitForAllAudio();
```

## Stopping All Audio

You can stop all currently playing audio:

```
using CaptainCoder.SimpleAudioLib;
SimpleAudio.PlayURL("https://github.com/jcollard/SimpleAudioLib/blob/main/audio/Victory.mp3?raw=true");
SimpleAudio.StopAllAudio();
```
