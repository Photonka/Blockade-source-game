using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200078A RID: 1930
	public class SignedData : Asn1Encodable
	{
		// Token: 0x0600455B RID: 17755 RVA: 0x00192C7B File Offset: 0x00190E7B
		public static SignedData GetInstance(object obj)
		{
			if (obj is SignedData)
			{
				return (SignedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x00192CBC File Offset: 0x00190EBC
		public SignedData(Asn1Set digestAlgorithms, ContentInfo contentInfo, Asn1Set certificates, Asn1Set crls, Asn1Set signerInfos)
		{
			this.version = this.CalculateVersion(contentInfo.ContentType, certificates, crls, signerInfos);
			this.digestAlgorithms = digestAlgorithms;
			this.contentInfo = contentInfo;
			this.certificates = certificates;
			this.crls = crls;
			this.signerInfos = signerInfos;
			this.crlsBer = (crls is BerSet);
			this.certsBer = (certificates is BerSet);
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x00192D2C File Offset: 0x00190F2C
		private DerInteger CalculateVersion(DerObjectIdentifier contentOid, Asn1Set certs, Asn1Set crls, Asn1Set signerInfs)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					if (obj is Asn1TaggedObject)
					{
						Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
						if (asn1TaggedObject.TagNo == 1)
						{
							flag3 = true;
						}
						else if (asn1TaggedObject.TagNo == 2)
						{
							flag4 = true;
						}
						else if (asn1TaggedObject.TagNo == 3)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag)
			{
				return SignedData.Version5;
			}
			if (crls != null)
			{
				using (IEnumerator enumerator = crls.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current is Asn1TaggedObject)
						{
							flag2 = true;
							break;
						}
					}
				}
			}
			if (flag2)
			{
				return SignedData.Version5;
			}
			if (flag4)
			{
				return SignedData.Version4;
			}
			if (flag3 || !CmsObjectIdentifiers.Data.Equals(contentOid) || this.CheckForVersion3(signerInfs))
			{
				return SignedData.Version3;
			}
			return SignedData.Version1;
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x00192E50 File Offset: 0x00191050
		private bool CheckForVersion3(Asn1Set signerInfs)
		{
			using (IEnumerator enumerator = signerInfs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (SignerInfo.GetInstance(enumerator.Current).Version.Value.IntValue == 3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x00192EB8 File Offset: 0x001910B8
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
				if (asn1Object is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Object;
					int tagNo = asn1TaggedObject.TagNo;
					if (tagNo != 0)
					{
						if (tagNo != 1)
						{
							throw new ArgumentException("unknown tag value " + asn1TaggedObject.TagNo);
						}
						this.crlsBer = (asn1TaggedObject is BerTaggedObject);
						this.crls = Asn1Set.GetInstance(asn1TaggedObject, false);
					}
					else
					{
						this.certsBer = (asn1TaggedObject is BerTaggedObject);
						this.certificates = Asn1Set.GetInstance(asn1TaggedObject, false);
					}
				}
				else
				{
					this.signerInfos = (Asn1Set)asn1Object;
				}
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x00192FB8 File Offset: 0x001911B8
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x00192FC0 File Offset: 0x001911C0
		public Asn1Set DigestAlgorithms
		{
			get
			{
				return this.digestAlgorithms;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x00192FC8 File Offset: 0x001911C8
		public ContentInfo EncapContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x00192FD0 File Offset: 0x001911D0
		public Asn1Set Certificates
		{
			get
			{
				return this.certificates;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x00192FD8 File Offset: 0x001911D8
		public Asn1Set CRLs
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x00192FE0 File Offset: 0x001911E0
		public Asn1Set SignerInfos
		{
			get
			{
				return this.signerInfos;
			}
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x00192FE8 File Offset: 0x001911E8
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
				if (this.certsBer)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new BerTaggedObject(false, 0, this.certificates)
					});
				}
				else
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 0, this.certificates)
					});
				}
			}
			if (this.crls != null)
			{
				if (this.crlsBer)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new BerTaggedObject(false, 1, this.crls)
					});
				}
				else
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 1, this.crls)
					});
				}
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.signerInfos
			});
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C3A RID: 11322
		private static readonly DerInteger Version1 = new DerInteger(1);

		// Token: 0x04002C3B RID: 11323
		private static readonly DerInteger Version3 = new DerInteger(3);

		// Token: 0x04002C3C RID: 11324
		private static readonly DerInteger Version4 = new DerInteger(4);

		// Token: 0x04002C3D RID: 11325
		private static readonly DerInteger Version5 = new DerInteger(5);

		// Token: 0x04002C3E RID: 11326
		private readonly DerInteger version;

		// Token: 0x04002C3F RID: 11327
		private readonly Asn1Set digestAlgorithms;

		// Token: 0x04002C40 RID: 11328
		private readonly ContentInfo contentInfo;

		// Token: 0x04002C41 RID: 11329
		private readonly Asn1Set certificates;

		// Token: 0x04002C42 RID: 11330
		private readonly Asn1Set crls;

		// Token: 0x04002C43 RID: 11331
		private readonly Asn1Set signerInfos;

		// Token: 0x04002C44 RID: 11332
		private readonly bool certsBer;

		// Token: 0x04002C45 RID: 11333
		private readonly bool crlsBer;
	}
}
