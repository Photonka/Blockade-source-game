using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class PreviewAnimation : MonoBehaviour
{
	// Token: 0x06000503 RID: 1283 RVA: 0x000622A2 File Offset: 0x000604A2
	private void Awake()
	{
		this.anim = base.gameObject.GetComponent<Animator>();
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x000622B5 File Offset: 0x000604B5
	public void SetState(int val)
	{
		this.anim.SetInteger("anim", val);
	}

	// Token: 0x04000909 RID: 2313
	protected Animator anim;
}
