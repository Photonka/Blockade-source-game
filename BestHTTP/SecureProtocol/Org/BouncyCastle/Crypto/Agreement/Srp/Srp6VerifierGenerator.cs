using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x020005BC RID: 1468
	public class Srp6VerifierGenerator
	{
		// Token: 0x060038B2 RID: 14514 RVA: 0x00167250 File Offset: 0x00165450
		public virtual void Init(BigInteger N, BigInteger g, IDigest digest)
		{
			this.N = N;
			this.g = g;
			this.digest = digest;
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x00167267 File Offset: 0x00165467
		public virtual void Init(Srp6GroupParameters group, IDigest digest)
		{
			this.Init(group.N, group.G, digest);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x0016727C File Offset: 0x0016547C
		public virtual BigInteger GenerateVerifier(byte[] salt, byte[] identity, byte[] password)
		{
			BigInteger e = Srp6Utilities.CalculateX(this.digest, this.N, salt, identity, password);
			return this.g.ModPow(e, this.N);
		}

		// Token: 0x04002438 RID: 9272
		protected BigInteger N;

		// Token: 0x04002439 RID: 9273
		protected BigInteger g;

		// Token: 0x0400243A RID: 9274
		protected IDigest digest;
	}
}
