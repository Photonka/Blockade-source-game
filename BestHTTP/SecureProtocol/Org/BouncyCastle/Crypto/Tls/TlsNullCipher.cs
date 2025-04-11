using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200045D RID: 1117
	public class TlsNullCipher : TlsCipher
	{
		// Token: 0x06002C0D RID: 11277 RVA: 0x00119D6B File Offset: 0x00117F6B
		public TlsNullCipher(TlsContext context)
		{
			this.context = context;
			this.writeMac = null;
			this.readMac = null;
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x00119D88 File Offset: 0x00117F88
		public TlsNullCipher(TlsContext context, IDigest clientWriteDigest, IDigest serverWriteDigest)
		{
			if (clientWriteDigest == null != (serverWriteDigest == null))
			{
				throw new TlsFatalAlert(80);
			}
			this.context = context;
			TlsMac tlsMac = null;
			TlsMac tlsMac2 = null;
			if (clientWriteDigest != null)
			{
				int num = clientWriteDigest.GetDigestSize() + serverWriteDigest.GetDigestSize();
				byte[] key = TlsUtilities.CalculateKeyBlock(context, num);
				int num2 = 0;
				tlsMac = new TlsMac(context, clientWriteDigest, key, num2, clientWriteDigest.GetDigestSize());
				num2 += clientWriteDigest.GetDigestSize();
				tlsMac2 = new TlsMac(context, serverWriteDigest, key, num2, serverWriteDigest.GetDigestSize());
				num2 += serverWriteDigest.GetDigestSize();
				if (num2 != num)
				{
					throw new TlsFatalAlert(80);
				}
			}
			if (context.IsServer)
			{
				this.writeMac = tlsMac2;
				this.readMac = tlsMac;
				return;
			}
			this.writeMac = tlsMac;
			this.readMac = tlsMac2;
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x00119E40 File Offset: 0x00118040
		public virtual int GetPlaintextLimit(int ciphertextLimit)
		{
			int num = ciphertextLimit;
			if (this.writeMac != null)
			{
				num -= this.writeMac.Size;
			}
			return num;
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00119E68 File Offset: 0x00118068
		public virtual byte[] EncodePlaintext(long seqNo, byte type, byte[] plaintext, int offset, int len)
		{
			if (this.writeMac == null)
			{
				return Arrays.CopyOfRange(plaintext, offset, offset + len);
			}
			byte[] array = this.writeMac.CalculateMac(seqNo, type, plaintext, offset, len);
			byte[] array2 = new byte[len + array.Length];
			Array.Copy(plaintext, offset, array2, 0, len);
			Array.Copy(array, 0, array2, len, array.Length);
			return array2;
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x00119EC4 File Offset: 0x001180C4
		public virtual byte[] DecodeCiphertext(long seqNo, byte type, byte[] ciphertext, int offset, int len)
		{
			if (this.readMac == null)
			{
				return Arrays.CopyOfRange(ciphertext, offset, offset + len);
			}
			int size = this.readMac.Size;
			if (len < size)
			{
				throw new TlsFatalAlert(50);
			}
			int num = len - size;
			byte[] a = Arrays.CopyOfRange(ciphertext, offset + num, offset + len);
			byte[] b = this.readMac.CalculateMac(seqNo, type, ciphertext, offset, num);
			if (!Arrays.ConstantTimeAreEqual(a, b))
			{
				throw new TlsFatalAlert(20);
			}
			return Arrays.CopyOfRange(ciphertext, offset, offset + num);
		}

		// Token: 0x04001D0F RID: 7439
		protected readonly TlsContext context;

		// Token: 0x04001D10 RID: 7440
		protected readonly TlsMac writeMac;

		// Token: 0x04001D11 RID: 7441
		protected readonly TlsMac readMac;
	}
}
