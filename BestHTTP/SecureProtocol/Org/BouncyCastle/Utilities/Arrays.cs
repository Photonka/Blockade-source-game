using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities
{
	// Token: 0x0200024A RID: 586
	public abstract class Arrays
	{
		// Token: 0x060015B3 RID: 5555 RVA: 0x000B17A0 File Offset: 0x000AF9A0
		public static bool AreAllZeroes(byte[] buf, int off, int len)
		{
			uint num = 0U;
			for (int i = 0; i < len; i++)
			{
				num |= (uint)buf[off + i];
			}
			return num == 0U;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000B17C7 File Offset: 0x000AF9C7
		public static bool AreEqual(bool[] a, bool[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000B17DE File Offset: 0x000AF9DE
		public static bool AreEqual(char[] a, char[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000B17F5 File Offset: 0x000AF9F5
		public static bool AreEqual(byte[] a, byte[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x000B180C File Offset: 0x000AFA0C
		[Obsolete("Use 'AreEqual' method instead")]
		public static bool AreSame(byte[] a, byte[] b)
		{
			return Arrays.AreEqual(a, b);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000B1818 File Offset: 0x000AFA18
		public static bool ConstantTimeAreEqual(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			int num2 = 0;
			while (num != 0)
			{
				num--;
				num2 |= (int)(a[num] ^ b[num]);
			}
			return num2 == 0;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000B184A File Offset: 0x000AFA4A
		public static bool AreEqual(int[] a, int[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x000B1861 File Offset: 0x000AFA61
		[CLSCompliant(false)]
		public static bool AreEqual(uint[] a, uint[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x000B1878 File Offset: 0x000AFA78
		private static bool HaveSameContents(bool[] a, bool[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x000B18A4 File Offset: 0x000AFAA4
		private static bool HaveSameContents(char[] a, char[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000B18D0 File Offset: 0x000AFAD0
		private static bool HaveSameContents(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000B18FC File Offset: 0x000AFAFC
		private static bool HaveSameContents(int[] a, int[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000B1928 File Offset: 0x000AFB28
		private static bool HaveSameContents(uint[] a, uint[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000B1954 File Offset: 0x000AFB54
		public static string ToString(object[] a)
		{
			StringBuilder stringBuilder = new StringBuilder(91);
			if (a.Length != 0)
			{
				stringBuilder.Append(a[0]);
				for (int i = 1; i < a.Length; i++)
				{
					stringBuilder.Append(", ").Append(a[i]);
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000B19A8 File Offset: 0x000AFBA8
		public static int GetHashCode(byte[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[num];
			}
			return num2;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000B19DC File Offset: 0x000AFBDC
		public static int GetHashCode(byte[] data, int off, int len)
		{
			if (data == null)
			{
				return 0;
			}
			int num = len;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[off + num];
			}
			return num2;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000B1A10 File Offset: 0x000AFC10
		public static int GetHashCode(int[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= data[num];
			}
			return num2;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x000B1A44 File Offset: 0x000AFC44
		public static int GetHashCode(int[] data, int off, int len)
		{
			if (data == null)
			{
				return 0;
			}
			int num = len;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= data[off + num];
			}
			return num2;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x000B1A78 File Offset: 0x000AFC78
		[CLSCompliant(false)]
		public static int GetHashCode(uint[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[num];
			}
			return num2;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000B1AAC File Offset: 0x000AFCAC
		[CLSCompliant(false)]
		public static int GetHashCode(uint[] data, int off, int len)
		{
			if (data == null)
			{
				return 0;
			}
			int num = len;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[off + num];
			}
			return num2;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000B1AE0 File Offset: 0x000AFCE0
		[CLSCompliant(false)]
		public static int GetHashCode(ulong[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				ulong num3 = data[num];
				num2 *= 257;
				num2 ^= (int)num3;
				num2 *= 257;
				num2 ^= (int)(num3 >> 32);
			}
			return num2;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000B1B28 File Offset: 0x000AFD28
		[CLSCompliant(false)]
		public static int GetHashCode(ulong[] data, int off, int len)
		{
			if (data == null)
			{
				return 0;
			}
			int num = len;
			int num2 = num + 1;
			while (--num >= 0)
			{
				ulong num3 = data[off + num];
				num2 *= 257;
				num2 ^= (int)num3;
				num2 *= 257;
				num2 ^= (int)(num3 >> 32);
			}
			return num2;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000B1B6E File Offset: 0x000AFD6E
		public static byte[] Clone(byte[] data)
		{
			if (data != null)
			{
				return (byte[])data.Clone();
			}
			return null;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000B1B80 File Offset: 0x000AFD80
		public static byte[] Clone(byte[] data, byte[] existing)
		{
			if (data == null)
			{
				return null;
			}
			if (existing == null || existing.Length != data.Length)
			{
				return Arrays.Clone(data);
			}
			Array.Copy(data, 0, existing, 0, existing.Length);
			return existing;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x000B1BA6 File Offset: 0x000AFDA6
		public static int[] Clone(int[] data)
		{
			if (data != null)
			{
				return (int[])data.Clone();
			}
			return null;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000B1BB8 File Offset: 0x000AFDB8
		internal static uint[] Clone(uint[] data)
		{
			if (data != null)
			{
				return (uint[])data.Clone();
			}
			return null;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000B1BCA File Offset: 0x000AFDCA
		public static long[] Clone(long[] data)
		{
			if (data != null)
			{
				return (long[])data.Clone();
			}
			return null;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000B1BDC File Offset: 0x000AFDDC
		[CLSCompliant(false)]
		public static ulong[] Clone(ulong[] data)
		{
			if (data != null)
			{
				return (ulong[])data.Clone();
			}
			return null;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000B1BEE File Offset: 0x000AFDEE
		[CLSCompliant(false)]
		public static ulong[] Clone(ulong[] data, ulong[] existing)
		{
			if (data == null)
			{
				return null;
			}
			if (existing == null || existing.Length != data.Length)
			{
				return Arrays.Clone(data);
			}
			Array.Copy(data, 0, existing, 0, existing.Length);
			return existing;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000B1C14 File Offset: 0x000AFE14
		public static bool Contains(byte[] a, byte n)
		{
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == n)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000B1C38 File Offset: 0x000AFE38
		public static bool Contains(short[] a, short n)
		{
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == n)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000B1C5C File Offset: 0x000AFE5C
		public static bool Contains(int[] a, int n)
		{
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == n)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000B1C80 File Offset: 0x000AFE80
		public static void Fill(byte[] buf, byte b)
		{
			int i = buf.Length;
			while (i > 0)
			{
				buf[--i] = b;
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000B1CA0 File Offset: 0x000AFEA0
		public static void Fill(byte[] buf, int from, int to, byte b)
		{
			for (int i = from; i < to; i++)
			{
				buf[i] = b;
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000B1CC0 File Offset: 0x000AFEC0
		public static byte[] CopyOf(byte[] data, int newLength)
		{
			byte[] array = new byte[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000B1CE8 File Offset: 0x000AFEE8
		public static char[] CopyOf(char[] data, int newLength)
		{
			char[] array = new char[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000B1D10 File Offset: 0x000AFF10
		public static int[] CopyOf(int[] data, int newLength)
		{
			int[] array = new int[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000B1D38 File Offset: 0x000AFF38
		public static long[] CopyOf(long[] data, int newLength)
		{
			long[] array = new long[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000B1D60 File Offset: 0x000AFF60
		public static BigInteger[] CopyOf(BigInteger[] data, int newLength)
		{
			BigInteger[] array = new BigInteger[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000B1D88 File Offset: 0x000AFF88
		public static byte[] CopyOfRange(byte[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			byte[] array = new byte[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000B1DBC File Offset: 0x000AFFBC
		public static int[] CopyOfRange(int[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			int[] array = new int[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000B1DF0 File Offset: 0x000AFFF0
		public static long[] CopyOfRange(long[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			long[] array = new long[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000B1E24 File Offset: 0x000B0024
		public static BigInteger[] CopyOfRange(BigInteger[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			BigInteger[] array = new BigInteger[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000B1E55 File Offset: 0x000B0055
		private static int GetLength(int from, int to)
		{
			int num = to - from;
			if (num < 0)
			{
				throw new ArgumentException(from + " > " + to);
			}
			return num;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000B1E7C File Offset: 0x000B007C
		public static byte[] Append(byte[] a, byte b)
		{
			if (a == null)
			{
				return new byte[]
				{
					b
				};
			}
			int num = a.Length;
			byte[] array = new byte[num + 1];
			Array.Copy(a, 0, array, 0, num);
			array[num] = b;
			return array;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000B1EB4 File Offset: 0x000B00B4
		public static short[] Append(short[] a, short b)
		{
			if (a == null)
			{
				return new short[]
				{
					b
				};
			}
			int num = a.Length;
			short[] array = new short[num + 1];
			Array.Copy(a, 0, array, 0, num);
			array[num] = b;
			return array;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000B1EEC File Offset: 0x000B00EC
		public static int[] Append(int[] a, int b)
		{
			if (a == null)
			{
				return new int[]
				{
					b
				};
			}
			int num = a.Length;
			int[] array = new int[num + 1];
			Array.Copy(a, 0, array, 0, num);
			array[num] = b;
			return array;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000B1F24 File Offset: 0x000B0124
		public static byte[] Concatenate(byte[] a, byte[] b)
		{
			if (a == null)
			{
				return Arrays.Clone(b);
			}
			if (b == null)
			{
				return Arrays.Clone(a);
			}
			byte[] array = new byte[a.Length + b.Length];
			Array.Copy(a, 0, array, 0, a.Length);
			Array.Copy(b, 0, array, a.Length, b.Length);
			return array;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000B1F70 File Offset: 0x000B0170
		public static byte[] ConcatenateAll(params byte[][] vs)
		{
			byte[][] array = new byte[vs.Length][];
			int num = 0;
			int num2 = 0;
			foreach (byte[] array2 in vs)
			{
				if (array2 != null)
				{
					array[num++] = array2;
					num2 += array2.Length;
				}
			}
			byte[] array3 = new byte[num2];
			int num3 = 0;
			for (int j = 0; j < num; j++)
			{
				byte[] array4 = array[j];
				Array.Copy(array4, 0, array3, num3, array4.Length);
				num3 += array4.Length;
			}
			return array3;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000B1FF0 File Offset: 0x000B01F0
		public static int[] Concatenate(int[] a, int[] b)
		{
			if (a == null)
			{
				return Arrays.Clone(b);
			}
			if (b == null)
			{
				return Arrays.Clone(a);
			}
			int[] array = new int[a.Length + b.Length];
			Array.Copy(a, 0, array, 0, a.Length);
			Array.Copy(b, 0, array, a.Length, b.Length);
			return array;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000B203C File Offset: 0x000B023C
		public static byte[] Prepend(byte[] a, byte b)
		{
			if (a == null)
			{
				return new byte[]
				{
					b
				};
			}
			int num = a.Length;
			byte[] array = new byte[num + 1];
			Array.Copy(a, 0, array, 1, num);
			array[0] = b;
			return array;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000B2074 File Offset: 0x000B0274
		public static short[] Prepend(short[] a, short b)
		{
			if (a == null)
			{
				return new short[]
				{
					b
				};
			}
			int num = a.Length;
			short[] array = new short[num + 1];
			Array.Copy(a, 0, array, 1, num);
			array[0] = b;
			return array;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000B20AC File Offset: 0x000B02AC
		public static int[] Prepend(int[] a, int b)
		{
			if (a == null)
			{
				return new int[]
				{
					b
				};
			}
			int num = a.Length;
			int[] array = new int[num + 1];
			Array.Copy(a, 0, array, 1, num);
			array[0] = b;
			return array;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000B20E4 File Offset: 0x000B02E4
		public static byte[] Reverse(byte[] a)
		{
			if (a == null)
			{
				return null;
			}
			int num = 0;
			int num2 = a.Length;
			byte[] array = new byte[num2];
			while (--num2 >= 0)
			{
				array[num2] = a[num++];
			}
			return array;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000B2118 File Offset: 0x000B0318
		public static int[] Reverse(int[] a)
		{
			if (a == null)
			{
				return null;
			}
			int num = 0;
			int num2 = a.Length;
			int[] array = new int[num2];
			while (--num2 >= 0)
			{
				array[num2] = a[num++];
			}
			return array;
		}

		// Token: 0x04001547 RID: 5447
		public static readonly byte[] EmptyBytes = new byte[0];

		// Token: 0x04001548 RID: 5448
		public static readonly int[] EmptyInts = new int[0];
	}
}
