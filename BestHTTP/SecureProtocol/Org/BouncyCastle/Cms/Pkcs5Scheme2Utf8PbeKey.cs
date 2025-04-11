using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x02000600 RID: 1536
	public class Pkcs5Scheme2Utf8PbeKey : CmsPbeKey
	{
		// Token: 0x06003A7F RID: 14975 RVA: 0x0016E572 File Offset: 0x0016C772
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2Utf8PbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x0016E582 File Offset: 0x0016C782
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2Utf8PbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x0016E523 File Offset: 0x0016C723
		public Pkcs5Scheme2Utf8PbeKey(char[] password, byte[] salt, int iterationCount) : base(password, salt, iterationCount)
		{
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x0016E52E File Offset: 0x0016C72E
		public Pkcs5Scheme2Utf8PbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm) : base(password, keyDerivationAlgorithm)
		{
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x0016E591 File Offset: 0x0016C791
		internal override KeyParameter GetEncoded(string algorithmOid)
		{
			Pkcs5S2ParametersGenerator pkcs5S2ParametersGenerator = new Pkcs5S2ParametersGenerator();
			pkcs5S2ParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToUtf8Bytes(this.password), this.salt, this.iterationCount);
			return (KeyParameter)pkcs5S2ParametersGenerator.GenerateDerivedParameters(algorithmOid, CmsEnvelopedHelper.Instance.GetKeySize(algorithmOid));
		}
	}
}
