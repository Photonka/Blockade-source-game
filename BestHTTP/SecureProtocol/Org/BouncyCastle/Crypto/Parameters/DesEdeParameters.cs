using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004AC RID: 1196
	public class DesEdeParameters : DesParameters
	{
		// Token: 0x06002EF9 RID: 12025 RVA: 0x00126EF0 File Offset: 0x001250F0
		private static byte[] FixKey(byte[] key, int keyOff, int keyLen)
		{
			byte[] array = new byte[24];
			if (keyLen != 16)
			{
				if (keyLen != 24)
				{
					throw new ArgumentException("Bad length for DESede key: " + keyLen, "keyLen");
				}
				Array.Copy(key, keyOff, array, 0, 24);
			}
			else
			{
				Array.Copy(key, keyOff, array, 0, 16);
				Array.Copy(key, keyOff, array, 16, 8);
			}
			if (DesEdeParameters.IsWeakKey(array))
			{
				throw new ArgumentException("attempt to create weak DESede key");
			}
			return array;
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x00126F65 File Offset: 0x00125165
		public DesEdeParameters(byte[] key) : base(DesEdeParameters.FixKey(key, 0, key.Length))
		{
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x00126F77 File Offset: 0x00125177
		public DesEdeParameters(byte[] key, int keyOff, int keyLen) : base(DesEdeParameters.FixKey(key, keyOff, keyLen))
		{
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x00126F88 File Offset: 0x00125188
		public static bool IsWeakKey(byte[] key, int offset, int length)
		{
			for (int i = offset; i < length; i += 8)
			{
				if (DesParameters.IsWeakKey(key, i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x00126FAD File Offset: 0x001251AD
		public new static bool IsWeakKey(byte[] key, int offset)
		{
			return DesEdeParameters.IsWeakKey(key, offset, key.Length - offset);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x00126FBB File Offset: 0x001251BB
		public new static bool IsWeakKey(byte[] key)
		{
			return DesEdeParameters.IsWeakKey(key, 0, key.Length);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x00126FC7 File Offset: 0x001251C7
		public static bool IsRealEdeKey(byte[] key, int offset)
		{
			if (key.Length != 16)
			{
				return DesEdeParameters.IsReal3Key(key, offset);
			}
			return DesEdeParameters.IsReal2Key(key, offset);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x00126FE0 File Offset: 0x001251E0
		public static bool IsReal2Key(byte[] key, int offset)
		{
			bool flag = false;
			for (int num = offset; num != offset + 8; num++)
			{
				flag |= (key[num] != key[num + 8]);
			}
			return flag;
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x00127010 File Offset: 0x00125210
		public static bool IsReal3Key(byte[] key, int offset)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int num = offset; num != offset + 8; num++)
			{
				flag |= (key[num] != key[num + 8]);
				flag2 |= (key[num] != key[num + 16]);
				flag3 |= (key[num + 8] != key[num + 16]);
			}
			return flag && flag2 && flag3;
		}

		// Token: 0x04001E70 RID: 7792
		public const int DesEdeKeyLength = 24;
	}
}
