using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000493 RID: 1171
	public class SM2Signer : ISigner
	{
		// Token: 0x06002E59 RID: 11865 RVA: 0x0012429A File Offset: 0x0012249A
		public SM2Signer()
		{
			this.encoding = StandardDsaEncoding.Instance;
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x001242C3 File Offset: 0x001224C3
		public SM2Signer(IDsaEncoding encoding)
		{
			this.encoding = encoding;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x001242E8 File Offset: 0x001224E8
		public virtual string AlgorithmName
		{
			get
			{
				return "SM2Sign";
			}
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x001242F0 File Offset: 0x001224F0
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			ICipherParameters cipherParameters;
			byte[] userID;
			if (parameters is ParametersWithID)
			{
				cipherParameters = ((ParametersWithID)parameters).Parameters;
				userID = ((ParametersWithID)parameters).GetID();
			}
			else
			{
				cipherParameters = parameters;
				userID = Hex.Decode("31323334353637383132333435363738");
			}
			if (forSigning)
			{
				if (cipherParameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)cipherParameters;
					this.ecKey = (ECKeyParameters)parametersWithRandom.Parameters;
					this.ecParams = this.ecKey.Parameters;
					this.kCalculator.Init(this.ecParams.N, parametersWithRandom.Random);
				}
				else
				{
					this.ecKey = (ECKeyParameters)cipherParameters;
					this.ecParams = this.ecKey.Parameters;
					this.kCalculator.Init(this.ecParams.N, new SecureRandom());
				}
				this.pubPoint = this.CreateBasePointMultiplier().Multiply(this.ecParams.G, ((ECPrivateKeyParameters)this.ecKey).D).Normalize();
			}
			else
			{
				this.ecKey = (ECKeyParameters)cipherParameters;
				this.ecParams = this.ecKey.Parameters;
				this.pubPoint = ((ECPublicKeyParameters)this.ecKey).Q;
			}
			this.digest.Reset();
			this.z = this.GetZ(userID);
			this.digest.BlockUpdate(this.z, 0, this.z.Length);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x00124451 File Offset: 0x00122651
		public virtual void Update(byte b)
		{
			this.digest.Update(b);
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x0012445F File Offset: 0x0012265F
		public virtual void BlockUpdate(byte[] buf, int off, int len)
		{
			this.digest.BlockUpdate(buf, off, len);
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x00124470 File Offset: 0x00122670
		public virtual bool VerifySignature(byte[] signature)
		{
			try
			{
				BigInteger[] array = this.encoding.Decode(this.ecParams.N, signature);
				return this.VerifySignature(array[0], array[1]);
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x001244BC File Offset: 0x001226BC
		public virtual void Reset()
		{
			if (this.z != null)
			{
				this.digest.Reset();
				this.digest.BlockUpdate(this.z, 0, this.z.Length);
			}
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x001244EC File Offset: 0x001226EC
		public virtual byte[] GenerateSignature()
		{
			byte[] message = DigestUtilities.DoFinal(this.digest);
			BigInteger n = this.ecParams.N;
			BigInteger bigInteger = this.CalculateE(message);
			BigInteger d = ((ECPrivateKeyParameters)this.ecKey).D;
			ECMultiplier ecmultiplier = this.CreateBasePointMultiplier();
			BigInteger bigInteger3;
			BigInteger bigInteger5;
			for (;;)
			{
				BigInteger bigInteger2 = this.kCalculator.NextK();
				ECPoint ecpoint = ecmultiplier.Multiply(this.ecParams.G, bigInteger2).Normalize();
				bigInteger3 = bigInteger.Add(ecpoint.AffineXCoord.ToBigInteger()).Mod(n);
				if (bigInteger3.SignValue != 0 && !bigInteger3.Add(bigInteger2).Equals(n))
				{
					BigInteger bigInteger4 = d.Add(BigInteger.One).ModInverse(n);
					bigInteger5 = bigInteger2.Subtract(bigInteger3.Multiply(d)).Mod(n);
					bigInteger5 = bigInteger4.Multiply(bigInteger5).Mod(n);
					if (bigInteger5.SignValue != 0)
					{
						break;
					}
				}
			}
			byte[] result;
			try
			{
				result = this.encoding.Encode(this.ecParams.N, bigInteger3, bigInteger5);
			}
			catch (Exception ex)
			{
				throw new CryptoException("unable to encode signature: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x00124620 File Offset: 0x00122820
		private bool VerifySignature(BigInteger r, BigInteger s)
		{
			BigInteger n = this.ecParams.N;
			if (r.CompareTo(BigInteger.One) < 0 || r.CompareTo(n) >= 0)
			{
				return false;
			}
			if (s.CompareTo(BigInteger.One) < 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			byte[] message = DigestUtilities.DoFinal(this.digest);
			BigInteger bigInteger = this.CalculateE(message);
			BigInteger bigInteger2 = r.Add(s).Mod(n);
			if (bigInteger2.SignValue == 0)
			{
				return false;
			}
			ECPoint q = ((ECPublicKeyParameters)this.ecKey).Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(this.ecParams.G, s, q, bigInteger2).Normalize();
			return !ecpoint.IsInfinity && r.Equals(bigInteger.Add(ecpoint.AffineXCoord.ToBigInteger()).Mod(n));
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x001246F0 File Offset: 0x001228F0
		private byte[] GetZ(byte[] userID)
		{
			this.AddUserID(this.digest, userID);
			this.AddFieldElement(this.digest, this.ecParams.Curve.A);
			this.AddFieldElement(this.digest, this.ecParams.Curve.B);
			this.AddFieldElement(this.digest, this.ecParams.G.AffineXCoord);
			this.AddFieldElement(this.digest, this.ecParams.G.AffineYCoord);
			this.AddFieldElement(this.digest, this.pubPoint.AffineXCoord);
			this.AddFieldElement(this.digest, this.pubPoint.AffineYCoord);
			return DigestUtilities.DoFinal(this.digest);
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x001247B4 File Offset: 0x001229B4
		private void AddUserID(IDigest digest, byte[] userID)
		{
			int num = userID.Length * 8;
			digest.Update((byte)(num >> 8));
			digest.Update((byte)num);
			digest.BlockUpdate(userID, 0, userID.Length);
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x001247E4 File Offset: 0x001229E4
		private void AddFieldElement(IDigest digest, ECFieldElement v)
		{
			byte[] encoded = v.GetEncoded();
			digest.BlockUpdate(encoded, 0, encoded.Length);
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x00124803 File Offset: 0x00122A03
		protected virtual BigInteger CalculateE(byte[] message)
		{
			return new BigInteger(1, message);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x00120BB4 File Offset: 0x0011EDB4
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x04001E07 RID: 7687
		private readonly IDsaKCalculator kCalculator = new RandomDsaKCalculator();

		// Token: 0x04001E08 RID: 7688
		private readonly SM3Digest digest = new SM3Digest();

		// Token: 0x04001E09 RID: 7689
		private readonly IDsaEncoding encoding;

		// Token: 0x04001E0A RID: 7690
		private ECDomainParameters ecParams;

		// Token: 0x04001E0B RID: 7691
		private ECPoint pubPoint;

		// Token: 0x04001E0C RID: 7692
		private ECKeyParameters ecKey;

		// Token: 0x04001E0D RID: 7693
		private byte[] z;
	}
}
