using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public struct Vector3i
{
	// Token: 0x060009DD RID: 2525 RVA: 0x00083465 File Offset: 0x00081665
	public Vector3i(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0008347C File Offset: 0x0008167C
	public Vector3i(int x, int y)
	{
		this.x = x;
		this.y = y;
		this.z = 0;
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00083494 File Offset: 0x00081694
	public static int DistanceSquared(Vector3i a, Vector3i b)
	{
		int num = b.x - a.x;
		int num2 = b.y - a.y;
		int num3 = b.z - a.z;
		return num * num + num2 * num2 + num3 * num3;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x000834D4 File Offset: 0x000816D4
	public int DistanceSquared(Vector3i v)
	{
		return Vector3i.DistanceSquared(this, v);
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x000834E2 File Offset: 0x000816E2
	public override int GetHashCode()
	{
		return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0008350C File Offset: 0x0008170C
	public override bool Equals(object other)
	{
		if (!(other is Vector3i))
		{
			return false;
		}
		Vector3i vector3i = (Vector3i)other;
		return this.x == vector3i.x && this.y == vector3i.y && this.z == vector3i.z;
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00083558 File Offset: 0x00081758
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"Vector3i(",
			this.x,
			" ",
			this.y,
			" ",
			this.z,
			")"
		});
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x000835BA File Offset: 0x000817BA
	public static Vector3i Min(Vector3i a, Vector3i b)
	{
		return new Vector3i(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x000835F4 File Offset: 0x000817F4
	public static Vector3i Max(Vector3i a, Vector3i b)
	{
		return new Vector3i(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0008362E File Offset: 0x0008182E
	public static bool operator ==(Vector3i a, Vector3i b)
	{
		return a.x == b.x && a.y == b.y && a.z == b.z;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0008365C File Offset: 0x0008185C
	public static bool operator !=(Vector3i a, Vector3i b)
	{
		return a.x != b.x || a.y != b.y || a.z != b.z;
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0008368D File Offset: 0x0008188D
	public static Vector3i operator -(Vector3i a, Vector3i b)
	{
		return new Vector3i(a.x - b.x, a.y - b.y, a.z - b.z);
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x000836BB File Offset: 0x000818BB
	public static Vector3i operator +(Vector3i a, Vector3i b)
	{
		return new Vector3i(a.x + b.x, a.y + b.y, a.z + b.z);
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000836E9 File Offset: 0x000818E9
	public static implicit operator Vector3(Vector3i v)
	{
		return new Vector3((float)v.x, (float)v.y, (float)v.z);
	}

	// Token: 0x04000ECF RID: 3791
	public int x;

	// Token: 0x04000ED0 RID: 3792
	public int y;

	// Token: 0x04000ED1 RID: 3793
	public int z;

	// Token: 0x04000ED2 RID: 3794
	public static readonly Vector3i zero = new Vector3i(0, 0, 0);

	// Token: 0x04000ED3 RID: 3795
	public static readonly Vector3i one = new Vector3i(1, 1, 1);

	// Token: 0x04000ED4 RID: 3796
	public static readonly Vector3i forward = new Vector3i(0, 0, 1);

	// Token: 0x04000ED5 RID: 3797
	public static readonly Vector3i back = new Vector3i(0, 0, -1);

	// Token: 0x04000ED6 RID: 3798
	public static readonly Vector3i up = new Vector3i(0, 1, 0);

	// Token: 0x04000ED7 RID: 3799
	public static readonly Vector3i down = new Vector3i(0, -1, 0);

	// Token: 0x04000ED8 RID: 3800
	public static readonly Vector3i left = new Vector3i(-1, 0, 0);

	// Token: 0x04000ED9 RID: 3801
	public static readonly Vector3i right = new Vector3i(1, 0, 0);

	// Token: 0x04000EDA RID: 3802
	public static readonly Vector3i[] directions = new Vector3i[]
	{
		Vector3i.left,
		Vector3i.right,
		Vector3i.back,
		Vector3i.forward,
		Vector3i.down,
		Vector3i.up
	};
}
