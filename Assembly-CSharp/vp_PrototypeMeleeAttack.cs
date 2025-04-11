using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class vp_PrototypeMeleeAttack : vp_Component
{
	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000855 RID: 2133 RVA: 0x0007D112 File Offset: 0x0007B312
	private vp_FPPlayerEventHandler Player
	{
		get
		{
			if (this.m_Player == null && this.EventHandler != null)
			{
				this.m_Player = (vp_FPPlayerEventHandler)this.EventHandler;
			}
			return this.m_Player;
		}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0007D148 File Offset: 0x0007B348
	protected override void Start()
	{
		base.Start();
		this.m_Controller = (vp_FPController)base.Root.GetComponent(typeof(vp_FPController));
		this.m_Camera = (vp_FPCamera)base.Root.GetComponentInChildren(typeof(vp_FPCamera));
		this.m_Weapon = (vp_FPWeapon)base.Transform.GetComponent(typeof(vp_FPWeapon));
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0007D1BB File Offset: 0x0007B3BB
	protected override void Update()
	{
		base.Update();
		this.UpdateAttack();
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0007D1CC File Offset: 0x0007B3CC
	private void UpdateAttack()
	{
		if (!this.Player.Attack.Active)
		{
			return;
		}
		if (this.Player.SetWeapon.Active)
		{
			return;
		}
		if (this.m_Weapon == null)
		{
			return;
		}
		if (!this.m_Weapon.Wielded)
		{
			return;
		}
		if (Time.time < this.m_NextAllowedSwingTime)
		{
			return;
		}
		this.m_NextAllowedSwingTime = Time.time + this.SwingRate;
		this.PickAttack();
		vp_Timer.In(this.SwingDelay, delegate()
		{
			if (this.SoundSwing.Count > 0)
			{
				base.Audio.pitch = Random.Range(this.SoundSwingPitch.x, this.SoundSwingPitch.y) * Time.timeScale;
				base.Audio.PlayOneShot((AudioClip)this.SoundSwing[Random.Range(0, this.SoundSwing.Count)], AudioListener.volume);
			}
			this.m_Weapon.SetState(this.WeaponStateCharge, false, false, false);
			this.m_Weapon.SetState(this.WeaponStateSwing, true, false, false);
			this.m_Weapon.Refresh();
			this.m_Weapon.AddSoftForce(this.SwingPositionSoftForce, this.SwingRotationSoftForce, this.SwingSoftForceFrames);
			vp_Timer.In(this.ImpactTime, delegate()
			{
				RaycastHit hit;
				Physics.SphereCast(new Ray(new Vector3(this.m_Controller.Transform.position.x, this.m_Camera.Transform.position.y, this.m_Controller.Transform.position.z), this.m_Camera.Transform.forward), this.DamageRadius, ref hit, this.DamageRange, -1811939349);
				if (hit.collider != null)
				{
					this.SpawnImpactFX(hit);
					this.ApplyDamage(hit);
					this.ApplyRecoil();
					return;
				}
				vp_Timer.In(this.SwingDuration - this.ImpactTime, delegate()
				{
					this.m_Weapon.StopSprings();
					this.Reset();
				}, this.SwingDurationTimer);
			}, this.ImpactTimer);
		}, this.SwingDelayTimer);
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0007D260 File Offset: 0x0007B460
	private void PickAttack()
	{
		int num;
		do
		{
			num = Random.Range(0, this.States.Count - 1);
		}
		while (this.States.Count > 1 && num == this.m_AttackCurrent && Random.value < 0.5f);
		this.m_AttackCurrent = num;
		base.SetState(this.States[this.m_AttackCurrent].Name, true, false, false);
		this.m_Weapon.SetState(this.WeaponStateCharge, true, false, false);
		this.m_Weapon.Refresh();
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0007D2EC File Offset: 0x0007B4EC
	private void SpawnImpactFX(RaycastHit hit)
	{
		Quaternion quaternion = Quaternion.LookRotation(hit.normal);
		if (this.m_DustPrefab != null)
		{
			Object.Instantiate<GameObject>(this.m_DustPrefab, hit.point, quaternion);
		}
		if (this.m_SparkPrefab != null && Random.value < this.SparkFactor)
		{
			Object.Instantiate<GameObject>(this.m_SparkPrefab, hit.point, quaternion);
		}
		if (this.m_DebrisPrefab != null)
		{
			Object.Instantiate<GameObject>(this.m_DebrisPrefab, hit.point, quaternion);
		}
		if (this.SoundImpact.Count > 0)
		{
			base.Audio.pitch = Random.Range(this.SoundImpactPitch.x, this.SoundImpactPitch.y) * Time.timeScale;
			base.Audio.PlayOneShot((AudioClip)this.SoundImpact[Random.Range(0, this.SoundImpact.Count)], AudioListener.volume);
		}
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0007D3E4 File Offset: 0x0007B5E4
	private void ApplyDamage(RaycastHit hit)
	{
		hit.collider.SendMessage(this.DamageMethodName, this.Damage, 1);
		Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
		if (attachedRigidbody != null && !attachedRigidbody.isKinematic)
		{
			attachedRigidbody.AddForceAtPosition(this.m_Camera.Transform.forward * this.DamageForce / Time.timeScale, hit.point);
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0007D460 File Offset: 0x0007B660
	private void ApplyRecoil()
	{
		this.m_Weapon.StopSprings();
		this.m_Weapon.AddForce(this.ImpactPositionSpringRecoil, this.ImpactRotationSpringRecoil);
		this.m_Weapon.AddForce2(this.ImpactPositionSpring2Recoil, this.ImpactRotationSpring2Recoil);
		this.Reset();
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0007D4AC File Offset: 0x0007B6AC
	private void Reset()
	{
		vp_Timer.In(0.05f, delegate()
		{
			if (this.m_Weapon != null)
			{
				this.m_Weapon.SetState(this.WeaponStateCharge, false, false, false);
				this.m_Weapon.SetState(this.WeaponStateSwing, false, false, false);
				this.m_Weapon.Refresh();
				base.ResetState();
			}
		}, this.ResetTimer);
	}

	// Token: 0x04000DFF RID: 3583
	private vp_FPWeapon m_Weapon;

	// Token: 0x04000E00 RID: 3584
	private vp_FPController m_Controller;

	// Token: 0x04000E01 RID: 3585
	private vp_FPCamera m_Camera;

	// Token: 0x04000E02 RID: 3586
	public string WeaponStateCharge = "Charge";

	// Token: 0x04000E03 RID: 3587
	public string WeaponStateSwing = "Swing";

	// Token: 0x04000E04 RID: 3588
	public float SwingDelay = 0.5f;

	// Token: 0x04000E05 RID: 3589
	public float SwingDuration = 0.5f;

	// Token: 0x04000E06 RID: 3590
	public float SwingRate = 1f;

	// Token: 0x04000E07 RID: 3591
	protected float m_NextAllowedSwingTime;

	// Token: 0x04000E08 RID: 3592
	public int SwingSoftForceFrames = 50;

	// Token: 0x04000E09 RID: 3593
	public Vector3 SwingPositionSoftForce = new Vector3(-0.5f, -0.1f, 0.3f);

	// Token: 0x04000E0A RID: 3594
	public Vector3 SwingRotationSoftForce = new Vector3(50f, -25f, 0f);

	// Token: 0x04000E0B RID: 3595
	public float ImpactTime = 0.11f;

	// Token: 0x04000E0C RID: 3596
	public Vector3 ImpactPositionSpringRecoil = new Vector3(0.01f, 0.03f, -0.05f);

	// Token: 0x04000E0D RID: 3597
	public Vector3 ImpactPositionSpring2Recoil = Vector3.zero;

	// Token: 0x04000E0E RID: 3598
	public Vector3 ImpactRotationSpringRecoil = Vector3.zero;

	// Token: 0x04000E0F RID: 3599
	public Vector3 ImpactRotationSpring2Recoil = new Vector3(0f, 0f, 10f);

	// Token: 0x04000E10 RID: 3600
	public string DamageMethodName = "Damage";

	// Token: 0x04000E11 RID: 3601
	public float Damage = 5f;

	// Token: 0x04000E12 RID: 3602
	public float DamageRadius = 0.3f;

	// Token: 0x04000E13 RID: 3603
	public float DamageRange = 2f;

	// Token: 0x04000E14 RID: 3604
	public float DamageForce = 1000f;

	// Token: 0x04000E15 RID: 3605
	protected int m_AttackCurrent;

	// Token: 0x04000E16 RID: 3606
	public float SparkFactor = 0.1f;

	// Token: 0x04000E17 RID: 3607
	public GameObject m_DustPrefab;

	// Token: 0x04000E18 RID: 3608
	public GameObject m_SparkPrefab;

	// Token: 0x04000E19 RID: 3609
	public GameObject m_DebrisPrefab;

	// Token: 0x04000E1A RID: 3610
	public List<Object> SoundSwing = new List<Object>();

	// Token: 0x04000E1B RID: 3611
	public List<Object> SoundImpact = new List<Object>();

	// Token: 0x04000E1C RID: 3612
	public Vector2 SoundSwingPitch = new Vector2(0.5f, 1.5f);

	// Token: 0x04000E1D RID: 3613
	public Vector2 SoundImpactPitch = new Vector2(1f, 1.5f);

	// Token: 0x04000E1E RID: 3614
	private vp_Timer.Handle SwingDelayTimer = new vp_Timer.Handle();

	// Token: 0x04000E1F RID: 3615
	private vp_Timer.Handle ImpactTimer = new vp_Timer.Handle();

	// Token: 0x04000E20 RID: 3616
	private vp_Timer.Handle SwingDurationTimer = new vp_Timer.Handle();

	// Token: 0x04000E21 RID: 3617
	private vp_Timer.Handle ResetTimer = new vp_Timer.Handle();

	// Token: 0x04000E22 RID: 3618
	private vp_FPPlayerEventHandler m_Player;
}
