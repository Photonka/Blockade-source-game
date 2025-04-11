using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B7 RID: 1207
	public class DsaParameters : ICipherParameters
	{
		// Token: 0x06002F46 RID: 12102 RVA: 0x0012786E File Offset: 0x00125A6E
		public DsaParameters(BigInteger p, BigInteger q, BigInteger g) : this(p, q, g, null)
		{
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0012787C File Offset: 0x00125A7C
		public DsaParameters(BigInteger p, BigInteger q, BigInteger g, DsaValidationParameters parameters)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			this.p = p;
			this.q = q;
			this.g = g;
			this.validation = parameters;
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x001278D6 File Offset: 0x00125AD6
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x001278DE File Offset: 0x00125ADE
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x001278E6 File Offset: 0x00125AE6
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x001278EE File Offset: 0x00125AEE
		public DsaValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x001278F8 File Offset: 0x00125AF8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaParameters dsaParameters = obj as DsaParameters;
			return dsaParameters != null && this.Equals(dsaParameters);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x0012791E File Offset: 0x00125B1E
		protected bool Equals(DsaParameters other)
		{
			return this.p.Equals(other.p) && this.q.Equals(other.q) && this.g.Equals(other.g);
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x00127959 File Offset: 0x00125B59
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.q.GetHashCode() ^ this.g.GetHashCode();
		}

		// Token: 0x04001E8C RID: 7820
		private readonly BigInteger p;

		// Token: 0x04001E8D RID: 7821
		private readonly BigInteger q;

		// Token: 0x04001E8E RID: 7822
		private readonly BigInteger g;

		// Token: 0x04001E8F RID: 7823
		private readonly DsaValidationParameters validation;
	}
}
