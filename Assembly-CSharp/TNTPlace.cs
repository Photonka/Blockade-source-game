using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class TNTPlace : MonoBehaviour
{
	// Token: 0x0600033D RID: 829 RVA: 0x0003BA80 File Offset: 0x00039C80
	private void Awake()
	{
		this.timer = Time.time;
		this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		this.obj = base.gameObject;
		this.goPlayer = GameObject.Find("Player");
		this.goMainCamera = this.goPlayer.GetComponentInChildren<Camera>();
		this.TNT_place[0] = (Texture2D)Resources.Load("GUI/tnt_arrow");
		this.TNT_place[1] = (Texture2D)Resources.Load("GUI/defend_arrow");
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0003BB0D File Offset: 0x00039D0D
	private void OnGUI()
	{
		if (this.goMainCamera == null)
		{
			return;
		}
		this.DrawTNTPlace(this.obj.transform.position, (int)this.dist);
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0003BB3C File Offset: 0x00039D3C
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

	// Token: 0x06000340 RID: 832 RVA: 0x0003BBA4 File Offset: 0x00039DA4
	private void DrawTNTPlace(Vector3 p, int dist)
	{
		if (dist <= 1)
		{
			dist = 1;
		}
		if (dist >= 200)
		{
			dist = 200;
		}
		p.y += 1f;
		Vector3 vector = this.goMainCamera.GetComponent<Camera>().WorldToScreenPoint(p);
		vector.y = (float)Screen.height - vector.y;
		if (Vector3.Angle(p - this.goPlayer.transform.position, this.goPlayer.transform.forward) < 90f)
		{
			float num = ((float)dist + -206.72f) / -3.23f;
			if (num < 24f)
			{
				num = 24f;
			}
			int team = this.cscl.cspc.GetTeam();
			if (team == 0 || team == 1)
			{
				GUI.DrawTexture(new Rect(vector.x - num / 2f, vector.y, num, num), this.TNT_place[team]);
			}
		}
	}

	// Token: 0x040005D8 RID: 1496
	public int id;

	// Token: 0x040005D9 RID: 1497
	public int uid;

	// Token: 0x040005DA RID: 1498
	public int entid;

	// Token: 0x040005DB RID: 1499
	private GameObject obj;

	// Token: 0x040005DC RID: 1500
	private float timer;

	// Token: 0x040005DD RID: 1501
	private GameObject goPlayer;

	// Token: 0x040005DE RID: 1502
	private Camera goMainCamera;

	// Token: 0x040005DF RID: 1503
	private float dist;

	// Token: 0x040005E0 RID: 1504
	public Texture2D[] TNT_place = new Texture2D[2];

	// Token: 0x040005E1 RID: 1505
	private Client cscl;
}
