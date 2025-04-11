using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C6 RID: 1222
	public class ElGamalKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002FAB RID: 12203 RVA: 0x0012866B File Offset: 0x0012686B
		public ElGamalKeyGenerationParameters(SecureRandom random, ElGamalParameters parameters) : base(random, ElGamalKeyGenerationParameters.GetStrength(parameters))
		{
			this.parameters = parameters;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x00128681 File Offset: 0x00126881
		public ElGamalParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00128689 File Offset: 0x00126889
		internal static int GetStrength(ElGamalParameters parameters)
		{
			if (parameters.L == 0)
			{
				return parameters.P.BitLength;
			}
			return parameters.L;
		}

		// Token: 0x04001EAD RID: 7853
		private readonly ElGamalParameters parameters;
	}
}
