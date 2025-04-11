using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class SnowFlake : MonoBehaviour
{
	// Token: 0x06000572 RID: 1394 RVA: 0x00068DEC File Offset: 0x00066FEC
	private void Awake()
	{
		this.active = true;
		this.sc_width = (float)Screen.width;
		this.sc_height = (float)Screen.height;
		for (int i = 0; i < 6; i++)
		{
			this.arr_tex[i] = (Resources.Load("GUI/ValentinesDay/heart" + Random.Range(1, 3)) as Texture2D);
		}
		this.num = (float)Random.Range(20, 60);
		int num = 0;
		while ((float)num < this.num)
		{
			int num2 = Random.Range(8, 32);
			this.Snow13.Add(new flakesnow(this.arr_tex[Random.Range(0, 6)], new Rect(Random.Range(0f, this.sc_width), Random.Range(0f, this.sc_height), (float)num2, (float)num2)));
			num++;
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00068EBE File Offset: 0x000670BE
	private void OnResize()
	{
		this.sc_width = (float)Screen.width;
		this.sc_height = (float)Screen.height;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00068ED8 File Offset: 0x000670D8
	private void FixedUpdate()
	{
		for (int i = 0; i < this.Snow13.Count; i++)
		{
			Vector2 vector;
			vector..ctor(this.Snow13[i].rec_flake.x, this.Snow13[i].rec_flake.y);
			Vector2 vector2;
			vector2..ctor(this.Snow13[i].rec_flake.x + Random.Range(-5f, 5f), this.Snow13[i].rec_flake.y + Random.Range(10f, 10f));
			vector = Vector2.Lerp(vector, vector2, 5f * Time.deltaTime);
			this.Snow13[i].rec_flake.x = vector.x;
			this.Snow13[i].rec_flake.y = vector.y;
			if (this.Snow13[i].rec_flake.y > this.sc_height - 50f || this.Snow13[i].rec_flake.x > this.sc_width - 50f || this.Snow13[i].rec_flake.x < 50f)
			{
				this.Snow13[i].tex_flake = this.arr_tex[Random.Range(0, 6)];
				this.Snow13[i].rec_flake = new Rect(Random.Range(0f, this.sc_width), 2f, 32f, 32f);
			}
		}
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0006908C File Offset: 0x0006728C
	public void OnGUI()
	{
		if (!this.active)
		{
			return;
		}
		for (int i = 0; i < this.Snow13.Count; i++)
		{
			GUI.DrawTexture(this.Snow13[i].rec_flake, this.Snow13[i].tex_flake);
		}
	}

	// Token: 0x040009A3 RID: 2467
	public List<flakesnow> Snow13 = new List<flakesnow>();

	// Token: 0x040009A4 RID: 2468
	public bool active;

	// Token: 0x040009A5 RID: 2469
	private float sc_width;

	// Token: 0x040009A6 RID: 2470
	private float sc_height;

	// Token: 0x040009A7 RID: 2471
	private float num;

	// Token: 0x040009A8 RID: 2472
	private Texture2D[] arr_tex = new Texture2D[6];
}
