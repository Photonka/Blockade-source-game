using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020005A8 RID: 1448
	public class SkeinDigest : IDigest, IMemoable
	{
		// Token: 0x060037F6 RID: 14326 RVA: 0x00163283 File Offset: 0x00161483
		public SkeinDigest(int stateSizeBits, int digestSizeBits)
		{
			this.engine = new SkeinEngine(stateSizeBits, digestSizeBits);
			this.Init(null);
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x0016329F File Offset: 0x0016149F
		public SkeinDigest(SkeinDigest digest)
		{
			this.engine = new SkeinEngine(digest.engine);
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x001632B8 File Offset: 0x001614B8
		public void Reset(IMemoable other)
		{
			SkeinDigest skeinDigest = (SkeinDigest)other;
			this.engine.Reset(skeinDigest.engine);
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x001632DD File Offset: 0x001614DD
		public IMemoable Copy()
		{
			return new SkeinDigest(this);
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x001632E8 File Offset: 0x001614E8
		public string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					"Skein-",
					this.engine.BlockSize * 8,
					"-",
					this.engine.OutputSize * 8
				});
			}
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x0016333A File Offset: 0x0016153A
		public int GetDigestSize()
		{
			return this.engine.OutputSize;
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x00163347 File Offset: 0x00161547
		public int GetByteLength()
		{
			return this.engine.BlockSize;
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x00163354 File Offset: 0x00161554
		public void Init(SkeinParameters parameters)
		{
			this.engine.Init(parameters);
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x00163362 File Offset: 0x00161562
		public void Reset()
		{
			this.engine.Reset();
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x0016336F File Offset: 0x0016156F
		public void Update(byte inByte)
		{
			this.engine.Update(inByte);
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0016337D File Offset: 0x0016157D
		public void BlockUpdate(byte[] inBytes, int inOff, int len)
		{
			this.engine.Update(inBytes, inOff, len);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x0016338D File Offset: 0x0016158D
		public int DoFinal(byte[] outBytes, int outOff)
		{
			return this.engine.DoFinal(outBytes, outOff);
		}

		// Token: 0x0400237D RID: 9085
		public const int SKEIN_256 = 256;

		// Token: 0x0400237E RID: 9086
		public const int SKEIN_512 = 512;

		// Token: 0x0400237F RID: 9087
		public const int SKEIN_1024 = 1024;

		// Token: 0x04002380 RID: 9088
		private readonly SkeinEngine engine;
	}
}
