using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200042E RID: 1070
	public class ServerNameList
	{
		// Token: 0x06002AA5 RID: 10917 RVA: 0x0011591C File Offset: 0x00113B1C
		public ServerNameList(IList serverNameList)
		{
			if (serverNameList == null)
			{
				throw new ArgumentNullException("serverNameList");
			}
			this.mServerNameList = serverNameList;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x00115939 File Offset: 0x00113B39
		public virtual IList ServerNames
		{
			get
			{
				return this.mServerNameList;
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00115944 File Offset: 0x00113B44
		public virtual void Encode(Stream output)
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = TlsUtilities.EmptyBytes;
			foreach (object obj in this.ServerNames)
			{
				ServerName serverName = (ServerName)obj;
				array = ServerNameList.CheckNameType(array, serverName.NameType);
				if (array == null)
				{
					throw new TlsFatalAlert(80);
				}
				serverName.Encode(memoryStream);
			}
			TlsUtilities.CheckUint16(memoryStream.Length);
			TlsUtilities.WriteUint16((int)memoryStream.Length, output);
			Streams.WriteBufTo(memoryStream, output);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x001159E4 File Offset: 0x00113BE4
		public static ServerNameList Parse(Stream input)
		{
			int num = TlsUtilities.ReadUint16(input);
			if (num < 1)
			{
				throw new TlsFatalAlert(50);
			}
			MemoryStream memoryStream = new MemoryStream(TlsUtilities.ReadFully(num, input), false);
			byte[] array = TlsUtilities.EmptyBytes;
			IList list = Platform.CreateArrayList();
			while (memoryStream.Position < memoryStream.Length)
			{
				ServerName serverName = ServerName.Parse(memoryStream);
				array = ServerNameList.CheckNameType(array, serverName.NameType);
				if (array == null)
				{
					throw new TlsFatalAlert(47);
				}
				list.Add(serverName);
			}
			return new ServerNameList(list);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x00115A59 File Offset: 0x00113C59
		private static byte[] CheckNameType(byte[] nameTypesSeen, byte nameType)
		{
			if (!NameType.IsValid(nameType) || Arrays.Contains(nameTypesSeen, nameType))
			{
				return null;
			}
			return Arrays.Append(nameTypesSeen, nameType);
		}

		// Token: 0x04001C9C RID: 7324
		protected readonly IList mServerNameList;
	}
}
