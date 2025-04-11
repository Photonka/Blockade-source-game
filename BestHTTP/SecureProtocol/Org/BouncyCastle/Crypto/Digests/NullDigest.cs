using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200059A RID: 1434
	public class NullDigest : IDigest
	{
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x0015ACFB File Offset: 0x00158EFB
		public string AlgorithmName
		{
			get
			{
				return "NULL";
			}
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public int GetByteLength()
		{
			return 0;
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x0015AD02 File Offset: 0x00158F02
		public int GetDigestSize()
		{
			return (int)this.bOut.Length;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x0015AD10 File Offset: 0x00158F10
		public void Update(byte b)
		{
			this.bOut.WriteByte(b);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x0015AD1E File Offset: 0x00158F1E
		public void BlockUpdate(byte[] inBytes, int inOff, int len)
		{
			this.bOut.Write(inBytes, inOff, len);
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x0015AD30 File Offset: 0x00158F30
		public int DoFinal(byte[] outBytes, int outOff)
		{
			int result;
			try
			{
				result = Streams.WriteBufTo(this.bOut, outBytes, outOff);
			}
			finally
			{
				this.Reset();
			}
			return result;
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x0015AD68 File Offset: 0x00158F68
		public void Reset()
		{
			this.bOut.SetLength(0L);
		}

		// Token: 0x04002323 RID: 8995
		private readonly MemoryStream bOut = new MemoryStream();
	}
}
