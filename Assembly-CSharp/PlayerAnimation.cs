using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class PlayerAnimation : MonoBehaviour
{
	// Token: 0x060004F5 RID: 1269 RVA: 0x0006204C File Offset: 0x0006024C
	private void Awake()
	{
		this.anim = base.gameObject.GetComponent<Animator>();
		if (this.anim.layerCount == 0)
		{
			base.StartCoroutine(this.layerupdate());
			return;
		}
		this.anim.SetLayerWeight(1, 1f);
		this.anim.SetLayerWeight(2, 1f);
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x000620A7 File Offset: 0x000602A7
	private IEnumerator layerupdate()
	{
		yield return null;
		if (this.anim.layerCount == 0)
		{
			base.StartCoroutine(this.layerupdate());
		}
		else
		{
			this.anim.SetLayerWeight(1, 1f);
			this.anim.SetLayerWeight(2, 1f);
		}
		yield break;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x000620B8 File Offset: 0x000602B8
	private void FixedUpdate()
	{
		if (this.anim == null)
		{
			return;
		}
		if (this.anim.layerCount == 0)
		{
			return;
		}
		AnimatorStateInfo currentAnimatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(1);
		if (currentAnimatorStateInfo.nameHash == PlayerAnimation.Reload_state)
		{
			this.anim.SetBool("inReload", false);
		}
		if (currentAnimatorStateInfo.nameHash == PlayerAnimation.Draw_state)
		{
			this.anim.SetBool("inDraw", false);
		}
		if (currentAnimatorStateInfo.nameHash == PlayerAnimation.Shot_state)
		{
			this.anim.SetBool("inShot", false);
		}
		if (base.GetComponent<AudioSource>().clip == null)
		{
			base.GetComponent<AudioSource>().clip = ContentLoader.LoadSound("walk");
		}
		this.xval_lerp = Mathf.Lerp(this.xval_lerp, this.xval, Time.deltaTime * 5f);
		this.anim.SetFloat("Angle", this.xval_lerp);
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00002B75 File Offset: 0x00000D75
	private void UpdateAim()
	{
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x000621AC File Offset: 0x000603AC
	public void SetSpeed(float fval)
	{
		this.anim.SetFloat("Speed", fval);
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x000621BF File Offset: 0x000603BF
	public void SetWeaponClass()
	{
		this.anim.SetInteger("WeaponClass", 0);
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x000621D2 File Offset: 0x000603D2
	public void SetReload()
	{
		this.anim.SetBool("inReload", true);
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x000621E5 File Offset: 0x000603E5
	public void SetDraw()
	{
		this.anim.SetBool("inDraw", true);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x000621F8 File Offset: 0x000603F8
	public void SetShot()
	{
		this.anim.SetBool("inShot", true);
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0006220B File Offset: 0x0006040B
	public void SetCrouch(bool bval)
	{
		this.anim.SetBool("Crouch", bval);
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0006221E File Offset: 0x0006041E
	public void SetJeepGunner(bool bval)
	{
		this.anim.SetBool("inJeepGunner", bval);
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00062231 File Offset: 0x00060431
	public void SetRotation(float x)
	{
		this.xval = (x - 90f) * -1f - 15f;
		if (this.xval < -90f)
		{
			this.xval = -90f;
		}
	}

	// Token: 0x040008FE RID: 2302
	public Transform[] upperBodyChain;

	// Token: 0x040008FF RID: 2303
	protected Animator anim;

	// Token: 0x04000900 RID: 2304
	protected float sliderval;

	// Token: 0x04000901 RID: 2305
	protected float sliderval_rotation_y;

	// Token: 0x04000902 RID: 2306
	protected bool crouch;

	// Token: 0x04000903 RID: 2307
	private static int Reload_state = Animator.StringToHash("UpperBody.Reload");

	// Token: 0x04000904 RID: 2308
	private static int Draw_state = Animator.StringToHash("UpperBody.Draw");

	// Token: 0x04000905 RID: 2309
	private static int Shot_state = Animator.StringToHash("UpperBody.Shot");

	// Token: 0x04000906 RID: 2310
	private static int JeepGunner_state = Animator.StringToHash("UpperBody.JeepGunner");

	// Token: 0x04000907 RID: 2311
	private float xval;

	// Token: 0x04000908 RID: 2312
	private float xval_lerp;
}
