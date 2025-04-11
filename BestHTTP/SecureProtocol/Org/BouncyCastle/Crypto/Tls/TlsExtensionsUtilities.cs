using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000456 RID: 1110
	public abstract class TlsExtensionsUtilities
	{
		// Token: 0x06002BBA RID: 11194 RVA: 0x001196CB File Offset: 0x001178CB
		public static IDictionary EnsureExtensionsInitialised(IDictionary extensions)
		{
			if (extensions != null)
			{
				return extensions;
			}
			return Platform.CreateHashtable();
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x001196D7 File Offset: 0x001178D7
		public static void AddClientCertificateTypeExtensionClient(IDictionary extensions, byte[] certificateTypes)
		{
			extensions[19] = TlsExtensionsUtilities.CreateCertificateTypeExtensionClient(certificateTypes);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x001196EC File Offset: 0x001178EC
		public static void AddClientCertificateTypeExtensionServer(IDictionary extensions, byte certificateType)
		{
			extensions[19] = TlsExtensionsUtilities.CreateCertificateTypeExtensionServer(certificateType);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00119701 File Offset: 0x00117901
		public static void AddEncryptThenMacExtension(IDictionary extensions)
		{
			extensions[22] = TlsExtensionsUtilities.CreateEncryptThenMacExtension();
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x00119715 File Offset: 0x00117915
		public static void AddExtendedMasterSecretExtension(IDictionary extensions)
		{
			extensions[23] = TlsExtensionsUtilities.CreateExtendedMasterSecretExtension();
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00119729 File Offset: 0x00117929
		public static void AddHeartbeatExtension(IDictionary extensions, HeartbeatExtension heartbeatExtension)
		{
			extensions[15] = TlsExtensionsUtilities.CreateHeartbeatExtension(heartbeatExtension);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x0011973E File Offset: 0x0011793E
		public static void AddMaxFragmentLengthExtension(IDictionary extensions, byte maxFragmentLength)
		{
			extensions[1] = TlsExtensionsUtilities.CreateMaxFragmentLengthExtension(maxFragmentLength);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00119752 File Offset: 0x00117952
		public static void AddPaddingExtension(IDictionary extensions, int dataLength)
		{
			extensions[21] = TlsExtensionsUtilities.CreatePaddingExtension(dataLength);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x00119767 File Offset: 0x00117967
		public static void AddServerCertificateTypeExtensionClient(IDictionary extensions, byte[] certificateTypes)
		{
			extensions[20] = TlsExtensionsUtilities.CreateCertificateTypeExtensionClient(certificateTypes);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x0011977C File Offset: 0x0011797C
		public static void AddServerCertificateTypeExtensionServer(IDictionary extensions, byte certificateType)
		{
			extensions[20] = TlsExtensionsUtilities.CreateCertificateTypeExtensionServer(certificateType);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x00119791 File Offset: 0x00117991
		public static void AddServerNameExtension(IDictionary extensions, ServerNameList serverNameList)
		{
			extensions[0] = TlsExtensionsUtilities.CreateServerNameExtension(serverNameList);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x001197A5 File Offset: 0x001179A5
		public static void AddStatusRequestExtension(IDictionary extensions, CertificateStatusRequest statusRequest)
		{
			extensions[5] = TlsExtensionsUtilities.CreateStatusRequestExtension(statusRequest);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x001197B9 File Offset: 0x001179B9
		public static void AddTruncatedHMacExtension(IDictionary extensions)
		{
			extensions[4] = TlsExtensionsUtilities.CreateTruncatedHMacExtension();
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x001197CC File Offset: 0x001179CC
		public static byte[] GetClientCertificateTypeExtensionClient(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 19);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadCertificateTypeExtensionClient(extensionData);
			}
			return null;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x001197F0 File Offset: 0x001179F0
		public static short GetClientCertificateTypeExtensionServer(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 19);
			if (extensionData != null)
			{
				return (short)TlsExtensionsUtilities.ReadCertificateTypeExtensionServer(extensionData);
			}
			return -1;
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x00119814 File Offset: 0x00117A14
		public static HeartbeatExtension GetHeartbeatExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 15);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadHeartbeatExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x00119838 File Offset: 0x00117A38
		public static short GetMaxFragmentLengthExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 1);
			if (extensionData != null)
			{
				return (short)TlsExtensionsUtilities.ReadMaxFragmentLengthExtension(extensionData);
			}
			return -1;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x00119858 File Offset: 0x00117A58
		public static int GetPaddingExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 21);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadPaddingExtension(extensionData);
			}
			return -1;
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x0011987C File Offset: 0x00117A7C
		public static byte[] GetServerCertificateTypeExtensionClient(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 20);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadCertificateTypeExtensionClient(extensionData);
			}
			return null;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x001198A0 File Offset: 0x00117AA0
		public static short GetServerCertificateTypeExtensionServer(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 20);
			if (extensionData != null)
			{
				return (short)TlsExtensionsUtilities.ReadCertificateTypeExtensionServer(extensionData);
			}
			return -1;
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x001198C4 File Offset: 0x00117AC4
		public static ServerNameList GetServerNameExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 0);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadServerNameExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x001198E4 File Offset: 0x00117AE4
		public static CertificateStatusRequest GetStatusRequestExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 5);
			if (extensionData != null)
			{
				return TlsExtensionsUtilities.ReadStatusRequestExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x00119904 File Offset: 0x00117B04
		public static bool HasEncryptThenMacExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 22);
			return extensionData != null && TlsExtensionsUtilities.ReadEncryptThenMacExtension(extensionData);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x00119928 File Offset: 0x00117B28
		public static bool HasExtendedMasterSecretExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 23);
			return extensionData != null && TlsExtensionsUtilities.ReadExtendedMasterSecretExtension(extensionData);
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0011994C File Offset: 0x00117B4C
		public static bool HasTruncatedHMacExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 4);
			return extensionData != null && TlsExtensionsUtilities.ReadTruncatedHMacExtension(extensionData);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x00118128 File Offset: 0x00116328
		public static byte[] CreateCertificateTypeExtensionClient(byte[] certificateTypes)
		{
			if (certificateTypes == null || certificateTypes.Length < 1 || certificateTypes.Length > 255)
			{
				throw new TlsFatalAlert(80);
			}
			return TlsUtilities.EncodeUint8ArrayWithUint8Length(certificateTypes);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x0011814B File Offset: 0x0011634B
		public static byte[] CreateCertificateTypeExtensionServer(byte certificateType)
		{
			return TlsUtilities.EncodeUint8(certificateType);
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x0011996C File Offset: 0x00117B6C
		public static byte[] CreateEmptyExtensionData()
		{
			return TlsUtilities.EmptyBytes;
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x00119973 File Offset: 0x00117B73
		public static byte[] CreateEncryptThenMacExtension()
		{
			return TlsExtensionsUtilities.CreateEmptyExtensionData();
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00119973 File Offset: 0x00117B73
		public static byte[] CreateExtendedMasterSecretExtension()
		{
			return TlsExtensionsUtilities.CreateEmptyExtensionData();
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x0011997C File Offset: 0x00117B7C
		public static byte[] CreateHeartbeatExtension(HeartbeatExtension heartbeatExtension)
		{
			if (heartbeatExtension == null)
			{
				throw new TlsFatalAlert(80);
			}
			MemoryStream memoryStream = new MemoryStream();
			heartbeatExtension.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x0011814B File Offset: 0x0011634B
		public static byte[] CreateMaxFragmentLengthExtension(byte maxFragmentLength)
		{
			return TlsUtilities.EncodeUint8(maxFragmentLength);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x001199A7 File Offset: 0x00117BA7
		public static byte[] CreatePaddingExtension(int dataLength)
		{
			TlsUtilities.CheckUint16(dataLength);
			return new byte[dataLength];
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x001199B8 File Offset: 0x00117BB8
		public static byte[] CreateServerNameExtension(ServerNameList serverNameList)
		{
			if (serverNameList == null)
			{
				throw new TlsFatalAlert(80);
			}
			MemoryStream memoryStream = new MemoryStream();
			serverNameList.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x001199E4 File Offset: 0x00117BE4
		public static byte[] CreateStatusRequestExtension(CertificateStatusRequest statusRequest)
		{
			if (statusRequest == null)
			{
				throw new TlsFatalAlert(80);
			}
			MemoryStream memoryStream = new MemoryStream();
			statusRequest.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x00119973 File Offset: 0x00117B73
		public static byte[] CreateTruncatedHMacExtension()
		{
			return TlsExtensionsUtilities.CreateEmptyExtensionData();
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x00119A0F File Offset: 0x00117C0F
		private static bool ReadEmptyExtensionData(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			if (extensionData.Length != 0)
			{
				throw new TlsFatalAlert(47);
			}
			return true;
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x00118153 File Offset: 0x00116353
		public static byte[] ReadCertificateTypeExtensionClient(byte[] extensionData)
		{
			byte[] array = TlsUtilities.DecodeUint8ArrayWithUint8Length(extensionData);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(50);
			}
			return array;
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x00118169 File Offset: 0x00116369
		public static byte ReadCertificateTypeExtensionServer(byte[] extensionData)
		{
			return TlsUtilities.DecodeUint8(extensionData);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x00119A2C File Offset: 0x00117C2C
		public static bool ReadEncryptThenMacExtension(byte[] extensionData)
		{
			return TlsExtensionsUtilities.ReadEmptyExtensionData(extensionData);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x00119A2C File Offset: 0x00117C2C
		public static bool ReadExtendedMasterSecretExtension(byte[] extensionData)
		{
			return TlsExtensionsUtilities.ReadEmptyExtensionData(extensionData);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x00119A34 File Offset: 0x00117C34
		public static HeartbeatExtension ReadHeartbeatExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			HeartbeatExtension result = HeartbeatExtension.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x00118169 File Offset: 0x00116369
		public static byte ReadMaxFragmentLengthExtension(byte[] extensionData)
		{
			return TlsUtilities.DecodeUint8(extensionData);
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x00119A64 File Offset: 0x00117C64
		public static int ReadPaddingExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			for (int i = 0; i < extensionData.Length; i++)
			{
				if (extensionData[i] != 0)
				{
					throw new TlsFatalAlert(47);
				}
			}
			return extensionData.Length;
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x00119AA0 File Offset: 0x00117CA0
		public static ServerNameList ReadServerNameExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			ServerNameList result = ServerNameList.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x00119AD0 File Offset: 0x00117CD0
		public static CertificateStatusRequest ReadStatusRequestExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			CertificateStatusRequest result = CertificateStatusRequest.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x00119A2C File Offset: 0x00117C2C
		public static bool ReadTruncatedHMacExtension(byte[] extensionData)
		{
			return TlsExtensionsUtilities.ReadEmptyExtensionData(extensionData);
		}
	}
}
