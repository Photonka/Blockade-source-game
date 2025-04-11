using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E5 RID: 1253
	public class RsaKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06003033 RID: 12339 RVA: 0x0012938D File Offset: 0x0012758D
		public RsaKeyGenerationParameters(BigInteger publicExponent, SecureRandom random, int strength, int certainty) : base(random, strength)
		{
			this.publicExponent = publicExponent;
			this.certainty = certainty;
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x001293A6 File Offset: 0x001275A6
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x001293AE File Offset: 0x001275AE
		public int Certainty
		{
			get
			{
				return this.certainty;
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x001293B8 File Offset: 0x001275B8
		public override bool Equals(object obj)
		{
			RsaKeyGenerationParameters rsaKeyGenerationParameters = obj as RsaKeyGenerationParameters;
			return rsaKeyGenerationParameters != null && this.certainty == rsaKeyGenerationParameters.certainty && this.publicExponent.Equals(rsaKeyGenerationParameters.publicExponent);
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x001293F4 File Offset: 0x001275F4
		public override int GetHashCode()
		{
			return this.certainty.GetHashCode() ^ this.publicExponent.GetHashCode();
		}

		// Token: 0x04001EE9 RID: 7913
		private readonly BigInteger publicExponent;

		// Token: 0x04001EEA RID: 7914
		private readonly int certainty;
	}
}
