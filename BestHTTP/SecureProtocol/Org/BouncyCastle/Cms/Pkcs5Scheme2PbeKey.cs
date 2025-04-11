using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005FF RID: 1535
	public class Pkcs5Scheme2PbeKey : CmsPbeKey
	{
		// Token: 0x06003A7A RID: 14970 RVA: 0x0016E504 File Offset: 0x0016C704
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2PbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x0016E514 File Offset: 0x0016C714
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2PbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0016E523 File Offset: 0x0016C723
		public Pkcs5Scheme2PbeKey(char[] password, byte[] salt, int iterationCount) : base(password, salt, iterationCount)
		{
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0016E52E File Offset: 0x0016C72E
		public Pkcs5Scheme2PbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm) : base(password, keyDerivationAlgorithm)
		{
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x0016E538 File Offset: 0x0016C738
		internal override KeyParameter GetEncoded(string algorithmOid)
		{
			Pkcs5S2ParametersGenerator pkcs5S2ParametersGenerator = new Pkcs5S2ParametersGenerator();
			pkcs5S2ParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(this.password), this.salt, this.iterationCount);
			return (KeyParameter)pkcs5S2ParametersGenerator.GenerateDerivedParameters(algorithmOid, CmsEnvelopedHelper.Instance.GetKeySize(algorithmOid));
		}
	}
}
