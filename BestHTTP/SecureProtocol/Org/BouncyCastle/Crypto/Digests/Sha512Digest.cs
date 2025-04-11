using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020005A4 RID: 1444
	public class Sha512Digest : LongDigest
	{
		// Token: 0x060037D0 RID: 14288 RVA: 0x00162810 File Offset: 0x00160A10
		public Sha512Digest()
		{
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x00162818 File Offset: 0x00160A18
		public Sha512Digest(Sha512Digest t) : base(t)
		{
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x00162A66 File Offset: 0x00160C66
		public override string AlgorithmName
		{
			get
			{
				return "SHA-512";
			}
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x001554A4 File Offset: 0x001536A4
		public override int GetDigestSize()
		{
			return 64;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x00162A70 File Offset: 0x00160C70
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt64_To_BE(this.H1, output, outOff);
			Pack.UInt64_To_BE(this.H2, output, outOff + 8);
			Pack.UInt64_To_BE(this.H3, output, outOff + 16);
			Pack.UInt64_To_BE(this.H4, output, outOff + 24);
			Pack.UInt64_To_BE(this.H5, output, outOff + 32);
			Pack.UInt64_To_BE(this.H6, output, outOff + 40);
			Pack.UInt64_To_BE(this.H7, output, outOff + 48);
			Pack.UInt64_To_BE(this.H8, output, outOff + 56);
			this.Reset();
			return 64;
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x00162B08 File Offset: 0x00160D08
		public override void Reset()
		{
			base.Reset();
			this.H1 = 7640891576956012808UL;
			this.H2 = 13503953896175478587UL;
			this.H3 = 4354685564936845355UL;
			this.H4 = 11912009170470909681UL;
			this.H5 = 5840696475078001361UL;
			this.H6 = 11170449401992604703UL;
			this.H7 = 2270897969802886507UL;
			this.H8 = 6620516959819538809UL;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x00162B93 File Offset: 0x00160D93
		public override IMemoable Copy()
		{
			return new Sha512Digest(this);
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x00162B9C File Offset: 0x00160D9C
		public override void Reset(IMemoable other)
		{
			Sha512Digest t = (Sha512Digest)other;
			base.CopyIn(t);
		}

		// Token: 0x04002370 RID: 9072
		private const int DigestLength = 64;
	}
}
