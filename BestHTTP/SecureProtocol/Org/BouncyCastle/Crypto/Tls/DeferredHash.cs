using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000406 RID: 1030
	internal class DeferredHash : TlsHandshakeHash, IDigest
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x00111538 File Offset: 0x0010F738
		internal DeferredHash()
		{
			this.mBuf = new DigestInputBuffer();
			this.mHashes = Platform.CreateHashtable();
			this.mPrfHashAlgorithm = -1;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x0011155D File Offset: 0x0010F75D
		private DeferredHash(byte prfHashAlgorithm, IDigest prfHash)
		{
			this.mBuf = null;
			this.mHashes = Platform.CreateHashtable();
			this.mPrfHashAlgorithm = (int)prfHashAlgorithm;
			this.mHashes[prfHashAlgorithm] = prfHash;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00111590 File Offset: 0x0010F790
		public virtual void Init(TlsContext context)
		{
			this.mContext = context;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x0011159C File Offset: 0x0010F79C
		public virtual TlsHandshakeHash NotifyPrfDetermined()
		{
			int prfAlgorithm = this.mContext.SecurityParameters.PrfAlgorithm;
			if (prfAlgorithm == 0)
			{
				CombinedHash combinedHash = new CombinedHash();
				combinedHash.Init(this.mContext);
				this.mBuf.UpdateDigest(combinedHash);
				return combinedHash.NotifyPrfDetermined();
			}
			this.mPrfHashAlgorithm = (int)TlsUtilities.GetHashAlgorithmForPrfAlgorithm(prfAlgorithm);
			this.CheckTrackingHash((byte)this.mPrfHashAlgorithm);
			return this;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x001115FC File Offset: 0x0010F7FC
		public virtual void TrackHashAlgorithm(byte hashAlgorithm)
		{
			if (this.mBuf == null)
			{
				throw new InvalidOperationException("Too late to track more hash algorithms");
			}
			this.CheckTrackingHash(hashAlgorithm);
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00111618 File Offset: 0x0010F818
		public virtual void SealHashAlgorithms()
		{
			this.CheckStopBuffering();
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00111620 File Offset: 0x0010F820
		public virtual TlsHandshakeHash StopTracking()
		{
			byte b = (byte)this.mPrfHashAlgorithm;
			IDigest digest = TlsUtilities.CloneHash(b, (IDigest)this.mHashes[b]);
			if (this.mBuf != null)
			{
				this.mBuf.UpdateDigest(digest);
			}
			DeferredHash deferredHash = new DeferredHash(b, digest);
			deferredHash.Init(this.mContext);
			return deferredHash;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x0011167C File Offset: 0x0010F87C
		public virtual IDigest ForkPrfHash()
		{
			this.CheckStopBuffering();
			byte b = (byte)this.mPrfHashAlgorithm;
			if (this.mBuf != null)
			{
				IDigest digest = TlsUtilities.CreateHash(b);
				this.mBuf.UpdateDigest(digest);
				return digest;
			}
			return TlsUtilities.CloneHash(b, (IDigest)this.mHashes[b]);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x001116D0 File Offset: 0x0010F8D0
		public virtual byte[] GetFinalHash(byte hashAlgorithm)
		{
			IDigest digest = (IDigest)this.mHashes[hashAlgorithm];
			if (digest == null)
			{
				throw new InvalidOperationException("HashAlgorithm." + HashAlgorithm.GetText(hashAlgorithm) + " is not being tracked");
			}
			digest = TlsUtilities.CloneHash(hashAlgorithm, digest);
			if (this.mBuf != null)
			{
				this.mBuf.UpdateDigest(digest);
			}
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x00111734 File Offset: 0x0010F934
		public virtual string AlgorithmName
		{
			get
			{
				throw new InvalidOperationException("Use Fork() to get a definite IDigest");
			}
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x00111734 File Offset: 0x0010F934
		public virtual int GetByteLength()
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x00111734 File Offset: 0x0010F934
		public virtual int GetDigestSize()
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00111740 File Offset: 0x0010F940
		public virtual void Update(byte input)
		{
			if (this.mBuf != null)
			{
				this.mBuf.WriteByte(input);
				return;
			}
			foreach (object obj in this.mHashes.Values)
			{
				((IDigest)obj).Update(input);
			}
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x001117B4 File Offset: 0x0010F9B4
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (this.mBuf != null)
			{
				this.mBuf.Write(input, inOff, len);
				return;
			}
			foreach (object obj in this.mHashes.Values)
			{
				((IDigest)obj).BlockUpdate(input, inOff, len);
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x00111734 File Offset: 0x0010F934
		public virtual int DoFinal(byte[] output, int outOff)
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x0011182C File Offset: 0x0010FA2C
		public virtual void Reset()
		{
			if (this.mBuf != null)
			{
				this.mBuf.SetLength(0L);
				return;
			}
			foreach (object obj in this.mHashes.Values)
			{
				((IDigest)obj).Reset();
			}
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x001118A0 File Offset: 0x0010FAA0
		protected virtual void CheckStopBuffering()
		{
			if (this.mBuf != null && this.mHashes.Count <= 4)
			{
				foreach (object obj in this.mHashes.Values)
				{
					IDigest d = (IDigest)obj;
					this.mBuf.UpdateDigest(d);
				}
				this.mBuf = null;
			}
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00111920 File Offset: 0x0010FB20
		protected virtual void CheckTrackingHash(byte hashAlgorithm)
		{
			if (!this.mHashes.Contains(hashAlgorithm))
			{
				IDigest value = TlsUtilities.CreateHash(hashAlgorithm);
				this.mHashes[hashAlgorithm] = value;
			}
		}

		// Token: 0x04001B7E RID: 7038
		protected const int BUFFERING_HASH_LIMIT = 4;

		// Token: 0x04001B7F RID: 7039
		protected TlsContext mContext;

		// Token: 0x04001B80 RID: 7040
		private DigestInputBuffer mBuf;

		// Token: 0x04001B81 RID: 7041
		private IDictionary mHashes;

		// Token: 0x04001B82 RID: 7042
		private int mPrfHashAlgorithm;
	}
}
