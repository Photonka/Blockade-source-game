using System;
using System.Collections;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000475 RID: 1141
	public abstract class TlsSRTPUtils
	{
		// Token: 0x06002CE7 RID: 11495 RVA: 0x0011D26E File Offset: 0x0011B46E
		public static void AddUseSrtpExtension(IDictionary extensions, UseSrtpData useSRTPData)
		{
			extensions[14] = TlsSRTPUtils.CreateUseSrtpExtension(useSRTPData);
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x0011D284 File Offset: 0x0011B484
		public static UseSrtpData GetUseSrtpExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 14);
			if (extensionData != null)
			{
				return TlsSRTPUtils.ReadUseSrtpExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x0011D2A8 File Offset: 0x0011B4A8
		public static byte[] CreateUseSrtpExtension(UseSrtpData useSrtpData)
		{
			if (useSrtpData == null)
			{
				throw new ArgumentNullException("useSrtpData");
			}
			MemoryStream memoryStream = new MemoryStream();
			TlsUtilities.WriteUint16ArrayWithUint16Length(useSrtpData.ProtectionProfiles, memoryStream);
			TlsUtilities.WriteOpaque8(useSrtpData.Mki, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x0011D2E8 File Offset: 0x0011B4E8
		public static UseSrtpData ReadUseSrtpExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, true);
			int num = TlsUtilities.ReadUint16(memoryStream);
			if (num < 2 || (num & 1) != 0)
			{
				throw new TlsFatalAlert(50);
			}
			int[] protectionProfiles = TlsUtilities.ReadUint16Array(num / 2, memoryStream);
			byte[] mki = TlsUtilities.ReadOpaque8(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return new UseSrtpData(protectionProfiles, mki);
		}
	}
}
