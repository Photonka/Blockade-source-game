using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BE RID: 1214
	public class ECPrivateKeyParameters : ECKeyParameters
	{
		// Token: 0x06002F7F RID: 12159 RVA: 0x00128029 File Offset: 0x00126229
		public ECPrivateKeyParameters(BigInteger d, ECDomainParameters parameters) : this("EC", d, parameters)
		{
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x00128038 File Offset: 0x00126238
		[Obsolete("Use version with explicit 'algorithm' parameter")]
		public ECPrivateKeyParameters(BigInteger d, DerObjectIdentifier publicKeyParamSet) : base("ECGOST3410", true, publicKeyParamSet)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			this.d = d;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x0012805C File Offset: 0x0012625C
		public ECPrivateKeyParameters(string algorithm, BigInteger d, ECDomainParameters parameters) : base(algorithm, true, parameters)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			this.d = d;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x0012807C File Offset: 0x0012627C
		public ECPrivateKeyParameters(string algorithm, BigInteger d, DerObjectIdentifier publicKeyParamSet) : base(algorithm, true, publicKeyParamSet)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			this.d = d;
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x0012809C File Offset: 0x0012629C
		public BigInteger D
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x001280A4 File Offset: 0x001262A4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECPrivateKeyParameters ecprivateKeyParameters = obj as ECPrivateKeyParameters;
			return ecprivateKeyParameters != null && this.Equals(ecprivateKeyParameters);
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x001280CA File Offset: 0x001262CA
		protected bool Equals(ECPrivateKeyParameters other)
		{
			return this.d.Equals(other.d) && base.Equals(other);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x001280E8 File Offset: 0x001262E8
		public override int GetHashCode()
		{
			return this.d.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001EA1 RID: 7841
		private readonly BigInteger d;
	}
}
