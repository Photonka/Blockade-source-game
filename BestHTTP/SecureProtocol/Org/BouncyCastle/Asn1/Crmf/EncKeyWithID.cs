using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000757 RID: 1879
	public class EncKeyWithID : Asn1Encodable
	{
		// Token: 0x060043D7 RID: 17367 RVA: 0x0018EC6B File Offset: 0x0018CE6B
		public static EncKeyWithID GetInstance(object obj)
		{
			if (obj is EncKeyWithID)
			{
				return (EncKeyWithID)obj;
			}
			if (obj != null)
			{
				return new EncKeyWithID(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x0018EC8C File Offset: 0x0018CE8C
		private EncKeyWithID(Asn1Sequence seq)
		{
			this.privKeyInfo = PrivateKeyInfo.GetInstance(seq[0]);
			if (seq.Count <= 1)
			{
				this.identifier = null;
				return;
			}
			if (!(seq[1] is DerUtf8String))
			{
				this.identifier = GeneralName.GetInstance(seq[1]);
				return;
			}
			this.identifier = seq[1];
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x0018ECF0 File Offset: 0x0018CEF0
		public EncKeyWithID(PrivateKeyInfo privKeyInfo)
		{
			this.privKeyInfo = privKeyInfo;
			this.identifier = null;
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x0018ED06 File Offset: 0x0018CF06
		public EncKeyWithID(PrivateKeyInfo privKeyInfo, DerUtf8String str)
		{
			this.privKeyInfo = privKeyInfo;
			this.identifier = str;
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x0018ED06 File Offset: 0x0018CF06
		public EncKeyWithID(PrivateKeyInfo privKeyInfo, GeneralName generalName)
		{
			this.privKeyInfo = privKeyInfo;
			this.identifier = generalName;
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060043DC RID: 17372 RVA: 0x0018ED1C File Offset: 0x0018CF1C
		public virtual PrivateKeyInfo PrivateKey
		{
			get
			{
				return this.privKeyInfo;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x0018ED24 File Offset: 0x0018CF24
		public virtual bool HasIdentifier
		{
			get
			{
				return this.identifier != null;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x0018ED2F File Offset: 0x0018CF2F
		public virtual bool IsIdentifierUtf8String
		{
			get
			{
				return this.identifier is DerUtf8String;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x0018ED3F File Offset: 0x0018CF3F
		public virtual Asn1Encodable Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x0018ED48 File Offset: 0x0018CF48
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.privKeyInfo
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.identifier
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B94 RID: 11156
		private readonly PrivateKeyInfo privKeyInfo;

		// Token: 0x04002B95 RID: 11157
		private readonly Asn1Encodable identifier;
	}
}
