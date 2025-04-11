using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E7 RID: 1255
	public class RsaPrivateCrtKeyParameters : RsaKeyParameters
	{
		// Token: 0x0600303F RID: 12351 RVA: 0x00129598 File Offset: 0x00127798
		public RsaPrivateCrtKeyParameters(BigInteger modulus, BigInteger publicExponent, BigInteger privateExponent, BigInteger p, BigInteger q, BigInteger dP, BigInteger dQ, BigInteger qInv) : base(true, modulus, privateExponent)
		{
			RsaPrivateCrtKeyParameters.ValidateValue(publicExponent, "publicExponent", "exponent");
			RsaPrivateCrtKeyParameters.ValidateValue(p, "p", "P value");
			RsaPrivateCrtKeyParameters.ValidateValue(q, "q", "Q value");
			RsaPrivateCrtKeyParameters.ValidateValue(dP, "dP", "DP value");
			RsaPrivateCrtKeyParameters.ValidateValue(dQ, "dQ", "DQ value");
			RsaPrivateCrtKeyParameters.ValidateValue(qInv, "qInv", "InverseQ value");
			this.e = publicExponent;
			this.p = p;
			this.q = q;
			this.dP = dP;
			this.dQ = dQ;
			this.qInv = qInv;
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x00129644 File Offset: 0x00127844
		public RsaPrivateCrtKeyParameters(RsaPrivateKeyStructure rsaPrivateKey) : this(rsaPrivateKey.Modulus, rsaPrivateKey.PublicExponent, rsaPrivateKey.PrivateExponent, rsaPrivateKey.Prime1, rsaPrivateKey.Prime2, rsaPrivateKey.Exponent1, rsaPrivateKey.Exponent2, rsaPrivateKey.Coefficient)
		{
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x00129687 File Offset: 0x00127887
		public BigInteger PublicExponent
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x0012968F File Offset: 0x0012788F
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x00129697 File Offset: 0x00127897
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x0012969F File Offset: 0x0012789F
		public BigInteger DP
		{
			get
			{
				return this.dP;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x001296A7 File Offset: 0x001278A7
		public BigInteger DQ
		{
			get
			{
				return this.dQ;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x001296AF File Offset: 0x001278AF
		public BigInteger QInv
		{
			get
			{
				return this.qInv;
			}
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x001296B8 File Offset: 0x001278B8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = obj as RsaPrivateCrtKeyParameters;
			return rsaPrivateCrtKeyParameters != null && (rsaPrivateCrtKeyParameters.DP.Equals(this.dP) && rsaPrivateCrtKeyParameters.DQ.Equals(this.dQ) && rsaPrivateCrtKeyParameters.Exponent.Equals(base.Exponent) && rsaPrivateCrtKeyParameters.Modulus.Equals(base.Modulus) && rsaPrivateCrtKeyParameters.P.Equals(this.p) && rsaPrivateCrtKeyParameters.Q.Equals(this.q) && rsaPrivateCrtKeyParameters.PublicExponent.Equals(this.e)) && rsaPrivateCrtKeyParameters.QInv.Equals(this.qInv);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x00129774 File Offset: 0x00127974
		public override int GetHashCode()
		{
			return this.DP.GetHashCode() ^ this.DQ.GetHashCode() ^ base.Exponent.GetHashCode() ^ base.Modulus.GetHashCode() ^ this.P.GetHashCode() ^ this.Q.GetHashCode() ^ this.PublicExponent.GetHashCode() ^ this.QInv.GetHashCode();
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x001297E0 File Offset: 0x001279E0
		private static void ValidateValue(BigInteger x, string name, string desc)
		{
			if (x == null)
			{
				throw new ArgumentNullException(name);
			}
			if (x.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA " + desc, name);
			}
		}

		// Token: 0x04001EEE RID: 7918
		private readonly BigInteger e;

		// Token: 0x04001EEF RID: 7919
		private readonly BigInteger p;

		// Token: 0x04001EF0 RID: 7920
		private readonly BigInteger q;

		// Token: 0x04001EF1 RID: 7921
		private readonly BigInteger dP;

		// Token: 0x04001EF2 RID: 7922
		private readonly BigInteger dQ;

		// Token: 0x04001EF3 RID: 7923
		private readonly BigInteger qInv;
	}
}
