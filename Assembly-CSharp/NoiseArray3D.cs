using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
internal class NoiseArray3D
{
	// Token: 0x06000A05 RID: 2565 RVA: 0x000842E0 File Offset: 0x000824E0
	public NoiseArray3D(float scale)
	{
		this.noise = new PerlinNoise3D(scale);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00084305 File Offset: 0x00082505
	public void GenerateNoise(int offsetX, int offsetY, int offsetZ)
	{
		this.GenerateNoise(new Vector3i(offsetX, offsetY, offsetZ));
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00084318 File Offset: 0x00082518
	public void GenerateNoise(Vector3i offset)
	{
		this.offset = offset;
		int num = 10;
		int num2 = 10;
		int num3 = 10;
		for (int i = 0; i < num; i += 4)
		{
			for (int j = 0; j < num2; j += 4)
			{
				for (int k = 0; k < num3; k += 4)
				{
					Vector3i vector3i = new Vector3i(i, j, k) + offset;
					Vector3i vector3i2 = vector3i + new Vector3i(4, 4, 4);
					float num4 = this.noise.Noise((float)vector3i.x, (float)vector3i.y, (float)vector3i.z);
					float num5 = this.noise.Noise((float)vector3i2.x, (float)vector3i.y, (float)vector3i.z);
					float num6 = this.noise.Noise((float)vector3i.x, (float)vector3i2.y, (float)vector3i.z);
					float num7 = this.noise.Noise((float)vector3i2.x, (float)vector3i2.y, (float)vector3i.z);
					float num8 = this.noise.Noise((float)vector3i.x, (float)vector3i.y, (float)vector3i2.z);
					float num9 = this.noise.Noise((float)vector3i2.x, (float)vector3i.y, (float)vector3i2.z);
					float num10 = this.noise.Noise((float)vector3i.x, (float)vector3i2.y, (float)vector3i2.z);
					float num11 = this.noise.Noise((float)vector3i2.x, (float)vector3i2.y, (float)vector3i2.z);
					int num12 = 0;
					while (num12 < 4 && i + num12 < num)
					{
						int num13 = 0;
						while (num13 < 4 && j + num13 < num2)
						{
							int num14 = 0;
							while (num14 < 4 && k + num14 < num3)
							{
								float num15 = (float)num12 / 4f;
								float num16 = (float)num13 / 4f;
								float num17 = (float)num14 / 4f;
								float num18 = Mathf.Lerp(num4, num5, num15);
								float num19 = Mathf.Lerp(num6, num7, num15);
								float num20 = Mathf.Lerp(num18, num19, num16);
								float num21 = Mathf.Lerp(num8, num9, num15);
								float num22 = Mathf.Lerp(num10, num11, num15);
								float num23 = Mathf.Lerp(num21, num22, num16);
								float num24 = Mathf.Lerp(num20, num23, num17);
								this.map[i + num12, j + num13, k + num14] = num24;
								num14++;
							}
							num13++;
						}
						num12++;
					}
				}
			}
		}
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x000845A2 File Offset: 0x000827A2
	public float GetNoise(Vector3i pos)
	{
		return this.GetNoise(pos.x, pos.y, pos.z);
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x000845BC File Offset: 0x000827BC
	public float GetNoise(int x, int y, int z)
	{
		x -= this.offset.x;
		y -= this.offset.y;
		z -= this.offset.z;
		return this.map[x + 1, y + 1, z + 1];
	}

	// Token: 0x04000EF1 RID: 3825
	private const int step = 4;

	// Token: 0x04000EF2 RID: 3826
	private PerlinNoise3D noise;

	// Token: 0x04000EF3 RID: 3827
	private float[,,] map = new float[10, 10, 10];

	// Token: 0x04000EF4 RID: 3828
	private Vector3i offset;
}
