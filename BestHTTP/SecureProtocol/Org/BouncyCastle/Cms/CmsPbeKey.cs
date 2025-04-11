using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005DF RID: 1503
	public abstract class CmsPbeKey : ICipherParameters
	{
		// Token: 0x0600397D RID: 14717 RVA: 0x0016A141 File Offset: 0x00168341
		[Obsolete("Use version taking 'char[]' instead")]
		public CmsPbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x0016A151 File Offset: 0x00168351
		[Obsolete("Use version taking 'char[]' instead")]
		public CmsPbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x0016A160 File Offset: 0x00168360
		public CmsPbeKey(char[] password, byte[] salt, int iterationCount)
		{
			this.password = (char[])password.Clone();
			this.salt = Arrays.Clone(salt);
			this.iterationCount = iterationCount;
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x0016A18C File Offset: 0x0016838C
		public CmsPbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm)
		{
			if (!keyDerivationAlgorithm.Algorithm.Equals(PkcsObjectIdentifiers.IdPbkdf2))
			{
				throw new ArgumentException("Unsupported key derivation algorithm: " + keyDerivationAlgorithm.Algorithm);
			}
			Pbkdf2Params instance = Pbkdf2Params.GetInstance(keyDerivationAlgorithm.Parameters.ToAsn1Object());
			this.password = (char[])password.Clone();
			this.salt = instance.GetSalt();
			this.iterationCount = instance.IterationCount.IntValue;
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x0016A208 File Offset: 0x00168408
		~CmsPbeKey()
		{
			Array.Clear(this.password, 0, this.password.Length);
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x0016A244 File Offset: 0x00168444
		[Obsolete("Will be removed")]
		public string Password
		{
			get
			{
				return new string(this.password);
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x0016A251 File Offset: 0x00168451
		public byte[] Salt
		{
			get
			{
				return Arrays.Clone(this.salt);
			}
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x0016A25E File Offset: 0x0016845E
		[Obsolete("Use 'Salt' property instead")]
		public byte[] GetSalt()
		{
			return this.Salt;
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x0016A266 File Offset: 0x00168466
		public int IterationCount
		{
			get
			{
				return this.iterationCount;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x0016A26E File Offset: 0x0016846E
		public string Algorithm
		{
			get
			{
				return "PKCS5S2";
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x0016A275 File Offset: 0x00168475
		public string Format
		{
			get
			{
				return "RAW";
			}
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x0008F86E File Offset: 0x0008DA6E
		public byte[] GetEncoded()
		{
			return null;
		}

		// Token: 0x06003989 RID: 14729
		internal abstract KeyParameter GetEncoded(string algorithmOid);

		// Token: 0x040024C8 RID: 9416
		internal readonly char[] password;

		// Token: 0x040024C9 RID: 9417
		internal readonly byte[] salt;

		// Token: 0x040024CA RID: 9418
		internal readonly int iterationCount;
	}
}
