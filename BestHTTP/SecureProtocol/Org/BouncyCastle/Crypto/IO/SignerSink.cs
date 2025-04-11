using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO
{
	// Token: 0x0200052C RID: 1324
	public class SignerSink : BaseOutputStream
	{
		// Token: 0x06003299 RID: 12953 RVA: 0x001342B7 File Offset: 0x001324B7
		public SignerSink(ISigner signer)
		{
			this.mSigner = signer;
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x001342C6 File Offset: 0x001324C6
		public virtual ISigner Signer
		{
			get
			{
				return this.mSigner;
			}
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x001342CE File Offset: 0x001324CE
		public override void WriteByte(byte b)
		{
			this.mSigner.Update(b);
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x001342DC File Offset: 0x001324DC
		public override void Write(byte[] buf, int off, int len)
		{
			if (len > 0)
			{
				this.mSigner.BlockUpdate(buf, off, len);
			}
		}

		// Token: 0x0400203A RID: 8250
		private readonly ISigner mSigner;
	}
}
