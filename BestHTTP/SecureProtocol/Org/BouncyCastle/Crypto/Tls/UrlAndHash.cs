using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000479 RID: 1145
	public class UrlAndHash
	{
		// Token: 0x06002D86 RID: 11654 RVA: 0x00120228 File Offset: 0x0011E428
		public UrlAndHash(string url, byte[] sha1Hash)
		{
			if (url == null || url.Length < 1 || url.Length >= 65536)
			{
				throw new ArgumentException("must have length from 1 to (2^16 - 1)", "url");
			}
			if (sha1Hash != null && sha1Hash.Length != 20)
			{
				throw new ArgumentException("must have length == 20, if present", "sha1Hash");
			}
			this.mUrl = url;
			this.mSha1Hash = sha1Hash;
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x0012028C File Offset: 0x0011E48C
		public virtual string Url
		{
			get
			{
				return this.mUrl;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x00120294 File Offset: 0x0011E494
		public virtual byte[] Sha1Hash
		{
			get
			{
				return this.mSha1Hash;
			}
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0012029C File Offset: 0x0011E49C
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteOpaque16(Strings.ToByteArray(this.mUrl), output);
			if (this.mSha1Hash == null)
			{
				TlsUtilities.WriteUint8(0, output);
				return;
			}
			TlsUtilities.WriteUint8(1, output);
			output.Write(this.mSha1Hash, 0, this.mSha1Hash.Length);
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x001202DC File Offset: 0x0011E4DC
		public static UrlAndHash Parse(TlsContext context, Stream input)
		{
			byte[] array = TlsUtilities.ReadOpaque16(input);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(47);
			}
			string url = Strings.FromByteArray(array);
			byte[] sha1Hash = null;
			byte b = TlsUtilities.ReadUint8(input);
			if (b != 0)
			{
				if (b != 1)
				{
					throw new TlsFatalAlert(47);
				}
				sha1Hash = TlsUtilities.ReadFully(20, input);
			}
			else if (TlsUtilities.IsTlsV12(context))
			{
				throw new TlsFatalAlert(47);
			}
			return new UrlAndHash(url, sha1Hash);
		}

		// Token: 0x04001D7F RID: 7551
		protected readonly string mUrl;

		// Token: 0x04001D80 RID: 7552
		protected readonly byte[] mSha1Hash;
	}
}
