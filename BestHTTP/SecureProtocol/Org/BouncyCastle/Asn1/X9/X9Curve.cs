using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000666 RID: 1638
	public class X9Curve : Asn1Encodable
	{
		// Token: 0x06003D2C RID: 15660 RVA: 0x00175E37 File Offset: 0x00174037
		public X9Curve(ECCurve curve) : this(curve, null)
		{
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x00175E44 File Offset: 0x00174044
		public X9Curve(ECCurve curve, byte[] seed)
		{
			if (curve == null)
			{
				throw new ArgumentNullException("curve");
			}
			this.curve = curve;
			this.seed = Arrays.Clone(seed);
			if (ECAlgorithms.IsFpCurve(curve))
			{
				this.fieldIdentifier = X9ObjectIdentifiers.PrimeField;
				return;
			}
			if (ECAlgorithms.IsF2mCurve(curve))
			{
				this.fieldIdentifier = X9ObjectIdentifiers.CharacteristicTwoField;
				return;
			}
			throw new ArgumentException("This type of ECCurve is not implemented");
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x00175EAA File Offset: 0x001740AA
		[Obsolete("Use constructor including order/cofactor")]
		public X9Curve(X9FieldID fieldID, Asn1Sequence seq) : this(fieldID, null, null, seq)
		{
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x00175EB8 File Offset: 0x001740B8
		public X9Curve(X9FieldID fieldID, BigInteger order, BigInteger cofactor, Asn1Sequence seq)
		{
			if (fieldID == null)
			{
				throw new ArgumentNullException("fieldID");
			}
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			this.fieldIdentifier = fieldID.Identifier;
			if (this.fieldIdentifier.Equals(X9ObjectIdentifiers.PrimeField))
			{
				BigInteger value = ((DerInteger)fieldID.Parameters).Value;
				BigInteger a = new BigInteger(1, Asn1OctetString.GetInstance(seq[0]).GetOctets());
				BigInteger b = new BigInteger(1, Asn1OctetString.GetInstance(seq[1]).GetOctets());
				this.curve = new FpCurve(value, a, b, order, cofactor);
			}
			else
			{
				if (!this.fieldIdentifier.Equals(X9ObjectIdentifiers.CharacteristicTwoField))
				{
					throw new ArgumentException("This type of ECCurve is not implemented");
				}
				DerSequence derSequence = (DerSequence)fieldID.Parameters;
				int intValue = ((DerInteger)derSequence[0]).Value.IntValue;
				object obj = (DerObjectIdentifier)derSequence[1];
				int k = 0;
				int k2 = 0;
				int intValue2;
				if (obj.Equals(X9ObjectIdentifiers.TPBasis))
				{
					intValue2 = ((DerInteger)derSequence[2]).Value.IntValue;
				}
				else
				{
					DerSequence derSequence2 = (DerSequence)derSequence[2];
					intValue2 = ((DerInteger)derSequence2[0]).Value.IntValue;
					k = ((DerInteger)derSequence2[1]).Value.IntValue;
					k2 = ((DerInteger)derSequence2[2]).Value.IntValue;
				}
				BigInteger a2 = new BigInteger(1, Asn1OctetString.GetInstance(seq[0]).GetOctets());
				BigInteger b2 = new BigInteger(1, Asn1OctetString.GetInstance(seq[1]).GetOctets());
				this.curve = new F2mCurve(intValue, intValue2, k, k2, a2, b2, order, cofactor);
			}
			if (seq.Count == 3)
			{
				this.seed = ((DerBitString)seq[2]).GetBytes();
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x001760A1 File Offset: 0x001742A1
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x001760A9 File Offset: 0x001742A9
		public byte[] GetSeed()
		{
			return Arrays.Clone(this.seed);
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x001760B8 File Offset: 0x001742B8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.fieldIdentifier.Equals(X9ObjectIdentifiers.PrimeField) || this.fieldIdentifier.Equals(X9ObjectIdentifiers.CharacteristicTwoField))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new X9FieldElement(this.curve.A).ToAsn1Object()
				});
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new X9FieldElement(this.curve.B).ToAsn1Object()
				});
			}
			if (this.seed != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerBitString(this.seed)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040025E4 RID: 9700
		private readonly ECCurve curve;

		// Token: 0x040025E5 RID: 9701
		private readonly byte[] seed;

		// Token: 0x040025E6 RID: 9702
		private readonly DerObjectIdentifier fieldIdentifier;
	}
}
