using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class NoiseArray2D
{
	// Token: 0x060009F8 RID: 2552 RVA: 0x00083E0E File Offset: 0x0008200E
	public NoiseArray2D(float scale)
	{
		this.noise = new PerlinNoise2D(scale);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00083E31 File Offset: 0x00082031
	public NoiseArray2D SetPersistence(float persistence)
	{
		this.noise.SetPersistence(persistence);
		return this;
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00083E41 File Offset: 0x00082041
	public NoiseArray2D SetOctaves(int octaves)
	{
		this.noise.SetOctaves(octaves);
		return this;
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x00083E51 File Offset: 0x00082051
	public void GenerateNoise(int offsetX, int offsetY)
	{
		this.GenerateNoise(new Vector2i(offsetX, offsetY));
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x00083E60 File Offset: 0x00082060
	public void GenerateNoise(Vector2i offset)
	{
		this.offset = offset;
		int length = this.map.GetLength(0);
		int length2 = this.map.GetLength(1);
		for (int i = 0; i < length; i += 4)
		{
			for (int j = 0; j < length2; j += 4)
			{
				Vector2i vector2i = new Vector2i(i, j) + offset;
				Vector2i vector2i2 = vector2i + new Vector2i(4, 4);
				float num = this.noise.Noise((float)vector2i.x, (float)vector2i.y);
				float num2 = this.noise.Noise((float)vector2i2.x, (float)vector2i.y);
				float num3 = this.noise.Noise((float)vector2i.x, (float)vector2i2.y);
				float num4 = this.noise.Noise((float)vector2i2.x, (float)vector2i2.y);
				int num5 = 0;
				while (num5 < 4 && i + num5 < length)
				{
					int num6 = 0;
					while (num6 < 4 && j + num6 < length2)
					{
						float num7 = (float)num5 / 4f;
						float num8 = (float)num6 / 4f;
						float num9 = Mathf.Lerp(num, num2, num7);
						float num10 = Mathf.Lerp(num3, num4, num7);
						this.map[i + num5, j + num6] = Mathf.Lerp(num9, num10, num8);
						num6++;
					}
					num5++;
				}
			}
		}
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00083FC1 File Offset: 0x000821C1
	public float GetNoise(int x, int y)
	{
		x -= this.offset.x;
		y -= this.offset.y;
		return this.map[x + 1, y + 1];
	}

	// Token: 0x04000EE9 RID: 3817
	private const int step = 4;

	// Token: 0x04000EEA RID: 3818
	private PerlinNoise2D noise;

	// Token: 0x04000EEB RID: 3819
	private float[,] map = new float[10, 10];

	// Token: 0x04000EEC RID: 3820
	private Vector2i offset;
}
