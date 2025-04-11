using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class vp_Earthquake : MonoBehaviour
{
	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000809 RID: 2057 RVA: 0x0007AC64 File Offset: 0x00078E64
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

	// Token: 0x0600080A RID: 2058 RVA: 0x0007AC99 File Offset: 0x00078E99
	protected virtual void Awake()
	{
		this.EventHandler = (vp_EventHandler)Object.FindObjectOfType(typeof(vp_EventHandler));
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x0007ACB5 File Offset: 0x00078EB5
	protected virtual void OnEnable()
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Register(this);
		}
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x0007ACD1 File Offset: 0x00078ED1
	protected virtual void OnDisable()
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Unregister(this);
		}
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x0007ACED File Offset: 0x00078EED
	protected void FixedUpdate()
	{
		if (Time.timeScale != 0f)
		{
			this.UpdateEarthQuake();
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x0007AD04 File Offset: 0x00078F04
	protected void UpdateEarthQuake()
	{
		if (!this.Player.Earthquake.Active)
		{
			this.m_EarthQuakeForce = Vector3.zero;
			return;
		}
		this.m_EarthQuakeForce = Vector3.Scale(vp_SmoothRandom.GetVector3Centered(1f), this.m_Magnitude.x * (Vector3.right + Vector3.forward) * Mathf.Min(this.m_Endtime - Time.time, 1f) * Time.timeScale);
		this.m_EarthQuakeForce.y = 0f;
		if (Random.value < 0.3f * Time.timeScale)
		{
			this.m_EarthQuakeForce.y = Random.Range(0f, this.m_Magnitude.y * 0.35f) * Mathf.Min(this.m_Endtime - Time.time, 1f);
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x0007ADE8 File Offset: 0x00078FE8
	protected virtual void OnStart_Earthquake()
	{
		Vector3 vector = (Vector3)this.Player.Earthquake.Argument;
		this.m_Magnitude.x = vector.x;
		this.m_Magnitude.y = vector.y;
		this.m_Endtime = Time.time + vector.z;
		this.Player.Earthquake.AutoDuration = vector.z;
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0007AE55 File Offset: 0x00079055
	protected virtual void OnMessage_BombShake(float impact)
	{
		this.Player.Earthquake.TryStart<Vector3>(new Vector3(impact * 0.5f, impact * 0.5f, 1f));
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000811 RID: 2065 RVA: 0x0007AE80 File Offset: 0x00079080
	// (set) Token: 0x06000812 RID: 2066 RVA: 0x0007AE88 File Offset: 0x00079088
	protected virtual Vector3 OnValue_EarthQuakeForce
	{
		get
		{
			return this.m_EarthQuakeForce;
		}
		set
		{
			this.m_EarthQuakeForce = value;
		}
	}

	// Token: 0x04000D91 RID: 3473
	protected Vector3 m_EarthQuakeForce;

	// Token: 0x04000D92 RID: 3474
	protected float m_Endtime;

	// Token: 0x04000D93 RID: 3475
	protected Vector2 m_Magnitude = Vector2.zero;

	// Token: 0x04000D94 RID: 3476
	protected vp_EventHandler EventHandler;

	// Token: 0x04000D95 RID: 3477
	private vp_FPPlayerEventHandler m_Player;
}
