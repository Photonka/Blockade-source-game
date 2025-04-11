using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200048D RID: 1165
	public class Iso9796d2Signer : ISignerWithRecovery, ISigner
	{
		// Token: 0x06002E16 RID: 11798 RVA: 0x00122BC0 File Offset: 0x00120DC0
		public byte[] GetRecoveredMessage()
		{
			return this.recoveredMessage;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x00122BC8 File Offset: 0x00120DC8
		public Iso9796d2Signer(IAsymmetricBlockCipher cipher, IDigest digest, bool isImplicit)
		{
			this.cipher = cipher;
			this.digest = digest;
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

		// Token: 0x06002E18 RID: 11800 RVA: 0x00122C1C File Offset: 0x00120E1C
		public Iso9796d2Signer(IAsymmetricBlockCipher cipher, IDigest digest) : this(cipher, digest, false)
		{
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x00122C27 File Offset: 0x00120E27
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withISO9796-2S1";
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x00122C40 File Offset: 0x00120E40
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)parameters;
			this.cipher.Init(forSigning, rsaKeyParameters);
			this.keyBits = rsaKeyParameters.Modulus.BitLength;
			this.block = new byte[(this.keyBits + 7) / 8];
			if (this.trailer == 188)
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - 2];
			}
			else
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - 3];
			}
			this.Reset();
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x00122CE0 File Offset: 0x00120EE0
		private bool IsSameAs(byte[] a, byte[] b)
		{
			int num;
			if (this.messageLength > this.mBuf.Length)
			{
				if (this.mBuf.Length > b.Length)
				{
					return false;
				}
				num = this.mBuf.Length;
			}
			else
			{
				if (this.messageLength != b.Length)
				{
					return false;
				}
				num = b.Length;
			}
			bool result = true;
			for (int num2 = 0; num2 != num; num2++)
			{
				if (a[num2] != b[num2])
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x00122321 File Offset: 0x00120521
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x00122D44 File Offset: 0x00120F44
		public virtual void UpdateWithRecoveredMessage(byte[] signature)
		{
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			if (((array[0] & 192) ^ 64) != 0)
			{
				throw new InvalidCipherTextException("malformed signature");
			}
			if (((array[array.Length - 1] & 15) ^ 12) != 0)
			{
				throw new InvalidCipherTextException("malformed signature");
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
			int num3 = 0;
			while (num3 != array.Length && ((array[num3] & 15) ^ 10) != 0)
			{
				num3++;
			}
			num3++;
			int num4 = array.Length - num - this.digest.GetDigestSize();
			if (num4 - num3 <= 0)
			{
				throw new InvalidCipherTextException("malformed block");
			}
			if ((array[0] & 32) == 0)
			{
				this.fullMessage = true;
				this.recoveredMessage = new byte[num4 - num3];
				Array.Copy(array, num3, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			else
			{
				this.fullMessage = false;
				this.recoveredMessage = new byte[num4 - num3];
				Array.Copy(array, num3, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			this.preSig = signature;
			this.preBlock = array;
			this.digest.BlockUpdate(this.recoveredMessage, 0, this.recoveredMessage.Length);
			this.messageLength = this.recoveredMessage.Length;
			this.recoveredMessage.CopyTo(this.mBuf, 0);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x00122EF9 File Offset: 0x001210F9
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
			if (this.messageLength < this.mBuf.Length)
			{
				this.mBuf[this.messageLength] = input;
			}
			this.messageLength++;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x00122F34 File Offset: 0x00121134
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (length > 0 && this.messageLength < this.mBuf.Length)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			this.digest.BlockUpdate(input, inOff, length);
			this.messageLength += length;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x00122F88 File Offset: 0x00121188
		public virtual void Reset()
		{
			this.digest.Reset();
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			if (this.recoveredMessage != null)
			{
				this.ClearBlock(this.recoveredMessage);
			}
			this.recoveredMessage = null;
			this.fullMessage = false;
			if (this.preSig != null)
			{
				this.preSig = null;
				this.ClearBlock(this.preBlock);
				this.preBlock = null;
			}
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x00122FF8 File Offset: 0x001211F8
		public virtual byte[] GenerateSignature()
		{
			int digestSize = this.digest.GetDigestSize();
			int num;
			int num2;
			if (this.trailer == 188)
			{
				num = 8;
				num2 = this.block.Length - digestSize - 1;
				this.digest.DoFinal(this.block, num2);
				this.block[this.block.Length - 1] = 188;
			}
			else
			{
				num = 16;
				num2 = this.block.Length - digestSize - 2;
				this.digest.DoFinal(this.block, num2);
				this.block[this.block.Length - 2] = (byte)((uint)this.trailer >> 8);
				this.block[this.block.Length - 1] = (byte)this.trailer;
			}
			int num3 = (digestSize + this.messageLength) * 8 + num + 4 - this.keyBits;
			byte b;
			if (num3 > 0)
			{
				int num4 = this.messageLength - (num3 + 7) / 8;
				b = 96;
				num2 -= num4;
				Array.Copy(this.mBuf, 0, this.block, num2, num4);
			}
			else
			{
				b = 64;
				num2 -= this.messageLength;
				Array.Copy(this.mBuf, 0, this.block, num2, this.messageLength);
			}
			if (num2 - 1 > 0)
			{
				for (int num5 = num2 - 1; num5 != 0; num5--)
				{
					this.block[num5] = 187;
				}
				byte[] array = this.block;
				int num6 = num2 - 1;
				array[num6] ^= 1;
				this.block[0] = 11;
				byte[] array2 = this.block;
				int num7 = 0;
				array2[num7] |= b;
			}
			else
			{
				this.block[0] = 10;
				byte[] array3 = this.block;
				int num8 = 0;
				array3[num8] |= b;
			}
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			this.ClearBlock(this.block);
			return result;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x001231D0 File Offset: 0x001213D0
		public virtual bool VerifySignature(byte[] signature)
		{
			byte[] array;
			if (this.preSig == null)
			{
				try
				{
					array = this.cipher.ProcessBlock(signature, 0, signature.Length);
					goto IL_52;
				}
				catch (Exception)
				{
					return false;
				}
			}
			if (!Arrays.AreEqual(this.preSig, signature))
			{
				throw new InvalidOperationException("updateWithRecoveredMessage called on different signature");
			}
			array = this.preBlock;
			this.preSig = null;
			this.preBlock = null;
			IL_52:
			if (((array[0] & 192) ^ 64) != 0)
			{
				return this.ReturnFalse(array);
			}
			if (((array[array.Length - 1] & 15) ^ 12) != 0)
			{
				return this.ReturnFalse(array);
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
			int num3 = 0;
			while (num3 != array.Length && ((array[num3] & 15) ^ 10) != 0)
			{
				num3++;
			}
			num3++;
			byte[] array2 = new byte[this.digest.GetDigestSize()];
			int num4 = array.Length - num - array2.Length;
			if (num4 - num3 <= 0)
			{
				return this.ReturnFalse(array);
			}
			if ((array[0] & 32) == 0)
			{
				this.fullMessage = true;
				if (this.messageLength > num4 - num3)
				{
					return this.ReturnFalse(array);
				}
				this.digest.Reset();
				this.digest.BlockUpdate(array, num3, num4 - num3);
				this.digest.DoFinal(array2, 0);
				bool flag = true;
				for (int num5 = 0; num5 != array2.Length; num5++)
				{
					byte[] array3 = array;
					int num6 = num4 + num5;
					array3[num6] ^= array2[num5];
					if (array[num4 + num5] != 0)
					{
						flag = false;
					}
				}
				if (!flag)
				{
					return this.ReturnFalse(array);
				}
				this.recoveredMessage = new byte[num4 - num3];
				Array.Copy(array, num3, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			else
			{
				this.fullMessage = false;
				this.digest.DoFinal(array2, 0);
				bool flag2 = true;
				for (int num7 = 0; num7 != array2.Length; num7++)
				{
					byte[] array4 = array;
					int num8 = num4 + num7;
					array4[num8] ^= array2[num7];
					if (array[num4 + num7] != 0)
					{
						flag2 = false;
					}
				}
				if (!flag2)
				{
					return this.ReturnFalse(array);
				}
				this.recoveredMessage = new byte[num4 - num3];
				Array.Copy(array, num3, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			if (this.messageLength != 0 && !this.IsSameAs(this.mBuf, this.recoveredMessage))
			{
				return this.ReturnFalse(array);
			}
			this.ClearBlock(this.mBuf);
			this.ClearBlock(array);
			this.messageLength = 0;
			return true;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x001234A4 File Offset: 0x001216A4
		private bool ReturnFalse(byte[] block)
		{
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			this.ClearBlock(block);
			return false;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x001234C1 File Offset: 0x001216C1
		public virtual bool HasFullMessage()
		{
			return this.fullMessage;
		}

		// Token: 0x04001DD1 RID: 7633
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerImplicit = 188;

		// Token: 0x04001DD2 RID: 7634
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerRipeMD160 = 12748;

		// Token: 0x04001DD3 RID: 7635
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerRipeMD128 = 13004;

		// Token: 0x04001DD4 RID: 7636
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha1 = 13260;

		// Token: 0x04001DD5 RID: 7637
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha256 = 13516;

		// Token: 0x04001DD6 RID: 7638
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha512 = 13772;

		// Token: 0x04001DD7 RID: 7639
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha384 = 14028;

		// Token: 0x04001DD8 RID: 7640
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerWhirlpool = 14284;

		// Token: 0x04001DD9 RID: 7641
		private IDigest digest;

		// Token: 0x04001DDA RID: 7642
		private IAsymmetricBlockCipher cipher;

		// Token: 0x04001DDB RID: 7643
		private int trailer;

		// Token: 0x04001DDC RID: 7644
		private int keyBits;

		// Token: 0x04001DDD RID: 7645
		private byte[] block;

		// Token: 0x04001DDE RID: 7646
		private byte[] mBuf;

		// Token: 0x04001DDF RID: 7647
		private int messageLength;

		// Token: 0x04001DE0 RID: 7648
		private bool fullMessage;

		// Token: 0x04001DE1 RID: 7649
		private byte[] recoveredMessage;

		// Token: 0x04001DE2 RID: 7650
		private byte[] preSig;

		// Token: 0x04001DE3 RID: 7651
		private byte[] preBlock;
	}
}
