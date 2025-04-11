using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E0 RID: 224
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class vp_MovingPlatform : MonoBehaviour
{
	// Token: 0x0600081A RID: 2074 RVA: 0x0007B540 File Offset: 0x00079740
	private void Start()
	{
		this.m_Transform = base.transform;
		this.m_Collider = base.GetComponentInChildren<Collider>();
		this.m_RigidBody = base.GetComponent<Rigidbody>();
		this.m_RigidBody.useGravity = false;
		this.m_RigidBody.isKinematic = true;
		this.m_NextWaypoint = 0;
		this.m_Audio = base.GetComponent<AudioSource>();
		this.m_Audio.loop = true;
		this.m_Audio.clip = this.SoundMove;
		if (this.PathWaypoints == null)
		{
			return;
		}
		base.gameObject.layer = 28;
		foreach (object obj in this.PathWaypoints.transform)
		{
			Transform transform = (Transform)obj;
			if (vp_Utility.IsActive(transform.gameObject))
			{
				this.m_Waypoints.Add(transform);
				transform.gameObject.layer = 28;
			}
			if (transform.GetComponent<Renderer>() != null)
			{
				transform.GetComponent<Renderer>().enabled = false;
			}
			if (transform.GetComponent<Collider>() != null)
			{
				transform.GetComponent<Collider>().enabled = false;
			}
		}
		IComparer @object = new vp_MovingPlatform.WaypointComparer();
		this.m_Waypoints.Sort(new Comparison<Transform>(@object.Compare));
		if (this.m_Waypoints.Count > 0)
		{
			this.m_CurrentTargetPosition = this.m_Waypoints[this.m_NextWaypoint].position;
			this.m_CurrentTargetAngle = this.m_Waypoints[this.m_NextWaypoint].eulerAngles;
			this.m_Transform.position = this.m_CurrentTargetPosition;
			this.m_Transform.eulerAngles = this.m_CurrentTargetAngle;
			if (this.MoveAutoStartTarget > this.m_Waypoints.Count - 1)
			{
				this.MoveAutoStartTarget = this.m_Waypoints.Count - 1;
			}
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0007B72C File Offset: 0x0007992C
	private void FixedUpdate()
	{
		this.UpdatePath();
		this.UpdateMovement();
		this.UpdateRotation();
		this.UpdateVelocity();
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x0007B748 File Offset: 0x00079948
	private void UpdatePath()
	{
		if (this.m_Waypoints.Count < 2)
		{
			return;
		}
		if (this.GetDistanceLeft() < 0.01f && Time.time >= this.m_NextAllowedMoveTime)
		{
			switch (this.PathType)
			{
			case vp_MovingPlatform.PathMoveType.PingPong:
				if (this.PathDirection == vp_MovingPlatform.Direction.Backwards)
				{
					if (this.m_NextWaypoint == 0)
					{
						this.PathDirection = vp_MovingPlatform.Direction.Forward;
					}
				}
				else if (this.m_NextWaypoint == this.m_Waypoints.Count - 1)
				{
					this.PathDirection = vp_MovingPlatform.Direction.Backwards;
				}
				this.OnArriveAtWaypoint();
				this.GoToNextWaypoint();
				break;
			case vp_MovingPlatform.PathMoveType.Loop:
				this.OnArriveAtWaypoint();
				this.GoToNextWaypoint();
				return;
			case vp_MovingPlatform.PathMoveType.Target:
				if (this.m_NextWaypoint != this.m_TargetedWayPoint)
				{
					if (this.m_Moving)
					{
						if (this.m_PhysicsCurrentMoveVelocity == 0f)
						{
							this.OnStart();
						}
						else
						{
							this.OnArriveAtWaypoint();
						}
					}
					this.GoToNextWaypoint();
					return;
				}
				if (this.m_Moving)
				{
					this.OnStop();
					return;
				}
				if (this.m_NextWaypoint != 0)
				{
					this.OnArriveAtDestination();
					return;
				}
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0007B846 File Offset: 0x00079A46
	private void OnStart()
	{
		if (this.SoundStart != null)
		{
			this.m_Audio.PlayOneShot(this.SoundStart, AudioListener.volume);
		}
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0007B86C File Offset: 0x00079A6C
	private void OnArriveAtWaypoint()
	{
		if (this.SoundWaypoint != null)
		{
			this.m_Audio.PlayOneShot(this.SoundWaypoint, AudioListener.volume);
		}
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0007B892 File Offset: 0x00079A92
	private void OnArriveAtDestination()
	{
		if (this.MoveReturnDelay > 0f && !this.m_ReturnDelayTimer.Active)
		{
			vp_Timer.In(this.MoveReturnDelay, delegate()
			{
				this.GoTo(0);
			}, this.m_ReturnDelayTimer);
		}
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x0007B8CC File Offset: 0x00079ACC
	private void OnStop()
	{
		this.m_Audio.Stop();
		if (this.SoundStop != null)
		{
			this.m_Audio.PlayOneShot(this.SoundStop, AudioListener.volume);
		}
		this.m_Transform.position = this.m_CurrentTargetPosition;
		this.m_Transform.eulerAngles = this.m_CurrentTargetAngle;
		this.m_Moving = false;
		if (this.m_NextWaypoint == 0)
		{
			this.m_NextAllowedMoveTime = Time.time + this.MoveCooldown;
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0007B94C File Offset: 0x00079B4C
	private void UpdateMovement()
	{
		if (this.m_Waypoints.Count < 2)
		{
			return;
		}
		switch (this.MoveInterpolationMode)
		{
		case vp_MovingPlatform.MovementInterpolationMode.EaseInOut:
			this.m_Transform.position = vp_Utility.NaNSafeVector3(Vector3.Lerp(this.m_Transform.position, this.m_CurrentTargetPosition, this.m_EaseInOutCurve.Evaluate(this.m_MoveTime)), default(Vector3));
			return;
		case vp_MovingPlatform.MovementInterpolationMode.EaseIn:
			this.m_Transform.position = vp_Utility.NaNSafeVector3(Vector3.MoveTowards(this.m_Transform.position, this.m_CurrentTargetPosition, this.m_MoveTime), default(Vector3));
			return;
		case vp_MovingPlatform.MovementInterpolationMode.EaseOut:
			this.m_Transform.position = vp_Utility.NaNSafeVector3(Vector3.Lerp(this.m_Transform.position, this.m_CurrentTargetPosition, this.m_LinearCurve.Evaluate(this.m_MoveTime)), default(Vector3));
			return;
		case vp_MovingPlatform.MovementInterpolationMode.EaseOut2:
			this.m_Transform.position = vp_Utility.NaNSafeVector3(Vector3.Lerp(this.m_Transform.position, this.m_CurrentTargetPosition, this.MoveSpeed * 0.25f), default(Vector3));
			return;
		case vp_MovingPlatform.MovementInterpolationMode.Slerp:
			this.m_Transform.position = vp_Utility.NaNSafeVector3(Vector3.Slerp(this.m_Transform.position, this.m_CurrentTargetPosition, this.m_LinearCurve.Evaluate(this.m_MoveTime)), default(Vector3));
			return;
		case vp_MovingPlatform.MovementInterpolationMode.Lerp:
			this.m_Transform.position = vp_Utility.NaNSafeVector3(Vector3.MoveTowards(this.m_Transform.position, this.m_CurrentTargetPosition, this.MoveSpeed), default(Vector3));
			return;
		default:
			return;
		}
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0007BAF8 File Offset: 0x00079CF8
	private void UpdateRotation()
	{
		switch (this.RotationInterpolationMode)
		{
		case vp_MovingPlatform.RotateInterpolationMode.SyncToMovement:
			if (this.m_Moving)
			{
				this.m_Transform.eulerAngles = vp_Utility.NaNSafeVector3(new Vector3(Mathf.LerpAngle(this.m_OriginalAngle.x, this.m_CurrentTargetAngle.x, 1f - this.GetDistanceLeft() / this.m_TravelDistance), Mathf.LerpAngle(this.m_OriginalAngle.y, this.m_CurrentTargetAngle.y, 1f - this.GetDistanceLeft() / this.m_TravelDistance), Mathf.LerpAngle(this.m_OriginalAngle.z, this.m_CurrentTargetAngle.z, 1f - this.GetDistanceLeft() / this.m_TravelDistance)), default(Vector3));
				return;
			}
			break;
		case vp_MovingPlatform.RotateInterpolationMode.EaseOut:
			this.m_Transform.eulerAngles = vp_Utility.NaNSafeVector3(new Vector3(Mathf.LerpAngle(this.m_Transform.eulerAngles.x, this.m_CurrentTargetAngle.x, this.m_LinearCurve.Evaluate(this.m_MoveTime)), Mathf.LerpAngle(this.m_Transform.eulerAngles.y, this.m_CurrentTargetAngle.y, this.m_LinearCurve.Evaluate(this.m_MoveTime)), Mathf.LerpAngle(this.m_Transform.eulerAngles.z, this.m_CurrentTargetAngle.z, this.m_LinearCurve.Evaluate(this.m_MoveTime))), default(Vector3));
			return;
		case vp_MovingPlatform.RotateInterpolationMode.CustomEaseOut:
			this.m_Transform.eulerAngles = vp_Utility.NaNSafeVector3(new Vector3(Mathf.LerpAngle(this.m_Transform.eulerAngles.x, this.m_CurrentTargetAngle.x, this.RotationEaseAmount), Mathf.LerpAngle(this.m_Transform.eulerAngles.y, this.m_CurrentTargetAngle.y, this.RotationEaseAmount), Mathf.LerpAngle(this.m_Transform.eulerAngles.z, this.m_CurrentTargetAngle.z, this.RotationEaseAmount)), default(Vector3));
			return;
		case vp_MovingPlatform.RotateInterpolationMode.CustomRotate:
			this.m_Transform.Rotate(this.RotationSpeed);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0007BD2C File Offset: 0x00079F2C
	private void UpdateVelocity()
	{
		this.m_MoveTime += this.MoveSpeed * 0.01f;
		this.m_PhysicsCurrentMoveVelocity = (this.m_Transform.position - this.m_PrevPos).magnitude;
		this.m_PhysicsCurrentRotationVelocity = (this.m_Transform.eulerAngles - this.m_PrevAngle).magnitude;
		this.m_PrevPos = this.m_Transform.position;
		this.m_PrevAngle = this.m_Transform.eulerAngles;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0007BDBC File Offset: 0x00079FBC
	public void GoTo(int targetWayPoint)
	{
		if (Time.time < this.m_NextAllowedMoveTime)
		{
			return;
		}
		if (this.PathType != vp_MovingPlatform.PathMoveType.Target)
		{
			return;
		}
		this.m_TargetedWayPoint = this.GetValidWaypoint(targetWayPoint);
		if (targetWayPoint > this.m_NextWaypoint)
		{
			if (this.PathDirection != vp_MovingPlatform.Direction.Direct)
			{
				this.PathDirection = vp_MovingPlatform.Direction.Forward;
			}
		}
		else if (this.PathDirection != vp_MovingPlatform.Direction.Direct)
		{
			this.PathDirection = vp_MovingPlatform.Direction.Backwards;
		}
		this.m_Moving = true;
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x0007BE20 File Offset: 0x0007A020
	protected float GetDistanceLeft()
	{
		if (this.m_Waypoints.Count < 2)
		{
			return 0f;
		}
		return Vector3.Distance(this.m_Transform.position, this.m_Waypoints[this.m_NextWaypoint].position);
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x0007BE5C File Offset: 0x0007A05C
	protected void GoToNextWaypoint()
	{
		if (this.m_Waypoints.Count < 2)
		{
			return;
		}
		this.m_MoveTime = 0f;
		if (!this.m_Audio.isPlaying)
		{
			this.m_Audio.Play();
		}
		this.m_CurrentWaypoint = this.m_NextWaypoint;
		switch (this.PathDirection)
		{
		case vp_MovingPlatform.Direction.Forward:
			this.m_NextWaypoint = this.GetValidWaypoint(this.m_NextWaypoint + 1);
			break;
		case vp_MovingPlatform.Direction.Backwards:
			this.m_NextWaypoint = this.GetValidWaypoint(this.m_NextWaypoint - 1);
			break;
		case vp_MovingPlatform.Direction.Direct:
			this.m_NextWaypoint = this.m_TargetedWayPoint;
			break;
		}
		this.m_OriginalAngle = this.m_CurrentTargetAngle;
		this.m_CurrentTargetPosition = this.m_Waypoints[this.m_NextWaypoint].position;
		this.m_CurrentTargetAngle = this.m_Waypoints[this.m_NextWaypoint].eulerAngles;
		this.m_TravelDistance = this.GetDistanceLeft();
		this.m_Moving = true;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x0007BF51 File Offset: 0x0007A151
	protected int GetValidWaypoint(int wayPoint)
	{
		if (wayPoint < 0)
		{
			return this.m_Waypoints.Count - 1;
		}
		if (wayPoint > this.m_Waypoints.Count - 1)
		{
			return 0;
		}
		return wayPoint;
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x0007BF78 File Offset: 0x0007A178
	private void OnTriggerEnter(Collider col)
	{
		if (!this.GetPlayer(col))
		{
			return;
		}
		this.TryPushPlayer();
		this.TryAutoStart();
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x0007BF90 File Offset: 0x0007A190
	private void OnTriggerStay(Collider col)
	{
		if (!this.PhysicsSnapPlayerToTopOnIntersect)
		{
			return;
		}
		if (!this.GetPlayer(col))
		{
			return;
		}
		this.TrySnapPlayerToTop();
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0007BFAC File Offset: 0x0007A1AC
	private bool GetPlayer(Collider col)
	{
		if (!this.m_KnownPlayers.ContainsKey(col))
		{
			if (col.gameObject.layer != 30)
			{
				return false;
			}
			vp_FPPlayerEventHandler component = col.transform.root.GetComponent<vp_FPPlayerEventHandler>();
			if (component == null)
			{
				return false;
			}
			this.m_KnownPlayers.Add(col, component);
		}
		if (!this.m_KnownPlayers.TryGetValue(col, out this.m_PlayerToPush))
		{
			return false;
		}
		this.m_PlayerCollider = col;
		return true;
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0007C020 File Offset: 0x0007A220
	private void TryPushPlayer()
	{
		if (this.m_PlayerToPush == null || this.m_PlayerToPush.Platform == null)
		{
			return;
		}
		if (this.m_PlayerToPush.Position.Get().y > this.m_Collider.bounds.max.y)
		{
			return;
		}
		if (this.m_PlayerToPush.Platform.Get() == this.m_Transform)
		{
			return;
		}
		float num = this.m_PhysicsCurrentMoveVelocity;
		if (num == 0f)
		{
			num = this.m_PhysicsCurrentRotationVelocity * 0.1f;
		}
		if (num > 0f)
		{
			this.m_PlayerToPush.ForceImpact.Send(vp_Utility.HorizontalVector(-(this.m_Transform.position - this.m_PlayerCollider.bounds.center).normalized * num * this.m_PhysicsPushForce));
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0007C120 File Offset: 0x0007A320
	private void TrySnapPlayerToTop()
	{
		if (this.m_PlayerToPush == null || this.m_PlayerToPush.Platform == null)
		{
			return;
		}
		if (this.m_PlayerToPush.Position.Get().y > this.m_Collider.bounds.max.y)
		{
			return;
		}
		if (this.m_PlayerToPush.Platform.Get() == this.m_Transform)
		{
			return;
		}
		if (this.RotationSpeed.x != 0f || this.RotationSpeed.z != 0f || this.m_CurrentTargetAngle.x != 0f || this.m_CurrentTargetAngle.z != 0f)
		{
			return;
		}
		if (this.m_Collider.bounds.max.x < this.m_PlayerCollider.bounds.max.x || this.m_Collider.bounds.max.z < this.m_PlayerCollider.bounds.max.z || this.m_Collider.bounds.min.x > this.m_PlayerCollider.bounds.min.x || this.m_Collider.bounds.min.z > this.m_PlayerCollider.bounds.min.z)
		{
			return;
		}
		Vector3 o = this.m_PlayerToPush.Position.Get();
		o.y = this.m_Collider.bounds.max.y - 0.1f;
		this.m_PlayerToPush.Position.Set(o);
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0007C307 File Offset: 0x0007A507
	private void TryAutoStart()
	{
		if (this.MoveAutoStartTarget == 0)
		{
			return;
		}
		if (this.m_PhysicsCurrentMoveVelocity > 0f || this.m_Moving)
		{
			return;
		}
		this.GoTo(this.MoveAutoStartTarget);
	}

	// Token: 0x04000DAF RID: 3503
	protected Transform m_Transform;

	// Token: 0x04000DB0 RID: 3504
	public vp_MovingPlatform.PathMoveType PathType;

	// Token: 0x04000DB1 RID: 3505
	public GameObject PathWaypoints;

	// Token: 0x04000DB2 RID: 3506
	public vp_MovingPlatform.Direction PathDirection;

	// Token: 0x04000DB3 RID: 3507
	protected List<Transform> m_Waypoints = new List<Transform>();

	// Token: 0x04000DB4 RID: 3508
	protected int m_NextWaypoint;

	// Token: 0x04000DB5 RID: 3509
	protected Vector3 m_CurrentTargetPosition = Vector3.zero;

	// Token: 0x04000DB6 RID: 3510
	protected Vector3 m_CurrentTargetAngle = Vector3.zero;

	// Token: 0x04000DB7 RID: 3511
	protected int m_TargetedWayPoint;

	// Token: 0x04000DB8 RID: 3512
	protected float m_TravelDistance;

	// Token: 0x04000DB9 RID: 3513
	protected Vector3 m_OriginalAngle = Vector3.zero;

	// Token: 0x04000DBA RID: 3514
	protected int m_CurrentWaypoint;

	// Token: 0x04000DBB RID: 3515
	public int MoveAutoStartTarget = 1000;

	// Token: 0x04000DBC RID: 3516
	public float MoveSpeed = 0.1f;

	// Token: 0x04000DBD RID: 3517
	public float MoveReturnDelay;

	// Token: 0x04000DBE RID: 3518
	public float MoveCooldown;

	// Token: 0x04000DBF RID: 3519
	protected bool m_Moving;

	// Token: 0x04000DC0 RID: 3520
	protected float m_NextAllowedMoveTime;

	// Token: 0x04000DC1 RID: 3521
	protected float m_MoveTime;

	// Token: 0x04000DC2 RID: 3522
	protected vp_Timer.Handle m_ReturnDelayTimer = new vp_Timer.Handle();

	// Token: 0x04000DC3 RID: 3523
	protected Vector3 m_PrevPos = Vector3.zero;

	// Token: 0x04000DC4 RID: 3524
	public vp_MovingPlatform.MovementInterpolationMode MoveInterpolationMode;

	// Token: 0x04000DC5 RID: 3525
	protected AnimationCurve m_EaseInOutCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000DC6 RID: 3526
	protected AnimationCurve m_LinearCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04000DC7 RID: 3527
	public float RotationEaseAmount = 0.1f;

	// Token: 0x04000DC8 RID: 3528
	public Vector3 RotationSpeed = Vector3.zero;

	// Token: 0x04000DC9 RID: 3529
	protected Vector3 m_PrevAngle = Vector3.zero;

	// Token: 0x04000DCA RID: 3530
	public vp_MovingPlatform.RotateInterpolationMode RotationInterpolationMode;

	// Token: 0x04000DCB RID: 3531
	public AudioClip SoundStart;

	// Token: 0x04000DCC RID: 3532
	public AudioClip SoundStop;

	// Token: 0x04000DCD RID: 3533
	public AudioClip SoundMove;

	// Token: 0x04000DCE RID: 3534
	public AudioClip SoundWaypoint;

	// Token: 0x04000DCF RID: 3535
	protected AudioSource m_Audio;

	// Token: 0x04000DD0 RID: 3536
	public bool PhysicsSnapPlayerToTopOnIntersect = true;

	// Token: 0x04000DD1 RID: 3537
	protected Rigidbody m_RigidBody;

	// Token: 0x04000DD2 RID: 3538
	protected Collider m_Collider;

	// Token: 0x04000DD3 RID: 3539
	protected Collider m_PlayerCollider;

	// Token: 0x04000DD4 RID: 3540
	protected vp_FPPlayerEventHandler m_PlayerToPush;

	// Token: 0x04000DD5 RID: 3541
	public float m_PhysicsPushForce = 2f;

	// Token: 0x04000DD6 RID: 3542
	protected float m_PhysicsCurrentMoveVelocity;

	// Token: 0x04000DD7 RID: 3543
	protected float m_PhysicsCurrentRotationVelocity;

	// Token: 0x04000DD8 RID: 3544
	protected Dictionary<Collider, vp_FPPlayerEventHandler> m_KnownPlayers = new Dictionary<Collider, vp_FPPlayerEventHandler>();

	// Token: 0x0200089E RID: 2206
	public enum PathMoveType
	{
		// Token: 0x040032B7 RID: 12983
		PingPong,
		// Token: 0x040032B8 RID: 12984
		Loop,
		// Token: 0x040032B9 RID: 12985
		Target
	}

	// Token: 0x0200089F RID: 2207
	public enum Direction
	{
		// Token: 0x040032BB RID: 12987
		Forward,
		// Token: 0x040032BC RID: 12988
		Backwards,
		// Token: 0x040032BD RID: 12989
		Direct
	}

	// Token: 0x020008A0 RID: 2208
	protected class WaypointComparer : IComparer
	{
		// Token: 0x06004C9E RID: 19614 RVA: 0x001AEA33 File Offset: 0x001ACC33
		int IComparer.Compare(object x, object y)
		{
			return new CaseInsensitiveComparer().Compare(((Transform)x).name, ((Transform)y).name);
		}
	}

	// Token: 0x020008A1 RID: 2209
	public enum MovementInterpolationMode
	{
		// Token: 0x040032BF RID: 12991
		EaseInOut,
		// Token: 0x040032C0 RID: 12992
		EaseIn,
		// Token: 0x040032C1 RID: 12993
		EaseOut,
		// Token: 0x040032C2 RID: 12994
		EaseOut2,
		// Token: 0x040032C3 RID: 12995
		Slerp,
		// Token: 0x040032C4 RID: 12996
		Lerp
	}

	// Token: 0x020008A2 RID: 2210
	public enum RotateInterpolationMode
	{
		// Token: 0x040032C6 RID: 12998
		SyncToMovement,
		// Token: 0x040032C7 RID: 12999
		EaseOut,
		// Token: 0x040032C8 RID: 13000
		CustomEaseOut,
		// Token: 0x040032C9 RID: 13001
		CustomRotate
	}
}
