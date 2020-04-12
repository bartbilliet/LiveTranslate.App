using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using SpeechTranslation.App.Services;
using UIKit;

namespace SpeechTranslation.App.iOS.Services
{
    class MicrophoneService : IMicrophoneService
    {
        private TaskCompletionSource<bool> tcsPermissions;

        public Task<bool> GetPermissionsAsync()
        {
            tcsPermissions = new TaskCompletionSource<bool>();
            RequestMicPermission();
            return tcsPermissions.Task;
        }

        private void RequestMicPermission()
        {
            var session = AVAudioSession.SharedInstance();
            session.RequestRecordPermission((granted) =>
            {
                Console.WriteLine($"Audio Permission: {granted}");
                if (granted)
                {
                    tcsPermissions.TrySetResult(granted);
                }
                else
                {
                    tcsPermissions.TrySetResult(false);
                    Console.WriteLine("YOU MUST ENABLE MICROPHONE PERMISSION");
                }
            });
        }

        public void OnRequestPermissionsResult(bool isGranted)
        {
            tcsPermissions.TrySetResult(isGranted);
        }
    }
}