using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006FA RID: 1786
	public class ResponderID : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004189 RID: 16777 RVA: 0x00186078 File Offset: 0x00184278
		public static ResponderID GetInstance(object obj)
		{
			if (obj == null || obj is ResponderID)
			{
				return (ResponderID)obj;
			}
			if (obj is DerOctetString)
			{
				return new ResponderID((DerOctetString)obj);
			}
			if (!(obj is Asn1TaggedObject))
			{
				return new ResponderID(X509Name.GetInstance(obj));
			}
			Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
			if (asn1TaggedObject.TagNo == 1)
			{
				return new ResponderID(X509Name.GetInstance(asn1TaggedObject, true));
			}
			return new ResponderID(Asn1OctetString.GetInstance(asn1TaggedObject, true));
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x001860E8 File Offset: 0x001842E8
		public ResponderID(Asn1OctetString id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x001860E8 File Offset: 0x001842E8
		public ResponderID(X509Name id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x00186105 File Offset: 0x00184305
		public static ResponderID GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return ResponderID.GetInstance(obj.GetObject());
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x00186112 File Offset: 0x00184312
		public virtual byte[] GetKeyHash()
		{
			if (this.id is Asn1OctetString)
			{
				return ((Asn1OctetString)this.id).GetOctets();
			}
			return null;
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600418E RID: 16782 RVA: 0x00186133 File Offset: 0x00184333
		public virtual X509Name Name
		{
			get
			{
				if (this.id is Asn1OctetString)
				{
					return null;
				}
				return X509Name.GetInstance(this.id);
			}
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0018614F File Offset: 0x0018434F
		public override Asn1Object ToAsn1Object()
		{
			if (this.id is Asn1OctetString)
			{
				return new DerTaggedObject(true, 2, this.id);
			}
			return new DerTaggedObject(true, 1, this.id);
		}

		// Token: 0x04002986 RID: 10630
		private readonly Asn1Encodable id;
	}
}
