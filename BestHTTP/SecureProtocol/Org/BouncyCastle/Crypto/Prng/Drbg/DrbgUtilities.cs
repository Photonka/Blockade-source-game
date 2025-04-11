using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x020004A6 RID: 1190
	internal class DrbgUtilities
	{
		// Token: 0x06002ED7 RID: 11991 RVA: 0x001263AC File Offset: 0x001245AC
		static DrbgUtilities()
		{
			DrbgUtilities.maxSecurityStrengths.Add("SHA-1", 128);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-224", 192);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-256", 256);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-384", 256);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-512", 256);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-512/224", 192);
			DrbgUtilities.maxSecurityStrengths.Add("SHA-512/256", 256);
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x00126472 File Offset: 0x00124672
		internal static int GetMaxSecurityStrength(IDigest d)
		{
			return (int)DrbgUtilities.maxSecurityStrengths[d.AlgorithmName];
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x0012648C File Offset: 0x0012468C
		internal static int GetMaxSecurityStrength(IMac m)
		{
			string algorithmName = m.AlgorithmName;
			return (int)DrbgUtilities.maxSecurityStrengths[algorithmName.Substring(0, algorithmName.IndexOf("/"))];
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x001264C4 File Offset: 0x001246C4
		internal static byte[] HashDF(IDigest digest, byte[] seedMaterial, int seedLength)
		{
			byte[] array = new byte[(seedLength + 7) / 8];
			int num = array.Length / digest.GetDigestSize();
			int num2 = 1;
			byte[] array2 = new byte[digest.GetDigestSize()];
			for (int i = 0; i <= num; i++)
			{
				digest.Update((byte)num2);
				digest.Update((byte)(seedLength >> 24));
				digest.Update((byte)(seedLength >> 16));
				digest.Update((byte)(seedLength >> 8));
				digest.Update((byte)seedLength);
				digest.BlockUpdate(seedMaterial, 0, seedMaterial.Length);
				digest.DoFinal(array2, 0);
				int length = (array.Length - i * array2.Length > array2.Length) ? array2.Length : (array.Length - i * array2.Length);
				Array.Copy(array2, 0, array, i * array2.Length, length);
				num2++;
			}
			if (seedLength % 8 != 0)
			{
				int num3 = 8 - seedLength % 8;
				uint num4 = 0U;
				for (int num5 = 0; num5 != array.Length; num5++)
				{
					uint num6 = (uint)array[num5];
					array[num5] = (byte)(num6 >> num3 | num4 << 8 - num3);
					num4 = num6;
				}
			}
			return array;
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x00125957 File Offset: 0x00123B57
		internal static bool IsTooLarge(byte[] bytes, int maxBytes)
		{
			return bytes != null && bytes.Length > maxBytes;
		}

		// Token: 0x04001E58 RID: 7768
		private static readonly IDictionary maxSecurityStrengths = Platform.CreateHashtable();
	}
}
