using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CE RID: 206
[RequireComponent(typeof(vp_FPPlayerEventHandler))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(vp_FPInput))]
public class vp_FPController : vp_Component
{
	// Token: 0x06000715 RID: 1813 RVA: 0x000725AB File Offset: 0x000707AB
	public void ClearPos(float x, float y, float z)
	{
		this.m_PrevPosition.Clear();
		this.m_PrevPos = new Vector3(x, y, z);
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000716 RID: 1814 RVA: 0x000725C6 File Offset: 0x000707C6
	public bool Grounded
	{
		get
		{
			return this.m_Grounded;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000717 RID: 1815 RVA: 0x000725CE File Offset: 0x000707CE
	public bool HeadContact
	{
		get
		{
			return this.m_HeadContact;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000718 RID: 1816 RVA: 0x000725D6 File Offset: 0x000707D6
	public Vector3 GroundNormal
	{
		get
		{
			return this.m_GroundHit.normal;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000719 RID: 1817 RVA: 0x000725E3 File Offset: 0x000707E3
	public float GroundAngle
	{
		get
		{
			return Vector3.Angle(this.m_GroundHit.normal, Vector3.up);
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x0600071A RID: 1818 RVA: 0x000725FA File Offset: 0x000707FA
	public Transform GroundTransform
	{
		get
		{
			return this.m_GroundHit.transform;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x0600071B RID: 1819 RVA: 0x00072607 File Offset: 0x00070807
	public Vector3 SmoothPosition
	{
		get
		{
			return this.m_SmoothPosition;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x0600071C RID: 1820 RVA: 0x0007260F File Offset: 0x0007080F
	public Vector3 Velocity
	{
		get
		{
			return this.m_CharacterController.velocity;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x0600071D RID: 1821 RVA: 0x0007261C File Offset: 0x0007081C
	public CharacterController CharacterController
	{
		get
		{
			return this.m_CharacterController;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600071E RID: 1822 RVA: 0x00072624 File Offset: 0x00070824
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

	// Token: 0x0600071F RID: 1823 RVA: 0x0007265C File Offset: 0x0007085C
	protected override void Awake()
	{
		base.Awake();
		this.m_FPSCamera = base.transform.root.GetComponentInChildren<vp_FPCamera>();
		this.m_Map = (Map)Object.FindObjectOfType(typeof(Map));
		this.m_CharacterController = base.gameObject.GetComponent<CharacterController>();
		this.m_NormalHeight = this.CharacterController.height;
		this.CharacterController.center = (this.m_NormalCenter = new Vector3(0f, this.m_NormalHeight * 0.5f, 0f));
		this.CharacterController.radius = this.m_NormalHeight * 0.25f;
		this.m_CrouchHeight = this.m_NormalHeight * 0.5f;
		this.m_CrouchCenter = this.m_NormalCenter * 0.5f;
		this.m_CrouchHeight = 1.036f;
		this.m_CrouchCenter = new Vector3(0f, 0.518f, 0f);
		this.myTransform = base.transform;
		this.vecDataPos = Random.Range(0, 16);
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00072770 File Offset: 0x00070970
	protected override void Start()
	{
		base.Start();
		this.SetPosition(base.Transform.position);
		if (this.PhysicsHasCollisionTrigger)
		{
			this.m_Trigger = new GameObject("Trigger");
			this.m_Trigger.transform.parent = this.m_Transform;
			CapsuleCollider capsuleCollider = this.m_Trigger.AddComponent<CapsuleCollider>();
			capsuleCollider.isTrigger = true;
			capsuleCollider.radius = this.CharacterController.radius + this.m_SkinWidth;
			capsuleCollider.height = this.CharacterController.height + this.m_SkinWidth * 2f;
			capsuleCollider.center = this.CharacterController.center;
			this.m_Trigger.layer = 30;
			this.m_Trigger.transform.localPosition = Vector3.zero;
		}
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0007283F File Offset: 0x00070A3F
	protected override void Update()
	{
		base.Update();
		this.SmoothMove();
		this.UpdateMove();
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00072854 File Offset: 0x00070A54
	private void UpdateMove()
	{
		bool flag = false;
		Vector3 position = base.gameObject.transform.position;
		if (base.gameObject.transform.position.x > this.m_Map.mlx.y)
		{
			position.x = this.m_Map.mlx.y;
			flag = true;
		}
		if (base.gameObject.transform.position.y > this.m_Map.mly.y)
		{
			position.y = this.m_Map.mly.y;
			flag = true;
		}
		if (base.gameObject.transform.position.z > this.m_Map.mlz.y)
		{
			position.z = this.m_Map.mlz.y;
			flag = true;
		}
		if (base.gameObject.transform.position.x < this.m_Map.mlx.x)
		{
			position.x = this.m_Map.mlx.x;
			flag = true;
		}
		if (base.gameObject.transform.position.y < this.m_Map.mly.x)
		{
			position.y = this.m_Map.mly.x;
			flag = true;
		}
		if (base.gameObject.transform.position.z < this.m_Map.mlz.x)
		{
			position.z = this.m_Map.mlz.x;
			flag = true;
		}
		if (flag)
		{
			base.gameObject.transform.position = position;
			this.m_SmoothPosition = position;
		}
		this.b = this.m_Map.GetBlock((int)this.myTransform.position.x, (int)this.myTransform.position.y, (int)this.myTransform.position.z).block;
		this.bUp = this.m_Map.GetBlock((int)this.myTransform.position.x, (int)this.myTransform.position.y + 1, (int)this.myTransform.position.z).block;
		if (this.bUp != null && this.bUp.GetName() == "!Water")
		{
			this.b = this.bUp;
		}
		int num = 1;
		RaycastHit raycastHit;
		if (!Physics.Raycast(new Ray(base.gameObject.transform.position, -Vector3.up), ref raycastHit, 64f, num) && Physics.Raycast(new Ray(base.gameObject.transform.position + 1.65f * Vector3.up, -Vector3.up), ref raycastHit, 64f, num))
		{
			base.gameObject.transform.position = raycastHit.point;
		}
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00072B58 File Offset: 0x00070D58
	protected override void FixedUpdate()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		this.UpdateMotor();
		this.UpdateJump();
		this.UpdateForces();
		this.UpdateSliding();
		this.FixedMove();
		this.UpdateCollisions();
		this.UpdatePlatformMove();
		this.m_PrevPos = base.Transform.position;
		if (this.Velocity.magnitude > 4f && this.m_Grounded)
		{
			if (this.b != null)
			{
				if (ZipLoader.GetBlockType(this.b) != this.currBlockType)
				{
					base.GetComponent<Sound>().PlaySound_Stop(null);
					this.currBlockType = ZipLoader.GetBlockType(this.b);
				}
				base.GetComponent<Sound>().PlaySound_Walk(this.currBlockType);
			}
			else
			{
				if (1 != this.currBlockType)
				{
					base.GetComponent<Sound>().PlaySound_Stop(null);
					this.currBlockType = 1;
				}
				base.GetComponent<Sound>().PlaySound_Walk(this.currBlockType);
			}
		}
		else
		{
			base.GetComponent<Sound>().PlaySound_Stop(null);
		}
		this.CheckValue();
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00072C59 File Offset: 0x00070E59
	protected virtual void UpdateMotor()
	{
		if (!this.MotorFreeFly)
		{
			this.UpdateThrottleWalk();
		}
		else
		{
			this.UpdateThrottleFree();
		}
		this.m_MotorThrottle = vp_Utility.SnapToZero(this.m_MotorThrottle, 0.0001f);
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00072C88 File Offset: 0x00070E88
	protected virtual void UpdateThrottleWalk()
	{
		this.UpdateSlopeFactor();
		this.m_MotorAirSpeedModifier = (this.m_Grounded ? 1f : this.MotorAirSpeed);
		this.m_MotorThrottle += this.m_MoveVector.x * base.Transform.TransformDirection(Vector3.right * this.m_MotorAirSpeedModifier * (this.MotorAcceleration * 0.1f)) * this.m_SlopeFactor;
		this.vecData[this.vecDataPos] = this.m_MoveVector.y * base.Transform.TransformDirection(Vector3.forward * this.m_MotorAirSpeedModifier * (this.MotorAcceleration * 0.1f)) * this.m_SlopeFactor;
		this.m_MotorThrottle += this.vecData[this.vecDataPos];
		this.m_MotorThrottle.x = this.m_MotorThrottle.x / (1f + this.MotorDamping * this.m_MotorAirSpeedModifier * Time.timeScale);
		this.m_MotorThrottle.z = this.m_MotorThrottle.z / (1f + this.MotorDamping * this.m_MotorAirSpeedModifier * Time.timeScale);
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00072DD8 File Offset: 0x00070FD8
	protected virtual void UpdateThrottleFree()
	{
		this.m_MotorThrottle += this.m_MoveVector.y * base.Transform.TransformDirection(base.Transform.InverseTransformDirection(this.Player.Forward.Get()) * (this.MotorAcceleration * 0.1f));
		this.m_MotorThrottle += this.m_MoveVector.x * base.Transform.TransformDirection(Vector3.right * (this.MotorAcceleration * 0.1f));
		this.m_MotorThrottle.x = this.m_MotorThrottle.x / (1f + this.MotorDamping * Time.timeScale);
		this.m_MotorThrottle.z = this.m_MotorThrottle.z / (1f + this.MotorDamping * Time.timeScale);
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00072EC8 File Offset: 0x000710C8
	protected virtual void UpdateJump()
	{
		if (!this.MotorFreeFly)
		{
			this.UpdateJumpForceWalk();
		}
		else
		{
			this.UpdateJumpForceFree();
		}
		this.m_MotorThrottle.y = this.m_MotorThrottle.y + this.m_MotorJumpForceAcc * Time.timeScale;
		this.m_MotorJumpForceAcc /= 1f + this.MotorJumpForceHoldDamping * Time.timeScale;
		this.m_MotorThrottle.y = this.m_MotorThrottle.y / (1f + this.MotorJumpForceDamping * Time.timeScale);
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00072F48 File Offset: 0x00071148
	protected virtual void UpdateJumpForceWalk()
	{
		if (this.Player.Jump.Active && !this.m_Grounded)
		{
			if (this.m_MotorJumpForceHoldSkipFrames > 2)
			{
				if (this.m_CharacterController.velocity.y >= 0f)
				{
					this.m_MotorJumpForceAcc += this.MotorJumpForceHold;
					return;
				}
			}
			else
			{
				this.m_MotorJumpForceHoldSkipFrames++;
			}
		}
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00072FB4 File Offset: 0x000711B4
	protected virtual void UpdateJumpForceFree()
	{
		if (this.Player.Jump.Active && this.Player.Crouch.Active)
		{
			return;
		}
		if (this.Player.Jump.Active)
		{
			this.m_MotorJumpForceAcc += this.MotorJumpForceHold;
			return;
		}
		if (this.Player.Crouch.Active)
		{
			this.m_MotorJumpForceAcc -= this.MotorJumpForceHold;
			if (this.Grounded && this.CharacterController.height == this.m_NormalHeight)
			{
				this.CharacterController.height = this.m_CrouchHeight;
				this.CharacterController.center = this.m_CrouchCenter;
			}
		}
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00073070 File Offset: 0x00071270
	protected virtual void UpdateForces()
	{
		if (this.m_Grounded && this.m_FallSpeed <= 0f)
		{
			this.m_FallSpeed = Physics.gravity.y * (this.PhysicsGravityModifier * 0.002f);
		}
		else
		{
			this.m_FallSpeed += Physics.gravity.y * (this.PhysicsGravityModifier * 0.002f);
		}
		if (this.m_FallSpeed < this.m_LastFallSpeed)
		{
			this.m_HighestFallSpeed = this.m_FallSpeed;
		}
		this.m_LastFallSpeed = this.m_FallSpeed;
		if (this.m_SmoothForceFrame[0] != Vector3.zero)
		{
			this.AddForceInternal(this.m_SmoothForceFrame[0]);
			for (int i = 0; i < 120; i++)
			{
				this.m_SmoothForceFrame[i] = ((i < 119) ? this.m_SmoothForceFrame[i + 1] : Vector3.zero);
				if (this.m_SmoothForceFrame[i] == Vector3.zero)
				{
					break;
				}
			}
		}
		this.m_ExternalForce /= 1f + this.PhysicsForceDamping * Time.timeScale;
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00073194 File Offset: 0x00071394
	protected virtual void UpdateSliding()
	{
		bool slideFast = this.m_SlideFast;
		bool slide = this.m_Slide;
		this.m_Slide = false;
		if (!this.m_Grounded)
		{
			this.m_OnSteepGroundSince = 0f;
			this.m_SlideFast = false;
		}
		else if (this.GroundAngle > this.PhysicsSlopeSlideLimit)
		{
			this.m_Slide = true;
			if (this.GroundAngle <= this.m_CharacterController.slopeLimit)
			{
				this.m_SlopeSlideSpeed = Mathf.Max(this.m_SlopeSlideSpeed, this.PhysicsSlopeSlidiness * 0.01f);
				this.m_OnSteepGroundSince = 0f;
				this.m_SlideFast = false;
				this.m_SlopeSlideSpeed = ((Mathf.Abs(this.m_SlopeSlideSpeed) < 0.0001f) ? 0f : (this.m_SlopeSlideSpeed / (1f + 0.05f * Time.timeScale)));
			}
			else
			{
				if (this.m_SlopeSlideSpeed > 0.01f)
				{
					this.m_SlideFast = true;
				}
				if (this.m_OnSteepGroundSince == 0f)
				{
					this.m_OnSteepGroundSince = Time.time;
				}
				this.m_SlopeSlideSpeed += this.PhysicsSlopeSlidiness * 0.01f * ((Time.time - this.m_OnSteepGroundSince) * 0.125f) * Time.timeScale;
				this.m_SlopeSlideSpeed = Mathf.Max(this.PhysicsSlopeSlidiness * 0.01f, this.m_SlopeSlideSpeed);
			}
			this.AddForce(Vector3.Cross(Vector3.Cross(this.GroundNormal, Vector3.down), this.GroundNormal) * this.m_SlopeSlideSpeed * Time.timeScale);
		}
		else
		{
			this.m_OnSteepGroundSince = 0f;
			this.m_SlideFast = false;
			this.m_SlopeSlideSpeed = 0f;
		}
		if (this.m_MotorThrottle != Vector3.zero)
		{
			this.m_Slide = false;
		}
		if (this.m_SlideFast)
		{
			this.m_SlideFallSpeed = base.Transform.position.y;
		}
		else if (slideFast && !this.Grounded)
		{
			this.m_FallSpeed = base.Transform.position.y - this.m_SlideFallSpeed;
		}
		if (slide != this.m_Slide)
		{
			this.Player.SetState("Slide", this.m_Slide, true, false);
		}
		if (slideFast != this.m_SlideFast)
		{
			this.Player.SetState("SlideFast", this.m_SlideFast, true, false);
		}
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x000733DC File Offset: 0x000715DC
	protected virtual void FixedMove()
	{
		this.m_MoveDirection = Vector3.zero;
		this.m_MoveDirection += this.m_ExternalForce;
		this.m_MoveDirection += this.m_MotorThrottle;
		this.m_MoveDirection.y = this.m_MoveDirection.y + this.m_FallSpeed;
		this.m_CurrentAntiBumpOffset = 0f;
		if (this.m_Grounded && this.m_MotorThrottle.y <= 0.001f)
		{
			this.m_CurrentAntiBumpOffset = Mathf.Max(this.m_CharacterController.stepOffset, Vector3.Scale(this.m_MoveDirection, Vector3.one - Vector3.up).magnitude);
			this.m_MoveDirection += this.m_CurrentAntiBumpOffset * Vector3.down;
		}
		this.m_PredictedPos = base.Transform.position + vp_Utility.NaNSafeVector3(this.m_MoveDirection * base.Delta * Time.timeScale, default(Vector3));
		if (this.m_Platform != null && this.m_PositionOnPlatform != Vector3.zero)
		{
			this.m_CharacterController.Move(vp_Utility.NaNSafeVector3(this.m_Platform.TransformPoint(this.m_PositionOnPlatform) - this.m_Transform.position, default(Vector3)));
		}
		this.m_CharacterController.Move(vp_Utility.NaNSafeVector3(this.m_MoveDirection * base.Delta * Time.timeScale, default(Vector3)));
		if (this.Player.Dead.Active)
		{
			this.m_MoveVector = Vector2.zero;
			return;
		}
		Physics.SphereCast(new Ray(base.Transform.position + Vector3.up * this.m_CharacterController.radius, Vector3.down), this.m_CharacterController.radius, ref this.m_GroundHit, this.m_SkinWidth + 0.001f, -1744831509);
		this.m_Grounded = (this.m_GroundHit.collider != null);
		this.check_m_Grounded = (this.m_GroundHit.collider != null);
		if (this.m_GroundHit.transform == null && this.m_LastGroundHit.transform != null)
		{
			if (this.m_Platform != null && this.m_PositionOnPlatform != Vector3.zero)
			{
				this.AddForce(this.m_Platform.position - this.m_LastPlatformPos);
				this.m_Platform = null;
			}
			if (this.m_CurrentAntiBumpOffset != 0f)
			{
				this.m_CharacterController.Move(vp_Utility.NaNSafeVector3(this.m_CurrentAntiBumpOffset * Vector3.up, default(Vector3)) * base.Delta * Time.timeScale);
				this.m_PredictedPos += vp_Utility.NaNSafeVector3(this.m_CurrentAntiBumpOffset * Vector3.up, default(Vector3)) * base.Delta * Time.timeScale;
				this.m_MoveDirection += this.m_CurrentAntiBumpOffset * Vector3.up;
			}
		}
		if (this.m_Grounded != this.m_NETGrounded)
		{
			this.m_NETGrounded = this.m_Grounded;
			if (this.m_NETGrounded)
			{
				this.client.send_position(4);
				return;
			}
			this.client.send_position(3);
		}
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00073788 File Offset: 0x00071988
	protected virtual void SmoothMove()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		this.m_FixedPosition = base.Transform.position;
		base.Transform.position = this.m_SmoothPosition;
		this.m_CharacterController.Move(vp_Utility.NaNSafeVector3(this.m_MoveDirection * base.Delta * Time.timeScale, default(Vector3)));
		this.m_SmoothPosition = base.Transform.position;
		base.Transform.position = this.m_FixedPosition;
		if (Vector3.Distance(base.Transform.position, this.m_SmoothPosition) > this.m_CharacterController.radius)
		{
			this.m_SmoothPosition = base.Transform.position;
		}
		if (this.m_Platform != null && (this.m_LastPlatformPos.y < this.m_Platform.position.y || this.m_LastPlatformPos.y > this.m_Platform.position.y))
		{
			this.m_SmoothPosition.y = base.Transform.position.y;
		}
		this.m_SmoothPosition = Vector3.Lerp(this.m_SmoothPosition, base.Transform.position, Time.deltaTime);
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x000738D4 File Offset: 0x00071AD4
	protected virtual void UpdateCollisions()
	{
		if (this.m_GroundHit.transform != null && this.m_GroundHit.transform != this.m_LastGroundHit.transform)
		{
			this.m_SmoothPosition.y = base.Transform.position.y;
			if (!this.MotorFreeFly)
			{
				this.m_FallImpact = -this.m_HighestFallSpeed * Time.timeScale;
			}
			else
			{
				this.m_FallImpact = -(this.CharacterController.velocity.y * 0.01f) * Time.timeScale;
			}
			this.DeflectDownForce();
			this.m_HighestFallSpeed = 0f;
			this.Player.FallImpact.Send(this.m_FallImpact);
			this.m_MotorThrottle.y = 0f;
			this.m_MotorJumpForceAcc = 0f;
			this.m_MotorJumpForceHoldSkipFrames = 0;
			if (this.m_GroundHit.collider.gameObject.layer == 28)
			{
				this.m_Platform = this.m_GroundHit.transform;
				this.m_LastPlatformAngle = this.m_Platform.eulerAngles.y;
			}
			else
			{
				this.m_Platform = null;
			}
		}
		else
		{
			this.m_FallImpact = 0f;
		}
		this.m_LastGroundHit = this.m_GroundHit;
		if (this.m_PredictedPos.y > base.Transform.position.y && (this.m_ExternalForce.y > 0f || this.m_MotorThrottle.y > 0f))
		{
			this.DeflectUpForce();
		}
		if (this.m_PredictedPos.x != base.Transform.position.x || (this.m_PredictedPos.z != base.Transform.position.z && this.m_ExternalForce != Vector3.zero))
		{
			this.DeflectHorizontalForce();
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00073ABC File Offset: 0x00071CBC
	private void UpdatePlatformMove()
	{
		if (this.m_Platform == null)
		{
			return;
		}
		this.m_PositionOnPlatform = this.m_Platform.InverseTransformPoint(this.m_Transform.position);
		this.m_Player.Rotation.Set(new Vector2(this.m_Player.Rotation.Get().x, this.m_Player.Rotation.Get().y - Mathf.DeltaAngle(this.m_Platform.eulerAngles.y, this.m_LastPlatformAngle)));
		this.m_LastPlatformAngle = this.m_Platform.eulerAngles.y;
		this.m_LastPlatformPos = this.m_Platform.position;
		this.m_SmoothPosition = base.Transform.position;
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00073B98 File Offset: 0x00071D98
	protected virtual void UpdateSlopeFactor()
	{
		if (!this.m_Grounded)
		{
			this.m_SlopeFactor = 1f;
			return;
		}
		this.m_SlopeFactor = 1f + (1f - Vector3.Angle(this.m_GroundHit.normal, this.m_MotorThrottle) / 90f);
		if (Mathf.Abs(1f - this.m_SlopeFactor) < 0.01f)
		{
			this.m_SlopeFactor = 1f;
			return;
		}
		if (this.m_SlopeFactor <= 1f)
		{
			if (this.MotorSlopeSpeedUp == 1f)
			{
				this.m_SlopeFactor *= 1.2f;
			}
			else
			{
				this.m_SlopeFactor *= this.MotorSlopeSpeedUp;
			}
			this.m_SlopeFactor = ((this.GroundAngle > this.m_CharacterController.slopeLimit) ? 0f : this.m_SlopeFactor);
			return;
		}
		if (this.MotorSlopeSpeedDown == 1f)
		{
			this.m_SlopeFactor = 1f / this.m_SlopeFactor;
			this.m_SlopeFactor *= 1.2f;
			return;
		}
		this.m_SlopeFactor *= this.MotorSlopeSpeedDown;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00073CB8 File Offset: 0x00071EB8
	public virtual void SetPosition(Vector3 position)
	{
		base.Transform.position = position;
		this.m_SmoothPosition = position;
		this.m_PrevPos = position;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00073CD4 File Offset: 0x00071ED4
	protected virtual void AddForceInternal(Vector3 force)
	{
		this.m_ExternalForce += force;
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x00073CE8 File Offset: 0x00071EE8
	public virtual void AddForce(float x, float y, float z)
	{
		this.AddForce(new Vector3(x, y, z));
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00073CF8 File Offset: 0x00071EF8
	public virtual void AddForce(Vector3 force)
	{
		if (Time.timeScale >= 1f)
		{
			this.AddForceInternal(force);
			return;
		}
		this.AddSoftForce(force, 1f);
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00073D1C File Offset: 0x00071F1C
	public virtual void AddSoftForce(Vector3 force, float frames)
	{
		force /= Time.timeScale;
		frames = Mathf.Clamp(frames, 1f, 120f);
		this.AddForceInternal(force / frames);
		for (int i = 0; i < Mathf.RoundToInt(frames) - 1; i++)
		{
			this.m_SmoothForceFrame[i] += force / frames;
		}
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00073D8C File Offset: 0x00071F8C
	public virtual void StopSoftForce()
	{
		int num = 0;
		while (num < 120 && !(this.m_SmoothForceFrame[num] == Vector3.zero))
		{
			this.m_SmoothForceFrame[num] = Vector3.zero;
			num++;
		}
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00073DD0 File Offset: 0x00071FD0
	public virtual void Stop()
	{
		this.m_CharacterController.Move(Vector3.zero);
		this.m_MotorThrottle = Vector3.zero;
		this.m_ExternalForce = Vector3.zero;
		this.StopSoftForce();
		this.m_MoveVector = Vector2.zero;
		this.m_FallSpeed = 0f;
		this.m_SmoothPosition = base.Transform.position;
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x00073E34 File Offset: 0x00072034
	public virtual void DeflectDownForce()
	{
		if (this.GroundAngle > this.PhysicsSlopeSlideLimit)
		{
			this.m_SlopeSlideSpeed = this.m_FallImpact * (0.25f * Time.timeScale);
		}
		if (this.GroundAngle > 85f)
		{
			this.m_MotorThrottle += vp_Utility.HorizontalVector(this.GroundNormal * this.m_FallImpact);
			this.m_Grounded = false;
			this.check_m_Grounded = false;
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00073EAC File Offset: 0x000720AC
	protected virtual void DeflectUpForce()
	{
		if (!this.m_HeadContact)
		{
			return;
		}
		this.m_NewDir = Vector3.Cross(Vector3.Cross(this.m_CeilingHit.normal, Vector3.up), this.m_CeilingHit.normal);
		this.m_ForceImpact = this.m_MotorThrottle.y + this.m_ExternalForce.y;
		Vector3 vector = this.m_NewDir * (this.m_MotorThrottle.y + this.m_ExternalForce.y) * (1f - this.PhysicsWallFriction);
		this.m_ForceImpact -= vector.magnitude;
		this.AddForce(vector * Time.timeScale);
		this.m_MotorThrottle.y = 0f;
		this.m_ExternalForce.y = 0f;
		this.m_FallSpeed = 0f;
		this.m_NewDir.x = base.Transform.InverseTransformDirection(this.m_NewDir).x;
		this.Player.HeadImpact.Send((this.m_NewDir.x < 0f || (this.m_NewDir.x == 0f && Random.value < 0.5f)) ? (-this.m_ForceImpact) : this.m_ForceImpact);
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00074004 File Offset: 0x00072204
	protected virtual void DeflectHorizontalForce()
	{
		this.m_PredictedPos.y = base.Transform.position.y;
		this.m_PrevPos.y = base.Transform.position.y;
		this.m_PrevDir = (this.m_PredictedPos - this.m_PrevPos).normalized;
		this.CapsuleBottom = this.m_PrevPos + Vector3.up * this.m_CharacterController.radius;
		this.CapsuleTop = this.CapsuleBottom + Vector3.up * (this.m_CharacterController.height - this.m_CharacterController.radius * 2f);
		if (!Physics.CapsuleCast(this.CapsuleBottom, this.CapsuleTop, this.m_CharacterController.radius, this.m_PrevDir, ref this.m_WallHit, Vector3.Distance(this.m_PrevPos, this.m_PredictedPos), -1744831509))
		{
			return;
		}
		this.m_NewDir = Vector3.Cross(this.m_WallHit.normal, Vector3.up).normalized;
		if (Vector3.Dot(Vector3.Cross(this.m_WallHit.point - base.Transform.position, this.m_PrevPos - base.Transform.position), Vector3.up) > 0f)
		{
			this.m_NewDir = -this.m_NewDir;
		}
		this.m_ForceMultiplier = Mathf.Abs(Vector3.Dot(this.m_PrevDir, this.m_NewDir)) * (1f - this.PhysicsWallFriction);
		if (this.PhysicsWallBounce > 0f)
		{
			this.m_NewDir = Vector3.Lerp(this.m_NewDir, Vector3.Reflect(this.m_PrevDir, this.m_WallHit.normal), this.PhysicsWallBounce);
			this.m_ForceMultiplier = Mathf.Lerp(this.m_ForceMultiplier, 1f, this.PhysicsWallBounce * (1f - this.PhysicsWallFriction));
		}
		this.m_ForceImpact = 0f;
		float y = this.m_ExternalForce.y;
		this.m_ExternalForce.y = 0f;
		this.m_ForceImpact = this.m_ExternalForce.magnitude;
		this.m_ExternalForce = this.m_NewDir * this.m_ExternalForce.magnitude * this.m_ForceMultiplier;
		this.m_ForceImpact -= this.m_ExternalForce.magnitude;
		int num = 0;
		while (num < 120 && !(this.m_SmoothForceFrame[num] == Vector3.zero))
		{
			this.m_SmoothForceFrame[num] = this.m_SmoothForceFrame[num].magnitude * this.m_NewDir * this.m_ForceMultiplier;
			num++;
		}
		this.m_ExternalForce.y = y;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x000742E8 File Offset: 0x000724E8
	protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
		if (attachedRigidbody == null || attachedRigidbody.isKinematic)
		{
			return;
		}
		if (hit.moveDirection.y < -0.3f)
		{
			return;
		}
		Vector3 vector;
		vector..ctor(hit.moveDirection.x, 0f, hit.moveDirection.z);
		attachedRigidbody.velocity = vector * (this.PhysicsPushForce / attachedRigidbody.mass);
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00074361 File Offset: 0x00072561
	protected virtual bool CanStart_Jump()
	{
		return this.MotorFreeFly || (this.m_Grounded && this.m_MotorJumpDone && this.GroundAngle <= this.m_CharacterController.slopeLimit);
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00074397 File Offset: 0x00072597
	protected virtual bool CanStart_Walk()
	{
		return !this.Player.Crouch.Active;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x000743B0 File Offset: 0x000725B0
	protected virtual void OnStart_Jump()
	{
		this.m_MotorJumpDone = false;
		this.m_SmoothPosition.y = base.Transform.position.y;
		if (this.MotorFreeFly && !this.Grounded)
		{
			return;
		}
		this.m_MotorThrottle.y = this.MotorJumpForce / Time.timeScale;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00074407 File Offset: 0x00072607
	protected virtual void OnStop_Jump()
	{
		this.m_MotorJumpDone = true;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x00074410 File Offset: 0x00072610
	protected virtual bool CanStop_Crouch()
	{
		if (Physics.SphereCast(new Ray(base.Transform.position, Vector3.up), this.m_CharacterController.radius, this.m_NormalHeight - this.m_CharacterController.radius + 0.01f, -1744831509))
		{
			this.Player.Crouch.NextAllowedStopTime = Time.time + 1f;
			return false;
		}
		return true;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0007447F File Offset: 0x0007267F
	protected virtual void OnStart_Crouch()
	{
		if (this.MotorFreeFly && !this.Grounded)
		{
			return;
		}
		this.CharacterController.height = this.m_CrouchHeight;
		this.CharacterController.center = this.m_CrouchCenter;
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x000744B4 File Offset: 0x000726B4
	protected virtual void OnStop_Crouch()
	{
		this.CharacterController.height = this.m_NormalHeight;
		this.CharacterController.center = this.m_NormalCenter;
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x000744D8 File Offset: 0x000726D8
	protected virtual void OnMessage_ForceImpact(Vector3 force)
	{
		this.AddForce(force);
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x000744E1 File Offset: 0x000726E1
	protected virtual void OnMessage_Stop()
	{
		this.Stop();
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000745 RID: 1861 RVA: 0x000744E9 File Offset: 0x000726E9
	// (set) Token: 0x06000746 RID: 1862 RVA: 0x000744F6 File Offset: 0x000726F6
	protected virtual Vector3 OnValue_Position
	{
		get
		{
			return base.Transform.position;
		}
		set
		{
			this.SetPosition(value);
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000747 RID: 1863 RVA: 0x000744FF File Offset: 0x000726FF
	protected virtual Transform OnValue_Platform
	{
		get
		{
			return this.m_Platform;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000748 RID: 1864 RVA: 0x00074507 File Offset: 0x00072707
	// (set) Token: 0x06000749 RID: 1865 RVA: 0x0007450F File Offset: 0x0007270F
	protected virtual Vector2 OnValue_InputMoveVector
	{
		get
		{
			return this.m_MoveVector;
		}
		set
		{
			this.m_MoveVector = value.normalized;
		}
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0007451E File Offset: 0x0007271E
	private void CheckValue()
	{
		if (Time.time < this.lastcheck + 5f)
		{
			return;
		}
		this.lastcheck = Time.time;
	}

	// Token: 0x04000C2B RID: 3115
	protected vp_FPCamera m_FPSCamera;

	// Token: 0x04000C2C RID: 3116
	protected Map m_Map;

	// Token: 0x04000C2D RID: 3117
	private List<Vector3> m_PrevPosition = new List<Vector3>();

	// Token: 0x04000C2E RID: 3118
	protected CharacterController m_CharacterController;

	// Token: 0x04000C2F RID: 3119
	protected Vector3 m_FixedPosition = Vector3.zero;

	// Token: 0x04000C30 RID: 3120
	protected Vector3 m_SmoothPosition = Vector3.zero;

	// Token: 0x04000C31 RID: 3121
	public Client client;

	// Token: 0x04000C32 RID: 3122
	protected bool fake1;

	// Token: 0x04000C33 RID: 3123
	protected bool fake2;

	// Token: 0x04000C34 RID: 3124
	protected bool m_HeadContact;

	// Token: 0x04000C35 RID: 3125
	protected bool fake3;

	// Token: 0x04000C36 RID: 3126
	protected bool fake4;

	// Token: 0x04000C37 RID: 3127
	protected bool m_Grounded;

	// Token: 0x04000C38 RID: 3128
	protected bool m_NETGrounded;

	// Token: 0x04000C39 RID: 3129
	protected bool fake5;

	// Token: 0x04000C3A RID: 3130
	protected RaycastHit m_GroundHit;

	// Token: 0x04000C3B RID: 3131
	protected RaycastHit m_LastGroundHit;

	// Token: 0x04000C3C RID: 3132
	protected RaycastHit m_CeilingHit;

	// Token: 0x04000C3D RID: 3133
	protected RaycastHit m_WallHit;

	// Token: 0x04000C3E RID: 3134
	protected float m_FallImpact;

	// Token: 0x04000C3F RID: 3135
	protected float m_MotorAirSpeedModifier = 1f;

	// Token: 0x04000C40 RID: 3136
	protected float m_CurrentAntiBumpOffset;

	// Token: 0x04000C41 RID: 3137
	protected float m_SlopeFactor = 1f;

	// Token: 0x04000C42 RID: 3138
	protected Vector3 m_MoveDirection = Vector3.zero;

	// Token: 0x04000C43 RID: 3139
	protected Vector2 m_MoveVector = Vector2.zero;

	// Token: 0x04000C44 RID: 3140
	protected Vector3 m_MotorThrottle = Vector3.zero;

	// Token: 0x04000C45 RID: 3141
	public float MotorAcceleration = 0.18f;

	// Token: 0x04000C46 RID: 3142
	public float MotorDamping = 0.17f;

	// Token: 0x04000C47 RID: 3143
	public float MotorAirSpeed = 0.35f;

	// Token: 0x04000C48 RID: 3144
	public float MotorSlopeSpeedUp = 1f;

	// Token: 0x04000C49 RID: 3145
	public float MotorSlopeSpeedDown = 1f;

	// Token: 0x04000C4A RID: 3146
	public bool MotorFreeFly;

	// Token: 0x04000C4B RID: 3147
	public float MotorJumpForce = 0.2f;

	// Token: 0x04000C4C RID: 3148
	public float MotorJumpForceDamping = 0.1f;

	// Token: 0x04000C4D RID: 3149
	public float MotorJumpForceHold;

	// Token: 0x04000C4E RID: 3150
	public float MotorJumpForceHoldDamping;

	// Token: 0x04000C4F RID: 3151
	protected int m_MotorJumpForceHoldSkipFrames;

	// Token: 0x04000C50 RID: 3152
	protected float m_MotorJumpForceAcc;

	// Token: 0x04000C51 RID: 3153
	private bool m_MotorJumpDone = true;

	// Token: 0x04000C52 RID: 3154
	protected float m_FallSpeed;

	// Token: 0x04000C53 RID: 3155
	protected float m_LastFallSpeed;

	// Token: 0x04000C54 RID: 3156
	protected float m_HighestFallSpeed;

	// Token: 0x04000C55 RID: 3157
	public float PhysicsForceDamping = 0.05f;

	// Token: 0x04000C56 RID: 3158
	public float PhysicsPushForce = 5f;

	// Token: 0x04000C57 RID: 3159
	public float PhysicsGravityModifier = 0.2f;

	// Token: 0x04000C58 RID: 3160
	public float PhysicsSlopeSlideLimit = 30f;

	// Token: 0x04000C59 RID: 3161
	public float PhysicsSlopeSlidiness = 0.15f;

	// Token: 0x04000C5A RID: 3162
	public float PhysicsWallBounce;

	// Token: 0x04000C5B RID: 3163
	public float PhysicsWallFriction;

	// Token: 0x04000C5C RID: 3164
	public bool PhysicsHasCollisionTrigger = true;

	// Token: 0x04000C5D RID: 3165
	protected GameObject m_Trigger;

	// Token: 0x04000C5E RID: 3166
	protected Vector3 m_ExternalForce = Vector3.zero;

	// Token: 0x04000C5F RID: 3167
	protected Vector3[] m_SmoothForceFrame = new Vector3[120];

	// Token: 0x04000C60 RID: 3168
	protected bool m_Slide;

	// Token: 0x04000C61 RID: 3169
	protected bool m_SlideFast;

	// Token: 0x04000C62 RID: 3170
	protected float m_SlideFallSpeed;

	// Token: 0x04000C63 RID: 3171
	protected float m_OnSteepGroundSince;

	// Token: 0x04000C64 RID: 3172
	protected float m_SlopeSlideSpeed;

	// Token: 0x04000C65 RID: 3173
	protected Vector3 m_PredictedPos = Vector3.zero;

	// Token: 0x04000C66 RID: 3174
	protected Vector3 m_PrevPos = Vector3.zero;

	// Token: 0x04000C67 RID: 3175
	protected Vector3 m_PrevDir = Vector3.zero;

	// Token: 0x04000C68 RID: 3176
	protected Vector3 m_NewDir = Vector3.zero;

	// Token: 0x04000C69 RID: 3177
	protected float m_ForceImpact;

	// Token: 0x04000C6A RID: 3178
	protected float m_ForceMultiplier;

	// Token: 0x04000C6B RID: 3179
	protected Vector3 CapsuleBottom = Vector3.zero;

	// Token: 0x04000C6C RID: 3180
	protected Vector3 CapsuleTop = Vector3.zero;

	// Token: 0x04000C6D RID: 3181
	protected float m_SkinWidth = 0.08f;

	// Token: 0x04000C6E RID: 3182
	protected Transform m_Platform;

	// Token: 0x04000C6F RID: 3183
	protected Vector3 m_PositionOnPlatform = Vector3.zero;

	// Token: 0x04000C70 RID: 3184
	protected float m_LastPlatformAngle;

	// Token: 0x04000C71 RID: 3185
	protected Vector3 m_LastPlatformPos = Vector3.zero;

	// Token: 0x04000C72 RID: 3186
	protected float m_NormalHeight;

	// Token: 0x04000C73 RID: 3187
	protected Vector3 m_NormalCenter = Vector3.zero;

	// Token: 0x04000C74 RID: 3188
	protected float m_CrouchHeight;

	// Token: 0x04000C75 RID: 3189
	protected Vector3 m_CrouchCenter = Vector3.zero;

	// Token: 0x04000C76 RID: 3190
	private Block b;

	// Token: 0x04000C77 RID: 3191
	private Block bUp;

	// Token: 0x04000C78 RID: 3192
	private int currBlockType = 1;

	// Token: 0x04000C79 RID: 3193
	private Transform myTransform;

	// Token: 0x04000C7A RID: 3194
	private bool check_m_Grounded;

	// Token: 0x04000C7B RID: 3195
	protected Vector3[] vecData = new Vector3[16];

	// Token: 0x04000C7C RID: 3196
	protected int vecDataPos;

	// Token: 0x04000C7D RID: 3197
	private vp_FPPlayerEventHandler m_Player;

	// Token: 0x04000C7E RID: 3198
	private float maxspeed;

	// Token: 0x04000C7F RID: 3199
	private Vector3 m_PrevPos_;

	// Token: 0x04000C80 RID: 3200
	private int m_PosError;

	// Token: 0x04000C81 RID: 3201
	private Vector3 oldPos = Vector3.zero;

	// Token: 0x04000C82 RID: 3202
	private float lastcheck;
}
