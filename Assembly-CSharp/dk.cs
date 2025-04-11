using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class dk : MonoBehaviour
{
	// Token: 0x06000142 RID: 322 RVA: 0x0001C670 File Offset: 0x0001A870
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.8f);
		if (base.GetComponent<Collider>())
		{
			base.GetComponent<Collider>().enabled = false;
		}
		yield return new WaitForSeconds(2f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0001C67F File Offset: 0x0001A87F
	private void KillSelf()
	{
		if (base.gameObject)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
