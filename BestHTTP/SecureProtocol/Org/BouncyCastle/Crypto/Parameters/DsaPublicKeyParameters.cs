using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B9 RID: 1209
	public class DsaPublicKeyParameters : DsaKeyParameters
	{
		// Token: 0x06002F54 RID: 12116 RVA: 0x00127A00 File Offset: 0x00125C00
		private static BigInteger Validate(BigInteger y, DsaParameters parameters)
		{
			if (parameters != null && (y.CompareTo(BigInteger.Two) < 0 || y.CompareTo(parameters.P.Subtract(BigInteger.Two)) > 0 || !y.ModPow(parameters.Q, parameters.P).Equals(BigInteger.One)))
			{
				throw new ArgumentException("y value does not appear to be in correct group");
			}
			return y;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x00127A61 File Offset: 0x00125C61
		public DsaPublicKeyParameters(BigInteger y, DsaParameters parameters) : base(false, parameters)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = DsaPublicKeyParameters.Validate(y, parameters);
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x00127A86 File Offset: 0x00125C86
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x00127A90 File Offset: 0x00125C90
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaPublicKeyParameters dsaPublicKeyParameters = obj as DsaPublicKeyParameters;
			return dsaPublicKeyParameters != null && this.Equals(dsaPublicKeyParameters);
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x00127AB6 File Offset: 0x00125CB6
		protected bool Equals(DsaPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x00127AD4 File Offset: 0x00125CD4
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001E91 RID: 7825
		private readonly BigInteger y;
	}
}
