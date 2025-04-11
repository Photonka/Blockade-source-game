using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x020005C5 RID: 1477
	public class JPakeRound2Payload
	{
		// Token: 0x060038E0 RID: 14560 RVA: 0x00168140 File Offset: 0x00166340
		public JPakeRound2Payload(string participantId, BigInteger a, BigInteger[] knowledgeProofForX2s)
		{
			JPakeUtilities.ValidateNotNull(participantId, "participantId");
			JPakeUtilities.ValidateNotNull(a, "a");
			JPakeUtilities.ValidateNotNull(knowledgeProofForX2s, "knowledgeProofForX2s");
			this.participantId = participantId;
			this.a = a;
			this.knowledgeProofForX2s = new BigInteger[knowledgeProofForX2s.Length];
			knowledgeProofForX2s.CopyTo(this.knowledgeProofForX2s, 0);
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x0016819D File Offset: 0x0016639D
		public virtual string ParticipantId
		{
			get
			{
				return this.participantId;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x001681A5 File Offset: 0x001663A5
		public virtual BigInteger A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x001681B0 File Offset: 0x001663B0
		public virtual BigInteger[] KnowledgeProofForX2s
		{
			get
			{
				BigInteger[] array = new BigInteger[this.knowledgeProofForX2s.Length];
				Array.Copy(this.knowledgeProofForX2s, array, this.knowledgeProofForX2s.Length);
				return array;
			}
		}

		// Token: 0x0400246F RID: 9327
		private readonly string participantId;

		// Token: 0x04002470 RID: 9328
		private readonly BigInteger a;

		// Token: 0x04002471 RID: 9329
		private readonly BigInteger[] knowledgeProofForX2s;
	}
}
