using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200040C RID: 1036
	public abstract class DtlsProtocol
	{
		// Token: 0x060029D5 RID: 10709 RVA: 0x00112987 File Offset: 0x00110B87
		protected DtlsProtocol(SecureRandom secureRandom)
		{
			if (secureRandom == null)
			{
				throw new ArgumentNullException("secureRandom");
			}
			this.mSecureRandom = secureRandom;
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x001129A4 File Offset: 0x00110BA4
		protected virtual void ProcessFinished(byte[] body, byte[] expected_verify_data)
		{
			MemoryStream memoryStream = new MemoryStream(body, false);
			byte[] b = TlsUtilities.ReadFully(expected_verify_data.Length, memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			if (!Arrays.ConstantTimeAreEqual(expected_verify_data, b))
			{
				throw new TlsFatalAlert(40);
			}
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x001129DC File Offset: 0x00110BDC
		internal static void ApplyMaxFragmentLengthExtension(DtlsRecordLayer recordLayer, short maxFragmentLength)
		{
			if (maxFragmentLength >= 0)
			{
				if (!MaxFragmentLength.IsValid((byte)maxFragmentLength))
				{
					throw new TlsFatalAlert(80);
				}
				int plaintextLimit = 1 << (int)(8 + maxFragmentLength);
				recordLayer.SetPlaintextLimit(plaintextLimit);
			}
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x00112A10 File Offset: 0x00110C10
		protected static short EvaluateMaxFragmentLengthExtension(bool resumedSession, IDictionary clientExtensions, IDictionary serverExtensions, byte alertDescription)
		{
			short maxFragmentLengthExtension = TlsExtensionsUtilities.GetMaxFragmentLengthExtension(serverExtensions);
			if (maxFragmentLengthExtension >= 0 && (!MaxFragmentLength.IsValid((byte)maxFragmentLengthExtension) || (!resumedSession && maxFragmentLengthExtension != TlsExtensionsUtilities.GetMaxFragmentLengthExtension(clientExtensions))))
			{
				throw new TlsFatalAlert(alertDescription);
			}
			return maxFragmentLengthExtension;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x00112A48 File Offset: 0x00110C48
		protected static byte[] GenerateCertificate(Certificate certificate)
		{
			MemoryStream memoryStream = new MemoryStream();
			certificate.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x00112A68 File Offset: 0x00110C68
		protected static byte[] GenerateSupplementalData(IList supplementalData)
		{
			MemoryStream memoryStream = new MemoryStream();
			TlsProtocol.WriteSupplementalData(memoryStream, supplementalData);
			return memoryStream.ToArray();
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x00112A7C File Offset: 0x00110C7C
		protected static void ValidateSelectedCipherSuite(int selectedCipherSuite, byte alertDescription)
		{
			int encryptionAlgorithm = TlsUtilities.GetEncryptionAlgorithm(selectedCipherSuite);
			if (encryptionAlgorithm - 1 <= 1)
			{
				throw new TlsFatalAlert(alertDescription);
			}
		}

		// Token: 0x04001B89 RID: 7049
		protected readonly SecureRandom mSecureRandom;
	}
}
