using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D1 RID: 209
[RequireComponent(typeof(AudioSource))]
public class vp_FPWeapon : vp_Component
{
	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000775 RID: 1909 RVA: 0x000758C7 File Offset: 0x00073AC7
	public bool Wielded
	{
		get
		{
			return this.m_Wielded && base.Rendering;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000776 RID: 1910 RVA: 0x000758D9 File Offset: 0x00073AD9
	public GameObject WeaponCamera
	{
		get
		{
			return this.m_WeaponCamera;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000777 RID: 1911 RVA: 0x000758E1 File Offset: 0x00073AE1
	public GameObject WeaponModel
	{
		get
		{
			return this.m_WeaponModel;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000778 RID: 1912 RVA: 0x000758E9 File Offset: 0x00073AE9
	public Vector3 DefaultPosition
	{
		get
		{
			return (Vector3)base.DefaultState.Preset.GetFieldValue("PositionOffset");
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000779 RID: 1913 RVA: 0x00075905 File Offset: 0x00073B05
	public Vector3 DefaultRotation
	{
		get
		{
			return (Vector3)base.DefaultState.Preset.GetFieldValue("RotationOffset");
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600077A RID: 1914 RVA: 0x00075921 File Offset: 0x00073B21
	// (set) Token: 0x0600077B RID: 1915 RVA: 0x00075929 File Offset: 0x00073B29
	public bool DrawRetractionDebugLine
	{
		get
		{
			return this.m_DrawRetractionDebugLine;
		}
		set
		{
			this.m_DrawRetractionDebugLine = value;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600077C RID: 1916 RVA: 0x00075932 File Offset: 0x00073B32
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

	// Token: 0x0600077D RID: 1917 RVA: 0x00075968 File Offset: 0x00073B68
	protected override void Awake()
	{
		base.Awake();
		if (base.transform.parent == null)
		{
			Debug.LogError("Error (" + this + ") Must not be placed in scene root. Disabling self.");
			vp_Utility.Activate(base.gameObject, false);
			return;
		}
		this.Controller = base.Transform.root.GetComponent<CharacterController>();
		if (this.Controller == null)
		{
			Debug.LogError("Error (" + this + ") Could not find CharacterController. Disabling self.");
			vp_Utility.Activate(base.gameObject, false);
			return;
		}
		base.Transform.eulerAngles = Vector3.zero;
		foreach (object obj in base.Transform.parent)
		{
			Camera camera = (Camera)((Transform)obj).GetComponent(typeof(Camera));
			if (camera != null)
			{
				this.m_WeaponCamera = camera.gameObject;
				break;
			}
		}
		if (base.GetComponent<Collider>() != null)
		{
			base.GetComponent<Collider>().enabled = false;
		}
		this.m_FPSCamera = base.transform.root.GetComponentInChildren<vp_FPCamera>();
		this.m_Input = base.transform.root.GetComponentInChildren<vp_FPInput>();
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00075AC4 File Offset: 0x00073CC4
	protected override void Start()
	{
		base.Start();
		this.InstantiateWeaponModel();
		this.m_WeaponGroup = new GameObject(base.name + "Transform");
		this.m_WeaponGroupTransform = this.m_WeaponGroup.transform;
		this.m_WeaponGroupTransform.parent = base.Transform.parent;
		this.m_WeaponGroupTransform.localPosition = this.PositionOffset;
		vp_Layer.Set(this.m_WeaponGroup, 31, false);
		base.Transform.parent = this.m_WeaponGroupTransform;
		base.Transform.localPosition = Vector3.zero;
		this.m_WeaponGroupTransform.localEulerAngles = this.RotationOffset;
		if (this.m_WeaponCamera != null && vp_Utility.IsActive(this.m_WeaponCamera.gameObject))
		{
			vp_Layer.Set(base.gameObject, 31, true);
		}
		this.m_Pivot = GameObject.CreatePrimitive(0);
		this.m_Pivot.name = "Pivot";
		this.m_Pivot.GetComponent<Collider>().enabled = false;
		this.m_Pivot.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		this.m_Pivot.transform.parent = this.m_WeaponGroupTransform;
		this.m_Pivot.transform.localPosition = Vector3.zero;
		this.m_Pivot.layer = 31;
		vp_Utility.Activate(this.m_Pivot.gameObject, false);
		Material material = new Material(Shader.Find("Transparent/Diffuse"));
		material.color = new Color(0f, 0f, 1f, 0.5f);
		this.m_Pivot.GetComponent<Renderer>().material = material;
		this.m_PositionSpring = new vp_Spring(this.m_WeaponGroup.gameObject.transform, vp_Spring.UpdateMode.Position, true);
		this.m_PositionSpring.RestState = this.PositionOffset;
		this.m_PositionPivotSpring = new vp_Spring(base.Transform, vp_Spring.UpdateMode.Position, true);
		this.m_PositionPivotSpring.RestState = this.PositionPivot;
		this.m_PositionSpring2 = new vp_Spring(base.Transform, vp_Spring.UpdateMode.PositionAdditive, true);
		this.m_PositionSpring2.MinVelocity = 1E-05f;
		this.m_RotationSpring = new vp_Spring(this.m_WeaponGroup.gameObject.transform, vp_Spring.UpdateMode.Rotation, true);
		this.m_RotationSpring.RestState = this.RotationOffset;
		this.m_RotationPivotSpring = new vp_Spring(base.Transform, vp_Spring.UpdateMode.Rotation, true);
		this.m_RotationPivotSpring.RestState = this.RotationPivot;
		this.m_RotationSpring2 = new vp_Spring(this.m_WeaponGroup.gameObject.transform, vp_Spring.UpdateMode.RotationAdditive, true);
		this.m_RotationSpring2.MinVelocity = 1E-05f;
		this.SnapSprings();
		this.Refresh();
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00075D80 File Offset: 0x00073F80
	public virtual void InstantiateWeaponModel()
	{
		if (this.WeaponPrefab != null)
		{
			if (this.m_WeaponModel != null && this.m_WeaponModel != base.gameObject)
			{
				Object.Destroy(this.m_WeaponModel);
			}
			this.m_WeaponModel = Object.Instantiate<GameObject>(this.WeaponPrefab);
			this.m_WeaponModel.transform.parent = base.transform;
			this.m_WeaponModel.transform.localPosition = Vector3.zero;
			this.m_WeaponModel.transform.localScale = new Vector3(1f, 1f, this.RenderingZScale);
			this.m_WeaponModel.transform.localEulerAngles = Vector3.zero;
			foreach (object obj in this.m_WeaponModel.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.name == "left_hand")
				{
					this.m_LeftHandModel = transform.gameObject;
				}
				else if (transform.gameObject.name == "right_hand")
				{
					this.m_RightHandModel = transform.gameObject;
				}
				if (transform.gameObject.name == "block")
				{
					foreach (object obj2 in transform.transform)
					{
						Transform transform2 = (Transform)obj2;
						if (transform2.gameObject.name == "top")
						{
							this.m_Top = transform2.gameObject;
						}
						else if (transform2.gameObject.name == "face")
						{
							this.m_Face = transform2.gameObject;
						}
					}
				}
			}
			if (this.m_WeaponCamera != null && vp_Utility.IsActive(this.m_WeaponCamera.gameObject))
			{
				vp_Layer.Set(this.m_WeaponModel, 31, true);
			}
		}
		else
		{
			this.m_WeaponModel = base.gameObject;
		}
		base.CacheRenderers();
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x00075FDC File Offset: 0x000741DC
	protected override void Init()
	{
		base.Init();
		this.ScheduleAmbientAnimation();
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00075FEA File Offset: 0x000741EA
	protected override void Update()
	{
		base.Update();
		this.UpdateMouseLook();
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00075FF8 File Offset: 0x000741F8
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		this.UpdateSwaying();
		this.UpdateBob();
		this.UpdateEarthQuake();
		this.UpdateStep();
		this.UpdateShakes();
		this.UpdateSprings();
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00076024 File Offset: 0x00074224
	public virtual void AddForce2(Vector3 positional, Vector3 angular)
	{
		this.m_PositionSpring2.AddForce(positional);
		this.m_RotationSpring2.AddForce(angular);
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0007603E File Offset: 0x0007423E
	public virtual void AddForce2(float xPos, float yPos, float zPos, float xRot, float yRot, float zRot)
	{
		this.AddForce2(new Vector3(xPos, yPos, zPos), new Vector3(xRot, yRot, zRot));
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00076059 File Offset: 0x00074259
	public virtual void AddForce(Vector3 force)
	{
		this.m_PositionSpring.AddForce(force);
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x00076067 File Offset: 0x00074267
	public virtual void AddForce(float x, float y, float z)
	{
		this.AddForce(new Vector3(x, y, z));
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00076077 File Offset: 0x00074277
	public virtual void AddForce(Vector3 positional, Vector3 angular)
	{
		this.m_PositionSpring.AddForce(positional);
		this.m_RotationSpring.AddForce(angular);
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00076091 File Offset: 0x00074291
	public virtual void AddForce(float xPos, float yPos, float zPos, float xRot, float yRot, float zRot)
	{
		this.AddForce(new Vector3(xPos, yPos, zPos), new Vector3(xRot, yRot, zRot));
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x000760AC File Offset: 0x000742AC
	public virtual void AddSoftForce(Vector3 force, int frames)
	{
		this.m_PositionSpring.AddSoftForce(force, (float)frames);
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x000760BC File Offset: 0x000742BC
	public virtual void AddSoftForce(float x, float y, float z, int frames)
	{
		this.AddSoftForce(new Vector3(x, y, z), frames);
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x000760CE File Offset: 0x000742CE
	public virtual void AddSoftForce(Vector3 positional, Vector3 angular, int frames)
	{
		this.m_PositionSpring.AddSoftForce(positional, (float)frames);
		this.m_RotationSpring.AddSoftForce(angular, (float)frames);
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x000760EC File Offset: 0x000742EC
	public virtual void AddSoftForce(float xPos, float yPos, float zPos, float xRot, float yRot, float zRot, int frames)
	{
		this.AddSoftForce(new Vector3(xPos, yPos, zPos), new Vector3(xRot, yRot, zRot), frames);
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0007610C File Offset: 0x0007430C
	protected virtual void UpdateMouseLook()
	{
		this.m_MouseMove.x = Input.GetAxisRaw("Mouse X") / base.Delta * Time.timeScale * Time.timeScale;
		this.m_MouseMove.y = Input.GetAxisRaw("Mouse Y") / base.Delta * Time.timeScale * Time.timeScale;
		this.m_MouseMove *= this.RotationInputVelocityScale;
		this.m_MouseMove = Vector3.Min(this.m_MouseMove, Vector3.one * this.RotationMaxInputVelocity);
		this.m_MouseMove = Vector3.Max(this.m_MouseMove, Vector3.one * -this.RotationMaxInputVelocity);
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x000761D8 File Offset: 0x000743D8
	protected virtual void UpdateShakes()
	{
		if (this.ShakeSpeed != 0f)
		{
			this.m_Shake = Vector3.Scale(vp_SmoothRandom.GetVector3Centered(this.ShakeSpeed), this.ShakeAmplitude);
			this.m_RotationSpring.AddForce(this.m_Shake * Time.timeScale);
		}
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0007622C File Offset: 0x0007442C
	protected virtual void UpdateRetraction(bool firstIteration = true)
	{
		if (this.RetractionDistance == 0f)
		{
			return;
		}
		Vector3 vector = this.WeaponModel.transform.TransformPoint(this.RetractionOffset);
		Vector3 vector2 = vector + this.WeaponModel.transform.forward * this.RetractionDistance;
		RaycastHit raycastHit;
		if (Physics.Linecast(vector, vector2, ref raycastHit, -1744831509) && !raycastHit.collider.isTrigger)
		{
			this.WeaponModel.transform.position = raycastHit.point - (raycastHit.point - vector).normalized * (this.RetractionDistance * 0.99f);
			this.WeaponModel.transform.localPosition = Vector3.forward * Mathf.Min(this.WeaponModel.transform.localPosition.z, 0f);
			return;
		}
		if (firstIteration && this.WeaponModel.transform.localPosition != Vector3.zero && this.WeaponModel != base.gameObject)
		{
			this.WeaponModel.transform.localPosition = Vector3.forward * Mathf.SmoothStep(this.WeaponModel.transform.localPosition.z, 0f, this.RetractionRelaxSpeed * Time.timeScale);
			this.UpdateRetraction(false);
		}
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x000763A0 File Offset: 0x000745A0
	protected virtual void UpdateBob()
	{
		if (this.BobAmplitude == Vector4.zero || this.BobRate == Vector4.zero)
		{
			return;
		}
		this.m_BobSpeed = ((this.BobRequireGroundContact && !this.Controller.isGrounded) ? 0f : this.Controller.velocity.sqrMagnitude);
		this.m_BobSpeed = Mathf.Min(this.m_BobSpeed * this.BobInputVelocityScale, this.BobMaxInputVelocity);
		this.m_BobSpeed = Mathf.Round(this.m_BobSpeed * 1000f) / 1000f;
		if (this.m_BobSpeed == 0f)
		{
			this.m_BobSpeed = Mathf.Min(this.m_LastBobSpeed * 0.93f, this.BobMaxInputVelocity);
		}
		this.m_CurrentBobAmp.x = this.m_BobSpeed * (this.BobAmplitude.x * -0.0001f);
		this.m_CurrentBobVal.x = Mathf.Cos(Time.time * (this.BobRate.x * 10f)) * this.m_CurrentBobAmp.x;
		this.m_CurrentBobAmp.y = this.m_BobSpeed * (this.BobAmplitude.y * 0.0001f);
		this.m_CurrentBobVal.y = Mathf.Cos(Time.time * (this.BobRate.y * 10f)) * this.m_CurrentBobAmp.y;
		this.m_CurrentBobAmp.z = this.m_BobSpeed * (this.BobAmplitude.z * 0.0001f);
		this.m_CurrentBobVal.z = Mathf.Cos(Time.time * (this.BobRate.z * 10f)) * this.m_CurrentBobAmp.z;
		this.m_CurrentBobAmp.w = this.m_BobSpeed * (this.BobAmplitude.w * 0.0001f);
		this.m_CurrentBobVal.w = Mathf.Cos(Time.time * (this.BobRate.w * 10f)) * this.m_CurrentBobAmp.w;
		this.m_RotationSpring.AddForce(this.m_CurrentBobVal * Time.timeScale);
		this.m_PositionSpring.AddForce(Vector3.forward * this.m_CurrentBobVal.w * Time.timeScale);
		this.m_LastBobSpeed = this.m_BobSpeed;
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0007661C File Offset: 0x0007481C
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
		if (!this.Controller.isGrounded)
		{
			return;
		}
		Vector3 vector = this.Player.EarthQuakeForce.Get();
		this.AddForce(new Vector3(0f, 0f, -vector.z * 0.015f), new Vector3(vector.y * 2f, -vector.x, vector.x * 2f));
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x000766B8 File Offset: 0x000748B8
	protected virtual void UpdateSprings()
	{
		this.m_PositionSpring.FixedUpdate();
		this.m_PositionPivotSpring.FixedUpdate();
		this.m_RotationPivotSpring.FixedUpdate();
		this.m_PositionSpring2.FixedUpdate();
		this.m_RotationSpring.FixedUpdate();
		this.m_RotationSpring2.FixedUpdate();
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00076708 File Offset: 0x00074908
	protected virtual void UpdateStep()
	{
		if (this.StepMinVelocity <= 0f || (this.BobRequireGroundContact && !this.Controller.isGrounded) || this.Controller.velocity.sqrMagnitude < this.StepMinVelocity)
		{
			return;
		}
		bool flag = this.m_LastUpBob < this.m_CurrentBobVal.x;
		this.m_LastUpBob = this.m_CurrentBobVal.x;
		if (flag && !this.m_BobWasElevating)
		{
			if (Mathf.Cos(Time.time * (this.BobRate.x * 5f)) > 0f)
			{
				this.m_PosStep = this.StepPositionForce - this.StepPositionForce * this.StepPositionBalance;
				this.m_RotStep = this.StepRotationForce - this.StepPositionForce * this.StepRotationBalance;
			}
			else
			{
				this.m_PosStep = this.StepPositionForce + this.StepPositionForce * this.StepPositionBalance;
				this.m_RotStep = Vector3.Scale(this.StepRotationForce - this.StepPositionForce * this.StepRotationBalance, -Vector3.one + Vector3.right * 2f);
			}
			this.AddSoftForce(this.m_PosStep * this.StepForceScale, this.m_RotStep * this.StepForceScale, this.StepSoftness);
		}
		this.m_BobWasElevating = flag;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00076894 File Offset: 0x00074A94
	protected virtual void UpdateSwaying()
	{
		this.m_SwayVel = this.Controller.velocity * this.PositionInputVelocityScale;
		this.m_SwayVel = Vector3.Min(this.m_SwayVel, Vector3.one * this.PositionMaxInputVelocity);
		this.m_SwayVel = Vector3.Max(this.m_SwayVel, Vector3.one * -this.PositionMaxInputVelocity);
		this.m_SwayVel *= Time.timeScale;
		Vector3 vector = base.Transform.InverseTransformDirection(this.m_SwayVel / 60f);
		if (this.m_RotationSpring == null)
		{
			return;
		}
		this.m_RotationSpring.AddForce(new Vector3(this.m_MouseMove.y * (this.RotationLookSway.x * 0.025f), this.m_MouseMove.x * (this.RotationLookSway.y * -0.025f), this.m_MouseMove.x * (this.RotationLookSway.z * -0.025f)));
		this.m_FallSway = this.RotationFallSway * (this.m_SwayVel.y * 0.005f);
		if (this.Controller.isGrounded)
		{
			this.m_FallSway *= this.RotationSlopeSway;
		}
		this.m_FallSway.z = Mathf.Max(0f, this.m_FallSway.z);
		this.m_RotationSpring.AddForce(this.m_FallSway);
		this.m_PositionSpring.AddForce(Vector3.forward * -Mathf.Abs(this.m_SwayVel.y * (this.PositionFallRetract * 2.5E-05f)));
		this.m_PositionSpring.AddForce(new Vector3(vector.x * (this.PositionWalkSlide.x * 0.0016f), -Mathf.Abs(vector.x * (this.PositionWalkSlide.y * 0.0016f)), -vector.z * (this.PositionWalkSlide.z * 0.0016f)));
		this.m_RotationSpring.AddForce(new Vector3(-Mathf.Abs(vector.x * (this.RotationStrafeSway.x * 0.16f)), -(vector.x * (this.RotationStrafeSway.y * 0.16f)), vector.x * (this.RotationStrafeSway.z * 0.16f)));
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00076B0C File Offset: 0x00074D0C
	public virtual void ResetSprings(float positionReset, float rotationReset, float positionPauseTime = 0f, float rotationPauseTime = 0f)
	{
		this.m_PositionSpring.State = Vector3.Lerp(this.m_PositionSpring.State, this.m_PositionSpring.RestState, positionReset);
		this.m_RotationSpring.State = Vector3.Lerp(this.m_RotationSpring.State, this.m_RotationSpring.RestState, rotationReset);
		this.m_PositionPivotSpring.State = Vector3.Lerp(this.m_PositionPivotSpring.State, this.m_PositionPivotSpring.RestState, positionReset);
		this.m_RotationPivotSpring.State = Vector3.Lerp(this.m_RotationPivotSpring.State, this.m_RotationPivotSpring.RestState, rotationReset);
		if (positionPauseTime != 0f)
		{
			this.m_PositionSpring.ForceVelocityFadeIn(positionPauseTime);
		}
		if (rotationPauseTime != 0f)
		{
			this.m_RotationSpring.ForceVelocityFadeIn(rotationPauseTime);
		}
		if (positionPauseTime != 0f)
		{
			this.m_PositionPivotSpring.ForceVelocityFadeIn(positionPauseTime);
		}
		if (rotationPauseTime != 0f)
		{
			this.m_RotationPivotSpring.ForceVelocityFadeIn(rotationPauseTime);
		}
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00076C0C File Offset: 0x00074E0C
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
			this.m_PositionSpring.RestState = this.PositionOffset - this.PositionPivot;
		}
		if (this.m_PositionPivotSpring != null)
		{
			this.m_PositionPivotSpring.Stiffness = new Vector3(this.PositionPivotSpringStiffness, this.PositionPivotSpringStiffness, this.PositionPivotSpringStiffness);
			this.m_PositionPivotSpring.Damping = Vector3.one - new Vector3(this.PositionPivotSpringDamping, this.PositionPivotSpringDamping, this.PositionPivotSpringDamping);
			this.m_PositionPivotSpring.RestState = this.PositionPivot;
		}
		if (this.m_RotationPivotSpring != null)
		{
			this.m_RotationPivotSpring.Stiffness = new Vector3(this.RotationPivotSpringStiffness, this.RotationPivotSpringStiffness, this.RotationPivotSpringStiffness);
			this.m_RotationPivotSpring.Damping = Vector3.one - new Vector3(this.RotationPivotSpringDamping, this.RotationPivotSpringDamping, this.RotationPivotSpringDamping);
			this.m_RotationPivotSpring.RestState = this.RotationPivot;
		}
		if (this.m_PositionSpring2 != null)
		{
			this.m_PositionSpring2.Stiffness = new Vector3(this.PositionSpring2Stiffness, this.PositionSpring2Stiffness, this.PositionSpring2Stiffness);
			this.m_PositionSpring2.Damping = Vector3.one - new Vector3(this.PositionSpring2Damping, this.PositionSpring2Damping, this.PositionSpring2Damping);
			this.m_PositionSpring2.RestState = Vector3.zero;
		}
		if (this.m_RotationSpring != null)
		{
			this.m_RotationSpring.Stiffness = new Vector3(this.RotationSpringStiffness, this.RotationSpringStiffness, this.RotationSpringStiffness);
			this.m_RotationSpring.Damping = Vector3.one - new Vector3(this.RotationSpringDamping, this.RotationSpringDamping, this.RotationSpringDamping);
			this.m_RotationSpring.RestState = this.RotationOffset;
		}
		if (this.m_RotationSpring2 != null)
		{
			this.m_RotationSpring2.Stiffness = new Vector3(this.RotationSpring2Stiffness, this.RotationSpring2Stiffness, this.RotationSpring2Stiffness);
			this.m_RotationSpring2.Damping = Vector3.one - new Vector3(this.RotationSpring2Damping, this.RotationSpring2Damping, this.RotationSpring2Damping);
			this.m_RotationSpring2.RestState = Vector3.zero;
		}
		if (base.Rendering && this.m_WeaponCamera != null && vp_Utility.IsActive(this.m_WeaponCamera.gameObject))
		{
			this.m_WeaponCamera.GetComponent<Camera>().nearClipPlane = this.RenderingClippingPlanes.x;
			this.m_WeaponCamera.GetComponent<Camera>().farClipPlane = this.RenderingClippingPlanes.y;
		}
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00076EF4 File Offset: 0x000750F4
	public override void Activate()
	{
		this.m_FPSCamera.RenderingFieldOfView = 65f;
		this.m_FPSCamera.GetComponent<Camera>().fieldOfView = 65f;
		this.m_Input.MouseBlockZoom = Time.time + 0.6f;
		this.m_Wielded = true;
		base.Rendering = true;
		this.m_DeactivationTimer.Cancel();
		if (this.m_WeaponGroup != null && !vp_Utility.IsActive(this.m_WeaponGroup))
		{
			vp_Utility.Activate(this.m_WeaponGroup, true);
		}
		this.SetPivotVisible(false);
		if (this.m_WeaponSystem == null)
		{
			this.m_WeaponSystem = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		this.m_WeaponSystem.OnWeaponSelect(this);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00076FB8 File Offset: 0x000751B8
	public override void Deactivate()
	{
		this.m_FPSCamera.RenderingFieldOfView = 65f;
		this.m_FPSCamera.GetComponent<Camera>().fieldOfView = 65f;
		this.m_Wielded = false;
		if (this.m_WeaponGroup != null && vp_Utility.IsActive(this.m_WeaponGroup))
		{
			vp_Utility.Activate(this.m_WeaponGroup, false);
		}
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00077018 File Offset: 0x00075218
	public virtual void SnapPivot()
	{
		if (this.m_PositionSpring != null)
		{
			this.m_PositionSpring.RestState = this.PositionOffset - this.PositionPivot;
			this.m_PositionSpring.State = this.PositionOffset - this.PositionPivot;
		}
		if (this.m_WeaponGroup != null)
		{
			this.m_WeaponGroupTransform.localPosition = this.PositionOffset - this.PositionPivot;
		}
		if (this.m_PositionPivotSpring != null)
		{
			this.m_PositionPivotSpring.RestState = this.PositionPivot;
			this.m_PositionPivotSpring.State = this.PositionPivot;
		}
		if (this.m_RotationPivotSpring != null)
		{
			this.m_RotationPivotSpring.RestState = this.RotationPivot;
			this.m_RotationPivotSpring.State = this.RotationPivot;
		}
		base.Transform.localPosition = this.PositionPivot;
		base.Transform.localEulerAngles = this.RotationPivot;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00077105 File Offset: 0x00075305
	public virtual void SetPivotVisible(bool visible)
	{
		if (this.m_Pivot == null)
		{
			return;
		}
		vp_Utility.Activate(this.m_Pivot.gameObject, visible);
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00077127 File Offset: 0x00075327
	public virtual void SnapToExit()
	{
		this.RotationOffset = this.RotationExitOffset;
		this.PositionOffset = this.PositionExitOffset;
		this.SnapSprings();
		this.SnapPivot();
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00077150 File Offset: 0x00075350
	public virtual void SnapSprings()
	{
		if (this.m_PositionSpring != null)
		{
			this.m_PositionSpring.RestState = this.PositionOffset - this.PositionPivot;
			this.m_PositionSpring.State = this.PositionOffset - this.PositionPivot;
			this.m_PositionSpring.Stop(true);
		}
		if (this.m_WeaponGroup != null)
		{
			this.m_WeaponGroupTransform.localPosition = this.PositionOffset - this.PositionPivot;
		}
		if (this.m_PositionPivotSpring != null)
		{
			this.m_PositionPivotSpring.RestState = this.PositionPivot;
			this.m_PositionPivotSpring.State = this.PositionPivot;
			this.m_PositionPivotSpring.Stop(true);
		}
		base.Transform.localPosition = this.PositionPivot;
		if (this.m_PositionSpring2 != null)
		{
			this.m_PositionSpring2.RestState = Vector3.zero;
			this.m_PositionSpring2.State = Vector3.zero;
			this.m_PositionSpring2.Stop(true);
		}
		if (this.m_RotationPivotSpring != null)
		{
			this.m_RotationPivotSpring.RestState = this.RotationPivot;
			this.m_RotationPivotSpring.State = this.RotationPivot;
			this.m_RotationPivotSpring.Stop(true);
		}
		base.Transform.localEulerAngles = this.RotationPivot;
		if (this.m_RotationSpring != null)
		{
			this.m_RotationSpring.RestState = this.RotationOffset;
			this.m_RotationSpring.State = this.RotationOffset;
			this.m_RotationSpring.Stop(true);
		}
		if (this.m_RotationSpring2 != null)
		{
			this.m_RotationSpring2.RestState = Vector3.zero;
			this.m_RotationSpring2.State = Vector3.zero;
			this.m_RotationSpring2.Stop(true);
		}
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00077300 File Offset: 0x00075500
	public virtual void StopSprings()
	{
		if (this.m_PositionSpring != null)
		{
			this.m_PositionSpring.Stop(true);
		}
		if (this.m_PositionPivotSpring != null)
		{
			this.m_PositionPivotSpring.Stop(true);
		}
		if (this.m_PositionSpring2 != null)
		{
			this.m_PositionSpring2.Stop(true);
		}
		if (this.m_RotationSpring != null)
		{
			this.m_RotationSpring.Stop(true);
		}
		if (this.m_RotationPivotSpring != null)
		{
			this.m_RotationPivotSpring.Stop(true);
		}
		if (this.m_RotationSpring2 != null)
		{
			this.m_RotationSpring2.Stop(true);
		}
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x00077388 File Offset: 0x00075588
	public virtual void Wield(bool showWeapon = true)
	{
		if (showWeapon)
		{
			this.SnapToExit();
		}
		this.PositionOffset = (showWeapon ? this.DefaultPosition : this.PositionExitOffset);
		this.RotationOffset = (showWeapon ? this.DefaultRotation : this.RotationExitOffset);
		this.m_Wielded = showWeapon;
		this.Refresh();
		base.StateManager.CombineStates();
		if (base.Audio != null)
		{
			if (this.sound == null)
			{
				this.sound = (Sound)Object.FindObjectOfType(typeof(Sound));
			}
			if (this.SoundWield == null)
			{
				this.SoundWield = this.sound.GetSelect();
			}
			if ((showWeapon ? this.SoundWield : this.SoundUnWield) != null && vp_Utility.IsActive(base.gameObject))
			{
				base.Audio.pitch = Time.timeScale;
				base.Audio.PlayOneShot(showWeapon ? this.SoundWield : this.SoundUnWield, AudioListener.volume);
			}
		}
		if ((showWeapon ? this.AnimationWield : this.AnimationUnWield) != null && vp_Utility.IsActive(base.gameObject))
		{
			this.m_WeaponModel.GetComponent<Animation>().CrossFade((showWeapon ? this.AnimationWield : this.AnimationUnWield).name);
		}
		if (this.WeaponID == 221 && showWeapon)
		{
			this.m_WeaponModel.GetComponent<CustomAnimation>().DrawIn();
		}
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00077500 File Offset: 0x00075700
	public virtual void ScheduleAmbientAnimation()
	{
		if (this.AnimationAmbient.Count == 0 || !vp_Utility.IsActive(base.gameObject))
		{
			return;
		}
		vp_Timer.In(Random.Range(this.AmbientInterval.x, this.AmbientInterval.y), delegate()
		{
			if (vp_Utility.IsActive(base.gameObject))
			{
				this.m_CurrentAmbientAnimation = Random.Range(0, this.AnimationAmbient.Count);
				if (this.AnimationAmbient[this.m_CurrentAmbientAnimation] != null)
				{
					this.m_WeaponModel.GetComponent<Animation>().CrossFadeQueued(this.AnimationAmbient[this.m_CurrentAmbientAnimation].name);
					this.ScheduleAmbientAnimation();
				}
			}
		}, this.m_AnimationAmbientTimer);
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0007755C File Offset: 0x0007575C
	protected virtual void OnMessage_FallImpact(float impact)
	{
		if (this.m_PositionSpring != null)
		{
			this.m_PositionSpring.AddSoftForce(Vector3.down * impact * this.PositionKneeling, (float)this.PositionKneelingSoftness);
		}
		if (this.m_RotationSpring != null)
		{
			this.m_RotationSpring.AddSoftForce(Vector3.right * impact * this.RotationKneeling, (float)this.RotationKneelingSoftness);
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void OnMessage_HeadImpact(float impact)
	{
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x000775C9 File Offset: 0x000757C9
	protected virtual void OnMessage_GroundStomp(float impact)
	{
		this.AddForce(Vector3.zero, new Vector3(-0.25f, 0f, 0f) * impact);
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x000775F0 File Offset: 0x000757F0
	protected virtual void OnMessage_BombShake(float impact)
	{
		this.AddForce(Vector3.zero, new Vector3(-0.3f, 0.1f, 0.5f) * impact);
	}

	// Token: 0x04000CBE RID: 3262
	protected WeaponSystem m_WeaponSystem;

	// Token: 0x04000CBF RID: 3263
	protected vp_FPCamera m_FPSCamera;

	// Token: 0x04000CC0 RID: 3264
	public vp_FPInput m_Input;

	// Token: 0x04000CC1 RID: 3265
	public GameObject WeaponPrefab;

	// Token: 0x04000CC2 RID: 3266
	protected GameObject m_WeaponModel;

	// Token: 0x04000CC3 RID: 3267
	public GameObject m_LeftHandModel;

	// Token: 0x04000CC4 RID: 3268
	public GameObject m_RightHandModel;

	// Token: 0x04000CC5 RID: 3269
	public GameObject m_Face;

	// Token: 0x04000CC6 RID: 3270
	public GameObject m_Top;

	// Token: 0x04000CC7 RID: 3271
	protected CharacterController Controller;

	// Token: 0x04000CC8 RID: 3272
	public float RenderingZoomDamping = 0.5f;

	// Token: 0x04000CC9 RID: 3273
	protected float m_FinalZoomTime;

	// Token: 0x04000CCA RID: 3274
	public float RenderingFieldOfView = 65f;

	// Token: 0x04000CCB RID: 3275
	public Vector2 RenderingClippingPlanes = new Vector2(0.01f, 10f);

	// Token: 0x04000CCC RID: 3276
	public float RenderingZScale = 1f;

	// Token: 0x04000CCD RID: 3277
	public Vector3 PositionOffset = new Vector3(0.15f, -0.15f, -0.15f);

	// Token: 0x04000CCE RID: 3278
	public float PositionSpringStiffness = 0.01f;

	// Token: 0x04000CCF RID: 3279
	public float PositionSpringDamping = 0.25f;

	// Token: 0x04000CD0 RID: 3280
	public float PositionFallRetract = 1f;

	// Token: 0x04000CD1 RID: 3281
	public float PositionPivotSpringStiffness = 0.01f;

	// Token: 0x04000CD2 RID: 3282
	public float PositionPivotSpringDamping = 0.25f;

	// Token: 0x04000CD3 RID: 3283
	public float PositionSpring2Stiffness = 0.95f;

	// Token: 0x04000CD4 RID: 3284
	public float PositionSpring2Damping = 0.25f;

	// Token: 0x04000CD5 RID: 3285
	public float PositionKneeling = 0.06f;

	// Token: 0x04000CD6 RID: 3286
	public int PositionKneelingSoftness = 1;

	// Token: 0x04000CD7 RID: 3287
	public Vector3 PositionWalkSlide = new Vector3(0.5f, 0.75f, 0.5f);

	// Token: 0x04000CD8 RID: 3288
	public Vector3 PositionPivot = Vector3.zero;

	// Token: 0x04000CD9 RID: 3289
	public Vector3 RotationPivot = Vector3.zero;

	// Token: 0x04000CDA RID: 3290
	public float PositionInputVelocityScale = 1f;

	// Token: 0x04000CDB RID: 3291
	public float PositionMaxInputVelocity = 25f;

	// Token: 0x04000CDC RID: 3292
	protected vp_Spring m_PositionSpring;

	// Token: 0x04000CDD RID: 3293
	protected vp_Spring m_PositionSpring2;

	// Token: 0x04000CDE RID: 3294
	protected vp_Spring m_PositionPivotSpring;

	// Token: 0x04000CDF RID: 3295
	protected vp_Spring m_RotationPivotSpring;

	// Token: 0x04000CE0 RID: 3296
	protected GameObject m_WeaponCamera;

	// Token: 0x04000CE1 RID: 3297
	protected GameObject m_WeaponGroup;

	// Token: 0x04000CE2 RID: 3298
	protected GameObject m_Pivot;

	// Token: 0x04000CE3 RID: 3299
	protected Transform m_WeaponGroupTransform;

	// Token: 0x04000CE4 RID: 3300
	public Vector3 RotationOffset = Vector3.zero;

	// Token: 0x04000CE5 RID: 3301
	public float RotationSpringStiffness = 0.01f;

	// Token: 0x04000CE6 RID: 3302
	public float RotationSpringDamping = 0.25f;

	// Token: 0x04000CE7 RID: 3303
	public float RotationPivotSpringStiffness = 0.01f;

	// Token: 0x04000CE8 RID: 3304
	public float RotationPivotSpringDamping = 0.25f;

	// Token: 0x04000CE9 RID: 3305
	public float RotationSpring2Stiffness = 0.95f;

	// Token: 0x04000CEA RID: 3306
	public float RotationSpring2Damping = 0.25f;

	// Token: 0x04000CEB RID: 3307
	public float RotationKneeling;

	// Token: 0x04000CEC RID: 3308
	public int RotationKneelingSoftness = 1;

	// Token: 0x04000CED RID: 3309
	public Vector3 RotationLookSway = new Vector3(1f, 0.7f, 0f);

	// Token: 0x04000CEE RID: 3310
	public Vector3 RotationStrafeSway = new Vector3(0.3f, 1f, 1.5f);

	// Token: 0x04000CEF RID: 3311
	public Vector3 RotationFallSway = new Vector3(1f, -0.5f, -3f);

	// Token: 0x04000CF0 RID: 3312
	public float RotationSlopeSway = 0.5f;

	// Token: 0x04000CF1 RID: 3313
	public float RotationInputVelocityScale = 1f;

	// Token: 0x04000CF2 RID: 3314
	public float RotationMaxInputVelocity = 15f;

	// Token: 0x04000CF3 RID: 3315
	protected vp_Spring m_RotationSpring;

	// Token: 0x04000CF4 RID: 3316
	protected vp_Spring m_RotationSpring2;

	// Token: 0x04000CF5 RID: 3317
	protected Vector3 m_SwayVel = Vector3.zero;

	// Token: 0x04000CF6 RID: 3318
	protected Vector3 m_FallSway = Vector3.zero;

	// Token: 0x04000CF7 RID: 3319
	public float RetractionDistance;

	// Token: 0x04000CF8 RID: 3320
	public Vector2 RetractionOffset = new Vector2(0f, 0f);

	// Token: 0x04000CF9 RID: 3321
	public float RetractionRelaxSpeed = 0.25f;

	// Token: 0x04000CFA RID: 3322
	protected bool m_DrawRetractionDebugLine;

	// Token: 0x04000CFB RID: 3323
	public float ShakeSpeed = 0.05f;

	// Token: 0x04000CFC RID: 3324
	public Vector3 ShakeAmplitude = new Vector3(0.25f, 0f, 2f);

	// Token: 0x04000CFD RID: 3325
	protected Vector3 m_Shake = Vector3.zero;

	// Token: 0x04000CFE RID: 3326
	public Vector4 BobRate = new Vector4(0.9f, 0.45f, 0f, 0f);

	// Token: 0x04000CFF RID: 3327
	public Vector4 BobAmplitude = new Vector4(0.35f, 0.5f, 0f, 0f);

	// Token: 0x04000D00 RID: 3328
	public float BobInputVelocityScale = 1f;

	// Token: 0x04000D01 RID: 3329
	public float BobMaxInputVelocity = 100f;

	// Token: 0x04000D02 RID: 3330
	public bool BobRequireGroundContact = true;

	// Token: 0x04000D03 RID: 3331
	protected float m_LastBobSpeed;

	// Token: 0x04000D04 RID: 3332
	protected Vector4 m_CurrentBobAmp = Vector4.zero;

	// Token: 0x04000D05 RID: 3333
	protected Vector4 m_CurrentBobVal = Vector4.zero;

	// Token: 0x04000D06 RID: 3334
	protected float m_BobSpeed;

	// Token: 0x04000D07 RID: 3335
	public Vector3 StepPositionForce = new Vector3(0f, -0.0012f, -0.0012f);

	// Token: 0x04000D08 RID: 3336
	public Vector3 StepRotationForce = new Vector3(0f, 0f, 0f);

	// Token: 0x04000D09 RID: 3337
	public int StepSoftness = 4;

	// Token: 0x04000D0A RID: 3338
	public float StepMinVelocity;

	// Token: 0x04000D0B RID: 3339
	public float StepPositionBalance;

	// Token: 0x04000D0C RID: 3340
	public float StepRotationBalance;

	// Token: 0x04000D0D RID: 3341
	public float StepForceScale = 1f;

	// Token: 0x04000D0E RID: 3342
	protected float m_LastUpBob;

	// Token: 0x04000D0F RID: 3343
	protected bool m_BobWasElevating;

	// Token: 0x04000D10 RID: 3344
	protected Vector3 m_PosStep = Vector3.zero;

	// Token: 0x04000D11 RID: 3345
	protected Vector3 m_RotStep = Vector3.zero;

	// Token: 0x04000D12 RID: 3346
	public AudioClip SoundWield;

	// Token: 0x04000D13 RID: 3347
	public AudioClip SoundUnWield;

	// Token: 0x04000D14 RID: 3348
	private Sound sound;

	// Token: 0x04000D15 RID: 3349
	public AnimationClip AnimationWield;

	// Token: 0x04000D16 RID: 3350
	public AnimationClip AnimationUnWield;

	// Token: 0x04000D17 RID: 3351
	public List<Object> AnimationAmbient = new List<Object>();

	// Token: 0x04000D18 RID: 3352
	protected List<bool> m_AmbAnimPlayed = new List<bool>();

	// Token: 0x04000D19 RID: 3353
	public Vector2 AmbientInterval = new Vector2(2.5f, 7.5f);

	// Token: 0x04000D1A RID: 3354
	protected int m_CurrentAmbientAnimation;

	// Token: 0x04000D1B RID: 3355
	protected vp_Timer.Handle m_AnimationAmbientTimer = new vp_Timer.Handle();

	// Token: 0x04000D1C RID: 3356
	public Vector3 PositionExitOffset = new Vector3(0f, -1f, 0f);

	// Token: 0x04000D1D RID: 3357
	public Vector3 RotationExitOffset = new Vector3(40f, 0f, 0f);

	// Token: 0x04000D1E RID: 3358
	protected bool m_Wielded = true;

	// Token: 0x04000D1F RID: 3359
	protected Vector2 m_MouseMove = Vector2.zero;

	// Token: 0x04000D20 RID: 3360
	public int WeaponID;

	// Token: 0x04000D21 RID: 3361
	private vp_FPPlayerEventHandler m_Player;
}
