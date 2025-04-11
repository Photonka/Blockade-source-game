using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E6 RID: 1254
	public class RsaKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06003038 RID: 12344 RVA: 0x0012941B File Offset: 0x0012761B
		private static BigInteger Validate(BigInteger modulus)
		{
			if ((modulus.IntValue & 1) == 0)
			{
				throw new ArgumentException("RSA modulus is even", "modulus");
			}
			if (!modulus.Gcd(RsaKeyParameters.SmallPrimesProduct).Equals(BigInteger.One))
			{
				throw new ArgumentException("RSA modulus has a small prime factor");
			}
			return modulus;
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x0012945C File Offset: 0x0012765C
		public RsaKeyParameters(bool isPrivate, BigInteger modulus, BigInteger exponent) : base(isPrivate)
		{
			if (modulus == null)
			{
				throw new ArgumentNullException("modulus");
			}
			if (exponent == null)
			{
				throw new ArgumentNullException("exponent");
			}
			if (modulus.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA modulus", "modulus");
			}
			if (exponent.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA exponent", "exponent");
			}
			if (!isPrivate && (exponent.IntValue & 1) == 0)
			{
				throw new ArgumentException("RSA publicExponent is even", "exponent");
			}
			this.modulus = RsaKeyParameters.Validate(modulus);
			this.exponent = exponent;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x001294EE File Offset: 0x001276EE
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x001294F6 File Offset: 0x001276F6
		public BigInteger Exponent
		{
			get
			{
				return this.exponent;
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x00129500 File Offset: 0x00127700
		public override bool Equals(object obj)
		{
			RsaKeyParameters rsaKeyParameters = obj as RsaKeyParameters;
			return rsaKeyParameters != null && (rsaKeyParameters.IsPrivate == base.IsPrivate && rsaKeyParameters.Modulus.Equals(this.modulus)) && rsaKeyParameters.Exponent.Equals(this.exponent);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x00129550 File Offset: 0x00127750
		public override int GetHashCode()
		{
			return this.modulus.GetHashCode() ^ this.exponent.GetHashCode() ^ base.IsPrivate.GetHashCode();
		}

		// Token: 0x04001EEB RID: 7915
		private static BigInteger SmallPrimesProduct = new BigInteger("8138E8A0FCF3A4E84A771D40FD305D7F4AA59306D7251DE54D98AF8FE95729A1F73D893FA424CD2EDC8636A6C3285E022B0E3866A565AE8108EED8591CD4FE8D2CE86165A978D719EBF647F362D33FCA29CD179FB42401CBAF3DF0C614056F9C8F3CFD51E474AFB6BC6974F78DB8ABA8E9E517FDED658591AB7502BD41849462F", 16);

		// Token: 0x04001EEC RID: 7916
		private readonly BigInteger modulus;

		// Token: 0x04001EED RID: 7917
		private readonly BigInteger exponent;
	}
}
