using UnityEngine;

namespace PolyWare.Debug {
	public static class GizmoHelper {
		private const float thicknessOffset = 0.01f;

		public static void DrawCircle(Vector3 center, Vector3 axisNormal, float axisOffset, float radius, int segments, Color color, uint thickness) {
			Gizmos.color = color;
			Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, axisNormal.normalized);

			for (int i = 0; i < segments; i++) {
				float angle = Mathf.PI * 2 * i / segments;
				float nextAngle = Mathf.PI * 2 * (i + 1) / segments;

				Vector3 point1 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
				Vector3 point2 = new Vector3(Mathf.Cos(nextAngle), Mathf.Sin(nextAngle), 0) * radius;

				Vector3 offsetCenter = center + axisNormal.normalized * axisOffset;
				point1 = rotation * point1 + offsetCenter;
				point2 = rotation * point2 + offsetCenter;

				DrawLine(point1, point2, color, thickness);
			}
		}

		public static void DrawLine(Vector3 start, Vector3 end, Color color, uint thickness) {
			if (thickness == 0) thickness = 1;

			Gizmos.color = color;

			Vector3 lineDir = end - start;
			Vector3 perpendicular = Vector3.Cross(lineDir, Vector3.up);

			if (perpendicular == Vector3.zero) perpendicular = Vector3.Cross(lineDir, Vector3.right);

			perpendicular.Normalize();

			int halfThickness = (int)thickness / 2;

			for (int i = -halfThickness; i <= halfThickness; i++) {
				Vector3 currentThicknessOffset = perpendicular * (thicknessOffset * i);
				Gizmos.DrawLine(start + currentThicknessOffset, end + currentThicknessOffset);
			}
		}
		
		public enum ConeStyle {
			Arc,
			Line
		}
		
		public static void DrawCone(Vector3 origin, Vector3 forward, Vector3 up, float angle, float length, ConeStyle style, Color color, uint thickness = 1) {
			float maxAngleRad = Mathf.Atan2(angle, length);
			Quaternion leftRot = Quaternion.AngleAxis(-maxAngleRad * Mathf.Rad2Deg, up);
			Quaternion rightRot = Quaternion.AngleAxis(maxAngleRad * Mathf.Rad2Deg, up);

			Vector3 leftDir = leftRot * forward;
			Vector3 rightDir = rightRot * forward;

			DrawLine(origin, origin + leftDir * length, color, thickness);
			DrawLine(origin, origin + rightDir * length, color, thickness);
			
			switch (style) {
				case ConeStyle.Arc:
					DrawArc(origin, forward, up, angle, length, color, thickness);
					break;
				case ConeStyle.Line:
				default:
					DrawLine(origin + leftDir * length, origin + rightDir * length, color, thickness);
					break;
			}
		}
		
		public static void DrawArc(Vector3 origin, Vector3 forward, Vector3 up, float radius, float length, Color color, uint thickness, int segments = 16) {
			float angle = Mathf.Atan2(radius, length) * Mathf.Rad2Deg;
			Vector3 lastPoint = Quaternion.AngleAxis(-angle, up) * forward * length + origin;
		
			for (int i = 1; i <= segments; i++) {
				float t = Mathf.Lerp(-angle, angle, i / (float)segments);
				Vector3 nextPoint = Quaternion.AngleAxis(t, up) * forward * length + origin;
				DrawLine(lastPoint, nextPoint, color, thickness);
				lastPoint = nextPoint;
			}
		}
	}
}