using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x020004FF RID: 1279
	public class DefaultSignatureCalculator : IStreamCalculator
	{
		// Token: 0x060030BB RID: 12475 RVA: 0x0012AC01 File Offset: 0x00128E01
		public DefaultSignatureCalculator(ISigner signer)
		{
			this.mSignerSink = new SignerSink(signer);
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x0012AC15 File Offset: 0x00128E15
		public Stream Stream
		{
			get
			{
				return this.mSignerSink;
			}
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x0012AC1D File Offset: 0x00128E1D
		public object GetResult()
		{
			return new DefaultSignatureResult(this.mSignerSink.Signer);
		}

		// Token: 0x04001F20 RID: 7968
		private readonly SignerSink mSignerSink;
	}
}
