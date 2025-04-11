using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
[RequireComponent(typeof(AudioSource))]
public class vp_SimpleHUD : MonoBehaviour
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000872 RID: 2162 RVA: 0x0007DE78 File Offset: 0x0007C078
	public static GUIStyle MessageStyle
	{
		get
		{
			if (vp_SimpleHUD.m_MessageStyle == null)
			{
				vp_SimpleHUD.m_MessageStyle = new GUIStyle("Label");
				vp_SimpleHUD.m_MessageStyle.alignment = 4;
			}
			return vp_SimpleHUD.m_MessageStyle;
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0007DEA5 File Offset: 0x0007C0A5
	private void Awake()
	{
		this.m_Player = base.transform.GetComponent<vp_FPPlayerEventHandler>();
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0007DEB8 File Offset: 0x0007C0B8
	protected virtual void OnEnable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Register(this);
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0007DED4 File Offset: 0x0007C0D4
	protected virtual void OnDisable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Unregister(this);
		}
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0007DEF0 File Offset: 0x0007C0F0
	protected virtual void OnGUI()
	{
		if (!this.ShowHUD)
		{
			return;
		}
		GUI.Box(new Rect(10f, (float)(Screen.height - 30), 100f, 22f), "Health: " + (int)(this.m_Player.Health.Get() * 100f) + "%");
		GUI.Box(new Rect((float)(Screen.width - 220), (float)(Screen.height - 30), 100f, 22f), "Clips: " + this.m_Player.GetItemCount.Send("AmmoClip"));
		GUI.Box(new Rect((float)(Screen.width - 110), (float)(Screen.height - 30), 100f, 22f), "Ammo: " + this.m_Player.CurrentWeaponAmmoCount.Get());
		if (!string.IsNullOrEmpty(this.m_PickupMessage) && this.m_MessageColor.a > 0.01f)
		{
			this.m_MessageColor = Color.Lerp(this.m_MessageColor, this.m_InvisibleColor, Time.deltaTime * 0.4f);
			GUI.color = this.m_MessageColor;
			GUI.Box(new Rect(200f, 150f, (float)(Screen.width - 400), (float)(Screen.height - 400)), this.m_PickupMessage, vp_SimpleHUD.MessageStyle);
			GUI.color = Color.white;
		}
		if (this.DamageFlashTexture != null && this.m_DamageFlashColor.a > 0.01f)
		{
			this.m_DamageFlashColor = Color.Lerp(this.m_DamageFlashColor, this.m_DamageFlashInvisibleColor, Time.deltaTime * 0.4f);
			GUI.color = this.m_DamageFlashColor;
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.DamageFlashTexture);
			GUI.color = Color.white;
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0007E101 File Offset: 0x0007C301
	protected virtual void OnMessage_HUDText(string message)
	{
		this.m_MessageColor = Color.white;
		this.m_PickupMessage = message;
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0007E115 File Offset: 0x0007C315
	protected virtual void OnMessage_HUDDamageFlash(float intensity)
	{
		if (this.DamageFlashTexture == null)
		{
			return;
		}
		if (intensity == 0f)
		{
			this.m_DamageFlashColor.a = 0f;
			return;
		}
		this.m_DamageFlashColor.a = this.m_DamageFlashColor.a + intensity;
	}

	// Token: 0x04000E34 RID: 3636
	public Texture DamageFlashTexture;

	// Token: 0x04000E35 RID: 3637
	public bool ShowHUD = true;

	// Token: 0x04000E36 RID: 3638
	private Color m_MessageColor = new Color(2f, 2f, 0f, 2f);

	// Token: 0x04000E37 RID: 3639
	private Color m_InvisibleColor = new Color(1f, 1f, 0f, 0f);

	// Token: 0x04000E38 RID: 3640
	private Color m_DamageFlashColor = new Color(0.8f, 0f, 0f, 0f);

	// Token: 0x04000E39 RID: 3641
	private Color m_DamageFlashInvisibleColor = new Color(1f, 0f, 0f, 0f);

	// Token: 0x04000E3A RID: 3642
	private string m_PickupMessage = "";

	// Token: 0x04000E3B RID: 3643
	protected static GUIStyle m_MessageStyle;

	// Token: 0x04000E3C RID: 3644
	private vp_FPPlayerEventHandler m_Player;
}
