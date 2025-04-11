using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x020004FE RID: 1278
	public class Asn1VerifierFactoryProvider : IVerifierFactoryProvider
	{
		// Token: 0x060030B8 RID: 12472 RVA: 0x0012ABDF File Offset: 0x00128DDF
		public Asn1VerifierFactoryProvider(AsymmetricKeyParameter publicKey)
		{
			this.publicKey = publicKey;
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0012ABEE File Offset: 0x00128DEE
		public IVerifierFactory CreateVerifierFactory(object algorithmDetails)
		{
			return new Asn1VerifierFactory((AlgorithmIdentifier)algorithmDetails, this.publicKey);
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x0012AB37 File Offset: 0x00128D37
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x04001F1F RID: 7967
		private readonly AsymmetricKeyParameter publicKey;
	}
}
