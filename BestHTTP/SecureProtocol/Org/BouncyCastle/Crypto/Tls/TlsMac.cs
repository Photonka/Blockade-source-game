using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200045B RID: 1115
	public class TlsMac
	{
		// Token: 0x06002C05 RID: 11269 RVA: 0x00119B48 File Offset: 0x00117D48
		public TlsMac(TlsContext context, IDigest digest, byte[] key, int keyOff, int keyLen)
		{
			this.context = context;
			KeyParameter keyParameter = new KeyParameter(key, keyOff, keyLen);
			this.secret = Arrays.Clone(keyParameter.GetKey());
			if (digest is LongDigest)
			{
				this.digestBlockSize = 128;
				this.digestOverhead = 16;
			}
			else
			{
				this.digestBlockSize = 64;
				this.digestOverhead = 8;
			}
			if (TlsUtilities.IsSsl(context))
			{
				this.mac = new Ssl3Mac(digest);
				if (digest.GetDigestSize() == 20)
				{
					this.digestOverhead = 4;
				}
			}
			else
			{
				this.mac = new HMac(digest);
			}
			this.mac.Init(keyParameter);
			this.macLength = this.mac.GetMacSize();
			if (context.SecurityParameters.truncatedHMac)
			{
				this.macLength = Math.Min(this.macLength, 10);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x00119C1A File Offset: 0x00117E1A
		public virtual byte[] MacSecret
		{
			get
			{
				return this.secret;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x00119C22 File Offset: 0x00117E22
		public virtual int Size
		{
			get
			{
				return this.macLength;
			}
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00119C2C File Offset: 0x00117E2C
		public virtual byte[] CalculateMac(long seqNo, byte type, byte[] message, int offset, int length)
		{
			ProtocolVersion serverVersion = this.context.ServerVersion;
			bool isSsl = serverVersion.IsSsl;
			byte[] array = new byte[isSsl ? 11 : 13];
			TlsUtilities.WriteUint64(seqNo, array, 0);
			TlsUtilities.WriteUint8(type, array, 8);
			if (!isSsl)
			{
				TlsUtilities.WriteVersion(serverVersion, array, 9);
			}
			TlsUtilities.WriteUint16(length, array, array.Length - 2);
			this.mac.BlockUpdate(array, 0, array.Length);
			this.mac.BlockUpdate(message, offset, length);
			return this.Truncate(MacUtilities.DoFinal(this.mac));
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x00119CB4 File Offset: 0x00117EB4
		public virtual byte[] CalculateMacConstantTime(long seqNo, byte type, byte[] message, int offset, int length, int fullLength, byte[] dummyData)
		{
			byte[] result = this.CalculateMac(seqNo, type, message, offset, length);
			int num = TlsUtilities.IsSsl(this.context) ? 11 : 13;
			int num2 = this.GetDigestBlockCount(num + fullLength) - this.GetDigestBlockCount(num + length);
			while (--num2 >= 0)
			{
				this.mac.BlockUpdate(dummyData, 0, this.digestBlockSize);
			}
			this.mac.Update(dummyData[0]);
			this.mac.Reset();
			return result;
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x00119D32 File Offset: 0x00117F32
		protected virtual int GetDigestBlockCount(int inputLength)
		{
			return (inputLength + this.digestOverhead) / this.digestBlockSize;
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00119D43 File Offset: 0x00117F43
		protected virtual byte[] Truncate(byte[] bs)
		{
			if (bs.Length <= this.macLength)
			{
				return bs;
			}
			return Arrays.CopyOf(bs, this.macLength);
		}

		// Token: 0x04001D09 RID: 7433
		protected readonly TlsContext context;

		// Token: 0x04001D0A RID: 7434
		protected readonly byte[] secret;

		// Token: 0x04001D0B RID: 7435
		protected readonly IMac mac;

		// Token: 0x04001D0C RID: 7436
		protected readonly int digestBlockSize;

		// Token: 0x04001D0D RID: 7437
		protected readonly int digestOverhead;

		// Token: 0x04001D0E RID: 7438
		protected readonly int macLength;
	}
}
