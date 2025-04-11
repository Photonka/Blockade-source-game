using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200047B RID: 1147
	public class UseSrtpData
	{
		// Token: 0x06002D8C RID: 11660 RVA: 0x00120340 File Offset: 0x0011E540
		public UseSrtpData(int[] protectionProfiles, byte[] mki)
		{
			if (protectionProfiles == null || protectionProfiles.Length < 1 || protectionProfiles.Length >= 32768)
			{
				throw new ArgumentException("must have length from 1 to (2^15 - 1)", "protectionProfiles");
			}
			if (mki == null)
			{
				mki = TlsUtilities.EmptyBytes;
			}
			else if (mki.Length > 255)
			{
				throw new ArgumentException("cannot be longer than 255 bytes", "mki");
			}
			this.mProtectionProfiles = protectionProfiles;
			this.mMki = mki;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x001203AA File Offset: 0x0011E5AA
		public virtual int[] ProtectionProfiles
		{
			get
			{
				return this.mProtectionProfiles;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x001203B2 File Offset: 0x0011E5B2
		public virtual byte[] Mki
		{
			get
			{
				return this.mMki;
			}
		}

		// Token: 0x04001D82 RID: 7554
		protected readonly int[] mProtectionProfiles;

		// Token: 0x04001D83 RID: 7555
		protected readonly byte[] mMki;
	}
}
