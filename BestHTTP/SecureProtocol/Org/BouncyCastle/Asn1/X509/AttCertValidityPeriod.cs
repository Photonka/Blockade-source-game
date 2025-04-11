using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000671 RID: 1649
	public class AttCertValidityPeriod : Asn1Encodable
	{
		// Token: 0x06003D79 RID: 15737 RVA: 0x00176FCC File Offset: 0x001751CC
		public static AttCertValidityPeriod GetInstance(object obj)
		{
			if (obj is AttCertValidityPeriod || obj == null)
			{
				return (AttCertValidityPeriod)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttCertValidityPeriod((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x00177019 File Offset: 0x00175219
		public static AttCertValidityPeriod GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AttCertValidityPeriod.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x00177028 File Offset: 0x00175228
		private AttCertValidityPeriod(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.notBeforeTime = DerGeneralizedTime.GetInstance(seq[0]);
			this.notAfterTime = DerGeneralizedTime.GetInstance(seq[1]);
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x00177083 File Offset: 0x00175283
		public AttCertValidityPeriod(DerGeneralizedTime notBeforeTime, DerGeneralizedTime notAfterTime)
		{
			this.notBeforeTime = notBeforeTime;
			this.notAfterTime = notAfterTime;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x00177099 File Offset: 0x00175299
		public DerGeneralizedTime NotBeforeTime
		{
			get
			{
				return this.notBeforeTime;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x001770A1 File Offset: 0x001752A1
		public DerGeneralizedTime NotAfterTime
		{
			get
			{
				return this.notAfterTime;
			}
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x001770A9 File Offset: 0x001752A9
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.notBeforeTime,
				this.notAfterTime
			});
		}

		// Token: 0x0400263E RID: 9790
		private readonly DerGeneralizedTime notBeforeTime;

		// Token: 0x0400263F RID: 9791
		private readonly DerGeneralizedTime notAfterTime;
	}
}
