using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DE RID: 222
[RequireComponent(typeof(AudioSource))]
public class vp_Explosion : MonoBehaviour
{
	// Token: 0x06000814 RID: 2068 RVA: 0x0007AEA4 File Offset: 0x000790A4
	private void Awake()
	{
		Transform transform = base.transform;
		this.m_Audio = base.GetComponent<AudioSource>();
		foreach (GameObject gameObject in this.FXPrefabs)
		{
			Component[] components = gameObject.GetComponents<vp_Explosion>();
			if (components.Length == 0)
			{
				Object.Instantiate<GameObject>(gameObject, transform.position, transform.rotation);
			}
			else
			{
				Debug.LogError("Error: vp_Explosion->FXPrefab must not be a vp_Explosion (risk of infinite loop).");
			}
		}
		foreach (Collider collider in Physics.OverlapSphere(transform.position, this.Radius, -738197525))
		{
			if (collider != base.GetComponent<Collider>())
			{
				float num = 1f - Vector3.Distance(transform.position, collider.transform.position) / this.Radius;
				if (collider.GetComponent<Rigidbody>())
				{
					RaycastHit raycastHit;
					if (!Physics.Raycast(new Ray(collider.transform.position, -Vector3.up), ref raycastHit, 1f))
					{
						this.UpForce = 0f;
					}
					collider.GetComponent<Rigidbody>().AddExplosionForce(this.Force / Time.timeScale, transform.position, this.Radius, this.UpForce);
				}
				else if (collider.gameObject.layer == 30)
				{
					vp_FPPlayerEventHandler component = collider.GetComponent<vp_FPPlayerEventHandler>();
					if (component)
					{
						component.ForceImpact.Send((collider.transform.position - transform.position).normalized * this.Force * 0.001f * num);
						component.BombShake.Send(num * this.CameraShake);
					}
				}
				if (collider.gameObject.layer != 29)
				{
					collider.gameObject.BroadcastMessage(this.DamageMessageName, num * this.Damage, 1);
				}
			}
		}
		this.m_Audio.clip = this.Sound;
		this.m_Audio.pitch = Random.Range(this.SoundMinPitch, this.SoundMaxPitch) * Time.timeScale;
		if (!this.m_Audio.playOnAwake)
		{
			this.m_Audio.Play();
		}
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x0007B114 File Offset: 0x00079314
	private void Update()
	{
		if (!this.m_Audio.isPlaying)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000D96 RID: 3478
	public float Radius = 15f;

	// Token: 0x04000D97 RID: 3479
	public float Force = 1000f;

	// Token: 0x04000D98 RID: 3480
	public float UpForce = 10f;

	// Token: 0x04000D99 RID: 3481
	public float Damage = 10f;

	// Token: 0x04000D9A RID: 3482
	public float CameraShake = 1f;

	// Token: 0x04000D9B RID: 3483
	public string DamageMessageName = "Damage";

	// Token: 0x04000D9C RID: 3484
	private AudioSource m_Audio;

	// Token: 0x04000D9D RID: 3485
	public AudioClip Sound;

	// Token: 0x04000D9E RID: 3486
	public float SoundMinPitch = 0.8f;

	// Token: 0x04000D9F RID: 3487
	public float SoundMaxPitch = 1.2f;

	// Token: 0x04000DA0 RID: 3488
	public List<GameObject> FXPrefabs = new List<GameObject>();
}
