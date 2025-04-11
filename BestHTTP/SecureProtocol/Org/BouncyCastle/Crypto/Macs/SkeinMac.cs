using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000525 RID: 1317
	public class SkeinMac : IMac
	{
		// Token: 0x0600324A RID: 12874 RVA: 0x001332F7 File Offset: 0x001314F7
		public SkeinMac(int stateSizeBits, int digestSizeBits)
		{
			this.engine = new SkeinEngine(stateSizeBits, digestSizeBits);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0013330C File Offset: 0x0013150C
		public SkeinMac(SkeinMac mac)
		{
			this.engine = new SkeinEngine(mac.engine);
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x00133328 File Offset: 0x00131528
		public string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					"Skein-MAC-",
					this.engine.BlockSize * 8,
					"-",
					this.engine.OutputSize * 8
				});
			}
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x0013337C File Offset: 0x0013157C
		public void Init(ICipherParameters parameters)
		{
			SkeinParameters skeinParameters;
			if (parameters is SkeinParameters)
			{
				skeinParameters = (SkeinParameters)parameters;
			}
			else
			{
				if (!(parameters is KeyParameter))
				{
					throw new ArgumentException("Invalid parameter passed to Skein MAC init - " + Platform.GetTypeName(parameters));
				}
				skeinParameters = new SkeinParameters.Builder().SetKey(((KeyParameter)parameters).GetKey()).Build();
			}
			if (skeinParameters.GetKey() == null)
			{
				throw new ArgumentException("Skein MAC requires a key parameter.");
			}
			this.engine.Init(skeinParameters);
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x001333F4 File Offset: 0x001315F4
		public int GetMacSize()
		{
			return this.engine.OutputSize;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x00133401 File Offset: 0x00131601
		public void Reset()
		{
			this.engine.Reset();
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x0013340E File Offset: 0x0013160E
		public void Update(byte inByte)
		{
			this.engine.Update(inByte);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x0013341C File Offset: 0x0013161C
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.engine.Update(input, inOff, len);
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0013342C File Offset: 0x0013162C
		public int DoFinal(byte[] output, int outOff)
		{
			return this.engine.DoFinal(output, outOff);
		}

		// Token: 0x0400201D RID: 8221
		public const int SKEIN_256 = 256;

		// Token: 0x0400201E RID: 8222
		public const int SKEIN_512 = 512;

		// Token: 0x0400201F RID: 8223
		public const int SKEIN_1024 = 1024;

		// Token: 0x04002020 RID: 8224
		private readonly SkeinEngine engine;
	}
}
