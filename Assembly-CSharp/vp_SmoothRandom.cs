using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class vp_SmoothRandom
{
	// Token: 0x0600068B RID: 1675 RVA: 0x0006F134 File Offset: 0x0006D334
	public static Vector3 GetVector3(float speed)
	{
		float x = Time.time * 0.01f * speed;
		return new Vector3(vp_SmoothRandom.Get().HybridMultifractal(x, 15.73f, 0.58f), vp_SmoothRandom.Get().HybridMultifractal(x, 63.94f, 0.58f), vp_SmoothRandom.Get().HybridMultifractal(x, 0.2f, 0.58f));
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0006F194 File Offset: 0x0006D394
	public static Vector3 GetVector3Centered(float speed)
	{
		float x = Time.time * 0.01f * speed;
		float x2 = (Time.time - 1f) * 0.01f * speed;
		Vector3 vector = new Vector3(vp_SmoothRandom.Get().HybridMultifractal(x, 15.73f, 0.58f), vp_SmoothRandom.Get().HybridMultifractal(x, 63.94f, 0.58f), vp_SmoothRandom.Get().HybridMultifractal(x, 0.2f, 0.58f));
		Vector3 vector2;
		vector2..ctor(vp_SmoothRandom.Get().HybridMultifractal(x2, 15.73f, 0.58f), vp_SmoothRandom.Get().HybridMultifractal(x2, 63.94f, 0.58f), vp_SmoothRandom.Get().HybridMultifractal(x2, 0.2f, 0.58f));
		return vector - vector2;
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0006F254 File Offset: 0x0006D454
	public static float Get(float speed)
	{
		float num = Time.time * 0.01f * speed;
		return vp_SmoothRandom.Get().HybridMultifractal(num * 0.01f, 15.7f, 0.65f);
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0006F28A File Offset: 0x0006D48A
	private static vp_FractalNoise Get()
	{
		if (vp_SmoothRandom.s_Noise == null)
		{
			vp_SmoothRandom.s_Noise = new vp_FractalNoise(1.27f, 2.04f, 8.36f);
		}
		return vp_SmoothRandom.s_Noise;
	}

	// Token: 0x04000BB8 RID: 3000
	private static vp_FractalNoise s_Noise;
}
