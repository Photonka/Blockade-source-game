using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200074F RID: 1871
	public class CertId : Asn1Encodable
	{
		// Token: 0x0600439B RID: 17307 RVA: 0x0018E3F4 File Offset: 0x0018C5F4
		private CertId(Asn1Sequence seq)
		{
			this.issuer = GeneralName.GetInstance(seq[0]);
			this.serialNumber = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x0018E420 File Offset: 0x0018C620
		public static CertId GetInstance(object obj)
		{
			if (obj is CertId)
			{
				return (CertId)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertId((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x0018E45F File Offset: 0x0018C65F
		public static CertId GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return CertId.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600439E RID: 17310 RVA: 0x0018E46D File Offset: 0x0018C66D
		public virtual GeneralName Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600439F RID: 17311 RVA: 0x0018E475 File Offset: 0x0018C675
		public virtual DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x0018E47D File Offset: 0x0018C67D
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.issuer,
				this.serialNumber
			});
		}

		// Token: 0x04002B6D RID: 11117
		private readonly GeneralName issuer;

		// Token: 0x04002B6E RID: 11118
		private readonly DerInteger serialNumber;
	}
}
