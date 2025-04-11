using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000669 RID: 1641
	public class X9ECPoint : Asn1Encodable
	{
		// Token: 0x06003D46 RID: 15686 RVA: 0x001764A4 File Offset: 0x001746A4
		public X9ECPoint(ECPoint p) : this(p, false)
		{
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x001764AE File Offset: 0x001746AE
		public X9ECPoint(ECPoint p, bool compressed)
		{
			this.p = p.Normalize();
			this.encoding = new DerOctetString(p.GetEncoded(compressed));
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x001764D4 File Offset: 0x001746D4
		public X9ECPoint(ECCurve c, byte[] encoding)
		{
			this.c = c;
			this.encoding = new DerOctetString(Arrays.Clone(encoding));
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x001764F4 File Offset: 0x001746F4
		public X9ECPoint(ECCurve c, Asn1OctetString s) : this(c, s.GetOctets())
		{
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x00176503 File Offset: 0x00174703
		public byte[] GetPointEncoding()
		{
			return Arrays.Clone(this.encoding.GetOctets());
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06003D4B RID: 15691 RVA: 0x00176515 File Offset: 0x00174715
		public ECPoint Point
		{
			get
			{
				if (this.p == null)
				{
					this.p = this.c.DecodePoint(this.encoding.GetOctets()).Normalize();
				}
				return this.p;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x00176548 File Offset: 0x00174748
		public bool IsPointCompressed
		{
			get
			{
				byte[] octets = this.encoding.GetOctets();
				return octets != null && octets.Length != 0 && (octets[0] == 2 || octets[0] == 3);
			}
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x00176578 File Offset: 0x00174778
		public override Asn1Object ToAsn1Object()
		{
			return this.encoding;
		}

		// Token: 0x040025EE RID: 9710
		private readonly Asn1OctetString encoding;

		// Token: 0x040025EF RID: 9711
		private ECCurve c;

		// Token: 0x040025F0 RID: 9712
		private ECPoint p;
	}
}
