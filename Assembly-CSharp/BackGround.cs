using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class BackGround : MonoBehaviour
{
	// Token: 0x06000045 RID: 69 RVA: 0x00003218 File Offset: 0x00001418
	private void OnGUI()
	{
		if (BackGround.background[0] == null)
		{
			return;
		}
		Vector2 vector;
		vector..ctor(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		Vector2 vector2;
		vector2..ctor((vector.x - (float)Screen.width / 2f) / 200f, (vector.y - (float)Screen.height / 2f) / 200f);
		Rect rect;
		rect..ctor((float)Screen.width / 2f - (float)Screen.height * 1.1f, (float)Screen.height / 2f - (float)Screen.height * 1.1f / 2f, (float)Screen.height * 1.1f * 2f, (float)Screen.height * 1.1f);
		Rect rect2 = new Rect(rect.x - vector2.x, rect.y - vector2.y, (float)Screen.height * 1.2f * 2f, (float)Screen.height * 1.2f);
		rect.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
		Rect rect3;
		rect3..ctor(this.x1, 0f, 1f, 1f);
		Rect rect4;
		rect4..ctor(this.x2, 0f, 1f, 1f);
		GUI.DrawTexture(rect, BackGround.background[0]);
		GUI.DrawTextureWithTexCoords(rect, BackGround.background[1], rect3);
		GUI.DrawTexture(rect2, BackGround.background[2]);
		GUI.DrawTextureWithTexCoords(rect, BackGround.background[3], rect4);
		this.x1 += 0.02f * Time.deltaTime;
		this.x2 -= 0.015f * Time.deltaTime;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000033EC File Offset: 0x000015EC
	public static void Init()
	{
		BackGround.background[0] = ContentLoader.LoadTexture("layer_wall");
		BackGround.background[1] = ContentLoader.LoadTexture("layer_back");
		BackGround.background[2] = ContentLoader.LoadTexture("layer_player");
		BackGround.background[3] = ContentLoader.LoadTexture("layer_front");
	}

	// Token: 0x04000032 RID: 50
	public static Texture2D[] background = new Texture2D[4];

	// Token: 0x04000033 RID: 51
	private Rect r;

	// Token: 0x04000034 RID: 52
	private float x1;

	// Token: 0x04000035 RID: 53
	private float x2;

	// Token: 0x04000036 RID: 54
	private float y1;

	// Token: 0x04000037 RID: 55
	private float y2;
}
