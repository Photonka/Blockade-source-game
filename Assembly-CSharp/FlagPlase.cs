using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class FlagPlase : MonoBehaviour
{
	// Token: 0x060002C2 RID: 706 RVA: 0x000394E8 File Offset: 0x000376E8
	private void Awake()
	{
		this.timer = Time.time;
		this.obj = base.gameObject;
		this.goPlayer = GameObject.Find("Player");
		this.goMainCamera = this.goPlayer.GetComponentInChildren<Camera>();
		this.flag_place[0] = (Texture2D)Resources.Load("GUI/bflag");
		this.flag_place[1] = (Texture2D)Resources.Load("GUI/rflag");
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0003955B File Offset: 0x0003775B
	public void MyGUI()
	{
		if (this.goMainCamera == null)
		{
			return;
		}
		if (!this.accepted)
		{
			return;
		}
		this.DrawFlagPlace(this.obj.transform.position, (int)this.dist);
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00039594 File Offset: 0x00037794
	private void Update()
	{
		if (this.goMainCamera == null)
		{
			return;
		}
		if (Time.time < this.timer + 1f)
		{
			return;
		}
		this.dist = Vector3.Distance(this.goPlayer.transform.position, this.obj.transform.position);
		this.timer = Time.time;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x000395FC File Offset: 0x000377FC
	private void DrawFlagPlace(Vector3 p, int dist)
	{
		if (dist <= 1)
		{
			dist = 1;
		}
		if (dist >= 200)
		{
			dist = 200;
		}
		p.y += 12f;
		Vector3 vector = this.goMainCamera.GetComponent<Camera>().WorldToScreenPoint(p);
		vector.y = (float)Screen.height - vector.y;
		if (Vector3.Angle(p - this.goPlayer.transform.position, this.goPlayer.transform.forward) < 90f)
		{
			float num = ((float)dist + -206.72f) / -3.23f;
			if (num < 24f)
			{
				num = 24f;
			}
			if (this.team == 0 || this.team == 1)
			{
				GUI.DrawTexture(new Rect(vector.x - num / 2f, vector.y, num, num), this.flag_place[this.team]);
			}
		}
	}

	// Token: 0x04000537 RID: 1335
	private GameObject obj;

	// Token: 0x04000538 RID: 1336
	private float timer;

	// Token: 0x04000539 RID: 1337
	private GameObject goPlayer;

	// Token: 0x0400053A RID: 1338
	private Camera goMainCamera;

	// Token: 0x0400053B RID: 1339
	private float dist;

	// Token: 0x0400053C RID: 1340
	public Texture2D[] flag_place = new Texture2D[2];

	// Token: 0x0400053D RID: 1341
	public int team = 1;

	// Token: 0x0400053E RID: 1342
	public bool accepted;
}
