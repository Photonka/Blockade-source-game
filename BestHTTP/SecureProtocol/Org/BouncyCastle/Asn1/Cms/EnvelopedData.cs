using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000774 RID: 1908
	public class EnvelopedData : Asn1Encodable
	{
		// Token: 0x060044A6 RID: 17574 RVA: 0x001911A9 File Offset: 0x0018F3A9
		public EnvelopedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, EncryptedContentInfo encryptedContentInfo, Asn1Set unprotectedAttrs)
		{
			this.version = new DerInteger(EnvelopedData.CalculateVersion(originatorInfo, recipientInfos, unprotectedAttrs));
			this.originatorInfo = originatorInfo;
			this.recipientInfos = recipientInfos;
			this.encryptedContentInfo = encryptedContentInfo;
			this.unprotectedAttrs = unprotectedAttrs;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x001911E4 File Offset: 0x0018F3E4
		public EnvelopedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, EncryptedContentInfo encryptedContentInfo, Attributes unprotectedAttrs)
		{
			this.version = new DerInteger(EnvelopedData.CalculateVersion(originatorInfo, recipientInfos, Asn1Set.GetInstance(unprotectedAttrs)));
			this.originatorInfo = originatorInfo;
			this.recipientInfos = recipientInfos;
			this.encryptedContentInfo = encryptedContentInfo;
			this.unprotectedAttrs = Asn1Set.GetInstance(unprotectedAttrs);
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x00191234 File Offset: 0x0018F434
		[Obsolete("Use 'GetInstance' instead")]
		public EnvelopedData(Asn1Sequence seq)
		{
			int num = 0;
			this.version = (DerInteger)seq[num++];
			object obj = seq[num++];
			if (obj is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)obj, false);
				obj = seq[num++];
			}
			this.recipientInfos = Asn1Set.GetInstance(obj);
			this.encryptedContentInfo = EncryptedContentInfo.GetInstance(seq[num++]);
			if (seq.Count > num)
			{
				this.unprotectedAttrs = Asn1Set.GetInstance((Asn1TaggedObject)seq[num], false);
			}
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x001912D4 File Offset: 0x0018F4D4
		public static EnvelopedData GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return EnvelopedData.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x001912E2 File Offset: 0x0018F4E2
		public static EnvelopedData GetInstance(object obj)
		{
			if (obj is EnvelopedData)
			{
				return (EnvelopedData)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new EnvelopedData(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x00191303 File Offset: 0x0018F503
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x0019130B File Offset: 0x0018F50B
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x00191313 File Offset: 0x0018F513
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x0019131B File Offset: 0x0018F51B
		public EncryptedContentInfo EncryptedContentInfo
		{
			get
			{
				return this.encryptedContentInfo;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x00191323 File Offset: 0x0018F523
		public Asn1Set UnprotectedAttrs
		{
			get
			{
				return this.unprotectedAttrs;
			}
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x0019132C File Offset: 0x0018F52C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			if (this.originatorInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.originatorInfo)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.recipientInfos,
				this.encryptedContentInfo
			});
			if (this.unprotectedAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.unprotectedAttrs)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x001913BC File Offset: 0x0018F5BC
		public static int CalculateVersion(OriginatorInfo originatorInfo, Asn1Set recipientInfos, Asn1Set unprotectedAttrs)
		{
			if (originatorInfo != null || unprotectedAttrs != null)
			{
				return 2;
			}
			using (IEnumerator enumerator = recipientInfos.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (RecipientInfo.GetInstance(enumerator.Current).Version.Value.IntValue != 0)
					{
						return 2;
					}
				}
			}
			return 0;
		}

		// Token: 0x04002C00 RID: 11264
		private DerInteger version;

		// Token: 0x04002C01 RID: 11265
		private OriginatorInfo originatorInfo;

		// Token: 0x04002C02 RID: 11266
		private Asn1Set recipientInfos;

		// Token: 0x04002C03 RID: 11267
		private EncryptedContentInfo encryptedContentInfo;

		// Token: 0x04002C04 RID: 11268
		private Asn1Set unprotectedAttrs;
	}
}
