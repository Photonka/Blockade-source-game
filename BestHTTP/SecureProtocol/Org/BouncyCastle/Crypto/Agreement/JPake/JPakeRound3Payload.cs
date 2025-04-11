using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x020005C6 RID: 1478
	public class JPakeRound3Payload
	{
		// Token: 0x060038E4 RID: 14564 RVA: 0x001681E0 File Offset: 0x001663E0
		public JPakeRound3Payload(string participantId, BigInteger magTag)
		{
			this.participantId = participantId;
			this.macTag = magTag;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x001681F6 File Offset: 0x001663F6
		public virtual string ParticipantId
		{
			get
			{
				return this.participantId;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x001681FE File Offset: 0x001663FE
		public virtual BigInteger MacTag
		{
			get
			{
				return this.macTag;
			}
		}

		// Token: 0x04002472 RID: 9330
		private readonly string participantId;

		// Token: 0x04002473 RID: 9331
		private readonly BigInteger macTag;
	}
}
