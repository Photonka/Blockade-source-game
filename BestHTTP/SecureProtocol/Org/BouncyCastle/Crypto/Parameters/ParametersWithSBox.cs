using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E1 RID: 1249
	public class ParametersWithSBox : ICipherParameters
	{
		// Token: 0x06003026 RID: 12326 RVA: 0x0012929A File Offset: 0x0012749A
		public ParametersWithSBox(ICipherParameters parameters, byte[] sBox)
		{
			this.parameters = parameters;
			this.sBox = sBox;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x001292B0 File Offset: 0x001274B0
		public byte[] GetSBox()
		{
			return this.sBox;
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x001292B8 File Offset: 0x001274B8
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001EE3 RID: 7907
		private ICipherParameters parameters;

		// Token: 0x04001EE4 RID: 7908
		private byte[] sBox;
	}
}
