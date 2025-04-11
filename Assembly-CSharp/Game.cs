using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class Game : MonoBehaviour
{
	// Token: 0x0600018D RID: 397 RVA: 0x00024024 File Offset: 0x00022224
	public static bool isStandAlone()
	{
		return !Application.isEditor;
	}

	// Token: 0x0400016C RID: 364
	public static string username = "...";

	// Token: 0x0400016D RID: 365
	public static bool SteamIsActiv;
}
