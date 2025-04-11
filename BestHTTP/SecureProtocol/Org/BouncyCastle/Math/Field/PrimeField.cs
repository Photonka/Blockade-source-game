using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field
{
	// Token: 0x02000305 RID: 773
	internal class PrimeField : IFiniteField
	{
		// Token: 0x06001D90 RID: 7568 RVA: 0x000E2979 File Offset: 0x000E0B79
		internal PrimeField(BigInteger characteristic)
		{
			this.characteristic = characteristic;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x000E2988 File Offset: 0x000E0B88
		public virtual BigInteger Characteristic
		{
			get
			{
				return this.characteristic;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual int Dimension
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000E2990 File Offset: 0x000E0B90
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			PrimeField primeField = obj as PrimeField;
			return primeField != null && this.characteristic.Equals(primeField.characteristic);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000E29C0 File Offset: 0x000E0BC0
		public override int GetHashCode()
		{
			return this.characteristic.GetHashCode();
		}

		// Token: 0x04001810 RID: 6160
		protected readonly BigInteger characteristic;
	}
}
