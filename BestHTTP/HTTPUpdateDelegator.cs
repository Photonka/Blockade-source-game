using System;
using System.Threading;
using BestHTTP.Caching;
using BestHTTP.Cookies;
using UnityEngine;

namespace BestHTTP
{
	// Token: 0x02000182 RID: 386
	[ExecuteInEditMode]
	public sealed class HTTPUpdateDelegator : MonoBehaviour
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000985D0 File Offset: 0x000967D0
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x000985D7 File Offset: 0x000967D7
		public static HTTPUpdateDelegator Instance { get; private set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x000985DF File Offset: 0x000967DF
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x000985E6 File Offset: 0x000967E6
		public static bool IsCreated { get; private set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x000985EE File Offset: 0x000967EE
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x000985F5 File Offset: 0x000967F5
		public static bool IsThreaded { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x000985FD File Offset: 0x000967FD
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x00098604 File Offset: 0x00096804
		public static bool IsThreadRunning { get; private set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0009860C File Offset: 0x0009680C
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x00098613 File Offset: 0x00096813
		public static int ThreadFrequencyInMS { get; set; } = 100;

		// Token: 0x06000E74 RID: 3700 RVA: 0x00098624 File Offset: 0x00096824
		public static void CheckInstance()
		{
			try
			{
				if (!HTTPUpdateDelegator.IsCreated)
				{
					GameObject gameObject = GameObject.Find("HTTP Update Delegator");
					if (gameObject != null)
					{
						HTTPUpdateDelegator.Instance = gameObject.GetComponent<HTTPUpdateDelegator>();
					}
					if (HTTPUpdateDelegator.Instance == null)
					{
						HTTPUpdateDelegator.Instance = new GameObject("HTTP Update Delegator")
						{
							hideFlags = 52
						}.AddComponent<HTTPUpdateDelegator>();
					}
					HTTPUpdateDelegator.IsCreated = true;
					HTTPManager.Logger.Information("HTTPUpdateDelegator", "Instance Created!");
				}
			}
			catch
			{
				HTTPManager.Logger.Error("HTTPUpdateDelegator", "Please call the BestHTTP.HTTPManager.Setup() from one of Unity's event(eg. awake, start) before you send any request!");
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000986C8 File Offset: 0x000968C8
		private void Setup()
		{
			HTTPCacheService.SetupCacheFolder();
			CookieJar.SetupFolder();
			CookieJar.Load();
			if (HTTPUpdateDelegator.IsThreaded)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadFunc));
			}
			HTTPUpdateDelegator.IsSetupCalled = true;
			if (!Application.isEditor || Application.isPlaying)
			{
				Object.DontDestroyOnLoad(base.gameObject);
			}
			HTTPManager.Logger.Information("HTTPUpdateDelegator", "Setup done!");
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00098730 File Offset: 0x00096930
		private void ThreadFunc(object obj)
		{
			HTTPManager.Logger.Information("HTTPUpdateDelegator", "Update Thread Started");
			try
			{
				HTTPUpdateDelegator.IsThreadRunning = true;
				while (HTTPUpdateDelegator.IsThreadRunning)
				{
					HTTPManager.OnUpdate();
					Thread.Sleep(HTTPUpdateDelegator.ThreadFrequencyInMS);
				}
			}
			finally
			{
				HTTPManager.Logger.Information("HTTPUpdateDelegator", "Update Thread Ended");
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00098798 File Offset: 0x00096998
		private void Update()
		{
			if (!HTTPUpdateDelegator.IsSetupCalled)
			{
				HTTPUpdateDelegator.IsSetupCalled = true;
				this.Setup();
			}
			if (!HTTPUpdateDelegator.IsThreaded)
			{
				HTTPManager.OnUpdate();
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000987B9 File Offset: 0x000969B9
		private void OnDisable()
		{
			HTTPManager.Logger.Information("HTTPUpdateDelegator", "OnDisable Called!");
			this.OnApplicationQuit();
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000987D5 File Offset: 0x000969D5
		private void OnApplicationPause(bool isPaused)
		{
			if (HTTPUpdateDelegator.OnApplicationForegroundStateChanged != null)
			{
				HTTPUpdateDelegator.OnApplicationForegroundStateChanged(isPaused);
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x000987EC File Offset: 0x000969EC
		private void OnApplicationQuit()
		{
			HTTPManager.Logger.Information("HTTPUpdateDelegator", "OnApplicationQuit Called!");
			if (HTTPUpdateDelegator.OnBeforeApplicationQuit != null)
			{
				try
				{
					if (!HTTPUpdateDelegator.OnBeforeApplicationQuit())
					{
						HTTPManager.Logger.Information("HTTPUpdateDelegator", "OnBeforeApplicationQuit call returned false, postponing plugin shutdown.");
						return;
					}
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("HTTPUpdateDelegator", string.Empty, ex);
				}
			}
			HTTPUpdateDelegator.IsThreadRunning = false;
			if (!HTTPUpdateDelegator.IsCreated)
			{
				return;
			}
			HTTPUpdateDelegator.IsCreated = false;
			HTTPManager.OnQuit();
		}

		// Token: 0x04001235 RID: 4661
		public static Func<bool> OnBeforeApplicationQuit;

		// Token: 0x04001236 RID: 4662
		public static Action<bool> OnApplicationForegroundStateChanged;

		// Token: 0x04001237 RID: 4663
		private static bool IsSetupCalled;
	}
}
