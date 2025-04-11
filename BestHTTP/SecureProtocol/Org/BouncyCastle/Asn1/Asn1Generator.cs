using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200060F RID: 1551
	public abstract class Asn1Generator
	{
		// Token: 0x06003AE2 RID: 15074 RVA: 0x0016FB52 File Offset: 0x0016DD52
		protected Asn1Generator(Stream outStream)
		{
			this._out = outStream;
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x0016FB61 File Offset: 0x0016DD61
		protected Stream Out
		{
			get
			{
				return this._out;
			}
		}

		// Token: 0x06003AE4 RID: 15076
		public abstract void AddObject(Asn1Encodable obj);

		// Token: 0x06003AE5 RID: 15077
		public abstract Stream GetRawOutputStream();

		// Token: 0x06003AE6 RID: 15078
		public abstract void Close();

		// Token: 0x0400255C RID: 9564
		private Stream _out;
	}
}
