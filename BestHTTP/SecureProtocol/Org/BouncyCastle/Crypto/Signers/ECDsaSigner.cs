using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200047E RID: 1150
	public class ECDsaSigner : IDsaExt, IDsa
	{
		// Token: 0x06002DA2 RID: 11682 RVA: 0x0012087C File Offset: 0x0011EA7C
		public ECDsaSigner()
		{
			this.kCalculator = new RandomDsaKCalculator();
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x0012088F File Offset: 0x0011EA8F
		public ECDsaSigner(IDsaKCalculator kCalculator)
		{
			this.kCalculator = kCalculator;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x0012089E File Offset: 0x0011EA9E
		public virtual string AlgorithmName
		{
			get
			{
				return "ECDSA";
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x001208A8 File Offset: 0x0011EAA8
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			SecureRandom provided = null;
			if (forSigning)
			{
				if (parameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
					provided = parametersWithRandom.Random;
					parameters = parametersWithRandom.Parameters;
				}
				if (!(parameters is ECPrivateKeyParameters))
				{
					throw new InvalidKeyException("EC private key required for signing");
				}
				this.key = (ECPrivateKeyParameters)parameters;
			}
			else
			{
				if (!(parameters is ECPublicKeyParameters))
				{
					throw new InvalidKeyException("EC public key required for verification");
				}
				this.key = (ECPublicKeyParameters)parameters;
			}
			this.random = this.InitSecureRandom(forSigning && !this.kCalculator.IsDeterministic, provided);
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x00120937 File Offset: 0x0011EB37
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.N;
			}
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x0012094C File Offset: 0x0011EB4C
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			ECDomainParameters parameters = this.key.Parameters;
			BigInteger n = parameters.N;
			BigInteger bigInteger = this.CalculateE(n, message);
			BigInteger d = ((ECPrivateKeyParameters)this.key).D;
			if (this.kCalculator.IsDeterministic)
			{
				this.kCalculator.Init(n, d, message);
			}
			else
			{
				this.kCalculator.Init(n, this.random);
			}
			ECMultiplier ecmultiplier = this.CreateBasePointMultiplier();
			BigInteger bigInteger3;
			BigInteger bigInteger4;
			for (;;)
			{
				BigInteger bigInteger2 = this.kCalculator.NextK();
				bigInteger3 = ecmultiplier.Multiply(parameters.G, bigInteger2).Normalize().AffineXCoord.ToBigInteger().Mod(n);
				if (bigInteger3.SignValue != 0)
				{
					bigInteger4 = bigInteger2.ModInverse(n).Multiply(bigInteger.Add(d.Multiply(bigInteger3))).Mod(n);
					if (bigInteger4.SignValue != 0)
					{
						break;
					}
				}
			}
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x00120A38 File Offset: 0x0011EC38
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			BigInteger n = this.key.Parameters.N;
			if (r.SignValue < 1 || s.SignValue < 1 || r.CompareTo(n) >= 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			BigInteger bigInteger = this.CalculateE(n, message);
			BigInteger val = s.ModInverse(n);
			BigInteger a = bigInteger.Multiply(val).Mod(n);
			BigInteger b = r.Multiply(val).Mod(n);
			ECPoint g = this.key.Parameters.G;
			ECPoint q = ((ECPublicKeyParameters)this.key).Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, a, q, b);
			if (ecpoint.IsInfinity)
			{
				return false;
			}
			ECCurve curve = ecpoint.Curve;
			if (curve != null)
			{
				BigInteger cofactor = curve.Cofactor;
				if (cofactor != null && cofactor.CompareTo(ECDsaSigner.Eight) <= 0)
				{
					ECFieldElement denominator = this.GetDenominator(curve.CoordinateSystem, ecpoint);
					if (denominator != null && !denominator.IsZero)
					{
						ECFieldElement xcoord = ecpoint.XCoord;
						while (curve.IsValidFieldElement(r))
						{
							if (curve.FromBigInteger(r).Multiply(denominator).Equals(xcoord))
							{
								return true;
							}
							r = r.Add(n);
						}
						return false;
					}
				}
			}
			return ecpoint.Normalize().AffineXCoord.ToBigInteger().Mod(n).Equals(r);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x00120B80 File Offset: 0x0011ED80
		protected virtual BigInteger CalculateE(BigInteger n, byte[] message)
		{
			int num = message.Length * 8;
			BigInteger bigInteger = new BigInteger(1, message);
			if (n.BitLength < num)
			{
				bigInteger = bigInteger.ShiftRight(num - n.BitLength);
			}
			return bigInteger;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x00120BB4 File Offset: 0x0011EDB4
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x00120BBB File Offset: 0x0011EDBB
		protected virtual ECFieldElement GetDenominator(int coordinateSystem, ECPoint p)
		{
			switch (coordinateSystem)
			{
			case 1:
			case 6:
			case 7:
				return p.GetZCoord(0);
			case 2:
			case 3:
			case 4:
				return p.GetZCoord(0).Square();
			}
			return null;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x0012086B File Offset: 0x0011EA6B
		protected virtual SecureRandom InitSecureRandom(bool needed, SecureRandom provided)
		{
			if (!needed)
			{
				return null;
			}
			if (provided == null)
			{
				return new SecureRandom();
			}
			return provided;
		}

		// Token: 0x04001D8B RID: 7563
		private static readonly BigInteger Eight = BigInteger.ValueOf(8L);

		// Token: 0x04001D8C RID: 7564
		protected readonly IDsaKCalculator kCalculator;

		// Token: 0x04001D8D RID: 7565
		protected ECKeyParameters key;

		// Token: 0x04001D8E RID: 7566
		protected SecureRandom random;
	}
}
