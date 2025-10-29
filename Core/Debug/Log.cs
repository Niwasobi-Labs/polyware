namespace PolyWare.Debug {
	public static class Log {
		// ReSharper disable Unity.PerformanceAnalysis	
		public static void Message(string message) => UnityEngine.Debug.Log(message);
		public static void Message(string message, UnityEngine.Object context) => UnityEngine.Debug.Log(message, context);
		
		// ReSharper disable Unity.PerformanceAnalysis
		public static void Warning(string message) => UnityEngine.Debug.LogWarning(message);
		public static void Warning(string message, UnityEngine.Object context) => UnityEngine.Debug.LogWarning(message, context);
		
		// ReSharper disable Unity.PerformanceAnalysis
		public static void Error(string message) => UnityEngine.Debug.LogError(message);
		public static void Error(string message, UnityEngine.Object context) => UnityEngine.Debug.LogError(message, context);
	}
}