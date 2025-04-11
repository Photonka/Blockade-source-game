using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
[RequireComponent(typeof(vp_Shooter))]
public class vp_SimpleAITurret : MonoBehaviour
{
	// Token: 0x0600086A RID: 2154 RVA: 0x0007DC2A File Offset: 0x0007BE2A
	private void Start()
	{
		this.m_Shooter = base.GetComponent<vp_Shooter>();
		this.m_Transform = base.transform;
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0007DC44 File Offset: 0x0007BE44
	private void Update()
	{
		if (!this.m_Timer.Active)
		{
			vp_Timer.In(this.WakeInterval, delegate()
			{
				if (this.m_Target == null)
				{
					this.m_Target = this.ScanForLocalPlayer();
					return;
				}
				this.m_Target = null;
			}, this.m_Timer);
		}
		if (this.m_Target != null)
		{
			this.AttackTarget();
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0007DC84 File Offset: 0x0007BE84
	private Transform ScanForLocalPlayer()
	{
		foreach (Collider collider in Physics.OverlapSphere(this.m_Transform.position, this.ViewRange, 1073741824))
		{
			RaycastHit raycastHit;
			Physics.Linecast(this.m_Transform.position, collider.transform.position + Vector3.up, ref raycastHit);
			if (!(raycastHit.collider != null) || !(raycastHit.collider != collider))
			{
				return collider.transform;
			}
		}
		return null;
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0007DD10 File Offset: 0x0007BF10
	private void AttackTarget()
	{
		Quaternion quaternion = Quaternion.LookRotation(this.m_Target.position - this.m_Transform.position);
		this.m_Transform.rotation = Quaternion.RotateTowards(this.m_Transform.rotation, quaternion, Time.deltaTime * this.AimSpeed);
		this.m_Shooter.TryFire();
	}

	// Token: 0x04000E2C RID: 3628
	public float ViewRange = 10f;

	// Token: 0x04000E2D RID: 3629
	public float AimSpeed = 50f;

	// Token: 0x04000E2E RID: 3630
	public float WakeInterval = 2f;

	// Token: 0x04000E2F RID: 3631
	protected vp_Shooter m_Shooter;

	// Token: 0x04000E30 RID: 3632
	protected Transform m_Transform;

	// Token: 0x04000E31 RID: 3633
	protected Transform m_Target;

	// Token: 0x04000E32 RID: 3634
	protected vp_Timer.Handle m_Timer = new vp_Timer.Handle();
}
