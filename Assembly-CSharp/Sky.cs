using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class Sky : MonoBehaviour
{
	// Token: 0x06000149 RID: 329 RVA: 0x0001C6D0 File Offset: 0x0001A8D0
	public void SetSky(int skyid, bool prazd = false)
	{
		if (prazd)
		{
			RenderSettings.skybox = this.skybox[2];
		}
		else
		{
			RenderSettings.skybox = this.skybox[skyid];
		}
		if (skyid == 1)
		{
			this.SetFog(0f, 12f, new Color(0.05f, 0.05f, 0.05f, 1f));
		}
		else
		{
			this.SetFog(40f, 80f, new Color(0.58f, 0.68f, 0.72f, 1f));
		}
		if (prazd)
		{
			this.SetFog(40f, 80f, new Color(0.05f, 0.05f, 0.05f, 1f));
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0001C780 File Offset: 0x0001A980
	public void SetFog(float start, float end, Color color)
	{
		RenderSettings.fogDensity = 0f;
		RenderSettings.fogMode = 1;
		RenderSettings.fogStartDistance = start;
		RenderSettings.fogEndDistance = end;
		RenderSettings.fogColor = color;
	}

	// Token: 0x04000163 RID: 355
	public Material[] skybox = new Material[3];
}
