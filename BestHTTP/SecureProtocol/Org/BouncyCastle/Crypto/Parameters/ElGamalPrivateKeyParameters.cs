using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C9 RID: 1225
	public class ElGamalPrivateKeyParameters : ElGamalKeyParameters
	{
		// Token: 0x06002FBB RID: 12219 RVA: 0x0012880A File Offset: 0x00126A0A
		public ElGamalPrivateKeyParameters(BigInteger x, ElGamalParameters parameters) : base(true, parameters)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			this.x = x;
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x00128829 File Offset: 0x00126A29
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x00128834 File Offset: 0x00126A34
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = obj as ElGamalPrivateKeyParameters;
			return elGamalPrivateKeyParameters != null && this.Equals(elGamalPrivateKeyParameters);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x0012885A File Offset: 0x00126A5A
		protected bool Equals(ElGamalPrivateKeyParameters other)
		{
			return other.x.Equals(this.x) && base.Equals(other);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x00128878 File Offset: 0x00126A78
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001EB2 RID: 7858
		private readonly BigInteger x;
	}
}
