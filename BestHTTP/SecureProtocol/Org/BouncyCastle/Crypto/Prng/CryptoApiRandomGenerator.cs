using System;
using System.Security.Cryptography;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000498 RID: 1176
	public class CryptoApiRandomGenerator : IRandomGenerator
	{
		// Token: 0x06002E7F RID: 11903 RVA: 0x00124C84 File Offset: 0x00122E84
		public CryptoApiRandomGenerator() : this(RandomNumberGenerator.Create())
		{
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x00124C91 File Offset: 0x00122E91
		public CryptoApiRandomGenerator(RandomNumberGenerator rng)
		{
			this.rndProv = rng;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void AddSeedMaterial(byte[] seed)
		{
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void AddSeedMaterial(long seed)
		{
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x00124CA0 File Offset: 0x00122EA0
		public virtual void NextBytes(byte[] bytes)
		{
			this.rndProv.GetBytes(bytes);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x00124CB0 File Offset: 0x00122EB0
		public virtual void NextBytes(byte[] bytes, int start, int len)
		{
			if (start < 0)
			{
				throw new ArgumentException("Start offset cannot be negative", "start");
			}
			if (bytes.Length < start + len)
			{
				throw new ArgumentException("Byte array too small for requested offset and length");
			}
			if (bytes.Length == len && start == 0)
			{
				this.NextBytes(bytes);
				return;
			}
			byte[] array = new byte[len];
			this.NextBytes(array);
			Array.Copy(array, 0, bytes, start, len);
		}

		// Token: 0x04001E22 RID: 7714
		private readonly RandomNumberGenerator rndProv;
	}
}
