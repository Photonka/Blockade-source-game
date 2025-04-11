using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000590 RID: 1424
	public class GOST3411_2012_256Digest : GOST3411_2012Digest
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x001565A4 File Offset: 0x001547A4
		public override string AlgorithmName
		{
			get
			{
				return "GOST3411-2012-256";
			}
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x001565AB File Offset: 0x001547AB
		public GOST3411_2012_256Digest() : base(GOST3411_2012_256Digest.IV)
		{
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x001565B8 File Offset: 0x001547B8
		public GOST3411_2012_256Digest(GOST3411_2012_256Digest other) : base(GOST3411_2012_256Digest.IV)
		{
			base.Reset(other);
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x001565CC File Offset: 0x001547CC
		public override int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x001565D0 File Offset: 0x001547D0
		public override int DoFinal(byte[] output, int outOff)
		{
			byte[] array = new byte[64];
			base.DoFinal(array, 0);
			Array.Copy(array, 32, output, outOff, 32);
			return 32;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x001565FC File Offset: 0x001547FC
		public override IMemoable Copy()
		{
			return new GOST3411_2012_256Digest(this);
		}

		// Token: 0x040022B6 RID: 8886
		private static readonly byte[] IV = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		};
	}
}
