using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class Boat : MonoBehaviour
{
	// Token: 0x060002A4 RID: 676 RVA: 0x00038FAD File Offset: 0x000371AD
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
	}

	// Token: 0x04000512 RID: 1298
	public int id;

	// Token: 0x04000513 RID: 1299
	public int uid;

	// Token: 0x04000514 RID: 1300
	public int entid;

	// Token: 0x04000515 RID: 1301
	private Client cscl;

	// Token: 0x04000516 RID: 1302
	private EntManager entmanager;

	// Token: 0x04000517 RID: 1303
	private GameObject obj;
}
