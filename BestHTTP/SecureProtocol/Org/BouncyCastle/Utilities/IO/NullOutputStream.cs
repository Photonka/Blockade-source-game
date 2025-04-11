using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000265 RID: 613
	internal class NullOutputStream : BaseOutputStream
	{
		// Token: 0x06001711 RID: 5905 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void WriteByte(byte b)
		{
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void Write(byte[] buffer, int offset, int count)
		{
		}
	}
}
