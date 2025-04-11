using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004CD RID: 1229
	public class Gost3410Parameters : ICipherParameters
	{
		// Token: 0x06002FCF RID: 12239 RVA: 0x00128A24 File Offset: 0x00126C24
		public Gost3410Parameters(BigInteger p, BigInteger q, BigInteger a) : this(p, q, a, null)
		{
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00128A30 File Offset: 0x00126C30
		public Gost3410Parameters(BigInteger p, BigInteger q, BigInteger a, Gost3410ValidationParameters validation)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			this.p = p;
			this.q = q;
			this.a = a;
			this.validation = validation;
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x00128A8A File Offset: 0x00126C8A
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x00128A92 File Offset: 0x00126C92
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x00128A9A File Offset: 0x00126C9A
		public BigInteger A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x00128AA2 File Offset: 0x00126CA2
		public Gost3410ValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x00128AAC File Offset: 0x00126CAC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			Gost3410Parameters gost3410Parameters = obj as Gost3410Parameters;
			return gost3410Parameters != null && this.Equals(gost3410Parameters);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x00128AD2 File Offset: 0x00126CD2
		protected bool Equals(Gost3410Parameters other)
		{
			return this.p.Equals(other.p) && this.q.Equals(other.q) && this.a.Equals(other.a);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x00128B0D File Offset: 0x00126D0D
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.q.GetHashCode() ^ this.a.GetHashCode();
		}

		// Token: 0x04001EB8 RID: 7864
		private readonly BigInteger p;

		// Token: 0x04001EB9 RID: 7865
		private readonly BigInteger q;

		// Token: 0x04001EBA RID: 7866
		private readonly BigInteger a;

		// Token: 0x04001EBB RID: 7867
		private readonly Gost3410ValidationParameters validation;
	}
}
