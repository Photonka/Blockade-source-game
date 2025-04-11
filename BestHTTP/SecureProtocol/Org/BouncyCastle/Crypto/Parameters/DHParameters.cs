using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B0 RID: 1200
	public class DHParameters : ICipherParameters
	{
		// Token: 0x06002F14 RID: 12052 RVA: 0x0012727F File Offset: 0x0012547F
		private static int GetDefaultMParam(int lParam)
		{
			if (lParam == 0)
			{
				return 160;
			}
			return Math.Min(lParam, 160);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x00127295 File Offset: 0x00125495
		public DHParameters(BigInteger p, BigInteger g) : this(p, g, null, 0)
		{
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x001272A1 File Offset: 0x001254A1
		public DHParameters(BigInteger p, BigInteger g, BigInteger q) : this(p, g, q, 0)
		{
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x001272AD File Offset: 0x001254AD
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int l) : this(p, g, q, DHParameters.GetDefaultMParam(l), l, null, null)
		{
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x001272C3 File Offset: 0x001254C3
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int m, int l) : this(p, g, q, m, l, null, null)
		{
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x001272D4 File Offset: 0x001254D4
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, BigInteger j, DHValidationParameters validation) : this(p, g, q, 160, 0, j, validation)
		{
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x001272EC File Offset: 0x001254EC
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int m, int l, BigInteger j, DHValidationParameters validation)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (!p.TestBit(0))
			{
				throw new ArgumentException("field must be an odd prime", "p");
			}
			if (g.CompareTo(BigInteger.Two) < 0 || g.CompareTo(p.Subtract(BigInteger.Two)) > 0)
			{
				throw new ArgumentException("generator must in the range [2, p - 2]", "g");
			}
			if (q != null && q.BitLength >= p.BitLength)
			{
				throw new ArgumentException("q too big to be a factor of (p-1)", "q");
			}
			if (m >= p.BitLength)
			{
				throw new ArgumentException("m value must be < bitlength of p", "m");
			}
			if (l != 0)
			{
				if (l >= p.BitLength)
				{
					throw new ArgumentException("when l value specified, it must be less than bitlength(p)", "l");
				}
				if (l < m)
				{
					throw new ArgumentException("when l value specified, it may not be less than m value", "l");
				}
			}
			if (j != null && j.CompareTo(BigInteger.Two) < 0)
			{
				throw new ArgumentException("subgroup factor must be >= 2", "j");
			}
			this.p = p;
			this.g = g;
			this.q = q;
			this.m = m;
			this.l = l;
			this.j = j;
			this.validation = validation;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x0012742D File Offset: 0x0012562D
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x00127435 File Offset: 0x00125635
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x0012743D File Offset: 0x0012563D
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x00127445 File Offset: 0x00125645
		public BigInteger J
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x0012744D File Offset: 0x0012564D
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x00127455 File Offset: 0x00125655
		public int L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x0012745D File Offset: 0x0012565D
		public DHValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x00127468 File Offset: 0x00125668
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHParameters dhparameters = obj as DHParameters;
			return dhparameters != null && this.Equals(dhparameters);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0012748E File Offset: 0x0012568E
		protected virtual bool Equals(DHParameters other)
		{
			return this.p.Equals(other.p) && this.g.Equals(other.g) && object.Equals(this.q, other.q);
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x001274CC File Offset: 0x001256CC
		public override int GetHashCode()
		{
			int num = this.p.GetHashCode() ^ this.g.GetHashCode();
			if (this.q != null)
			{
				num ^= this.q.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001E77 RID: 7799
		private const int DefaultMinimumLength = 160;

		// Token: 0x04001E78 RID: 7800
		private readonly BigInteger p;

		// Token: 0x04001E79 RID: 7801
		private readonly BigInteger g;

		// Token: 0x04001E7A RID: 7802
		private readonly BigInteger q;

		// Token: 0x04001E7B RID: 7803
		private readonly BigInteger j;

		// Token: 0x04001E7C RID: 7804
		private readonly int m;

		// Token: 0x04001E7D RID: 7805
		private readonly int l;

		// Token: 0x04001E7E RID: 7806
		private readonly DHValidationParameters validation;
	}
}
