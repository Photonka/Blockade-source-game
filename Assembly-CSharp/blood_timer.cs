using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class blood_timer : MonoBehaviour
{
	// Token: 0x0600011B RID: 283 RVA: 0x00017EBF File Offset: 0x000160BF
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.2f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00017ECE File Offset: 0x000160CE
	private void KillSelf()
	{
		Object.Destroy(base.gameObject);
	}
}
