using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x020005C1 RID: 1473
	public class JPakeParticipant
	{
		// Token: 0x060038C7 RID: 14535 RVA: 0x00167754 File Offset: 0x00165954
		public JPakeParticipant(string participantId, char[] password) : this(participantId, password, JPakePrimeOrderGroups.NIST_3072)
		{
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x00167763 File Offset: 0x00165963
		public JPakeParticipant(string participantId, char[] password, JPakePrimeOrderGroup group) : this(participantId, password, group, new Sha256Digest(), new SecureRandom())
		{
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x00167778 File Offset: 0x00165978
		public JPakeParticipant(string participantId, char[] password, JPakePrimeOrderGroup group, IDigest digest, SecureRandom random)
		{
			JPakeUtilities.ValidateNotNull(participantId, "participantId");
			JPakeUtilities.ValidateNotNull(password, "password");
			JPakeUtilities.ValidateNotNull(group, "p");
			JPakeUtilities.ValidateNotNull(digest, "digest");
			JPakeUtilities.ValidateNotNull(random, "random");
			if (password.Length == 0)
			{
				throw new ArgumentException("Password must not be empty.");
			}
			this.participantId = participantId;
			this.password = new char[password.Length];
			Array.Copy(password, this.password, password.Length);
			this.p = group.P;
			this.q = group.Q;
			this.g = group.G;
			this.digest = digest;
			this.random = random;
			this.state = JPakeParticipant.STATE_INITIALIZED;
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x00167836 File Offset: 0x00165A36
		public virtual int State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x00167840 File Offset: 0x00165A40
		public virtual JPakeRound1Payload CreateRound1PayloadToSend()
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_1_CREATED)
			{
				throw new InvalidOperationException("Round 1 payload already created for " + this.participantId);
			}
			this.x1 = JPakeUtilities.GenerateX1(this.q, this.random);
			this.x2 = JPakeUtilities.GenerateX2(this.q, this.random);
			this.gx1 = JPakeUtilities.CalculateGx(this.p, this.g, this.x1);
			this.gx2 = JPakeUtilities.CalculateGx(this.p, this.g, this.x2);
			BigInteger[] knowledgeProofForX = JPakeUtilities.CalculateZeroKnowledgeProof(this.p, this.q, this.g, this.gx1, this.x1, this.participantId, this.digest, this.random);
			BigInteger[] knowledgeProofForX2 = JPakeUtilities.CalculateZeroKnowledgeProof(this.p, this.q, this.g, this.gx2, this.x2, this.participantId, this.digest, this.random);
			this.state = JPakeParticipant.STATE_ROUND_1_CREATED;
			return new JPakeRound1Payload(this.participantId, this.gx1, this.gx2, knowledgeProofForX, knowledgeProofForX2);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x00167968 File Offset: 0x00165B68
		public virtual void ValidateRound1PayloadReceived(JPakeRound1Payload round1PayloadReceived)
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_1_VALIDATED)
			{
				throw new InvalidOperationException("Validation already attempted for round 1 payload for " + this.participantId);
			}
			this.partnerParticipantId = round1PayloadReceived.ParticipantId;
			this.gx3 = round1PayloadReceived.Gx1;
			this.gx4 = round1PayloadReceived.Gx2;
			BigInteger[] knowledgeProofForX = round1PayloadReceived.KnowledgeProofForX1;
			BigInteger[] knowledgeProofForX2 = round1PayloadReceived.KnowledgeProofForX2;
			JPakeUtilities.ValidateParticipantIdsDiffer(this.participantId, round1PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateGx4(this.gx4);
			JPakeUtilities.ValidateZeroKnowledgeProof(this.p, this.q, this.g, this.gx3, knowledgeProofForX, round1PayloadReceived.ParticipantId, this.digest);
			JPakeUtilities.ValidateZeroKnowledgeProof(this.p, this.q, this.g, this.gx4, knowledgeProofForX2, round1PayloadReceived.ParticipantId, this.digest);
			this.state = JPakeParticipant.STATE_ROUND_1_VALIDATED;
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x00167A48 File Offset: 0x00165C48
		public virtual JPakeRound2Payload CreateRound2PayloadToSend()
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_2_CREATED)
			{
				throw new InvalidOperationException("Round 2 payload already created for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_ROUND_1_VALIDATED)
			{
				throw new InvalidOperationException("Round 1 payload must be validated prior to creating round 2 payload for " + this.participantId);
			}
			BigInteger gA = JPakeUtilities.CalculateGA(this.p, this.gx1, this.gx3, this.gx4);
			BigInteger s = JPakeUtilities.CalculateS(this.password);
			BigInteger bigInteger = JPakeUtilities.CalculateX2s(this.q, this.x2, s);
			BigInteger bigInteger2 = JPakeUtilities.CalculateA(this.p, this.q, gA, bigInteger);
			BigInteger[] knowledgeProofForX2s = JPakeUtilities.CalculateZeroKnowledgeProof(this.p, this.q, gA, bigInteger2, bigInteger, this.participantId, this.digest, this.random);
			this.state = JPakeParticipant.STATE_ROUND_2_CREATED;
			return new JPakeRound2Payload(this.participantId, bigInteger2, knowledgeProofForX2s);
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x00167B30 File Offset: 0x00165D30
		public virtual void ValidateRound2PayloadReceived(JPakeRound2Payload round2PayloadReceived)
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_2_VALIDATED)
			{
				throw new InvalidOperationException("Validation already attempted for round 2 payload for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_ROUND_1_VALIDATED)
			{
				throw new InvalidOperationException("Round 1 payload must be validated prior to validation round 2 payload for " + this.participantId);
			}
			BigInteger ga = JPakeUtilities.CalculateGA(this.p, this.gx3, this.gx1, this.gx2);
			this.b = round2PayloadReceived.A;
			BigInteger[] knowledgeProofForX2s = round2PayloadReceived.KnowledgeProofForX2s;
			JPakeUtilities.ValidateParticipantIdsDiffer(this.participantId, round2PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateParticipantIdsEqual(this.partnerParticipantId, round2PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateGa(ga);
			JPakeUtilities.ValidateZeroKnowledgeProof(this.p, this.q, ga, this.b, knowledgeProofForX2s, round2PayloadReceived.ParticipantId, this.digest);
			this.state = JPakeParticipant.STATE_ROUND_2_VALIDATED;
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x00167C0C File Offset: 0x00165E0C
		public virtual BigInteger CalculateKeyingMaterial()
		{
			if (this.state >= JPakeParticipant.STATE_KEY_CALCULATED)
			{
				throw new InvalidOperationException("Key already calculated for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_ROUND_2_VALIDATED)
			{
				throw new InvalidOperationException("Round 2 payload must be validated prior to creating key for " + this.participantId);
			}
			BigInteger s = JPakeUtilities.CalculateS(this.password);
			Array.Clear(this.password, 0, this.password.Length);
			this.password = null;
			BigInteger result = JPakeUtilities.CalculateKeyingMaterial(this.p, this.q, this.gx4, this.x2, s, this.b);
			this.x1 = null;
			this.x2 = null;
			this.b = null;
			this.state = JPakeParticipant.STATE_KEY_CALCULATED;
			return result;
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x00167CCC File Offset: 0x00165ECC
		public virtual JPakeRound3Payload CreateRound3PayloadToSend(BigInteger keyingMaterial)
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_3_CREATED)
			{
				throw new InvalidOperationException("Round 3 payload already created for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_KEY_CALCULATED)
			{
				throw new InvalidOperationException("Keying material must be calculated prior to creating round 3 payload for " + this.participantId);
			}
			BigInteger magTag = JPakeUtilities.CalculateMacTag(this.participantId, this.partnerParticipantId, this.gx1, this.gx2, this.gx3, this.gx4, keyingMaterial, this.digest);
			this.state = JPakeParticipant.STATE_ROUND_3_CREATED;
			return new JPakeRound3Payload(this.participantId, magTag);
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x00167D68 File Offset: 0x00165F68
		public virtual void ValidateRound3PayloadReceived(JPakeRound3Payload round3PayloadReceived, BigInteger keyingMaterial)
		{
			if (this.state >= JPakeParticipant.STATE_ROUND_3_VALIDATED)
			{
				throw new InvalidOperationException("Validation already attempted for round 3 payload for " + this.participantId);
			}
			if (this.state < JPakeParticipant.STATE_KEY_CALCULATED)
			{
				throw new InvalidOperationException("Keying material must be calculated prior to validating round 3 payload for " + this.participantId);
			}
			JPakeUtilities.ValidateParticipantIdsDiffer(this.participantId, round3PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateParticipantIdsEqual(this.partnerParticipantId, round3PayloadReceived.ParticipantId);
			JPakeUtilities.ValidateMacTag(this.participantId, this.partnerParticipantId, this.gx1, this.gx2, this.gx3, this.gx4, keyingMaterial, this.digest, round3PayloadReceived.MacTag);
			this.gx1 = null;
			this.gx2 = null;
			this.gx3 = null;
			this.gx4 = null;
			this.state = JPakeParticipant.STATE_ROUND_3_VALIDATED;
		}

		// Token: 0x0400244C RID: 9292
		public static readonly int STATE_INITIALIZED = 0;

		// Token: 0x0400244D RID: 9293
		public static readonly int STATE_ROUND_1_CREATED = 10;

		// Token: 0x0400244E RID: 9294
		public static readonly int STATE_ROUND_1_VALIDATED = 20;

		// Token: 0x0400244F RID: 9295
		public static readonly int STATE_ROUND_2_CREATED = 30;

		// Token: 0x04002450 RID: 9296
		public static readonly int STATE_ROUND_2_VALIDATED = 40;

		// Token: 0x04002451 RID: 9297
		public static readonly int STATE_KEY_CALCULATED = 50;

		// Token: 0x04002452 RID: 9298
		public static readonly int STATE_ROUND_3_CREATED = 60;

		// Token: 0x04002453 RID: 9299
		public static readonly int STATE_ROUND_3_VALIDATED = 70;

		// Token: 0x04002454 RID: 9300
		private string participantId;

		// Token: 0x04002455 RID: 9301
		private char[] password;

		// Token: 0x04002456 RID: 9302
		private IDigest digest;

		// Token: 0x04002457 RID: 9303
		private readonly SecureRandom random;

		// Token: 0x04002458 RID: 9304
		private readonly BigInteger p;

		// Token: 0x04002459 RID: 9305
		private readonly BigInteger q;

		// Token: 0x0400245A RID: 9306
		private readonly BigInteger g;

		// Token: 0x0400245B RID: 9307
		private string partnerParticipantId;

		// Token: 0x0400245C RID: 9308
		private BigInteger x1;

		// Token: 0x0400245D RID: 9309
		private BigInteger x2;

		// Token: 0x0400245E RID: 9310
		private BigInteger gx1;

		// Token: 0x0400245F RID: 9311
		private BigInteger gx2;

		// Token: 0x04002460 RID: 9312
		private BigInteger gx3;

		// Token: 0x04002461 RID: 9313
		private BigInteger gx4;

		// Token: 0x04002462 RID: 9314
		private BigInteger b;

		// Token: 0x04002463 RID: 9315
		private int state;
	}
}
