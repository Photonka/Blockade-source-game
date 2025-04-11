using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005B1 RID: 1457
	public class ECDHCBasicAgreement : IBasicAgreement
	{
		// Token: 0x06003868 RID: 14440 RVA: 0x00165ED6 File Offset: 0x001640D6
		public virtual void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.privKey = (ECPrivateKeyParameters)parameters;
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x00165EF9 File Offset: 0x001640F9
		public virtual int GetFieldSize()
		{
			return (this.privKey.Parameters.Curve.FieldSize + 7) / 8;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x00165F14 File Offset: 0x00164114
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)pubKey;
			ECDomainParameters parameters = this.privKey.Parameters;
			if (!parameters.Equals(ecpublicKeyParameters.Parameters))
			{
				throw new InvalidOperationException("ECDHC public key has wrong domain parameters");
			}
			BigInteger b = parameters.H.Multiply(this.privKey.D).Mod(parameters.N);
			ECPoint ecpoint = ECAlgorithms.CleanPoint(parameters.Curve, ecpublicKeyParameters.Q);
			if (ecpoint.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid public key for ECDHC");
			}
			ECPoint ecpoint2 = ecpoint.Multiply(b).Normalize();
			if (ecpoint2.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid agreement value for ECDHC");
			}
			return ecpoint2.AffineXCoord.ToBigInteger();
		}

		// Token: 0x040023F8 RID: 9208
		private ECPrivateKeyParameters privKey;
	}
}
