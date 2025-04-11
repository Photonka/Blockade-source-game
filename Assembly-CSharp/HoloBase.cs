using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class HoloBase : MonoBehaviour
{
	// Token: 0x06000192 RID: 402 RVA: 0x0002405C File Offset: 0x0002225C
	public static GameObject Create(Vector3 pos, int _mode)
	{
		if (_mode == CONST.CFG.BATTLE_MODE)
		{
			HoloBase.goCenter = (GameObject)Object.Instantiate(Resources.Load("Prefab/Center"));
		}
		else if (_mode == CONST.CFG.PRORIV_MODE)
		{
			HoloBase.goCenter = (GameObject)Object.Instantiate(Resources.Load("Prefab/evakPoint"));
		}
		else if (_mode == 777)
		{
			HoloBase.goCenter = (GameObject)Object.Instantiate(Resources.Load("Prefab/flagZone"));
		}
		HoloBase.goCenter.transform.position = pos;
		return HoloBase.goCenter;
	}

	// Token: 0x0400016F RID: 367
	private static GameObject goCenter;
}
