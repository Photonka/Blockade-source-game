using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
[AddComponentMenu("Camera-Control/Smooth Mouse Orbit - Unluck Software")]
public class SmoothCameraOrbit : MonoBehaviour
{
	// Token: 0x06000AD1 RID: 2769 RVA: 0x0008B939 File Offset: 0x00089B39
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0008B939 File Offset: 0x00089B39
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0008B944 File Offset: 0x00089B44
	public void Init()
	{
		if (!this.target)
		{
			this.target = new GameObject("Cam Target")
			{
				transform = 
				{
					position = base.transform.position + base.transform.forward * this.distance
				}
			}.transform;
		}
		this.currentDistance = this.distance;
		this.desiredDistance = this.distance;
		this.position = base.transform.position;
		this.rotation = base.transform.rotation;
		this.currentRotation = base.transform.rotation;
		this.desiredRotation = base.transform.rotation;
		this.xDeg = Vector3.Angle(Vector3.right, base.transform.right);
		this.yDeg = Vector3.Angle(Vector3.up, base.transform.up);
		this.position = this.target.position - (this.rotation * Vector3.forward * this.currentDistance + this.targetOffset);
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0008BA74 File Offset: 0x00089C74
	private void LateUpdate()
	{
		if (Input.GetMouseButton(2) && Input.GetKey(308) && Input.GetKey(306))
		{
			this.desiredDistance -= Input.GetAxis("Mouse Y") * 0.02f * (float)this.zoomRate * 0.125f * Mathf.Abs(this.desiredDistance);
		}
		else if (Input.GetMouseButton(0))
		{
			this.xDeg += Input.GetAxis("Mouse X") * this.xSpeed * 0.02f;
			this.yDeg -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
			this.yDeg = SmoothCameraOrbit.ClampAngle(this.yDeg, (float)this.yMinLimit, (float)this.yMaxLimit);
			this.desiredRotation = Quaternion.Euler(this.yDeg, this.xDeg, 0f);
			this.currentRotation = base.transform.rotation;
			this.rotation = Quaternion.Lerp(this.currentRotation, this.desiredRotation, 0.02f * this.zoomDampening);
			base.transform.rotation = this.rotation;
			this.idleTimer = 0f;
			this.idleSmooth = 0f;
		}
		else
		{
			this.idleTimer += 0.02f;
			if (this.idleTimer > this.autoRotate && this.autoRotate > 0f)
			{
				this.idleSmooth += (0.02f + this.idleSmooth) * 0.005f;
				this.idleSmooth = Mathf.Clamp(this.idleSmooth, 0f, 1f);
				this.xDeg += this.xSpeed * Time.deltaTime * this.idleSmooth * this.autoRotateSpeed;
			}
			this.yDeg = SmoothCameraOrbit.ClampAngle(this.yDeg, (float)this.yMinLimit, (float)this.yMaxLimit);
			this.desiredRotation = Quaternion.Euler(this.yDeg, this.xDeg, 0f);
			this.currentRotation = base.transform.rotation;
			this.rotation = Quaternion.Lerp(this.currentRotation, this.desiredRotation, 0.02f * this.zoomDampening * 2f);
			base.transform.rotation = this.rotation;
		}
		this.desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * 0.02f * (float)this.zoomRate * Mathf.Abs(this.desiredDistance);
		this.desiredDistance = Mathf.Clamp(this.desiredDistance, this.minDistance, this.maxDistance);
		this.currentDistance = Mathf.Lerp(this.currentDistance, this.desiredDistance, 0.02f * this.zoomDampening);
		this.position = this.target.position - (this.rotation * Vector3.forward * this.currentDistance + this.targetOffset);
		base.transform.position = this.position;
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0003294F File Offset: 0x00030B4F
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

	// Token: 0x0400109C RID: 4252
	public Transform target;

	// Token: 0x0400109D RID: 4253
	public Vector3 targetOffset;

	// Token: 0x0400109E RID: 4254
	public float distance = 5f;

	// Token: 0x0400109F RID: 4255
	public float maxDistance = 20f;

	// Token: 0x040010A0 RID: 4256
	public float minDistance = 0.6f;

	// Token: 0x040010A1 RID: 4257
	public float xSpeed = 200f;

	// Token: 0x040010A2 RID: 4258
	public float ySpeed = 200f;

	// Token: 0x040010A3 RID: 4259
	public int yMinLimit = -80;

	// Token: 0x040010A4 RID: 4260
	public int yMaxLimit = 80;

	// Token: 0x040010A5 RID: 4261
	public int zoomRate = 40;

	// Token: 0x040010A6 RID: 4262
	public float panSpeed = 0.3f;

	// Token: 0x040010A7 RID: 4263
	public float zoomDampening = 5f;

	// Token: 0x040010A8 RID: 4264
	public float autoRotate = 1f;

	// Token: 0x040010A9 RID: 4265
	public float autoRotateSpeed = 0.1f;

	// Token: 0x040010AA RID: 4266
	private float xDeg;

	// Token: 0x040010AB RID: 4267
	private float yDeg;

	// Token: 0x040010AC RID: 4268
	private float currentDistance;

	// Token: 0x040010AD RID: 4269
	private float desiredDistance;

	// Token: 0x040010AE RID: 4270
	private Quaternion currentRotation;

	// Token: 0x040010AF RID: 4271
	private Quaternion desiredRotation;

	// Token: 0x040010B0 RID: 4272
	private Quaternion rotation;

	// Token: 0x040010B1 RID: 4273
	private Vector3 position;

	// Token: 0x040010B2 RID: 4274
	private float idleTimer;

	// Token: 0x040010B3 RID: 4275
	private float idleSmooth;
}
