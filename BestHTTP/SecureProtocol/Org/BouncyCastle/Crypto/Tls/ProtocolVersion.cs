using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000428 RID: 1064
	public sealed class ProtocolVersion
	{
		// Token: 0x06002A57 RID: 10839 RVA: 0x00114D52 File Offset: 0x00112F52
		private ProtocolVersion(int v, string name)
		{
			this.version = (v & 65535);
			this.name = name;
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x00114D6E File Offset: 0x00112F6E
		public int FullVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x00114D76 File Offset: 0x00112F76
		public int MajorVersion
		{
			get
			{
				return this.version >> 8;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x00114D80 File Offset: 0x00112F80
		public int MinorVersion
		{
			get
			{
				return this.version & 255;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x00114D8E File Offset: 0x00112F8E
		public bool IsDtls
		{
			get
			{
				return this.MajorVersion == 254;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x00114D9D File Offset: 0x00112F9D
		public bool IsSsl
		{
			get
			{
				return this == ProtocolVersion.SSLv3;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x00114DA7 File Offset: 0x00112FA7
		public bool IsTls
		{
			get
			{
				return this.MajorVersion == 3;
			}
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x00114DB2 File Offset: 0x00112FB2
		public ProtocolVersion GetEquivalentTLSVersion()
		{
			if (!this.IsDtls)
			{
				return this;
			}
			if (this == ProtocolVersion.DTLSv10)
			{
				return ProtocolVersion.TLSv11;
			}
			return ProtocolVersion.TLSv12;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00114DD4 File Offset: 0x00112FD4
		public bool IsEqualOrEarlierVersionOf(ProtocolVersion version)
		{
			if (this.MajorVersion != version.MajorVersion)
			{
				return false;
			}
			int num = version.MinorVersion - this.MinorVersion;
			if (!this.IsDtls)
			{
				return num >= 0;
			}
			return num <= 0;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x00114E18 File Offset: 0x00113018
		public bool IsLaterVersionOf(ProtocolVersion version)
		{
			if (this.MajorVersion != version.MajorVersion)
			{
				return false;
			}
			int num = version.MinorVersion - this.MinorVersion;
			if (!this.IsDtls)
			{
				return num < 0;
			}
			return num > 0;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x00114E54 File Offset: 0x00113054
		public override bool Equals(object other)
		{
			return this == other || (other is ProtocolVersion && this.Equals((ProtocolVersion)other));
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00114E72 File Offset: 0x00113072
		public bool Equals(ProtocolVersion other)
		{
			return other != null && this.version == other.version;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x00114D6E File Offset: 0x00112F6E
		public override int GetHashCode()
		{
			return this.version;
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x00114E88 File Offset: 0x00113088
		public static ProtocolVersion Get(int major, int minor)
		{
			if (major != 3)
			{
				if (major != 254)
				{
					throw new TlsFatalAlert(47);
				}
				switch (minor)
				{
				case 253:
					return ProtocolVersion.DTLSv12;
				case 254:
					throw new TlsFatalAlert(47);
				case 255:
					return ProtocolVersion.DTLSv10;
				default:
					return ProtocolVersion.GetUnknownVersion(major, minor, "DTLS");
				}
			}
			else
			{
				switch (minor)
				{
				case 0:
					return ProtocolVersion.SSLv3;
				case 1:
					return ProtocolVersion.TLSv10;
				case 2:
					return ProtocolVersion.TLSv11;
				case 3:
					return ProtocolVersion.TLSv12;
				default:
					return ProtocolVersion.GetUnknownVersion(major, minor, "TLS");
				}
			}
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00114F22 File Offset: 0x00113122
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x00114F2C File Offset: 0x0011312C
		private static ProtocolVersion GetUnknownVersion(int major, int minor, string prefix)
		{
			TlsUtilities.CheckUint8(major);
			TlsUtilities.CheckUint8(minor);
			int num = major << 8 | minor;
			string str = Platform.ToUpperInvariant(Convert.ToString(65536 | num, 16).Substring(1));
			return new ProtocolVersion(num, prefix + " 0x" + str);
		}

		// Token: 0x04001C67 RID: 7271
		public static readonly ProtocolVersion SSLv3 = new ProtocolVersion(768, "SSL 3.0");

		// Token: 0x04001C68 RID: 7272
		public static readonly ProtocolVersion TLSv10 = new ProtocolVersion(769, "TLS 1.0");

		// Token: 0x04001C69 RID: 7273
		public static readonly ProtocolVersion TLSv11 = new ProtocolVersion(770, "TLS 1.1");

		// Token: 0x04001C6A RID: 7274
		public static readonly ProtocolVersion TLSv12 = new ProtocolVersion(771, "TLS 1.2");

		// Token: 0x04001C6B RID: 7275
		public static readonly ProtocolVersion DTLSv10 = new ProtocolVersion(65279, "DTLS 1.0");

		// Token: 0x04001C6C RID: 7276
		public static readonly ProtocolVersion DTLSv12 = new ProtocolVersion(65277, "DTLS 1.2");

		// Token: 0x04001C6D RID: 7277
		private readonly int version;

		// Token: 0x04001C6E RID: 7278
		private readonly string name;
	}
}
