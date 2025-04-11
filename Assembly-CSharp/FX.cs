using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class FX : MonoBehaviour
{
	// Token: 0x06000145 RID: 325 RVA: 0x0001C699 File Offset: 0x0001A899
	private void Awake()
	{
		this.fx = (Resources.Load("Prefab/Infect") as GameObject);
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
	public void Infect()
	{
		base.StartCoroutine(this.r_Infect());
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0001C6BF File Offset: 0x0001A8BF
	private IEnumerator r_Infect()
	{
		GameObject newfx = Object.Instantiate<GameObject>(this.fx);
		newfx.transform.position = base.gameObject.transform.position;
		yield return new WaitForSeconds(1f);
		Object.Destroy(newfx);
		yield break;
	}

	// Token: 0x04000162 RID: 354
	private GameObject fx;
}
