using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200066A RID: 1642
	public class X9FieldElement : Asn1Encodable
	{
		// Token: 0x06003D4E RID: 15694 RVA: 0x00176580 File Offset: 0x00174780
		public X9FieldElement(ECFieldElement f)
		{
			this.f = f;
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x0017658F File Offset: 0x0017478F
		[Obsolete("Will be removed")]
		public X9FieldElement(BigInteger p, Asn1OctetString s) : this(new FpFieldElement(p, new BigInteger(1, s.GetOctets())))
		{
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x001765A9 File Offset: 0x001747A9
		[Obsolete("Will be removed")]
		public X9FieldElement(int m, int k1, int k2, int k3, Asn1OctetString s) : this(new F2mFieldElement(m, k1, k2, k3, new BigInteger(1, s.GetOctets())))
		{
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06003D51 RID: 15697 RVA: 0x001765C8 File Offset: 0x001747C8
		public ECFieldElement Value
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x001765D0 File Offset: 0x001747D0
		public override Asn1Object ToAsn1Object()
		{
			int byteLength = X9IntegerConverter.GetByteLength(this.f);
			return new DerOctetString(X9IntegerConverter.IntegerToBytes(this.f.ToBigInteger(), byteLength));
		}

		// Token: 0x040025F1 RID: 9713
		private ECFieldElement f;
	}
}
