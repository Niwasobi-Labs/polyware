using UnityEditor;
using UnityEngine;
using System.IO;

namespace PolyWare.Editor {
	public static class TelemetryMenu {
		// Path to the telemetry directory
		private static string GetTelemetryDirectory() {
			string basePath = Application.persistentDataPath;
			string versionedDir = Path.Combine(basePath, Application.version);

			// Ensure the directory exists
			if (!Directory.Exists(versionedDir)) Directory.CreateDirectory(versionedDir); 

			return versionedDir;
		}

		[MenuItem("PolyWare/Open Telemetry %#t")] // Shortcut: Ctrl (Cmd) + Shift + T
		public static void OpenTelemetryDirectory() {
			string telemetryDir = GetTelemetryDirectory();

			if (Directory.Exists(telemetryDir)) EditorUtility.RevealInFinder(telemetryDir);
			else Debug.Logger.Error($"Telemetry directory does not exist: {telemetryDir}");
		}
	}
}