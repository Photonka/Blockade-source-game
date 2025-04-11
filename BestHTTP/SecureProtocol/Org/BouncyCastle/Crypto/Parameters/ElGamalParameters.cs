using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C8 RID: 1224
	public class ElGamalParameters : ICipherParameters
	{
		// Token: 0x06002FB3 RID: 12211 RVA: 0x0012872F File Offset: 0x0012692F
		public ElGamalParameters(BigInteger p, BigInteger g) : this(p, g, 0)
		{
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x0012873A File Offset: 0x0012693A
		public ElGamalParameters(BigInteger p, BigInteger g, int l)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			this.p = p;
			this.g = g;
			this.l = l;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x00128773 File Offset: 0x00126973
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x0012877B File Offset: 0x0012697B
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x00128783 File Offset: 0x00126983
		public int L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x0012878C File Offset: 0x0012698C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalParameters elGamalParameters = obj as ElGamalParameters;
			return elGamalParameters != null && this.Equals(elGamalParameters);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x001287B2 File Offset: 0x001269B2
		protected bool Equals(ElGamalParameters other)
		{
			return this.p.Equals(other.p) && this.g.Equals(other.g) && this.l == other.l;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x001287EA File Offset: 0x001269EA
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.g.GetHashCode() ^ this.l;
		}

		// Token: 0x04001EAF RID: 7855
		private readonly BigInteger p;

		// Token: 0x04001EB0 RID: 7856
		private readonly BigInteger g;

		// Token: 0x04001EB1 RID: 7857
		private readonly int l;
	}
}
