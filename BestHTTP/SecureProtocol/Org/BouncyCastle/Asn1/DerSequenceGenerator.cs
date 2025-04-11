using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000647 RID: 1607
	public class DerSequenceGenerator : DerGenerator
	{
		// Token: 0x06003C7B RID: 15483 RVA: 0x001743F4 File Offset: 0x001725F4
		public DerSequenceGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x00174408 File Offset: 0x00172608
		public DerSequenceGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x0017441E File Offset: 0x0017261E
		public override void AddObject(Asn1Encodable obj)
		{
			new DerOutputStream(this._bOut).WriteObject(obj);
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x00174431 File Offset: 0x00172631
		public override Stream GetRawOutputStream()
		{
			return this._bOut;
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x00174439 File Offset: 0x00172639
		public override void Close()
		{
			base.WriteDerEncoded(48, this._bOut.ToArray());
		}

		// Token: 0x040025BF RID: 9663
		private readonly MemoryStream _bOut = new MemoryStream();
	}
}
