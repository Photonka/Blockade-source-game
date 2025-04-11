using System;
using UnityEngine;

// Token: 0x020000CA RID: 202
public static class vp_TimeUtility
{
	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060006CC RID: 1740 RVA: 0x00070944 File Offset: 0x0006EB44
	// (set) Token: 0x060006CD RID: 1741 RVA: 0x0007094B File Offset: 0x0006EB4B
	public static float TimeScale
	{
		get
		{
			return Time.timeScale;
		}
		set
		{
			value = vp_TimeUtility.ClampTimeScale(value);
			Time.timeScale = value;
			Time.fixedDeltaTime = vp_TimeUtility.InitialFixedTimeStep * Time.timeScale;
		}
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0007096C File Offset: 0x0006EB6C
	public static void FadeTimeScale(float targetTimeScale, float fadeSpeed)
	{
		if (vp_TimeUtility.TimeScale == targetTimeScale)
		{
			return;
		}
		targetTimeScale = vp_TimeUtility.ClampTimeScale(targetTimeScale);
		vp_TimeUtility.TimeScale = Mathf.Lerp(vp_TimeUtility.TimeScale, targetTimeScale, Time.deltaTime * 60f * fadeSpeed);
		if (Mathf.Abs(vp_TimeUtility.TimeScale - targetTimeScale) < 0.01f)
		{
			vp_TimeUtility.TimeScale = targetTimeScale;
		}
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x000709C0 File Offset: 0x0006EBC0
	private static float ClampTimeScale(float t)
	{
		if (t < vp_TimeUtility.m_MinTimeScale || t > vp_TimeUtility.m_MaxTimeScale)
		{
			t = Mathf.Clamp(t, vp_TimeUtility.m_MinTimeScale, vp_TimeUtility.m_MaxTimeScale);
			Debug.LogWarning(string.Concat(new object[]
			{
				"Warning: (vp_TimeUtility) TimeScale was clamped to within the supported range (",
				vp_TimeUtility.m_MinTimeScale,
				" - ",
				vp_TimeUtility.m_MaxTimeScale,
				")."
			}));
		}
		return t;
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00070A32 File Offset: 0x0006EC32
	// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00070A3C File Offset: 0x0006EC3C
	public static bool Paused
	{
		get
		{
			return vp_TimeUtility.m_Paused;
		}
		set
		{
			if (value)
			{
				if (vp_TimeUtility.m_Paused)
				{
					return;
				}
				vp_TimeUtility.m_Paused = true;
				vp_TimeUtility.m_TimeScaleOnPause = Time.timeScale;
				Time.timeScale = 0f;
				return;
			}
			else
			{
				if (!vp_TimeUtility.m_Paused)
				{
					return;
				}
				vp_TimeUtility.m_Paused = false;
				Time.timeScale = vp_TimeUtility.m_TimeScaleOnPause;
				vp_TimeUtility.m_TimeScaleOnPause = 1f;
				return;
			}
		}
	}

	// Token: 0x04000BEB RID: 3051
	private static float m_MinTimeScale = 0.1f;

	// Token: 0x04000BEC RID: 3052
	private static float m_MaxTimeScale = 1f;

	// Token: 0x04000BED RID: 3053
	private static bool m_Paused = false;

	// Token: 0x04000BEE RID: 3054
	private static float m_TimeScaleOnPause = 1f;

	// Token: 0x04000BEF RID: 3055
	public static float InitialFixedTimeStep = Time.fixedDeltaTime;
}
