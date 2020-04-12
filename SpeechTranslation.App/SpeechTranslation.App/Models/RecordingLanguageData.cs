using System.Collections.Generic;

namespace SpeechTranslation.App.Models
{
	public static class RecordingLanguageData
	{
		public static IList<RecordingLanguage> RecordingLanguages { get; private set; }

		static RecordingLanguageData()
		{
			RecordingLanguages = new List<RecordingLanguage>();

			RecordingLanguages.Add(new RecordingLanguage
			{
				Name = "Japanese",
				Locale = "ja-JP"
			});

			RecordingLanguages.Add(new RecordingLanguage
			{
				Name = "Chinese",
				Locale = "zh-CN"
			});

			RecordingLanguages.Add(new RecordingLanguage
			{
				Name = "Korean",
				Locale = "ko-KR"
			});

		}
	}
	
}
