using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200042D RID: 1069
	public class ServerName
	{
		// Token: 0x06002A9E RID: 10910 RVA: 0x00115804 File Offset: 0x00113A04
		public ServerName(byte nameType, object name)
		{
			if (!ServerName.IsCorrectType(nameType, name))
			{
				throw new ArgumentException("not an instance of the correct type", "name");
			}
			this.mNameType = nameType;
			this.mName = name;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x00115833 File Offset: 0x00113A33
		public virtual byte NameType
		{
			get
			{
				return this.mNameType;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x0011583B File Offset: 0x00113A3B
		public virtual object Name
		{
			get
			{
				return this.mName;
			}
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x00115843 File Offset: 0x00113A43
		public virtual string GetHostName()
		{
			if (!ServerName.IsCorrectType(0, this.mName))
			{
				throw new InvalidOperationException("'name' is not a HostName string");
			}
			return (string)this.mName;
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0011586C File Offset: 0x00113A6C
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mNameType, output);
			if (this.mNameType != 0)
			{
				throw new TlsFatalAlert(80);
			}
			byte[] array = Strings.ToAsciiByteArray((string)this.mName);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(80);
			}
			TlsUtilities.WriteOpaque16(array, output);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x001158BC File Offset: 0x00113ABC
		public static ServerName Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (b != 0)
			{
				throw new TlsFatalAlert(50);
			}
			byte[] array = TlsUtilities.ReadOpaque16(input);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(50);
			}
			object name = Strings.FromAsciiByteArray(array);
			return new ServerName(b, name);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x001158FE File Offset: 0x00113AFE
		protected static bool IsCorrectType(byte nameType, object name)
		{
			if (nameType == 0)
			{
				return name is string;
			}
			throw new ArgumentException("unsupported NameType", "nameType");
		}

		// Token: 0x04001C9A RID: 7322
		protected readonly byte mNameType;

		// Token: 0x04001C9B RID: 7323
		protected readonly object mName;
	}
}
