using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004AF RID: 1199
	public class DHKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002F0D RID: 12045 RVA: 0x001271DA File Offset: 0x001253DA
		protected DHKeyParameters(bool isPrivate, DHParameters parameters) : this(isPrivate, parameters, PkcsObjectIdentifiers.DhKeyAgreement)
		{
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x001271E9 File Offset: 0x001253E9
		protected DHKeyParameters(bool isPrivate, DHParameters parameters, DerObjectIdentifier algorithmOid) : base(isPrivate)
		{
			this.parameters = parameters;
			this.algorithmOid = algorithmOid;
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x00127200 File Offset: 0x00125400
		public DHParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x00127208 File Offset: 0x00125408
		public DerObjectIdentifier AlgorithmOid
		{
			get
			{
				return this.algorithmOid;
			}
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x00127210 File Offset: 0x00125410
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHKeyParameters dhkeyParameters = obj as DHKeyParameters;
			return dhkeyParameters != null && this.Equals(dhkeyParameters);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x00127236 File Offset: 0x00125436
		protected bool Equals(DHKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x00127254 File Offset: 0x00125454
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001E75 RID: 7797
		private readonly DHParameters parameters;

		// Token: 0x04001E76 RID: 7798
		private readonly DerObjectIdentifier algorithmOid;
	}
}
