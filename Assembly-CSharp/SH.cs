using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000B3 RID: 179
public class SH : MonoBehaviour
{
	// Token: 0x060005CC RID: 1484 RVA: 0x0006AC0E File Offset: 0x00068E0E
	private void Start()
	{
		this.olddt = DateTime.Now;
		this.oldTick = (long)Environment.TickCount;
		base.InvokeRepeating("invSpeedHackX", 5f, 5f);
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0006AC3C File Offset: 0x00068E3C
	private void invSpeedHackX()
	{
		TimeSpan timeSpan = DateTime.Now - this.olddt;
		this.olddt = DateTime.Now;
		long num = (long)Environment.TickCount - this.oldTick;
		this.oldTick = (long)Environment.TickCount;
		if (timeSpan.TotalMilliseconds * 1.2999999523162842 < (double)num)
		{
			this.errorCount++;
		}
		if (this.errorCount > 5)
		{
			SceneManager.LoadScene(0);
		}
	}

	// Token: 0x04000B6E RID: 2926
	private DateTime olddt;

	// Token: 0x04000B6F RID: 2927
	private long oldTick;

	// Token: 0x04000B70 RID: 2928
	private int errorCount;
}
