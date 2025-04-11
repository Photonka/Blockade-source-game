using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class ragdoll_wreck : MonoBehaviour
{
	// Token: 0x0600052D RID: 1325 RVA: 0x000646CF File Offset: 0x000628CF
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(5f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00017ECE File Offset: 0x000160CE
	private void KillSelf()
	{
		Object.Destroy(base.gameObject);
	}
}
