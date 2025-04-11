using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020000CB RID: 203
public static class vp_Utility
{
	// Token: 0x060006D3 RID: 1747 RVA: 0x00070AC1 File Offset: 0x0006ECC1
	public static float NaNSafeFloat(float value, float prevValue = 0f)
	{
		value = (double.IsNaN((double)value) ? prevValue : value);
		return value;
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00070AD4 File Offset: 0x0006ECD4
	public static Vector2 NaNSafeVector2(Vector2 vector, Vector2 prevVector = default(Vector2))
	{
		vector.x = (double.IsNaN((double)vector.x) ? prevVector.x : vector.x);
		vector.y = (double.IsNaN((double)vector.y) ? prevVector.y : vector.y);
		return vector;
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00070B28 File Offset: 0x0006ED28
	public static Vector3 NaNSafeVector3(Vector3 vector, Vector3 prevVector = default(Vector3))
	{
		vector.x = (double.IsNaN((double)vector.x) ? prevVector.x : vector.x);
		vector.y = (double.IsNaN((double)vector.y) ? prevVector.y : vector.y);
		vector.z = (double.IsNaN((double)vector.z) ? prevVector.z : vector.z);
		return vector;
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00070BA0 File Offset: 0x0006EDA0
	public static Quaternion NaNSafeQuaternion(Quaternion quaternion, Quaternion prevQuaternion = default(Quaternion))
	{
		quaternion.x = (double.IsNaN((double)quaternion.x) ? prevQuaternion.x : quaternion.x);
		quaternion.y = (double.IsNaN((double)quaternion.y) ? prevQuaternion.y : quaternion.y);
		quaternion.z = (double.IsNaN((double)quaternion.z) ? prevQuaternion.z : quaternion.z);
		quaternion.w = (double.IsNaN((double)quaternion.w) ? prevQuaternion.w : quaternion.w);
		return quaternion;
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00070C3C File Offset: 0x0006EE3C
	public static Vector3 SnapToZero(Vector3 value, float epsilon = 0.0001f)
	{
		value.x = ((Mathf.Abs(value.x) < epsilon) ? 0f : value.x);
		value.y = ((Mathf.Abs(value.y) < epsilon) ? 0f : value.y);
		value.z = ((Mathf.Abs(value.z) < epsilon) ? 0f : value.z);
		return value;
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00070CB0 File Offset: 0x0006EEB0
	public static Vector3 HorizontalVector(Vector3 value)
	{
		value.y = 0f;
		return value;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00070CC0 File Offset: 0x0006EEC0
	public static string GetErrorLocation(int level = 1)
	{
		StackTrace stackTrace = new StackTrace();
		string text = "";
		string text2 = "";
		for (int i = stackTrace.FrameCount - 1; i > level; i--)
		{
			if (i < stackTrace.FrameCount - 1)
			{
				text += " --> ";
			}
			StackFrame frame = stackTrace.GetFrame(i);
			if (frame.GetMethod().DeclaringType.ToString() == text2)
			{
				text = "";
			}
			text2 = frame.GetMethod().DeclaringType.ToString();
			text = text + text2 + ":" + frame.GetMethod().Name;
		}
		return text;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00070D60 File Offset: 0x0006EF60
	public static string GetTypeAlias(Type type)
	{
		string result = "";
		if (!vp_Utility.m_TypeAliases.TryGetValue(type, out result))
		{
			return type.ToString();
		}
		return result;
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00070D8A File Offset: 0x0006EF8A
	public static void Activate(GameObject obj, bool activate = true)
	{
		obj.SetActiveRecursively(activate);
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00070D93 File Offset: 0x0006EF93
	public static bool IsActive(GameObject obj)
	{
		return obj.active;
	}

	// Token: 0x04000BF0 RID: 3056
	private static readonly Dictionary<Type, string> m_TypeAliases = new Dictionary<Type, string>
	{
		{
			typeof(void),
			"void"
		},
		{
			typeof(byte),
			"byte"
		},
		{
			typeof(sbyte),
			"sbyte"
		},
		{
			typeof(short),
			"short"
		},
		{
			typeof(ushort),
			"ushort"
		},
		{
			typeof(int),
			"int"
		},
		{
			typeof(uint),
			"uint"
		},
		{
			typeof(long),
			"long"
		},
		{
			typeof(ulong),
			"ulong"
		},
		{
			typeof(float),
			"float"
		},
		{
			typeof(double),
			"double"
		},
		{
			typeof(decimal),
			"decimal"
		},
		{
			typeof(object),
			"object"
		},
		{
			typeof(bool),
			"bool"
		},
		{
			typeof(char),
			"char"
		},
		{
			typeof(string),
			"string"
		},
		{
			typeof(Vector2),
			"Vector2"
		},
		{
			typeof(Vector3),
			"Vector3"
		},
		{
			typeof(Vector4),
			"Vector4"
		}
	};
}
