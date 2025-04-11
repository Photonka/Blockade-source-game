using System;
using System.Collections;
using AssemblyCSharp;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class NpcLerp : MonoBehaviour
{
	// Token: 0x06000199 RID: 409 RVA: 0x0002411B File Offset: 0x0002231B
	private void Awake()
	{
		this.anim = base.gameObject.GetComponent<Animator>();
		this.roar_sound = Resources.Load<AudioClip>("boss_ghost_roar");
		this.sound = base.gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00024150 File Offset: 0x00022350
	public void PlayAnimation(int move)
	{
		switch (move)
		{
		case 0:
			this.sound.PlayOneShot(this.roar_sound);
			base.StartCoroutine(this.Roar());
			return;
		case 1:
			base.StartCoroutine(this.Hit1());
			return;
		case 2:
			base.StartCoroutine(this.Hit2());
			return;
		default:
			return;
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x000241AA File Offset: 0x000223AA
	private IEnumerator Roar()
	{
		this.anim.SetBool("roar", true);
		yield return new WaitForSeconds(2.2f);
		if (base.gameObject == null)
		{
			yield break;
		}
		this.anim.SetBool("roar", false);
		yield break;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x000241B9 File Offset: 0x000223B9
	private IEnumerator Hit1()
	{
		this.anim.SetInteger("Hit", 1);
		yield return new WaitForSeconds(4f);
		if (base.gameObject == null)
		{
			yield break;
		}
		this.anim.SetInteger("Hit", 0);
		yield break;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x000241C8 File Offset: 0x000223C8
	private IEnumerator Hit2()
	{
		this.anim.SetInteger("Hit", 2);
		yield return new WaitForSeconds(1.84f);
		if (base.gameObject == null)
		{
			yield break;
		}
		this.anim.SetInteger("Hit", 0);
		yield break;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000241D8 File Offset: 0x000223D8
	private void FixedUpdate()
	{
		if (this.ent.go.name == "Boss")
		{
			Vector3 vector = this.ent.go.transform.position - this.ent.position;
			if ((double)vector.magnitude > 0.05)
			{
				this.anim.SetFloat("Speed", vector.magnitude);
			}
		}
		this.ent.go.transform.position = Vector3.Lerp(this.ent.go.transform.position, this.ent.position, Time.deltaTime * 2.5f);
		float num = this.ent.go.transform.eulerAngles.y;
		float num2 = this.ent.rotation.y - num;
		if (num2 > 180f)
		{
			num += 360f;
		}
		if (num2 < -180f)
		{
			num -= 360f;
		}
		num = Mathf.Lerp(num, this.ent.rotation.y, Time.deltaTime * 5f);
		this.ent.go.transform.eulerAngles = new Vector3(0f, num, 0f);
	}

	// Token: 0x04000170 RID: 368
	public CEnt ent;

	// Token: 0x04000171 RID: 369
	private Animator anim;

	// Token: 0x04000172 RID: 370
	private bool roar;

	// Token: 0x04000173 RID: 371
	private AudioSource sound;

	// Token: 0x04000174 RID: 372
	private AudioClip roar_sound;
}
