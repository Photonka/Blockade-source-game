using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000667 RID: 1639
	public class X9ECParameters : Asn1Encodable
	{
		// Token: 0x06003D33 RID: 15667 RVA: 0x00176164 File Offset: 0x00174364
		public static X9ECParameters GetInstance(object obj)
		{
			if (obj is X9ECParameters)
			{
				return (X9ECParameters)obj;
			}
			if (obj != null)
			{
				return new X9ECParameters(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x00176188 File Offset: 0x00174388
		public X9ECParameters(Asn1Sequence seq)
		{
			if (!(seq[0] is DerInteger) || !((DerInteger)seq[0]).Value.Equals(BigInteger.One))
			{
				throw new ArgumentException("bad version in X9ECParameters");
			}
			this.n = ((DerInteger)seq[4]).Value;
			if (seq.Count == 6)
			{
				this.h = ((DerInteger)seq[5]).Value;
			}
			X9Curve x9Curve = new X9Curve(X9FieldID.GetInstance(seq[1]), this.n, this.h, Asn1Sequence.GetInstance(seq[2]));
			this.curve = x9Curve.Curve;
			object obj = seq[3];
			if (obj is X9ECPoint)
			{
				this.g = (X9ECPoint)obj;
			}
			else
			{
				this.g = new X9ECPoint(this.curve, (Asn1OctetString)obj);
			}
			this.seed = x9Curve.GetSeed();
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x0017627F File Offset: 0x0017447F
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n) : this(curve, g, n, null, null)
		{
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x0017628C File Offset: 0x0017448C
		public X9ECParameters(ECCurve curve, X9ECPoint g, BigInteger n, BigInteger h) : this(curve, g, n, h, null)
		{
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x0017629A File Offset: 0x0017449A
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h) : this(curve, g, n, h, null)
		{
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x001762A8 File Offset: 0x001744A8
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h, byte[] seed) : this(curve, new X9ECPoint(g), n, h, seed)
		{
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x001762BC File Offset: 0x001744BC
		public X9ECParameters(ECCurve curve, X9ECPoint g, BigInteger n, BigInteger h, byte[] seed)
		{
			this.curve = curve;
			this.g = g;
			this.n = n;
			this.h = h;
			this.seed = seed;
			if (ECAlgorithms.IsFpCurve(curve))
			{
				this.fieldID = new X9FieldID(curve.Field.Characteristic);
				return;
			}
			if (!ECAlgorithms.IsF2mCurve(curve))
			{
				throw new ArgumentException("'curve' is of an unsupported type");
			}
			int[] exponentsPresent = ((IPolynomialExtensionField)curve.Field).MinimalPolynomial.GetExponentsPresent();
			if (exponentsPresent.Length == 3)
			{
				this.fieldID = new X9FieldID(exponentsPresent[2], exponentsPresent[1]);
				return;
			}
			if (exponentsPresent.Length == 5)
			{
				this.fieldID = new X9FieldID(exponentsPresent[4], exponentsPresent[1], exponentsPresent[2], exponentsPresent[3]);
				return;
			}
			throw new ArgumentException("Only trinomial and pentomial curves are supported");
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06003D3A RID: 15674 RVA: 0x0017637C File Offset: 0x0017457C
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x00176384 File Offset: 0x00174584
		public ECPoint G
		{
			get
			{
				return this.g.Point;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x00176391 File Offset: 0x00174591
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x00176399 File Offset: 0x00174599
		public BigInteger H
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x001763A1 File Offset: 0x001745A1
		public byte[] GetSeed()
		{
			return this.seed;
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x001763A9 File Offset: 0x001745A9
		public X9Curve CurveEntry
		{
			get
			{
				return new X9Curve(this.curve, this.seed);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x001763BC File Offset: 0x001745BC
		public X9FieldID FieldIDEntry
		{
			get
			{
				return this.fieldID;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x001763C4 File Offset: 0x001745C4
		public X9ECPoint BaseEntry
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x001763CC File Offset: 0x001745CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(BigInteger.One),
				this.fieldID,
				new X9Curve(this.curve, this.seed),
				this.g,
				new DerInteger(this.n)
			});
			if (this.h != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerInteger(this.h)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040025E7 RID: 9703
		private X9FieldID fieldID;

		// Token: 0x040025E8 RID: 9704
		private ECCurve curve;

		// Token: 0x040025E9 RID: 9705
		private X9ECPoint g;

		// Token: 0x040025EA RID: 9706
		private BigInteger n;

		// Token: 0x040025EB RID: 9707
		private BigInteger h;

		// Token: 0x040025EC RID: 9708
		private byte[] seed;
	}
}
