using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000495 RID: 1173
	public class X931Signer : ISigner
	{
		// Token: 0x06002E6F RID: 11887 RVA: 0x001248FC File Offset: 0x00122AFC
		public X931Signer(IAsymmetricBlockCipher cipher, IDigest digest, bool isImplicit)
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

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x00124950 File Offset: 0x00122B50
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "with" + this.cipher.AlgorithmName + "/X9.31";
			}
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x00124977 File Offset: 0x00122B77
		public X931Signer(IAsymmetricBlockCipher cipher, IDigest digest) : this(cipher, digest, false)
		{
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x00124984 File Offset: 0x00122B84
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.kParam = (RsaKeyParameters)parameters;
			this.cipher.Init(forSigning, this.kParam);
			this.keyBits = this.kParam.Modulus.BitLength;
			this.block = new byte[(this.keyBits + 7) / 8];
			this.Reset();
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x00122321 File Offset: 0x00120521
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x001249E0 File Offset: 0x00122BE0
		public virtual void Update(byte b)
		{
			this.digest.Update(b);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x001249EE File Offset: 0x00122BEE
		public virtual void BlockUpdate(byte[] input, int off, int len)
		{
			this.digest.BlockUpdate(input, off, len);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x001249FE File Offset: 0x00122BFE
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x00124A0C File Offset: 0x00122C0C
		public virtual byte[] GenerateSignature()
		{
			this.CreateSignatureBlock();
			BigInteger bigInteger = new BigInteger(1, this.cipher.ProcessBlock(this.block, 0, this.block.Length));
			this.ClearBlock(this.block);
			bigInteger = bigInteger.Min(this.kParam.Modulus.Subtract(bigInteger));
			return BigIntegers.AsUnsignedByteArray(BigIntegers.GetUnsignedByteLength(this.kParam.Modulus), bigInteger);
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x00124A7C File Offset: 0x00122C7C
		private void CreateSignatureBlock()
		{
			int digestSize = this.digest.GetDigestSize();
			int num;
			if (this.trailer == 188)
			{
				num = this.block.Length - digestSize - 1;
				this.digest.DoFinal(this.block, num);
				this.block[this.block.Length - 1] = 188;
			}
			else
			{
				num = this.block.Length - digestSize - 2;
				this.digest.DoFinal(this.block, num);
				this.block[this.block.Length - 2] = (byte)(this.trailer >> 8);
				this.block[this.block.Length - 1] = (byte)this.trailer;
			}
			this.block[0] = 107;
			for (int num2 = num - 2; num2 != 0; num2--)
			{
				this.block[num2] = 187;
			}
			this.block[num - 1] = 186;
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x00124B60 File Offset: 0x00122D60
		public virtual bool VerifySignature(byte[] signature)
		{
			try
			{
				this.block = this.cipher.ProcessBlock(signature, 0, signature.Length);
			}
			catch (Exception)
			{
				return false;
			}
			BigInteger bigInteger = new BigInteger(1, this.block);
			BigInteger n;
			if ((bigInteger.IntValue & 15) == 12)
			{
				n = bigInteger;
			}
			else
			{
				bigInteger = this.kParam.Modulus.Subtract(bigInteger);
				if ((bigInteger.IntValue & 15) != 12)
				{
					return false;
				}
				n = bigInteger;
			}
			this.CreateSignatureBlock();
			byte[] b = BigIntegers.AsUnsignedByteArray(this.block.Length, n);
			bool result = Arrays.ConstantTimeAreEqual(this.block, b);
			this.ClearBlock(this.block);
			this.ClearBlock(b);
			return result;
		}

		// Token: 0x04001E0F RID: 7695
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_IMPLICIT = 188;

		// Token: 0x04001E10 RID: 7696
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_RIPEMD160 = 12748;

		// Token: 0x04001E11 RID: 7697
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_RIPEMD128 = 13004;

		// Token: 0x04001E12 RID: 7698
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA1 = 13260;

		// Token: 0x04001E13 RID: 7699
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA256 = 13516;

		// Token: 0x04001E14 RID: 7700
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA512 = 13772;

		// Token: 0x04001E15 RID: 7701
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA384 = 14028;

		// Token: 0x04001E16 RID: 7702
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_WHIRLPOOL = 14284;

		// Token: 0x04001E17 RID: 7703
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA224 = 14540;

		// Token: 0x04001E18 RID: 7704
		private IDigest digest;

		// Token: 0x04001E19 RID: 7705
		private IAsymmetricBlockCipher cipher;

		// Token: 0x04001E1A RID: 7706
		private RsaKeyParameters kParam;

		// Token: 0x04001E1B RID: 7707
		private int trailer;

		// Token: 0x04001E1C RID: 7708
		private int keyBits;

		// Token: 0x04001E1D RID: 7709
		private byte[] block;
	}
}
