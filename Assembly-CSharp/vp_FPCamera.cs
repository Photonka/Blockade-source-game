using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CD RID: 205
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(AudioListener))]
public class vp_FPCamera : vp_Component
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0007105C File Offset: 0x0006F25C
	// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00071064 File Offset: 0x0006F264
	public bool DrawCameraCollisionDebugLine
	{
		get
		{
			return this.m_DrawCameraCollisionDebugLine;
		}
		set
		{
			this.m_DrawCameraCollisionDebugLine = value;
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0007106D File Offset: 0x0006F26D
	private vp_FPPlayerEventHandler Player
	{
		get
		{
			if (this.m_Player == null && this.EventHandler != null)
			{
				this.m_Player = (vp_FPPlayerEventHandler)this.EventHandler;
			}
			return this.m_Player;
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000710A2 File Offset: 0x0006F2A2
	// (set) Token: 0x060006E5 RID: 1765 RVA: 0x000710B5 File Offset: 0x0006F2B5
	public Vector2 Angle
	{
		get
		{
			return new Vector2(this.m_Pitch, this.m_Yaw);
		}
		set
		{
			this.Pitch = value.x;
			this.Yaw = value.y;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060006E6 RID: 1766 RVA: 0x000710CF File Offset: 0x0006F2CF
	public Vector3 Forward
	{
		get
		{
			return this.m_Transform.forward;
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060006E7 RID: 1767 RVA: 0x000710DC File Offset: 0x0006F2DC
	// (set) Token: 0x060006E8 RID: 1768 RVA: 0x000710E4 File Offset: 0x0006F2E4
	public float Pitch
	{
		get
		{
			return this.m_Pitch;
		}
		set
		{
			if (value > 90f)
			{
				value -= 360f;
			}
			this.m_Pitch = value;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060006E9 RID: 1769 RVA: 0x000710FE File Offset: 0x0006F2FE
	// (set) Token: 0x060006EA RID: 1770 RVA: 0x00071106 File Offset: 0x0006F306
	public float Yaw
	{
		get
		{
			return this.m_Yaw;
		}
		set
		{
			this.m_Yaw = value;
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00071110 File Offset: 0x0006F310
	protected override void Awake()
	{
		base.Awake();
		this.FPController = base.Root.GetComponent<vp_FPController>();
		this.m_InitialRotation = new Vector2(base.Transform.eulerAngles.y, base.Transform.eulerAngles.x);
		base.Parent.gameObject.layer = 30;
		foreach (object obj in base.Parent)
		{
			Transform transform = (Transform)obj;
			if (!(transform.gameObject.name == "Snow"))
			{
				transform.gameObject.layer = 30;
			}
		}
		base.GetComponent<Camera>().cullingMask &= 1073741823;
		base.GetComponent<Camera>().depth = 0f;
		foreach (object obj2 in base.Transform)
		{
			Camera camera = (Camera)((Transform)obj2).GetComponent(typeof(Camera));
			if (camera != null)
			{
				camera.transform.localPosition = Vector3.zero;
				camera.transform.localEulerAngles = Vector3.zero;
				camera.clearFlags = 3;
				camera.cullingMask = int.MinValue;
				camera.depth = 1f;
				camera.farClipPlane = 100f;
				camera.nearClipPlane = 0.01f;
				camera.fieldOfView = 65f;
				break;
			}
		}
		this.m_PositionSpring = new vp_Spring(base.Transform, vp_Spring.UpdateMode.Position, false);
		this.m_PositionSpring.MinVelocity = 1E-05f;
		this.m_PositionSpring.RestState = this.PositionOffset;
		this.m_PositionSpring2 = new vp_Spring(base.Transform, vp_Spring.UpdateMode.PositionAdditive, false);
		this.m_PositionSpring2.MinVelocity = 1E-05f;
		this.m_RotationSpring = new vp_Spring(base.Transform, vp_Spring.UpdateMode.RotationAdditive, false);
		this.m_RotationSpring.MinVelocity = 1E-05f;
		float sensitivity = Config.Sensitivity;
		this.MouseSensitivityRestore = new Vector2(sensitivity, sensitivity);
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0007135C File Offset: 0x0006F55C
	protected override void Start()
	{
		base.Start();
		this.Refresh();
		this.SnapSprings();
		this.SnapZoom();
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x00071376 File Offset: 0x0006F576
	protected override void Init()
	{
		base.Init();
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0007137E File Offset: 0x0006F57E
	protected override void Update()
	{
		base.Update();
		if (Time.timeScale == 0f)
		{
			return;
		}
		this.UpdateMouseLook();
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00071399 File Offset: 0x0006F599
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (Time.timeScale == 0f)
		{
			return;
		}
		this.UpdateZoom();
		this.UpdateSwaying();
		this.UpdateBob();
		this.UpdateEarthQuake();
		this.UpdateShakes();
		this.UpdateSprings();
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x000713D4 File Offset: 0x0006F5D4
	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (Time.timeScale == 0f)
		{
			return;
		}
		this.m_Transform.position = this.FPController.SmoothPosition;
		this.m_Transform.localPosition += this.m_PositionSpring.State + this.m_PositionSpring2.State;
		this.DoCameraCollision();
		Quaternion quaternion = Quaternion.AngleAxis(this.m_Yaw + this.m_InitialRotation.x, Vector3.up);
		Quaternion quaternion2 = Quaternion.AngleAxis(0f, Vector3.left);
		base.Parent.rotation = vp_Utility.NaNSafeQuaternion(quaternion * quaternion2, base.Parent.rotation);
		quaternion2 = Quaternion.AngleAxis(-this.m_Pitch - this.m_InitialRotation.y, Vector3.left);
		base.Transform.rotation = vp_Utility.NaNSafeQuaternion(quaternion * quaternion2, base.Transform.rotation);
		base.Transform.localEulerAngles += vp_Utility.NaNSafeVector3(Vector3.forward * this.m_RotationSpring.State.z, default(Vector3));
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x00071510 File Offset: 0x0006F710
	protected virtual void DoCameraCollision()
	{
		this.m_CameraCollisionStartPos = this.FPController.Transform.TransformPoint(0f, this.PositionOffset.y, 0f);
		this.m_CameraCollisionEndPos = base.Transform.position + (base.Transform.position - this.m_CameraCollisionStartPos).normalized * this.FPController.CharacterController.radius;
		if (Physics.Linecast(this.m_CameraCollisionStartPos, this.m_CameraCollisionEndPos, ref this.m_CameraHit, -1744831509) && !this.m_CameraHit.collider.isTrigger)
		{
			base.Transform.position = this.m_CameraHit.point - (this.m_CameraHit.point - this.m_CameraCollisionStartPos).normalized * this.FPController.CharacterController.radius;
		}
		if (base.Transform.localPosition.y < this.PositionGroundLimit)
		{
			base.Transform.localPosition = new Vector3(base.Transform.localPosition.x, this.PositionGroundLimit, base.Transform.localPosition.z);
		}
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0007165D File Offset: 0x0006F85D
	public virtual void AddForce(Vector3 force)
	{
		this.m_PositionSpring.AddForce(force);
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0007166B File Offset: 0x0006F86B
	public virtual void AddForce(float x, float y, float z)
	{
		this.AddForce(new Vector3(x, y, z));
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0007167B File Offset: 0x0006F87B
	public virtual void AddForce2(Vector3 force)
	{
		this.m_PositionSpring2.AddForce(force);
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x00071689 File Offset: 0x0006F889
	public void AddForce2(float x, float y, float z)
	{
		this.AddForce2(new Vector3(x, y, z));
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x00071699 File Offset: 0x0006F899
	public virtual void AddRollForce(float force)
	{
		this.m_RotationSpring.AddForce(Vector3.forward * force);
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x000716B1 File Offset: 0x0006F8B1
	public virtual void AddRotationForce(Vector3 force)
	{
		this.m_RotationSpring.AddForce(force);
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x000716BF File Offset: 0x0006F8BF
	public void AddRotationForce(float x, float y, float z)
	{
		this.AddRotationForce(new Vector3(x, y, z));
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x000716D0 File Offset: 0x0006F8D0
	protected virtual void UpdateMouseLook()
	{
		if (MainGUI.ForceCursor)
		{
			return;
		}
		this.m_MouseMove.x = Input.GetAxisRaw("Mouse X") * Time.timeScale;
		this.m_MouseMove.y = Input.GetAxisRaw("Mouse Y") * Time.timeScale;
		this.MouseSmoothSteps = Mathf.Clamp(this.MouseSmoothSteps, 1, 20);
		this.MouseSmoothWeight = Mathf.Clamp01(this.MouseSmoothWeight);
		while (this.m_MouseSmoothBuffer.Count > this.MouseSmoothSteps)
		{
			this.m_MouseSmoothBuffer.RemoveAt(0);
		}
		this.m_MouseSmoothBuffer.Add(this.m_MouseMove);
		float num = 1f;
		Vector2 vector = Vector2.zero;
		float num2 = 0f;
		for (int i = this.m_MouseSmoothBuffer.Count - 1; i > 0; i--)
		{
			vector += this.m_MouseSmoothBuffer[i] * num;
			num2 += 1f * num;
			num *= this.MouseSmoothWeight / base.Delta;
		}
		num2 = Mathf.Max(1f, num2);
		Vector2 vector2 = vp_Utility.NaNSafeVector2(vector / num2, default(Vector2));
		float num3 = 0f;
		float num4 = Mathf.Abs(vector2.x);
		float num5 = Mathf.Abs(vector2.y);
		if (this.MouseAcceleration)
		{
			num3 = Mathf.Sqrt(num4 * num4 + num5 * num5) / base.Delta;
			num3 = ((num3 <= this.MouseAccelerationThreshold) ? 0f : num3);
		}
		float num6 = Camera.main.fieldOfView / 65f;
		this.m_Yaw += vector2.x * (this.MouseSensitivityRestore.x * num6 + num3);
		this.m_Pitch -= vector2.y * (this.MouseSensitivityRestore.y * num6 + num3);
		this.m_Yaw = ((this.m_Yaw < -360f) ? (this.m_Yaw += 360f) : this.m_Yaw);
		this.m_Yaw = ((this.m_Yaw > 360f) ? (this.m_Yaw -= 360f) : this.m_Yaw);
		this.m_Yaw = Mathf.Clamp(this.m_Yaw, this.RotationYawLimit.x, this.RotationYawLimit.y);
		this.m_Pitch = ((this.m_Pitch < -360f) ? (this.m_Pitch += 360f) : this.m_Pitch);
		this.m_Pitch = ((this.m_Pitch > 360f) ? (this.m_Pitch -= 360f) : this.m_Pitch);
		this.m_Pitch = Mathf.Clamp(this.m_Pitch, -this.RotationPitchLimit.x, -this.RotationPitchLimit.y);
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x000719C4 File Offset: 0x0006FBC4
	protected virtual void UpdateZoom()
	{
		if (this.m_FinalZoomTime <= Time.time)
		{
			return;
		}
		this.RenderingZoomDamping = Mathf.Max(this.RenderingZoomDamping, 0.01f);
		float num = 1f - (this.m_FinalZoomTime - Time.time) / this.RenderingZoomDamping;
		base.gameObject.GetComponent<Camera>().fieldOfView = Mathf.SmoothStep(base.gameObject.GetComponent<Camera>().fieldOfView, this.RenderingFieldOfView, num);
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00071A3B File Offset: 0x0006FC3B
	public virtual void Zoom()
	{
		this.m_FinalZoomTime = Time.time + this.RenderingZoomDamping;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00071A4F File Offset: 0x0006FC4F
	public virtual void SnapZoom()
	{
		base.gameObject.GetComponent<Camera>().fieldOfView = this.RenderingFieldOfView;
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x00071A68 File Offset: 0x0006FC68
	protected virtual void UpdateShakes()
	{
		if (this.ShakeSpeed != 0f)
		{
			this.m_Yaw -= this.m_Shake.y;
			this.m_Pitch -= this.m_Shake.x;
			this.m_Shake = Vector3.Scale(vp_SmoothRandom.GetVector3Centered(this.ShakeSpeed), this.ShakeAmplitude);
			this.m_Yaw += this.m_Shake.y;
			this.m_Pitch += this.m_Shake.x;
			this.m_RotationSpring.AddForce(Vector3.forward * this.m_Shake.z * Time.timeScale);
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00071B2C File Offset: 0x0006FD2C
	protected virtual void UpdateBob()
	{
		if (this.BobAmplitude == Vector4.zero || this.BobRate == Vector4.zero)
		{
			return;
		}
		this.m_BobSpeed = ((this.BobRequireGroundContact && !this.FPController.Grounded) ? 0f : this.FPController.CharacterController.velocity.sqrMagnitude);
		this.m_BobSpeed = Mathf.Min(this.m_BobSpeed * this.BobInputVelocityScale, this.BobMaxInputVelocity);
		this.m_BobSpeed = Mathf.Round(this.m_BobSpeed * 1000f) / 1000f;
		if (this.m_BobSpeed == 0f)
		{
			this.m_BobSpeed = Mathf.Min(this.m_LastBobSpeed * 0.93f, this.BobMaxInputVelocity);
		}
		this.m_CurrentBobAmp.y = this.m_BobSpeed * (this.BobAmplitude.y * -0.0001f);
		this.m_CurrentBobVal.y = Mathf.Cos(Time.time * (this.BobRate.y * 10f)) * this.m_CurrentBobAmp.y;
		this.m_CurrentBobAmp.x = this.m_BobSpeed * (this.BobAmplitude.x * 0.0001f);
		this.m_CurrentBobVal.x = Mathf.Cos(Time.time * (this.BobRate.x * 10f)) * this.m_CurrentBobAmp.x;
		this.m_CurrentBobAmp.z = this.m_BobSpeed * (this.BobAmplitude.z * 0.0001f);
		this.m_CurrentBobVal.z = Mathf.Cos(Time.time * (this.BobRate.z * 10f)) * this.m_CurrentBobAmp.z;
		this.m_CurrentBobAmp.w = this.m_BobSpeed * (this.BobAmplitude.w * 0.0001f);
		this.m_CurrentBobVal.w = Mathf.Cos(Time.time * (this.BobRate.w * 10f)) * this.m_CurrentBobAmp.w;
		this.m_PositionSpring.AddForce(this.m_CurrentBobVal * Time.timeScale);
		this.AddRollForce(this.m_CurrentBobVal.w * Time.timeScale);
		this.m_LastBobSpeed = this.m_BobSpeed;
		this.DetectBobStep(this.m_BobSpeed, this.m_CurrentBobVal.y);
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00071DB0 File Offset: 0x0006FFB0
	protected virtual void DetectBobStep(float speed, float upBob)
	{
		if (this.BobStepCallback == null)
		{
			return;
		}
		if (speed < this.BobStepThreshold)
		{
			return;
		}
		bool flag = this.m_LastUpBob < upBob;
		this.m_LastUpBob = upBob;
		if (flag && !this.m_BobWasElevating)
		{
			this.BobStepCallback();
		}
		this.m_BobWasElevating = flag;
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00071E04 File Offset: 0x00070004
	protected virtual void UpdateSwaying()
	{
		Vector3 vector = base.Transform.InverseTransformDirection(this.FPController.CharacterController.velocity * 0.016f) * Time.timeScale;
		this.AddRollForce(vector.x * this.RotationStrafeRoll);
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00071E54 File Offset: 0x00070054
	protected virtual void UpdateEarthQuake()
	{
		if (this.Player == null)
		{
			return;
		}
		if (!this.Player.Earthquake.Active)
		{
			return;
		}
		if (this.m_PositionSpring.State.y >= this.m_PositionSpring.RestState.y)
		{
			Vector3 vector = this.Player.EarthQuakeForce.Get();
			vector.y = -vector.y;
			this.Player.EarthQuakeForce.Set(vector);
		}
		this.m_PositionSpring.AddForce(this.Player.EarthQuakeForce.Get() * this.PositionEarthQuakeFactor);
		this.m_RotationSpring.AddForce(Vector3.forward * (-this.Player.EarthQuakeForce.Get().x * 2f) * this.RotationEarthQuakeFactor);
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00071F4B File Offset: 0x0007014B
	protected virtual void UpdateSprings()
	{
		this.m_PositionSpring.FixedUpdate();
		this.m_PositionSpring2.FixedUpdate();
		this.m_RotationSpring.FixedUpdate();
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x00071F70 File Offset: 0x00070170
	public virtual void DoBomb(Vector3 positionForce, float minRollForce, float maxRollForce)
	{
		this.AddForce2(positionForce);
		float num = Random.Range(minRollForce, maxRollForce);
		if (Random.value > 0.5f)
		{
			num = -num;
		}
		this.AddRollForce(num);
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x00071FA4 File Offset: 0x000701A4
	public override void Refresh()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.m_PositionSpring != null)
		{
			this.m_PositionSpring.Stiffness = new Vector3(this.PositionSpringStiffness, this.PositionSpringStiffness, this.PositionSpringStiffness);
			this.m_PositionSpring.Damping = Vector3.one - new Vector3(this.PositionSpringDamping, this.PositionSpringDamping, this.PositionSpringDamping);
			this.m_PositionSpring.MinState.y = this.PositionGroundLimit;
			this.m_PositionSpring.RestState = this.PositionOffset;
		}
		if (this.m_PositionSpring2 != null)
		{
			this.m_PositionSpring2.Stiffness = new Vector3(this.PositionSpring2Stiffness, this.PositionSpring2Stiffness, this.PositionSpring2Stiffness);
			this.m_PositionSpring2.Damping = Vector3.one - new Vector3(this.PositionSpring2Damping, this.PositionSpring2Damping, this.PositionSpring2Damping);
			this.m_PositionSpring2.MinState.y = -this.PositionOffset.y + this.PositionGroundLimit;
		}
		if (this.m_RotationSpring != null)
		{
			this.m_RotationSpring.Stiffness = new Vector3(this.RotationSpringStiffness, this.RotationSpringStiffness, this.RotationSpringStiffness);
			this.m_RotationSpring.Damping = Vector3.one - new Vector3(this.RotationSpringDamping, this.RotationSpringDamping, this.RotationSpringDamping);
		}
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x00072108 File Offset: 0x00070308
	public virtual void SnapSprings()
	{
		if (this.m_PositionSpring != null)
		{
			this.m_PositionSpring.RestState = this.PositionOffset;
			this.m_PositionSpring.State = this.PositionOffset;
			this.m_PositionSpring.Stop(true);
		}
		if (this.m_PositionSpring2 != null)
		{
			this.m_PositionSpring2.RestState = Vector3.zero;
			this.m_PositionSpring2.State = Vector3.zero;
			this.m_PositionSpring2.Stop(true);
		}
		if (this.m_RotationSpring != null)
		{
			this.m_RotationSpring.RestState = Vector3.zero;
			this.m_RotationSpring.State = Vector3.zero;
			this.m_RotationSpring.Stop(true);
		}
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x000721B4 File Offset: 0x000703B4
	public virtual void StopSprings()
	{
		if (this.m_PositionSpring != null)
		{
			this.m_PositionSpring.Stop(true);
		}
		if (this.m_PositionSpring2 != null)
		{
			this.m_PositionSpring2.Stop(true);
		}
		if (this.m_RotationSpring != null)
		{
			this.m_RotationSpring.Stop(true);
		}
		this.m_BobSpeed = 0f;
		this.m_LastBobSpeed = 0f;
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00072213 File Offset: 0x00070413
	public virtual void Stop()
	{
		this.SnapSprings();
		this.SnapZoom();
		this.Refresh();
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00072227 File Offset: 0x00070427
	public virtual void SetRotation(Vector2 eulerAngles, bool stop = true, bool resetInitialRotation = true)
	{
		this.Angle = eulerAngles;
		if (stop)
		{
			this.Stop();
		}
		if (resetInitialRotation)
		{
			this.m_InitialRotation = Vector2.zero;
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00072248 File Offset: 0x00070448
	protected virtual void OnMessage_FallImpact(float impact)
	{
		impact = Mathf.Abs(impact * 55f);
		float num = impact * this.PositionKneeling;
		float num2 = impact * this.RotationKneeling;
		num = Mathf.SmoothStep(0f, 1f, num);
		num2 = Mathf.SmoothStep(0f, 1f, num2);
		num2 = Mathf.SmoothStep(0f, 1f, num2);
		if (this.m_PositionSpring != null)
		{
			this.m_PositionSpring.AddSoftForce(Vector3.down * num, (float)this.PositionKneelingSoftness);
		}
		if (this.m_RotationSpring != null)
		{
			float num3 = (Random.value > 0.5f) ? (num2 * 2f) : (-(num2 * 2f));
			this.m_RotationSpring.AddSoftForce(Vector3.forward * num3, (float)this.RotationKneelingSoftness);
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void OnMessage_HeadImpact(float impact)
	{
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00072314 File Offset: 0x00070514
	protected virtual void OnMessage_GroundStomp(float impact)
	{
		this.AddForce2(new Vector3(0f, -1f, 0f) * impact);
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00072336 File Offset: 0x00070536
	protected virtual void OnMessage_BombShake(float impact)
	{
		this.DoBomb(new Vector3(1f, -10f, 1f) * impact, 1f, 2f);
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00072362 File Offset: 0x00070562
	protected virtual bool CanStart_Walk()
	{
		this.Player == null;
		return true;
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x0600070E RID: 1806 RVA: 0x00072372 File Offset: 0x00070572
	// (set) Token: 0x0600070F RID: 1807 RVA: 0x0007237A File Offset: 0x0007057A
	protected virtual Vector2 OnValue_Rotation
	{
		get
		{
			return this.Angle;
		}
		set
		{
			this.Angle = value;
		}
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00072383 File Offset: 0x00070583
	protected virtual void OnMessage_Stop()
	{
		this.Stop();
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000711 RID: 1809 RVA: 0x0007238B File Offset: 0x0007058B
	protected virtual Vector3 OnValue_Forward
	{
		get
		{
			return this.Forward;
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00072393 File Offset: 0x00070593
	public void SetMouseFreeze(bool val)
	{
		this.MouseFreeze = val;
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0007239C File Offset: 0x0007059C
	public void SetMouseSensitivity(float sens)
	{
		this.MouseSensitivityRestore = new Vector2(sens, sens);
	}

	// Token: 0x04000BF3 RID: 3059
	public vp_FPController FPController;

	// Token: 0x04000BF4 RID: 3060
	private bool MouseFreeze;

	// Token: 0x04000BF5 RID: 3061
	private Vector2 MouseSensitivityRestore = new Vector2(5f, 5f);

	// Token: 0x04000BF6 RID: 3062
	public int MouseSmoothSteps = 10;

	// Token: 0x04000BF7 RID: 3063
	public float MouseSmoothWeight = 0.5f;

	// Token: 0x04000BF8 RID: 3064
	public bool MouseAcceleration;

	// Token: 0x04000BF9 RID: 3065
	public float MouseAccelerationThreshold = 0.4f;

	// Token: 0x04000BFA RID: 3066
	protected Vector2 m_MouseMove = Vector2.zero;

	// Token: 0x04000BFB RID: 3067
	protected List<Vector2> m_MouseSmoothBuffer = new List<Vector2>();

	// Token: 0x04000BFC RID: 3068
	public float RenderingFieldOfView = 65f;

	// Token: 0x04000BFD RID: 3069
	public float RenderingZoomDamping = 0.2f;

	// Token: 0x04000BFE RID: 3070
	public float m_FinalZoomTime;

	// Token: 0x04000BFF RID: 3071
	public Vector3 PositionOffset = new Vector3(0f, 1.635f, 0f);

	// Token: 0x04000C00 RID: 3072
	public float PositionGroundLimit = 0.1f;

	// Token: 0x04000C01 RID: 3073
	public float PositionSpringStiffness = 0.01f;

	// Token: 0x04000C02 RID: 3074
	public float PositionSpringDamping = 0.25f;

	// Token: 0x04000C03 RID: 3075
	public float PositionSpring2Stiffness = 0.95f;

	// Token: 0x04000C04 RID: 3076
	public float PositionSpring2Damping = 0.25f;

	// Token: 0x04000C05 RID: 3077
	public float PositionKneeling = 0.025f;

	// Token: 0x04000C06 RID: 3078
	public int PositionKneelingSoftness = 1;

	// Token: 0x04000C07 RID: 3079
	public float PositionEarthQuakeFactor = 1f;

	// Token: 0x04000C08 RID: 3080
	protected vp_Spring m_PositionSpring;

	// Token: 0x04000C09 RID: 3081
	protected vp_Spring m_PositionSpring2;

	// Token: 0x04000C0A RID: 3082
	protected bool m_DrawCameraCollisionDebugLine;

	// Token: 0x04000C0B RID: 3083
	public Vector2 RotationPitchLimit = new Vector2(90f, -90f);

	// Token: 0x04000C0C RID: 3084
	public Vector2 RotationYawLimit = new Vector2(-360f, 360f);

	// Token: 0x04000C0D RID: 3085
	public float RotationSpringStiffness = 0.01f;

	// Token: 0x04000C0E RID: 3086
	public float RotationSpringDamping = 0.25f;

	// Token: 0x04000C0F RID: 3087
	public float RotationKneeling = 0.025f;

	// Token: 0x04000C10 RID: 3088
	public int RotationKneelingSoftness = 1;

	// Token: 0x04000C11 RID: 3089
	public float RotationStrafeRoll = 0.01f;

	// Token: 0x04000C12 RID: 3090
	public float RotationEarthQuakeFactor;

	// Token: 0x04000C13 RID: 3091
	protected float m_Pitch;

	// Token: 0x04000C14 RID: 3092
	protected float m_Yaw;

	// Token: 0x04000C15 RID: 3093
	protected vp_Spring m_RotationSpring;

	// Token: 0x04000C16 RID: 3094
	protected Vector2 m_InitialRotation = Vector2.zero;

	// Token: 0x04000C17 RID: 3095
	public float ShakeSpeed;

	// Token: 0x04000C18 RID: 3096
	public Vector3 ShakeAmplitude = new Vector3(10f, 10f, 0f);

	// Token: 0x04000C19 RID: 3097
	protected Vector3 m_Shake = Vector3.zero;

	// Token: 0x04000C1A RID: 3098
	public Vector4 BobRate = new Vector4(0f, 1.4f, 0f, 0.7f);

	// Token: 0x04000C1B RID: 3099
	public Vector4 BobAmplitude = new Vector4(0f, 0.25f, 0f, 0.5f);

	// Token: 0x04000C1C RID: 3100
	public float BobInputVelocityScale = 1f;

	// Token: 0x04000C1D RID: 3101
	public float BobMaxInputVelocity = 100f;

	// Token: 0x04000C1E RID: 3102
	public bool BobRequireGroundContact = true;

	// Token: 0x04000C1F RID: 3103
	protected float m_LastBobSpeed;

	// Token: 0x04000C20 RID: 3104
	protected Vector4 m_CurrentBobAmp = Vector4.zero;

	// Token: 0x04000C21 RID: 3105
	protected Vector4 m_CurrentBobVal = Vector4.zero;

	// Token: 0x04000C22 RID: 3106
	protected float m_BobSpeed;

	// Token: 0x04000C23 RID: 3107
	public vp_FPCamera.BobStepDelegate BobStepCallback;

	// Token: 0x04000C24 RID: 3108
	public float BobStepThreshold = 10f;

	// Token: 0x04000C25 RID: 3109
	protected float m_LastUpBob;

	// Token: 0x04000C26 RID: 3110
	protected bool m_BobWasElevating;

	// Token: 0x04000C27 RID: 3111
	protected Vector3 m_CameraCollisionStartPos = Vector3.zero;

	// Token: 0x04000C28 RID: 3112
	protected Vector3 m_CameraCollisionEndPos = Vector3.zero;

	// Token: 0x04000C29 RID: 3113
	protected RaycastHit m_CameraHit;

	// Token: 0x04000C2A RID: 3114
	private vp_FPPlayerEventHandler m_Player;

	// Token: 0x02000899 RID: 2201
	// (Invoke) Token: 0x06004C8B RID: 19595
	public delegate void BobStepDelegate();
}
