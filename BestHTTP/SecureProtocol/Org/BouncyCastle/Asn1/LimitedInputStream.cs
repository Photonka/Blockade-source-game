using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200065C RID: 1628
	internal abstract class LimitedInputStream : BaseInputStream
	{
		// Token: 0x06003CED RID: 15597 RVA: 0x001752A0 File Offset: 0x001734A0
		internal LimitedInputStream(Stream inStream, int limit)
		{
			this._in = inStream;
			this._limit = limit;
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x001752B6 File Offset: 0x001734B6
		internal virtual int GetRemaining()
		{
			return this._limit;
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x001752BE File Offset: 0x001734BE
		protected virtual void SetParentEofDetect(bool on)
		{
			if (this._in is IndefiniteLengthInputStream)
			{
				((IndefiniteLengthInputStream)this._in).SetEofOn00(on);
			}
		}

		// Token: 0x040025CF RID: 9679
		protected readonly Stream _in;

		// Token: 0x040025D0 RID: 9680
		private int _limit;
	}
}
