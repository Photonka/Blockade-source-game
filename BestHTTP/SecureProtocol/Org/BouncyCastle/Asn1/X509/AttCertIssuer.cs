using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000670 RID: 1648
	public class AttCertIssuer : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003D73 RID: 15731 RVA: 0x00176EE0 File Offset: 0x001750E0
		public static AttCertIssuer GetInstance(object obj)
		{
			if (obj is AttCertIssuer)
			{
				return (AttCertIssuer)obj;
			}
			if (obj is V2Form)
			{
				return new AttCertIssuer(V2Form.GetInstance(obj));
			}
			if (obj is GeneralNames)
			{
				return new AttCertIssuer((GeneralNames)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return new AttCertIssuer(V2Form.GetInstance((Asn1TaggedObject)obj, false));
			}
			if (obj is Asn1Sequence)
			{
				return new AttCertIssuer(GeneralNames.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x00176F6C File Offset: 0x0017516C
		public static AttCertIssuer GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AttCertIssuer.GetInstance(obj.GetObject());
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x00176F79 File Offset: 0x00175179
		public AttCertIssuer(GeneralNames names)
		{
			this.obj = names;
			this.choiceObj = this.obj.ToAsn1Object();
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x00176F99 File Offset: 0x00175199
		public AttCertIssuer(V2Form v2Form)
		{
			this.obj = v2Form;
			this.choiceObj = new DerTaggedObject(false, 0, this.obj);
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x00176FBB File Offset: 0x001751BB
		public Asn1Encodable Issuer
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x00176FC3 File Offset: 0x001751C3
		public override Asn1Object ToAsn1Object()
		{
			return this.choiceObj;
		}

		// Token: 0x0400263C RID: 9788
		internal readonly Asn1Encodable obj;

		// Token: 0x0400263D RID: 9789
		internal readonly Asn1Object choiceObj;
	}
}
