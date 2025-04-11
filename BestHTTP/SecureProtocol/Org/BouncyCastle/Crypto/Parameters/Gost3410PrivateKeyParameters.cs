using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004CE RID: 1230
	public class Gost3410PrivateKeyParameters : Gost3410KeyParameters
	{
		// Token: 0x06002FD8 RID: 12248 RVA: 0x00128B34 File Offset: 0x00126D34
		public Gost3410PrivateKeyParameters(BigInteger x, Gost3410Parameters parameters) : base(true, parameters)
		{
			if (x.SignValue < 1 || x.BitLength > 256 || x.CompareTo(base.Parameters.Q) >= 0)
			{
				throw new ArgumentException("Invalid x for GOST3410 private key", "x");
			}
			this.x = x;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x00128B8C File Offset: 0x00126D8C
		public Gost3410PrivateKeyParameters(BigInteger x, DerObjectIdentifier publicKeyParamSet) : base(true, publicKeyParamSet)
		{
			if (x.SignValue < 1 || x.BitLength > 256 || x.CompareTo(base.Parameters.Q) >= 0)
			{
				throw new ArgumentException("Invalid x for GOST3410 private key", "x");
			}
			this.x = x;
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x00128BE2 File Offset: 0x00126DE2
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x04001EBC RID: 7868
		private readonly BigInteger x;
	}
}
