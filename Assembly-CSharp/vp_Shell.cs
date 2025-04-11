using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E6 RID: 230
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
public class vp_Shell : MonoBehaviour
{
	// Token: 0x06000863 RID: 2147 RVA: 0x0007D870 File Offset: 0x0007BA70
	private void Start()
	{
		this.m_Transform = base.transform;
		this.m_Rigidbody = base.GetComponent<Rigidbody>();
		this.m_Audio = base.GetComponent<AudioSource>();
		this.m_RestAngleFunc = null;
		this.m_RemoveTime = Time.time + this.LifeTime;
		this.m_RestTime = Time.time + this.LifeTime * 0.25f;
		this.m_Rigidbody.maxAngularVelocity = 100f;
		base.GetComponent<AudioSource>().playOnAwake = false;
		base.GetComponent<AudioSource>().dopplerLevel = 0f;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0007D900 File Offset: 0x0007BB00
	private void Update()
	{
		if (this.m_RestAngleFunc == null)
		{
			if (Time.time > this.m_RestTime)
			{
				this.DecideRestAngle();
			}
		}
		else
		{
			this.m_RestAngleFunc();
		}
		if (Time.time > this.m_RemoveTime)
		{
			this.m_Transform.localScale = Vector3.Lerp(this.m_Transform.localScale, Vector3.zero, Time.deltaTime * 60f * 0.2f);
			if (Time.time > this.m_RemoveTime + 0.5f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0007D994 File Offset: 0x0007BB94
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > 2f)
		{
			if (Random.value > 0.5f)
			{
				this.m_Rigidbody.AddRelativeTorque(-Random.rotation.eulerAngles * 0.15f);
			}
			else
			{
				this.m_Rigidbody.AddRelativeTorque(Random.rotation.eulerAngles * 0.15f);
			}
			if (this.m_Audio != null && this.m_BounceSounds.Count > 0)
			{
				this.m_Audio.pitch = Time.timeScale;
				this.m_Audio.PlayOneShot(this.m_BounceSounds[Random.Range(0, this.m_BounceSounds.Count)], AudioListener.volume);
				return;
			}
		}
		else if (Random.value > this.m_Persistence)
		{
			base.GetComponent<Collider>().enabled = false;
			this.m_RemoveTime = Time.time + 0.5f;
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0007DA94 File Offset: 0x0007BC94
	protected void DecideRestAngle()
	{
		if (Mathf.Abs(this.m_Transform.eulerAngles.x - 270f) < 55f)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(this.m_Transform.position, Vector3.down), ref raycastHit, 1f) && raycastHit.normal == Vector3.up)
			{
				this.m_RestAngleFunc = new vp_Shell.RestAngleFunc(this.UpRight);
				this.m_Rigidbody.constraints = 80;
			}
			return;
		}
		this.m_RestAngleFunc = new vp_Shell.RestAngleFunc(this.TippedOver);
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0007DB2C File Offset: 0x0007BD2C
	protected void UpRight()
	{
		this.m_Transform.rotation = Quaternion.Lerp(this.m_Transform.rotation, Quaternion.Euler(-90f, this.m_Transform.rotation.y, this.m_Transform.rotation.z), Time.time * (Time.deltaTime * 60f * 0.05f));
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0007DB98 File Offset: 0x0007BD98
	protected void TippedOver()
	{
		this.m_Transform.localRotation = Quaternion.Lerp(this.m_Transform.localRotation, Quaternion.Euler(0f, this.m_Transform.localEulerAngles.y, this.m_Transform.localEulerAngles.z), Time.time * (Time.deltaTime * 60f * 0.005f));
	}

	// Token: 0x04000E23 RID: 3619
	private Transform m_Transform;

	// Token: 0x04000E24 RID: 3620
	private Rigidbody m_Rigidbody;

	// Token: 0x04000E25 RID: 3621
	private AudioSource m_Audio;

	// Token: 0x04000E26 RID: 3622
	public float LifeTime = 10f;

	// Token: 0x04000E27 RID: 3623
	protected float m_RemoveTime;

	// Token: 0x04000E28 RID: 3624
	public float m_Persistence = 1f;

	// Token: 0x04000E29 RID: 3625
	protected vp_Shell.RestAngleFunc m_RestAngleFunc;

	// Token: 0x04000E2A RID: 3626
	protected float m_RestTime;

	// Token: 0x04000E2B RID: 3627
	public List<AudioClip> m_BounceSounds = new List<AudioClip>();

	// Token: 0x020008A3 RID: 2211
	// (Invoke) Token: 0x06004CA1 RID: 19617
	public delegate void RestAngleFunc();
}
