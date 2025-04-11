using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
[RequireComponent(typeof(AudioSource))]
public class vp_Shooter : vp_Component
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00079B08 File Offset: 0x00077D08
	public GameObject MuzzleFlash
	{
		get
		{
			return this.m_MuzzleFlash;
		}
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00079B10 File Offset: 0x00077D10
	protected override void Awake()
	{
		base.Awake();
		this.m_OperatorTransform = base.Transform;
		this.m_CharacterController = this.m_OperatorTransform.root.GetComponentInChildren<CharacterController>();
		this.m_NextAllowedFireTime = Time.time;
		this.m_SecondNextAllowedFireTime = Time.time;
		this.ProjectileSpawnDelay = Mathf.Min(this.ProjectileSpawnDelay, this.ProjectileFiringRate - 0.1f);
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00079B78 File Offset: 0x00077D78
	protected override void Start()
	{
		base.Start();
		if (this.MuzzleFlashPrefab != null)
		{
			this.m_MuzzleFlash = Object.Instantiate<GameObject>(this.MuzzleFlashPrefab, this.m_OperatorTransform.position, this.m_OperatorTransform.rotation);
			this.m_MuzzleFlash.name = base.transform.name + "MuzzleFlash";
			this.m_MuzzleFlash.transform.parent = this.m_OperatorTransform;
		}
		base.Audio.playOnAwake = false;
		base.Audio.dopplerLevel = 0f;
		base.RefreshDefaultState();
		this.Refresh();
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00079C1E File Offset: 0x00077E1E
	public virtual void TryFire()
	{
		if (this.secondFire)
		{
			if (Time.time < this.m_SecondNextAllowedFireTime)
			{
				return;
			}
		}
		else if (Time.time < this.m_NextAllowedFireTime)
		{
			return;
		}
		this.Fire();
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00079C4C File Offset: 0x00077E4C
	protected virtual void Fire()
	{
		if (!this.secondFire)
		{
			this.m_NextAllowedFireTime = Time.time + this.ProjectileFiringRate;
		}
		else
		{
			this.m_SecondNextAllowedFireTime = Time.time + this.ProjectileFiringRate;
		}
		if (this.SoundFireDelay == 0f)
		{
			this.PlayFireSound();
		}
		else
		{
			vp_Timer.In(this.SoundFireDelay, new vp_Timer.Callback(this.PlayFireSound), null);
		}
		if (this.ProjectileSpawnDelay == 0f)
		{
			this.SpawnProjectiles();
		}
		else
		{
			vp_Timer.In(this.ProjectileSpawnDelay, new vp_Timer.Callback(this.SpawnProjectiles), null);
		}
		if (this.ShellEjectDelay == 0f)
		{
			this.EjectShell();
		}
		else
		{
			vp_Timer.In(this.ShellEjectDelay, new vp_Timer.Callback(this.EjectShell), null);
		}
		if (this.MuzzleFlashDelay == 0f)
		{
			this.ShowMuzzleFlash();
			return;
		}
		vp_Timer.In(this.MuzzleFlashDelay, new vp_Timer.Callback(this.ShowMuzzleFlash), null);
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00079D40 File Offset: 0x00077F40
	protected virtual void PlayFireSound()
	{
		if (base.Audio == null)
		{
			return;
		}
		if (this.sound == null)
		{
			this.sound = (Sound)Object.FindObjectOfType(typeof(Sound));
		}
		if (this.SoundFire == null)
		{
			this.SoundFire = this.sound.ReturnSound_Weapon(base.GetComponent<vp_FPWeapon>().WeaponID);
		}
		if (this.currentFiringWeaponID != 315)
		{
			base.Audio.pitch = Random.Range(this.SoundFirePitch.x, this.SoundFirePitch.y) * Time.timeScale;
			base.Audio.clip = this.SoundFire;
			base.Audio.Play();
			return;
		}
		this.flameFiringTimer = Time.time + 0.15f;
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00079E18 File Offset: 0x00078018
	protected override void Update()
	{
		base.Update();
		if (this.currentFiringWeaponID != 315)
		{
			return;
		}
		if (this.flameFiringTimer < Time.time)
		{
			if (base.Audio.isPlaying && base.Audio.clip != SoundManager.weapon_flamethrower_end)
			{
				base.Audio.pitch = 1f;
				base.Audio.loop = false;
				base.Audio.clip = SoundManager.weapon_flamethrower_end;
				base.Audio.Play();
			}
			return;
		}
		if (!base.Audio.isPlaying && base.Audio.clip != SoundManager.weapon_flamethrower_start)
		{
			base.Audio.pitch = 1f;
			base.Audio.loop = false;
			base.Audio.clip = SoundManager.weapon_flamethrower_start;
			base.Audio.Play();
			vp_Timer.In(SoundManager.weapon_flamethrower_start.length - 0.1f, delegate()
			{
				if (this.flameFiringTimer > Time.time && base.Audio.clip != this.SoundFire)
				{
					base.Audio.pitch = 1f;
					base.Audio.loop = true;
					base.Audio.clip = this.SoundFire;
					base.Audio.Play();
				}
			}, null);
		}
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00079F20 File Offset: 0x00078120
	protected virtual void SpawnProjectiles()
	{
		for (int i = 0; i < this.ProjectileCount; i++)
		{
			if (this.ProjectilePrefab != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.ProjectilePrefab, this.m_OperatorTransform.position, this.m_OperatorTransform.rotation);
				gameObject.transform.localScale = new Vector3(this.ProjectileScale, this.ProjectileScale, this.ProjectileScale);
				gameObject.transform.Rotate(0f, 0f, (float)Random.Range(0, 360));
				gameObject.transform.Rotate(0f, Random.Range(-this.ProjectileSpread, this.ProjectileSpread), 0f);
			}
		}
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00079FDF File Offset: 0x000781DF
	protected virtual void ShowMuzzleFlash()
	{
		if (this.m_MuzzleFlash == null)
		{
			return;
		}
		this.m_MuzzleFlash.SendMessage("Shoot", 1);
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0007A004 File Offset: 0x00078204
	protected virtual void EjectShell()
	{
		if (this.ShellPrefab == null)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.ShellPrefab, this.m_OperatorTransform.position + this.m_OperatorTransform.TransformDirection(this.ShellEjectPosition), this.m_OperatorTransform.rotation);
		gameObject.transform.localScale = new Vector3(this.ShellScale, this.ShellScale, this.ShellScale);
		vp_Layer.Set(gameObject.gameObject, 29, false);
		if (gameObject.GetComponent<Rigidbody>())
		{
			Vector3 vector = base.transform.TransformDirection(this.ShellEjectDirection) * this.ShellEjectVelocity;
			gameObject.GetComponent<Rigidbody>().AddForce(vector, 1);
		}
		if (this.m_CharacterController)
		{
			Vector3 velocity = this.m_CharacterController.velocity;
			gameObject.GetComponent<Rigidbody>().AddForce(velocity, 2);
		}
		if (this.ShellEjectSpin > 0f)
		{
			if (Random.value > 0.5f)
			{
				gameObject.GetComponent<Rigidbody>().AddRelativeTorque(-Random.rotation.eulerAngles * this.ShellEjectSpin);
				return;
			}
			gameObject.GetComponent<Rigidbody>().AddRelativeTorque(Random.rotation.eulerAngles * this.ShellEjectSpin);
		}
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0007A14C File Offset: 0x0007834C
	public virtual void DisableFiring(float seconds = 10000000f)
	{
		this.m_NextAllowedFireTime = Time.time + seconds;
		this.m_SecondNextAllowedFireTime = Time.time + seconds;
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0007A168 File Offset: 0x00078368
	public virtual void EnableFiring()
	{
		this.m_NextAllowedFireTime = Time.time;
		this.m_SecondNextAllowedFireTime = Time.time;
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0007A180 File Offset: 0x00078380
	public override void Refresh()
	{
		if (this.m_MuzzleFlash != null)
		{
			this.m_MuzzleFlash.transform.localPosition = this.MuzzleFlashPosition;
			this.m_MuzzleFlash.transform.localScale = this.MuzzleFlashScale;
			this.m_MuzzleFlash.SendMessage("SetFadeSpeed", this.MuzzleFlashFadeSpeed, 1);
		}
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0007A1E3 File Offset: 0x000783E3
	public override void Activate()
	{
		base.Activate();
		if (this.m_MuzzleFlash != null)
		{
			vp_Utility.Activate(this.m_MuzzleFlash, true);
		}
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0007A205 File Offset: 0x00078405
	public override void Deactivate()
	{
		base.Deactivate();
		if (this.m_MuzzleFlash != null)
		{
			vp_Utility.Activate(this.m_MuzzleFlash, false);
		}
	}

	// Token: 0x04000D5A RID: 3418
	protected CharacterController m_CharacterController;

	// Token: 0x04000D5B RID: 3419
	protected Transform m_OperatorTransform;

	// Token: 0x04000D5C RID: 3420
	public GameObject ProjectilePrefab;

	// Token: 0x04000D5D RID: 3421
	public float ProjectileScale = 1f;

	// Token: 0x04000D5E RID: 3422
	public float ProjectileFiringRate = 0.3f;

	// Token: 0x04000D5F RID: 3423
	public float ProjectileSpawnDelay;

	// Token: 0x04000D60 RID: 3424
	public int ProjectileCount = 1;

	// Token: 0x04000D61 RID: 3425
	public float ProjectileSpread;

	// Token: 0x04000D62 RID: 3426
	protected float m_NextAllowedFireTime;

	// Token: 0x04000D63 RID: 3427
	protected float m_SecondNextAllowedFireTime;

	// Token: 0x04000D64 RID: 3428
	protected bool secondFire;

	// Token: 0x04000D65 RID: 3429
	protected int currentFiringWeaponID;

	// Token: 0x04000D66 RID: 3430
	protected float flameFiringTimer;

	// Token: 0x04000D67 RID: 3431
	public Vector3 MuzzleFlashPosition = Vector3.zero;

	// Token: 0x04000D68 RID: 3432
	public Vector3 MuzzleFlashScale = Vector3.one;

	// Token: 0x04000D69 RID: 3433
	public float MuzzleFlashFadeSpeed = 0.075f;

	// Token: 0x04000D6A RID: 3434
	public GameObject MuzzleFlashPrefab;

	// Token: 0x04000D6B RID: 3435
	public float MuzzleFlashDelay;

	// Token: 0x04000D6C RID: 3436
	protected GameObject m_MuzzleFlash;

	// Token: 0x04000D6D RID: 3437
	public GameObject ShellPrefab;

	// Token: 0x04000D6E RID: 3438
	public float ShellScale = 1f;

	// Token: 0x04000D6F RID: 3439
	public Vector3 ShellEjectDirection = new Vector3(1f, 1f, 1f);

	// Token: 0x04000D70 RID: 3440
	public Vector3 ShellEjectPosition = new Vector3(1f, 0f, 1f);

	// Token: 0x04000D71 RID: 3441
	public float ShellEjectVelocity = 0.2f;

	// Token: 0x04000D72 RID: 3442
	public float ShellEjectDelay;

	// Token: 0x04000D73 RID: 3443
	public float ShellEjectSpin;

	// Token: 0x04000D74 RID: 3444
	public AudioClip SoundFire;

	// Token: 0x04000D75 RID: 3445
	public float SoundFireDelay;

	// Token: 0x04000D76 RID: 3446
	public Vector2 SoundFirePitch = new Vector2(1f, 1f);

	// Token: 0x04000D77 RID: 3447
	public Sound sound;
}
