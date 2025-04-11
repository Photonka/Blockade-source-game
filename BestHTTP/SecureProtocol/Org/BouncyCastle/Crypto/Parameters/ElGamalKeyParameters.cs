using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C7 RID: 1223
	public class ElGamalKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002FAE RID: 12206 RVA: 0x001286A5 File Offset: 0x001268A5
		protected ElGamalKeyParameters(bool isPrivate, ElGamalParameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x001286B5 File Offset: 0x001268B5
		public ElGamalParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x001286C0 File Offset: 0x001268C0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalKeyParameters elGamalKeyParameters = obj as ElGamalKeyParameters;
			return elGamalKeyParameters != null && this.Equals(elGamalKeyParameters);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x001286E6 File Offset: 0x001268E6
		protected bool Equals(ElGamalKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x00128704 File Offset: 0x00126904
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001EAE RID: 7854
		private readonly ElGamalParameters parameters;
	}
}
