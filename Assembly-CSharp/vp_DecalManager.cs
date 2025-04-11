using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DC RID: 220
public sealed class vp_DecalManager
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060007FF RID: 2047 RVA: 0x0007A9C7 File Offset: 0x00078BC7
	// (set) Token: 0x06000800 RID: 2048 RVA: 0x0007A9CE File Offset: 0x00078BCE
	public static float MaxDecals
	{
		get
		{
			return vp_DecalManager.m_MaxDecals;
		}
		set
		{
			vp_DecalManager.m_MaxDecals = value;
			vp_DecalManager.Refresh();
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000801 RID: 2049 RVA: 0x0007A9DB File Offset: 0x00078BDB
	// (set) Token: 0x06000802 RID: 2050 RVA: 0x0007A9E2 File Offset: 0x00078BE2
	public static float FadedDecals
	{
		get
		{
			return vp_DecalManager.m_FadedDecals;
		}
		set
		{
			if (value > vp_DecalManager.m_MaxDecals)
			{
				Debug.LogError("FadedDecals can't be larger than MaxDecals");
				return;
			}
			vp_DecalManager.m_FadedDecals = value;
			vp_DecalManager.Refresh();
		}
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0007AA04 File Offset: 0x00078C04
	static vp_DecalManager()
	{
		vp_DecalManager.Refresh();
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00023EF4 File Offset: 0x000220F4
	private vp_DecalManager()
	{
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x0007AA52 File Offset: 0x00078C52
	public static void Add(GameObject decal)
	{
		vp_DecalManager.m_Decals.Add(decal);
		vp_DecalManager.FadeAndRemove();
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x0007AA64 File Offset: 0x00078C64
	private static void FadeAndRemove()
	{
		if ((float)vp_DecalManager.m_Decals.Count > vp_DecalManager.m_NonFadedDecals)
		{
			int num = 0;
			while ((float)num < (float)vp_DecalManager.m_Decals.Count - vp_DecalManager.m_NonFadedDecals)
			{
				if (vp_DecalManager.m_Decals[num] != null)
				{
					Color color = vp_DecalManager.m_Decals[num].GetComponent<Renderer>().material.color;
					color.a -= vp_DecalManager.m_FadeAmount;
					vp_DecalManager.m_Decals[num].GetComponent<Renderer>().material.color = color;
				}
				num++;
			}
		}
		if (vp_DecalManager.m_Decals[0] != null)
		{
			if (vp_DecalManager.m_Decals[0].GetComponent<Renderer>().material.color.a <= 0f)
			{
				Object.Destroy(vp_DecalManager.m_Decals[0]);
				vp_DecalManager.m_Decals.Remove(vp_DecalManager.m_Decals[0]);
				return;
			}
		}
		else
		{
			vp_DecalManager.m_Decals.RemoveAt(0);
		}
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x0007AB64 File Offset: 0x00078D64
	private static void Refresh()
	{
		if (vp_DecalManager.m_MaxDecals < vp_DecalManager.m_FadedDecals)
		{
			vp_DecalManager.m_MaxDecals = vp_DecalManager.m_FadedDecals;
		}
		vp_DecalManager.m_FadeAmount = vp_DecalManager.m_MaxDecals / vp_DecalManager.m_FadedDecals / vp_DecalManager.m_MaxDecals;
		vp_DecalManager.m_NonFadedDecals = vp_DecalManager.m_MaxDecals - vp_DecalManager.m_FadedDecals;
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x0007ABA4 File Offset: 0x00078DA4
	private static void DebugOutput()
	{
		int num = 0;
		int num2 = 0;
		using (List<GameObject>.Enumerator enumerator = vp_DecalManager.m_Decals.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetComponent<Renderer>().material.color.a == 1f)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
		}
		Debug.Log(string.Concat(new object[]
		{
			"Decal count: ",
			vp_DecalManager.m_Decals.Count,
			", Full: ",
			num,
			", Faded: ",
			num2
		}));
	}

	// Token: 0x04000D8B RID: 3467
	public static readonly vp_DecalManager instance = new vp_DecalManager();

	// Token: 0x04000D8C RID: 3468
	private static List<GameObject> m_Decals = new List<GameObject>();

	// Token: 0x04000D8D RID: 3469
	private static float m_MaxDecals = 100f;

	// Token: 0x04000D8E RID: 3470
	private static float m_FadedDecals = 20f;

	// Token: 0x04000D8F RID: 3471
	private static float m_NonFadedDecals = 0f;

	// Token: 0x04000D90 RID: 3472
	private static float m_FadeAmount = 0f;
}
