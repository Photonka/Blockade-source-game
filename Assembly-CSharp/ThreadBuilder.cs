using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class ThreadBuilder : MonoBehaviour
{
	// Token: 0x06000235 RID: 565 RVA: 0x0002CD44 File Offset: 0x0002AF44
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		for (int i = 0; i < 2; i++)
		{
			this.myJob[i] = new Job();
			this.myJob[i].InData = new Vector3[10];
			this.myJob[i].Start();
		}
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0002CD98 File Offset: 0x0002AF98
	private void Update()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.myJob[i] != null && this.myJob[i].Update())
			{
				this.myJob[i].Restart();
			}
		}
	}

	// Token: 0x040002CE RID: 718
	private Job[] myJob = new Job[2];
}
