using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class Fence : MonoBehaviour
{
	// Token: 0x060002C0 RID: 704 RVA: 0x000394AB File Offset: 0x000376AB
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
	}

	// Token: 0x04000531 RID: 1329
	public int id;

	// Token: 0x04000532 RID: 1330
	public int uid;

	// Token: 0x04000533 RID: 1331
	public int entid;

	// Token: 0x04000534 RID: 1332
	private Client cscl;

	// Token: 0x04000535 RID: 1333
	private EntManager entmanager;

	// Token: 0x04000536 RID: 1334
	private GameObject obj;
}
