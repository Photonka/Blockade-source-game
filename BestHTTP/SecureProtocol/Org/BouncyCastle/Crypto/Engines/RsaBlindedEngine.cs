using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000574 RID: 1396
	public class RsaBlindedEngine : IAsymmetricBlockCipher
	{
		// Token: 0x06003525 RID: 13605 RVA: 0x0014998C File Offset: 0x00147B8C
		public RsaBlindedEngine() : this(new RsaCoreEngine())
		{
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x00149999 File Offset: 0x00147B99
		public RsaBlindedEngine(IRsa rsa)
		{
			this.core = rsa;
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x001499A8 File Offset: 0x00147BA8
		public virtual string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x001499B0 File Offset: 0x00147BB0
		public virtual void Init(bool forEncryption, ICipherParameters param)
		{
			this.core.Init(forEncryption, param);
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.key = (RsaKeyParameters)parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
				return;
			}
			this.key = (RsaKeyParameters)param;
			this.random = new SecureRandom();
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x00149A0E File Offset: 0x00147C0E
		public virtual int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x00149A1B File Offset: 0x00147C1B
		public virtual int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x00149A28 File Offset: 0x00147C28
		public virtual byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("RSA engine not initialised");
			}
			BigInteger bigInteger = this.core.ConvertInput(inBuf, inOff, inLen);
			BigInteger bigInteger4;
			if (this.key is RsaPrivateCrtKeyParameters)
			{
				RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)this.key;
				BigInteger publicExponent = rsaPrivateCrtKeyParameters.PublicExponent;
				if (publicExponent != null)
				{
					BigInteger modulus = rsaPrivateCrtKeyParameters.Modulus;
					BigInteger bigInteger2 = BigIntegers.CreateRandomInRange(BigInteger.One, modulus.Subtract(BigInteger.One), this.random);
					BigInteger input = bigInteger2.ModPow(publicExponent, modulus).Multiply(bigInteger).Mod(modulus);
					BigInteger bigInteger3 = this.core.ProcessBlock(input);
					BigInteger val = bigInteger2.ModInverse(modulus);
					bigInteger4 = bigInteger3.Multiply(val).Mod(modulus);
					if (!bigInteger.Equals(bigInteger4.ModPow(publicExponent, modulus)))
					{
						throw new InvalidOperationException("RSA engine faulty decryption/signing detected");
					}
				}
				else
				{
					bigInteger4 = this.core.ProcessBlock(bigInteger);
				}
			}
			else
			{
				bigInteger4 = this.core.ProcessBlock(bigInteger);
			}
			return this.core.ConvertOutput(bigInteger4);
		}

		// Token: 0x040021CD RID: 8653
		private readonly IRsa core;

		// Token: 0x040021CE RID: 8654
		private RsaKeyParameters key;

		// Token: 0x040021CF RID: 8655
		private SecureRandom random;
	}
}
