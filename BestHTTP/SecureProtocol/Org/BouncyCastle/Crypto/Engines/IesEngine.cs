using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000566 RID: 1382
	public class IesEngine
	{
		// Token: 0x06003484 RID: 13444 RVA: 0x001451D0 File Offset: 0x001433D0
		public IesEngine(IBasicAgreement agree, IDerivationFunction kdf, IMac mac)
		{
			this.agree = agree;
			this.kdf = kdf;
			this.mac = mac;
			this.macBuf = new byte[mac.GetMacSize()];
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x001451FE File Offset: 0x001433FE
		public IesEngine(IBasicAgreement agree, IDerivationFunction kdf, IMac mac, BufferedBlockCipher cipher)
		{
			this.agree = agree;
			this.kdf = kdf;
			this.mac = mac;
			this.macBuf = new byte[mac.GetMacSize()];
			this.cipher = cipher;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x00145234 File Offset: 0x00143434
		public virtual void Init(bool forEncryption, ICipherParameters privParameters, ICipherParameters pubParameters, ICipherParameters iesParameters)
		{
			this.forEncryption = forEncryption;
			this.privParam = privParameters;
			this.pubParam = pubParameters;
			this.param = (IesParameters)iesParameters;
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x00145258 File Offset: 0x00143458
		private byte[] DecryptBlock(byte[] in_enc, int inOff, int inLen, byte[] z)
		{
			KdfParameters kdfParameters = new KdfParameters(z, this.param.GetDerivationV());
			int macKeySize = this.param.MacKeySize;
			this.kdf.Init(kdfParameters);
			if (inLen < this.mac.GetMacSize())
			{
				throw new InvalidCipherTextException("Length of input must be greater than the MAC");
			}
			inLen -= this.mac.GetMacSize();
			byte[] array2;
			KeyParameter parameters;
			if (this.cipher == null)
			{
				byte[] array = this.GenerateKdfBytes(kdfParameters, inLen + macKeySize / 8);
				array2 = new byte[inLen];
				for (int num = 0; num != inLen; num++)
				{
					array2[num] = (in_enc[inOff + num] ^ array[num]);
				}
				parameters = new KeyParameter(array, inLen, macKeySize / 8);
			}
			else
			{
				int cipherKeySize = ((IesWithCipherParameters)this.param).CipherKeySize;
				byte[] key = this.GenerateKdfBytes(kdfParameters, cipherKeySize / 8 + macKeySize / 8);
				this.cipher.Init(false, new KeyParameter(key, 0, cipherKeySize / 8));
				array2 = this.cipher.DoFinal(in_enc, inOff, inLen);
				parameters = new KeyParameter(key, cipherKeySize / 8, macKeySize / 8);
			}
			byte[] encodingV = this.param.GetEncodingV();
			this.mac.Init(parameters);
			this.mac.BlockUpdate(in_enc, inOff, inLen);
			this.mac.BlockUpdate(encodingV, 0, encodingV.Length);
			this.mac.DoFinal(this.macBuf, 0);
			inOff += inLen;
			if (!Arrays.ConstantTimeAreEqual(Arrays.CopyOfRange(in_enc, inOff, inOff + this.macBuf.Length), this.macBuf))
			{
				throw new InvalidCipherTextException("Invalid MAC.");
			}
			return array2;
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x001453DC File Offset: 0x001435DC
		private byte[] EncryptBlock(byte[] input, int inOff, int inLen, byte[] z)
		{
			KdfParameters kParam = new KdfParameters(z, this.param.GetDerivationV());
			int macKeySize = this.param.MacKeySize;
			byte[] array2;
			int num;
			KeyParameter parameters;
			if (this.cipher == null)
			{
				byte[] array = this.GenerateKdfBytes(kParam, inLen + macKeySize / 8);
				array2 = new byte[inLen + this.mac.GetMacSize()];
				num = inLen;
				for (int num2 = 0; num2 != inLen; num2++)
				{
					array2[num2] = (input[inOff + num2] ^ array[num2]);
				}
				parameters = new KeyParameter(array, inLen, macKeySize / 8);
			}
			else
			{
				int cipherKeySize = ((IesWithCipherParameters)this.param).CipherKeySize;
				byte[] key = this.GenerateKdfBytes(kParam, cipherKeySize / 8 + macKeySize / 8);
				this.cipher.Init(true, new KeyParameter(key, 0, cipherKeySize / 8));
				num = this.cipher.GetOutputSize(inLen);
				byte[] array3 = new byte[num];
				int num3 = this.cipher.ProcessBytes(input, inOff, inLen, array3, 0);
				num3 += this.cipher.DoFinal(array3, num3);
				array2 = new byte[num3 + this.mac.GetMacSize()];
				num = num3;
				Array.Copy(array3, 0, array2, 0, num3);
				parameters = new KeyParameter(key, cipherKeySize / 8, macKeySize / 8);
			}
			byte[] encodingV = this.param.GetEncodingV();
			this.mac.Init(parameters);
			this.mac.BlockUpdate(array2, 0, num);
			this.mac.BlockUpdate(encodingV, 0, encodingV.Length);
			this.mac.DoFinal(array2, num);
			return array2;
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x00145564 File Offset: 0x00143764
		private byte[] GenerateKdfBytes(KdfParameters kParam, int length)
		{
			byte[] array = new byte[length];
			this.kdf.Init(kParam);
			this.kdf.GenerateBytes(array, 0, array.Length);
			return array;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x00145598 File Offset: 0x00143798
		public virtual byte[] ProcessBlock(byte[] input, int inOff, int inLen)
		{
			this.agree.Init(this.privParam);
			BigInteger n = this.agree.CalculateAgreement(this.pubParam);
			byte[] array = BigIntegers.AsUnsignedByteArray(this.agree.GetFieldSize(), n);
			byte[] result;
			try
			{
				result = (this.forEncryption ? this.EncryptBlock(input, inOff, inLen, array) : this.DecryptBlock(input, inOff, inLen, array));
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
			}
			return result;
		}

		// Token: 0x04002169 RID: 8553
		private readonly IBasicAgreement agree;

		// Token: 0x0400216A RID: 8554
		private readonly IDerivationFunction kdf;

		// Token: 0x0400216B RID: 8555
		private readonly IMac mac;

		// Token: 0x0400216C RID: 8556
		private readonly BufferedBlockCipher cipher;

		// Token: 0x0400216D RID: 8557
		private readonly byte[] macBuf;

		// Token: 0x0400216E RID: 8558
		private bool forEncryption;

		// Token: 0x0400216F RID: 8559
		private ICipherParameters privParam;

		// Token: 0x04002170 RID: 8560
		private ICipherParameters pubParam;

		// Token: 0x04002171 RID: 8561
		private IesParameters param;
	}
}
