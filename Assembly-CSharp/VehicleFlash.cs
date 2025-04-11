using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000073 RID: 115
public class VehicleFlash : MonoBehaviour
{
	// Token: 0x06000344 RID: 836 RVA: 0x0003BCDF File Offset: 0x00039EDF
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(2.5f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0003BCEE File Offset: 0x00039EEE
	private void KillSelf()
	{
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040005E8 RID: 1512
	public int id;

	// Token: 0x040005E9 RID: 1513
	public int uid;

	// Token: 0x040005EA RID: 1514
	public int entid;
}
