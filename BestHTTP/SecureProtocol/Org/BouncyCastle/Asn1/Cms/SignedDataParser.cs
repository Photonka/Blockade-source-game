using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200078B RID: 1931
	public class SignedDataParser
	{
		// Token: 0x06004568 RID: 17768 RVA: 0x001930FC File Offset: 0x001912FC
		public static SignedDataParser GetInstance(object o)
		{
			if (o is Asn1Sequence)
			{
				return new SignedDataParser(((Asn1Sequence)o).Parser);
			}
			if (o is Asn1SequenceParser)
			{
				return new SignedDataParser((Asn1SequenceParser)o);
			}
			throw new IOException("unknown object encountered: " + Platform.GetTypeName(o));
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x0019314B File Offset: 0x0019134B
		public SignedDataParser(Asn1SequenceParser seq)
		{
			this._seq = seq;
			this._version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x0019316B File Offset: 0x0019136B
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x00193173 File Offset: 0x00191373
		public Asn1SetParser GetDigestAlgorithms()
		{
			return (Asn1SetParser)this._seq.ReadObject();
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x00193185 File Offset: 0x00191385
		public ContentInfoParser GetEncapContentInfo()
		{
			return new ContentInfoParser((Asn1SequenceParser)this._seq.ReadObject());
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x0019319C File Offset: 0x0019139C
		public Asn1SetParser GetCertificates()
		{
			this._certsCalled = true;
			this._nextObject = this._seq.ReadObject();
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 0)
			{
				Asn1SetParser result = (Asn1SetParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(17, false);
				this._nextObject = null;
				return result;
			}
			return null;
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x00193204 File Offset: 0x00191404
		public Asn1SetParser GetCrls()
		{
			if (!this._certsCalled)
			{
				throw new IOException("GetCerts() has not been called.");
			}
			this._crlsCalled = true;
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 1)
			{
				Asn1SetParser result = (Asn1SetParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(17, false);
				this._nextObject = null;
				return result;
			}
			return null;
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x00193288 File Offset: 0x00191488
		public Asn1SetParser GetSignerInfos()
		{
			if (!this._certsCalled || !this._crlsCalled)
			{
				throw new IOException("GetCerts() and/or GetCrls() has not been called.");
			}
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			return (Asn1SetParser)this._nextObject;
		}

		// Token: 0x04002C46 RID: 11334
		private Asn1SequenceParser _seq;

		// Token: 0x04002C47 RID: 11335
		private DerInteger _version;

		// Token: 0x04002C48 RID: 11336
		private object _nextObject;

		// Token: 0x04002C49 RID: 11337
		private bool _certsCalled;

		// Token: 0x04002C4A RID: 11338
		private bool _crlsCalled;
	}
}
