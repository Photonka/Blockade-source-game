using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000577 RID: 1399
	public class RsaEngine : IAsymmetricBlockCipher
	{
		// Token: 0x0600353D RID: 13629 RVA: 0x00149E8E File Offset: 0x0014808E
		public RsaEngine() : this(new RsaCoreEngine())
		{
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x00149E9B File Offset: 0x0014809B
		public RsaEngine(IRsa rsa)
		{
			this.core = rsa;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x001499A8 File Offset: 0x00147BA8
		public virtual string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x00149EAA File Offset: 0x001480AA
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.core.Init(forEncryption, parameters);
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x00149EB9 File Offset: 0x001480B9
		public virtual int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x00149EC6 File Offset: 0x001480C6
		public virtual int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x00149ED4 File Offset: 0x001480D4
		public virtual byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			BigInteger input = this.core.ConvertInput(inBuf, inOff, inLen);
			BigInteger result = this.core.ProcessBlock(input);
			return this.core.ConvertOutput(result);
		}

		// Token: 0x040021D7 RID: 8663
		private readonly IRsa core;
	}
}
