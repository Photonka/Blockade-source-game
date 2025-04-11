using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200049D RID: 1181
	public class ReversedWindowGenerator : IRandomGenerator
	{
		// Token: 0x06002E96 RID: 11926 RVA: 0x00124FB0 File Offset: 0x001231B0
		public ReversedWindowGenerator(IRandomGenerator generator, int windowSize)
		{
			if (generator == null)
			{
				throw new ArgumentNullException("generator");
			}
			if (windowSize < 2)
			{
				throw new ArgumentException("Window size must be at least 2", "windowSize");
			}
			this.generator = generator;
			this.window = new byte[windowSize];
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x00124FF0 File Offset: 0x001231F0
		public virtual void AddSeedMaterial(byte[] seed)
		{
			lock (this)
			{
				this.windowCount = 0;
				this.generator.AddSeedMaterial(seed);
			}
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x00125038 File Offset: 0x00123238
		public virtual void AddSeedMaterial(long seed)
		{
			lock (this)
			{
				this.windowCount = 0;
				this.generator.AddSeedMaterial(seed);
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x00125080 File Offset: 0x00123280
		public virtual void NextBytes(byte[] bytes)
		{
			this.doNextBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0012508D File Offset: 0x0012328D
		public virtual void NextBytes(byte[] bytes, int start, int len)
		{
			this.doNextBytes(bytes, start, len);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x00125098 File Offset: 0x00123298
		private void doNextBytes(byte[] bytes, int start, int len)
		{
			lock (this)
			{
				int i = 0;
				while (i < len)
				{
					if (this.windowCount < 1)
					{
						this.generator.NextBytes(this.window, 0, this.window.Length);
						this.windowCount = this.window.Length;
					}
					int num = start + i++;
					byte[] array = this.window;
					int num2 = this.windowCount - 1;
					this.windowCount = num2;
					bytes[num] = array[num2];
				}
			}
		}

		// Token: 0x04001E29 RID: 7721
		private readonly IRandomGenerator generator;

		// Token: 0x04001E2A RID: 7722
		private byte[] window;

		// Token: 0x04001E2B RID: 7723
		private int windowCount;
	}
}
