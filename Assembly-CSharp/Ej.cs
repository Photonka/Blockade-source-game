using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class Ej : MonoBehaviour
{
	// Token: 0x060002B5 RID: 693 RVA: 0x0003926C File Offset: 0x0003746C
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
	}

	// Token: 0x04000522 RID: 1314
	public int id;

	// Token: 0x04000523 RID: 1315
	public int uid;

	// Token: 0x04000524 RID: 1316
	public int entid;

	// Token: 0x04000525 RID: 1317
	private Client cscl;

	// Token: 0x04000526 RID: 1318
	private EntManager entmanager;

	// Token: 0x04000527 RID: 1319
	private GameObject obj;
}
