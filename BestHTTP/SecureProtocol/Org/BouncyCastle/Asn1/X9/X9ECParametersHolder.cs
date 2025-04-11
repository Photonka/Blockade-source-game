using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000668 RID: 1640
	public abstract class X9ECParametersHolder
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06003D43 RID: 15683 RVA: 0x00176450 File Offset: 0x00174650
		public X9ECParameters Parameters
		{
			get
			{
				X9ECParameters result;
				lock (this)
				{
					if (this.parameters == null)
					{
						this.parameters = this.CreateParameters();
					}
					result = this.parameters;
				}
				return result;
			}
		}

		// Token: 0x06003D44 RID: 15684
		protected abstract X9ECParameters CreateParameters();

		// Token: 0x040025ED RID: 9709
		private X9ECParameters parameters;
	}
}
