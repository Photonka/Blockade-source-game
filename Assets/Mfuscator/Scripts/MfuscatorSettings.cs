using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace Mfuscator {

	internal sealed class MfuscatorSettings : EditorWindow {

		// global settings
		public static bool enable;
		public static bool experimental;
		public static int callbackOrder;

		// cache
		private static string _libVersion = "?";

		[MenuItem("Window/" + nameof(Mfuscator) + " Settings")]
		private static void MenuItemShow() {
			_ = GetWindow<MfuscatorSettings>(utility: false, title: nameof(Mfuscator));
		}

		public static void Load() {
			// 'true' by default
			enable = PlayerPrefs.GetInt(Utils.GetPlayerPrefsKey(nameof(enable)), 1) == 1;
			// 'true' by default
			experimental = PlayerPrefs.GetInt(Utils.GetPlayerPrefsKey(nameof(experimental)), 1) == 1;
			// '5000' by default
			callbackOrder = PlayerPrefs.GetInt(Utils.GetPlayerPrefsKey(nameof(callbackOrder)), 5000);

			IntPtr verP = MfuscatorBridge.GetVersion();
			_libVersion = Marshal.PtrToStringAnsi(verP);
			MfuscatorBridge.FreeVersionStr(verP);
		}

		public static void Save() {
			PlayerPrefs.SetInt(Utils.GetPlayerPrefsKey(nameof(enable)), enable ? 1 : 0);
			PlayerPrefs.SetInt(Utils.GetPlayerPrefsKey(nameof(experimental)), experimental ? 1 : 0);
			PlayerPrefs.SetInt(Utils.GetPlayerPrefsKey(nameof(callbackOrder)), callbackOrder);
		}

		private void OnEnable() {
			Load();

			minSize = new(256f, 256f);
			maxSize = minSize;
		}

		private void OnFocus() {
			Load();
		}

		#region GUI
		// cache
		private static readonly GUILayoutOption _minWidth128 = GUILayout.MinWidth(128f);

		private static void DrawText(string title, string value, bool flexSpace, params GUILayoutOption[] options) {
			GUILayout.BeginHorizontal();
			GUILayout.Label(title);
			GUILayout.Space(2f);
			GUILayout.Label(value, EditorStyles.boldLabel, options);
			if (flexSpace) {
				GUILayout.FlexibleSpace();
			}
			GUILayout.EndHorizontal();
		}

		private static bool DrawToggle(string title, bool value, bool flexSpace, params GUILayoutOption[] options) {
			GUILayout.BeginHorizontal();
			GUILayout.Label(title);
			GUILayout.Space(2f);
			value = GUILayout.Toggle(value, string.Empty, options);
			if (flexSpace) {
				GUILayout.FlexibleSpace();
			}
			GUILayout.EndHorizontal();
			return value;
		}

		private static int DrawInt32Field(string title, int value, bool flexSpace, params GUILayoutOption[] options) {
			GUILayout.BeginHorizontal();
			GUILayout.Label(title);
			GUILayout.Space(2f);
			// sowwry
			string newStrValue = GUILayout.TextField(value.ToString(), options);
			if (string.IsNullOrWhiteSpace(newStrValue)) {
				value = 0;
			} else if (int.TryParse(newStrValue, out int newValue)) {
				value = newValue;
			}
			if (flexSpace) {
				GUILayout.FlexibleSpace();
			}
			GUILayout.EndHorizontal();
			return value;
		}
		#endregion

		private void OnGUI() {
			// hot reload support
			if (EditorApplication.isCompiling || EditorApplication.isUpdating || BuildPipeline.isBuildingPlayer) {
				Close();
				return;
			}

			// GUI
			GUILayout.Space(8f);
			GUILayout.Label("Settings", EditorStyles.boldLabel);
			GUILayout.Space(4f);
			DrawText("C++ Lib Version:", _libVersion, true);
			bool newEnableValue = DrawToggle("Enable", enable, true);
			if (newEnableValue != enable) {
				enable = newEnableValue;
				Save();
			}
			bool newExperimentalValue = DrawToggle("Experimental Layers (Recommended)", experimental, true);
			if (newExperimentalValue != experimental) {
				experimental = newExperimentalValue;
				Save();
			}
			int newCallbackOrderValue = DrawInt32Field("Callback Order", callbackOrder, true, _minWidth128);
			if (newCallbackOrderValue != callbackOrder) {
				callbackOrder = newCallbackOrderValue;
				Save();
			}
			GUILayout.Space(8f);
			GUIStyle s = new(EditorStyles.helpBox);
			GUILayout.Label("NOTE: You do not need to specify an encryption key because \"MfuscatorPipeline\" generates a strong key for each build automatically.", s);
			s.normal.textColor = Color.green;
			s.active.textColor = s.normal.textColor;
			s.hover.textColor = s.normal.textColor;
			GUILayout.Label("We work hard to regularly update this asset, providing indie developers with unique AAA-level encryption at a low cost. Please take a moment to write an honest review on the Asset Store page; it will help us a lot. Thank you!", s);
			GUILayout.Space(8f);
			if (GUILayout.Button("Restore Original Unity Files")) {
				if (MfuscatorBridge.Restore(MfuscatorPipeline.EditorPath)) {
					_ = EditorUtility.DisplayDialog(nameof(Mfuscator),
						"Success. However, if you still can't build a project without Mfuscator, we recommend reinstalling Unity to fix it.", "Ok");
					return;
				}
				Utils.LogWarning("The original files could not be restored");
			}
		}
	}
}
