using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x020005B9 RID: 1465
	public class Srp6Server
	{
		// Token: 0x06003898 RID: 14488 RVA: 0x00166CD7 File Offset: 0x00164ED7
		public virtual void Init(BigInteger N, BigInteger g, BigInteger v, IDigest digest, SecureRandom random)
		{
			this.N = N;
			this.g = g;
			this.v = v;
			this.random = random;
			this.digest = digest;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x00166CFE File Offset: 0x00164EFE
		public virtual void Init(Srp6GroupParameters group, BigInteger v, IDigest digest, SecureRandom random)
		{
			this.Init(group.N, group.G, v, digest, random);
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x00166D18 File Offset: 0x00164F18
		public virtual BigInteger GenerateServerCredentials()
		{
			BigInteger bigInteger = Srp6Utilities.CalculateK(this.digest, this.N, this.g);
			this.privB = this.SelectPrivateValue();
			this.pubB = bigInteger.Multiply(this.v).Mod(this.N).Add(this.g.ModPow(this.privB, this.N)).Mod(this.N);
			return this.pubB;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x00166D94 File Offset: 0x00164F94
		public virtual BigInteger CalculateSecret(BigInteger clientA)
		{
			this.A = Srp6Utilities.ValidatePublicValue(this.N, clientA);
			this.u = Srp6Utilities.CalculateU(this.digest, this.N, this.A, this.pubB);
			this.S = this.CalculateS();
			return this.S;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x00166DE8 File Offset: 0x00164FE8
		protected virtual BigInteger SelectPrivateValue()
		{
			return Srp6Utilities.GeneratePrivateValue(this.digest, this.N, this.g, this.random);
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x00166E07 File Offset: 0x00165007
		private BigInteger CalculateS()
		{
			return this.v.ModPow(this.u, this.N).Multiply(this.A).Mod(this.N).ModPow(this.privB, this.N);
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x00166E48 File Offset: 0x00165048
		public virtual bool VerifyClientEvidenceMessage(BigInteger clientM1)
		{
			if (this.A == null || this.pubB == null || this.S == null)
			{
				throw new CryptoException("Impossible to compute and verify M1: some data are missing from the previous operations (A,B,S)");
			}
			if (Srp6Utilities.CalculateM1(this.digest, this.N, this.A, this.pubB, this.S).Equals(clientM1))
			{
				this.M1 = clientM1;
				return true;
			}
			return false;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x00166EB0 File Offset: 0x001650B0
		public virtual BigInteger CalculateServerEvidenceMessage()
		{
			if (this.A == null || this.M1 == null || this.S == null)
			{
				throw new CryptoException("Impossible to compute M2: some data are missing from the previous operations (A,M1,S)");
			}
			this.M2 = Srp6Utilities.CalculateM2(this.digest, this.N, this.A, this.M1, this.S);
			return this.M2;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x00166F10 File Offset: 0x00165110
		public virtual BigInteger CalculateSessionKey()
		{
			if (this.S == null || this.M1 == null || this.M2 == null)
			{
				throw new CryptoException("Impossible to compute Key: some data are missing from the previous operations (S,M1,M2)");
			}
			this.Key = Srp6Utilities.CalculateKey(this.digest, this.N, this.S);
			return this.Key;
		}

		// Token: 0x04002416 RID: 9238
		protected BigInteger N;

		// Token: 0x04002417 RID: 9239
		protected BigInteger g;

		// Token: 0x04002418 RID: 9240
		protected BigInteger v;

		// Token: 0x04002419 RID: 9241
		protected SecureRandom random;

		// Token: 0x0400241A RID: 9242
		protected IDigest digest;

		// Token: 0x0400241B RID: 9243
		protected BigInteger A;

		// Token: 0x0400241C RID: 9244
		protected BigInteger privB;

		// Token: 0x0400241D RID: 9245
		protected BigInteger pubB;

		// Token: 0x0400241E RID: 9246
		protected BigInteger u;

		// Token: 0x0400241F RID: 9247
		protected BigInteger S;

		// Token: 0x04002420 RID: 9248
		protected BigInteger M1;

		// Token: 0x04002421 RID: 9249
		protected BigInteger M2;

		// Token: 0x04002422 RID: 9250
		protected BigInteger Key;
	}
}
