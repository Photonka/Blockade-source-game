using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000561 RID: 1377
	public class ElGamalEngine : IAsymmetricBlockCipher
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x00143A1A File Offset: 0x00141C1A
		public virtual string AlgorithmName
		{
			get
			{
				return "ElGamal";
			}
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x00143A24 File Offset: 0x00141C24
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				this.key = (ElGamalKeyParameters)parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
			}
			else
			{
				this.key = (ElGamalKeyParameters)parameters;
				this.random = new SecureRandom();
			}
			this.forEncryption = forEncryption;
			this.bitSize = this.key.Parameters.P.BitLength;
			if (forEncryption)
			{
				if (!(this.key is ElGamalPublicKeyParameters))
				{
					throw new ArgumentException("ElGamalPublicKeyParameters are required for encryption.");
				}
			}
			else if (!(this.key is ElGamalPrivateKeyParameters))
			{
				throw new ArgumentException("ElGamalPrivateKeyParameters are required for decryption.");
			}
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x00143ACB File Offset: 0x00141CCB
		public virtual int GetInputBlockSize()
		{
			if (this.forEncryption)
			{
				return (this.bitSize - 1) / 8;
			}
			return 2 * ((this.bitSize + 7) / 8);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x00143AEC File Offset: 0x00141CEC
		public virtual int GetOutputBlockSize()
		{
			if (this.forEncryption)
			{
				return 2 * ((this.bitSize + 7) / 8);
			}
			return (this.bitSize - 1) / 8;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x00143B10 File Offset: 0x00141D10
		public virtual byte[] ProcessBlock(byte[] input, int inOff, int length)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("ElGamal engine not initialised");
			}
			int num = this.forEncryption ? ((this.bitSize - 1 + 7) / 8) : this.GetInputBlockSize();
			if (length > num)
			{
				throw new DataLengthException("input too large for ElGamal cipher.\n");
			}
			BigInteger p = this.key.Parameters.P;
			byte[] array;
			if (this.key is ElGamalPrivateKeyParameters)
			{
				int num2 = length / 2;
				BigInteger bigInteger = new BigInteger(1, input, inOff, num2);
				BigInteger val = new BigInteger(1, input, inOff + num2, num2);
				ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = (ElGamalPrivateKeyParameters)this.key;
				array = bigInteger.ModPow(p.Subtract(BigInteger.One).Subtract(elGamalPrivateKeyParameters.X), p).Multiply(val).Mod(p).ToByteArrayUnsigned();
			}
			else
			{
				BigInteger bigInteger2 = new BigInteger(1, input, inOff, length);
				if (bigInteger2.BitLength >= p.BitLength)
				{
					throw new DataLengthException("input too large for ElGamal cipher.\n");
				}
				ElGamalPublicKeyParameters elGamalPublicKeyParameters = (ElGamalPublicKeyParameters)this.key;
				BigInteger value = p.Subtract(BigInteger.Two);
				BigInteger bigInteger3;
				do
				{
					bigInteger3 = new BigInteger(p.BitLength, this.random);
				}
				while (bigInteger3.SignValue == 0 || bigInteger3.CompareTo(value) > 0);
				BigInteger bigInteger4 = this.key.Parameters.G.ModPow(bigInteger3, p);
				BigInteger bigInteger5 = bigInteger2.Multiply(elGamalPublicKeyParameters.Y.ModPow(bigInteger3, p)).Mod(p);
				array = new byte[this.GetOutputBlockSize()];
				byte[] array2 = bigInteger4.ToByteArrayUnsigned();
				byte[] array3 = bigInteger5.ToByteArrayUnsigned();
				array2.CopyTo(array, array.Length / 2 - array2.Length);
				array3.CopyTo(array, array.Length - array3.Length);
			}
			return array;
		}

		// Token: 0x04002144 RID: 8516
		private ElGamalKeyParameters key;

		// Token: 0x04002145 RID: 8517
		private SecureRandom random;

		// Token: 0x04002146 RID: 8518
		private bool forEncryption;

		// Token: 0x04002147 RID: 8519
		private int bitSize;
	}
}
