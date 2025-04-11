using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200073C RID: 1852
	public class OtherRevRefs : Asn1Encodable
	{
		// Token: 0x0600431F RID: 17183 RVA: 0x0018C20C File Offset: 0x0018A40C
		public static OtherRevRefs GetInstance(object obj)
		{
			if (obj == null || obj is OtherRevRefs)
			{
				return (OtherRevRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherRevRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherRevRefs' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x0018C25C File Offset: 0x0018A45C
		private OtherRevRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherRevRefType = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.otherRevRefs = seq[1].ToAsn1Object();
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x0018C2CF File Offset: 0x0018A4CF
		public OtherRevRefs(DerObjectIdentifier otherRevRefType, Asn1Encodable otherRevRefs)
		{
			if (otherRevRefType == null)
			{
				throw new ArgumentNullException("otherRevRefType");
			}
			if (otherRevRefs == null)
			{
				throw new ArgumentNullException("otherRevRefs");
			}
			this.otherRevRefType = otherRevRefType;
			this.otherRevRefs = otherRevRefs.ToAsn1Object();
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06004322 RID: 17186 RVA: 0x0018C306 File Offset: 0x0018A506
		public DerObjectIdentifier OtherRevRefType
		{
			get
			{
				return this.otherRevRefType;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x0018C30E File Offset: 0x0018A50E
		public Asn1Object OtherRevRefsObject
		{
			get
			{
				return this.otherRevRefs;
			}
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x0018C316 File Offset: 0x0018A516
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.otherRevRefType,
				this.otherRevRefs
			});
		}

		// Token: 0x04002B0E RID: 11022
		private readonly DerObjectIdentifier otherRevRefType;

		// Token: 0x04002B0F RID: 11023
		private readonly Asn1Object otherRevRefs;
	}
}
