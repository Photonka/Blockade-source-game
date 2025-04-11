using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E7 RID: 999
	public class BasicTlsPskIdentity : TlsPskIdentity
	{
		// Token: 0x060028DD RID: 10461 RVA: 0x0010F6CA File Offset: 0x0010D8CA
		public BasicTlsPskIdentity(byte[] identity, byte[] psk)
		{
			this.mIdentity = Arrays.Clone(identity);
			this.mPsk = Arrays.Clone(psk);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x0010F6EA File Offset: 0x0010D8EA
		public BasicTlsPskIdentity(string identity, byte[] psk)
		{
			this.mIdentity = Strings.ToUtf8ByteArray(identity);
			this.mPsk = Arrays.Clone(psk);
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void SkipIdentityHint()
		{
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void NotifyIdentityHint(byte[] psk_identity_hint)
		{
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x0010F70A File Offset: 0x0010D90A
		public virtual byte[] GetPskIdentity()
		{
			return this.mIdentity;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x0010F712 File Offset: 0x0010D912
		public virtual byte[] GetPsk()
		{
			return this.mPsk;
		}

		// Token: 0x04001A19 RID: 6681
		protected byte[] mIdentity;

		// Token: 0x04001A1A RID: 6682
		protected byte[] mPsk;
	}
}
