using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200048C RID: 1164
	public class Iso9796d2PssSigner : ISignerWithRecovery, ISigner
	{
		// Token: 0x06002E05 RID: 11781 RVA: 0x00122118 File Offset: 0x00120318
		public byte[] GetRecoveredMessage()
		{
			return this.recoveredMessage;
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x00122120 File Offset: 0x00120320
		public Iso9796d2PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLength, bool isImplicit)
		{
			this.cipher = cipher;
			this.digest = digest;
			this.hLen = digest.GetDigestSize();
			this.saltLength = saltLength;
			if (isImplicit)
			{
				this.trailer = 188;
				return;
			}
			if (IsoTrailers.NoTrailerAvailable(digest))
			{
				throw new ArgumentException("no valid trailer", "digest");
			}
			this.trailer = IsoTrailers.GetTrailer(digest);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x00122188 File Offset: 0x00120388
		public Iso9796d2PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLength) : this(cipher, digest, saltLength, false)
		{
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x00122194 File Offset: 0x00120394
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withISO9796-2S2";
			}
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x001221AC File Offset: 0x001203AC
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			RsaKeyParameters rsaKeyParameters;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				rsaKeyParameters = (RsaKeyParameters)parametersWithRandom.Parameters;
				if (forSigning)
				{
					this.random = parametersWithRandom.Random;
				}
			}
			else if (parameters is ParametersWithSalt)
			{
				if (!forSigning)
				{
					throw new ArgumentException("ParametersWithSalt only valid for signing", "parameters");
				}
				ParametersWithSalt parametersWithSalt = (ParametersWithSalt)parameters;
				rsaKeyParameters = (RsaKeyParameters)parametersWithSalt.Parameters;
				this.standardSalt = parametersWithSalt.GetSalt();
				if (this.standardSalt.Length != this.saltLength)
				{
					throw new ArgumentException("Fixed salt is of wrong length");
				}
			}
			else
			{
				rsaKeyParameters = (RsaKeyParameters)parameters;
				if (forSigning)
				{
					this.random = new SecureRandom();
				}
			}
			this.cipher.Init(forSigning, rsaKeyParameters);
			this.keyBits = rsaKeyParameters.Modulus.BitLength;
			this.block = new byte[(this.keyBits + 7) / 8];
			if (this.trailer == 188)
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - this.saltLength - 1 - 1];
			}
			else
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - this.saltLength - 1 - 2];
			}
			this.Reset();
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x001222EC File Offset: 0x001204EC
		private bool IsSameAs(byte[] a, byte[] b)
		{
			if (this.messageLength != b.Length)
			{
				return false;
			}
			bool result = true;
			for (int num = 0; num != b.Length; num++)
			{
				if (a[num] != b[num])
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x00122321 File Offset: 0x00120521
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x00122330 File Offset: 0x00120530
		public virtual void UpdateWithRecoveredMessage(byte[] signature)
		{
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			if (array.Length < (this.keyBits + 7) / 8)
			{
				byte[] array2 = new byte[(this.keyBits + 7) / 8];
				Array.Copy(array, 0, array2, array2.Length - array.Length, array.Length);
				this.ClearBlock(array);
				array = array2;
			}
			int num;
			if (((array[array.Length - 1] & 255) ^ 188) == 0)
			{
				num = 1;
			}
			else
			{
				int num2 = (int)(array[array.Length - 2] & byte.MaxValue) << 8 | (int)(array[array.Length - 1] & byte.MaxValue);
				if (IsoTrailers.NoTrailerAvailable(this.digest))
				{
					throw new ArgumentException("unrecognised hash in signature");
				}
				if (num2 != IsoTrailers.GetTrailer(this.digest))
				{
					throw new InvalidOperationException("signer initialised with wrong digest for trailer " + num2);
				}
				num = 2;
			}
			byte[] output = new byte[this.hLen];
			this.digest.DoFinal(output, 0);
			byte[] array3 = this.MaskGeneratorFunction1(array, array.Length - this.hLen - num, this.hLen, array.Length - this.hLen - num);
			for (int num3 = 0; num3 != array3.Length; num3++)
			{
				byte[] array4 = array;
				int num4 = num3;
				array4[num4] ^= array3[num3];
			}
			byte[] array5 = array;
			int num5 = 0;
			array5[num5] &= 127;
			int num6 = 0;
			while (num6 < array.Length && array[num6++] != 1)
			{
			}
			if (num6 >= array.Length)
			{
				this.ClearBlock(array);
			}
			this.fullMessage = (num6 > 1);
			this.recoveredMessage = new byte[array3.Length - num6 - this.saltLength];
			Array.Copy(array, num6, this.recoveredMessage, 0, this.recoveredMessage.Length);
			this.recoveredMessage.CopyTo(this.mBuf, 0);
			this.preSig = signature;
			this.preBlock = array;
			this.preMStart = num6;
			this.preTLength = num;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x00122504 File Offset: 0x00120704
		public virtual void Update(byte input)
		{
			if (this.preSig == null && this.messageLength < this.mBuf.Length)
			{
				byte[] array = this.mBuf;
				int num = this.messageLength;
				this.messageLength = num + 1;
				array[num] = input;
				return;
			}
			this.digest.Update(input);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x00122550 File Offset: 0x00120750
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			if (this.preSig == null)
			{
				while (length > 0 && this.messageLength < this.mBuf.Length)
				{
					this.Update(input[inOff]);
					inOff++;
					length--;
				}
			}
			if (length > 0)
			{
				this.digest.BlockUpdate(input, inOff, length);
			}
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x001225A0 File Offset: 0x001207A0
		public virtual void Reset()
		{
			this.digest.Reset();
			this.messageLength = 0;
			if (this.mBuf != null)
			{
				this.ClearBlock(this.mBuf);
			}
			if (this.recoveredMessage != null)
			{
				this.ClearBlock(this.recoveredMessage);
				this.recoveredMessage = null;
			}
			this.fullMessage = false;
			if (this.preSig != null)
			{
				this.preSig = null;
				this.ClearBlock(this.preBlock);
				this.preBlock = null;
			}
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x00122618 File Offset: 0x00120818
		public virtual byte[] GenerateSignature()
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] array2 = new byte[8];
			this.LtoOSP((long)(this.messageLength * 8), array2);
			this.digest.BlockUpdate(array2, 0, array2.Length);
			this.digest.BlockUpdate(this.mBuf, 0, this.messageLength);
			this.digest.BlockUpdate(array, 0, array.Length);
			byte[] array3;
			if (this.standardSalt != null)
			{
				array3 = this.standardSalt;
			}
			else
			{
				array3 = new byte[this.saltLength];
				this.random.NextBytes(array3);
			}
			this.digest.BlockUpdate(array3, 0, array3.Length);
			byte[] array4 = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array4, 0);
			int num = 2;
			if (this.trailer == 188)
			{
				num = 1;
			}
			int num2 = this.block.Length - this.messageLength - array3.Length - this.hLen - num - 1;
			this.block[num2] = 1;
			Array.Copy(this.mBuf, 0, this.block, num2 + 1, this.messageLength);
			Array.Copy(array3, 0, this.block, num2 + 1 + this.messageLength, array3.Length);
			byte[] array5 = this.MaskGeneratorFunction1(array4, 0, array4.Length, this.block.Length - this.hLen - num);
			for (int num3 = 0; num3 != array5.Length; num3++)
			{
				byte[] array6 = this.block;
				int num4 = num3;
				array6[num4] ^= array5[num3];
			}
			Array.Copy(array4, 0, this.block, this.block.Length - this.hLen - num, this.hLen);
			if (this.trailer == 188)
			{
				this.block[this.block.Length - 1] = 188;
			}
			else
			{
				this.block[this.block.Length - 2] = (byte)((uint)this.trailer >> 8);
				this.block[this.block.Length - 1] = (byte)this.trailer;
			}
			byte[] array7 = this.block;
			int num5 = 0;
			array7[num5] &= 127;
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.ClearBlock(this.mBuf);
			this.ClearBlock(this.block);
			this.messageLength = 0;
			return result;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00122874 File Offset: 0x00120A74
		public virtual bool VerifySignature(byte[] signature)
		{
			byte[] array = new byte[this.hLen];
			this.digest.DoFinal(array, 0);
			if (this.preSig == null)
			{
				try
				{
					this.UpdateWithRecoveredMessage(signature);
					goto IL_4F;
				}
				catch (Exception)
				{
					return false;
				}
			}
			if (!Arrays.AreEqual(this.preSig, signature))
			{
				throw new InvalidOperationException("UpdateWithRecoveredMessage called on different signature");
			}
			IL_4F:
			byte[] array2 = this.preBlock;
			int num = this.preMStart;
			int num2 = this.preTLength;
			this.preSig = null;
			this.preBlock = null;
			byte[] array3 = new byte[8];
			this.LtoOSP((long)(this.recoveredMessage.Length * 8), array3);
			this.digest.BlockUpdate(array3, 0, array3.Length);
			if (this.recoveredMessage.Length != 0)
			{
				this.digest.BlockUpdate(this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			this.digest.BlockUpdate(array, 0, array.Length);
			if (this.standardSalt != null)
			{
				this.digest.BlockUpdate(this.standardSalt, 0, this.standardSalt.Length);
			}
			else
			{
				this.digest.BlockUpdate(array2, num + this.recoveredMessage.Length, this.saltLength);
			}
			byte[] array4 = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array4, 0);
			int num3 = array2.Length - num2 - array4.Length;
			bool flag = true;
			for (int num4 = 0; num4 != array4.Length; num4++)
			{
				if (array4[num4] != array2[num3 + num4])
				{
					flag = false;
				}
			}
			this.ClearBlock(array2);
			this.ClearBlock(array4);
			if (!flag)
			{
				this.fullMessage = false;
				this.messageLength = 0;
				this.ClearBlock(this.recoveredMessage);
				return false;
			}
			if (this.messageLength != 0 && !this.IsSameAs(this.mBuf, this.recoveredMessage))
			{
				this.messageLength = 0;
				this.ClearBlock(this.mBuf);
				return false;
			}
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			return true;
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x00122A70 File Offset: 0x00120C70
		public virtual bool HasFullMessage()
		{
			return this.fullMessage;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x00122A78 File Offset: 0x00120C78
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x00122A96 File Offset: 0x00120C96
		private void LtoOSP(long l, byte[] sp)
		{
			sp[0] = (byte)((ulong)l >> 56);
			sp[1] = (byte)((ulong)l >> 48);
			sp[2] = (byte)((ulong)l >> 40);
			sp[3] = (byte)((ulong)l >> 32);
			sp[4] = (byte)((ulong)l >> 24);
			sp[5] = (byte)((ulong)l >> 16);
			sp[6] = (byte)((ulong)l >> 8);
			sp[7] = (byte)l;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x00122AD4 File Offset: 0x00120CD4
		private byte[] MaskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.hLen];
			byte[] array3 = new byte[4];
			int num = 0;
			this.digest.Reset();
			do
			{
				this.ItoOSP(num, array3);
				this.digest.BlockUpdate(Z, zOff, zLen);
				this.digest.BlockUpdate(array3, 0, array3.Length);
				this.digest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, num * this.hLen, this.hLen);
			}
			while (++num < length / this.hLen);
			if (num * this.hLen < length)
			{
				this.ItoOSP(num, array3);
				this.digest.BlockUpdate(Z, zOff, zLen);
				this.digest.BlockUpdate(array3, 0, array3.Length);
				this.digest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, num * this.hLen, array.Length - num * this.hLen);
			}
			return array;
		}

		// Token: 0x04001DB8 RID: 7608
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerImplicit = 188;

		// Token: 0x04001DB9 RID: 7609
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerRipeMD160 = 12748;

		// Token: 0x04001DBA RID: 7610
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerRipeMD128 = 13004;

		// Token: 0x04001DBB RID: 7611
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha1 = 13260;

		// Token: 0x04001DBC RID: 7612
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha256 = 13516;

		// Token: 0x04001DBD RID: 7613
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha512 = 13772;

		// Token: 0x04001DBE RID: 7614
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha384 = 14028;

		// Token: 0x04001DBF RID: 7615
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerWhirlpool = 14284;

		// Token: 0x04001DC0 RID: 7616
		private IDigest digest;

		// Token: 0x04001DC1 RID: 7617
		private IAsymmetricBlockCipher cipher;

		// Token: 0x04001DC2 RID: 7618
		private SecureRandom random;

		// Token: 0x04001DC3 RID: 7619
		private byte[] standardSalt;

		// Token: 0x04001DC4 RID: 7620
		private int hLen;

		// Token: 0x04001DC5 RID: 7621
		private int trailer;

		// Token: 0x04001DC6 RID: 7622
		private int keyBits;

		// Token: 0x04001DC7 RID: 7623
		private byte[] block;

		// Token: 0x04001DC8 RID: 7624
		private byte[] mBuf;

		// Token: 0x04001DC9 RID: 7625
		private int messageLength;

		// Token: 0x04001DCA RID: 7626
		private readonly int saltLength;

		// Token: 0x04001DCB RID: 7627
		private bool fullMessage;

		// Token: 0x04001DCC RID: 7628
		private byte[] recoveredMessage;

		// Token: 0x04001DCD RID: 7629
		private byte[] preSig;

		// Token: 0x04001DCE RID: 7630
		private byte[] preBlock;

		// Token: 0x04001DCF RID: 7631
		private int preMStart;

		// Token: 0x04001DD0 RID: 7632
		private int preTLength;
	}
}
