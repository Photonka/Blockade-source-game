using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200042C RID: 1068
	public class SecurityParameters
	{
		// Token: 0x06002A90 RID: 10896 RVA: 0x0011575C File Offset: 0x0011395C
		internal virtual void Clear()
		{
			if (this.masterSecret != null)
			{
				Arrays.Fill(this.masterSecret, 0);
				this.masterSecret = null;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x00115779 File Offset: 0x00113979
		public virtual int Entity
		{
			get
			{
				return this.entity;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x00115781 File Offset: 0x00113981
		public virtual int CipherSuite
		{
			get
			{
				return this.cipherSuite;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x00115789 File Offset: 0x00113989
		public virtual byte CompressionAlgorithm
		{
			get
			{
				return this.compressionAlgorithm;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x00115791 File Offset: 0x00113991
		public virtual int PrfAlgorithm
		{
			get
			{
				return this.prfAlgorithm;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x00115799 File Offset: 0x00113999
		public virtual int VerifyDataLength
		{
			get
			{
				return this.verifyDataLength;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x001157A1 File Offset: 0x001139A1
		public virtual byte[] MasterSecret
		{
			get
			{
				return this.masterSecret;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x001157A9 File Offset: 0x001139A9
		public virtual byte[] ClientRandom
		{
			get
			{
				return this.clientRandom;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x001157B1 File Offset: 0x001139B1
		public virtual byte[] ServerRandom
		{
			get
			{
				return this.serverRandom;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x001157B9 File Offset: 0x001139B9
		public virtual byte[] SessionHash
		{
			get
			{
				return this.sessionHash;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x001157C1 File Offset: 0x001139C1
		public virtual byte[] PskIdentity
		{
			get
			{
				return this.pskIdentity;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x001157C9 File Offset: 0x001139C9
		public virtual byte[] SrpIdentity
		{
			get
			{
				return this.srpIdentity;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x001157D1 File Offset: 0x001139D1
		public virtual bool IsExtendedMasterSecret
		{
			get
			{
				return this.extendedMasterSecret;
			}
		}

		// Token: 0x04001C8B RID: 7307
		internal int entity = -1;

		// Token: 0x04001C8C RID: 7308
		internal int cipherSuite = -1;

		// Token: 0x04001C8D RID: 7309
		internal byte compressionAlgorithm;

		// Token: 0x04001C8E RID: 7310
		internal int prfAlgorithm = -1;

		// Token: 0x04001C8F RID: 7311
		internal int verifyDataLength = -1;

		// Token: 0x04001C90 RID: 7312
		internal byte[] masterSecret;

		// Token: 0x04001C91 RID: 7313
		internal byte[] clientRandom;

		// Token: 0x04001C92 RID: 7314
		internal byte[] serverRandom;

		// Token: 0x04001C93 RID: 7315
		internal byte[] sessionHash;

		// Token: 0x04001C94 RID: 7316
		internal byte[] pskIdentity;

		// Token: 0x04001C95 RID: 7317
		internal byte[] srpIdentity;

		// Token: 0x04001C96 RID: 7318
		internal short maxFragmentLength = -1;

		// Token: 0x04001C97 RID: 7319
		internal bool truncatedHMac;

		// Token: 0x04001C98 RID: 7320
		internal bool encryptThenMac;

		// Token: 0x04001C99 RID: 7321
		internal bool extendedMasterSecret;
	}
}
