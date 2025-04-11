using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class PerlinNoise3D
{
	// Token: 0x060009FE RID: 2558 RVA: 0x00083FF4 File Offset: 0x000821F4
	public PerlinNoise3D(float scale)
	{
		this.scale = scale;
		for (int i = 0; i < 256; i++)
		{
			float num = 1f - 2f * Random.value;
			float num2 = Mathf.Sqrt(1f - num * num);
			float num3 = 6.2831855f * Random.value;
			this.gradients[i].x = num2 * Mathf.Cos(num3);
			this.gradients[i].y = num2 * Mathf.Sin(num3);
			this.gradients[i].z = num;
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x000840C5 File Offset: 0x000822C5
	public float Noise(float x, float y, float z)
	{
		return this.PerlinNoise(x * this.scale, y * this.scale, z * this.scale) + 0.5f;
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x000840EC File Offset: 0x000822EC
	private float PerlinNoise(float x, float y, float z)
	{
		int num = (int)Mathf.Floor(x);
		float num2 = x - (float)num;
		float fx = num2 - 1f;
		float num3 = PerlinNoise3D.Smooth(num2);
		int num4 = (int)Mathf.Floor(y);
		float num5 = y - (float)num4;
		float fy = num5 - 1f;
		float num6 = PerlinNoise3D.Smooth(num5);
		int num7 = (int)Mathf.Floor(z);
		float num8 = z - (float)num7;
		float fz = num8 - 1f;
		float num9 = PerlinNoise3D.Smooth(num8);
		float num10 = this.Lattice(num, num4, num7, num2, num5, num8);
		float num11 = this.Lattice(num + 1, num4, num7, fx, num5, num8);
		float num12 = Mathf.Lerp(num10, num11, num3);
		float num13 = this.Lattice(num, num4 + 1, num7, num2, fy, num8);
		num11 = this.Lattice(num + 1, num4 + 1, num7, fx, fy, num8);
		float num14 = Mathf.Lerp(num13, num11, num3);
		float num15 = Mathf.Lerp(num12, num14, num6);
		float num16 = this.Lattice(num, num4, num7 + 1, num2, num5, fz);
		num11 = this.Lattice(num + 1, num4, num7 + 1, fx, num5, fz);
		float num17 = Mathf.Lerp(num16, num11, num3);
		float num18 = this.Lattice(num, num4 + 1, num7 + 1, num2, fy, fz);
		num11 = this.Lattice(num + 1, num4 + 1, num7 + 1, fx, fy, fz);
		num14 = Mathf.Lerp(num18, num11, num3);
		float num19 = Mathf.Lerp(num17, num14, num6);
		return Mathf.Lerp(num15, num19, num9);
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0008423C File Offset: 0x0008243C
	private int Index(int ix, int iy, int iz)
	{
		return this.Permutate(ix + this.Permutate(iy + this.Permutate(iz)));
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00084258 File Offset: 0x00082458
	private int Permutate(int x)
	{
		int num = 255;
		return (int)this.perm[x & num];
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00084278 File Offset: 0x00082478
	private float Lattice(int ix, int iy, int iz, float fx, float fy, float fz)
	{
		int num = this.Index(ix, iy, iz);
		return this.gradients[num].x * fx + this.gradients[num].y * fy + this.gradients[num].z * fz;
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x000842CD File Offset: 0x000824CD
	private static float Smooth(float x)
	{
		return x * x * (3f - 2f * x);
	}

	// Token: 0x04000EED RID: 3821
	private const int GradientSizeTable = 256;

	// Token: 0x04000EEE RID: 3822
	private Vector3[] gradients = new Vector3[256];

	// Token: 0x04000EEF RID: 3823
	private short[] perm = new short[]
	{
		225,
		155,
		210,
		108,
		175,
		199,
		221,
		144,
		203,
		116,
		70,
		213,
		69,
		158,
		33,
		252,
		5,
		82,
		173,
		133,
		222,
		139,
		174,
		27,
		9,
		71,
		90,
		246,
		75,
		130,
		91,
		191,
		169,
		138,
		2,
		151,
		194,
		235,
		81,
		7,
		25,
		113,
		228,
		159,
		205,
		253,
		134,
		142,
		248,
		65,
		224,
		217,
		22,
		121,
		229,
		63,
		89,
		103,
		96,
		104,
		156,
		17,
		201,
		129,
		36,
		8,
		165,
		110,
		237,
		117,
		231,
		56,
		132,
		211,
		152,
		20,
		181,
		111,
		239,
		218,
		170,
		163,
		51,
		172,
		157,
		47,
		80,
		212,
		176,
		250,
		87,
		49,
		99,
		242,
		136,
		189,
		162,
		115,
		44,
		43,
		124,
		94,
		150,
		16,
		141,
		247,
		32,
		10,
		198,
		223,
		255,
		72,
		53,
		131,
		84,
		57,
		220,
		197,
		58,
		50,
		208,
		11,
		241,
		28,
		3,
		192,
		62,
		202,
		18,
		215,
		153,
		24,
		76,
		41,
		15,
		179,
		39,
		46,
		55,
		6,
		128,
		167,
		23,
		188,
		106,
		34,
		187,
		140,
		164,
		73,
		112,
		182,
		244,
		195,
		227,
		13,
		35,
		77,
		196,
		185,
		26,
		200,
		226,
		119,
		31,
		123,
		168,
		125,
		249,
		68,
		183,
		230,
		177,
		135,
		160,
		180,
		12,
		1,
		243,
		148,
		102,
		166,
		38,
		238,
		251,
		37,
		240,
		126,
		64,
		74,
		161,
		40,
		184,
		149,
		171,
		178,
		101,
		66,
		29,
		59,
		146,
		61,
		254,
		107,
		42,
		86,
		154,
		4,
		236,
		232,
		120,
		21,
		233,
		209,
		45,
		98,
		193,
		114,
		78,
		19,
		206,
		14,
		118,
		127,
		48,
		79,
		147,
		85,
		30,
		207,
		219,
		54,
		88,
		234,
		190,
		122,
		95,
		67,
		143,
		109,
		137,
		214,
		145,
		93,
		92,
		100,
		245,
		0,
		216,
		186,
		60,
		83,
		105,
		97,
		204,
		52
	};

	// Token: 0x04000EF0 RID: 3824
	private float scale = 1f;
}
