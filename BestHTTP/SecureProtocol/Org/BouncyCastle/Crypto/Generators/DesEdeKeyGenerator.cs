using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000530 RID: 1328
	public class DesEdeKeyGenerator : DesKeyGenerator
	{
		// Token: 0x060032BD RID: 12989 RVA: 0x00134CDE File Offset: 0x00132EDE
		public DesEdeKeyGenerator()
		{
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x00134CE6 File Offset: 0x00132EE6
		internal DesEdeKeyGenerator(int defaultStrength) : base(defaultStrength)
		{
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x00134CF0 File Offset: 0x00132EF0
		protected override void engineInit(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
			this.strength = (parameters.Strength + 7) / 8;
			if (this.strength == 0 || this.strength == 21)
			{
				this.strength = 24;
				return;
			}
			if (this.strength == 14)
			{
				this.strength = 16;
				return;
			}
			if (this.strength != 24 && this.strength != 16)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"DESede key must be ",
					192,
					" or ",
					128,
					" bits long."
				}));
			}
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x00134DA0 File Offset: 0x00132FA0
		protected override byte[] engineGenerateKey()
		{
			byte[] array = new byte[this.strength];
			do
			{
				this.random.NextBytes(array);
				DesParameters.SetOddParity(array);
			}
			while (DesEdeParameters.IsWeakKey(array, 0, array.Length) || !DesEdeParameters.IsRealEdeKey(array, 0));
			return array;
		}
	}
}
