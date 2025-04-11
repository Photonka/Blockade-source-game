using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020006CE RID: 1742
	public class SmimeCapabilityVector
	{
		// Token: 0x0600405A RID: 16474 RVA: 0x00181F52 File Offset: 0x00180152
		public void AddCapability(DerObjectIdentifier capability)
		{
			this.capabilities.Add(new Asn1Encodable[]
			{
				new DerSequence(capability)
			});
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x00181F6E File Offset: 0x0018016E
		public void AddCapability(DerObjectIdentifier capability, int value)
		{
			this.capabilities.Add(new Asn1Encodable[]
			{
				new DerSequence(new Asn1Encodable[]
				{
					capability,
					new DerInteger(value)
				})
			});
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x00181F9C File Offset: 0x0018019C
		public void AddCapability(DerObjectIdentifier capability, Asn1Encodable parameters)
		{
			this.capabilities.Add(new Asn1Encodable[]
			{
				new DerSequence(new Asn1Encodable[]
				{
					capability,
					parameters
				})
			});
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x00181FC5 File Offset: 0x001801C5
		public Asn1EncodableVector ToAsn1EncodableVector()
		{
			return this.capabilities;
		}

		// Token: 0x04002838 RID: 10296
		private readonly Asn1EncodableVector capabilities = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
	}
}
