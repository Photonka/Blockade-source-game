using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200054A RID: 1354
	public class Poly1305KeyGenerator : CipherKeyGenerator
	{
		// Token: 0x06003346 RID: 13126 RVA: 0x0013824B File Offset: 0x0013644B
		protected override void engineInit(KeyGenerationParameters param)
		{
			this.random = param.Random;
			this.strength = 32;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x00138261 File Offset: 0x00136461
		protected override byte[] engineGenerateKey()
		{
			byte[] array = base.engineGenerateKey();
			Poly1305KeyGenerator.Clamp(array);
			return array;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x00138270 File Offset: 0x00136470
		public static void Clamp(byte[] key)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException("Poly1305 key must be 256 bits.");
			}
			int num = 3;
			key[num] &= 15;
			int num2 = 7;
			key[num2] &= 15;
			int num3 = 11;
			key[num3] &= 15;
			int num4 = 15;
			key[num4] &= 15;
			int num5 = 4;
			key[num5] &= 252;
			int num6 = 8;
			key[num6] &= 252;
			int num7 = 12;
			key[num7] &= 252;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x00138300 File Offset: 0x00136500
		public static void CheckKey(byte[] key)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException("Poly1305 key must be 256 bits.");
			}
			Poly1305KeyGenerator.CheckMask(key[3], 15);
			Poly1305KeyGenerator.CheckMask(key[7], 15);
			Poly1305KeyGenerator.CheckMask(key[11], 15);
			Poly1305KeyGenerator.CheckMask(key[15], 15);
			Poly1305KeyGenerator.CheckMask(key[4], 252);
			Poly1305KeyGenerator.CheckMask(key[8], 252);
			Poly1305KeyGenerator.CheckMask(key[12], 252);
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x00138371 File Offset: 0x00136571
		private static void CheckMask(byte b, byte mask)
		{
			if ((b & ~(mask != 0)) != 0)
			{
				throw new ArgumentException("Invalid format for r portion of Poly1305 key.");
			}
		}

		// Token: 0x0400208D RID: 8333
		private const byte R_MASK_LOW_2 = 252;

		// Token: 0x0400208E RID: 8334
		private const byte R_MASK_HIGH_4 = 15;
	}
}
