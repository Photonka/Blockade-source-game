using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005B3 RID: 1459
	public class ECMqvBasicAgreement : IBasicAgreement
	{
		// Token: 0x0600386F RID: 14447 RVA: 0x00166082 File Offset: 0x00164282
		public virtual void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.privParams = (MqvPrivateParameters)parameters;
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x001660A5 File Offset: 0x001642A5
		public virtual int GetFieldSize()
		{
			return (this.privParams.StaticPrivateKey.Parameters.Curve.FieldSize + 7) / 8;
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x001660C8 File Offset: 0x001642C8
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			MqvPublicParameters mqvPublicParameters = (MqvPublicParameters)pubKey;
			ECPrivateKeyParameters staticPrivateKey = this.privParams.StaticPrivateKey;
			ECDomainParameters parameters = staticPrivateKey.Parameters;
			if (!parameters.Equals(mqvPublicParameters.StaticPublicKey.Parameters))
			{
				throw new InvalidOperationException("ECMQV public key components have wrong domain parameters");
			}
			ECPoint ecpoint = ECMqvBasicAgreement.CalculateMqvAgreement(parameters, staticPrivateKey, this.privParams.EphemeralPrivateKey, this.privParams.EphemeralPublicKey, mqvPublicParameters.StaticPublicKey, mqvPublicParameters.EphemeralPublicKey).Normalize();
			if (ecpoint.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid agreement value for MQV");
			}
			return ecpoint.AffineXCoord.ToBigInteger();
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x00166158 File Offset: 0x00164358
		private static ECPoint CalculateMqvAgreement(ECDomainParameters parameters, ECPrivateKeyParameters d1U, ECPrivateKeyParameters d2U, ECPublicKeyParameters Q2U, ECPublicKeyParameters Q1V, ECPublicKeyParameters Q2V)
		{
			BigInteger n = parameters.N;
			int num = (n.BitLength + 1) / 2;
			BigInteger m = BigInteger.One.ShiftLeft(num);
			ECCurve curve = parameters.Curve;
			ECPoint ecpoint = ECAlgorithms.CleanPoint(curve, Q2U.Q);
			ECPoint p = ECAlgorithms.CleanPoint(curve, Q1V.Q);
			ECPoint ecpoint2 = ECAlgorithms.CleanPoint(curve, Q2V.Q);
			BigInteger val = ecpoint.AffineXCoord.ToBigInteger().Mod(m).SetBit(num);
			BigInteger val2 = d1U.D.Multiply(val).Add(d2U.D).Mod(n);
			BigInteger bigInteger = ecpoint2.AffineXCoord.ToBigInteger().Mod(m).SetBit(num);
			BigInteger bigInteger2 = parameters.H.Multiply(val2).Mod(n);
			return ECAlgorithms.SumOfTwoMultiplies(p, bigInteger.Multiply(bigInteger2).Mod(n), ecpoint2, bigInteger2);
		}

		// Token: 0x040023FB RID: 9211
		protected internal MqvPrivateParameters privParams;
	}
}
