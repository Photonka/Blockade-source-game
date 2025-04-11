using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000499 RID: 1177
	public class DigestRandomGenerator : IRandomGenerator
	{
		// Token: 0x06002E85 RID: 11909 RVA: 0x00124D10 File Offset: 0x00122F10
		public DigestRandomGenerator(IDigest digest)
		{
			this.digest = digest;
			this.seed = new byte[digest.GetDigestSize()];
			this.seedCounter = 1L;
			this.state = new byte[digest.GetDigestSize()];
			this.stateCounter = 1L;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x00124D5C File Offset: 0x00122F5C
		public void AddSeedMaterial(byte[] inSeed)
		{
			lock (this)
			{
				this.DigestUpdate(inSeed);
				this.DigestUpdate(this.seed);
				this.DigestDoFinal(this.seed);
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x00124DB0 File Offset: 0x00122FB0
		public void AddSeedMaterial(long rSeed)
		{
			lock (this)
			{
				this.DigestAddCounter(rSeed);
				this.DigestUpdate(this.seed);
				this.DigestDoFinal(this.seed);
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x00124E04 File Offset: 0x00123004
		public void NextBytes(byte[] bytes)
		{
			this.NextBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x00124E14 File Offset: 0x00123014
		public void NextBytes(byte[] bytes, int start, int len)
		{
			lock (this)
			{
				int num = 0;
				this.GenerateState();
				int num2 = start + len;
				for (int i = start; i < num2; i++)
				{
					if (num == this.state.Length)
					{
						this.GenerateState();
						num = 0;
					}
					bytes[i] = this.state[num++];
				}
			}
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x00124E88 File Offset: 0x00123088
		private void CycleSeed()
		{
			this.DigestUpdate(this.seed);
			long num = this.seedCounter;
			this.seedCounter = num + 1L;
			this.DigestAddCounter(num);
			this.DigestDoFinal(this.seed);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x00124EC8 File Offset: 0x001230C8
		private void GenerateState()
		{
			long num = this.stateCounter;
			this.stateCounter = num + 1L;
			this.DigestAddCounter(num);
			this.DigestUpdate(this.state);
			this.DigestUpdate(this.seed);
			this.DigestDoFinal(this.state);
			if (this.stateCounter % 10L == 0L)
			{
				this.CycleSeed();
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x00124F24 File Offset: 0x00123124
		private void DigestAddCounter(long seedVal)
		{
			byte[] array = new byte[8];
			Pack.UInt64_To_LE((ulong)seedVal, array);
			this.digest.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x00124F4F File Offset: 0x0012314F
		private void DigestUpdate(byte[] inSeed)
		{
			this.digest.BlockUpdate(inSeed, 0, inSeed.Length);
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x00124F61 File Offset: 0x00123161
		private void DigestDoFinal(byte[] result)
		{
			this.digest.DoFinal(result, 0);
		}

		// Token: 0x04001E23 RID: 7715
		private const long CYCLE_COUNT = 10L;

		// Token: 0x04001E24 RID: 7716
		private long stateCounter;

		// Token: 0x04001E25 RID: 7717
		private long seedCounter;

		// Token: 0x04001E26 RID: 7718
		private IDigest digest;

		// Token: 0x04001E27 RID: 7719
		private byte[] state;

		// Token: 0x04001E28 RID: 7720
		private byte[] seed;
	}
}
