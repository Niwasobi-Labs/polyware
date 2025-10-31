#if UNITY_EDITOR
using PolyWare.Game;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnEntity))]
public class SpawnEntityEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		SpawnEntity spawnEntity = (SpawnEntity)target;
		if (GUILayout.Button("Spawn Now"))
		{
			spawnEntity.Spawn();
		}
	}
}
#endif