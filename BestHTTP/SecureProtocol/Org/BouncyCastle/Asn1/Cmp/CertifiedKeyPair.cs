using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x02000796 RID: 1942
	public class CertifiedKeyPair : Asn1Encodable
	{
		// Token: 0x060045B6 RID: 17846 RVA: 0x00193EE0 File Offset: 0x001920E0
		private CertifiedKeyPair(Asn1Sequence seq)
		{
			this.certOrEncCert = CertOrEncCert.GetInstance(seq[0]);
			if (seq.Count >= 2)
			{
				if (seq.Count == 2)
				{
					Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[1]);
					if (instance.TagNo == 0)
					{
						this.privateKey = EncryptedValue.GetInstance(instance.GetObject());
						return;
					}
					this.publicationInfo = PkiPublicationInfo.GetInstance(instance.GetObject());
					return;
				}
				else
				{
					this.privateKey = EncryptedValue.GetInstance(Asn1TaggedObject.GetInstance(seq[1]));
					this.publicationInfo = PkiPublicationInfo.GetInstance(Asn1TaggedObject.GetInstance(seq[2]));
				}
			}
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x00193F7E File Offset: 0x0019217E
		public static CertifiedKeyPair GetInstance(object obj)
		{
			if (obj is CertifiedKeyPair)
			{
				return (CertifiedKeyPair)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertifiedKeyPair((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x00193FBD File Offset: 0x001921BD
		public CertifiedKeyPair(CertOrEncCert certOrEncCert) : this(certOrEncCert, null, null)
		{
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x00193FC8 File Offset: 0x001921C8
		public CertifiedKeyPair(CertOrEncCert certOrEncCert, EncryptedValue privateKey, PkiPublicationInfo publicationInfo)
		{
			if (certOrEncCert == null)
			{
				throw new ArgumentNullException("certOrEncCert");
			}
			this.certOrEncCert = certOrEncCert;
			this.privateKey = privateKey;
			this.publicationInfo = publicationInfo;
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x00193FF3 File Offset: 0x001921F3
		public virtual CertOrEncCert CertOrEncCert
		{
			get
			{
				return this.certOrEncCert;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060045BB RID: 17851 RVA: 0x00193FFB File Offset: 0x001921FB
		public virtual EncryptedValue PrivateKey
		{
			get
			{
				return this.privateKey;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x00194003 File Offset: 0x00192203
		public virtual PkiPublicationInfo PublicationInfo
		{
			get
			{
				return this.publicationInfo;
			}
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x0019400C File Offset: 0x0019220C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certOrEncCert
			});
			if (this.privateKey != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.privateKey)
				});
			}
			if (this.publicationInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.publicationInfo)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C68 RID: 11368
		private readonly CertOrEncCert certOrEncCert;

		// Token: 0x04002C69 RID: 11369
		private readonly EncryptedValue privateKey;

		// Token: 0x04002C6A RID: 11370
		private readonly PkiPublicationInfo publicationInfo;
	}
}
