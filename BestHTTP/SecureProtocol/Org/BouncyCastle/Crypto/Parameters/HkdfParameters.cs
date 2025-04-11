using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D1 RID: 1233
	public class HkdfParameters : IDerivationParameters
	{
		// Token: 0x06002FE6 RID: 12262 RVA: 0x00128D40 File Offset: 0x00126F40
		private HkdfParameters(byte[] ikm, bool skip, byte[] salt, byte[] info)
		{
			if (ikm == null)
			{
				throw new ArgumentNullException("ikm");
			}
			this.ikm = Arrays.Clone(ikm);
			this.skipExpand = skip;
			if (salt == null || salt.Length == 0)
			{
				this.salt = null;
			}
			else
			{
				this.salt = Arrays.Clone(salt);
			}
			if (info == null)
			{
				this.info = new byte[0];
				return;
			}
			this.info = Arrays.Clone(info);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x00128DAE File Offset: 0x00126FAE
		public HkdfParameters(byte[] ikm, byte[] salt, byte[] info) : this(ikm, false, salt, info)
		{
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x00128DBA File Offset: 0x00126FBA
		public static HkdfParameters SkipExtractParameters(byte[] ikm, byte[] info)
		{
			return new HkdfParameters(ikm, true, null, info);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x00128DC5 File Offset: 0x00126FC5
		public static HkdfParameters DefaultParameters(byte[] ikm)
		{
			return new HkdfParameters(ikm, false, null, null);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x00128DD0 File Offset: 0x00126FD0
		public virtual byte[] GetIkm()
		{
			return Arrays.Clone(this.ikm);
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x00128DDD File Offset: 0x00126FDD
		public virtual bool SkipExtract
		{
			get
			{
				return this.skipExpand;
			}
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x00128DE5 File Offset: 0x00126FE5
		public virtual byte[] GetSalt()
		{
			return Arrays.Clone(this.salt);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x00128DF2 File Offset: 0x00126FF2
		public virtual byte[] GetInfo()
		{
			return Arrays.Clone(this.info);
		}

		// Token: 0x04001EC2 RID: 7874
		private readonly byte[] ikm;

		// Token: 0x04001EC3 RID: 7875
		private readonly bool skipExpand;

		// Token: 0x04001EC4 RID: 7876
		private readonly byte[] salt;

		// Token: 0x04001EC5 RID: 7877
		private readonly byte[] info;
	}
}
