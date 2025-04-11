using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004AD RID: 1197
	public class DesParameters : KeyParameter
	{
		// Token: 0x06002F02 RID: 12034 RVA: 0x0012706A File Offset: 0x0012526A
		public DesParameters(byte[] key) : base(key)
		{
			if (DesParameters.IsWeakKey(key))
			{
				throw new ArgumentException("attempt to create weak DES key");
			}
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00127086 File Offset: 0x00125286
		public DesParameters(byte[] key, int keyOff, int keyLen) : base(key, keyOff, keyLen)
		{
			if (DesParameters.IsWeakKey(key, keyOff))
			{
				throw new ArgumentException("attempt to create weak DES key");
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x001270A8 File Offset: 0x001252A8
		public static bool IsWeakKey(byte[] key, int offset)
		{
			if (key.Length - offset < 8)
			{
				throw new ArgumentException("key material too short.");
			}
			for (int i = 0; i < 16; i++)
			{
				bool flag = false;
				for (int j = 0; j < 8; j++)
				{
					if (key[j + offset] != DesParameters.DES_weak_keys[i * 8 + j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x001270FF File Offset: 0x001252FF
		public static bool IsWeakKey(byte[] key)
		{
			return DesParameters.IsWeakKey(key, 0);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x00127108 File Offset: 0x00125308
		public static byte SetOddParity(byte b)
		{
			uint num = (uint)(b ^ 1);
			num ^= num >> 4;
			num ^= num >> 2;
			num ^= num >> 1;
			num &= 1U;
			return (byte)((uint)b ^ num);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x00127134 File Offset: 0x00125334
		public static void SetOddParity(byte[] bytes)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = DesParameters.SetOddParity(bytes[i]);
			}
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0012715C File Offset: 0x0012535C
		public static void SetOddParity(byte[] bytes, int off, int len)
		{
			for (int i = 0; i < len; i++)
			{
				bytes[off + i] = DesParameters.SetOddParity(bytes[off + i]);
			}
		}

		// Token: 0x04001E71 RID: 7793
		public const int DesKeyLength = 8;

		// Token: 0x04001E72 RID: 7794
		private const int N_DES_WEAK_KEYS = 16;

		// Token: 0x04001E73 RID: 7795
		private static readonly byte[] DES_weak_keys = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			31,
			31,
			31,
			31,
			14,
			14,
			14,
			14,
			224,
			224,
			224,
			224,
			241,
			241,
			241,
			241,
			254,
			254,
			254,
			254,
			254,
			254,
			254,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			31,
			224,
			31,
			224,
			14,
			241,
			14,
			241,
			1,
			224,
			1,
			224,
			1,
			241,
			1,
			241,
			31,
			254,
			31,
			254,
			14,
			254,
			14,
			254,
			1,
			31,
			1,
			31,
			1,
			14,
			1,
			14,
			224,
			254,
			224,
			254,
			241,
			254,
			241,
			254,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			1,
			224,
			31,
			224,
			31,
			241,
			14,
			241,
			14,
			224,
			1,
			224,
			1,
			241,
			1,
			241,
			1,
			254,
			31,
			254,
			31,
			254,
			14,
			254,
			14,
			31,
			1,
			31,
			1,
			14,
			1,
			14,
			1,
			254,
			224,
			254,
			224,
			254,
			241,
			254,
			241
		};
	}
}
