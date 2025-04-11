using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200062F RID: 1583
	public class BerTaggedObjectParser : Asn1TaggedObjectParser, IAsn1Convertible
	{
		// Token: 0x06003B93 RID: 15251 RVA: 0x00171910 File Offset: 0x0016FB10
		[Obsolete]
		internal BerTaggedObjectParser(int baseTag, int tagNumber, Stream contentStream) : this((baseTag & 32) != 0, tagNumber, new Asn1StreamParser(contentStream))
		{
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x00171926 File Offset: 0x0016FB26
		internal BerTaggedObjectParser(bool constructed, int tagNumber, Asn1StreamParser parser)
		{
			this._constructed = constructed;
			this._tagNumber = tagNumber;
			this._parser = parser;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x00171943 File Offset: 0x0016FB43
		public bool IsConstructed
		{
			get
			{
				return this._constructed;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x0017194B File Offset: 0x0016FB4B
		public int TagNo
		{
			get
			{
				return this._tagNumber;
			}
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x00171953 File Offset: 0x0016FB53
		public IAsn1Convertible GetObjectParser(int tag, bool isExplicit)
		{
			if (!isExplicit)
			{
				return this._parser.ReadImplicit(this._constructed, tag);
			}
			if (!this._constructed)
			{
				throw new IOException("Explicit tags must be constructed (see X.690 8.14.2)");
			}
			return this._parser.ReadObject();
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x0017198C File Offset: 0x0016FB8C
		public Asn1Object ToAsn1Object()
		{
			Asn1Object result;
			try
			{
				result = this._parser.ReadTaggedObject(this._constructed, this._tagNumber);
			}
			catch (IOException ex)
			{
				throw new Asn1ParsingException(ex.Message);
			}
			return result;
		}

		// Token: 0x04002591 RID: 9617
		private bool _constructed;

		// Token: 0x04002592 RID: 9618
		private int _tagNumber;

		// Token: 0x04002593 RID: 9619
		private Asn1StreamParser _parser;
	}
}
