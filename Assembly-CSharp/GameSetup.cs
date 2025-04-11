using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class GameSetup : MonoBehaviour
{
	// Token: 0x06000190 RID: 400 RVA: 0x0002403A File Offset: 0x0002223A
	private void OnLevelWasLoaded(int level)
	{
		if (GameSetup.blockSet != null)
		{
			base.GetComponent<Map>().SetBlockSet(GameSetup.blockSet);
		}
	}

	// Token: 0x0400016E RID: 366
	public static BlockSet blockSet;
}
