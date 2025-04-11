using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005B0 RID: 1456
	public class ECDHBasicAgreement : IBasicAgreement
	{
		// Token: 0x06003864 RID: 14436 RVA: 0x00165DCC File Offset: 0x00163FCC
		public virtual void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.privKey = (ECPrivateKeyParameters)parameters;
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x00165DEF File Offset: 0x00163FEF
		public virtual int GetFieldSize()
		{
			return (this.privKey.Parameters.Curve.FieldSize + 7) / 8;
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x00165E0C File Offset: 0x0016400C
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)pubKey;
			ECDomainParameters parameters = this.privKey.Parameters;
			if (!parameters.Equals(ecpublicKeyParameters.Parameters))
			{
				throw new InvalidOperationException("ECDH public key has wrong domain parameters");
			}
			BigInteger bigInteger = this.privKey.D;
			ECPoint ecpoint = ECAlgorithms.CleanPoint(parameters.Curve, ecpublicKeyParameters.Q);
			if (ecpoint.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid public key for ECDH");
			}
			BigInteger h = parameters.H;
			if (!h.Equals(BigInteger.One))
			{
				bigInteger = parameters.HInv.Multiply(bigInteger).Mod(parameters.N);
				ecpoint = ECAlgorithms.ReferenceMultiply(ecpoint, h);
			}
			ECPoint ecpoint2 = ecpoint.Multiply(bigInteger).Normalize();
			if (ecpoint2.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid agreement value for ECDH");
			}
			return ecpoint2.AffineXCoord.ToBigInteger();
		}

		// Token: 0x040023F7 RID: 9207
		protected internal ECPrivateKeyParameters privKey;
	}
}
