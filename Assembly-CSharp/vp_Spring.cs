using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class vp_Spring
{
	// Token: 0x17000023 RID: 35
	// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0006FCCB File Offset: 0x0006DECB
	public Transform Transform
	{
		set
		{
			this.m_Transform = value;
			this.RefreshUpdateMode();
		}
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0006FCDC File Offset: 0x0006DEDC
	public vp_Spring(Transform transform, vp_Spring.UpdateMode mode, bool autoUpdate = true)
	{
		this.Mode = mode;
		this.Transform = transform;
		this.m_AutoUpdate = autoUpdate;
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0006FDC4 File Offset: 0x0006DFC4
	public void FixedUpdate()
	{
		if (this.m_VelocityFadeInEndTime > Time.time)
		{
			this.m_VelocityFadeInCap = Mathf.Clamp01(1f - (this.m_VelocityFadeInEndTime - Time.time) / this.m_VelocityFadeInLength);
		}
		else
		{
			this.m_VelocityFadeInCap = 1f;
		}
		if (this.m_SoftForceFrame[0] != Vector3.zero)
		{
			this.AddForceInternal(this.m_SoftForceFrame[0]);
			for (int i = 0; i < 120; i++)
			{
				this.m_SoftForceFrame[i] = ((i < 119) ? this.m_SoftForceFrame[i + 1] : Vector3.zero);
				if (this.m_SoftForceFrame[i] == Vector3.zero)
				{
					break;
				}
			}
		}
		this.Calculate();
		this.m_UpdateFunc();
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0006FE95 File Offset: 0x0006E095
	private void Position()
	{
		this.m_Transform.localPosition = this.State;
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0006FEA8 File Offset: 0x0006E0A8
	private void Rotation()
	{
		this.m_Transform.localEulerAngles = this.State;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0006FEBB File Offset: 0x0006E0BB
	private void Scale()
	{
		this.m_Transform.localScale = this.State;
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x0006FECE File Offset: 0x0006E0CE
	private void PositionAdditive()
	{
		this.m_Transform.localPosition += this.State;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0006FEEC File Offset: 0x0006E0EC
	private void RotationAdditive()
	{
		this.m_Transform.localEulerAngles += this.State;
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0006FF0A File Offset: 0x0006E10A
	private void ScaleAdditive()
	{
		this.m_Transform.localScale += this.State;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00002B75 File Offset: 0x00000D75
	private void None()
	{
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0006FF28 File Offset: 0x0006E128
	protected void RefreshUpdateMode()
	{
		this.m_UpdateFunc = new vp_Spring.UpdateDelegate(this.None);
		switch (this.Mode)
		{
		case vp_Spring.UpdateMode.Position:
			this.State = this.m_Transform.localPosition;
			if (this.m_AutoUpdate)
			{
				this.m_UpdateFunc = new vp_Spring.UpdateDelegate(this.Position);
			}
			break;
		case vp_Spring.UpdateMode.PositionAdditive:
			this.State = this.m_Transform.localPosition;
			if (this.m_AutoUpdate)
			{
				this.m_UpdateFunc = new vp_Spring.UpdateDelegate(this.PositionAdditive);
			}
			break;
		case vp_Spring.UpdateMode.Rotation:
			this.State = this.m_Transform.localEulerAngles;
			if (this.m_AutoUpdate)
			{
				this.m_UpdateFunc = new vp_Spring.UpdateDelegate(this.Rotation);
			}
			break;
		case vp_Spring.UpdateMode.RotationAdditive:
			this.State = this.m_Transform.localEulerAngles;
			if (this.m_AutoUpdate)
			{
				this.m_UpdateFunc = new vp_Spring.UpdateDelegate(this.RotationAdditive);
			}
			break;
		case vp_Spring.UpdateMode.Scale:
			this.State = this.m_Transform.localScale;
			if (this.m_AutoUpdate)
			{
				this.m_UpdateFunc = new vp_Spring.UpdateDelegate(this.Scale);
			}
			break;
		case vp_Spring.UpdateMode.ScaleAdditive:
			this.State = this.m_Transform.localScale;
			if (this.m_AutoUpdate)
			{
				this.m_UpdateFunc = new vp_Spring.UpdateDelegate(this.ScaleAdditive);
			}
			break;
		}
		this.RestState = this.State;
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0007009C File Offset: 0x0006E29C
	protected void Calculate()
	{
		if (this.State == this.RestState)
		{
			return;
		}
		this.m_Velocity += Vector3.Scale(this.RestState - this.State, this.Stiffness);
		this.m_Velocity = Vector3.Scale(this.m_Velocity, this.Damping);
		this.m_Velocity = Vector3.ClampMagnitude(this.m_Velocity, this.MaxVelocity);
		if (this.m_Velocity.sqrMagnitude > this.MinVelocity * this.MinVelocity)
		{
			this.Move();
			return;
		}
		this.Reset();
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0007013F File Offset: 0x0006E33F
	private void AddForceInternal(Vector3 force)
	{
		force *= this.m_VelocityFadeInCap;
		this.m_Velocity += force;
		this.m_Velocity = Vector3.ClampMagnitude(this.m_Velocity, this.MaxVelocity);
		this.Move();
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0007017E File Offset: 0x0006E37E
	public void AddForce(Vector3 force)
	{
		if (Time.timeScale < 1f)
		{
			this.AddSoftForce(force, 1f);
			return;
		}
		this.AddForceInternal(force);
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x000701A0 File Offset: 0x0006E3A0
	public void AddSoftForce(Vector3 force, float frames)
	{
		force /= Time.timeScale;
		frames = Mathf.Clamp(frames, 1f, 120f);
		this.AddForceInternal(force / frames);
		for (int i = 0; i < Mathf.RoundToInt(frames) - 1; i++)
		{
			this.m_SoftForceFrame[i] += force / frames;
		}
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00070210 File Offset: 0x0006E410
	protected void Move()
	{
		this.State += this.m_Velocity * Time.timeScale;
		this.State.x = Mathf.Clamp(this.State.x, this.MinState.x, this.MaxState.x);
		this.State.y = Mathf.Clamp(this.State.y, this.MinState.y, this.MaxState.y);
		this.State.z = Mathf.Clamp(this.State.z, this.MinState.z, this.MaxState.z);
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x000702D1 File Offset: 0x0006E4D1
	public void Reset()
	{
		this.m_Velocity = Vector3.zero;
		this.State = this.RestState;
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x000702EA File Offset: 0x0006E4EA
	public void Stop(bool includeSoftForce = false)
	{
		this.m_Velocity = Vector3.zero;
		if (includeSoftForce)
		{
			this.StopSoftForce();
		}
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x00070300 File Offset: 0x0006E500
	public void StopSoftForce()
	{
		for (int i = 0; i < 120; i++)
		{
			this.m_SoftForceFrame[i] = Vector3.zero;
		}
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0007032B File Offset: 0x0006E52B
	public void ForceVelocityFadeIn(float seconds)
	{
		this.m_VelocityFadeInLength = seconds;
		this.m_VelocityFadeInEndTime = Time.time + seconds;
		this.m_VelocityFadeInCap = 0f;
	}

	// Token: 0x04000BC5 RID: 3013
	protected vp_Spring.UpdateMode Mode;

	// Token: 0x04000BC6 RID: 3014
	protected bool m_AutoUpdate = true;

	// Token: 0x04000BC7 RID: 3015
	protected vp_Spring.UpdateDelegate m_UpdateFunc;

	// Token: 0x04000BC8 RID: 3016
	public Vector3 State = Vector3.zero;

	// Token: 0x04000BC9 RID: 3017
	protected Vector3 m_Velocity = Vector3.zero;

	// Token: 0x04000BCA RID: 3018
	public Vector3 RestState = Vector3.zero;

	// Token: 0x04000BCB RID: 3019
	public Vector3 Stiffness = new Vector3(0.5f, 0.5f, 0.5f);

	// Token: 0x04000BCC RID: 3020
	public Vector3 Damping = new Vector3(0.75f, 0.75f, 0.75f);

	// Token: 0x04000BCD RID: 3021
	protected float m_VelocityFadeInCap = 1f;

	// Token: 0x04000BCE RID: 3022
	protected float m_VelocityFadeInEndTime;

	// Token: 0x04000BCF RID: 3023
	protected float m_VelocityFadeInLength;

	// Token: 0x04000BD0 RID: 3024
	protected Vector3[] m_SoftForceFrame = new Vector3[120];

	// Token: 0x04000BD1 RID: 3025
	public float MaxVelocity = 10000f;

	// Token: 0x04000BD2 RID: 3026
	public float MinVelocity = 1E-07f;

	// Token: 0x04000BD3 RID: 3027
	public Vector3 MaxState = new Vector3(10000f, 10000f, 10000f);

	// Token: 0x04000BD4 RID: 3028
	public Vector3 MinState = new Vector3(-10000f, -10000f, -10000f);

	// Token: 0x04000BD5 RID: 3029
	protected Transform m_Transform;

	// Token: 0x02000890 RID: 2192
	public enum UpdateMode
	{
		// Token: 0x04003291 RID: 12945
		Position,
		// Token: 0x04003292 RID: 12946
		PositionAdditive,
		// Token: 0x04003293 RID: 12947
		Rotation,
		// Token: 0x04003294 RID: 12948
		RotationAdditive,
		// Token: 0x04003295 RID: 12949
		Scale,
		// Token: 0x04003296 RID: 12950
		ScaleAdditive
	}

	// Token: 0x02000891 RID: 2193
	// (Invoke) Token: 0x06004C5D RID: 19549
	protected delegate void UpdateDelegate();
}
