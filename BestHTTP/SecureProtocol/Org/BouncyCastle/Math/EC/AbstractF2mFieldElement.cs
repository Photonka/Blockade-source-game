using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x0200030F RID: 783
	public abstract class AbstractF2mFieldElement : ECFieldElement
	{
		// Token: 0x06001E3B RID: 7739 RVA: 0x000E4F84 File Offset: 0x000E3184
		public virtual ECFieldElement HalfTrace()
		{
			int fieldSize = this.FieldSize;
			if ((fieldSize & 1) == 0)
			{
				throw new InvalidOperationException("Half-trace only defined for odd m");
			}
			ECFieldElement ecfieldElement = this;
			ECFieldElement ecfieldElement2 = ecfieldElement;
			for (int i = 2; i < fieldSize; i += 2)
			{
				ecfieldElement = ecfieldElement.SquarePow(2);
				ecfieldElement2 = ecfieldElement2.Add(ecfieldElement);
			}
			return ecfieldElement2;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000E4FCC File Offset: 0x000E31CC
		public virtual int Trace()
		{
			int fieldSize = this.FieldSize;
			ECFieldElement ecfieldElement = this;
			ECFieldElement ecfieldElement2 = ecfieldElement;
			for (int i = 1; i < fieldSize; i++)
			{
				ecfieldElement = ecfieldElement.Square();
				ecfieldElement2 = ecfieldElement2.Add(ecfieldElement);
			}
			if (ecfieldElement2.IsZero)
			{
				return 0;
			}
			if (ecfieldElement2.IsOne)
			{
				return 1;
			}
			throw new InvalidOperationException("Internal error in trace calculation");
		}
	}
}
