using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E0 RID: 1248
	public class ParametersWithSalt : ICipherParameters
	{
		// Token: 0x06003022 RID: 12322 RVA: 0x00129250 File Offset: 0x00127450
		public ParametersWithSalt(ICipherParameters parameters, byte[] salt) : this(parameters, salt, 0, salt.Length)
		{
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x0012925E File Offset: 0x0012745E
		public ParametersWithSalt(ICipherParameters parameters, byte[] salt, int saltOff, int saltLen)
		{
			this.salt = new byte[saltLen];
			this.parameters = parameters;
			Array.Copy(salt, saltOff, this.salt, 0, saltLen);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x0012928A File Offset: 0x0012748A
		public byte[] GetSalt()
		{
			return this.salt;
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x00129292 File Offset: 0x00127492
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001EE1 RID: 7905
		private byte[] salt;

		// Token: 0x04001EE2 RID: 7906
		private ICipherParameters parameters;
	}
}
