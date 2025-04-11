using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006EA RID: 1770
	public class RsaPrivateKeyStructure : Asn1Encodable
	{
		// Token: 0x06004112 RID: 16658 RVA: 0x00184B6F File Offset: 0x00182D6F
		public static RsaPrivateKeyStructure GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return RsaPrivateKeyStructure.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x00184B7D File Offset: 0x00182D7D
		public static RsaPrivateKeyStructure GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is RsaPrivateKeyStructure)
			{
				return (RsaPrivateKeyStructure)obj;
			}
			return new RsaPrivateKeyStructure(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x00184BA0 File Offset: 0x00182DA0
		public RsaPrivateKeyStructure(BigInteger modulus, BigInteger publicExponent, BigInteger privateExponent, BigInteger prime1, BigInteger prime2, BigInteger exponent1, BigInteger exponent2, BigInteger coefficient)
		{
			this.modulus = modulus;
			this.publicExponent = publicExponent;
			this.privateExponent = privateExponent;
			this.prime1 = prime1;
			this.prime2 = prime2;
			this.exponent1 = exponent1;
			this.exponent2 = exponent2;
			this.coefficient = coefficient;
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x00184BF0 File Offset: 0x00182DF0
		[Obsolete("Use 'GetInstance' method(s) instead")]
		public RsaPrivateKeyStructure(Asn1Sequence seq)
		{
			if (((DerInteger)seq[0]).Value.IntValue != 0)
			{
				throw new ArgumentException("wrong version for RSA private key");
			}
			this.modulus = ((DerInteger)seq[1]).Value;
			this.publicExponent = ((DerInteger)seq[2]).Value;
			this.privateExponent = ((DerInteger)seq[3]).Value;
			this.prime1 = ((DerInteger)seq[4]).Value;
			this.prime2 = ((DerInteger)seq[5]).Value;
			this.exponent1 = ((DerInteger)seq[6]).Value;
			this.exponent2 = ((DerInteger)seq[7]).Value;
			this.coefficient = ((DerInteger)seq[8]).Value;
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x00184CDE File Offset: 0x00182EDE
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x00184CE6 File Offset: 0x00182EE6
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x00184CEE File Offset: 0x00182EEE
		public BigInteger PrivateExponent
		{
			get
			{
				return this.privateExponent;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x00184CF6 File Offset: 0x00182EF6
		public BigInteger Prime1
		{
			get
			{
				return this.prime1;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x00184CFE File Offset: 0x00182EFE
		public BigInteger Prime2
		{
			get
			{
				return this.prime2;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x00184D06 File Offset: 0x00182F06
		public BigInteger Exponent1
		{
			get
			{
				return this.exponent1;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x00184D0E File Offset: 0x00182F0E
		public BigInteger Exponent2
		{
			get
			{
				return this.exponent2;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x00184D16 File Offset: 0x00182F16
		public BigInteger Coefficient
		{
			get
			{
				return this.coefficient;
			}
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x00184D20 File Offset: 0x00182F20
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(0),
				new DerInteger(this.Modulus),
				new DerInteger(this.PublicExponent),
				new DerInteger(this.PrivateExponent),
				new DerInteger(this.Prime1),
				new DerInteger(this.Prime2),
				new DerInteger(this.Exponent1),
				new DerInteger(this.Exponent2),
				new DerInteger(this.Coefficient)
			});
		}

		// Token: 0x04002936 RID: 10550
		private readonly BigInteger modulus;

		// Token: 0x04002937 RID: 10551
		private readonly BigInteger publicExponent;

		// Token: 0x04002938 RID: 10552
		private readonly BigInteger privateExponent;

		// Token: 0x04002939 RID: 10553
		private readonly BigInteger prime1;

		// Token: 0x0400293A RID: 10554
		private readonly BigInteger prime2;

		// Token: 0x0400293B RID: 10555
		private readonly BigInteger exponent1;

		// Token: 0x0400293C RID: 10556
		private readonly BigInteger exponent2;

		// Token: 0x0400293D RID: 10557
		private readonly BigInteger coefficient;
	}
}
