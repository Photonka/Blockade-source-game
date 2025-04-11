using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006DF RID: 1759
	public class KeyDerivationFunc : AlgorithmIdentifier
	{
		// Token: 0x060040C5 RID: 16581 RVA: 0x001835CC File Offset: 0x001817CC
		internal KeyDerivationFunc(Asn1Sequence seq) : base(seq)
		{
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x0018344B File Offset: 0x0018164B
		public KeyDerivationFunc(DerObjectIdentifier id, Asn1Encodable parameters) : base(id, parameters)
		{
		}
	}
}
