using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x02000308 RID: 776
	public abstract class AbstractFpCurve : ECCurve
	{
		// Token: 0x06001DD2 RID: 7634 RVA: 0x000E3B22 File Offset: 0x000E1D22
		protected AbstractFpCurve(BigInteger q) : base(FiniteFields.GetPrimeField(q))
		{
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000E3B30 File Offset: 0x000E1D30
		public override bool IsValidFieldElement(BigInteger x)
		{
			return x != null && x.SignValue >= 0 && x.CompareTo(this.Field.Characteristic) < 0;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000E3B54 File Offset: 0x000E1D54
		protected override ECPoint DecompressPoint(int yTilde, BigInteger X1)
		{
			ECFieldElement ecfieldElement = this.FromBigInteger(X1);
			ECFieldElement ecfieldElement2 = ecfieldElement.Square().Add(this.A).Multiply(ecfieldElement).Add(this.B).Sqrt();
			if (ecfieldElement2 == null)
			{
				throw new ArgumentException("Invalid point compression");
			}
			if (ecfieldElement2.TestBitZero() != (yTilde == 1))
			{
				ecfieldElement2 = ecfieldElement2.Negate();
			}
			return this.CreateRawPoint(ecfieldElement, ecfieldElement2, true);
		}
	}
}
