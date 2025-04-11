using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004CF RID: 1231
	public class Gost3410PublicKeyParameters : Gost3410KeyParameters
	{
		// Token: 0x06002FDB RID: 12251 RVA: 0x00128BEA File Offset: 0x00126DEA
		public Gost3410PublicKeyParameters(BigInteger y, Gost3410Parameters parameters) : base(false, parameters)
		{
			if (y.SignValue < 1 || y.CompareTo(base.Parameters.P) >= 0)
			{
				throw new ArgumentException("Invalid y for GOST3410 public key", "y");
			}
			this.y = y;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x00128C28 File Offset: 0x00126E28
		public Gost3410PublicKeyParameters(BigInteger y, DerObjectIdentifier publicKeyParamSet) : base(false, publicKeyParamSet)
		{
			if (y.SignValue < 1 || y.CompareTo(base.Parameters.P) >= 0)
			{
				throw new ArgumentException("Invalid y for GOST3410 public key", "y");
			}
			this.y = y;
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x00128C66 File Offset: 0x00126E66
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x04001EBD RID: 7869
		private readonly BigInteger y;
	}
}
