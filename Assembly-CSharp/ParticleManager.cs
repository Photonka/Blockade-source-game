using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class ParticleManager : MonoBehaviour
{
	// Token: 0x060001E5 RID: 485 RVA: 0x00025DF9 File Offset: 0x00023FF9
	private void Awake()
	{
		ParticleManager.THIS = this;
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x00025E01 File Offset: 0x00024001
	public void CreateParticle(float x, float y, float z, float r, float g, float b, float a)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.pgoBrick);
		gameObject.transform.position = new Vector3(x, y, z);
		gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00025E40 File Offset: 0x00024040
	public void CreateHit(Transform _player, int hitbox, float x, float y, float z)
	{
		GameObject gameObject;
		if (hitbox == 1)
		{
			gameObject = Object.Instantiate<GameObject>(this.pgoHitHead);
		}
		else
		{
			gameObject = Object.Instantiate<GameObject>(this.pgoHitBody);
		}
		gameObject.transform.position = new Vector3(x, y, z);
		gameObject.transform.LookAt(_player.position + new Vector3(0f, 1.75f, 0f));
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00025EAA File Offset: 0x000240AA
	public void CreateFlame(Vector3 pos, Transform parent)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.pgoFlame);
		gameObject.transform.position = pos;
		gameObject.transform.parent = parent;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00002B75 File Offset: 0x00000D75
	public void CreateGhostDeath(Vector3 pos)
	{
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Update()
	{
	}

	// Token: 0x040001F9 RID: 505
	public static ParticleManager THIS;

	// Token: 0x040001FA RID: 506
	public GameObject pgoBrick;

	// Token: 0x040001FB RID: 507
	public GameObject pgoHitHead;

	// Token: 0x040001FC RID: 508
	public GameObject pgoHitBody;

	// Token: 0x040001FD RID: 509
	public GameObject pgoFlame;

	// Token: 0x040001FE RID: 510
	public GameObject pgoGhostDeath;

	// Token: 0x040001FF RID: 511
	public GameObject FireWork1;

	// Token: 0x04000200 RID: 512
	public GameObject FireWork2;

	// Token: 0x04000201 RID: 513
	private bool Fire;
}
