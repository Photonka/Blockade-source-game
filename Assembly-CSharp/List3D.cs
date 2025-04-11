using System;

// Token: 0x0200010A RID: 266
public class List3D<T>
{
	// Token: 0x060009A2 RID: 2466 RVA: 0x00082C10 File Offset: 0x00080E10
	public List3D()
	{
		this.list = new T[0, 0, 0];
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00082C28 File Offset: 0x00080E28
	public List3D(Vector3i min, Vector3i max)
	{
		this.min = min;
		this.max = max;
		Vector3i size = this.GetSize();
		this.list = new T[size.z, size.y, size.x];
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00082C6D File Offset: 0x00080E6D
	public void Set(T obj, Vector3i pos)
	{
		this.Set(obj, pos.x, pos.y, pos.z);
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00082C88 File Offset: 0x00080E88
	public void Set(T obj, int x, int y, int z)
	{
		this.list[z - this.min.z, y - this.min.y, x - this.min.x] = obj;
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x00082CBE File Offset: 0x00080EBE
	public T GetInstance(Vector3i pos)
	{
		return this.GetInstance(pos.x, pos.y, pos.z);
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x00082CD8 File Offset: 0x00080ED8
	public T GetInstance(int x, int y, int z)
	{
		T t = this.SafeGet(x, y, z);
		if (object.Equals(t, default(T)))
		{
			t = Activator.CreateInstance<T>();
			this.AddOrReplace(t, x, y, z);
		}
		return t;
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00082D1B File Offset: 0x00080F1B
	public T Get(Vector3i pos)
	{
		return this.Get(pos.x, pos.y, pos.z);
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00082D35 File Offset: 0x00080F35
	public T Get(int x, int y, int z)
	{
		return this.list[z - this.min.z, y - this.min.y, x - this.min.x];
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00082D69 File Offset: 0x00080F69
	public T SafeGet(Vector3i pos)
	{
		return this.SafeGet(pos.x, pos.y, pos.z);
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x00082D84 File Offset: 0x00080F84
	public T SafeGet(int x, int y, int z)
	{
		if (!this.IsCorrectIndex(x, y, z))
		{
			return default(T);
		}
		return this.Get(x, y, z);
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00082DB0 File Offset: 0x00080FB0
	public void AddOrReplace(T obj, Vector3i pos)
	{
		Vector3i vector3i = Vector3i.Min(this.min, pos);
		Vector3i vector3i2 = Vector3i.Max(this.max, pos + Vector3i.one);
		if (vector3i != this.min || vector3i2 != this.max)
		{
			this.Resize(vector3i, vector3i2);
		}
		this.Set(obj, pos);
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x00082E0D File Offset: 0x0008100D
	public void AddOrReplace(T obj, int x, int y, int z)
	{
		this.AddOrReplace(obj, new Vector3i(x, y, z));
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x00082E20 File Offset: 0x00081020
	private void Resize(Vector3i newMin, Vector3i newMax)
	{
		Vector3i vector3i = this.min;
		Vector3i vector3i2 = this.max;
		T[,,] array = this.list;
		this.min = newMin;
		this.max = newMax;
		Vector3i vector3i3 = newMax - newMin;
		this.list = new T[vector3i3.z, vector3i3.y, vector3i3.x];
		for (int i = vector3i.x; i < vector3i2.x; i++)
		{
			for (int j = vector3i.y; j < vector3i2.y; j++)
			{
				for (int k = vector3i.z; k < vector3i2.z; k++)
				{
					T obj = array[k - vector3i.z, j - vector3i.y, i - vector3i.x];
					this.Set(obj, i, j, k);
				}
			}
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x00082EF4 File Offset: 0x000810F4
	public bool IsCorrectIndex(Vector3i pos)
	{
		return this.IsCorrectIndex(pos.x, pos.y, pos.z);
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00082F10 File Offset: 0x00081110
	public bool IsCorrectIndex(int x, int y, int z)
	{
		return x >= this.min.x && y >= this.min.y && z >= this.min.z && x < this.max.x && y < this.max.y && z < this.max.z;
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00082F76 File Offset: 0x00081176
	public Vector3i GetMin()
	{
		return this.min;
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x00082F7E File Offset: 0x0008117E
	public Vector3i GetMax()
	{
		return this.max;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x00082F86 File Offset: 0x00081186
	public Vector3i GetSize()
	{
		return this.max - this.min;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x00082F99 File Offset: 0x00081199
	public int GetMinX()
	{
		return this.min.x;
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00082FA6 File Offset: 0x000811A6
	public int GetMinY()
	{
		return this.min.y;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00082FB3 File Offset: 0x000811B3
	public int GetMinZ()
	{
		return this.min.z;
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x00082FC0 File Offset: 0x000811C0
	public int GetMaxX()
	{
		return this.max.x;
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00082FCD File Offset: 0x000811CD
	public int GetMaxY()
	{
		return this.max.y;
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00082FDA File Offset: 0x000811DA
	public int GetMaxZ()
	{
		return this.max.z;
	}

	// Token: 0x04000EBE RID: 3774
	private T[,,] list;

	// Token: 0x04000EBF RID: 3775
	private Vector3i min;

	// Token: 0x04000EC0 RID: 3776
	private Vector3i max;
}
