using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000501 RID: 1281
	public class DefaultVerifierCalculator : IStreamCalculator
	{
		// Token: 0x060030C1 RID: 12481 RVA: 0x0012AC5D File Offset: 0x00128E5D
		public DefaultVerifierCalculator(ISigner signer)
		{
			this.mSignerSink = new SignerSink(signer);
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x0012AC71 File Offset: 0x00128E71
		public Stream Stream
		{
			get
			{
				return this.mSignerSink;
			}
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x0012AC79 File Offset: 0x00128E79
		public object GetResult()
		{
			return new DefaultVerifierResult(this.mSignerSink.Signer);
		}

		// Token: 0x04001F22 RID: 7970
		private readonly SignerSink mSignerSink;
	}
}
