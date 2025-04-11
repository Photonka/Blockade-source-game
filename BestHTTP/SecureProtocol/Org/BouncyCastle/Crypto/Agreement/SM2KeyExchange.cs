using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005B5 RID: 1461
	public class SM2KeyExchange
	{
		// Token: 0x06003877 RID: 14455 RVA: 0x001662FF File Offset: 0x001644FF
		public SM2KeyExchange() : this(new SM3Digest())
		{
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x0016630C File Offset: 0x0016450C
		public SM2KeyExchange(IDigest digest)
		{
			this.mDigest = digest;
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x0016631C File Offset: 0x0016451C
		public virtual void Init(ICipherParameters privParam)
		{
			SM2KeyExchangePrivateParameters sm2KeyExchangePrivateParameters;
			if (privParam is ParametersWithID)
			{
				sm2KeyExchangePrivateParameters = (SM2KeyExchangePrivateParameters)((ParametersWithID)privParam).Parameters;
				this.mUserID = ((ParametersWithID)privParam).GetID();
			}
			else
			{
				sm2KeyExchangePrivateParameters = (SM2KeyExchangePrivateParameters)privParam;
				this.mUserID = new byte[0];
			}
			this.mInitiator = sm2KeyExchangePrivateParameters.IsInitiator;
			this.mStaticKey = sm2KeyExchangePrivateParameters.StaticPrivateKey;
			this.mEphemeralKey = sm2KeyExchangePrivateParameters.EphemeralPrivateKey;
			this.mECParams = this.mStaticKey.Parameters;
			this.mStaticPubPoint = sm2KeyExchangePrivateParameters.StaticPublicPoint;
			this.mEphemeralPubPoint = sm2KeyExchangePrivateParameters.EphemeralPublicPoint;
			this.mW = this.mECParams.Curve.FieldSize / 2 - 1;
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x001663D0 File Offset: 0x001645D0
		public virtual byte[] CalculateKey(int kLen, ICipherParameters pubParam)
		{
			SM2KeyExchangePublicParameters sm2KeyExchangePublicParameters;
			byte[] userID;
			if (pubParam is ParametersWithID)
			{
				sm2KeyExchangePublicParameters = (SM2KeyExchangePublicParameters)((ParametersWithID)pubParam).Parameters;
				userID = ((ParametersWithID)pubParam).GetID();
			}
			else
			{
				sm2KeyExchangePublicParameters = (SM2KeyExchangePublicParameters)pubParam;
				userID = new byte[0];
			}
			byte[] z = this.GetZ(this.mDigest, this.mUserID, this.mStaticPubPoint);
			byte[] z2 = this.GetZ(this.mDigest, userID, sm2KeyExchangePublicParameters.StaticPublicKey.Q);
			ECPoint u = this.CalculateU(sm2KeyExchangePublicParameters);
			byte[] result;
			if (this.mInitiator)
			{
				result = this.Kdf(u, z, z2, kLen);
			}
			else
			{
				result = this.Kdf(u, z2, z, kLen);
			}
			return result;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x00166474 File Offset: 0x00164674
		public virtual byte[][] CalculateKeyWithConfirmation(int kLen, byte[] confirmationTag, ICipherParameters pubParam)
		{
			SM2KeyExchangePublicParameters sm2KeyExchangePublicParameters;
			byte[] userID;
			if (pubParam is ParametersWithID)
			{
				sm2KeyExchangePublicParameters = (SM2KeyExchangePublicParameters)((ParametersWithID)pubParam).Parameters;
				userID = ((ParametersWithID)pubParam).GetID();
			}
			else
			{
				sm2KeyExchangePublicParameters = (SM2KeyExchangePublicParameters)pubParam;
				userID = new byte[0];
			}
			if (this.mInitiator && confirmationTag == null)
			{
				throw new ArgumentException("if initiating, confirmationTag must be set");
			}
			byte[] z = this.GetZ(this.mDigest, this.mUserID, this.mStaticPubPoint);
			byte[] z2 = this.GetZ(this.mDigest, userID, sm2KeyExchangePublicParameters.StaticPublicKey.Q);
			ECPoint u = this.CalculateU(sm2KeyExchangePublicParameters);
			byte[] array;
			if (!this.mInitiator)
			{
				array = this.Kdf(u, z2, z, kLen);
				byte[] inner = this.CalculateInnerHash(this.mDigest, u, z2, z, sm2KeyExchangePublicParameters.EphemeralPublicKey.Q, this.mEphemeralPubPoint);
				return new byte[][]
				{
					array,
					this.S1(this.mDigest, u, inner),
					this.S2(this.mDigest, u, inner)
				};
			}
			array = this.Kdf(u, z, z2, kLen);
			byte[] inner2 = this.CalculateInnerHash(this.mDigest, u, z, z2, this.mEphemeralPubPoint, sm2KeyExchangePublicParameters.EphemeralPublicKey.Q);
			if (!Arrays.ConstantTimeAreEqual(this.S1(this.mDigest, u, inner2), confirmationTag))
			{
				throw new InvalidOperationException("confirmation tag mismatch");
			}
			return new byte[][]
			{
				array,
				this.S2(this.mDigest, u, inner2)
			};
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x001665E4 File Offset: 0x001647E4
		protected virtual ECPoint CalculateU(SM2KeyExchangePublicParameters otherPub)
		{
			ECDomainParameters parameters = this.mStaticKey.Parameters;
			ECPoint p = ECAlgorithms.CleanPoint(parameters.Curve, otherPub.StaticPublicKey.Q);
			ECPoint ecpoint = ECAlgorithms.CleanPoint(parameters.Curve, otherPub.EphemeralPublicKey.Q);
			BigInteger bigInteger = this.Reduce(this.mEphemeralPubPoint.AffineXCoord.ToBigInteger());
			BigInteger val = this.Reduce(ecpoint.AffineXCoord.ToBigInteger());
			BigInteger val2 = this.mStaticKey.D.Add(bigInteger.Multiply(this.mEphemeralKey.D));
			BigInteger bigInteger2 = this.mECParams.H.Multiply(val2).Mod(this.mECParams.N);
			BigInteger b = bigInteger2.Multiply(val).Mod(this.mECParams.N);
			return ECAlgorithms.SumOfTwoMultiplies(p, bigInteger2, ecpoint, b).Normalize();
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x001666C4 File Offset: 0x001648C4
		protected virtual byte[] Kdf(ECPoint u, byte[] za, byte[] zb, int klen)
		{
			int digestSize = this.mDigest.GetDigestSize();
			byte[] array = new byte[Math.Max(4, digestSize)];
			byte[] array2 = new byte[(klen + 7) / 8];
			int i = 0;
			IMemoable memoable = this.mDigest as IMemoable;
			IMemoable other = null;
			if (memoable != null)
			{
				this.AddFieldElement(this.mDigest, u.AffineXCoord);
				this.AddFieldElement(this.mDigest, u.AffineYCoord);
				this.mDigest.BlockUpdate(za, 0, za.Length);
				this.mDigest.BlockUpdate(zb, 0, zb.Length);
				other = memoable.Copy();
			}
			uint num = 0U;
			while (i < array2.Length)
			{
				if (memoable != null)
				{
					memoable.Reset(other);
				}
				else
				{
					this.AddFieldElement(this.mDigest, u.AffineXCoord);
					this.AddFieldElement(this.mDigest, u.AffineYCoord);
					this.mDigest.BlockUpdate(za, 0, za.Length);
					this.mDigest.BlockUpdate(zb, 0, zb.Length);
				}
				Pack.UInt32_To_BE(num += 1U, array, 0);
				this.mDigest.BlockUpdate(array, 0, 4);
				this.mDigest.DoFinal(array, 0);
				int num2 = Math.Min(digestSize, array2.Length - i);
				Array.Copy(array, 0, array2, i, num2);
				i += num2;
			}
			return array2;
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x00166805 File Offset: 0x00164A05
		private BigInteger Reduce(BigInteger x)
		{
			return x.And(BigInteger.One.ShiftLeft(this.mW).Subtract(BigInteger.One)).SetBit(this.mW);
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x00166832 File Offset: 0x00164A32
		private byte[] S1(IDigest digest, ECPoint u, byte[] inner)
		{
			digest.Update(2);
			this.AddFieldElement(digest, u.AffineYCoord);
			digest.BlockUpdate(inner, 0, inner.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x0016685C File Offset: 0x00164A5C
		private byte[] CalculateInnerHash(IDigest digest, ECPoint u, byte[] za, byte[] zb, ECPoint p1, ECPoint p2)
		{
			this.AddFieldElement(digest, u.AffineXCoord);
			digest.BlockUpdate(za, 0, za.Length);
			digest.BlockUpdate(zb, 0, zb.Length);
			this.AddFieldElement(digest, p1.AffineXCoord);
			this.AddFieldElement(digest, p1.AffineYCoord);
			this.AddFieldElement(digest, p2.AffineXCoord);
			this.AddFieldElement(digest, p2.AffineYCoord);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x001668CC File Offset: 0x00164ACC
		private byte[] S2(IDigest digest, ECPoint u, byte[] inner)
		{
			digest.Update(3);
			this.AddFieldElement(digest, u.AffineYCoord);
			digest.BlockUpdate(inner, 0, inner.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x001668F4 File Offset: 0x00164AF4
		private byte[] GetZ(IDigest digest, byte[] userID, ECPoint pubPoint)
		{
			this.AddUserID(digest, userID);
			this.AddFieldElement(digest, this.mECParams.Curve.A);
			this.AddFieldElement(digest, this.mECParams.Curve.B);
			this.AddFieldElement(digest, this.mECParams.G.AffineXCoord);
			this.AddFieldElement(digest, this.mECParams.G.AffineYCoord);
			this.AddFieldElement(digest, pubPoint.AffineXCoord);
			this.AddFieldElement(digest, pubPoint.AffineYCoord);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x00166988 File Offset: 0x00164B88
		private void AddUserID(IDigest digest, byte[] userID)
		{
			uint num = (uint)(userID.Length * 8);
			digest.Update((byte)(num >> 8));
			digest.Update((byte)num);
			digest.BlockUpdate(userID, 0, userID.Length);
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x001669B8 File Offset: 0x00164BB8
		private void AddFieldElement(IDigest digest, ECFieldElement v)
		{
			byte[] encoded = v.GetEncoded();
			digest.BlockUpdate(encoded, 0, encoded.Length);
		}

		// Token: 0x040023FE RID: 9214
		private readonly IDigest mDigest;

		// Token: 0x040023FF RID: 9215
		private byte[] mUserID;

		// Token: 0x04002400 RID: 9216
		private ECPrivateKeyParameters mStaticKey;

		// Token: 0x04002401 RID: 9217
		private ECPoint mStaticPubPoint;

		// Token: 0x04002402 RID: 9218
		private ECPoint mEphemeralPubPoint;

		// Token: 0x04002403 RID: 9219
		private ECDomainParameters mECParams;

		// Token: 0x04002404 RID: 9220
		private int mW;

		// Token: 0x04002405 RID: 9221
		private ECPrivateKeyParameters mEphemeralKey;

		// Token: 0x04002406 RID: 9222
		private bool mInitiator;
	}
}
