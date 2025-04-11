using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000773 RID: 1907
	public class EncryptedData : Asn1Encodable
	{
		// Token: 0x0600449E RID: 17566 RVA: 0x00191028 File Offset: 0x0018F228
		public static EncryptedData GetInstance(object obj)
		{
			if (obj is EncryptedData)
			{
				return (EncryptedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid EncryptedData: " + Platform.GetTypeName(obj));
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x00191062 File Offset: 0x0018F262
		public EncryptedData(EncryptedContentInfo encInfo) : this(encInfo, null)
		{
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x0019106C File Offset: 0x0018F26C
		public EncryptedData(EncryptedContentInfo encInfo, Asn1Set unprotectedAttrs)
		{
			if (encInfo == null)
			{
				throw new ArgumentNullException("encInfo");
			}
			this.version = new DerInteger((unprotectedAttrs == null) ? 0 : 2);
			this.encryptedContentInfo = encInfo;
			this.unprotectedAttrs = unprotectedAttrs;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x001910A4 File Offset: 0x0018F2A4
		private EncryptedData(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.version = DerInteger.GetInstance(seq[0]);
			this.encryptedContentInfo = EncryptedContentInfo.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.unprotectedAttrs = Asn1Set.GetInstance((Asn1TaggedObject)seq[2], false);
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x0019113C File Offset: 0x0018F33C
		public virtual DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x00191144 File Offset: 0x0018F344
		public virtual EncryptedContentInfo EncryptedContentInfo
		{
			get
			{
				return this.encryptedContentInfo;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x0019114C File Offset: 0x0018F34C
		public virtual Asn1Set UnprotectedAttrs
		{
			get
			{
				return this.unprotectedAttrs;
			}
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x00191154 File Offset: 0x0018F354
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.encryptedContentInfo
			});
			if (this.unprotectedAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new BerTaggedObject(false, 1, this.unprotectedAttrs)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04002BFD RID: 11261
		private readonly DerInteger version;

		// Token: 0x04002BFE RID: 11262
		private readonly EncryptedContentInfo encryptedContentInfo;

		// Token: 0x04002BFF RID: 11263
		private readonly Asn1Set unprotectedAttrs;
	}
}
