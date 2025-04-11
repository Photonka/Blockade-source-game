using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004DE RID: 1246
	public class ParametersWithIV : ICipherParameters
	{
		// Token: 0x06003019 RID: 12313 RVA: 0x001291A2 File Offset: 0x001273A2
		public ParametersWithIV(ICipherParameters parameters, byte[] iv) : this(parameters, iv, 0, iv.Length)
		{
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x001291B0 File Offset: 0x001273B0
		public ParametersWithIV(ICipherParameters parameters, byte[] iv, int ivOff, int ivLen)
		{
			if (iv == null)
			{
				throw new ArgumentNullException("iv");
			}
			this.parameters = parameters;
			this.iv = Arrays.CopyOfRange(iv, ivOff, ivOff + ivLen);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x001291DE File Offset: 0x001273DE
		public byte[] GetIV()
		{
			return (byte[])this.iv.Clone();
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x001291F0 File Offset: 0x001273F0
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001EDD RID: 7901
		private readonly ICipherParameters parameters;

		// Token: 0x04001EDE RID: 7902
		private readonly byte[] iv;
	}
}
