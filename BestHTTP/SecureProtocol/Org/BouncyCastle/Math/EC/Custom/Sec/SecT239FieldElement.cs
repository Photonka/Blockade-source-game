using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200038C RID: 908
	internal class SecT239FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06002512 RID: 9490 RVA: 0x00104F8B File Offset: 0x0010318B
		public SecT239FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 239)
			{
				throw new ArgumentException("value invalid for SecT239FieldElement", "x");
			}
			this.x = SecT239Field.FromBigInteger(x);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x00104FC8 File Offset: 0x001031C8
		public SecT239FieldElement()
		{
			this.x = Nat256.Create64();
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00104FDB File Offset: 0x001031DB
		protected internal SecT239FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x00104FEA File Offset: 0x001031EA
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne64(this.x);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x00104FF7 File Offset: 0x001031F7
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero64(this.x);
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00105004 File Offset: 0x00103204
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00105015 File Offset: 0x00103215
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger64(this.x);
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x00105022 File Offset: 0x00103222
		public override string FieldName
		{
			get
			{
				return "SecT239Field";
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x00105029 File Offset: 0x00103229
		public override int FieldSize
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00105030 File Offset: 0x00103230
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Add(this.x, ((SecT239FieldElement)b).x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00105060 File Offset: 0x00103260
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.AddOne(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00105088 File Offset: 0x00103288
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Multiply(this.x, ((SecT239FieldElement)b).x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x001050B8 File Offset: 0x001032B8
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT239FieldElement)b).x;
			ulong[] array2 = ((SecT239FieldElement)x).x;
			ulong[] y3 = ((SecT239FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT239Field.MultiplyAddToExt(array, y2, array3);
			SecT239Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat256.Create64();
			SecT239Field.Reduce(array3, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0010511C File Offset: 0x0010331C
		public override ECFieldElement Square()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Square(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00105144 File Offset: 0x00103344
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT239FieldElement)x).x;
			ulong[] y2 = ((SecT239FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT239Field.SquareAddToExt(array, array3);
			SecT239Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat256.Create64();
			SecT239Field.Reduce(array3, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00105198 File Offset: 0x00103398
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat256.Create64();
			SecT239Field.SquareN(this.x, pow, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x001051C4 File Offset: 0x001033C4
		public override int Trace()
		{
			return (int)SecT239Field.Trace(this.x);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x001051D4 File Offset: 0x001033D4
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Invert(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x001051FC File Offset: 0x001033FC
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Sqrt(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x00105029 File Offset: 0x00103229
		public virtual int M
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x00105221 File Offset: 0x00103421
		public virtual int K1
		{
			get
			{
				return 158;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00105228 File Offset: 0x00103428
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT239FieldElement);
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00105228 File Offset: 0x00103428
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT239FieldElement);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00105236 File Offset: 0x00103436
		public virtual bool Equals(SecT239FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq64(this.x, other.x));
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00105254 File Offset: 0x00103454
		public override int GetHashCode()
		{
			return 23900158 ^ Arrays.GetHashCode(this.x, 0, 4);
		}

		// Token: 0x04001970 RID: 6512
		protected internal readonly ulong[] x;
	}
}
