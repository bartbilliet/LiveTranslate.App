//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;
using SpeechTranslation.App.Services;
using SpeechTranslation.App.ViewModels;
using SpeechTranslation.App.Models;

namespace SpeechTranslation.App.Views
{
    public partial class MainPage : ContentPage
    {
        public Boolean Recording = false;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        public async Task SpeechTranslationWithMicrophoneAsync()
        {
            // Translation source language.
            var selectedRecordingLanguage = RecordingLanguagesPicker.SelectedItem as RecordingLanguage;
            string fromLanguage = selectedRecordingLanguage.Locale; //"ja-JP";

            // Creates an instance of a speech translation config with specified subscription key and service region.
            string speechSubscriptionKey = AppSettingsManager.Settings["SpeechSubscriptionKey"];
            string speechSubscriptionRegion = AppSettingsManager.Settings["SpeechSubscriptionRegion"];
            var config = SpeechTranslationConfig.FromSubscription(speechSubscriptionKey, speechSubscriptionRegion);
            config.SpeechRecognitionLanguage = fromLanguage;

            // Translation target language(s).
            config.AddTargetLanguage("en-US");

            // Creates a translation recognizer using microphone as audio input.
            using (var recognizer = new TranslationRecognizer(config))
            {
                //Subscribes to events.
                recognizer.Recognizing += (s, e) =>
                {
                    Console.WriteLine($"RECOGNIZING in '{fromLanguage}': Text={e.Result.Text}");
                    foreach (var element in e.Result.Translations)
                    {
                        Console.WriteLine($"    TRANSLATING into '{element.Key}': {element.Value}");
                        UpdateRecognizingText(element.Value);
                    }
                };

                recognizer.Recognized += (s, e) =>
                {
                    if (e.Result.Reason == ResultReason.TranslatedSpeech)
                    {
                        //Console.WriteLine($"RECOGNIZED in '{fromLanguage}': Text={e.Result.Text}");
                        foreach (var element in e.Result.Translations)
                        {
                            Console.WriteLine($"    TRANSLATED into '{element.Key}': {element.Value}");
                            UpdateRecognizedText(element.Value);
                        }
                    }
                    /*
                    //Triggered when text recongized but not able to translate
                    else if (e.Result.Reason == ResultReason.RecognizedSpeech)
                    {
                        Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                        Console.WriteLine($"    Speech not translated.");
                    }
                    else if (e.Result.Reason == ResultReason.NoMatch)
                    {
                        Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    }
                    */
                };

                recognizer.Canceled += (s, e) =>
                {
                    Console.WriteLine($"CANCELED: Reason={e.Reason}");

                    if (e.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                };

                recognizer.SessionStarted += (s, e) =>
                {
                    Console.WriteLine("\nSession started event.");
                };

                recognizer.SessionStopped += (s, e) =>
                {
                    Console.WriteLine("\nSession stopped event.");
                };

                // Starts continuous recognition. Uses StopContinuousRecognitionAsync() to stop recognition.
                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                // Recognize as long as 'stopped' is clicked
                Recording = true;
                do
                {
                    //loop until stop button is pressed
                } while (Recording != false);

                // Stops continuous recognition.
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
            }

        }

        private async void OnRecognitionButtonClicked(object sender, EventArgs e)
        {
            await SpeechTranslationWithMicrophoneAsync();
        }

        private async void OnEnableMicrophoneButtonClicked(object sender, EventArgs e)
        {
            bool micAccessGranted = await DependencyService.Get<IMicrophoneService>().GetPermissionsAsync();
            if (!micAccessGranted)
            {
                UpdateRecognizingText("Please give access to microphone");
            }
        }

        private void UpdateRecognizedText(String message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                RecognizedText.Text += "- " + message + "\n";
                RecognizedText_ScrollView.ScrollToAsync(RecognizedText, ScrollToPosition.End, true);
            });
        }
        private void UpdateRecognizingText(String message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                RecognizingText.Text = message;
            });
        }

        private void StopRecognitionButton_Clicked(object sender, EventArgs e)
        {
            Recording = false;
        }

        private void layout_LayoutChanged(object sender, EventArgs e)
        {
            RecognizedText_ScrollView.ScrollToAsync(RecognizedText, ScrollToPosition.End, true);
        }

        private void RecordingLanguagesPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RecognizedText.Text = "";
        }
    }
}
