using System;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class Overlay : MonoBehaviour
{
	// Token: 0x060003B4 RID: 948 RVA: 0x00048C9A File Offset: 0x00046E9A
	private void Awake()
	{
		this.tex_heart = (Resources.Load("GUI/heart") as Texture2D);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00048CB1 File Offset: 0x00046EB1
	public void SetActive(bool val)
	{
		this.active = val;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00048CBC File Offset: 0x00046EBC
	private void OnGUI()
	{
		if (!this.active)
		{
			return;
		}
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.tex_heart);
		GUI.color = this.color1;
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), GUIManager.tex_red);
		GUI.color = this.color2;
	}

	// Token: 0x040007D5 RID: 2005
	private bool active;

	// Token: 0x040007D6 RID: 2006
	private Texture2D tex_heart;

	// Token: 0x040007D7 RID: 2007
	private Color color1 = new Color(1f, 1f, 1f, 0.25f);

	// Token: 0x040007D8 RID: 2008
	private Color color2 = new Color(1f, 1f, 1f, 1f);
}
