using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class PerlinNoise2D
{
	// Token: 0x060009F4 RID: 2548 RVA: 0x00083D18 File Offset: 0x00081F18
	public PerlinNoise2D(float scale)
	{
		this.scale = scale;
		this.offset = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00083D78 File Offset: 0x00081F78
	public PerlinNoise2D SetPersistence(float persistence)
	{
		this.persistence = persistence;
		return this;
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00083D82 File Offset: 0x00081F82
	public PerlinNoise2D SetOctaves(int octaves)
	{
		this.octaves = octaves;
		return this;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00083D8C File Offset: 0x00081F8C
	public float Noise(float x, float y)
	{
		x = x * this.scale + this.offset.x;
		y = y * this.scale + this.offset.y;
		float num = 0f;
		float num2 = 1f;
		float num3 = 1f;
		for (int i = 0; i < this.octaves; i++)
		{
			if (i >= 1)
			{
				num2 *= 2f;
				num3 *= this.persistence;
			}
			num += Mathf.PerlinNoise(x * num2, y * num2) * num3;
		}
		return num;
	}

	// Token: 0x04000EE5 RID: 3813
	private float scale;

	// Token: 0x04000EE6 RID: 3814
	private Vector2 offset = Vector2.zero;

	// Token: 0x04000EE7 RID: 3815
	private float persistence = 0.5f;

	// Token: 0x04000EE8 RID: 3816
	private int octaves = 5;
}
