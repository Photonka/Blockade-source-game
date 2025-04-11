using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E2 RID: 226
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public abstract class vp_Pickup : MonoBehaviour
{
	// Token: 0x0600083A RID: 2106 RVA: 0x0007C648 File Offset: 0x0007A848
	protected virtual void Start()
	{
		this.m_Transform = base.transform;
		this.m_Rigidbody = base.GetComponent<Rigidbody>();
		this.m_Audio = base.GetComponent<AudioSource>();
		if (Camera.main != null)
		{
			this.m_CameraMainTransform = Camera.main.transform;
		}
		base.GetComponent<Collider>().isTrigger = true;
		this.m_Audio.clip = this.PickupSound;
		this.m_Audio.playOnAwake = false;
		this.m_Audio.minDistance = 3f;
		this.m_Audio.maxDistance = 150f;
		this.m_Audio.rolloffMode = 1;
		this.m_Audio.dopplerLevel = 0f;
		this.m_SpawnPosition = this.m_Transform.position;
		this.m_SpawnScale = this.m_Transform.localScale;
		this.RespawnScaleUpDuration = ((this.m_Rigidbody == null) ? Mathf.Abs(this.RespawnScaleUpDuration) : 0f);
		if (this.BobOffset == -1f)
		{
			this.BobOffset = Random.value;
		}
		if (this.RecipientTags.Count == 0)
		{
			this.RecipientTags.Add("Player");
		}
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0007A524 File Offset: 0x00078724
	protected virtual void Update()
	{
		this.UpdateMotion();
		if (this.m_Depleted && !this.m_Audio.isPlaying)
		{
			this.Remove();
		}
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0007C778 File Offset: 0x0007A978
	protected virtual void UpdateMotion()
	{
		if (this.m_Rigidbody != null)
		{
			return;
		}
		if (this.Billboard)
		{
			if (this.m_CameraMainTransform != null)
			{
				this.m_Transform.localEulerAngles = this.m_CameraMainTransform.eulerAngles;
			}
		}
		else
		{
			this.m_Transform.localEulerAngles += this.Spin * Time.deltaTime;
		}
		if (this.BobRate != 0f && this.BobAmp != 0f)
		{
			this.m_Transform.position = this.m_SpawnPosition + Vector3.up * (Mathf.Cos((Time.time + this.BobOffset) * (this.BobRate * 10f)) * this.BobAmp);
		}
		if (this.m_Transform.localScale != this.m_SpawnScale)
		{
			this.m_Transform.localScale = Vector3.Lerp(this.m_Transform.localScale, this.m_SpawnScale, Time.deltaTime / this.RespawnScaleUpDuration);
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x0007C88C File Offset: 0x0007AA8C
	protected virtual void OnTriggerEnter(Collider col)
	{
		if (this.m_Depleted)
		{
			return;
		}
		foreach (string b in this.RecipientTags)
		{
			if (col.gameObject.tag == b)
			{
				goto IL_4E;
			}
		}
		return;
		IL_4E:
		if (col != this.m_LastCollider)
		{
			this.m_Recipient = col.gameObject.GetComponent<vp_FPPlayerEventHandler>();
		}
		if (this.m_Recipient == null)
		{
			return;
		}
		if (this.TryGive(this.m_Recipient))
		{
			this.m_Audio.pitch = (this.PickupSoundSlomo ? Time.timeScale : 1f);
			this.m_Audio.Play();
			base.GetComponent<Renderer>().enabled = false;
			this.m_Depleted = true;
			this.m_Recipient.HUDText.Send(this.GiveMessage);
			return;
		}
		if (!this.m_AlreadyFailed)
		{
			this.m_Audio.pitch = (this.FailSoundSlomo ? Time.timeScale : 1f);
			this.m_Audio.PlayOneShot(this.PickupFailSound);
			this.m_AlreadyFailed = true;
			this.m_Recipient.HUDText.Send(this.FailMessage);
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0007C9E8 File Offset: 0x0007ABE8
	protected virtual void OnTriggerExit(Collider col)
	{
		this.m_AlreadyFailed = false;
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0007C9F1 File Offset: 0x0007ABF1
	protected virtual bool TryGive(vp_FPPlayerEventHandler player)
	{
		return player.AddItem.Try(new object[]
		{
			this.InventoryName,
			1
		});
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0007CA20 File Offset: 0x0007AC20
	protected virtual void Remove()
	{
		if (this.RespawnDuration == 0f)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (!this.m_RespawnTimer.Active)
		{
			vp_Utility.Activate(base.gameObject, false);
			vp_Timer.In(this.RespawnDuration, new vp_Timer.Callback(this.Respawn), this.m_RespawnTimer);
		}
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0007CA80 File Offset: 0x0007AC80
	protected virtual void Respawn()
	{
		if (Camera.main != null)
		{
			this.m_CameraMainTransform = Camera.main.transform;
		}
		this.m_RespawnTimer.Cancel();
		this.m_Transform.position = this.m_SpawnPosition;
		if (this.m_Rigidbody == null && this.RespawnScaleUpDuration > 0f)
		{
			this.m_Transform.localScale = Vector3.zero;
		}
		base.GetComponent<Renderer>().enabled = true;
		vp_Utility.Activate(base.gameObject, true);
		this.m_Audio.pitch = (this.RespawnSoundSlomo ? Time.timeScale : 1f);
		this.m_Audio.PlayOneShot(this.RespawnSound);
		this.m_Depleted = false;
		if (this.BobOffset == -1f)
		{
			this.BobOffset = Random.value;
		}
	}

	// Token: 0x04000DE0 RID: 3552
	protected Transform m_Transform;

	// Token: 0x04000DE1 RID: 3553
	protected Rigidbody m_Rigidbody;

	// Token: 0x04000DE2 RID: 3554
	protected AudioSource m_Audio;

	// Token: 0x04000DE3 RID: 3555
	public string InventoryName = "Unnamed";

	// Token: 0x04000DE4 RID: 3556
	public List<string> RecipientTags = new List<string>();

	// Token: 0x04000DE5 RID: 3557
	private Collider m_LastCollider;

	// Token: 0x04000DE6 RID: 3558
	private vp_FPPlayerEventHandler m_Recipient;

	// Token: 0x04000DE7 RID: 3559
	public string GiveMessage = "Picked up an item";

	// Token: 0x04000DE8 RID: 3560
	public string FailMessage = "You currently can't pick up this item!";

	// Token: 0x04000DE9 RID: 3561
	protected Vector3 m_SpawnPosition = Vector3.zero;

	// Token: 0x04000DEA RID: 3562
	protected Vector3 m_SpawnScale = Vector3.zero;

	// Token: 0x04000DEB RID: 3563
	public bool Billboard;

	// Token: 0x04000DEC RID: 3564
	public Vector3 Spin = Vector3.zero;

	// Token: 0x04000DED RID: 3565
	public float BobAmp;

	// Token: 0x04000DEE RID: 3566
	public float BobRate;

	// Token: 0x04000DEF RID: 3567
	public float BobOffset = -1f;

	// Token: 0x04000DF0 RID: 3568
	public float RespawnDuration = 10f;

	// Token: 0x04000DF1 RID: 3569
	public float RespawnScaleUpDuration;

	// Token: 0x04000DF2 RID: 3570
	public AudioClip PickupSound;

	// Token: 0x04000DF3 RID: 3571
	public AudioClip PickupFailSound;

	// Token: 0x04000DF4 RID: 3572
	public AudioClip RespawnSound;

	// Token: 0x04000DF5 RID: 3573
	public bool PickupSoundSlomo = true;

	// Token: 0x04000DF6 RID: 3574
	public bool FailSoundSlomo = true;

	// Token: 0x04000DF7 RID: 3575
	public bool RespawnSoundSlomo = true;

	// Token: 0x04000DF8 RID: 3576
	protected bool m_Depleted;

	// Token: 0x04000DF9 RID: 3577
	protected bool m_AlreadyFailed;

	// Token: 0x04000DFA RID: 3578
	protected vp_Timer.Handle m_RespawnTimer = new vp_Timer.Handle();

	// Token: 0x04000DFB RID: 3579
	private Transform m_CameraMainTransform;
}
