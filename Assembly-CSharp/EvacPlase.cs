using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class EvacPlase : MonoBehaviour
{
	// Token: 0x060002B7 RID: 695 RVA: 0x000392A8 File Offset: 0x000374A8
	private void Awake()
	{
		this.timer = Time.time;
		this.obj = base.gameObject;
		this.goPlayer = GameObject.Find("Player");
		this.goMainCamera = this.goPlayer.GetComponentInChildren<Camera>();
		this.evac_place = (Texture2D)Resources.Load("GUI/evacuate");
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00039302 File Offset: 0x00037502
	private void OnGUI()
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

	// Token: 0x060002B9 RID: 697 RVA: 0x0003933C File Offset: 0x0003753C
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

	// Token: 0x060002BA RID: 698 RVA: 0x000393A4 File Offset: 0x000375A4
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
		p.y += 6f;
		Vector3 vector = this.goMainCamera.GetComponent<Camera>().WorldToScreenPoint(p);
		vector.y = (float)Screen.height - vector.y;
		if (Vector3.Angle(p - this.goPlayer.transform.position, this.goPlayer.transform.forward) < 90f)
		{
			float num = ((float)dist + -206.72f) / -3.23f;
			if (num < 24f)
			{
				num = 24f;
			}
			GUI.DrawTexture(new Rect(vector.x - num / 2f, vector.y, num, num), this.evac_place);
		}
	}

	// Token: 0x04000528 RID: 1320
	private GameObject obj;

	// Token: 0x04000529 RID: 1321
	private float timer;

	// Token: 0x0400052A RID: 1322
	private GameObject goPlayer;

	// Token: 0x0400052B RID: 1323
	private Camera goMainCamera;

	// Token: 0x0400052C RID: 1324
	private float dist;

	// Token: 0x0400052D RID: 1325
	public Texture2D evac_place;

	// Token: 0x0400052E RID: 1326
	public bool accepted;
}
