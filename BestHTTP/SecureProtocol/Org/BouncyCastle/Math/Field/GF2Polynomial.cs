using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field
{
	// Token: 0x02000300 RID: 768
	internal class GF2Polynomial : IPolynomial
	{
		// Token: 0x06001D84 RID: 7556 RVA: 0x000E2905 File Offset: 0x000E0B05
		internal GF2Polynomial(int[] exponents)
		{
			this.exponents = Arrays.Clone(exponents);
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x000E2919 File Offset: 0x000E0B19
		public virtual int Degree
		{
			get
			{
				return this.exponents[this.exponents.Length - 1];
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000E292C File Offset: 0x000E0B2C
		public virtual int[] GetExponentsPresent()
		{
			return Arrays.Clone(this.exponents);
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x000E293C File Offset: 0x000E0B3C
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			GF2Polynomial gf2Polynomial = obj as GF2Polynomial;
			return gf2Polynomial != null && Arrays.AreEqual(this.exponents, gf2Polynomial.exponents);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000E296C File Offset: 0x000E0B6C
		public override int GetHashCode()
		{
			return Arrays.GetHashCode(this.exponents);
		}

		// Token: 0x0400180F RID: 6159
		protected readonly int[] exponents;
	}
}
