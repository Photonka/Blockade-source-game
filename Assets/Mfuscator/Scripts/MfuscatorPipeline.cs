using System;
using System.IO;
using UnityEditor;
using UnityEditor.Android;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Mfuscator {

	internal sealed class MfuscatorPipeline : IPreprocessBuildWithReport, IPostGenerateGradleAndroidProject, IPostprocessBuildWithReport {

		private const int _ENC_KEY_SUBSTR_MIN = 4, _ENC_KEY_SUBSTR_MAX = 36;
		private static string _encKey;
		private static bool _doNotContinue;

		private static bool IsGoodReport(BuildReport report) {
			return report.summary.result != BuildResult.Failed && report.summary.result != BuildResult.Cancelled;
		}
		private static bool IsIL2CPP(BuildReport report) {
			return PlayerSettings.GetScriptingBackend(report.summary.platformGroup) == ScriptingImplementation.IL2CPP;
		}
		private static bool IsSupported(BuildReport report) {
			return
				report.summary.platform == BuildTarget.StandaloneWindows64 ||
				report.summary.platform == BuildTarget.StandaloneLinux64 ||
				report.summary.platform == BuildTarget.Android;
		}
		public static string EditorPath => EditorApplication.applicationPath.Split("/Editor/Unity.")[0];
		private static string GetDataPath(BuildReport report) {
			return report.summary.outputPath.Replace(Path.GetExtension(report.summary.outputPath), "_Data");
		}
		private static string GetMetaFilePathStandalone(BuildReport report) {
			return Path.Combine(GetDataPath(report), "il2cpp_data", "Metadata", "global-metadata.dat");
		}
		private static string GetMetaFilePathAndroid(string basePath) {
			return Path.Combine(basePath, "src", "main", "assets", "bin", "Data", "Managed", "Metadata", "global-metadata.dat");
		}
		private static string GetMetaFilePathAndroid() {
			return GetMetaFilePathAndroid(Path.Combine(UnityEngine.Application.dataPath, "../", "Library", "Bee", "Android", "Prj", "IL2CPP", "Gradle", "unityLibrary"));
		}

		public MfuscatorPipeline() {
			MfuscatorSettings.Load();
		}

		// [Unity]
		public int callbackOrder => MfuscatorSettings.callbackOrder;

		// [Unity]
		public void OnPreprocessBuild(BuildReport report) {
			// reset
			// NOTE: we generate a strong key with a random length EVERY time for extra security
			// -> Guid * 2 = ~64 characters (good enough)
			_encKey = (Guid.NewGuid().ToString("N", null) + Guid.NewGuid().ToString("N", null))[UnityEngine.Random.Range(_ENC_KEY_SUBSTR_MIN, _ENC_KEY_SUBSTR_MAX + 1)..];
			_doNotContinue = false;

			// NOTE: clear cache NO MATTER what
			string metaFilePath = GetMetaFilePathStandalone(report);
			if (File.Exists(metaFilePath)) {
				File.Delete(metaFilePath);
			}
			metaFilePath = GetMetaFilePathAndroid();
			if (File.Exists(metaFilePath)) {
				File.Delete(metaFilePath);
			}

			// ignore?
			if (!MfuscatorSettings.enable || !IsGoodReport(report) || !IsIL2CPP(report) || !IsSupported(report)) {
				_doNotContinue = true;
				Utils.LogInfo("Ignoring this build (disabled/Mono/unsupported platform/error)");
				return;
			}

			string editorPath = EditorPath;

			// access test
			string testFilePath = Path.Combine(editorPath, Path.GetRandomFileName());
			try {
				using FileStream fS = File.Create(testFilePath, 1, FileOptions.DeleteOnClose);
				fS.Close();
			} catch {
				_doNotContinue = true;
				Utils.LogError(string.Concat("Failed to write to \"", editorPath, "\". Make sure that the current user has the necessary access (see \"ReadMe.txt\")"));
				return;
			}

			// NOTE: not supported on Android (< C++ 14)
			bool experimental = MfuscatorSettings.experimental && report.summary.platform != BuildTarget.Android;
			MfuscatorBridge.Prepare(editorPath, _encKey, experimental);
		}

		// [Unity]
		public void OnPostGenerateGradleAndroidProject(string path) {
			// NOTE: this code is expected to be executed only when building Android builds

			// ignore?
			if (_doNotContinue) {
				Utils.LogInfo("Ignoring step 2");
				return;
			}

			// NOTE: it is expected that "OnPostprocessBuild" will be called after this method

			path = GetMetaFilePathAndroid(path);
			if (!File.Exists(path)) {
				_doNotContinue = true;
				Utils.LogInfo("Ignoring step 2 (error)");
				return;
			}

			string editorPath = EditorPath;
			MfuscatorBridge.Modify(path, editorPath, _encKey);
			_ = MfuscatorBridge.Restore(editorPath);
			_doNotContinue = true;
		}

		// [Unity]
		public void OnPostprocessBuild(BuildReport report) {
			// ignore?
			if (_doNotContinue || !IsGoodReport(report)) {
				Utils.LogInfo("Ignoring step 3 (error/Android build)");
				return;
			}

			string editorPath = EditorPath;
			MfuscatorBridge.Modify(GetMetaFilePathStandalone(report), editorPath, _encKey);
			_ = MfuscatorBridge.Restore(editorPath);
		}
	}
}
