using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020006CC RID: 1740
	public class SmimeCapabilitiesAttribute : AttributeX509
	{
		// Token: 0x06004052 RID: 16466 RVA: 0x00181DF6 File Offset: 0x0017FFF6
		public SmimeCapabilitiesAttribute(SmimeCapabilityVector capabilities) : base(SmimeAttributes.SmimeCapabilities, new DerSet(new DerSequence(capabilities.ToAsn1EncodableVector())))
		{
		}
	}
}
