using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class TransportExit : MonoBehaviour
{
	// Token: 0x06000294 RID: 660 RVA: 0x000388A8 File Offset: 0x00036AA8
	private void Start()
	{
		this.cscr = (Crosshair)Object.FindObjectOfType(typeof(Crosshair));
		this.oc = (OrbitCam)Object.FindObjectOfType(typeof(OrbitCam));
		this.cscr.SetActive(false);
	}

	// Token: 0x06000295 RID: 661 RVA: 0x000388F8 File Offset: 0x00036AF8
	private void Update()
	{
		if (this.activeControl && Input.GetKeyDown(102))
		{
			if (this.cl == null)
			{
				this.cl = (Client)Object.FindObjectOfType(typeof(Client));
			}
			this.cscr.SetActive(true);
			this.oc.zoom = false;
			if (this.vehicleType == CONST.VEHICLES.TANKS)
			{
				this.cl.send_exit_the_ent(base.gameObject.GetComponentInChildren<Tank>().uid);
				return;
			}
			if (this.vehicleType == CONST.VEHICLES.JEEP)
			{
				Car car = base.gameObject.GetComponentInChildren<Car>();
				if (car == null)
				{
					car = base.gameObject.GetComponentInParent<Car>();
				}
				if (car == null)
				{
					return;
				}
				this.cl.send_exit_the_ent(car.uid);
			}
		}
	}

	// Token: 0x040004F2 RID: 1266
	private Client cl;

	// Token: 0x040004F3 RID: 1267
	public bool activeControl = true;

	// Token: 0x040004F4 RID: 1268
	private Crosshair cscr;

	// Token: 0x040004F5 RID: 1269
	private OrbitCam oc;

	// Token: 0x040004F6 RID: 1270
	public int vehicleType;
}
