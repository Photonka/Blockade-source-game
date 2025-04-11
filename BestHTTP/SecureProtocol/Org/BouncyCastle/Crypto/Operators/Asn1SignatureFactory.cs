using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x020004FC RID: 1276
	public class Asn1SignatureFactory : ISignatureFactory
	{
		// Token: 0x060030AF RID: 12463 RVA: 0x0012AA95 File Offset: 0x00128C95
		public Asn1SignatureFactory(string algorithm, AsymmetricKeyParameter privateKey) : this(algorithm, privateKey, null)
		{
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x0012AAA0 File Offset: 0x00128CA0
		public Asn1SignatureFactory(string algorithm, AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("Key for signing must be private", "privateKey");
			}
			DerObjectIdentifier algorithmOid = X509Utilities.GetAlgorithmOid(algorithm);
			this.algorithm = algorithm;
			this.privateKey = privateKey;
			this.random = random;
			this.algID = X509Utilities.GetSigAlgID(algorithmOid, algorithm);
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x0012AB10 File Offset: 0x00128D10
		public object AlgorithmDetails
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0012AB18 File Offset: 0x00128D18
		public IStreamCalculator CreateCalculator()
		{
			return new DefaultSignatureCalculator(SignerUtilities.InitSigner(this.algorithm, true, this.privateKey, this.random));
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x0012AB37 File Offset: 0x00128D37
		public static IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x04001F19 RID: 7961
		private readonly AlgorithmIdentifier algID;

		// Token: 0x04001F1A RID: 7962
		private readonly string algorithm;

		// Token: 0x04001F1B RID: 7963
		private readonly AsymmetricKeyParameter privateKey;

		// Token: 0x04001F1C RID: 7964
		private readonly SecureRandom random;
	}
}
