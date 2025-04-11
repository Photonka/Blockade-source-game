using System;

// Token: 0x02000109 RID: 265
public class List2D<T>
{
	// Token: 0x06000994 RID: 2452 RVA: 0x00082965 File Offset: 0x00080B65
	public List2D()
	{
		this.list = new T[0, 0];
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0008297A File Offset: 0x00080B7A
	public void Set(T obj, Vector2i pos)
	{
		this.Set(obj, pos.x, pos.y);
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0008298F File Offset: 0x00080B8F
	public void Set(T obj, int x, int y)
	{
		this.list[y - this.min.y, x - this.min.x] = obj;
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x000829B7 File Offset: 0x00080BB7
	public T GetInstance(Vector2i pos)
	{
		return this.GetInstance(pos.x, pos.y);
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x000829CC File Offset: 0x00080BCC
	public T GetInstance(int x, int y)
	{
		T t = this.SafeGet(x, y);
		if (object.Equals(t, default(T)))
		{
			t = Activator.CreateInstance<T>();
			this.AddOrReplace(t, x, y);
		}
		return t;
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x00082A0D File Offset: 0x00080C0D
	public T Get(Vector2i pos)
	{
		return this.Get(pos.x, pos.y);
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00082A21 File Offset: 0x00080C21
	public T Get(int x, int y)
	{
		return this.list[y - this.min.y, x - this.min.x];
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00082A48 File Offset: 0x00080C48
	public T SafeGet(Vector2i pos)
	{
		return this.SafeGet(pos.x, pos.y);
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00082A5C File Offset: 0x00080C5C
	public T SafeGet(int x, int y)
	{
		if (!this.IsCorrectIndex(x, y))
		{
			return default(T);
		}
		return this.list[y - this.min.y, x - this.min.x];
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x00082AA4 File Offset: 0x00080CA4
	public void AddOrReplace(T obj, Vector2i pos)
	{
		Vector2i vector2i = Vector2i.Min(this.min, pos);
		Vector2i vector2i2 = Vector2i.Max(this.max, pos + Vector2i.one);
		if (vector2i != this.min || vector2i2 != this.max)
		{
			this.Resize(vector2i, vector2i2);
		}
		this.Set(obj, pos);
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x00082B01 File Offset: 0x00080D01
	public void AddOrReplace(T obj, int x, int y)
	{
		this.AddOrReplace(obj, new Vector2i(x, y));
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x00082B14 File Offset: 0x00080D14
	private void Resize(Vector2i newMin, Vector2i newMax)
	{
		Vector2i vector2i = this.min;
		Vector2i vector2i2 = this.max;
		T[,] array = this.list;
		this.min = newMin;
		this.max = newMax;
		Vector2i vector2i3 = newMax - newMin;
		this.list = new T[vector2i3.y, vector2i3.x];
		for (int i = vector2i.x; i < vector2i2.x; i++)
		{
			for (int j = vector2i.y; j < vector2i2.y; j++)
			{
				T obj = array[j - vector2i.y, i - vector2i.x];
				this.Set(obj, i, j);
			}
		}
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00082BBD File Offset: 0x00080DBD
	public bool IsCorrectIndex(Vector2i pos)
	{
		return this.IsCorrectIndex(pos.x, pos.y);
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00082BD1 File Offset: 0x00080DD1
	private bool IsCorrectIndex(int x, int y)
	{
		return x >= this.min.x && y >= this.min.y && x < this.max.x && y < this.max.y;
	}

	// Token: 0x04000EBB RID: 3771
	private T[,] list;

	// Token: 0x04000EBC RID: 3772
	private Vector2i min;

	// Token: 0x04000EBD RID: 3773
	private Vector2i max;
}
