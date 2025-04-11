using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000585 RID: 1413
	public class VmpcKsa3Engine : VmpcEngine
	{
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x00152525 File Offset: 0x00150725
		public override string AlgorithmName
		{
			get
			{
				return "VMPC-KSA3";
			}
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x0015252C File Offset: 0x0015072C
		protected override void InitKey(byte[] keyBytes, byte[] ivBytes)
		{
			this.s = 0;
			this.P = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				this.P[i] = (byte)i;
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + keyBytes[j % keyBytes.Length] & byte.MaxValue)];
				byte b = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
			}
			for (int k = 0; k < 768; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] + ivBytes[k % ivBytes.Length] & byte.MaxValue)];
				byte b2 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			for (int l = 0; l < 768; l++)
			{
				this.s = this.P[(int)(this.s + this.P[l & 255] + keyBytes[l % keyBytes.Length] & byte.MaxValue)];
				byte b3 = this.P[l & 255];
				this.P[l & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b3;
			}
			this.n = 0;
		}
	}
}
