using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003F9 RID: 1017
	internal class CombinedHash : TlsHandshakeHash, IDigest
	{
		// Token: 0x0600293C RID: 10556 RVA: 0x00110699 File Offset: 0x0010E899
		internal CombinedHash()
		{
			this.mMd5 = TlsUtilities.CreateHash(1);
			this.mSha1 = TlsUtilities.CreateHash(2);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x001106B9 File Offset: 0x0010E8B9
		internal CombinedHash(CombinedHash t)
		{
			this.mContext = t.mContext;
			this.mMd5 = TlsUtilities.CloneHash(1, t.mMd5);
			this.mSha1 = TlsUtilities.CloneHash(2, t.mSha1);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x001106F1 File Offset: 0x0010E8F1
		public virtual void Init(TlsContext context)
		{
			this.mContext = context;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public virtual TlsHandshakeHash NotifyPrfDetermined()
		{
			return this;
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x001106FA File Offset: 0x0010E8FA
		public virtual void TrackHashAlgorithm(byte hashAlgorithm)
		{
			throw new InvalidOperationException("CombinedHash only supports calculating the legacy PRF for handshake hash");
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void SealHashAlgorithms()
		{
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x00110706 File Offset: 0x0010E906
		public virtual TlsHandshakeHash StopTracking()
		{
			return new CombinedHash(this);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x00110706 File Offset: 0x0010E906
		public virtual IDigest ForkPrfHash()
		{
			return new CombinedHash(this);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0011070E File Offset: 0x0010E90E
		public virtual byte[] GetFinalHash(byte hashAlgorithm)
		{
			throw new InvalidOperationException("CombinedHash doesn't support multiple hashes");
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x0011071A File Offset: 0x0010E91A
		public virtual string AlgorithmName
		{
			get
			{
				return this.mMd5.AlgorithmName + " and " + this.mSha1.AlgorithmName;
			}
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0011073C File Offset: 0x0010E93C
		public virtual int GetByteLength()
		{
			return Math.Max(this.mMd5.GetByteLength(), this.mSha1.GetByteLength());
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x00110759 File Offset: 0x0010E959
		public virtual int GetDigestSize()
		{
			return this.mMd5.GetDigestSize() + this.mSha1.GetDigestSize();
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x00110772 File Offset: 0x0010E972
		public virtual void Update(byte input)
		{
			this.mMd5.Update(input);
			this.mSha1.Update(input);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0011078C File Offset: 0x0010E98C
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.mMd5.BlockUpdate(input, inOff, len);
			this.mSha1.BlockUpdate(input, inOff, len);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x001107AC File Offset: 0x0010E9AC
		public virtual int DoFinal(byte[] output, int outOff)
		{
			if (this.mContext != null && TlsUtilities.IsSsl(this.mContext))
			{
				this.Ssl3Complete(this.mMd5, Ssl3Mac.IPAD, Ssl3Mac.OPAD, 48);
				this.Ssl3Complete(this.mSha1, Ssl3Mac.IPAD, Ssl3Mac.OPAD, 40);
			}
			int num = this.mMd5.DoFinal(output, outOff);
			int num2 = this.mSha1.DoFinal(output, outOff + num);
			return num + num2;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x0011081F File Offset: 0x0010EA1F
		public virtual void Reset()
		{
			this.mMd5.Reset();
			this.mSha1.Reset();
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x00110838 File Offset: 0x0010EA38
		protected virtual void Ssl3Complete(IDigest d, byte[] ipad, byte[] opad, int padLength)
		{
			byte[] masterSecret = this.mContext.SecurityParameters.masterSecret;
			d.BlockUpdate(masterSecret, 0, masterSecret.Length);
			d.BlockUpdate(ipad, 0, padLength);
			byte[] array = DigestUtilities.DoFinal(d);
			d.BlockUpdate(masterSecret, 0, masterSecret.Length);
			d.BlockUpdate(opad, 0, padLength);
			d.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x04001B5F RID: 7007
		protected TlsContext mContext;

		// Token: 0x04001B60 RID: 7008
		protected IDigest mMd5;

		// Token: 0x04001B61 RID: 7009
		protected IDigest mSha1;
	}
}
