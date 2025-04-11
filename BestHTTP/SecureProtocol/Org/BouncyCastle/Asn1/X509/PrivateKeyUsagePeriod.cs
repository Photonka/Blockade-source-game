using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000696 RID: 1686
	public class PrivateKeyUsagePeriod : Asn1Encodable
	{
		// Token: 0x06003E9B RID: 16027 RVA: 0x0017AAB8 File Offset: 0x00178CB8
		public static PrivateKeyUsagePeriod GetInstance(object obj)
		{
			if (obj is PrivateKeyUsagePeriod)
			{
				return (PrivateKeyUsagePeriod)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PrivateKeyUsagePeriod((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return PrivateKeyUsagePeriod.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x0017AB1C File Offset: 0x00178D1C
		private PrivateKeyUsagePeriod(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				if (asn1TaggedObject.TagNo == 0)
				{
					this._notBefore = DerGeneralizedTime.GetInstance(asn1TaggedObject, false);
				}
				else if (asn1TaggedObject.TagNo == 1)
				{
					this._notAfter = DerGeneralizedTime.GetInstance(asn1TaggedObject, false);
				}
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x0017AB9C File Offset: 0x00178D9C
		public DerGeneralizedTime NotBefore
		{
			get
			{
				return this._notBefore;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x0017ABA4 File Offset: 0x00178DA4
		public DerGeneralizedTime NotAfter
		{
			get
			{
				return this._notAfter;
			}
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x0017ABAC File Offset: 0x00178DAC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this._notBefore != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this._notBefore)
				});
			}
			if (this._notAfter != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this._notAfter)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040026C8 RID: 9928
		private DerGeneralizedTime _notBefore;

		// Token: 0x040026C9 RID: 9929
		private DerGeneralizedTime _notAfter;
	}
}
