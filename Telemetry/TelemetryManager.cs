using System;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

namespace PolyWare.Telemetry {
	public class TelemetryManager {
		public const string EventSerializationSeparator = " | ";
		public const string EventSerializationBracketStart = "{ ";
		public const string EventSerializationBracketEnd = " }";
		public const string EventSerializationSpace = " ";
		public const string EventSerializationEventIDPrefix = "ID: ";
		
		private static readonly ThreadLocal<StringBuilder> ThreadLocalBuilder = new(() => new StringBuilder(256), false);
		
		private StreamWriter writer;
		
		public TelemetryManager() {
			string filePath = Application.persistentDataPath;

			string directory = Path.GetDirectoryName(filePath);

			if (directory != null && !Directory.Exists(directory)) Directory.CreateDirectory(directory);

			// Create a build-specific directory
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

		public void LogEvent<T>(T @event) where T : ITelemetryEvent {
			SerializeEvent(@event);
			writer.WriteLine();
		}

		private void SerializeEvent(ITelemetryEvent @event) {
			StringBuilder sb = ThreadLocalBuilder.Value;
			sb.Clear();
			
			sb.Append(EventSerializationBracketStart);
			sb.Append(DateTime.UtcNow.Ticks);
			sb.Append(EventSerializationBracketEnd);
			
			sb.Append(EventSerializationSeparator);
			
			sb.Append(EventSerializationBracketStart);
			sb.Append(EventSerializationEventIDPrefix);
			sb.Append(@event.EventID);
			sb.Append(EventSerializationBracketEnd);
			
			sb.Append(EventSerializationSeparator);
			
			sb.Append(EventSerializationBracketStart);
			sb.Append(@event.EventName);
			sb.Append(EventSerializationBracketEnd);
			
			sb.Append(EventSerializationSeparator);
			
			@event.SerializeTo(sb);
			
			writer.Write(sb);
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
	}
}