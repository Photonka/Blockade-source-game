using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x020005B8 RID: 1464
	public class Srp6Client
	{
		// Token: 0x0600388E RID: 14478 RVA: 0x00166A2B File Offset: 0x00164C2B
		public virtual void Init(BigInteger N, BigInteger g, IDigest digest, SecureRandom random)
		{
			this.N = N;
			this.g = g;
			this.digest = digest;
			this.random = random;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x00166A4A File Offset: 0x00164C4A
		public virtual void Init(Srp6GroupParameters group, IDigest digest, SecureRandom random)
		{
			this.Init(group.N, group.G, digest, random);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x00166A60 File Offset: 0x00164C60
		public virtual BigInteger GenerateClientCredentials(byte[] salt, byte[] identity, byte[] password)
		{
			this.x = Srp6Utilities.CalculateX(this.digest, this.N, salt, identity, password);
			this.privA = this.SelectPrivateValue();
			this.pubA = this.g.ModPow(this.privA, this.N);
			return this.pubA;
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x00166AB8 File Offset: 0x00164CB8
		public virtual BigInteger CalculateSecret(BigInteger serverB)
		{
			this.B = Srp6Utilities.ValidatePublicValue(this.N, serverB);
			this.u = Srp6Utilities.CalculateU(this.digest, this.N, this.pubA, this.B);
			this.S = this.CalculateS();
			return this.S;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x00166B0C File Offset: 0x00164D0C
		protected virtual BigInteger SelectPrivateValue()
		{
			return Srp6Utilities.GeneratePrivateValue(this.digest, this.N, this.g, this.random);
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x00166B2C File Offset: 0x00164D2C
		private BigInteger CalculateS()
		{
			BigInteger val = Srp6Utilities.CalculateK(this.digest, this.N, this.g);
			BigInteger e = this.u.Multiply(this.x).Add(this.privA);
			BigInteger n = this.g.ModPow(this.x, this.N).Multiply(val).Mod(this.N);
			return this.B.Subtract(n).Mod(this.N).ModPow(e, this.N);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x00166BBC File Offset: 0x00164DBC
		public virtual BigInteger CalculateClientEvidenceMessage()
		{
			if (this.pubA == null || this.B == null || this.S == null)
			{
				throw new CryptoException("Impossible to compute M1: some data are missing from the previous operations (A,B,S)");
			}
			this.M1 = Srp6Utilities.CalculateM1(this.digest, this.N, this.pubA, this.B, this.S);
			return this.M1;
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x00166C1C File Offset: 0x00164E1C
		public virtual bool VerifyServerEvidenceMessage(BigInteger serverM2)
		{
			if (this.pubA == null || this.M1 == null || this.S == null)
			{
				throw new CryptoException("Impossible to compute and verify M2: some data are missing from the previous operations (A,M1,S)");
			}
			if (Srp6Utilities.CalculateM2(this.digest, this.N, this.pubA, this.M1, this.S).Equals(serverM2))
			{
				this.M2 = serverM2;
				return true;
			}
			return false;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x00166C84 File Offset: 0x00164E84
		public virtual BigInteger CalculateSessionKey()
		{
			if (this.S == null || this.M1 == null || this.M2 == null)
			{
				throw new CryptoException("Impossible to compute Key: some data are missing from the previous operations (S,M1,M2)");
			}
			this.Key = Srp6Utilities.CalculateKey(this.digest, this.N, this.S);
			return this.Key;
		}

		// Token: 0x04002409 RID: 9225
		protected BigInteger N;

		// Token: 0x0400240A RID: 9226
		protected BigInteger g;

		// Token: 0x0400240B RID: 9227
		protected BigInteger privA;

		// Token: 0x0400240C RID: 9228
		protected BigInteger pubA;

		// Token: 0x0400240D RID: 9229
		protected BigInteger B;

		// Token: 0x0400240E RID: 9230
		protected BigInteger x;

		// Token: 0x0400240F RID: 9231
		protected BigInteger u;

		// Token: 0x04002410 RID: 9232
		protected BigInteger S;

		// Token: 0x04002411 RID: 9233
		protected BigInteger M1;

		// Token: 0x04002412 RID: 9234
		protected BigInteger M2;

		// Token: 0x04002413 RID: 9235
		protected BigInteger Key;

		// Token: 0x04002414 RID: 9236
		protected IDigest digest;

		// Token: 0x04002415 RID: 9237
		protected SecureRandom random;
	}
}
