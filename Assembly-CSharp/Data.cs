using System;
using AssemblyCSharp;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class Data : MonoBehaviour
{
	// Token: 0x0600013F RID: 319 RVA: 0x0001C634 File Offset: 0x0001A834
	public void SetIndex(BotsController csbc, int id, int _hitzone)
	{
		this.index = id;
		this.data = csbc.Bots[id];
		this.go = csbc.BotsGmObj[id];
		this.hitzone = _hitzone;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0001C660 File Offset: 0x0001A860
	public void SetIndex(int id, int _hitzone)
	{
		this.index = id;
		this.hitzone = _hitzone;
	}

	// Token: 0x0400015D RID: 349
	public GameObject go;

	// Token: 0x0400015E RID: 350
	public BotData data;

	// Token: 0x0400015F RID: 351
	public int index;

	// Token: 0x04000160 RID: 352
	public int hitzone;

	// Token: 0x04000161 RID: 353
	public bool isGost;
}
