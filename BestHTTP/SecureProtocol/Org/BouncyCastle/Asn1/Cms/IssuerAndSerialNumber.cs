using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000777 RID: 1911
	public class IssuerAndSerialNumber : Asn1Encodable
	{
		// Token: 0x060044BD RID: 17597 RVA: 0x0019161C File Offset: 0x0018F81C
		public static IssuerAndSerialNumber GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			IssuerAndSerialNumber issuerAndSerialNumber = obj as IssuerAndSerialNumber;
			if (issuerAndSerialNumber != null)
			{
				return issuerAndSerialNumber;
			}
			return new IssuerAndSerialNumber(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x00191645 File Offset: 0x0018F845
		[Obsolete("Use GetInstance() instead")]
		public IssuerAndSerialNumber(Asn1Sequence seq)
		{
			this.name = X509Name.GetInstance(seq[0]);
			this.serialNumber = (DerInteger)seq[1];
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x00191671 File Offset: 0x0018F871
		public IssuerAndSerialNumber(X509Name name, BigInteger serialNumber)
		{
			this.name = name;
			this.serialNumber = new DerInteger(serialNumber);
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x0019168C File Offset: 0x0018F88C
		public IssuerAndSerialNumber(X509Name name, DerInteger serialNumber)
		{
			this.name = name;
			this.serialNumber = serialNumber;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060044C1 RID: 17601 RVA: 0x001916A2 File Offset: 0x0018F8A2
		public X509Name Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x001916AA File Offset: 0x0018F8AA
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x001916B2 File Offset: 0x0018F8B2
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.name,
				this.serialNumber
			});
		}

		// Token: 0x04002C0A RID: 11274
		private X509Name name;

		// Token: 0x04002C0B RID: 11275
		private DerInteger serialNumber;
	}
}
