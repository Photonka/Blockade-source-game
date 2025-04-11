using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
public struct Contact
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0008757C File Offset: 0x0008577C
	public float sqrDistance
	{
		get
		{
			return (this.a - this.b).sqrMagnitude;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000875A2 File Offset: 0x000857A2
	public Vector3 delta
	{
		get
		{
			return this.a - this.b;
		}
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x000875B5 File Offset: 0x000857B5
	public Contact(Vector3 a, Vector3 b)
	{
		this.a = a;
		this.b = b;
	}

	// Token: 0x04000F9D RID: 3997
	public Vector3 a;

	// Token: 0x04000F9E RID: 3998
	public Vector3 b;
}
