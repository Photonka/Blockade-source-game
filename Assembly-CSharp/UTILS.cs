using System;
using System.Collections;
using System.Text;
using BestHTTP;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class UTILS
{
	// Token: 0x0600058C RID: 1420 RVA: 0x000432E8 File Offset: 0x000414E8
	public static float ANGLE_SIGNED(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00069E97 File Offset: 0x00068097
	public static IEnumerator PostMyItem(int _type)
	{
		if (string.IsNullOrEmpty(PlayerProfile.screenShotURL))
		{
			yield return null;
		}
		yield return new WaitForEndOfFrame();
		int num = 780;
		int num2 = 524;
		if (_type == 2 || _type == 3)
		{
			num = 800;
			num2 = 600;
		}
		else if (_type == 4)
		{
			num = 400;
			num2 = 500;
		}
		Texture2D texture2D = new Texture2D(num, num2, 3, false);
		texture2D.ReadPixels(new Rect((float)(Screen.width / 2 - num / 2), (float)(Screen.height / 2 - num2 / 2), (float)num, (float)num2), 0, 0);
		texture2D.Apply(false);
		byte[] array = ImageConversion.EncodeToPNG(texture2D);
		WWWForm wwwform = new WWWForm();
		wwwform.AddBinaryData("photo", array);
		WWW w = new WWW(PlayerProfile.screenShotURL, wwwform);
		yield return w;
		if (!string.IsNullOrEmpty(w.error))
		{
			Debug.Log(w.error);
		}
		else if (_type == 1)
		{
			Application.ExternalCall("PostMyItem", new object[]
			{
				w.text
			});
		}
		else if (_type == 2)
		{
			Application.ExternalCall("PostMyGift", new object[]
			{
				w.text
			});
		}
		else if (_type == 3)
		{
			Application.ExternalCall("PostMyBox", new object[]
			{
				w.text
			});
		}
		else if (_type == 4)
		{
			Application.ExternalCall("PostNewLvl", new object[]
			{
				w.text
			});
		}
		yield break;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00069EA6 File Offset: 0x000680A6
	public static void DownloadImage(string url, OnRequestFinishedDelegate _callback)
	{
		if (string.IsNullOrEmpty(url))
		{
			return;
		}
		new HTTPRequest(new Uri(url), _callback).Send();
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00069EC3 File Offset: 0x000680C3
	public static void BEGIN_WRITE(int size)
	{
		UTILS.tmpBuffer = new byte[size];
		UTILS.pos = 0;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00069ED6 File Offset: 0x000680D6
	public static void WRITE_BYTE(byte bvalue)
	{
		UTILS.tmpBuffer[UTILS.pos] = bvalue;
		UTILS.pos++;
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00069EF0 File Offset: 0x000680F0
	public static void WRITE_SHORT(short svalue)
	{
		byte[] array = UTILS.EncodeShort(svalue);
		UTILS.tmpBuffer[UTILS.pos] = array[0];
		UTILS.tmpBuffer[UTILS.pos + 1] = array[1];
		UTILS.pos += 2;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00069F30 File Offset: 0x00068130
	public static void WRITE_FLOAT(float fvalue)
	{
		byte[] array = UTILS.EncodeFloat(fvalue);
		UTILS.tmpBuffer[UTILS.pos] = array[0];
		UTILS.tmpBuffer[UTILS.pos + 1] = array[1];
		UTILS.tmpBuffer[UTILS.pos + 2] = array[2];
		UTILS.tmpBuffer[UTILS.pos + 3] = array[3];
		UTILS.pos += 4;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00069F90 File Offset: 0x00068190
	public static void WRITE_LONG(int ivalue)
	{
		byte[] array = UTILS.EncodeInteger(ivalue);
		UTILS.tmpBuffer[UTILS.pos] = array[0];
		UTILS.tmpBuffer[UTILS.pos + 1] = array[1];
		UTILS.tmpBuffer[UTILS.pos + 2] = array[2];
		UTILS.tmpBuffer[UTILS.pos + 3] = array[3];
		UTILS.pos += 4;
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00069FF0 File Offset: 0x000681F0
	public static void WRITE_STRING(string svalue)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		int byteCount = utf8Encoding.GetByteCount(svalue);
		UTILS.WRITE_LONG(byteCount);
		byte[] array = new byte[byteCount];
		Buffer.BlockCopy(utf8Encoding.GetBytes(svalue), 0, array, 0, byteCount);
		for (int i = 0; i < byteCount; i++)
		{
			UTILS.WRITE_BYTE(array[i]);
		}
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0006A03A File Offset: 0x0006823A
	public static int WRITE_LEN()
	{
		return UTILS.pos;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0006A041 File Offset: 0x00068241
	public static byte[] END_WRITE()
	{
		return UTILS.tmpBuffer;
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0006A048 File Offset: 0x00068248
	public static void BEGIN_READ(byte[] inBytes, int _pos = 0)
	{
		UTILS.tmpBuffer = inBytes;
		UTILS.readlen = UTILS.tmpBuffer.Length;
		UTILS.pos = _pos;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0006A03A File Offset: 0x0006823A
	public static int GET_POS()
	{
		return UTILS.pos;
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0006A062 File Offset: 0x00068262
	public static int GET_LEN()
	{
		return UTILS.readlen;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x0006A06C File Offset: 0x0006826C
	public static byte READ_BYTE()
	{
		if (UTILS.pos >= UTILS.readlen)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"PACKET: ",
				UTILS.tmpBuffer[1],
				" - POS = ",
				UTILS.pos,
				" LEN = ",
				UTILS.readlen
			}));
			UTILS.ERROR = true;
			return 0;
		}
		byte result = UTILS.tmpBuffer[UTILS.pos];
		UTILS.pos++;
		return result;
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0006A0F8 File Offset: 0x000682F8
	public static float READ_ANGLE()
	{
		if (UTILS.pos >= UTILS.readlen)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"PACKET: ",
				UTILS.tmpBuffer[1],
				" - POS = ",
				UTILS.pos,
				" LEN = ",
				UTILS.readlen
			}));
			UTILS.ERROR = true;
			return 0f;
		}
		float result = (float)UTILS.tmpBuffer[UTILS.pos] * 360f / 255f;
		UTILS.pos++;
		return result;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0006A194 File Offset: 0x00068394
	public static float READ_COORD()
	{
		if (UTILS.pos + 2 > UTILS.readlen)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"PACKET: ",
				UTILS.tmpBuffer[1],
				" - POS = ",
				(UTILS.pos + 2).ToString(),
				" LEN = ",
				UTILS.readlen
			}));
			UTILS.ERROR = true;
			return 0f;
		}
		float result = (float)UTILS.DecodeShort(UTILS.tmpBuffer, UTILS.pos) * 0.015873017f;
		UTILS.pos += 2;
		return result;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0006A234 File Offset: 0x00068434
	public static ushort READ_SHORT()
	{
		if (UTILS.pos + 2 > UTILS.readlen)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"PACKET: ",
				UTILS.tmpBuffer[1],
				" - POS = ",
				(UTILS.pos + 2).ToString(),
				" LEN = ",
				UTILS.readlen
			}));
			UTILS.ERROR = true;
			return 0;
		}
		ushort result = UTILS.DecodeShort(UTILS.tmpBuffer, UTILS.pos);
		UTILS.pos += 2;
		return result;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0006A2C8 File Offset: 0x000684C8
	public static int READ_LONG()
	{
		if (UTILS.pos + 4 > UTILS.readlen)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"PACKET: ",
				UTILS.tmpBuffer[1],
				" - POS = ",
				(UTILS.pos + 4).ToString(),
				" LEN = ",
				UTILS.readlen
			}));
			UTILS.ERROR = true;
			return 0;
		}
		int result = UTILS.DecodeInteger(UTILS.tmpBuffer, UTILS.pos);
		UTILS.pos += 4;
		return result;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0006A35C File Offset: 0x0006855C
	public static string READ_STRING()
	{
		int num = 0;
		int index = UTILS.pos;
		while (UTILS.pos < UTILS.readlen && UTILS.tmpBuffer[UTILS.pos] != 0)
		{
			num++;
			UTILS.pos++;
		}
		UTILS.pos++;
		if (num == 0)
		{
			return "";
		}
		return Encoding.UTF8.GetString(UTILS.tmpBuffer, index, num);
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0006A3C2 File Offset: 0x000685C2
	public static byte[] EncodeShort(short inShort)
	{
		return BitConverter.GetBytes(inShort);
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0006A3CA File Offset: 0x000685CA
	public static byte[] EncodeInteger(int inInt)
	{
		return BitConverter.GetBytes(inInt);
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0006A3D2 File Offset: 0x000685D2
	public static byte[] EncodeFloat(float inFloat)
	{
		return BitConverter.GetBytes(inFloat);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0006A3DC File Offset: 0x000685DC
	public static byte[] EncodeStringUTF8(string inString)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		int byteCount = utf8Encoding.GetByteCount(inString);
		byte[] array = new byte[byteCount];
		Buffer.BlockCopy(utf8Encoding.GetBytes(inString), 0, array, 0, byteCount);
		return array;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0006A410 File Offset: 0x00068610
	public static byte[] EncodeStringASCII(string inString)
	{
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		int byteCount = asciiencoding.GetByteCount(inString);
		byte[] array = new byte[byteCount];
		Buffer.BlockCopy(asciiencoding.GetBytes(inString), 0, array, 0, byteCount);
		return array;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x0006A444 File Offset: 0x00068644
	public static byte[] EncodeVector2(Vector2 inObject)
	{
		byte[] array = new byte[8];
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.x), 0, array, 0, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.y), 0, array, 4, 4);
		return array;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0006A484 File Offset: 0x00068684
	public static byte[] EncodeVector3(Vector3 inObject)
	{
		byte[] array = new byte[12];
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.x), 0, array, 0, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.y), 0, array, 4, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.z), 0, array, 8, 4);
		return array;
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0006A4D8 File Offset: 0x000686D8
	public static byte[] EncodeVector4(Vector4 inObject)
	{
		byte[] array = new byte[16];
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.x), 0, array, 0, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.y), 0, array, 4, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.z), 0, array, 8, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.w), 0, array, 12, 4);
		return array;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0006A540 File Offset: 0x00068740
	public static byte[] EncodeQuaternion(Quaternion inObject)
	{
		byte[] array = new byte[16];
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.x), 0, array, 0, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.y), 0, array, 4, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.z), 0, array, 8, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.w), 0, array, 12, 4);
		return array;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0006A5A8 File Offset: 0x000687A8
	public static byte[] EncodeColor(Color inObject)
	{
		byte[] array = new byte[16];
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.r), 0, array, 0, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.g), 0, array, 4, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.b), 0, array, 8, 4);
		Buffer.BlockCopy(BitConverter.GetBytes(inObject.a), 0, array, 12, 4);
		return array;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0006A60F File Offset: 0x0006880F
	public static ushort DecodeShort(byte[] inBytes, int pos)
	{
		return BitConverter.ToUInt16(inBytes, pos);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0006A618 File Offset: 0x00068818
	public static int DecodeInteger(byte[] inBytes, int pos)
	{
		return BitConverter.ToInt32(inBytes, pos);
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0006A621 File Offset: 0x00068821
	public static string DecodeString(byte[] inBytes)
	{
		return Encoding.UTF8.GetString(inBytes);
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0006A62E File Offset: 0x0006882E
	public static float XRES(float val)
	{
		return val * ((float)Screen.width / 1024f);
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0006A63E File Offset: 0x0006883E
	public static float YRES(float val)
	{
		return val * ((float)Screen.height / 768f);
	}

	// Token: 0x04000B69 RID: 2921
	private static byte[] tmpBuffer;

	// Token: 0x04000B6A RID: 2922
	private static int pos;

	// Token: 0x04000B6B RID: 2923
	private static int readlen;

	// Token: 0x04000B6C RID: 2924
	public static bool ERROR;
}
