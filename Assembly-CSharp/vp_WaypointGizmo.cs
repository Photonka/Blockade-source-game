using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class vp_WaypointGizmo : MonoBehaviour
{
	// Token: 0x060006DE RID: 1758 RVA: 0x00070F44 File Offset: 0x0006F144
	public void OnDrawGizmos()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = this.m_GizmoColor;
		Gizmos.DrawCube(Vector3.zero, Vector3.one);
		Gizmos.color = new Color(0f, 0f, 0f, 1f);
		Gizmos.DrawLine(Vector3.zero, Vector3.forward);
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00070FA8 File Offset: 0x0006F1A8
	public void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = this.m_SelectedGizmoColor;
		Gizmos.DrawCube(Vector3.zero, Vector3.one);
		Gizmos.color = new Color(0f, 0f, 0f, 1f);
		Gizmos.DrawLine(Vector3.zero, Vector3.forward);
	}

	// Token: 0x04000BF1 RID: 3057
	protected Color m_GizmoColor = new Color(1f, 1f, 1f, 0.4f);

	// Token: 0x04000BF2 RID: 3058
	protected Color m_SelectedGizmoColor = new Color32(160, byte.MaxValue, 100, 100);
}
