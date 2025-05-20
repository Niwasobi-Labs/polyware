using System;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

namespace PolyWare.Metrics {
	public class TelemetryManager {
		private static readonly ThreadLocal<StringBuilder> ThreadLocalBuilder =
			new(() => new StringBuilder(256), false);

		private StreamWriter writer;

		public TelemetryManager() {
			string filePath = Application.persistentDataPath;

			string directory = Path.GetDirectoryName(filePath);

			if (directory != null && !Directory.Exists(directory)) Directory.CreateDirectory(directory);

			// Create build-specific directory
			string buildDir = Path.Combine(filePath, Application.version);
			if (!Directory.Exists(buildDir)) Directory.CreateDirectory(buildDir);

			// Find a unique file path with a maximum of 20 files
			string baseFilePath = Path.Combine(buildDir, "telemetry.log");
			string finalFilePath = baseFilePath;
			int counter = 1;

			while (File.Exists(finalFilePath) && counter < 20) {
				finalFilePath = Path.Combine(buildDir, $"telemetry({counter}).log");
				counter++;
			}

			if (File.Exists(finalFilePath)) {
				// Hit the limit, reset the directory
				foreach (string file in Directory.GetFiles(buildDir)) File.Delete(file);
				finalFilePath = baseFilePath;
			}

			writer = new StreamWriter(finalFilePath, false, Encoding.UTF8);
			writer.AutoFlush = true;

			Application.quitting += OnApplicationQuit;
		}

		public void LogEvent<T>(T eventData) where T : ITelemetryEvent {
			TelemetryEventData newEvent = new(eventData);
			newEvent.SerializeTo(writer);
			writer.WriteLine();
		}

		private void OnApplicationQuit() {
			CloseWriter();
		}

		public void OutputToFile(string path) {
			writer.Flush();
			Debug.Logger.Error($"Telemetry output flushed to file: {path}");
		}

		private void CloseWriter() {
			writer?.Flush();
			writer?.Close();
			writer = null;
		}

		private readonly struct TelemetryEventData {
			private readonly long timestamp;
			private readonly ITelemetryEvent data;

			public TelemetryEventData(ITelemetryEvent data) {
				timestamp = DateTime.UtcNow.Ticks;
				this.data = data;
			}

			public void SerializeTo(TextWriter writer) {
				StringBuilder sb = ThreadLocalBuilder.Value;
				sb.Clear();
				sb.Append('[');
				sb.Append(timestamp);
				sb.Append("] ");
				writer.Write(sb);
				data.SerializeTo(writer);
			}
		}
	}
}