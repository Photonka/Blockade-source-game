using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000709 RID: 1801
	public class NetscapeCertType : DerBitString
	{
		// Token: 0x060041EF RID: 16879 RVA: 0x0017105D File Offset: 0x0016F25D
		public NetscapeCertType(int usage) : base(usage)
		{
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x0017A21D File Offset: 0x0017841D
		public NetscapeCertType(DerBitString usage) : base(usage.GetBytes(), usage.PadBits)
		{
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00187794 File Offset: 0x00185994
		public override string ToString()
		{
			byte[] bytes = this.GetBytes();
			return "NetscapeCertType: 0x" + ((int)(bytes[0] & byte.MaxValue)).ToString("X");
		}

		// Token: 0x04002A02 RID: 10754
		public const int SslClient = 128;

		// Token: 0x04002A03 RID: 10755
		public const int SslServer = 64;

		// Token: 0x04002A04 RID: 10756
		public const int Smime = 32;

		// Token: 0x04002A05 RID: 10757
		public const int ObjectSigning = 16;

		// Token: 0x04002A06 RID: 10758
		public const int Reserved = 8;

		// Token: 0x04002A07 RID: 10759
		public const int SslCA = 4;

		// Token: 0x04002A08 RID: 10760
		public const int SmimeCA = 2;

		// Token: 0x04002A09 RID: 10761
		public const int ObjectSigningCA = 1;
	}
}
