using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs
{
	// Token: 0x020002B1 RID: 689
	public sealed class EncryptedPrivateKeyInfoFactory
	{
		// Token: 0x060019B9 RID: 6585 RVA: 0x00023EF4 File Offset: 0x000220F4
		private EncryptedPrivateKeyInfoFactory()
		{
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000C581C File Offset: 0x000C3A1C
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(DerObjectIdentifier algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm.Id, passPhrase, salt, iterationCount, PrivateKeyInfoFactory.CreatePrivateKeyInfo(key));
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000C5833 File Offset: 0x000C3A33
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(string algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm, passPhrase, salt, iterationCount, PrivateKeyInfoFactory.CreatePrivateKeyInfo(key));
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000C5848 File Offset: 0x000C3A48
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(string algorithm, char[] passPhrase, byte[] salt, int iterationCount, PrivateKeyInfo keyInfo)
		{
			IBufferedCipher bufferedCipher = PbeUtilities.CreateEngine(algorithm) as IBufferedCipher;
			if (bufferedCipher == null)
			{
				throw new Exception("Unknown encryption algorithm: " + algorithm);
			}
			Asn1Encodable asn1Encodable = PbeUtilities.GenerateAlgorithmParameters(algorithm, salt, iterationCount);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(algorithm, passPhrase, asn1Encodable);
			bufferedCipher.Init(true, parameters);
			byte[] encoding = bufferedCipher.DoFinal(keyInfo.GetEncoded());
			return new EncryptedPrivateKeyInfo(new AlgorithmIdentifier(PbeUtilities.GetObjectIdentifier(algorithm), asn1Encodable), encoding);
		}
	}
}
