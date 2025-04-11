using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004CA RID: 1226
	public class ElGamalPublicKeyParameters : ElGamalKeyParameters
	{
		// Token: 0x06002FC0 RID: 12224 RVA: 0x0012888C File Offset: 0x00126A8C
		public ElGamalPublicKeyParameters(BigInteger y, ElGamalParameters parameters) : base(false, parameters)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = y;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x001288AB File Offset: 0x00126AAB
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x001288B4 File Offset: 0x00126AB4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalPublicKeyParameters elGamalPublicKeyParameters = obj as ElGamalPublicKeyParameters;
			return elGamalPublicKeyParameters != null && this.Equals(elGamalPublicKeyParameters);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x001288DA File Offset: 0x00126ADA
		protected bool Equals(ElGamalPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x001288F8 File Offset: 0x00126AF8
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001EB3 RID: 7859
		private readonly BigInteger y;
	}
}
