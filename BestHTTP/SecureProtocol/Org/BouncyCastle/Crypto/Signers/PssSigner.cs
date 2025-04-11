using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000490 RID: 1168
	public class PssSigner : ISigner
	{
		// Token: 0x06002E31 RID: 11825 RVA: 0x00123705 File Offset: 0x00121905
		public static PssSigner CreateRawSigner(IAsymmetricBlockCipher cipher, IDigest digest)
		{
			return new PssSigner(cipher, new NullDigest(), digest, digest, digest.GetDigestSize(), null, 188);
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x00123720 File Offset: 0x00121920
		public static PssSigner CreateRawSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, int saltLen, byte trailer)
		{
			return new PssSigner(cipher, new NullDigest(), contentDigest, mgfDigest, saltLen, null, trailer);
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x00123733 File Offset: 0x00121933
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest) : this(cipher, digest, digest.GetDigestSize())
		{
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x00123743 File Offset: 0x00121943
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLen) : this(cipher, digest, saltLen, 188)
		{
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x00123753 File Offset: 0x00121953
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, byte[] salt) : this(cipher, digest, digest, digest, salt.Length, salt, 188)
		{
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x00123768 File Offset: 0x00121968
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, int saltLen) : this(cipher, contentDigest, mgfDigest, saltLen, 188)
		{
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x0012377A File Offset: 0x0012197A
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, byte[] salt) : this(cipher, contentDigest, contentDigest, mgfDigest, salt.Length, salt, 188)
		{
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x00123791 File Offset: 0x00121991
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLen, byte trailer) : this(cipher, digest, digest, saltLen, 188)
		{
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x001237A2 File Offset: 0x001219A2
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, int saltLen, byte trailer) : this(cipher, contentDigest, contentDigest, mgfDigest, saltLen, null, trailer)
		{
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x001237B4 File Offset: 0x001219B4
		private PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest1, IDigest contentDigest2, IDigest mgfDigest, int saltLen, byte[] salt, byte trailer)
		{
			this.cipher = cipher;
			this.contentDigest1 = contentDigest1;
			this.contentDigest2 = contentDigest2;
			this.mgfDigest = mgfDigest;
			this.hLen = contentDigest2.GetDigestSize();
			this.mgfhLen = mgfDigest.GetDigestSize();
			this.sLen = saltLen;
			this.sSet = (salt != null);
			if (this.sSet)
			{
				this.salt = salt;
			}
			else
			{
				this.salt = new byte[saltLen];
			}
			this.mDash = new byte[8 + saltLen + this.hLen];
			this.trailer = trailer;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x0012384D File Offset: 0x00121A4D
		public virtual string AlgorithmName
		{
			get
			{
				return this.mgfDigest.AlgorithmName + "withRSAandMGF1";
			}
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x00123864 File Offset: 0x00121A64
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				parameters = parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
			}
			else if (forSigning)
			{
				this.random = new SecureRandom();
			}
			this.cipher.Init(forSigning, parameters);
			RsaKeyParameters rsaKeyParameters;
			if (parameters is RsaBlindingParameters)
			{
				rsaKeyParameters = ((RsaBlindingParameters)parameters).PublicKey;
			}
			else
			{
				rsaKeyParameters = (RsaKeyParameters)parameters;
			}
			this.emBits = rsaKeyParameters.Modulus.BitLength - 1;
			if (this.emBits < 8 * this.hLen + 8 * this.sLen + 9)
			{
				throw new ArgumentException("key too small for specified hash and salt lengths");
			}
			this.block = new byte[(this.emBits + 7) / 8];
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x00122321 File Offset: 0x00120521
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x0012391D File Offset: 0x00121B1D
		public virtual void Update(byte input)
		{
			this.contentDigest1.Update(input);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x0012392B File Offset: 0x00121B2B
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.contentDigest1.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x0012393B File Offset: 0x00121B3B
		public virtual void Reset()
		{
			this.contentDigest1.Reset();
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x00123948 File Offset: 0x00121B48
		public virtual byte[] GenerateSignature()
		{
			this.contentDigest1.DoFinal(this.mDash, this.mDash.Length - this.hLen - this.sLen);
			if (this.sLen != 0)
			{
				if (!this.sSet)
				{
					this.random.NextBytes(this.salt);
				}
				this.salt.CopyTo(this.mDash, this.mDash.Length - this.sLen);
			}
			byte[] array = new byte[this.hLen];
			this.contentDigest2.BlockUpdate(this.mDash, 0, this.mDash.Length);
			this.contentDigest2.DoFinal(array, 0);
			this.block[this.block.Length - this.sLen - 1 - this.hLen - 1] = 1;
			this.salt.CopyTo(this.block, this.block.Length - this.sLen - this.hLen - 1);
			byte[] array2 = this.MaskGeneratorFunction1(array, 0, array.Length, this.block.Length - this.hLen - 1);
			for (int num = 0; num != array2.Length; num++)
			{
				byte[] array3 = this.block;
				int num2 = num;
				array3[num2] ^= array2[num];
			}
			byte[] array4 = this.block;
			int num3 = 0;
			array4[num3] &= (byte)(255 >> this.block.Length * 8 - this.emBits);
			array.CopyTo(this.block, this.block.Length - this.hLen - 1);
			this.block[this.block.Length - 1] = this.trailer;
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.ClearBlock(this.block);
			return result;
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x00123B04 File Offset: 0x00121D04
		public virtual bool VerifySignature(byte[] signature)
		{
			this.contentDigest1.DoFinal(this.mDash, this.mDash.Length - this.hLen - this.sLen);
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			array.CopyTo(this.block, this.block.Length - array.Length);
			if (this.block[this.block.Length - 1] != this.trailer)
			{
				this.ClearBlock(this.block);
				return false;
			}
			byte[] array2 = this.MaskGeneratorFunction1(this.block, this.block.Length - this.hLen - 1, this.hLen, this.block.Length - this.hLen - 1);
			for (int num = 0; num != array2.Length; num++)
			{
				byte[] array3 = this.block;
				int num2 = num;
				array3[num2] ^= array2[num];
			}
			byte[] array4 = this.block;
			int num3 = 0;
			array4[num3] &= (byte)(255 >> this.block.Length * 8 - this.emBits);
			for (int num4 = 0; num4 != this.block.Length - this.hLen - this.sLen - 2; num4++)
			{
				if (this.block[num4] != 0)
				{
					this.ClearBlock(this.block);
					return false;
				}
			}
			if (this.block[this.block.Length - this.hLen - this.sLen - 2] != 1)
			{
				this.ClearBlock(this.block);
				return false;
			}
			if (this.sSet)
			{
				Array.Copy(this.salt, 0, this.mDash, this.mDash.Length - this.sLen, this.sLen);
			}
			else
			{
				Array.Copy(this.block, this.block.Length - this.sLen - this.hLen - 1, this.mDash, this.mDash.Length - this.sLen, this.sLen);
			}
			this.contentDigest2.BlockUpdate(this.mDash, 0, this.mDash.Length);
			this.contentDigest2.DoFinal(this.mDash, this.mDash.Length - this.hLen);
			int num5 = this.block.Length - this.hLen - 1;
			for (int num6 = this.mDash.Length - this.hLen; num6 != this.mDash.Length; num6++)
			{
				if ((this.block[num5] ^ this.mDash[num6]) != 0)
				{
					this.ClearBlock(this.mDash);
					this.ClearBlock(this.block);
					return false;
				}
				num5++;
			}
			this.ClearBlock(this.mDash);
			this.ClearBlock(this.block);
			return true;
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x00122A78 File Offset: 0x00120C78
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x00123DA8 File Offset: 0x00121FA8
		private byte[] MaskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.mgfhLen];
			byte[] array3 = new byte[4];
			int i = 0;
			this.mgfDigest.Reset();
			while (i < length / this.mgfhLen)
			{
				this.ItoOSP(i, array3);
				this.mgfDigest.BlockUpdate(Z, zOff, zLen);
				this.mgfDigest.BlockUpdate(array3, 0, array3.Length);
				this.mgfDigest.DoFinal(array2, 0);
				array2.CopyTo(array, i * this.mgfhLen);
				i++;
			}
			if (i * this.mgfhLen < length)
			{
				this.ItoOSP(i, array3);
				this.mgfDigest.BlockUpdate(Z, zOff, zLen);
				this.mgfDigest.BlockUpdate(array3, 0, array3.Length);
				this.mgfDigest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, i * this.mgfhLen, array.Length - i * this.mgfhLen);
			}
			return array;
		}

		// Token: 0x04001DF1 RID: 7665
		public const byte TrailerImplicit = 188;

		// Token: 0x04001DF2 RID: 7666
		private readonly IDigest contentDigest1;

		// Token: 0x04001DF3 RID: 7667
		private readonly IDigest contentDigest2;

		// Token: 0x04001DF4 RID: 7668
		private readonly IDigest mgfDigest;

		// Token: 0x04001DF5 RID: 7669
		private readonly IAsymmetricBlockCipher cipher;

		// Token: 0x04001DF6 RID: 7670
		private SecureRandom random;

		// Token: 0x04001DF7 RID: 7671
		private int hLen;

		// Token: 0x04001DF8 RID: 7672
		private int mgfhLen;

		// Token: 0x04001DF9 RID: 7673
		private int sLen;

		// Token: 0x04001DFA RID: 7674
		private bool sSet;

		// Token: 0x04001DFB RID: 7675
		private int emBits;

		// Token: 0x04001DFC RID: 7676
		private byte[] salt;

		// Token: 0x04001DFD RID: 7677
		private byte[] mDash;

		// Token: 0x04001DFE RID: 7678
		private byte[] block;

		// Token: 0x04001DFF RID: 7679
		private byte trailer;
	}
}
