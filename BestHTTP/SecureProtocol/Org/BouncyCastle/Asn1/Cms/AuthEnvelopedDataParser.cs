using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200076A RID: 1898
	public class AuthEnvelopedDataParser
	{
		// Token: 0x06004471 RID: 17521 RVA: 0x001908E3 File Offset: 0x0018EAE3
		public AuthEnvelopedDataParser(Asn1SequenceParser seq)
		{
			this.seq = seq;
			this.version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x00190903 File Offset: 0x0018EB03
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x0019090C File Offset: 0x0018EB0C
		public OriginatorInfo GetOriginatorInfo()
		{
			this.originatorInfoCalled = true;
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this.nextObject).TagNo == 0)
			{
				IAsn1Convertible asn1Convertible = (Asn1SequenceParser)((Asn1TaggedObjectParser)this.nextObject).GetObjectParser(16, false);
				this.nextObject = null;
				return OriginatorInfo.GetInstance(asn1Convertible.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x00190983 File Offset: 0x0018EB83
		public Asn1SetParser GetRecipientInfos()
		{
			if (!this.originatorInfoCalled)
			{
				this.GetOriginatorInfo();
			}
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			Asn1SetParser result = (Asn1SetParser)this.nextObject;
			this.nextObject = null;
			return result;
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x001909BF File Offset: 0x0018EBBF
		public EncryptedContentInfoParser GetAuthEncryptedContentInfo()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return new EncryptedContentInfoParser(asn1SequenceParser);
			}
			return null;
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x001909FC File Offset: 0x0018EBFC
		public Asn1SetParser GetAuthAttrs()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser)
			{
				IAsn1Convertible asn1Convertible = this.nextObject;
				this.nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)asn1Convertible).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x00190A50 File Offset: 0x0018EC50
		public Asn1OctetString GetMac()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			IAsn1Convertible asn1Convertible = this.nextObject;
			this.nextObject = null;
			return Asn1OctetString.GetInstance(asn1Convertible.ToAsn1Object());
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x00190A84 File Offset: 0x0018EC84
		public Asn1SetParser GetUnauthAttrs()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				IAsn1Convertible asn1Convertible = this.nextObject;
				this.nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)asn1Convertible).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x04002BD7 RID: 11223
		private Asn1SequenceParser seq;

		// Token: 0x04002BD8 RID: 11224
		private DerInteger version;

		// Token: 0x04002BD9 RID: 11225
		private IAsn1Convertible nextObject;

		// Token: 0x04002BDA RID: 11226
		private bool originatorInfoCalled;
	}
}
