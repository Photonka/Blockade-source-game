using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class flash_timer : MonoBehaviour
{
	// Token: 0x0600034C RID: 844 RVA: 0x0003BD99 File Offset: 0x00039F99
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(2f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00017ECE File Offset: 0x000160CE
	private void KillSelf()
	{
		Object.Destroy(base.gameObject);
	}
}
