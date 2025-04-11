using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Kdf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005B2 RID: 1458
	public class ECDHWithKdfBasicAgreement : ECDHBasicAgreement
	{
		// Token: 0x0600386C RID: 14444 RVA: 0x00165FBB File Offset: 0x001641BB
		public ECDHWithKdfBasicAgreement(string algorithm, IDerivationFunction kdf)
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

		// Token: 0x0600386D RID: 14445 RVA: 0x00165FF0 File Offset: 0x001641F0
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

		// Token: 0x0600386E RID: 14446 RVA: 0x00166058 File Offset: 0x00164258
		private byte[] BigIntToBytes(BigInteger r)
		{
			int byteLength = X9IntegerConverter.GetByteLength(this.privKey.Parameters.Curve);
			return X9IntegerConverter.IntegerToBytes(r, byteLength);
		}

		// Token: 0x040023F9 RID: 9209
		private readonly string algorithm;

		// Token: 0x040023FA RID: 9210
		private readonly IDerivationFunction kdf;
	}
}
