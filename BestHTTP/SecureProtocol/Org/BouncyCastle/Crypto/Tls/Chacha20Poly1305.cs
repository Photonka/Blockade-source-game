using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003F3 RID: 1011
	public class Chacha20Poly1305 : TlsCipher
	{
		// Token: 0x0600292A RID: 10538 RVA: 0x001102F4 File Offset: 0x0010E4F4
		public Chacha20Poly1305(TlsContext context)
		{
			if (!TlsUtilities.IsTlsV12(context))
			{
				throw new TlsFatalAlert(80);
			}
			this.context = context;
			int num = 32;
			int num2 = 12;
			int num3 = 2 * num + 2 * num2;
			byte[] array = TlsUtilities.CalculateKeyBlock(context, num3);
			int num4 = 0;
			KeyParameter keyParameter = new KeyParameter(array, num4, num);
			num4 += num;
			KeyParameter keyParameter2 = new KeyParameter(array, num4, num);
			num4 += num;
			byte[] array2 = Arrays.CopyOfRange(array, num4, num4 + num2);
			num4 += num2;
			byte[] array3 = Arrays.CopyOfRange(array, num4, num4 + num2);
			num4 += num2;
			if (num4 != num3)
			{
				throw new TlsFatalAlert(80);
			}
			this.encryptCipher = new ChaCha7539Engine();
			this.decryptCipher = new ChaCha7539Engine();
			KeyParameter parameters;
			KeyParameter parameters2;
			if (context.IsServer)
			{
				parameters = keyParameter2;
				parameters2 = keyParameter;
				this.encryptIV = array3;
				this.decryptIV = array2;
			}
			else
			{
				parameters = keyParameter;
				parameters2 = keyParameter2;
				this.encryptIV = array2;
				this.decryptIV = array3;
			}
			this.encryptCipher.Init(true, new ParametersWithIV(parameters, this.encryptIV));
			this.decryptCipher.Init(false, new ParametersWithIV(parameters2, this.decryptIV));
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x001103FE File Offset: 0x0010E5FE
		public virtual int GetPlaintextLimit(int ciphertextLimit)
		{
			return ciphertextLimit - 16;
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x00110404 File Offset: 0x0010E604
		public virtual byte[] EncodePlaintext(long seqNo, byte type, byte[] plaintext, int offset, int len)
		{
			KeyParameter macKey = this.InitRecord(this.encryptCipher, true, seqNo, this.encryptIV);
			byte[] array = new byte[len + 16];
			this.encryptCipher.ProcessBytes(plaintext, offset, len, array, 0);
			byte[] additionalData = this.GetAdditionalData(seqNo, type, len);
			byte[] array2 = this.CalculateRecordMac(macKey, additionalData, array, 0, len);
			Array.Copy(array2, 0, array, len, array2.Length);
			return array;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x0011046C File Offset: 0x0010E66C
		public virtual byte[] DecodeCiphertext(long seqNo, byte type, byte[] ciphertext, int offset, int len)
		{
			if (this.GetPlaintextLimit(len) < 0)
			{
				throw new TlsFatalAlert(50);
			}
			KeyParameter macKey = this.InitRecord(this.decryptCipher, false, seqNo, this.decryptIV);
			int num = len - 16;
			byte[] additionalData = this.GetAdditionalData(seqNo, type, num);
			byte[] a = this.CalculateRecordMac(macKey, additionalData, ciphertext, offset, num);
			byte[] b = Arrays.CopyOfRange(ciphertext, offset + num, offset + len);
			if (!Arrays.ConstantTimeAreEqual(a, b))
			{
				throw new TlsFatalAlert(20);
			}
			byte[] array = new byte[num];
			this.decryptCipher.ProcessBytes(ciphertext, offset, num, array, 0);
			return array;
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x001104FC File Offset: 0x0010E6FC
		protected virtual KeyParameter InitRecord(IStreamCipher cipher, bool forEncryption, long seqNo, byte[] iv)
		{
			byte[] iv2 = this.CalculateNonce(seqNo, iv);
			cipher.Init(forEncryption, new ParametersWithIV(null, iv2));
			return this.GenerateRecordMacKey(cipher);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00110528 File Offset: 0x0010E728
		protected virtual byte[] CalculateNonce(long seqNo, byte[] iv)
		{
			byte[] array = new byte[12];
			TlsUtilities.WriteUint64(seqNo, array, 4);
			for (int i = 0; i < 12; i++)
			{
				byte[] array2 = array;
				int num = i;
				array2[num] ^= iv[i];
			}
			return array;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x00110564 File Offset: 0x0010E764
		protected virtual KeyParameter GenerateRecordMacKey(IStreamCipher cipher)
		{
			byte[] array = new byte[64];
			cipher.ProcessBytes(array, 0, array.Length, array, 0);
			KeyParameter result = new KeyParameter(array, 0, 32);
			Arrays.Fill(array, 0);
			return result;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x00110598 File Offset: 0x0010E798
		protected virtual byte[] CalculateRecordMac(KeyParameter macKey, byte[] additionalData, byte[] buf, int off, int len)
		{
			IMac mac = new Poly1305();
			mac.Init(macKey);
			this.UpdateRecordMacText(mac, additionalData, 0, additionalData.Length);
			this.UpdateRecordMacText(mac, buf, off, len);
			this.UpdateRecordMacLength(mac, additionalData.Length);
			this.UpdateRecordMacLength(mac, len);
			return MacUtilities.DoFinal(mac);
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x001105E4 File Offset: 0x0010E7E4
		protected virtual void UpdateRecordMacLength(IMac mac, int len)
		{
			byte[] array = Pack.UInt64_To_LE((ulong)((long)len));
			mac.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x00110604 File Offset: 0x0010E804
		protected virtual void UpdateRecordMacText(IMac mac, byte[] buf, int off, int len)
		{
			mac.BlockUpdate(buf, off, len);
			int num = len % 16;
			if (num != 0)
			{
				mac.BlockUpdate(Chacha20Poly1305.Zeroes, 0, 16 - num);
			}
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x00110634 File Offset: 0x0010E834
		protected virtual byte[] GetAdditionalData(long seqNo, byte type, int len)
		{
			byte[] array = new byte[13];
			TlsUtilities.WriteUint64(seqNo, array, 0);
			TlsUtilities.WriteUint8(type, array, 8);
			TlsUtilities.WriteVersion(this.context.ServerVersion, array, 9);
			TlsUtilities.WriteUint16(len, array, 11);
			return array;
		}

		// Token: 0x04001A3A RID: 6714
		private static readonly byte[] Zeroes = new byte[15];

		// Token: 0x04001A3B RID: 6715
		protected readonly TlsContext context;

		// Token: 0x04001A3C RID: 6716
		protected readonly ChaCha7539Engine encryptCipher;

		// Token: 0x04001A3D RID: 6717
		protected readonly ChaCha7539Engine decryptCipher;

		// Token: 0x04001A3E RID: 6718
		protected readonly byte[] encryptIV;

		// Token: 0x04001A3F RID: 6719
		protected readonly byte[] decryptIV;
	}
}
