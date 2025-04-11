using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000723 RID: 1827
	public class ContentHints : Asn1Encodable
	{
		// Token: 0x0600427D RID: 17021 RVA: 0x00189E50 File Offset: 0x00188050
		public static ContentHints GetInstance(object o)
		{
			if (o == null || o is ContentHints)
			{
				return (ContentHints)o;
			}
			if (o is Asn1Sequence)
			{
				return new ContentHints((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'ContentHints' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x00189EA0 File Offset: 0x001880A0
		private ContentHints(Asn1Sequence seq)
		{
			IAsn1Convertible asn1Convertible = seq[0];
			if (asn1Convertible.ToAsn1Object() is DerUtf8String)
			{
				this.contentDescription = DerUtf8String.GetInstance(asn1Convertible);
				this.contentType = DerObjectIdentifier.GetInstance(seq[1]);
				return;
			}
			this.contentType = DerObjectIdentifier.GetInstance(seq[0]);
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x00189EF9 File Offset: 0x001880F9
		public ContentHints(DerObjectIdentifier contentType)
		{
			this.contentType = contentType;
			this.contentDescription = null;
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x00189F0F File Offset: 0x0018810F
		public ContentHints(DerObjectIdentifier contentType, DerUtf8String contentDescription)
		{
			this.contentType = contentType;
			this.contentDescription = contentDescription;
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x00189F25 File Offset: 0x00188125
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x00189F2D File Offset: 0x0018812D
		public DerUtf8String ContentDescription
		{
			get
			{
				return this.contentDescription;
			}
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x00189F38 File Offset: 0x00188138
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.contentDescription != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.contentDescription
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.contentType
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002ACE RID: 10958
		private readonly DerUtf8String contentDescription;

		// Token: 0x04002ACF RID: 10959
		private readonly DerObjectIdentifier contentType;
	}
}
