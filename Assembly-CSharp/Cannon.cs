using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class Cannon : MonoBehaviour
{
	// Token: 0x06000242 RID: 578 RVA: 0x0002CEE1 File Offset: 0x0002B0E1
	private void Start()
	{
		this.preZoom = ContentLoader.LoadTexture("preZoom");
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0002CEF4 File Offset: 0x0002B0F4
	private void Update()
	{
		if (this.tc == null)
		{
			this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
		}
		RaycastHit raycastHit;
		if (base.transform.parent.parent.parent.parent != null && base.transform.parent.parent.parent.parent.transform.name == "Player" && this.tc.activeControl && this.tc.enabled && Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.forward), ref raycastHit))
		{
			Debug.DrawLine(base.transform.position, raycastHit.point, Color.red);
			this.pointCursor = Camera.main.WorldToScreenPoint(raycastHit.point);
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0002CFF0 File Offset: 0x0002B1F0
	private void OnGUI()
	{
		if (this.tc == null)
		{
			this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
		}
		if (base.transform.parent.parent.parent.parent != null && base.transform.parent.parent.parent.parent.transform.name == "Player" && this.tc.activeControl && this.tc.enabled)
		{
			Rect rect = default(Rect);
			rect.x = this.pointCursor.x - 14f;
			rect.y = (float)Screen.height - this.pointCursor.y - 12f;
			rect.width = 28f;
			rect.height = 24f;
			GUI.DrawTexture(rect, this.preZoom);
		}
	}

	// Token: 0x040002D2 RID: 722
	public float rotSpeed;

	// Token: 0x040002D3 RID: 723
	private Vector3 pointCursor;

	// Token: 0x040002D4 RID: 724
	private Texture preZoom;

	// Token: 0x040002D5 RID: 725
	private TankController tc;
}
