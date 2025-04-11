using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000380 RID: 896
	internal class SecT193FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06002443 RID: 9283 RVA: 0x00101C97 File Offset: 0x000FFE97
		public SecT193FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 193)
			{
				throw new ArgumentException("value invalid for SecT193FieldElement", "x");
			}
			this.x = SecT193Field.FromBigInteger(x);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00101CD4 File Offset: 0x000FFED4
		public SecT193FieldElement()
		{
			this.x = Nat256.Create64();
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00101CE7 File Offset: 0x000FFEE7
		protected internal SecT193FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x00101CF6 File Offset: 0x000FFEF6
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne64(this.x);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x00101D03 File Offset: 0x000FFF03
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero64(this.x);
			}
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00101D10 File Offset: 0x000FFF10
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) > 0UL;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00101D21 File Offset: 0x000FFF21
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger64(this.x);
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x00101D2E File Offset: 0x000FFF2E
		public override string FieldName
		{
			get
			{
				return "SecT193Field";
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x00101D35 File Offset: 0x000FFF35
		public override int FieldSize
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00101D3C File Offset: 0x000FFF3C
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Add(this.x, ((SecT193FieldElement)b).x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00101D6C File Offset: 0x000FFF6C
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.AddOne(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000E5231 File Offset: 0x000E3431
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00101D94 File Offset: 0x000FFF94
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Multiply(this.x, ((SecT193FieldElement)b).x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000E526F File Offset: 0x000E346F
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x00101DC4 File Offset: 0x000FFFC4
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT193FieldElement)b).x;
			ulong[] array2 = ((SecT193FieldElement)x).x;
			ulong[] y3 = ((SecT193FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT193Field.MultiplyAddToExt(array, y2, array3);
			SecT193Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat256.Create64();
			SecT193Field.Reduce(array3, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00101E28 File Offset: 0x00100028
		public override ECFieldElement Square()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Square(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000E535D File Offset: 0x000E355D
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x00101E50 File Offset: 0x00100050
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT193FieldElement)x).x;
			ulong[] y2 = ((SecT193FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT193Field.SquareAddToExt(array, array3);
			SecT193Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat256.Create64();
			SecT193Field.Reduce(array3, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00101EA4 File Offset: 0x001000A4
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat256.Create64();
			SecT193Field.SquareN(this.x, pow, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x00101ED0 File Offset: 0x001000D0
		public override int Trace()
		{
			return (int)SecT193Field.Trace(this.x);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00101EE0 File Offset: 0x001000E0
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Invert(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00101F08 File Offset: 0x00100108
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Sqrt(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x00101D35 File Offset: 0x000FFF35
		public virtual int M
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x00101F2D File Offset: 0x0010012D
		public virtual int K1
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00101F31 File Offset: 0x00100131
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT193FieldElement);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x00101F31 File Offset: 0x00100131
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT193FieldElement);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00101F3F File Offset: 0x0010013F
		public virtual bool Equals(SecT193FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq64(this.x, other.x));
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00101F5D File Offset: 0x0010015D
		public override int GetHashCode()
		{
			return 1930015 ^ Arrays.GetHashCode(this.x, 0, 4);
		}

		// Token: 0x0400195E RID: 6494
		protected internal readonly ulong[] x;
	}
}
