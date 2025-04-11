using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class vp_MuzzleFlash : MonoBehaviour
{
	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000830 RID: 2096 RVA: 0x0007C424 File Offset: 0x0007A624
	// (set) Token: 0x06000831 RID: 2097 RVA: 0x0007C42C File Offset: 0x0007A62C
	public float FadeSpeed
	{
		get
		{
			return this.m_FadeSpeed;
		}
		set
		{
			this.m_FadeSpeed = value;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000832 RID: 2098 RVA: 0x0007C435 File Offset: 0x0007A635
	// (set) Token: 0x06000833 RID: 2099 RVA: 0x0007C43D File Offset: 0x0007A63D
	public bool ForceShow
	{
		get
		{
			return this.m_ForceShow;
		}
		set
		{
			this.m_ForceShow = value;
		}
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0007C448 File Offset: 0x0007A648
	private void Awake()
	{
		this.m_Transform = base.transform;
		if (this.flamethrower)
		{
			this.myPS = base.GetComponent<ParticleSystem>();
		}
		else
		{
			this.m_Color = base.GetComponent<Renderer>().material.GetColor("_TintColor");
			this.m_Color.a = 0f;
		}
		this.m_ForceShow = false;
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0007C4A9 File Offset: 0x0007A6A9
	private void Start()
	{
		if (this.m_Transform.root.gameObject.layer == 30)
		{
			base.gameObject.layer = 31;
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0007C4D4 File Offset: 0x0007A6D4
	private void Update()
	{
		if (!this.flamethrower)
		{
			if (this.m_ForceShow)
			{
				this.Show();
			}
			else if (this.m_Color.a > 0f)
			{
				this.m_Color.a = this.m_Color.a - this.m_FadeSpeed * (Time.deltaTime * 60f);
			}
			base.GetComponent<Renderer>().material.SetColor("_TintColor", this.m_Color);
			return;
		}
		if (this.myPS == null)
		{
			return;
		}
		if (!this.myPS.isPlaying)
		{
			return;
		}
		if (this.flameTimer > Time.time)
		{
			return;
		}
		this.myPS.Stop(true);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0007C57F File Offset: 0x0007A77F
	public void Show()
	{
		this.m_Color.a = 0.5f;
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0007C594 File Offset: 0x0007A794
	public void Shoot()
	{
		if (!this.flamethrower)
		{
			this.m_Transform.Rotate(0f, 0f, (float)Random.Range(0, 360));
			this.m_Color.a = 0.5f;
			return;
		}
		if (this.myPS == null)
		{
			return;
		}
		if (!this.myPS.isPlaying)
		{
			this.myPS.Play(true);
		}
		this.flameTimer = Time.time + 0.1f;
	}

	// Token: 0x04000DD9 RID: 3545
	protected float m_FadeSpeed = 0.075f;

	// Token: 0x04000DDA RID: 3546
	protected bool m_ForceShow;

	// Token: 0x04000DDB RID: 3547
	protected Color m_Color = new Color(1f, 1f, 1f, 0f);

	// Token: 0x04000DDC RID: 3548
	protected Transform m_Transform;

	// Token: 0x04000DDD RID: 3549
	public bool flamethrower;

	// Token: 0x04000DDE RID: 3550
	private ParticleSystem myPS;

	// Token: 0x04000DDF RID: 3551
	private float flameTimer;
}
