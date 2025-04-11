using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public struct Vector2i
{
	// Token: 0x060009D2 RID: 2514 RVA: 0x00083285 File Offset: 0x00081485
	public Vector2i(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00083295 File Offset: 0x00081495
	public override int GetHashCode()
	{
		return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x000832B0 File Offset: 0x000814B0
	public override bool Equals(object other)
	{
		if (!(other is Vector2i))
		{
			return false;
		}
		Vector2i vector2i = (Vector2i)other;
		return this.x == vector2i.x && this.y == vector2i.y;
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x000832EC File Offset: 0x000814EC
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"Vector2i(",
			this.x,
			" ",
			this.y,
			")"
		});
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x00083338 File Offset: 0x00081538
	public static Vector2i Min(Vector2i a, Vector2i b)
	{
		return new Vector2i(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y));
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00083361 File Offset: 0x00081561
	public static Vector2i Max(Vector2i a, Vector2i b)
	{
		return new Vector2i(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0008338A File Offset: 0x0008158A
	public static bool operator ==(Vector2i a, Vector2i b)
	{
		return a.x == b.x && a.y == b.y;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x000833AA File Offset: 0x000815AA
	public static bool operator !=(Vector2i a, Vector2i b)
	{
		return a.x != b.x || a.y != b.y;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x000833CD File Offset: 0x000815CD
	public static Vector2i operator -(Vector2i a, Vector2i b)
	{
		return new Vector2i(a.x - b.x, a.y - b.y);
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x000833EE File Offset: 0x000815EE
	public static Vector2i operator +(Vector2i a, Vector2i b)
	{
		return new Vector2i(a.x + b.x, a.y + b.y);
	}

	// Token: 0x04000EC7 RID: 3783
	public int x;

	// Token: 0x04000EC8 RID: 3784
	public int y;

	// Token: 0x04000EC9 RID: 3785
	public static readonly Vector2i zero = new Vector2i(0, 0);

	// Token: 0x04000ECA RID: 3786
	public static readonly Vector2i one = new Vector2i(1, 1);

	// Token: 0x04000ECB RID: 3787
	public static readonly Vector2i up = new Vector2i(0, 1);

	// Token: 0x04000ECC RID: 3788
	public static readonly Vector2i down = new Vector2i(0, -1);

	// Token: 0x04000ECD RID: 3789
	public static readonly Vector2i left = new Vector2i(-1, 0);

	// Token: 0x04000ECE RID: 3790
	public static readonly Vector2i right = new Vector2i(1, 0);
}
