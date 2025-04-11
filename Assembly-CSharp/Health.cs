using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class Health : MonoBehaviour
{
	// Token: 0x06000386 RID: 902 RVA: 0x00042E14 File Offset: 0x00041014
	private void Awake()
	{
		if (!this.tex)
		{
			this.tex = (Resources.Load("GUI/health") as Texture);
		}
		this.texHelmet = (Resources.Load("GUI/helmet") as Texture);
		this.texArmor = (Resources.Load("GUI/armor") as Texture);
		this.gasMask = (Resources.Load("GUI/gasmask2") as Texture);
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[0];
		this.gui_style.fontSize = 52;
		GameObject.Find("Map");
		if (!this.tex_indicator)
		{
			this.tex_indicator = (Resources.Load("GUI/damage") as Texture);
		}
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00042EDC File Offset: 0x000410DC
	private void OnGUI()
	{
		if (!this.init)
		{
			this.init = true;
		}
		GUI.depth = -1;
		if (this.tc == null)
		{
			this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
		}
		if (this.tc != null)
		{
			this.activeTC = this.tc.enabled;
		}
		if (this.cc == null)
		{
			this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
		}
		if (this.cc != null)
		{
			this.activeCC = this.cc.enabled;
		}
		if (!this.activeTC && !this.activeCC)
		{
			this.gui_style.normal.textColor = GUIManager.c[9];
			GUI.Label(new Rect(GUIManager.XRES(512f) + 2f, GUIManager.YRES(768f) - 39f + 2f, 0f, 0f), this.health.ToString(), this.gui_style);
			if (this.health < 30)
			{
				this.gui_style.normal.textColor = GUIManager.c[1];
			}
			else if (this.health <= 60)
			{
				this.gui_style.normal.textColor = GUIManager.c[3];
			}
			else
			{
				this.gui_style.normal.textColor = GUIManager.c[8];
			}
			GUI.Label(new Rect(GUIManager.XRES(512f), GUIManager.YRES(768f) - 39f, 0f, 0f), this.health.ToString(), this.gui_style);
			GUI.DrawTexture(new Rect(GUIManager.XRES(512f) - 40f, GUIManager.YRES(768f) - 40f, 32f, 32f), this.tex);
			if (this.helmet)
			{
				GUI.DrawTexture(new Rect(GUIManager.XRES(512f) - 112f, GUIManager.YRES(768f) - 40f, 32f, 32f), this.texHelmet);
			}
			if (this.armor)
			{
				GUI.DrawTexture(new Rect(GUIManager.XRES(512f) - 76f, GUIManager.YRES(768f) - 40f, 32f, 32f), this.texArmor);
			}
			if (this.mask)
			{
				GUI.DrawTexture(new Rect(GUIManager.XRES(512f) - 148f, GUIManager.YRES(768f) - 40f, 32f, 32f), this.gasMask);
			}
		}
		if (this.IndicatorTime < Time.time)
		{
			return;
		}
		float num = Health.AngleSigned(Camera.main.transform.forward, this.DamagePos - this.goPlayer.transform.position, this.goPlayer.transform.up);
		Matrix4x4 matrix = GUI.matrix;
		GUIUtility.RotateAroundPivot(num, new Vector2((float)Screen.width, (float)Screen.height) / 2f);
		GUI.DrawTexture(new Rect(0f, 0f, GUIManager.XRES(1024f), GUIManager.YRES(768f)), this.tex_indicator);
		GUI.matrix = matrix;
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00043258 File Offset: 0x00041458
	public void SetHealth(int _health)
	{
		this.health = _health;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00043261 File Offset: 0x00041461
	public int GetHealth()
	{
		return this.health;
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00043269 File Offset: 0x00041469
	public void SetHelmet(bool val)
	{
		this.helmet = val;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x00043272 File Offset: 0x00041472
	public void SetArmor(bool val)
	{
		this.armor = val;
	}

	// Token: 0x0600038C RID: 908 RVA: 0x0004327B File Offset: 0x0004147B
	public void SetMask(bool val)
	{
		this.mask = val;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00043284 File Offset: 0x00041484
	public void SetDamageIndicator(int attackerid)
	{
		if (attackerid >= 32)
		{
			return;
		}
		this.IndicatorTime = Time.time + 1f;
		this.aid = attackerid;
		this.DamagePos = BotsController.Instance.Bots[this.aid].position;
		if (!this.goPlayer)
		{
			this.goPlayer = GameObject.Find("Player");
		}
	}

	// Token: 0x0600038E RID: 910 RVA: 0x000432E8 File Offset: 0x000414E8
	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
	}

	// Token: 0x0400073B RID: 1851
	private bool init;

	// Token: 0x0400073C RID: 1852
	private GUIStyle gui_style;

	// Token: 0x0400073D RID: 1853
	private Texture tex;

	// Token: 0x0400073E RID: 1854
	private Texture texHelmet;

	// Token: 0x0400073F RID: 1855
	private Texture texArmor;

	// Token: 0x04000740 RID: 1856
	private Texture gasMask;

	// Token: 0x04000741 RID: 1857
	private int health = 100;

	// Token: 0x04000742 RID: 1858
	private bool helmet;

	// Token: 0x04000743 RID: 1859
	private bool armor;

	// Token: 0x04000744 RID: 1860
	private bool mask;

	// Token: 0x04000745 RID: 1861
	private float IndicatorTime;

	// Token: 0x04000746 RID: 1862
	private Texture tex_indicator;

	// Token: 0x04000747 RID: 1863
	private int aid;

	// Token: 0x04000748 RID: 1864
	private Vector3 DamagePos = Vector3.zero;

	// Token: 0x04000749 RID: 1865
	private GameObject goPlayer;

	// Token: 0x0400074A RID: 1866
	private TankController tc;

	// Token: 0x0400074B RID: 1867
	private bool activeTC;

	// Token: 0x0400074C RID: 1868
	private CarController cc;

	// Token: 0x0400074D RID: 1869
	private bool activeCC;
}
