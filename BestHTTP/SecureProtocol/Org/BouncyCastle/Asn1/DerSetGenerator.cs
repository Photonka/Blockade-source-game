using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200064A RID: 1610
	public class DerSetGenerator : DerGenerator
	{
		// Token: 0x06003C8C RID: 15500 RVA: 0x001745FC File Offset: 0x001727FC
		public DerSetGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x00174610 File Offset: 0x00172810
		public DerSetGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x00174626 File Offset: 0x00172826
		public override void AddObject(Asn1Encodable obj)
		{
			new DerOutputStream(this._bOut).WriteObject(obj);
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x00174639 File Offset: 0x00172839
		public override Stream GetRawOutputStream()
		{
			return this._bOut;
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x00174641 File Offset: 0x00172841
		public override void Close()
		{
			base.WriteDerEncoded(49, this._bOut.ToArray());
		}

		// Token: 0x040025C2 RID: 9666
		private readonly MemoryStream _bOut = new MemoryStream();
	}
}
