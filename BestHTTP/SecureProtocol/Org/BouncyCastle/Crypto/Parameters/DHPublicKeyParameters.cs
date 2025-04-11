using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B2 RID: 1202
	public class DHPublicKeyParameters : DHKeyParameters
	{
		// Token: 0x06002F2B RID: 12075 RVA: 0x0012758C File Offset: 0x0012578C
		private static BigInteger Validate(BigInteger y, DHParameters dhParams)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			if (y.CompareTo(BigInteger.Two) < 0 || y.CompareTo(dhParams.P.Subtract(BigInteger.Two)) > 0)
			{
				throw new ArgumentException("invalid DH public key", "y");
			}
			if (dhParams.Q != null && !y.ModPow(dhParams.Q, dhParams.P).Equals(BigInteger.One))
			{
				throw new ArgumentException("y value does not appear to be in correct group", "y");
			}
			return y;
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x00127615 File Offset: 0x00125815
		public DHPublicKeyParameters(BigInteger y, DHParameters parameters) : base(false, parameters)
		{
			this.y = DHPublicKeyParameters.Validate(y, parameters);
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x0012762C File Offset: 0x0012582C
		public DHPublicKeyParameters(BigInteger y, DHParameters parameters, DerObjectIdentifier algorithmOid) : base(false, parameters, algorithmOid)
		{
			this.y = DHPublicKeyParameters.Validate(y, parameters);
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x00127644 File Offset: 0x00125844
		public virtual BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x0012764C File Offset: 0x0012584C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHPublicKeyParameters dhpublicKeyParameters = obj as DHPublicKeyParameters;
			return dhpublicKeyParameters != null && this.Equals(dhpublicKeyParameters);
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x00127672 File Offset: 0x00125872
		protected bool Equals(DHPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x00127690 File Offset: 0x00125890
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001E80 RID: 7808
		private readonly BigInteger y;
	}
}
