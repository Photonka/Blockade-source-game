using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200041C RID: 1052
	public class HeartbeatExtension
	{
		// Token: 0x06002A38 RID: 10808 RVA: 0x0011499D File Offset: 0x00112B9D
		public HeartbeatExtension(byte mode)
		{
			if (!HeartbeatMode.IsValid(mode))
			{
				throw new ArgumentException("not a valid HeartbeatMode value", "mode");
			}
			this.mMode = mode;
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x001149C4 File Offset: 0x00112BC4
		public virtual byte Mode
		{
			get
			{
				return this.mMode;
			}
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x001149CC File Offset: 0x00112BCC
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mMode, output);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x001149DA File Offset: 0x00112BDA
		public static HeartbeatExtension Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (!HeartbeatMode.IsValid(b))
			{
				throw new TlsFatalAlert(47);
			}
			return new HeartbeatExtension(b);
		}

		// Token: 0x04001C14 RID: 7188
		protected readonly byte mMode;
	}
}
