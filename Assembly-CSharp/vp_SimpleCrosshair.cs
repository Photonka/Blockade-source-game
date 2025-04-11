using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class vp_SimpleCrosshair : MonoBehaviour
{
	// Token: 0x06000870 RID: 2160 RVA: 0x0007DDCC File Offset: 0x0007BFCC
	private void OnGUI()
	{
		if (this.m_ImageCrosshair != null)
		{
			GUI.color = new Color(1f, 1f, 1f, 0.8f);
			GUI.DrawTexture(new Rect((float)Screen.width * 0.5f - (float)this.m_ImageCrosshair.width * 0.5f, (float)Screen.height * 0.5f - (float)this.m_ImageCrosshair.height * 0.5f, (float)this.m_ImageCrosshair.width, (float)this.m_ImageCrosshair.height), this.m_ImageCrosshair);
			GUI.color = Color.white;
		}
	}

	// Token: 0x04000E33 RID: 3635
	public Texture m_ImageCrosshair;
}
