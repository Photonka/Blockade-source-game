using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200057E RID: 1406
	public class SM2Engine
	{
		// Token: 0x0600359A RID: 13722 RVA: 0x0014DCE4 File Offset: 0x0014BEE4
		public SM2Engine() : this(new SM3Digest())
		{
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x0014DCF1 File Offset: 0x0014BEF1
		public SM2Engine(IDigest digest)
		{
			this.mDigest = digest;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x0014DD00 File Offset: 0x0014BF00
		public virtual void Init(bool forEncryption, ICipherParameters param)
		{
			this.mForEncryption = forEncryption;
			if (forEncryption)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.mECKey = (ECKeyParameters)parametersWithRandom.Parameters;
				this.mECParams = this.mECKey.Parameters;
				if (((ECPublicKeyParameters)this.mECKey).Q.Multiply(this.mECParams.H).IsInfinity)
				{
					throw new ArgumentException("invalid key: [h]Q at infinity");
				}
				this.mRandom = parametersWithRandom.Random;
			}
			else
			{
				this.mECKey = (ECKeyParameters)param;
				this.mECParams = this.mECKey.Parameters;
			}
			this.mCurveLength = (this.mECParams.Curve.FieldSize + 7) / 8;
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x0014DDB7 File Offset: 0x0014BFB7
		public virtual byte[] ProcessBlock(byte[] input, int inOff, int inLen)
		{
			if (this.mForEncryption)
			{
				return this.Encrypt(input, inOff, inLen);
			}
			return this.Decrypt(input, inOff, inLen);
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x00120BB4 File Offset: 0x0011EDB4
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x0014DDD4 File Offset: 0x0014BFD4
		private byte[] Encrypt(byte[] input, int inOff, int inLen)
		{
			byte[] array = new byte[inLen];
			Array.Copy(input, inOff, array, 0, array.Length);
			ECMultiplier ecmultiplier = this.CreateBasePointMultiplier();
			byte[] encoded;
			ECPoint ecpoint;
			do
			{
				BigInteger bigInteger = this.NextK();
				encoded = ecmultiplier.Multiply(this.mECParams.G, bigInteger).Normalize().GetEncoded(false);
				ecpoint = ((ECPublicKeyParameters)this.mECKey).Q.Multiply(bigInteger).Normalize();
				this.Kdf(this.mDigest, ecpoint, array);
			}
			while (this.NotEncrypted(array, input, inOff));
			this.AddFieldElement(this.mDigest, ecpoint.AffineXCoord);
			this.mDigest.BlockUpdate(input, inOff, inLen);
			this.AddFieldElement(this.mDigest, ecpoint.AffineYCoord);
			byte[] array2 = DigestUtilities.DoFinal(this.mDigest);
			return Arrays.ConcatenateAll(new byte[][]
			{
				encoded,
				array,
				array2
			});
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x0014DEB0 File Offset: 0x0014C0B0
		private byte[] Decrypt(byte[] input, int inOff, int inLen)
		{
			byte[] array = new byte[this.mCurveLength * 2 + 1];
			Array.Copy(input, inOff, array, 0, array.Length);
			ECPoint ecpoint = this.mECParams.Curve.DecodePoint(array);
			if (ecpoint.Multiply(this.mECParams.H).IsInfinity)
			{
				throw new InvalidCipherTextException("[h]C1 at infinity");
			}
			ecpoint = ecpoint.Multiply(((ECPrivateKeyParameters)this.mECKey).D).Normalize();
			byte[] array2 = new byte[inLen - array.Length - this.mDigest.GetDigestSize()];
			Array.Copy(input, inOff + array.Length, array2, 0, array2.Length);
			this.Kdf(this.mDigest, ecpoint, array2);
			this.AddFieldElement(this.mDigest, ecpoint.AffineXCoord);
			this.mDigest.BlockUpdate(array2, 0, array2.Length);
			this.AddFieldElement(this.mDigest, ecpoint.AffineYCoord);
			byte[] array3 = DigestUtilities.DoFinal(this.mDigest);
			int num = 0;
			for (int num2 = 0; num2 != array3.Length; num2++)
			{
				num |= (int)(array3[num2] ^ input[inOff + array.Length + array2.Length + num2]);
			}
			Arrays.Fill(array, 0);
			Arrays.Fill(array3, 0);
			if (num != 0)
			{
				Arrays.Fill(array2, 0);
				throw new InvalidCipherTextException("invalid cipher text");
			}
			return array2;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x0014DFF4 File Offset: 0x0014C1F4
		private bool NotEncrypted(byte[] encData, byte[] input, int inOff)
		{
			for (int num = 0; num != encData.Length; num++)
			{
				if (encData[num] != input[inOff])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x0014E01C File Offset: 0x0014C21C
		private void Kdf(IDigest digest, ECPoint c1, byte[] encData)
		{
			int digestSize = digest.GetDigestSize();
			byte[] array = new byte[Math.Max(4, digestSize)];
			int i = 0;
			IMemoable memoable = digest as IMemoable;
			IMemoable other = null;
			if (memoable != null)
			{
				this.AddFieldElement(digest, c1.AffineXCoord);
				this.AddFieldElement(digest, c1.AffineYCoord);
				other = memoable.Copy();
			}
			uint num = 0U;
			while (i < encData.Length)
			{
				if (memoable != null)
				{
					memoable.Reset(other);
				}
				else
				{
					this.AddFieldElement(digest, c1.AffineXCoord);
					this.AddFieldElement(digest, c1.AffineYCoord);
				}
				Pack.UInt32_To_BE(num += 1U, array, 0);
				digest.BlockUpdate(array, 0, 4);
				digest.DoFinal(array, 0);
				int num2 = Math.Min(digestSize, encData.Length - i);
				this.Xor(encData, array, i, num2);
				i += num2;
			}
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x0014E0E0 File Offset: 0x0014C2E0
		private void Xor(byte[] data, byte[] kdfOut, int dOff, int dRemaining)
		{
			for (int num = 0; num != dRemaining; num++)
			{
				int num2 = dOff + num;
				data[num2] ^= kdfOut[num];
			}
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x0014E10C File Offset: 0x0014C30C
		private BigInteger NextK()
		{
			int bitLength = this.mECParams.N.BitLength;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(bitLength, this.mRandom);
			}
			while (bigInteger.SignValue == 0 || bigInteger.CompareTo(this.mECParams.N) >= 0);
			return bigInteger;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x0014E154 File Offset: 0x0014C354
		private void AddFieldElement(IDigest digest, ECFieldElement v)
		{
			byte[] encoded = v.GetEncoded();
			digest.BlockUpdate(encoded, 0, encoded.Length);
		}

		// Token: 0x040021FE RID: 8702
		private readonly IDigest mDigest;

		// Token: 0x040021FF RID: 8703
		private bool mForEncryption;

		// Token: 0x04002200 RID: 8704
		private ECKeyParameters mECKey;

		// Token: 0x04002201 RID: 8705
		private ECDomainParameters mECParams;

		// Token: 0x04002202 RID: 8706
		private int mCurveLength;

		// Token: 0x04002203 RID: 8707
		private SecureRandom mRandom;
	}
}
