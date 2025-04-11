using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x020004FD RID: 1277
	public class Asn1VerifierFactory : IVerifierFactory
	{
		// Token: 0x060030B4 RID: 12468 RVA: 0x0012AB40 File Offset: 0x00128D40
		public Asn1VerifierFactory(string algorithm, AsymmetricKeyParameter publicKey)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("Key for verifying must be public", "publicKey");
			}
			DerObjectIdentifier algorithmOid = X509Utilities.GetAlgorithmOid(algorithm);
			this.publicKey = publicKey;
			this.algID = X509Utilities.GetSigAlgID(algorithmOid, algorithm);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0012ABA2 File Offset: 0x00128DA2
		public Asn1VerifierFactory(AlgorithmIdentifier algorithm, AsymmetricKeyParameter publicKey)
		{
			this.publicKey = publicKey;
			this.algID = algorithm;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x0012ABB8 File Offset: 0x00128DB8
		public object AlgorithmDetails
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0012ABC0 File Offset: 0x00128DC0
		public IStreamCalculator CreateCalculator()
		{
			return new DefaultVerifierCalculator(SignerUtilities.InitSigner(X509Utilities.GetSignatureName(this.algID), false, this.publicKey, null));
		}

		// Token: 0x04001F1D RID: 7965
		private readonly AlgorithmIdentifier algID;

		// Token: 0x04001F1E RID: 7966
		private readonly AsymmetricKeyParameter publicKey;
	}
}
