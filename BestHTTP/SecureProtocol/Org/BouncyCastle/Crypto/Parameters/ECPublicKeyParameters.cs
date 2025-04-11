using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BF RID: 1215
	public class ECPublicKeyParameters : ECKeyParameters
	{
		// Token: 0x06002F87 RID: 12167 RVA: 0x001280FC File Offset: 0x001262FC
		public ECPublicKeyParameters(ECPoint q, ECDomainParameters parameters) : this("EC", q, parameters)
		{
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x0012810B File Offset: 0x0012630B
		[Obsolete("Use version with explicit 'algorithm' parameter")]
		public ECPublicKeyParameters(ECPoint q, DerObjectIdentifier publicKeyParamSet) : base("ECGOST3410", false, publicKeyParamSet)
		{
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			this.q = ECDomainParameters.Validate(base.Parameters.Curve, q);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x0012813F File Offset: 0x0012633F
		public ECPublicKeyParameters(string algorithm, ECPoint q, ECDomainParameters parameters) : base(algorithm, false, parameters)
		{
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			this.q = ECDomainParameters.Validate(base.Parameters.Curve, q);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x0012816F File Offset: 0x0012636F
		public ECPublicKeyParameters(string algorithm, ECPoint q, DerObjectIdentifier publicKeyParamSet) : base(algorithm, false, publicKeyParamSet)
		{
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			this.q = ECDomainParameters.Validate(base.Parameters.Curve, q);
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x0012819F File Offset: 0x0012639F
		public ECPoint Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x001281A8 File Offset: 0x001263A8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECPublicKeyParameters ecpublicKeyParameters = obj as ECPublicKeyParameters;
			return ecpublicKeyParameters != null && this.Equals(ecpublicKeyParameters);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x001281CE File Offset: 0x001263CE
		protected bool Equals(ECPublicKeyParameters other)
		{
			return this.q.Equals(other.q) && base.Equals(other);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x001281EC File Offset: 0x001263EC
		public override int GetHashCode()
		{
			return this.q.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001EA2 RID: 7842
		private readonly ECPoint q;
	}
}
