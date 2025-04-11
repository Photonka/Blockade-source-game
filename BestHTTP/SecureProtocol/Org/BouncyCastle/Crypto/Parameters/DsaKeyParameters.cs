using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B5 RID: 1205
	public abstract class DsaKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002F3A RID: 12090 RVA: 0x00127784 File Offset: 0x00125984
		protected DsaKeyParameters(bool isPrivate, DsaParameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x00127794 File Offset: 0x00125994
		public DsaParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x0012779C File Offset: 0x0012599C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaKeyParameters dsaKeyParameters = obj as DsaKeyParameters;
			return dsaKeyParameters != null && this.Equals(dsaKeyParameters);
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x001277C2 File Offset: 0x001259C2
		protected bool Equals(DsaKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x001277E0 File Offset: 0x001259E0
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001E84 RID: 7812
		private readonly DsaParameters parameters;
	}
}
