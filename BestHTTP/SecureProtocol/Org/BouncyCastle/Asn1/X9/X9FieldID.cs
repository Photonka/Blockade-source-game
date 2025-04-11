using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200066B RID: 1643
	public class X9FieldID : Asn1Encodable
	{
		// Token: 0x06003D53 RID: 15699 RVA: 0x001765FF File Offset: 0x001747FF
		public X9FieldID(BigInteger primeP)
		{
			this.id = X9ObjectIdentifiers.PrimeField;
			this.parameters = new DerInteger(primeP);
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x0017661E File Offset: 0x0017481E
		public X9FieldID(int m, int k1) : this(m, k1, 0, 0)
		{
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x0017662C File Offset: 0x0017482C
		public X9FieldID(int m, int k1, int k2, int k3)
		{
			this.id = X9ObjectIdentifiers.CharacteristicTwoField;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(m)
			});
			if (k2 == 0)
			{
				if (k3 != 0)
				{
					throw new ArgumentException("inconsistent k values");
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					X9ObjectIdentifiers.TPBasis,
					new DerInteger(k1)
				});
			}
			else
			{
				if (k2 <= k1 || k3 <= k2)
				{
					throw new ArgumentException("inconsistent k values");
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					X9ObjectIdentifiers.PPBasis,
					new DerSequence(new Asn1Encodable[]
					{
						new DerInteger(k1),
						new DerInteger(k2),
						new DerInteger(k3)
					})
				});
			}
			this.parameters = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x001766EE File Offset: 0x001748EE
		private X9FieldID(Asn1Sequence seq)
		{
			this.id = DerObjectIdentifier.GetInstance(seq[0]);
			this.parameters = seq[1].ToAsn1Object();
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x0017671A File Offset: 0x0017491A
		public static X9FieldID GetInstance(object obj)
		{
			if (obj is X9FieldID)
			{
				return (X9FieldID)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new X9FieldID(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x0017673B File Offset: 0x0017493B
		public DerObjectIdentifier Identifier
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x00176743 File Offset: 0x00174943
		public Asn1Object Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x0017674B File Offset: 0x0017494B
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.id,
				this.parameters
			});
		}

		// Token: 0x040025F2 RID: 9714
		private readonly DerObjectIdentifier id;

		// Token: 0x040025F3 RID: 9715
		private readonly Asn1Object parameters;
	}
}
