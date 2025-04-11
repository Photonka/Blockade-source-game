using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000233 RID: 563
	public class X509KeyUsage : Asn1Encodable
	{
		// Token: 0x060014BE RID: 5310 RVA: 0x000AED06 File Offset: 0x000ACF06
		public X509KeyUsage(int usage)
		{
			this.usage = usage;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000AED15 File Offset: 0x000ACF15
		public override Asn1Object ToAsn1Object()
		{
			return new KeyUsage(this.usage);
		}

		// Token: 0x040014FD RID: 5373
		public const int DigitalSignature = 128;

		// Token: 0x040014FE RID: 5374
		public const int NonRepudiation = 64;

		// Token: 0x040014FF RID: 5375
		public const int KeyEncipherment = 32;

		// Token: 0x04001500 RID: 5376
		public const int DataEncipherment = 16;

		// Token: 0x04001501 RID: 5377
		public const int KeyAgreement = 8;

		// Token: 0x04001502 RID: 5378
		public const int KeyCertSign = 4;

		// Token: 0x04001503 RID: 5379
		public const int CrlSign = 2;

		// Token: 0x04001504 RID: 5380
		public const int EncipherOnly = 1;

		// Token: 0x04001505 RID: 5381
		public const int DecipherOnly = 32768;

		// Token: 0x04001506 RID: 5382
		private readonly int usage;
	}
}
