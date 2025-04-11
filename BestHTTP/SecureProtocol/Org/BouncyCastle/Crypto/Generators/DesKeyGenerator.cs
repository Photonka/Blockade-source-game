using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000531 RID: 1329
	public class DesKeyGenerator : CipherKeyGenerator
	{
		// Token: 0x060032C1 RID: 12993 RVA: 0x00134DE1 File Offset: 0x00132FE1
		public DesKeyGenerator()
		{
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x00134DE9 File Offset: 0x00132FE9
		internal DesKeyGenerator(int defaultStrength) : base(defaultStrength)
		{
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x00134DF4 File Offset: 0x00132FF4
		protected override void engineInit(KeyGenerationParameters parameters)
		{
			base.engineInit(parameters);
			if (this.strength == 0 || this.strength == 7)
			{
				this.strength = 8;
				return;
			}
			if (this.strength != 8)
			{
				throw new ArgumentException("DES key must be " + 64 + " bits long.");
			}
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x00134E48 File Offset: 0x00133048
		protected override byte[] engineGenerateKey()
		{
			byte[] array = new byte[8];
			do
			{
				this.random.NextBytes(array);
				DesParameters.SetOddParity(array);
			}
			while (DesParameters.IsWeakKey(array, 0));
			return array;
		}
	}
}
