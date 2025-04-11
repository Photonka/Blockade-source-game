using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BB RID: 1211
	public class ECDomainParameters
	{
		// Token: 0x06002F62 RID: 12130 RVA: 0x00127BBF File Offset: 0x00125DBF
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n) : this(curve, g, n, BigInteger.One, null)
		{
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x00127BD0 File Offset: 0x00125DD0
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h) : this(curve, g, n, h, null)
		{
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x00127BE0 File Offset: 0x00125DE0
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h, byte[] seed)
		{
			if (curve == null)
			{
				throw new ArgumentNullException("curve");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (n == null)
			{
				throw new ArgumentNullException("n");
			}
			this.curve = curve;
			this.g = ECDomainParameters.Validate(curve, g);
			this.n = n;
			this.h = h;
			this.seed = Arrays.Clone(seed);
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x00127C4D File Offset: 0x00125E4D
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x00127C55 File Offset: 0x00125E55
		public ECPoint G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x00127C5D File Offset: 0x00125E5D
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x00127C65 File Offset: 0x00125E65
		public BigInteger H
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x00127C70 File Offset: 0x00125E70
		public BigInteger HInv
		{
			get
			{
				BigInteger result;
				lock (this)
				{
					if (this.hInv == null)
					{
						this.hInv = this.h.ModInverse(this.n);
					}
					result = this.hInv;
				}
				return result;
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x00127CCC File Offset: 0x00125ECC
		public byte[] GetSeed()
		{
			return Arrays.Clone(this.seed);
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x00127CDC File Offset: 0x00125EDC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECDomainParameters ecdomainParameters = obj as ECDomainParameters;
			return ecdomainParameters != null && this.Equals(ecdomainParameters);
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x00127D04 File Offset: 0x00125F04
		protected virtual bool Equals(ECDomainParameters other)
		{
			return this.curve.Equals(other.curve) && this.g.Equals(other.g) && this.n.Equals(other.n) && this.h.Equals(other.h);
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x00127D5D File Offset: 0x00125F5D
		public override int GetHashCode()
		{
			return ((this.curve.GetHashCode() * 37 ^ this.g.GetHashCode()) * 37 ^ this.n.GetHashCode()) * 37 ^ this.h.GetHashCode();
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x00127D98 File Offset: 0x00125F98
		internal static ECPoint Validate(ECCurve c, ECPoint q)
		{
			if (q == null)
			{
				throw new ArgumentException("Point has null value", "q");
			}
			q = ECAlgorithms.ImportPoint(c, q).Normalize();
			if (q.IsInfinity)
			{
				throw new ArgumentException("Point at infinity", "q");
			}
			if (!q.IsValid())
			{
				throw new ArgumentException("Point not on curve", "q");
			}
			return q;
		}

		// Token: 0x04001E95 RID: 7829
		internal ECCurve curve;

		// Token: 0x04001E96 RID: 7830
		internal byte[] seed;

		// Token: 0x04001E97 RID: 7831
		internal ECPoint g;

		// Token: 0x04001E98 RID: 7832
		internal BigInteger n;

		// Token: 0x04001E99 RID: 7833
		internal BigInteger h;

		// Token: 0x04001E9A RID: 7834
		internal BigInteger hInv;
	}
}
