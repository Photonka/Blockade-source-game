using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000474 RID: 1140
	public abstract class TlsSrpUtilities
	{
		// Token: 0x06002CDF RID: 11487 RVA: 0x0011D1E6 File Offset: 0x0011B3E6
		public static void AddSrpExtension(IDictionary extensions, byte[] identity)
		{
			extensions[12] = TlsSrpUtilities.CreateSrpExtension(identity);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x0011D1FC File Offset: 0x0011B3FC
		public static byte[] GetSrpExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 12);
			if (extensionData != null)
			{
				return TlsSrpUtilities.ReadSrpExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x0011D21D File Offset: 0x0011B41D
		public static byte[] CreateSrpExtension(byte[] identity)
		{
			if (identity == null)
			{
				throw new TlsFatalAlert(80);
			}
			return TlsUtilities.EncodeOpaque8(identity);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x0011D230 File Offset: 0x0011B430
		public static byte[] ReadSrpExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			byte[] result = TlsUtilities.ReadOpaque8(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x001184FB File Offset: 0x001166FB
		public static BigInteger ReadSrpParameter(Stream input)
		{
			return new BigInteger(1, TlsUtilities.ReadOpaque16(input));
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x00118552 File Offset: 0x00116752
		public static void WriteSrpParameter(BigInteger x, Stream output)
		{
			TlsUtilities.WriteOpaque16(BigIntegers.AsUnsignedByteArray(x), output);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x0011D25F File Offset: 0x0011B45F
		public static bool IsSrpCipherSuite(int cipherSuite)
		{
			return cipherSuite - 49178 <= 8;
		}
	}
}
