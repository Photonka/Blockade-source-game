using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class OrbitCam : MonoBehaviour
{
	// Token: 0x0600026C RID: 620 RVA: 0x00031C10 File Offset: 0x0002FE10
	private void Start()
	{
		this.myTransform = base.transform;
		Vector3 eulerAngles = this.myTransform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
		this.tankZoom = ContentLoader.LoadTexture("tank_hud");
		this.carZoom = (Resources.Load("car_hud") as Texture);
		this.zoom = false;
		this.Player = (vp_FPPlayerEventHandler)Object.FindObjectOfType(typeof(vp_FPPlayerEventHandler));
		this.koefX1 = -20f;
		this.koefX2 = 18f;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00031CC4 File Offset: 0x0002FEC4
	private void Update()
	{
		if (this.target == null)
		{
			return;
		}
		if (Input.GetMouseButtonDown(1))
		{
			this.zoom = !this.zoom;
			this.myTransform.parent.GetComponent<Sound>().PlaySound_TankZoom(base.gameObject.GetComponent<AudioSource>());
			if (this.zoom)
			{
				if (this.vehicleType == CONST.VEHICLES.TANKS)
				{
					SkinnedMeshRenderer[] componentsInChildren = this.target.GetComponentInChildren<Tank>().gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].enabled = false;
					}
				}
				else if (this.vehicleType == CONST.VEHICLES.JEEP)
				{
					if (this.cc == null)
					{
						this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
					}
					if (this.cc.myPosition != CONST.VEHICLES.POSITION_JEEP_GUNNER)
					{
						SkinnedMeshRenderer[] componentsInChildren = this.cc.currCar.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							componentsInChildren[i].enabled = false;
						}
					}
					else
					{
						this._trooper = this.cc.currCar.Gunner.activeSelf;
						this._helmet = this.cc.currCar.GunnerHelmet.activeSelf;
						this._cap = this.cc.currCar.GunnerCap.activeSelf;
						this._budge = this.cc.currCar.GunnerBudge.activeSelf;
						this.cc.currCar.UnactiveGunner();
					}
				}
			}
			else if (this.vehicleType == CONST.VEHICLES.TANKS)
			{
				SkinnedMeshRenderer[] componentsInChildren = this.target.GetComponentInChildren<Tank>().gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = true;
				}
			}
			else if (this.vehicleType == CONST.VEHICLES.JEEP)
			{
				SkinnedMeshRenderer[] componentsInChildren = this.cc.currCar.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = true;
				}
				this.cc.currCar.ActiveGunner(this._trooper, this._helmet, this._cap, this._budge);
				this._trooper = false;
				this._helmet = false;
				this._cap = false;
				this._budge = false;
			}
		}
		if (this.zoom)
		{
			if (this.vehicleType == CONST.VEHICLES.TANKS)
			{
				this.koefX1 = -18f;
				this.koefX2 = 9f;
				this.visota = 2.3f;
				this.distance = -1f;
			}
			else
			{
				if (this.cc == null)
				{
					this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
				}
				if (this.cc.myPosition == CONST.VEHICLES.POSITION_JEEP_GUNNER)
				{
					this.koefX1 = -10f;
					this.koefX2 = 5f;
					this.visota = 3f;
					this.distance = -0.1f;
				}
				else
				{
					this.koefX1 = -18f;
					this.koefX2 = 9f;
					this.visota = 1.5f;
					this.distance = -1f;
				}
			}
		}
		else
		{
			this.koefX1 = -20f;
			this.koefX2 = 18f;
			this.visota = 5f;
			this.distance = 10f;
		}
		if (this.vehicleType == CONST.VEHICLES.TANKS)
		{
			if (this.tc == null)
			{
				this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
			}
			if (this.tc.activeControl && this.target)
			{
				this.x += Input.GetAxis("Mouse X") * this.xSpeed * Time.deltaTime;
				this.y -= Input.GetAxis("Mouse Y") * this.ySpeed * Time.deltaTime;
				float num = this.x;
				float num2 = this.myTransform.rotation.eulerAngles.y - num;
				if (num2 > 180f)
				{
					num += 360f;
				}
				if (num2 < -180f)
				{
					num -= 360f;
				}
				float num3 = this.y;
				if (num3 < 1f)
				{
					this.y = Mathf.Max(num3, this.koefX1);
				}
				if (num3 >= 1f)
				{
					this.y = Mathf.Min(num3, this.koefX2);
				}
				num = Mathf.Lerp(num, this.myTransform.rotation.eulerAngles.y, Time.deltaTime * 12f);
				this.x = num;
				if (!this.zoom)
				{
					this.ray = new Ray(new Vector3(this.target.position.x, this.target.position.y + 2.5f, this.target.position.z), Quaternion.Euler(this.y, this.x, 0f) * new Vector3(0f, this.visota, -this.distance));
					this.ray2 = new Ray(new Vector3(this.target.position.x, this.target.position.y + 2.5f, this.target.position.z), new Vector3(this.target.position.x, this.target.position.y + 5f, this.target.position.z));
					if (Physics.Raycast(this.ray, ref this.hit, 10f, 1))
					{
						this.distance = Vector3.Distance(new Vector3(this.target.position.x, this.target.position.y + 2.5f, this.target.position.z), this.hit.point) - 2f;
						this.visota = Mathf.Max(3f, this.hit.point.y - this.target.position.y);
						Debug.DrawLine(this.target.position, this.hit.point);
						return;
					}
				}
			}
		}
		else if (this.vehicleType == CONST.VEHICLES.JEEP)
		{
			if (this.cc == null)
			{
				this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
			}
			if (this.cc.activeControl && this.target)
			{
				if (this.cc.myPosition == CONST.VEHICLES.POSITION_JEEP_GUNNER || !this.zoom)
				{
					this.x += Input.GetAxis("Mouse X") * this.xSpeed * Time.deltaTime;
					this.y -= Input.GetAxis("Mouse Y") * this.ySpeed * Time.deltaTime;
					this.z = 0f;
				}
				else
				{
					this.y = this.target.rotation.eulerAngles.x;
					this.x = this.target.rotation.eulerAngles.y;
					this.z = this.target.rotation.eulerAngles.z;
				}
				float num4 = this.x;
				float num5 = this.myTransform.rotation.eulerAngles.y - num4;
				if (num5 > 180f)
				{
					num4 += 360f;
				}
				if (num5 < -180f)
				{
					num4 -= 360f;
				}
				float num6 = this.y;
				if (this.cc.myPosition == CONST.VEHICLES.POSITION_JEEP_GUNNER || !this.zoom)
				{
					if (num6 < 1f)
					{
						this.y = Mathf.Max(num6, this.koefX1);
					}
					if (num6 >= 1f)
					{
						this.y = Mathf.Min(num6, this.koefX2);
					}
				}
				num4 = Mathf.Lerp(num4, this.myTransform.rotation.eulerAngles.y, Time.deltaTime * 12f);
				this.x = num4;
				if (!this.zoom)
				{
					this.ray = new Ray(new Vector3(this.target.position.x, this.target.position.y + 2.5f, this.target.position.z), Quaternion.Euler(this.y, this.x, 0f) * new Vector3(0f, this.visota, -this.distance));
					this.ray2 = new Ray(new Vector3(this.target.position.x, this.target.position.y + 2.5f, this.target.position.z), new Vector3(this.target.position.x, this.target.position.y + 5f, this.target.position.z));
					if (Physics.Raycast(this.ray, ref this.hit, 10f, 1))
					{
						this.distance = Vector3.Distance(new Vector3(this.target.position.x, this.target.position.y + 2.5f, this.target.position.z), this.hit.point) - 2f;
						this.visota = Mathf.Max(3f, this.hit.point.y - this.target.position.y);
						Debug.DrawLine(this.target.position, this.hit.point);
					}
				}
			}
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x000326F8 File Offset: 0x000308F8
	private void LateUpdate()
	{
		if (this.target == null)
		{
			return;
		}
		this.myTransform.rotation = Quaternion.Euler(this.y, this.x, this.z);
		this.myTransform.position = Quaternion.Euler(this.y, this.x, this.z) * new Vector3(0f, this.visota, -this.distance) + this.target.position;
		this.originalPos = this.myTransform.localPosition;
		if (this.shake > 0f)
		{
			this.myTransform.localPosition = this.originalPos + Random.insideUnitSphere * (this.shakeAmount - (this.shakeAmount - this.shakeAmount * this.shake));
			this.shake -= Time.deltaTime * this.decreaseFactor;
			return;
		}
		this.shake = 0f;
		this.myTransform.localPosition = this.originalPos;
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00032814 File Offset: 0x00030A14
	private void OnGUI()
	{
		if (this.target == null)
		{
			return;
		}
		if (this.zoom)
		{
			if (this.vehicleType == CONST.VEHICLES.TANKS)
			{
				this.txt = this.tankZoom;
			}
			else if (this.vehicleType == CONST.VEHICLES.JEEP)
			{
				this.txt = this.carZoom;
			}
			if (this.cc == null)
			{
				this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
			}
			if (this.cc.myPosition == CONST.VEHICLES.POSITION_JEEP_GUNNER)
			{
				return;
			}
			float num = (float)Screen.height * 1.33333f;
			float num2 = ((float)Screen.width - num) / 2f;
			float num3 = 0f;
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			else
			{
				num3 *= 300f;
			}
			GUI.DrawTexture(new Rect(num2 + -num3 / 2f, -num3 / 2f, num + num3, (float)Screen.height + num3), this.txt);
			GUI.DrawTexture(new Rect(0f, 0f, num2, (float)Screen.height), GUIManager.tex_black);
			GUI.DrawTexture(new Rect(num + num2, 0f, num2, (float)Screen.height), GUIManager.tex_black);
		}
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0003294F File Offset: 0x00030B4F
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x040003B0 RID: 944
	public Transform target;

	// Token: 0x040003B1 RID: 945
	public float distance = 10f;

	// Token: 0x040003B2 RID: 946
	public float visota = 3f;

	// Token: 0x040003B3 RID: 947
	public float xSpeed = 250f;

	// Token: 0x040003B4 RID: 948
	public float ySpeed = 120f;

	// Token: 0x040003B5 RID: 949
	public float yMinLimit = -20f;

	// Token: 0x040003B6 RID: 950
	public float yMaxLimit = 80f;

	// Token: 0x040003B7 RID: 951
	private float x;

	// Token: 0x040003B8 RID: 952
	private float y;

	// Token: 0x040003B9 RID: 953
	private float z;

	// Token: 0x040003BA RID: 954
	private float sSpeed = 1f;

	// Token: 0x040003BB RID: 955
	private float sMinDistance = 2f;

	// Token: 0x040003BC RID: 956
	private float sMaxDistance = 20f;

	// Token: 0x040003BD RID: 957
	private TankController tc;

	// Token: 0x040003BE RID: 958
	private CarController cc;

	// Token: 0x040003BF RID: 959
	private Texture tankZoom;

	// Token: 0x040003C0 RID: 960
	private Texture carZoom;

	// Token: 0x040003C1 RID: 961
	public bool zoom;

	// Token: 0x040003C2 RID: 962
	private Ray ray;

	// Token: 0x040003C3 RID: 963
	private Ray ray2;

	// Token: 0x040003C4 RID: 964
	private RaycastHit hit;

	// Token: 0x040003C5 RID: 965
	private Transform myTransform;

	// Token: 0x040003C6 RID: 966
	public Transform zoomPoint;

	// Token: 0x040003C7 RID: 967
	public vp_FPPlayerEventHandler Player;

	// Token: 0x040003C8 RID: 968
	public float shake;

	// Token: 0x040003C9 RID: 969
	public float shakeAmount = 0.1f;

	// Token: 0x040003CA RID: 970
	public float decreaseFactor = 1f;

	// Token: 0x040003CB RID: 971
	private Vector3 originalPos;

	// Token: 0x040003CC RID: 972
	private float koefX1;

	// Token: 0x040003CD RID: 973
	private float koefX2;

	// Token: 0x040003CE RID: 974
	public int vehicleType;

	// Token: 0x040003CF RID: 975
	private bool _trooper;

	// Token: 0x040003D0 RID: 976
	private bool _helmet;

	// Token: 0x040003D1 RID: 977
	private bool _cap;

	// Token: 0x040003D2 RID: 978
	private bool _budge;

	// Token: 0x040003D3 RID: 979
	private Texture txt;
}
