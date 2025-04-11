using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class vp_DamageHandler : MonoBehaviour
{
	// Token: 0x060007F7 RID: 2039 RVA: 0x0007A62E File Offset: 0x0007882E
	protected virtual void Awake()
	{
		this.m_Audio = base.GetComponent<AudioSource>();
		this.m_CurrentHealth = this.MaxHealth;
		this.m_StartPosition = base.transform.position;
		this.m_StartRotation = base.transform.rotation;
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0007A66C File Offset: 0x0007886C
	public virtual void Damage(float damage)
	{
		if (!base.enabled)
		{
			return;
		}
		if (!vp_Utility.IsActive(base.gameObject))
		{
			return;
		}
		if (this.m_CurrentHealth <= 0f)
		{
			return;
		}
		this.m_CurrentHealth = Mathf.Min(this.m_CurrentHealth - damage, this.MaxHealth);
		if (this.m_CurrentHealth <= 0f)
		{
			if (this.m_Audio != null)
			{
				if (this.sound == null)
				{
					this.sound = (Sound)Object.FindObjectOfType(typeof(Sound));
				}
				if (this.DeathSound == null)
				{
					this.DeathSound = this.sound.GetDeath();
				}
				this.m_Audio.pitch = Time.timeScale;
				this.m_Audio.PlayOneShot(this.DeathSound, AudioListener.volume);
			}
			vp_Timer.In(Random.Range(this.MinDeathDelay, this.MaxDeathDelay), new vp_Timer.Callback(this.Die), null);
			return;
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0007A768 File Offset: 0x00078968
	public virtual void Die()
	{
		if (!base.enabled || !vp_Utility.IsActive(base.gameObject))
		{
			return;
		}
		this.RemoveBulletHoles();
		vp_Utility.Activate(base.gameObject, false);
		if (this.DeathEffect != null)
		{
			Object.Instantiate<GameObject>(this.DeathEffect, base.transform.position, base.transform.rotation);
		}
		if (this.Respawns)
		{
			vp_Timer.In(Random.Range(this.MinRespawnTime, this.MaxRespawnTime), new vp_Timer.Callback(this.Respawn), null);
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0007A808 File Offset: 0x00078A08
	protected virtual void Respawn()
	{
		if (this == null)
		{
			return;
		}
		if (Physics.CheckSphere(this.m_StartPosition, this.RespawnCheckRadius, 1342177280))
		{
			vp_Timer.In(Random.Range(this.MinRespawnTime, this.MaxRespawnTime), new vp_Timer.Callback(this.Respawn), null);
			return;
		}
		this.Reset();
		this.Reactivate();
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x0007A868 File Offset: 0x00078A68
	protected virtual void Reset()
	{
		this.m_CurrentHealth = this.MaxHealth;
		base.transform.position = this.m_StartPosition;
		base.transform.rotation = this.m_StartRotation;
		if (base.GetComponent<Rigidbody>() != null)
		{
			base.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0007A8D4 File Offset: 0x00078AD4
	protected virtual void Reactivate()
	{
		vp_Utility.Activate(base.gameObject, true);
		if (this.m_Audio != null)
		{
			this.m_Audio.pitch = Time.timeScale;
			this.m_Audio.PlayOneShot(this.RespawnSound, AudioListener.volume);
		}
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x0007A924 File Offset: 0x00078B24
	protected virtual void RemoveBulletHoles()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			Component[] components = transform.GetComponents<vp_HitscanBullet>();
			if (components.Length != 0)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x04000D7C RID: 3452
	public float MaxHealth = 1f;

	// Token: 0x04000D7D RID: 3453
	public GameObject DeathEffect;

	// Token: 0x04000D7E RID: 3454
	public float MinDeathDelay;

	// Token: 0x04000D7F RID: 3455
	public float MaxDeathDelay;

	// Token: 0x04000D80 RID: 3456
	public float m_CurrentHealth;

	// Token: 0x04000D81 RID: 3457
	public bool Respawns = true;

	// Token: 0x04000D82 RID: 3458
	public float MinRespawnTime = 3f;

	// Token: 0x04000D83 RID: 3459
	public float MaxRespawnTime = 3f;

	// Token: 0x04000D84 RID: 3460
	public float RespawnCheckRadius = 1f;

	// Token: 0x04000D85 RID: 3461
	protected AudioSource m_Audio;

	// Token: 0x04000D86 RID: 3462
	public AudioClip DeathSound;

	// Token: 0x04000D87 RID: 3463
	public AudioClip RespawnSound;

	// Token: 0x04000D88 RID: 3464
	protected Vector3 m_StartPosition;

	// Token: 0x04000D89 RID: 3465
	protected Quaternion m_StartRotation;

	// Token: 0x04000D8A RID: 3466
	public Sound sound;
}
