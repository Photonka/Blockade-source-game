using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004AE RID: 1198
	public class DHKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002F0A RID: 12042 RVA: 0x001271A0 File Offset: 0x001253A0
		public DHKeyGenerationParameters(SecureRandom random, DHParameters parameters) : base(random, DHKeyGenerationParameters.GetStrength(parameters))
		{
			this.parameters = parameters;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x001271B6 File Offset: 0x001253B6
		public DHParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x001271BE File Offset: 0x001253BE
		internal static int GetStrength(DHParameters parameters)
		{
			if (parameters.L == 0)
			{
				return parameters.P.BitLength;
			}
			return parameters.L;
		}

		// Token: 0x04001E74 RID: 7796
		private readonly DHParameters parameters;
	}
}
