# Live translation mobile app
Alpha (PoC) version of a Xamarin-based mobile app that can generate translated subtitles for any live audio source, using Azure Speech service.
In the PoC, this is currently configured for Chinese, Japanese and Korean. 

## Prerequisites
1. Create an [Azure Speech service](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/overview#try-the-speech-service-for-free) service. 
2. Create an AppSettings.json file with following contents:

```json
{
  "SpeechSubscriptionKey": "<YourSpeechServiceKey>",
  "SpeechSubscriptionRegion": "<YourSpeechServiceRegion>"
}
```

## Getting started
To use the app in the  Android emulator, do the following: 
1. Click the *Enable mic* button to give access to the phone's microphone.
2. Choose your audio source language.
3. Click *Start* to generate the translated subtitles. 
4. Click *Stop* to stop listening to audio (which will also stop Azure Speech consumption).

## Known issues
- The app is currently only tested on Android Emulator, but could technically also work on iOS (although untested).

## To do
- Optimize the currently very poor user experience
- Fix some bugs
