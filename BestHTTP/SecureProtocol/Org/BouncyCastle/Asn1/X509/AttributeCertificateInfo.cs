using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000674 RID: 1652
	public class AttributeCertificateInfo : Asn1Encodable
	{
		// Token: 0x06003D8F RID: 15759 RVA: 0x001772BE File Offset: 0x001754BE
		public static AttributeCertificateInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AttributeCertificateInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x001772CC File Offset: 0x001754CC
		public static AttributeCertificateInfo GetInstance(object obj)
		{
			if (obj is AttributeCertificateInfo)
			{
				return (AttributeCertificateInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeCertificateInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x0017730C File Offset: 0x0017550C
		private AttributeCertificateInfo(Asn1Sequence seq)
		{
			if (seq.Count < 7 || seq.Count > 9)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.version = DerInteger.GetInstance(seq[0]);
			this.holder = Holder.GetInstance(seq[1]);
			this.issuer = AttCertIssuer.GetInstance(seq[2]);
			this.signature = AlgorithmIdentifier.GetInstance(seq[3]);
			this.serialNumber = DerInteger.GetInstance(seq[4]);
			this.attrCertValidityPeriod = AttCertValidityPeriod.GetInstance(seq[5]);
			this.attributes = Asn1Sequence.GetInstance(seq[6]);
			for (int i = 7; i < seq.Count; i++)
			{
				Asn1Encodable asn1Encodable = seq[i];
				if (asn1Encodable is DerBitString)
				{
					this.issuerUniqueID = DerBitString.GetInstance(seq[i]);
				}
				else if (asn1Encodable is Asn1Sequence || asn1Encodable is X509Extensions)
				{
					this.extensions = X509Extensions.GetInstance(seq[i]);
				}
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x00177422 File Offset: 0x00175622
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x0017742A File Offset: 0x0017562A
		public Holder Holder
		{
			get
			{
				return this.holder;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x00177432 File Offset: 0x00175632
		public AttCertIssuer Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06003D95 RID: 15765 RVA: 0x0017743A File Offset: 0x0017563A
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06003D96 RID: 15766 RVA: 0x00177442 File Offset: 0x00175642
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06003D97 RID: 15767 RVA: 0x0017744A File Offset: 0x0017564A
		public AttCertValidityPeriod AttrCertValidityPeriod
		{
			get
			{
				return this.attrCertValidityPeriod;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x00177452 File Offset: 0x00175652
		public Asn1Sequence Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x0017745A File Offset: 0x0017565A
		public DerBitString IssuerUniqueID
		{
			get
			{
				return this.issuerUniqueID;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x00177462 File Offset: 0x00175662
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x0017746C File Offset: 0x0017566C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.holder,
				this.issuer,
				this.signature,
				this.serialNumber,
				this.attrCertValidityPeriod,
				this.attributes
			});
			if (this.issuerUniqueID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerUniqueID
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.extensions
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002645 RID: 9797
		internal readonly DerInteger version;

		// Token: 0x04002646 RID: 9798
		internal readonly Holder holder;

		// Token: 0x04002647 RID: 9799
		internal readonly AttCertIssuer issuer;

		// Token: 0x04002648 RID: 9800
		internal readonly AlgorithmIdentifier signature;

		// Token: 0x04002649 RID: 9801
		internal readonly DerInteger serialNumber;

		// Token: 0x0400264A RID: 9802
		internal readonly AttCertValidityPeriod attrCertValidityPeriod;

		// Token: 0x0400264B RID: 9803
		internal readonly Asn1Sequence attributes;

		// Token: 0x0400264C RID: 9804
		internal readonly DerBitString issuerUniqueID;

		// Token: 0x0400264D RID: 9805
		internal readonly X509Extensions extensions;
	}
}
