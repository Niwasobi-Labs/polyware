#if UNITY_EDITOR
using System.IO;
using PolyWare.Debug;
using UnityEditor;
using UnityEngine;

namespace PolyWare.Editor {
	public class CreateEntityWindow : EditorWindow {
		private string entityName = "NewEntity";
		private string nameSpace = "PolyWare.Gameplay";
		private string targetFolder = "Assets";
		private bool createSubfolder = false;
		private string subfolderName = "";

		[MenuItem("PolyWare/Create New Entity")]
		public static void ShowWindow() {
			var window = GetWindow<CreateEntityWindow>(true, "Create New Entity", true);
			window.minSize = new Vector2(520, 180);
			window.Show();
		}

		private void OnGUI() {
			EditorGUILayout.LabelField("Entity Generator", EditorStyles.boldLabel);
			EditorGUILayout.Space(4);

			nameSpace = EditorGUILayout.TextField("Namespace", nameSpace);
			entityName = EditorGUILayout.TextField("Entity Name", entityName);

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
			EditorGUILayout.HelpBox("Creates {Name}Definition.cs, {Name}Data.cs (with {Name}SpawnData), and {Name}.cs.", MessageType.Info);
		}

		private bool InputsValid() {
			return !string.IsNullOrWhiteSpace(entityName)
			       && CodeSafe(entityName)
			       && !string.IsNullOrWhiteSpace(nameSpace)
			       && CodeSafeNamespace(nameSpace)
			       && targetFolder.StartsWith("Assets");
		}

		private static bool CodeSafe(string s) {
			// Very light check: starts with letter/underscore and contains only letters, digits, or underscores
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
			Directory.CreateDirectory(targetFolder);

			string defPath = Path.Combine(targetFolder, $"{entityName}Definition.cs");
			string dataPath = Path.Combine(targetFolder, $"{entityName}Data.cs");
			string entPath = Path.Combine(targetFolder, $"{entityName}.cs");

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
	public class {name}Definition : EntityDefinition {{
		public override IEntityData CreateDefaultInstance() => new {name}Data(this);
	}}
}}";
		}

		private static string GetDataSource(string ns, string name) {
			return
$@"using PolyWare.Core.Entities;

namespace {ns} {{
	public class {name}SpawnData : IEntitySpawnData {{ }}

	public class {name}Data : EntityData<EntityDefinition>, IAllowSpawnOverride {{
		public {name}Data(EntityDefinition definition) : base(definition) {{ }}

		public void Override(IEntitySpawnData data) {{
			// TODO: apply spawn overrides from {name}SpawnData
		}}
	}}
}}";
		}

		private static string GetEntitySource(string ns, string name) {
			return
$@"using PolyWare.Core.Entities;

namespace {ns} {{
	public class {name} : Entity<{name}Data> {{
		public override {name}Data Data {{ get; protected set; }}

		protected override void OnInitialize() {{
			// TODO: initialization logic
		}}
	}}
}}";
		}
	}
}
#endif