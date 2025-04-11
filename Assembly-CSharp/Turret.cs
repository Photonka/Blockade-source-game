using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class Turret : MonoBehaviour
{
	// Token: 0x06000342 RID: 834 RVA: 0x0003BCA3 File Offset: 0x00039EA3
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
	}

	// Token: 0x040005E2 RID: 1506
	public int id;

	// Token: 0x040005E3 RID: 1507
	public int uid;

	// Token: 0x040005E4 RID: 1508
	public int entid;

	// Token: 0x040005E5 RID: 1509
	private Client cscl;

	// Token: 0x040005E6 RID: 1510
	private EntManager entmanager;

	// Token: 0x040005E7 RID: 1511
	private GameObject obj;
}
