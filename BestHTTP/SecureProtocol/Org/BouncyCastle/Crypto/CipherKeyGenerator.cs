using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B5 RID: 949
	public class CipherKeyGenerator
	{
		// Token: 0x0600279B RID: 10139 RVA: 0x0010E189 File Offset: 0x0010C389
		public CipherKeyGenerator()
		{
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x0010E198 File Offset: 0x0010C398
		internal CipherKeyGenerator(int defaultStrength)
		{
			if (defaultStrength < 1)
			{
				throw new ArgumentException("strength must be a positive value", "defaultStrength");
			}
			this.defaultStrength = defaultStrength;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x0010E1C2 File Offset: 0x0010C3C2
		public int DefaultStrength
		{
			get
			{
				return this.defaultStrength;
			}
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0010E1CA File Offset: 0x0010C3CA
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.uninitialised = false;
			this.engineInit(parameters);
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0010E1E8 File Offset: 0x0010C3E8
		protected virtual void engineInit(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
			this.strength = (parameters.Strength + 7) / 8;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0010E208 File Offset: 0x0010C408
		public byte[] GenerateKey()
		{
			if (this.uninitialised)
			{
				if (this.defaultStrength < 1)
				{
					throw new InvalidOperationException("Generator has not been initialised");
				}
				this.uninitialised = false;
				this.engineInit(new KeyGenerationParameters(new SecureRandom(), this.defaultStrength));
			}
			return this.engineGenerateKey();
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x0010E254 File Offset: 0x0010C454
		protected virtual byte[] engineGenerateKey()
		{
			return SecureRandom.GetNextBytes(this.random, this.strength);
		}

		// Token: 0x040019C6 RID: 6598
		protected internal SecureRandom random;

		// Token: 0x040019C7 RID: 6599
		protected internal int strength;

		// Token: 0x040019C8 RID: 6600
		private bool uninitialised = true;

		// Token: 0x040019C9 RID: 6601
		private int defaultStrength;
	}
}
