using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc7748
{
	// Token: 0x0200031F RID: 799
	public abstract class X25519
	{
		// Token: 0x06001F76 RID: 8054 RVA: 0x000ED72C File Offset: 0x000EB92C
		public static bool CalculateAgreement(byte[] k, int kOff, byte[] u, int uOff, byte[] r, int rOff)
		{
			X25519.ScalarMult(k, kOff, u, uOff, r, rOff);
			return !Arrays.AreAllZeroes(r, rOff, 32);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000E9C24 File Offset: 0x000E7E24
		private static uint Decode32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] | (int)bs[++off] << 8 | (int)bs[++off] << 16 | (int)bs[++off] << 24);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000ED74C File Offset: 0x000EB94C
		private static void DecodeScalar(byte[] k, int kOff, uint[] n)
		{
			for (int i = 0; i < 8; i++)
			{
				n[i] = X25519.Decode32(k, kOff + i * 4);
			}
			n[0] &= 4294967288U;
			n[7] &= 2147483647U;
			n[7] |= 1073741824U;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000ED7A0 File Offset: 0x000EB9A0
		public static void GeneratePrivateKey(SecureRandom random, byte[] k)
		{
			random.NextBytes(k);
			int num = 0;
			k[num] &= 248;
			int num2 = 31;
			k[num2] &= 127;
			int num3 = 31;
			k[num3] |= 64;
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000ED7D8 File Offset: 0x000EB9D8
		public static void GeneratePublicKey(byte[] k, int kOff, byte[] r, int rOff)
		{
			X25519.ScalarMultBase(k, kOff, r, rOff);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000ED7E4 File Offset: 0x000EB9E4
		private static void PointDouble(int[] x, int[] z)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			X25519Field.Apm(x, z, array, array2);
			X25519Field.Sqr(array, array);
			X25519Field.Sqr(array2, array2);
			X25519Field.Mul(array, array2, x);
			X25519Field.Sub(array, array2, array);
			X25519Field.Mul(array, 121666, z);
			X25519Field.Add(z, array2, z);
			X25519Field.Mul(z, array, z);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000ED840 File Offset: 0x000EBA40
		public static void Precompute()
		{
			Ed25519.Precompute();
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000ED848 File Offset: 0x000EBA48
		public static void ScalarMult(byte[] k, int kOff, byte[] u, int uOff, byte[] r, int rOff)
		{
			uint[] array = new uint[8];
			X25519.DecodeScalar(k, kOff, array);
			int[] array2 = X25519Field.Create();
			X25519Field.Decode(u, uOff, array2);
			int[] array3 = X25519Field.Create();
			X25519Field.Copy(array2, 0, array3, 0);
			int[] array4 = X25519Field.Create();
			array4[0] = 1;
			int[] array5 = X25519Field.Create();
			array5[0] = 1;
			int[] array6 = X25519Field.Create();
			int[] array7 = X25519Field.Create();
			int[] array8 = X25519Field.Create();
			int num = 254;
			int num2 = 1;
			do
			{
				X25519Field.Apm(array5, array6, array7, array5);
				X25519Field.Apm(array3, array4, array6, array3);
				X25519Field.Mul(array7, array3, array7);
				X25519Field.Mul(array5, array6, array5);
				X25519Field.Sqr(array6, array6);
				X25519Field.Sqr(array3, array3);
				X25519Field.Sub(array6, array3, array8);
				X25519Field.Mul(array8, 121666, array4);
				X25519Field.Add(array4, array3, array4);
				X25519Field.Mul(array4, array8, array4);
				X25519Field.Mul(array3, array6, array3);
				X25519Field.Apm(array7, array5, array5, array6);
				X25519Field.Sqr(array5, array5);
				X25519Field.Sqr(array6, array6);
				X25519Field.Mul(array6, array2, array6);
				num--;
				int num3 = num >> 5;
				int num4 = num & 31;
				int num5 = (int)(array[num3] >> num4 & 1U);
				num2 ^= num5;
				X25519Field.CSwap(num2, array3, array5);
				X25519Field.CSwap(num2, array4, array6);
				num2 = num5;
			}
			while (num >= 3);
			for (int i = 0; i < 3; i++)
			{
				X25519.PointDouble(array3, array4);
			}
			X25519Field.Inv(array4, array4);
			X25519Field.Mul(array3, array4, array3);
			X25519Field.Normalize(array3);
			X25519Field.Encode(array3, r, rOff);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000ED9CC File Offset: 0x000EBBCC
		public static void ScalarMultBase(byte[] k, int kOff, byte[] r, int rOff)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			Ed25519.ScalarMultBaseYZ(k, kOff, array, array2);
			X25519Field.Apm(array2, array, array, array2);
			X25519Field.Inv(array2, array2);
			X25519Field.Mul(array, array2, array);
			X25519Field.Normalize(array);
			X25519Field.Encode(array, r, rOff);
		}

		// Token: 0x04001890 RID: 6288
		public const int PointSize = 32;

		// Token: 0x04001891 RID: 6289
		public const int ScalarSize = 32;

		// Token: 0x04001892 RID: 6290
		private const int C_A = 486662;

		// Token: 0x04001893 RID: 6291
		private const int C_A24 = 121666;
	}
}
