using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class Gunner : MonoBehaviour
{
	// Token: 0x06000268 RID: 616 RVA: 0x00031A3F File Offset: 0x0002FC3F
	private void Start()
	{
		this.cross = (Resources.Load("GUI/humvee_dynamic") as Texture);
		this.crossStatic = (Resources.Load("GUI/humvee_static") as Texture);
		this.myTransform = base.transform;
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00031A78 File Offset: 0x0002FC78
	private void Update()
	{
		if (this.cc == null)
		{
			this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
		}
		RaycastHit raycastHit;
		if (this.cc != null && this.cc.activeControl && this.cc.enabled && this.cc.myPosition == CONST.VEHICLES.POSITION_JEEP_GUNNER && Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.forward), ref raycastHit))
		{
			this.pointCursor = Camera.main.WorldToScreenPoint(raycastHit.point);
		}
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00031B24 File Offset: 0x0002FD24
	private void OnGUI()
	{
		if (this.cc == null)
		{
			this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
		}
		if (this.cc != null && this.cc.activeControl && this.cc.enabled && this.cc.myPosition == CONST.VEHICLES.POSITION_JEEP_GUNNER)
		{
			Rect rect = default(Rect);
			rect.x = this.pointCursor.x - 16f;
			rect.y = (float)(Screen.height / 2 - 16);
			rect.width = 32f;
			rect.height = 32f;
			GUI.DrawTexture(rect, this.cross);
			rect.x = (float)(Screen.width / 2 - 16);
			GUI.DrawTexture(rect, this.crossStatic);
		}
	}

	// Token: 0x040003AA RID: 938
	public float rotSpeed;

	// Token: 0x040003AB RID: 939
	private Vector3 pointCursor;

	// Token: 0x040003AC RID: 940
	private Texture cross;

	// Token: 0x040003AD RID: 941
	private Texture crossStatic;

	// Token: 0x040003AE RID: 942
	private CarController cc;

	// Token: 0x040003AF RID: 943
	private Transform myTransform;
}
