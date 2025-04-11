using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x0200070A RID: 1802
	public class NetscapeRevocationUrl : DerIA5String
	{
		// Token: 0x060041F2 RID: 16882 RVA: 0x001877C8 File Offset: 0x001859C8
		public NetscapeRevocationUrl(DerIA5String str) : base(str.GetString())
		{
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x001877D6 File Offset: 0x001859D6
		public override string ToString()
		{
			return "NetscapeRevocationUrl: " + this.GetString();
		}
	}
}
