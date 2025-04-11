using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B1 RID: 1201
	public class DHPrivateKeyParameters : DHKeyParameters
	{
		// Token: 0x06002F25 RID: 12069 RVA: 0x00127508 File Offset: 0x00125708
		public DHPrivateKeyParameters(BigInteger x, DHParameters parameters) : base(true, parameters)
		{
			this.x = x;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x00127519 File Offset: 0x00125719
		public DHPrivateKeyParameters(BigInteger x, DHParameters parameters, DerObjectIdentifier algorithmOid) : base(true, parameters, algorithmOid)
		{
			this.x = x;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x0012752B File Offset: 0x0012572B
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x00127534 File Offset: 0x00125734
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHPrivateKeyParameters dhprivateKeyParameters = obj as DHPrivateKeyParameters;
			return dhprivateKeyParameters != null && this.Equals(dhprivateKeyParameters);
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x0012755A File Offset: 0x0012575A
		protected bool Equals(DHPrivateKeyParameters other)
		{
			return this.x.Equals(other.x) && base.Equals(other);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x00127578 File Offset: 0x00125778
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001E7F RID: 7807
		private readonly BigInteger x;
	}
}
