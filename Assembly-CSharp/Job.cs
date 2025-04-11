using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class Job : ThreadedJob
{
	// Token: 0x06000232 RID: 562 RVA: 0x0002CC80 File Offset: 0x0002AE80
	protected override void ThreadFunction()
	{
		for (int i = 0; i < 100000000; i++)
		{
			this.InData[i % this.InData.Length] += this.InData[(i + 1) % this.InData.Length];
		}
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0002CCDC File Offset: 0x0002AEDC
	protected override void OnFinished()
	{
		for (int i = 0; i < this.InData.Length; i++)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Results(",
				i,
				"): ",
				this.InData[i]
			}));
		}
	}

	// Token: 0x040002CC RID: 716
	public Vector3[] InData;

	// Token: 0x040002CD RID: 717
	public Vector3[] OutData;
}
