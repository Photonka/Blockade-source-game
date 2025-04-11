using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x0200070B RID: 1803
	public class VerisignCzagExtension : DerIA5String
	{
		// Token: 0x060041F4 RID: 16884 RVA: 0x001877C8 File Offset: 0x001859C8
		public VerisignCzagExtension(DerIA5String str) : base(str.GetString())
		{
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x001877E8 File Offset: 0x001859E8
		public override string ToString()
		{
			return "VerisignCzagExtension: " + this.GetString();
		}
	}
}
