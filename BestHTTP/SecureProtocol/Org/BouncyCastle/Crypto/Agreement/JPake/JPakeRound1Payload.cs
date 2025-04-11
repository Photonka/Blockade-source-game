using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x020005C4 RID: 1476
	public class JPakeRound1Payload
	{
		// Token: 0x060038DA RID: 14554 RVA: 0x00168024 File Offset: 0x00166224
		public JPakeRound1Payload(string participantId, BigInteger gx1, BigInteger gx2, BigInteger[] knowledgeProofForX1, BigInteger[] knowledgeProofForX2)
		{
			JPakeUtilities.ValidateNotNull(participantId, "participantId");
			JPakeUtilities.ValidateNotNull(gx1, "gx1");
			JPakeUtilities.ValidateNotNull(gx2, "gx2");
			JPakeUtilities.ValidateNotNull(knowledgeProofForX1, "knowledgeProofForX1");
			JPakeUtilities.ValidateNotNull(knowledgeProofForX2, "knowledgeProofForX2");
			this.participantId = participantId;
			this.gx1 = gx1;
			this.gx2 = gx2;
			this.knowledgeProofForX1 = new BigInteger[knowledgeProofForX1.Length];
			Array.Copy(knowledgeProofForX1, this.knowledgeProofForX1, knowledgeProofForX1.Length);
			this.knowledgeProofForX2 = new BigInteger[knowledgeProofForX2.Length];
			Array.Copy(knowledgeProofForX2, this.knowledgeProofForX2, knowledgeProofForX2.Length);
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x001680C5 File Offset: 0x001662C5
		public virtual string ParticipantId
		{
			get
			{
				return this.participantId;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x001680CD File Offset: 0x001662CD
		public virtual BigInteger Gx1
		{
			get
			{
				return this.gx1;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x001680D5 File Offset: 0x001662D5
		public virtual BigInteger Gx2
		{
			get
			{
				return this.gx2;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x001680E0 File Offset: 0x001662E0
		public virtual BigInteger[] KnowledgeProofForX1
		{
			get
			{
				BigInteger[] array = new BigInteger[this.knowledgeProofForX1.Length];
				Array.Copy(this.knowledgeProofForX1, array, this.knowledgeProofForX1.Length);
				return array;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x00168110 File Offset: 0x00166310
		public virtual BigInteger[] KnowledgeProofForX2
		{
			get
			{
				BigInteger[] array = new BigInteger[this.knowledgeProofForX2.Length];
				Array.Copy(this.knowledgeProofForX2, array, this.knowledgeProofForX2.Length);
				return array;
			}
		}

		// Token: 0x0400246A RID: 9322
		private readonly string participantId;

		// Token: 0x0400246B RID: 9323
		private readonly BigInteger gx1;

		// Token: 0x0400246C RID: 9324
		private readonly BigInteger gx2;

		// Token: 0x0400246D RID: 9325
		private readonly BigInteger[] knowledgeProofForX1;

		// Token: 0x0400246E RID: 9326
		private readonly BigInteger[] knowledgeProofForX2;
	}
}
