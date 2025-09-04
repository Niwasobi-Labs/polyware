#if UNITY_EDITOR
using System.IO;
using PolyWare.Debug;
using UnityEditor;
using UnityEngine;

namespace PolyWare.ActionGame {
	public class CreateWeaponWindow : EditorWindow {
		private string entityName = "NewWeapon";
		private string nameSpace = "PolyWare.Weapons";
		private string targetFolder = "Assets";
		private bool createSubfolder = false;
		private string subfolderName = "";

		[MenuItem("PolyWare/Create New Weapon")]
		public static void ShowWindow() {
			var window = GetWindow<CreateWeaponWindow>(true, "Create New Weapon", true);
			window.minSize = new Vector2(520, 210);
			window.Show();
		}

		private void OnGUI() {
			EditorGUILayout.LabelField("Weapon Generator", EditorStyles.boldLabel);
			EditorGUILayout.Space(4);

			nameSpace = EditorGUILayout.TextField("Namespace", nameSpace);
			entityName = EditorGUILayout.TextField("Weapon Name", entityName);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Target Folder");
			EditorGUILayout.SelectableLabel(targetFolder, EditorStyles.textField, GUILayout.Height(18));
			if (GUILayout.Button("Browse...", GUILayout.Width(90))) {
				var absolute = EditorUtility.OpenFolderPanel("Choose Target Folder", Application.dataPath, "");
				if (!string.IsNullOrEmpty(absolute)) {
					var assetsPath = Application.dataPath.Replace("\\", "/");
					absolute = absolute.Replace("\\", "/");
					if (!absolute.StartsWith(assetsPath)) {
						EditorUtility.DisplayDialog("Invalid Folder",
							"Folder must be inside your project's Assets/ directory.", "OK");
					} else {
						targetFolder = "Assets" + absolute.Substring(assetsPath.Length);
					}
				}
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space(4);
			createSubfolder = EditorGUILayout.ToggleLeft("Create subfolder in target", createSubfolder);
			using (new EditorGUI.DisabledScope(!createSubfolder)) {
				subfolderName = EditorGUILayout.TextField("Subfolder Name", subfolderName);
			}

			EditorGUILayout.Space(8);
			using (new EditorGUI.DisabledScope(!InputsValid())) {
				if (GUILayout.Button("Create Files", GUILayout.Height(30))) {
					try {
						GenerateFiles();
						EditorUtility.DisplayDialog("Success", $"Created {entityName} files.", "OK");
						Close();
					} catch (System.SystemException ex) {
						Log.Error(ex.Message);
						EditorUtility.DisplayDialog("Error", ex.Message, "OK");
					}
				}
			}
			EditorGUILayout.HelpBox(
				"Creates {Name}Definition.cs, {Name}Data.cs (with {Name}SpawnData), and {Name}.cs.",
				MessageType.Info
			);
		}

		private bool InputsValid() {
			return !string.IsNullOrWhiteSpace(entityName)
			       && CodeSafe(entityName)
			       && !string.IsNullOrWhiteSpace(nameSpace)
			       && CodeSafeNamespace(nameSpace)
			       && targetFolder.StartsWith("Assets");
		}

		private static bool CodeSafe(string s) {
			if (string.IsNullOrEmpty(s)) return false;
			if (!(char.IsLetter(s[0]) || s[0] == '_')) return false;
			for (int i = 1; i < s.Length; i++) {
				char c = s[i];
				if (!(char.IsLetterOrDigit(c) || c == '_')) return false;
			}
			return true;
		}

		private static bool CodeSafeNamespace(string ns) {
			var parts = ns.Split('.');
			foreach (var p in parts) {
				if (!CodeSafe(p)) return false;
			}
			return true;
		}

		private void GenerateFiles() {
			string finalFolder = targetFolder;

			// Handle subfolder creation
			if (createSubfolder && !string.IsNullOrWhiteSpace(subfolderName)) {
				subfolderName = subfolderName.Trim().Replace("\\", "/");
				if (subfolderName.Contains("/")) {
					throw new IOException("Subfolder name cannot contain '/'. Use a single folder name.");
				}

				if (!AssetDatabase.IsValidFolder(finalFolder)) {
					throw new IOException($"Parent folder not found in project: {finalFolder}");
				}

				string candidate = $"{finalFolder}/{subfolderName}";
				if (!AssetDatabase.IsValidFolder(candidate)) {
					AssetDatabase.CreateFolder(finalFolder, subfolderName);
					AssetDatabase.Refresh();
				}
				finalFolder = candidate;
			}

			Directory.CreateDirectory(finalFolder);

			string defPath = Path.Combine(finalFolder, $"{entityName}Definition.cs");
			string dataPath = Path.Combine(finalFolder, $"{entityName}Data.cs");
			string entPath = Path.Combine(finalFolder, $"{entityName}.cs");

			if (File.Exists(defPath) || File.Exists(dataPath) || File.Exists(entPath)) {
				throw new IOException("One or more target files already exist. Choose a different name or folder.");
			}

			File.WriteAllText(defPath, GetDefinitionSource(nameSpace, entityName));
			File.WriteAllText(dataPath, GetDataSource(nameSpace, entityName));
			File.WriteAllText(entPath, GetEntitySource(nameSpace, entityName));

			AssetDatabase.Refresh();
		}

		private static string GetDefinitionSource(string ns, string name) {
			return
$@"using PolyWare.Core.Entities;
using UnityEngine;

namespace {ns} {{
	[CreateAssetMenu(fileName = ""{name} Definition"", menuName = ""Game/{name}"")]
	public class {name}Definition : EquipmentDefinition {{
		public override IEntityData CreateDefaultInstance() => new {name}Data(this);
	}}
}}";
		}

		private static string GetDataSource(string ns, string name) {
			return
$@"using PolyWare.Core.Entities;

namespace {ns} {{
	public class {name}SpawnData : EquipmentSpawnData {{ }}

	public class {name}Data : EquipmentData {{
		public {name}Data(EquipmentDefinition definition) : base(definition) {{ }}

		public override void Override(IEntitySpawnData data) {{
			base.Override(data);
			
			var spawnData = data as {name}SpawnData;
		}}
	}}
}}";
		}

		private static string GetEntitySource(string ns, string name) {
			return
$@"using PolyWare.Core.Entities;
using Sirenix.OdinInspector;

namespace {ns} {{
	public class {name} : Equipment {{
		[ShowInInspector, ReadOnly] 
		public {name}Data {name}Data => Data as {name}Data;

		protected override void OnInitialize() {{
			
		}}

		public override bool CanUse {{ get; }}

		public override void Use() {{
			throw new System.NotImplementedException();
		}}

		public override void CancelUse() {{
			throw new System.NotImplementedException();
		}}

		protected override void OnEquip() {{
			throw new System.NotImplementedException();
		}}

		protected override bool OnUnequip() {{
			throw new System.NotImplementedException();
		}}
	}}
}}";
		}
	}
}
#endif