using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200047F RID: 1151
	public class ECGost3410Signer : IDsaExt, IDsa
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x00120C07 File Offset: 0x0011EE07
		public virtual string AlgorithmName
		{
			get
			{
				return "ECGOST3410";
			}
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x00120C10 File Offset: 0x0011EE10
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
				if (!(parameters is ECPrivateKeyParameters))
				{
					throw new InvalidKeyException("EC private key required for signing");
				}
				this.key = (ECPrivateKeyParameters)parameters;
				return;
			}
			else
			{
				if (!(parameters is ECPublicKeyParameters))
				{
					throw new InvalidKeyException("EC public key required for verification");
				}
				this.key = (ECPublicKeyParameters)parameters;
				return;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x00120C8F File Offset: 0x0011EE8F
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.N;
			}
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x00120CA4 File Offset: 0x0011EEA4
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			byte[] array = new byte[message.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = message[array.Length - 1 - num];
			}
			BigInteger val = new BigInteger(1, array);
			ECDomainParameters parameters = this.key.Parameters;
			BigInteger n = parameters.N;
			BigInteger d = ((ECPrivateKeyParameters)this.key).D;
			ECMultiplier ecmultiplier = this.CreateBasePointMultiplier();
			BigInteger bigInteger2;
			BigInteger bigInteger3;
			for (;;)
			{
				BigInteger bigInteger = new BigInteger(n.BitLength, this.random);
				if (bigInteger.SignValue != 0)
				{
					bigInteger2 = ecmultiplier.Multiply(parameters.G, bigInteger).Normalize().AffineXCoord.ToBigInteger().Mod(n);
					if (bigInteger2.SignValue != 0)
					{
						bigInteger3 = bigInteger.Multiply(val).Add(d.Multiply(bigInteger2)).Mod(n);
						if (bigInteger3.SignValue != 0)
						{
							break;
						}
					}
				}
			}
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger3
			};
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x00120D94 File Offset: 0x0011EF94
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			byte[] array = new byte[message.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = message[array.Length - 1 - num];
			}
			BigInteger bigInteger = new BigInteger(1, array);
			BigInteger n = this.key.Parameters.N;
			if (r.CompareTo(BigInteger.One) < 0 || r.CompareTo(n) >= 0)
			{
				return false;
			}
			if (s.CompareTo(BigInteger.One) < 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			BigInteger val = bigInteger.ModInverse(n);
			BigInteger a = s.Multiply(val).Mod(n);
			BigInteger b = n.Subtract(r).Multiply(val).Mod(n);
			ECPoint g = this.key.Parameters.G;
			ECPoint q = ((ECPublicKeyParameters)this.key).Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, a, q, b).Normalize();
			return !ecpoint.IsInfinity && ecpoint.AffineXCoord.ToBigInteger().Mod(n).Equals(r);
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00120BB4 File Offset: 0x0011EDB4
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x04001D8F RID: 7567
		private ECKeyParameters key;

		// Token: 0x04001D90 RID: 7568
		private SecureRandom random;
	}
}
