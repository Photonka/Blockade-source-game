using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class Trackball : MonoBehaviour
{
	// Token: 0x06000042 RID: 66 RVA: 0x000030F0 File Offset: 0x000012F0
	private void Update()
	{
		if (Input.GetMouseButton(1))
		{
			float num = (float)Mathf.Max(Screen.width, Screen.height);
			float num2 = (Input.mousePosition.x - (float)(Screen.width / 2)) / num * 2f;
			float num3 = (Input.mousePosition.y - (float)(Screen.height / 2)) / num * 2f;
			num2 = Mathf.Clamp(num2, -1f, 1f);
			num3 = Mathf.Clamp(num3, -1f, 1f);
			Vector3 vector;
			vector..ctor(num2, num3, 0f);
			vector.z = -Mathf.Clamp01(1f - vector.magnitude);
			vector.Normalize();
			Vector3 vector2;
			vector2..ctor(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			vector3 = Camera.main.transform.TransformDirection(vector3);
			base.transform.Rotate(vector3, vector2.magnitude * 5f, 0);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000031F5 File Offset: 0x000013F5
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(base.transform.position, 0.1f);
	}
}
