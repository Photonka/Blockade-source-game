using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000769 RID: 1897
	public class AuthEnvelopedData : Asn1Encodable
	{
		// Token: 0x06004465 RID: 17509 RVA: 0x00190640 File Offset: 0x0018E840
		public AuthEnvelopedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, EncryptedContentInfo authEncryptedContentInfo, Asn1Set authAttrs, Asn1OctetString mac, Asn1Set unauthAttrs)
		{
			this.version = new DerInteger(0);
			this.originatorInfo = originatorInfo;
			this.recipientInfos = recipientInfos;
			this.authEncryptedContentInfo = authEncryptedContentInfo;
			this.authAttrs = authAttrs;
			this.mac = mac;
			this.unauthAttrs = unauthAttrs;
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x0019068C File Offset: 0x0018E88C
		private AuthEnvelopedData(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Object asn1Object = seq[num++].ToAsn1Object();
			this.version = (DerInteger)asn1Object;
			asn1Object = seq[num++].ToAsn1Object();
			if (asn1Object is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)asn1Object, false);
				asn1Object = seq[num++].ToAsn1Object();
			}
			this.recipientInfos = Asn1Set.GetInstance(asn1Object);
			asn1Object = seq[num++].ToAsn1Object();
			this.authEncryptedContentInfo = EncryptedContentInfo.GetInstance(asn1Object);
			asn1Object = seq[num++].ToAsn1Object();
			if (asn1Object is Asn1TaggedObject)
			{
				this.authAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Object, false);
				asn1Object = seq[num++].ToAsn1Object();
			}
			this.mac = Asn1OctetString.GetInstance(asn1Object);
			if (seq.Count > num)
			{
				asn1Object = seq[num++].ToAsn1Object();
				this.unauthAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Object, false);
			}
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x00190797 File Offset: 0x0018E997
		public static AuthEnvelopedData GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AuthEnvelopedData.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x001907A5 File Offset: 0x0018E9A5
		public static AuthEnvelopedData GetInstance(object obj)
		{
			if (obj == null || obj is AuthEnvelopedData)
			{
				return (AuthEnvelopedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthEnvelopedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid AuthEnvelopedData: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x001907E2 File Offset: 0x0018E9E2
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x001907EA File Offset: 0x0018E9EA
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x001907F2 File Offset: 0x0018E9F2
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x001907FA File Offset: 0x0018E9FA
		public EncryptedContentInfo AuthEncryptedContentInfo
		{
			get
			{
				return this.authEncryptedContentInfo;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x0600446D RID: 17517 RVA: 0x00190802 File Offset: 0x0018EA02
		public Asn1Set AuthAttrs
		{
			get
			{
				return this.authAttrs;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x0019080A File Offset: 0x0018EA0A
		public Asn1OctetString Mac
		{
			get
			{
				return this.mac;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x00190812 File Offset: 0x0018EA12
		public Asn1Set UnauthAttrs
		{
			get
			{
				return this.unauthAttrs;
			}
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x0019081C File Offset: 0x0018EA1C
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
				this.authEncryptedContentInfo
			});
			if (this.authAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.authAttrs)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.mac
			});
			if (this.unauthAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.unauthAttrs)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04002BD0 RID: 11216
		private DerInteger version;

		// Token: 0x04002BD1 RID: 11217
		private OriginatorInfo originatorInfo;

		// Token: 0x04002BD2 RID: 11218
		private Asn1Set recipientInfos;

		// Token: 0x04002BD3 RID: 11219
		private EncryptedContentInfo authEncryptedContentInfo;

		// Token: 0x04002BD4 RID: 11220
		private Asn1Set authAttrs;

		// Token: 0x04002BD5 RID: 11221
		private Asn1OctetString mac;

		// Token: 0x04002BD6 RID: 11222
		private Asn1Set unauthAttrs;
	}
}
