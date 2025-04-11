using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020005A2 RID: 1442
	public class Sha384Digest : LongDigest
	{
		// Token: 0x060037C0 RID: 14272 RVA: 0x00162810 File Offset: 0x00160A10
		public Sha384Digest()
		{
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x00162818 File Offset: 0x00160A18
		public Sha384Digest(Sha384Digest t) : base(t)
		{
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x00162821 File Offset: 0x00160A21
		public override string AlgorithmName
		{
			get
			{
				return "SHA-384";
			}
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x00162828 File Offset: 0x00160A28
		public override int GetDigestSize()
		{
			return 48;
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x0016282C File Offset: 0x00160A2C
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt64_To_BE(this.H1, output, outOff);
			Pack.UInt64_To_BE(this.H2, output, outOff + 8);
			Pack.UInt64_To_BE(this.H3, output, outOff + 16);
			Pack.UInt64_To_BE(this.H4, output, outOff + 24);
			Pack.UInt64_To_BE(this.H5, output, outOff + 32);
			Pack.UInt64_To_BE(this.H6, output, outOff + 40);
			this.Reset();
			return 48;
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x001628A4 File Offset: 0x00160AA4
		public override void Reset()
		{
			base.Reset();
			this.H1 = 14680500436340154072UL;
			this.H2 = 7105036623409894663UL;
			this.H3 = 10473403895298186519UL;
			this.H4 = 1526699215303891257UL;
			this.H5 = 7436329637833083697UL;
			this.H6 = 10282925794625328401UL;
			this.H7 = 15784041429090275239UL;
			this.H8 = 5167115440072839076UL;
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x0016292F File Offset: 0x00160B2F
		public override IMemoable Copy()
		{
			return new Sha384Digest(this);
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x00162938 File Offset: 0x00160B38
		public override void Reset(IMemoable other)
		{
			Sha384Digest t = (Sha384Digest)other;
			base.CopyIn(t);
		}

		// Token: 0x0400236F RID: 9071
		private const int DigestLength = 48;
	}
}
