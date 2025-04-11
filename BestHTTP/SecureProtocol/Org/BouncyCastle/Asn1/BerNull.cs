using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000623 RID: 1571
	public class BerNull : DerNull
	{
		// Token: 0x06003B5D RID: 15197 RVA: 0x001711C1 File Offset: 0x0016F3C1
		[Obsolete("Use static Instance object")]
		public BerNull()
		{
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x001711C9 File Offset: 0x0016F3C9
		private BerNull(int dummy) : base(dummy)
		{
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x001711D2 File Offset: 0x0016F3D2
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(5);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x04002589 RID: 9609
		public new static readonly BerNull Instance = new BerNull(0);
	}
}
