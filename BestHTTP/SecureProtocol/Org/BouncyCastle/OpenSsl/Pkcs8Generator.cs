using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.Pem;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.OpenSsl
{
	// Token: 0x020002C0 RID: 704
	public class Pkcs8Generator : PemObjectGenerator
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x000C9342 File Offset: 0x000C7542
		public Pkcs8Generator(AsymmetricKeyParameter privKey)
		{
			this.privKey = privKey;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000C9351 File Offset: 0x000C7551
		public Pkcs8Generator(AsymmetricKeyParameter privKey, string algorithm)
		{
			this.privKey = privKey;
			this.algorithm = algorithm;
			this.iterationCount = 2048;
		}

		// Token: 0x17000358 RID: 856
		// (set) Token: 0x06001A2B RID: 6699 RVA: 0x000C9372 File Offset: 0x000C7572
		public SecureRandom SecureRandom
		{
			set
			{
				this.random = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x000C937B File Offset: 0x000C757B
		public char[] Password
		{
			set
			{
				this.password = value;
			}
		}

		// Token: 0x1700035A RID: 858
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x000C9384 File Offset: 0x000C7584
		public int IterationCount
		{
			set
			{
				this.iterationCount = value;
			}
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000C9390 File Offset: 0x000C7590
		public PemObject Generate()
		{
			if (this.algorithm == null)
			{
				PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(this.privKey);
				return new PemObject("PRIVATE KEY", privateKeyInfo.GetEncoded());
			}
			byte[] array = new byte[20];
			if (this.random == null)
			{
				this.random = new SecureRandom();
			}
			this.random.NextBytes(array);
			PemObject result;
			try
			{
				EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(this.algorithm, this.password, array, this.iterationCount, this.privKey);
				result = new PemObject("ENCRYPTED PRIVATE KEY", encryptedPrivateKeyInfo.GetEncoded());
			}
			catch (Exception exception)
			{
				throw new PemGenerationException("Couldn't encrypt private key", exception);
			}
			return result;
		}

		// Token: 0x04001795 RID: 6037
		public static readonly string PbeSha1_RC4_128 = PkcsObjectIdentifiers.PbeWithShaAnd128BitRC4.Id;

		// Token: 0x04001796 RID: 6038
		public static readonly string PbeSha1_RC4_40 = PkcsObjectIdentifiers.PbeWithShaAnd40BitRC4.Id;

		// Token: 0x04001797 RID: 6039
		public static readonly string PbeSha1_3DES = PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc.Id;

		// Token: 0x04001798 RID: 6040
		public static readonly string PbeSha1_2DES = PkcsObjectIdentifiers.PbeWithShaAnd2KeyTripleDesCbc.Id;

		// Token: 0x04001799 RID: 6041
		public static readonly string PbeSha1_RC2_128 = PkcsObjectIdentifiers.PbeWithShaAnd128BitRC2Cbc.Id;

		// Token: 0x0400179A RID: 6042
		public static readonly string PbeSha1_RC2_40 = PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc.Id;

		// Token: 0x0400179B RID: 6043
		private char[] password;

		// Token: 0x0400179C RID: 6044
		private string algorithm;

		// Token: 0x0400179D RID: 6045
		private int iterationCount;

		// Token: 0x0400179E RID: 6046
		private AsymmetricKeyParameter privKey;

		// Token: 0x0400179F RID: 6047
		private SecureRandom random;
	}
}
