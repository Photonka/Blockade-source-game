using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D6 RID: 1238
	public class KeyParameter : ICipherParameters
	{
		// Token: 0x06002FF9 RID: 12281 RVA: 0x00128E8C File Offset: 0x0012708C
		public KeyParameter(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = (byte[])key.Clone();
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x00128EB4 File Offset: 0x001270B4
		public KeyParameter(byte[] key, int keyOff, int keyLen)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (keyOff < 0 || keyOff > key.Length)
			{
				throw new ArgumentOutOfRangeException("keyOff");
			}
			if (keyLen < 0 || keyLen > key.Length - keyOff)
			{
				throw new ArgumentOutOfRangeException("keyLen");
			}
			this.key = new byte[keyLen];
			Array.Copy(key, keyOff, this.key, 0, keyLen);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x00128F1C File Offset: 0x0012711C
		public byte[] GetKey()
		{
			return (byte[])this.key.Clone();
		}

		// Token: 0x04001ECD RID: 7885
		private readonly byte[] key;
	}
}
