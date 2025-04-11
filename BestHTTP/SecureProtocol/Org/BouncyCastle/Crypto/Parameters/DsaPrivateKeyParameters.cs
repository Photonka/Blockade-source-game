using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B8 RID: 1208
	public class DsaPrivateKeyParameters : DsaKeyParameters
	{
		// Token: 0x06002F4F RID: 12111 RVA: 0x0012797E File Offset: 0x00125B7E
		public DsaPrivateKeyParameters(BigInteger x, DsaParameters parameters) : base(true, parameters)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			this.x = x;
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x0012799D File Offset: 0x00125B9D
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x001279A8 File Offset: 0x00125BA8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaPrivateKeyParameters dsaPrivateKeyParameters = obj as DsaPrivateKeyParameters;
			return dsaPrivateKeyParameters != null && this.Equals(dsaPrivateKeyParameters);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x001279CE File Offset: 0x00125BCE
		protected bool Equals(DsaPrivateKeyParameters other)
		{
			return this.x.Equals(other.x) && base.Equals(other);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x001279EC File Offset: 0x00125BEC
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001E90 RID: 7824
		private readonly BigInteger x;
	}
}
