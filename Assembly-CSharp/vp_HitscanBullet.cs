using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DF RID: 223
[RequireComponent(typeof(AudioSource))]
public class vp_HitscanBullet : MonoBehaviour
{
	// Token: 0x06000817 RID: 2071 RVA: 0x0007B1A8 File Offset: 0x000793A8
	private void Start()
	{
		Transform transform = base.transform;
		this.m_Audio = base.GetComponent<AudioSource>();
		Ray ray;
		ray..ctor(transform.position, base.transform.forward);
		RaycastHit raycastHit;
		if (!Physics.Raycast(ray, ref raycastHit, this.Range, this.IgnoreLocalPlayer ? -1811939349 : -738197525))
		{
			Object.Destroy(base.gameObject);
			return;
		}
		Vector3 localScale = transform.localScale;
		transform.parent = raycastHit.transform;
		transform.localPosition = raycastHit.transform.InverseTransformPoint(raycastHit.point);
		transform.rotation = Quaternion.LookRotation(raycastHit.normal);
		if (raycastHit.transform.lossyScale == Vector3.one)
		{
			transform.Rotate(Vector3.forward, (float)Random.Range(0, 360), 1);
		}
		else
		{
			transform.parent = null;
			transform.localScale = localScale;
			transform.parent = raycastHit.transform;
		}
		Rigidbody attachedRigidbody = raycastHit.collider.attachedRigidbody;
		if (attachedRigidbody != null && !attachedRigidbody.isKinematic)
		{
			attachedRigidbody.AddForceAtPosition(ray.direction * this.Force / Time.timeScale, raycastHit.point);
		}
		if (this.m_ImpactPrefab != null)
		{
			Object.Instantiate<GameObject>(this.m_ImpactPrefab, transform.position, transform.rotation);
		}
		if (this.m_DustPrefab != null)
		{
			Object.Instantiate<GameObject>(this.m_DustPrefab, transform.position, transform.rotation);
		}
		if (this.m_SparkPrefab != null && Random.value < this.m_SparkFactor)
		{
			Object.Instantiate<GameObject>(this.m_SparkPrefab, transform.position, transform.rotation);
		}
		if (this.m_DebrisPrefab != null)
		{
			Object.Instantiate<GameObject>(this.m_DebrisPrefab, transform.position, transform.rotation);
		}
		if (this.m_ImpactSounds.Count > 0)
		{
			this.m_Audio.pitch = Random.Range(this.SoundImpactPitch.x, this.SoundImpactPitch.y) * Time.timeScale;
			this.m_Audio.PlayOneShot(this.m_ImpactSounds[Random.Range(0, this.m_ImpactSounds.Count)], AudioListener.volume);
		}
		raycastHit.collider.SendMessageUpwards(this.DamageMethodName, this.Damage, 1);
		if (this.NoDecalOnTheseLayers.Length != 0)
		{
			foreach (int num in this.NoDecalOnTheseLayers)
			{
				if (raycastHit.transform.gameObject.layer == num)
				{
					this.TryDestroy();
					return;
				}
			}
		}
		if (base.gameObject.GetComponent<Renderer>() != null)
		{
			vp_DecalManager.Add(base.gameObject);
			return;
		}
		vp_Timer.In(1f, new vp_Timer.Callback(this.TryDestroy), null);
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0007B490 File Offset: 0x00079690
	private void TryDestroy()
	{
		if (this == null)
		{
			return;
		}
		if (!this.m_Audio.isPlaying)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		vp_Timer.In(1f, new vp_Timer.Callback(this.TryDestroy), null);
	}

	// Token: 0x04000DA1 RID: 3489
	public bool IgnoreLocalPlayer = true;

	// Token: 0x04000DA2 RID: 3490
	public float Range = 100f;

	// Token: 0x04000DA3 RID: 3491
	public float Force = 100f;

	// Token: 0x04000DA4 RID: 3492
	public float Damage = 1f;

	// Token: 0x04000DA5 RID: 3493
	public string DamageMethodName = "Damage";

	// Token: 0x04000DA6 RID: 3494
	public float m_SparkFactor = 0.5f;

	// Token: 0x04000DA7 RID: 3495
	public GameObject m_ImpactPrefab;

	// Token: 0x04000DA8 RID: 3496
	public GameObject m_DustPrefab;

	// Token: 0x04000DA9 RID: 3497
	public GameObject m_SparkPrefab;

	// Token: 0x04000DAA RID: 3498
	public GameObject m_DebrisPrefab;

	// Token: 0x04000DAB RID: 3499
	protected AudioSource m_Audio;

	// Token: 0x04000DAC RID: 3500
	public List<AudioClip> m_ImpactSounds = new List<AudioClip>();

	// Token: 0x04000DAD RID: 3501
	public Vector2 SoundImpactPitch = new Vector2(1f, 1.5f);

	// Token: 0x04000DAE RID: 3502
	public int[] NoDecalOnTheseLayers;
}
