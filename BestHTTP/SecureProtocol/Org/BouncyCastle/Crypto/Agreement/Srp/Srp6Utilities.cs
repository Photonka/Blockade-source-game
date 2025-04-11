using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x020005BB RID: 1467
	public class Srp6Utilities
	{
		// Token: 0x060038A5 RID: 14501 RVA: 0x00167011 File Offset: 0x00165211
		public static BigInteger CalculateK(IDigest digest, BigInteger N, BigInteger g)
		{
			return Srp6Utilities.HashPaddedPair(digest, N, N, g);
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x0016701C File Offset: 0x0016521C
		public static BigInteger CalculateU(IDigest digest, BigInteger N, BigInteger A, BigInteger B)
		{
			return Srp6Utilities.HashPaddedPair(digest, N, A, B);
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x00167028 File Offset: 0x00165228
		public static BigInteger CalculateX(IDigest digest, BigInteger N, byte[] salt, byte[] identity, byte[] password)
		{
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(identity, 0, identity.Length);
			digest.Update(58);
			digest.BlockUpdate(password, 0, password.Length);
			digest.DoFinal(array, 0);
			digest.BlockUpdate(salt, 0, salt.Length);
			digest.BlockUpdate(array, 0, array.Length);
			digest.DoFinal(array, 0);
			return new BigInteger(1, array);
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x00167090 File Offset: 0x00165290
		public static BigInteger GeneratePrivateValue(IDigest digest, BigInteger N, BigInteger g, SecureRandom random)
		{
			int num = Math.Min(256, N.BitLength / 2);
			BigInteger min = BigInteger.One.ShiftLeft(num - 1);
			BigInteger max = N.Subtract(BigInteger.One);
			return BigIntegers.CreateRandomInRange(min, max, random);
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x001670D0 File Offset: 0x001652D0
		public static BigInteger ValidatePublicValue(BigInteger N, BigInteger val)
		{
			val = val.Mod(N);
			if (val.Equals(BigInteger.Zero))
			{
				throw new CryptoException("Invalid public value: 0");
			}
			return val;
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x001670F4 File Offset: 0x001652F4
		public static BigInteger CalculateM1(IDigest digest, BigInteger N, BigInteger A, BigInteger B, BigInteger S)
		{
			return Srp6Utilities.HashPaddedTriplet(digest, N, A, B, S);
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x001670F4 File Offset: 0x001652F4
		public static BigInteger CalculateM2(IDigest digest, BigInteger N, BigInteger A, BigInteger M1, BigInteger S)
		{
			return Srp6Utilities.HashPaddedTriplet(digest, N, A, M1, S);
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x00167104 File Offset: 0x00165304
		public static BigInteger CalculateKey(IDigest digest, BigInteger N, BigInteger S)
		{
			int length = (N.BitLength + 7) / 8;
			byte[] padded = Srp6Utilities.GetPadded(S, length);
			digest.BlockUpdate(padded, 0, padded.Length);
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return new BigInteger(1, array);
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x0016714C File Offset: 0x0016534C
		private static BigInteger HashPaddedTriplet(IDigest digest, BigInteger N, BigInteger n1, BigInteger n2, BigInteger n3)
		{
			int length = (N.BitLength + 7) / 8;
			byte[] padded = Srp6Utilities.GetPadded(n1, length);
			byte[] padded2 = Srp6Utilities.GetPadded(n2, length);
			byte[] padded3 = Srp6Utilities.GetPadded(n3, length);
			digest.BlockUpdate(padded, 0, padded.Length);
			digest.BlockUpdate(padded2, 0, padded2.Length);
			digest.BlockUpdate(padded3, 0, padded3.Length);
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return new BigInteger(1, array);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x001671C0 File Offset: 0x001653C0
		private static BigInteger HashPaddedPair(IDigest digest, BigInteger N, BigInteger n1, BigInteger n2)
		{
			int length = (N.BitLength + 7) / 8;
			byte[] padded = Srp6Utilities.GetPadded(n1, length);
			byte[] padded2 = Srp6Utilities.GetPadded(n2, length);
			digest.BlockUpdate(padded, 0, padded.Length);
			digest.BlockUpdate(padded2, 0, padded2.Length);
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return new BigInteger(1, array);
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x0016721C File Offset: 0x0016541C
		private static byte[] GetPadded(BigInteger n, int length)
		{
			byte[] array = BigIntegers.AsUnsignedByteArray(n);
			if (array.Length < length)
			{
				byte[] array2 = new byte[length];
				Array.Copy(array, 0, array2, length - array.Length, array.Length);
				array = array2;
			}
			return array;
		}
	}
}
