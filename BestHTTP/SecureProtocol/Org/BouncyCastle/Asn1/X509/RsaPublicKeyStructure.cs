using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000699 RID: 1689
	public class RsaPublicKeyStructure : Asn1Encodable
	{
		// Token: 0x06003EAD RID: 16045 RVA: 0x0017AECD File Offset: 0x001790CD
		public static RsaPublicKeyStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return RsaPublicKeyStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x0017AEDB File Offset: 0x001790DB
		public static RsaPublicKeyStructure GetInstance(object obj)
		{
			if (obj == null || obj is RsaPublicKeyStructure)
			{
				return (RsaPublicKeyStructure)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsaPublicKeyStructure((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RsaPublicKeyStructure: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0017AF18 File Offset: 0x00179118
		public RsaPublicKeyStructure(BigInteger modulus, BigInteger publicExponent)
		{
			if (modulus == null)
			{
				throw new ArgumentNullException("modulus");
			}
			if (publicExponent == null)
			{
				throw new ArgumentNullException("publicExponent");
			}
			if (modulus.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA modulus", "modulus");
			}
			if (publicExponent.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA public exponent", "publicExponent");
			}
			this.modulus = modulus;
			this.publicExponent = publicExponent;
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0017AF88 File Offset: 0x00179188
		private RsaPublicKeyStructure(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.modulus = DerInteger.GetInstance(seq[0]).PositiveValue;
			this.publicExponent = DerInteger.GetInstance(seq[1]).PositiveValue;
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06003EB1 RID: 16049 RVA: 0x0017AFED File Offset: 0x001791ED
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x0017AFF5 File Offset: 0x001791F5
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x0017AFFD File Offset: 0x001791FD
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(this.Modulus),
				new DerInteger(this.PublicExponent)
			});
		}

		// Token: 0x040026D5 RID: 9941
		private BigInteger modulus;

		// Token: 0x040026D6 RID: 9942
		private BigInteger publicExponent;
	}
}
