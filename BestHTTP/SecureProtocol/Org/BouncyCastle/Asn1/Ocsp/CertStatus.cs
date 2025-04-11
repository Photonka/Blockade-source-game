using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F3 RID: 1779
	public class CertStatus : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004163 RID: 16739 RVA: 0x001859E1 File Offset: 0x00183BE1
		public CertStatus()
		{
			this.tagNo = 0;
			this.value = DerNull.Instance;
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x001859FB File Offset: 0x00183BFB
		public CertStatus(RevokedInfo info)
		{
			this.tagNo = 1;
			this.value = info;
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x00185A11 File Offset: 0x00183C11
		public CertStatus(int tagNo, Asn1Encodable value)
		{
			this.tagNo = tagNo;
			this.value = value;
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x00185A28 File Offset: 0x00183C28
		public CertStatus(Asn1TaggedObject choice)
		{
			this.tagNo = choice.TagNo;
			switch (choice.TagNo)
			{
			case 0:
			case 2:
				this.value = DerNull.Instance;
				return;
			case 1:
				this.value = RevokedInfo.GetInstance(choice, false);
				return;
			default:
				throw new ArgumentException("Unknown tag encountered: " + choice.TagNo);
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x00185A98 File Offset: 0x00183C98
		public static CertStatus GetInstance(object obj)
		{
			if (obj == null || obj is CertStatus)
			{
				return (CertStatus)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new CertStatus((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x00185AE5 File Offset: 0x00183CE5
		public int TagNo
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x00185AED File Offset: 0x00183CED
		public Asn1Encodable Status
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x00185AF5 File Offset: 0x00183CF5
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.tagNo, this.value);
		}

		// Token: 0x0400296C RID: 10604
		private readonly int tagNo;

		// Token: 0x0400296D RID: 10605
		private readonly Asn1Encodable value;
	}
}
