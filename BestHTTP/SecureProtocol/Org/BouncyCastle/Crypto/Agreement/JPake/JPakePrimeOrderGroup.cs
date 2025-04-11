using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x020005C2 RID: 1474
	public class JPakePrimeOrderGroup
	{
		// Token: 0x060038D3 RID: 14547 RVA: 0x00167E73 File Offset: 0x00166073
		public JPakePrimeOrderGroup(BigInteger p, BigInteger q, BigInteger g) : this(p, q, g, false)
		{
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x00167E80 File Offset: 0x00166080
		public JPakePrimeOrderGroup(BigInteger p, BigInteger q, BigInteger g, bool skipChecks)
		{
			JPakeUtilities.ValidateNotNull(p, "p");
			JPakeUtilities.ValidateNotNull(q, "q");
			JPakeUtilities.ValidateNotNull(g, "g");
			if (!skipChecks)
			{
				if (!p.Subtract(JPakeUtilities.One).Mod(q).Equals(JPakeUtilities.Zero))
				{
					throw new ArgumentException("p-1 must be evenly divisible by q");
				}
				if (g.CompareTo(BigInteger.Two) == -1 || g.CompareTo(p.Subtract(JPakeUtilities.One)) == 1)
				{
					throw new ArgumentException("g must be in [2, p-1]");
				}
				if (!g.ModPow(q, p).Equals(JPakeUtilities.One))
				{
					throw new ArgumentException("g^q mod p must equal 1");
				}
				if (!p.IsProbablePrime(20))
				{
					throw new ArgumentException("p must be prime");
				}
				if (!q.IsProbablePrime(20))
				{
					throw new ArgumentException("q must be prime");
				}
			}
			this.p = p;
			this.q = q;
			this.g = g;
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060038D5 RID: 14549 RVA: 0x00167F6E File Offset: 0x0016616E
		public virtual BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x00167F76 File Offset: 0x00166176
		public virtual BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x00167F7E File Offset: 0x0016617E
		public virtual BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x04002464 RID: 9316
		private readonly BigInteger p;

		// Token: 0x04002465 RID: 9317
		private readonly BigInteger q;

		// Token: 0x04002466 RID: 9318
		private readonly BigInteger g;
	}
}
