using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc7748
{
	// Token: 0x02000321 RID: 801
	public abstract class X448
	{
		// Token: 0x06001FA1 RID: 8097 RVA: 0x000EEBCF File Offset: 0x000ECDCF
		public static bool CalculateAgreement(byte[] k, int kOff, byte[] u, int uOff, byte[] r, int rOff)
		{
			X448.ScalarMult(k, kOff, u, uOff, r, rOff);
			return !Arrays.AreAllZeroes(r, rOff, 56);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000E9C24 File Offset: 0x000E7E24
		private static uint Decode32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] | (int)bs[++off] << 8 | (int)bs[++off] << 16 | (int)bs[++off] << 24);
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000EEBEC File Offset: 0x000ECDEC
		private static void DecodeScalar(byte[] k, int kOff, uint[] n)
		{
			for (int i = 0; i < 14; i++)
			{
				n[i] = X448.Decode32(k, kOff + i * 4);
			}
			n[0] &= 4294967292U;
			n[13] |= 2147483648U;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000EEC32 File Offset: 0x000ECE32
		public static void GeneratePrivateKey(SecureRandom random, byte[] k)
		{
			random.NextBytes(k);
			int num = 0;
			k[num] &= 252;
			int num2 = 55;
			k[num2] |= 128;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000EEC5E File Offset: 0x000ECE5E
		public static void GeneratePublicKey(byte[] k, int kOff, byte[] r, int rOff)
		{
			X448.ScalarMultBase(k, kOff, r, rOff);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000EEC6C File Offset: 0x000ECE6C
		private static void PointDouble(uint[] x, uint[] z)
		{
			uint[] array = X448Field.Create();
			uint[] array2 = X448Field.Create();
			X448Field.Add(x, z, array);
			X448Field.Sub(x, z, array2);
			X448Field.Sqr(array, array);
			X448Field.Sqr(array2, array2);
			X448Field.Mul(array, array2, x);
			X448Field.Sub(array, array2, array);
			X448Field.Mul(array, 39082U, z);
			X448Field.Add(z, array2, z);
			X448Field.Mul(z, array, z);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000EECCF File Offset: 0x000ECECF
		public static void Precompute()
		{
			Ed448.Precompute();
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000EECD8 File Offset: 0x000ECED8
		public static void ScalarMult(byte[] k, int kOff, byte[] u, int uOff, byte[] r, int rOff)
		{
			uint[] array = new uint[14];
			X448.DecodeScalar(k, kOff, array);
			uint[] array2 = X448Field.Create();
			X448Field.Decode(u, uOff, array2);
			uint[] array3 = X448Field.Create();
			X448Field.Copy(array2, 0, array3, 0);
			uint[] array4 = X448Field.Create();
			array4[0] = 1U;
			uint[] array5 = X448Field.Create();
			array5[0] = 1U;
			uint[] array6 = X448Field.Create();
			uint[] array7 = X448Field.Create();
			uint[] array8 = X448Field.Create();
			int num = 447;
			int num2 = 1;
			do
			{
				X448Field.Add(array5, array6, array7);
				X448Field.Sub(array5, array6, array5);
				X448Field.Add(array3, array4, array6);
				X448Field.Sub(array3, array4, array3);
				X448Field.Mul(array7, array3, array7);
				X448Field.Mul(array5, array6, array5);
				X448Field.Sqr(array6, array6);
				X448Field.Sqr(array3, array3);
				X448Field.Sub(array6, array3, array8);
				X448Field.Mul(array8, 39082U, array4);
				X448Field.Add(array4, array3, array4);
				X448Field.Mul(array4, array8, array4);
				X448Field.Mul(array3, array6, array3);
				X448Field.Sub(array7, array5, array6);
				X448Field.Add(array7, array5, array5);
				X448Field.Sqr(array5, array5);
				X448Field.Sqr(array6, array6);
				X448Field.Mul(array6, array2, array6);
				num--;
				int num3 = num >> 5;
				int num4 = num & 31;
				int num5 = (int)(array[num3] >> num4 & 1U);
				num2 ^= num5;
				X448Field.CSwap(num2, array3, array5);
				X448Field.CSwap(num2, array4, array6);
				num2 = num5;
			}
			while (num >= 2);
			for (int i = 0; i < 2; i++)
			{
				X448.PointDouble(array3, array4);
			}
			X448Field.Inv(array4, array4);
			X448Field.Mul(array3, array4, array3);
			X448Field.Normalize(array3);
			X448Field.Encode(array3, r, rOff);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000EEE74 File Offset: 0x000ED074
		public static void ScalarMultBase(byte[] k, int kOff, byte[] r, int rOff)
		{
			uint[] array = X448Field.Create();
			uint[] y = X448Field.Create();
			Ed448.ScalarMultBaseXY(k, kOff, array, y);
			X448Field.Inv(array, array);
			X448Field.Mul(array, y, array);
			X448Field.Sqr(array, array);
			X448Field.Normalize(array);
			X448Field.Encode(array, r, rOff);
		}

		// Token: 0x04001899 RID: 6297
		public const int PointSize = 56;

		// Token: 0x0400189A RID: 6298
		public const int ScalarSize = 56;

		// Token: 0x0400189B RID: 6299
		private const uint C_A = 156326U;

		// Token: 0x0400189C RID: 6300
		private const uint C_A24 = 39082U;
	}
}
