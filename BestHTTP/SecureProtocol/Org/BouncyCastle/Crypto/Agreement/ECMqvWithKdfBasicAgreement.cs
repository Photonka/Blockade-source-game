using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Kdf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005B4 RID: 1460
	public class ECMqvWithKdfBasicAgreement : ECMqvBasicAgreement
	{
		// Token: 0x06003874 RID: 14452 RVA: 0x00166235 File Offset: 0x00164435
		public ECMqvWithKdfBasicAgreement(string algorithm, IDerivationFunction kdf)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (kdf == null)
			{
				throw new ArgumentNullException("kdf");
			}
			this.algorithm = algorithm;
			this.kdf = kdf;
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x00166268 File Offset: 0x00164468
		public override BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			BigInteger r = base.CalculateAgreement(pubKey);
			int defaultKeySize = GeneratorUtilities.GetDefaultKeySize(this.algorithm);
			DHKdfParameters parameters = new DHKdfParameters(new DerObjectIdentifier(this.algorithm), defaultKeySize, this.BigIntToBytes(r));
			this.kdf.Init(parameters);
			byte[] array = new byte[defaultKeySize / 8];
			this.kdf.GenerateBytes(array, 0, array.Length);
			return new BigInteger(1, array);
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x001662D0 File Offset: 0x001644D0
		private byte[] BigIntToBytes(BigInteger r)
		{
			int byteLength = X9IntegerConverter.GetByteLength(this.privParams.StaticPrivateKey.Parameters.Curve);
			return X9IntegerConverter.IntegerToBytes(r, byteLength);
		}

		// Token: 0x040023FC RID: 9212
		private readonly string algorithm;

		// Token: 0x040023FD RID: 9213
		private readonly IDerivationFunction kdf;
	}
}
