using SpeechTranslation.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTranslation.App.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
		public IList<RecordingLanguage> RecordingLanguages { get { return RecordingLanguageData.RecordingLanguages; } }

		public MainPageViewModel()
		{
			selectedRecordingLanguage = RecordingLanguageData.RecordingLanguages[0];
		}

		RecordingLanguage selectedRecordingLanguage;
		public RecordingLanguage SelectedRecordingLanguage
		{
			get { return selectedRecordingLanguage; }
			set
			{
				if (selectedRecordingLanguage != value)
				{
					selectedRecordingLanguage = value;
					OnPropertyChanged();
				}
			}
		}
	}
}
