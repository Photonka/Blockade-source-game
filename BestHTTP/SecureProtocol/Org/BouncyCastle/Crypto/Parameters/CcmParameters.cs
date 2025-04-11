using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004AB RID: 1195
	[Obsolete("Use AeadParameters")]
	public class CcmParameters : AeadParameters
	{
		// Token: 0x06002EF8 RID: 12024 RVA: 0x00126EE3 File Offset: 0x001250E3
		public CcmParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText) : base(key, macSize, nonce, associatedText)
		{
		}
	}
}
