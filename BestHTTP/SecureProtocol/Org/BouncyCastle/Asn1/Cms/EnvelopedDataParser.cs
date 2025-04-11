using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000775 RID: 1909
	public class EnvelopedDataParser
	{
		// Token: 0x060044B2 RID: 17586 RVA: 0x00191428 File Offset: 0x0018F628
		public EnvelopedDataParser(Asn1SequenceParser seq)
		{
			this._seq = seq;
			this._version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x00191448 File Offset: 0x0018F648
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x00191450 File Offset: 0x0018F650
		public OriginatorInfo GetOriginatorInfo()
		{
			this._originatorInfoCalled = true;
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 0)
			{
				IAsn1Convertible asn1Convertible = (Asn1SequenceParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(16, false);
				this._nextObject = null;
				return OriginatorInfo.GetInstance(asn1Convertible.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x001914C7 File Offset: 0x0018F6C7
		public Asn1SetParser GetRecipientInfos()
		{
			if (!this._originatorInfoCalled)
			{
				this.GetOriginatorInfo();
			}
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			Asn1SetParser result = (Asn1SetParser)this._nextObject;
			this._nextObject = null;
			return result;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x00191503 File Offset: 0x0018F703
		public EncryptedContentInfoParser GetEncryptedContentInfo()
		{
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject != null)
			{
				Asn1SequenceParser seq = (Asn1SequenceParser)this._nextObject;
				this._nextObject = null;
				return new EncryptedContentInfoParser(seq);
			}
			return null;
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x00191540 File Offset: 0x0018F740
		public Asn1SetParser GetUnprotectedAttrs()
		{
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject != null)
			{
				IAsn1Convertible nextObject = this._nextObject;
				this._nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)nextObject).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x04002C05 RID: 11269
		private Asn1SequenceParser _seq;

		// Token: 0x04002C06 RID: 11270
		private DerInteger _version;

		// Token: 0x04002C07 RID: 11271
		private IAsn1Convertible _nextObject;

		// Token: 0x04002C08 RID: 11272
		private bool _originatorInfoCalled;
	}
}
