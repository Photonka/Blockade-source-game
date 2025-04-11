using System;
using UnityEngine;

// Token: 0x02000122 RID: 290
[AddComponentMenu("Camera-Control/Mouse Look")]
public class SmoothMouseLook : MonoBehaviour
{
	// Token: 0x06000A3F RID: 2623 RVA: 0x00087058 File Offset: 0x00085258
	private void Update()
	{
		if (!this.Active)
		{
			return;
		}
		this.zoom_sensitivity = this._camera.GetComponent<Camera>().fieldOfView / 65f;
		if (this.axes == SmoothMouseLook.RotationAxes.MouseXAndY)
		{
			this.xInputOld = this.xInput;
			this.yInputOld = this.yInput;
			this.xInput = Input.GetAxis("Mouse X") * this.sensitivityX * this.zoom_sensitivity;
			this.yInput = Input.GetAxis("Mouse Y") * this.sensitivityY * this.zoom_sensitivity;
			this.averageXInput = this.xInput + this.xInputOld;
			this.averageYInput = this.yInput + this.yInputOld;
			this.averageXInput *= 0.5f;
			this.averageYInput *= 0.5f;
			this.rotationX += this.averageXInput;
			this.rotationY += this.averageYInput;
			this.rotationX = SmoothMouseLook.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
			this.rotationY = SmoothMouseLook.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
			Quaternion quaternion = Quaternion.AngleAxis(this.rotationX, Vector3.up);
			Quaternion quaternion2 = Quaternion.AngleAxis(this.rotationY, Vector3.left);
			base.transform.localRotation = this.originalRotation * quaternion * quaternion2;
			return;
		}
		if (this.axes == SmoothMouseLook.RotationAxes.MouseX)
		{
			this.xInputOld = this.xInput;
			this.xInput = Input.GetAxis("Mouse X") * this.sensitivityX * this.zoom_sensitivity;
			this.averageXInput = this.xInput + this.xInputOld;
			this.averageXInput *= 0.5f;
			this.rotationX += this.averageXInput;
			this.rotationX = SmoothMouseLook.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
			Quaternion quaternion3 = Quaternion.AngleAxis(this.rotationX, Vector3.up);
			base.transform.localRotation = this.originalRotation * quaternion3;
			return;
		}
		this.yInputOld = this.yInput;
		this.yInput = Input.GetAxis("Mouse Y") * this.sensitivityY * this.zoom_sensitivity;
		this.averageYInput = this.yInput + this.yInputOld;
		this.averageYInput *= 0.5f;
		this.rotationY += this.averageYInput;
		this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY * this.zoom_sensitivity;
		this.rotationY = SmoothMouseLook.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
		Quaternion quaternion4 = Quaternion.AngleAxis(this.rotationY, Vector3.left);
		base.transform.localRotation = this.originalRotation * quaternion4;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x00087354 File Offset: 0x00085554
	private void Start()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
		this.originalRotation = base.transform.localRotation;
		this._camera = GameObject.Find("Main Camera");
		this.sensitivityX = Config.Sensitivity;
		this.sensitivityY = Config.Sensitivity;
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0003294F File Offset: 0x00030B4F
	public static float ClampAngle(float angle, float min, float max)
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

	// Token: 0x04000F8A RID: 3978
	public bool Active = true;

	// Token: 0x04000F8B RID: 3979
	public SmoothMouseLook.RotationAxes axes;

	// Token: 0x04000F8C RID: 3980
	public float sensitivityX = 3f;

	// Token: 0x04000F8D RID: 3981
	public float sensitivityY = 3f;

	// Token: 0x04000F8E RID: 3982
	public float minimumX = -360f;

	// Token: 0x04000F8F RID: 3983
	public float maximumX = 360f;

	// Token: 0x04000F90 RID: 3984
	public float minimumY = -60f;

	// Token: 0x04000F91 RID: 3985
	public float maximumY = 60f;

	// Token: 0x04000F92 RID: 3986
	private float xInput;

	// Token: 0x04000F93 RID: 3987
	private float yInput;

	// Token: 0x04000F94 RID: 3988
	private float xInputOld;

	// Token: 0x04000F95 RID: 3989
	private float yInputOld;

	// Token: 0x04000F96 RID: 3990
	private float averageXInput;

	// Token: 0x04000F97 RID: 3991
	private float averageYInput;

	// Token: 0x04000F98 RID: 3992
	private float rotationX;

	// Token: 0x04000F99 RID: 3993
	private float rotationY;

	// Token: 0x04000F9A RID: 3994
	private Quaternion originalRotation;

	// Token: 0x04000F9B RID: 3995
	private GameObject _camera;

	// Token: 0x04000F9C RID: 3996
	private float zoom_sensitivity = 1f;

	// Token: 0x020008B2 RID: 2226
	public enum RotationAxes
	{
		// Token: 0x04003305 RID: 13061
		MouseXAndY,
		// Token: 0x04003306 RID: 13062
		MouseX,
		// Token: 0x04003307 RID: 13063
		MouseY
	}
}
