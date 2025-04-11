using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x02000794 RID: 1940
	public class CAKeyUpdAnnContent : Asn1Encodable
	{
		// Token: 0x060045AC RID: 17836 RVA: 0x00193D88 File Offset: 0x00191F88
		private CAKeyUpdAnnContent(Asn1Sequence seq)
		{
			this.oldWithNew = CmpCertificate.GetInstance(seq[0]);
			this.newWithOld = CmpCertificate.GetInstance(seq[1]);
			this.newWithNew = CmpCertificate.GetInstance(seq[2]);
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x00193DC6 File Offset: 0x00191FC6
		public static CAKeyUpdAnnContent GetInstance(object obj)
		{
			if (obj is CAKeyUpdAnnContent)
			{
				return (CAKeyUpdAnnContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CAKeyUpdAnnContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x00193E05 File Offset: 0x00192005
		public virtual CmpCertificate OldWithNew
		{
			get
			{
				return this.oldWithNew;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060045AF RID: 17839 RVA: 0x00193E0D File Offset: 0x0019200D
		public virtual CmpCertificate NewWithOld
		{
			get
			{
				return this.newWithOld;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x00193E15 File Offset: 0x00192015
		public virtual CmpCertificate NewWithNew
		{
			get
			{
				return this.newWithNew;
			}
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x00193E1D File Offset: 0x0019201D
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.oldWithNew,
				this.newWithOld,
				this.newWithNew
			});
		}

		// Token: 0x04002C64 RID: 11364
		private readonly CmpCertificate oldWithNew;

		// Token: 0x04002C65 RID: 11365
		private readonly CmpCertificate newWithOld;

		// Token: 0x04002C66 RID: 11366
		private readonly CmpCertificate newWithNew;
	}
}
