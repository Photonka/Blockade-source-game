using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class Capture : MonoBehaviour
{
	// Token: 0x0600018B RID: 395 RVA: 0x00023FDC File Offset: 0x000221DC
	public static void Create()
	{
		int width = Screen.width;
		int height = Screen.height;
		Texture2D texture2D = new Texture2D(width, height, 3, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		texture2D.Apply();
		Object.Destroy(texture2D);
	}
}
