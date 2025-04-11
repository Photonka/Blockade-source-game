using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020005AE RID: 1454
	public class DHBasicAgreement : IBasicAgreement
	{
		// Token: 0x0600385A RID: 14426 RVA: 0x001659A8 File Offset: 0x00163BA8
		public virtual void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			if (!(parameters is DHPrivateKeyParameters))
			{
				throw new ArgumentException("DHEngine expects DHPrivateKeyParameters");
			}
			this.key = (DHPrivateKeyParameters)parameters;
			this.dhParams = this.key.Parameters;
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x001659FA File Offset: 0x00163BFA
		public virtual int GetFieldSize()
		{
			return (this.key.Parameters.P.BitLength + 7) / 8;
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x00165A18 File Offset: 0x00163C18
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("Agreement algorithm not initialised");
			}
			DHPublicKeyParameters dhpublicKeyParameters = (DHPublicKeyParameters)pubKey;
			if (!dhpublicKeyParameters.Parameters.Equals(this.dhParams))
			{
				throw new ArgumentException("Diffie-Hellman public key has wrong parameters.");
			}
			BigInteger p = this.dhParams.P;
			BigInteger y = dhpublicKeyParameters.Y;
			if (y == null || y.CompareTo(BigInteger.One) <= 0 || y.CompareTo(p.Subtract(BigInteger.One)) >= 0)
			{
				throw new ArgumentException("Diffie-Hellman public key is weak");
			}
			BigInteger bigInteger = y.ModPow(this.key.X, p);
			if (bigInteger.Equals(BigInteger.One))
			{
				throw new InvalidOperationException("Shared key can't be 1");
			}
			return bigInteger;
		}

		// Token: 0x040023C3 RID: 9155
		private DHPrivateKeyParameters key;

		// Token: 0x040023C4 RID: 9156
		private DHParameters dhParams;
	}
}
