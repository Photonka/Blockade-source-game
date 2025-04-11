using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006ED RID: 1773
	public class SignedData : Asn1Encodable
	{
		// Token: 0x06004130 RID: 16688 RVA: 0x00185164 File Offset: 0x00183364
		public static SignedData GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			SignedData signedData = obj as SignedData;
			if (signedData != null)
			{
				return signedData;
			}
			return new SignedData(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x0018518D File Offset: 0x0018338D
		public SignedData(DerInteger _version, Asn1Set _digestAlgorithms, ContentInfo _contentInfo, Asn1Set _certificates, Asn1Set _crls, Asn1Set _signerInfos)
		{
			this.version = _version;
			this.digestAlgorithms = _digestAlgorithms;
			this.contentInfo = _contentInfo;
			this.certificates = _certificates;
			this.crls = _crls;
			this.signerInfos = _signerInfos;
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x001851C4 File Offset: 0x001833C4
		private SignedData(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.digestAlgorithms = (Asn1Set)enumerator.Current;
			enumerator.MoveNext();
			this.contentInfo = ContentInfo.GetInstance(enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1Object asn1Object = (Asn1Object)obj;
				if (asn1Object is DerTaggedObject)
				{
					DerTaggedObject derTaggedObject = (DerTaggedObject)asn1Object;
					int tagNo = derTaggedObject.TagNo;
					if (tagNo != 0)
					{
						if (tagNo != 1)
						{
							throw new ArgumentException("unknown tag value " + derTaggedObject.TagNo);
						}
						this.crls = Asn1Set.GetInstance(derTaggedObject, false);
					}
					else
					{
						this.certificates = Asn1Set.GetInstance(derTaggedObject, false);
					}
				}
				else
				{
					this.signerInfos = (Asn1Set)asn1Object;
				}
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x001852A0 File Offset: 0x001834A0
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x001852A8 File Offset: 0x001834A8
		public Asn1Set DigestAlgorithms
		{
			get
			{
				return this.digestAlgorithms;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x001852B0 File Offset: 0x001834B0
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06004136 RID: 16694 RVA: 0x001852B8 File Offset: 0x001834B8
		public Asn1Set Certificates
		{
			get
			{
				return this.certificates;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x001852C0 File Offset: 0x001834C0
		public Asn1Set Crls
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x001852C8 File Offset: 0x001834C8
		public Asn1Set SignerInfos
		{
			get
			{
				return this.signerInfos;
			}
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x001852D0 File Offset: 0x001834D0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.digestAlgorithms,
				this.contentInfo
			});
			if (this.certificates != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.certificates)
				});
			}
			if (this.crls != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.crls)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.signerInfos
			});
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04002949 RID: 10569
		private readonly DerInteger version;

		// Token: 0x0400294A RID: 10570
		private readonly Asn1Set digestAlgorithms;

		// Token: 0x0400294B RID: 10571
		private readonly ContentInfo contentInfo;

		// Token: 0x0400294C RID: 10572
		private readonly Asn1Set certificates;

		// Token: 0x0400294D RID: 10573
		private readonly Asn1Set crls;

		// Token: 0x0400294E RID: 10574
		private readonly Asn1Set signerInfos;
	}
}
