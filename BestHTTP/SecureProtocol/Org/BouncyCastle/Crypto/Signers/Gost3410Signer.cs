using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000488 RID: 1160
	public class Gost3410Signer : IDsaExt, IDsa
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x00121AF9 File Offset: 0x0011FCF9
		public virtual string AlgorithmName
		{
			get
			{
				return "GOST3410";
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x00121B00 File Offset: 0x0011FD00
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			if (forSigning)
			{
				if (parameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
					this.random = parametersWithRandom.Random;
					parameters = parametersWithRandom.Parameters;
				}
				else
				{
					this.random = new SecureRandom();
				}
				if (!(parameters is Gost3410PrivateKeyParameters))
				{
					throw new InvalidKeyException("GOST3410 private key required for signing");
				}
				this.key = (Gost3410PrivateKeyParameters)parameters;
				return;
			}
			else
			{
				if (!(parameters is Gost3410PublicKeyParameters))
				{
					throw new InvalidKeyException("GOST3410 public key required for signing");
				}
				this.key = (Gost3410PublicKeyParameters)parameters;
				return;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x00121B7F File Offset: 0x0011FD7F
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.Q;
			}
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x00121B94 File Offset: 0x0011FD94
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			byte[] array = new byte[message.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = message[array.Length - 1 - num];
			}
			BigInteger val = new BigInteger(1, array);
			Gost3410Parameters parameters = this.key.Parameters;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(parameters.Q.BitLength, this.random);
			}
			while (bigInteger.CompareTo(parameters.Q) >= 0);
			BigInteger bigInteger2 = parameters.A.ModPow(bigInteger, parameters.P).Mod(parameters.Q);
			BigInteger bigInteger3 = bigInteger.Multiply(val).Add(((Gost3410PrivateKeyParameters)this.key).X.Multiply(bigInteger2)).Mod(parameters.Q);
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger3
			};
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x00121C64 File Offset: 0x0011FE64
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			byte[] array = new byte[message.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = message[array.Length - 1 - num];
			}
			BigInteger bigInteger = new BigInteger(1, array);
			Gost3410Parameters parameters = this.key.Parameters;
			if (r.SignValue < 0 || parameters.Q.CompareTo(r) <= 0)
			{
				return false;
			}
			if (s.SignValue < 0 || parameters.Q.CompareTo(s) <= 0)
			{
				return false;
			}
			BigInteger val = bigInteger.ModPow(parameters.Q.Subtract(BigInteger.Two), parameters.Q);
			BigInteger bigInteger2 = s.Multiply(val).Mod(parameters.Q);
			BigInteger bigInteger3 = parameters.Q.Subtract(r).Multiply(val).Mod(parameters.Q);
			bigInteger2 = parameters.A.ModPow(bigInteger2, parameters.P);
			bigInteger3 = ((Gost3410PublicKeyParameters)this.key).Y.ModPow(bigInteger3, parameters.P);
			return bigInteger2.Multiply(bigInteger3).Mod(parameters.P).Mod(parameters.Q).Equals(r);
		}

		// Token: 0x04001DB2 RID: 7602
		private Gost3410KeyParameters key;

		// Token: 0x04001DB3 RID: 7603
		private SecureRandom random;
	}
}
