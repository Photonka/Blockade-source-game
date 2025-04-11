using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000069 RID: 105
public class Pashalka : MonoBehaviour
{
	// Token: 0x0600030E RID: 782 RVA: 0x0003AE42 File Offset: 0x00039042
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0003AE5E File Offset: 0x0003905E
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(10f);
		if (this.timer)
		{
			this.KillSelf();
		}
		yield break;
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0003AE6D File Offset: 0x0003906D
	public void KillSelf()
	{
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0003AE88 File Offset: 0x00039088
	private void Update()
	{
		if (this.healthImage != null)
		{
			this.healthImage.transform.LookAt(Camera.main.transform, Vector3.up);
			this.healthImage.fillAmount = (float)this.health / 500000f;
		}
		if (this.mat != null && this.mat.color.a > 0f)
		{
			this.mat.color = new Color(this.mat.color.r, this.mat.color.g, this.mat.color.b, this.mat.color.a - 0.05f);
		}
	}

	// Token: 0x06000312 RID: 786 RVA: 0x0003AF55 File Offset: 0x00039155
	public void SetHealth(int _health)
	{
		this.health = _health;
	}

	// Token: 0x040005A0 RID: 1440
	public int id;

	// Token: 0x040005A1 RID: 1441
	public int uid;

	// Token: 0x040005A2 RID: 1442
	public int entid;

	// Token: 0x040005A3 RID: 1443
	public bool timer = true;

	// Token: 0x040005A4 RID: 1444
	private Client cscl;

	// Token: 0x040005A5 RID: 1445
	private EntManager entmanager;

	// Token: 0x040005A6 RID: 1446
	public Image healthImage;

	// Token: 0x040005A7 RID: 1447
	public int health;

	// Token: 0x040005A8 RID: 1448
	public Material mat;
}
