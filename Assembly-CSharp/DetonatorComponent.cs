using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
public abstract class DetonatorComponent : MonoBehaviour
{
	// Token: 0x06000A89 RID: 2697
	public abstract void Explode();

	// Token: 0x06000A8A RID: 2698
	public abstract void Init();

	// Token: 0x06000A8B RID: 2699 RVA: 0x0008925C File Offset: 0x0008745C
	public void SetStartValues()
	{
		this.startSize = this.size;
		this.startForce = this.force;
		this.startVelocity = this.velocity;
		this.startDuration = this.duration;
		this.startDetail = this.detail;
		this.startColor = this.color;
		this.startLocalPosition = this.localPosition;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x000892BD File Offset: 0x000874BD
	public Detonator MyDetonator()
	{
		return base.GetComponent("Detonator") as Detonator;
	}

	// Token: 0x0400101C RID: 4124
	public bool on = true;

	// Token: 0x0400101D RID: 4125
	public bool detonatorControlled = true;

	// Token: 0x0400101E RID: 4126
	[HideInInspector]
	public float startSize = 1f;

	// Token: 0x0400101F RID: 4127
	public float size = 1f;

	// Token: 0x04001020 RID: 4128
	public float explodeDelayMin;

	// Token: 0x04001021 RID: 4129
	public float explodeDelayMax;

	// Token: 0x04001022 RID: 4130
	[HideInInspector]
	public float startDuration = 2f;

	// Token: 0x04001023 RID: 4131
	public float duration = 2f;

	// Token: 0x04001024 RID: 4132
	[HideInInspector]
	public float timeScale = 1f;

	// Token: 0x04001025 RID: 4133
	[HideInInspector]
	public float startDetail = 1f;

	// Token: 0x04001026 RID: 4134
	public float detail = 1f;

	// Token: 0x04001027 RID: 4135
	[HideInInspector]
	public Color startColor = Color.white;

	// Token: 0x04001028 RID: 4136
	public Color color = Color.white;

	// Token: 0x04001029 RID: 4137
	[HideInInspector]
	public Vector3 startLocalPosition = Vector3.zero;

	// Token: 0x0400102A RID: 4138
	public Vector3 localPosition = Vector3.zero;

	// Token: 0x0400102B RID: 4139
	[HideInInspector]
	public Vector3 startForce = Vector3.zero;

	// Token: 0x0400102C RID: 4140
	public Vector3 force = Vector3.zero;

	// Token: 0x0400102D RID: 4141
	[HideInInspector]
	public Vector3 startVelocity = Vector3.zero;

	// Token: 0x0400102E RID: 4142
	public Vector3 velocity = Vector3.zero;

	// Token: 0x0400102F RID: 4143
	public float detailThreshold;
}
