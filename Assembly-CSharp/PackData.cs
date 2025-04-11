using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000B2 RID: 178
public class PackData : MonoBehaviour
{
	// Token: 0x060005C8 RID: 1480 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Awake()
	{
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0006AB48 File Offset: 0x00068D48
	public void PackPlayerPos(int id, float a, float b, float c)
	{
		this.hashpos[id] = "Player" + (a + b + c).ToString("0");
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0006AB7C File Offset: 0x00068D7C
	public void CheckPlayerPos(int id)
	{
		if ("Player" + (BotsController.Instance.Bots[id].position.x + BotsController.Instance.Bots[id].position.y + BotsController.Instance.Bots[id].position.z).ToString("0") != this.hashpos[id])
		{
			SceneManager.LoadScene(0);
		}
	}

	// Token: 0x04000B6D RID: 2925
	private string[] hashpos = new string[32];
}
