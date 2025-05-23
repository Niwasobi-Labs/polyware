using System;
using UnityEngine;

namespace PolyWare.Timers {
	public static class TimeFormatter {
		public static string FormatCurrentTimeOfStopwatch(System.Diagnostics.Stopwatch stopwatch, bool includeHours = true, bool includeMinutes = true, bool includeSeconds = true, bool includeMilliseconds = true) {
			TimeSpan duration = stopwatch.Elapsed;
			string result = "";

			if (includeHours) result += $"{duration.Hours:D2}:";
			if (includeMinutes) result += $"{duration.Minutes:D2}:";
			if (includeSeconds) result += $"{duration.Seconds:D2}";
			if (includeMilliseconds) result += $"{(includeSeconds ? ":" : "")}{duration.Milliseconds:D3}";

			return result.TrimEnd(':');
		}
		
		public static string GetFormattedTime(Timer timer, bool showEmptyHours = true, bool showEmptyMinutes = true, bool showEmptySeconds = true, bool showMilliseconds = true) {
			int hours = (int)(timer.GetTime / 3600f);
			int minutes = (int)((timer.GetTime % 3600f) / 60f);
			int seconds = (int)(timer.GetTime % 60f);
			int milliseconds = (int)((timer.GetTime - Mathf.Floor(timer.GetTime)) * 1000f);
			
			string result = "";

			if (hours > 1 || showEmptyHours) result += $"{hours}:";
			if (minutes > 1 || showEmptyMinutes) result += $"{minutes}:";
			if (seconds > 1 || showEmptySeconds) result += $"{seconds}";
			if (showMilliseconds) result += $"{(seconds > 1 || showEmptySeconds ? ":" : "00:")}{milliseconds}";

			return result;
		}
	}
}