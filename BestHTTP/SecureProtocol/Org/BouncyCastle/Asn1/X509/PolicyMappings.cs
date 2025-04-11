using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000693 RID: 1683
	public class PolicyMappings : Asn1Encodable
	{
		// Token: 0x06003E8E RID: 16014 RVA: 0x0017A8E5 File Offset: 0x00178AE5
		public PolicyMappings(Asn1Sequence seq)
		{
			this.seq = seq;
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x0017A8F4 File Offset: 0x00178AF4
		public PolicyMappings(Hashtable mappings) : this(mappings)
		{
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x0017A900 File Offset: 0x00178B00
		public PolicyMappings(IDictionary mappings)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in mappings.Keys)
			{
				string text = (string)obj;
				string identifier = (string)mappings[text];
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSequence(new Asn1Encodable[]
					{
						new DerObjectIdentifier(text),
						new DerObjectIdentifier(identifier)
					})
				});
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x0017A9AC File Offset: 0x00178BAC
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x040026C2 RID: 9922
		private readonly Asn1Sequence seq;
	}
}
