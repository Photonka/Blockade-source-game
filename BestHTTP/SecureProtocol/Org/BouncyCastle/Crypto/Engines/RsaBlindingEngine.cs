using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000575 RID: 1397
	public class RsaBlindingEngine : IAsymmetricBlockCipher
	{
		// Token: 0x0600352C RID: 13612 RVA: 0x00149B2A File Offset: 0x00147D2A
		public RsaBlindingEngine() : this(new RsaCoreEngine())
		{
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x00149B37 File Offset: 0x00147D37
		public RsaBlindingEngine(IRsa rsa)
		{
			this.core = rsa;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x001499A8 File Offset: 0x00147BA8
		public virtual string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x00149B48 File Offset: 0x00147D48
		public virtual void Init(bool forEncryption, ICipherParameters param)
		{
			RsaBlindingParameters rsaBlindingParameters;
			if (param is ParametersWithRandom)
			{
				rsaBlindingParameters = (RsaBlindingParameters)((ParametersWithRandom)param).Parameters;
			}
			else
			{
				rsaBlindingParameters = (RsaBlindingParameters)param;
			}
			this.core.Init(forEncryption, rsaBlindingParameters.PublicKey);
			this.forEncryption = forEncryption;
			this.key = rsaBlindingParameters.PublicKey;
			this.blindingFactor = rsaBlindingParameters.BlindingFactor;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x00149BA8 File Offset: 0x00147DA8
		public virtual int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x00149BB5 File Offset: 0x00147DB5
		public virtual int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x00149BC4 File Offset: 0x00147DC4
		public virtual byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			BigInteger bigInteger = this.core.ConvertInput(inBuf, inOff, inLen);
			if (this.forEncryption)
			{
				bigInteger = this.BlindMessage(bigInteger);
			}
			else
			{
				bigInteger = this.UnblindMessage(bigInteger);
			}
			return this.core.ConvertOutput(bigInteger);
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x00149C08 File Offset: 0x00147E08
		private BigInteger BlindMessage(BigInteger msg)
		{
			BigInteger bigInteger = this.blindingFactor;
			bigInteger = msg.Multiply(bigInteger.ModPow(this.key.Exponent, this.key.Modulus));
			return bigInteger.Mod(this.key.Modulus);
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x00149C54 File Offset: 0x00147E54
		private BigInteger UnblindMessage(BigInteger blindedMsg)
		{
			BigInteger modulus = this.key.Modulus;
			BigInteger val = this.blindingFactor.ModInverse(modulus);
			return blindedMsg.Multiply(val).Mod(modulus);
		}

		// Token: 0x040021D0 RID: 8656
		private readonly IRsa core;

		// Token: 0x040021D1 RID: 8657
		private RsaKeyParameters key;

		// Token: 0x040021D2 RID: 8658
		private BigInteger blindingFactor;

		// Token: 0x040021D3 RID: 8659
		private bool forEncryption;
	}
}
