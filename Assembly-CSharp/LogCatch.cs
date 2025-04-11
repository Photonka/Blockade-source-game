using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class LogCatch : MonoBehaviour
{
	// Token: 0x06000194 RID: 404 RVA: 0x000240E5 File Offset: 0x000222E5
	private void OnEnable()
	{
		Application.RegisterLogCallback(new Application.LogCallback(this.HandleLog));
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00017EEE File Offset: 0x000160EE
	private void OnDisable()
	{
		Application.RegisterLogCallback(null);
	}

	// Token: 0x06000196 RID: 406 RVA: 0x000240F8 File Offset: 0x000222F8
	private void Awake()
	{
		this.OnEnable();
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00024100 File Offset: 0x00022300
	private void HandleLog(string logString, string stackTrace, LogType t)
	{
		if (logString == "NullReferenceException: Object reference not set to an instance of an object")
		{
			stackTrace == "vp_FPController.FixedMove ()\nvp_FPController.FixedUpdate ()\n";
		}
	}
}
