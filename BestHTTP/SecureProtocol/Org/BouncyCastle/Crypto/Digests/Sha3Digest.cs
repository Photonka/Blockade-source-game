using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020005A3 RID: 1443
	public class Sha3Digest : KeccakDigest
	{
		// Token: 0x060037C8 RID: 14280 RVA: 0x00162954 File Offset: 0x00160B54
		private static int CheckBitLength(int bitLength)
		{
			if (bitLength <= 256)
			{
				if (bitLength != 224 && bitLength != 256)
				{
					goto IL_2C;
				}
			}
			else if (bitLength != 384 && bitLength != 512)
			{
				goto IL_2C;
			}
			return bitLength;
			IL_2C:
			throw new ArgumentException(bitLength + " not supported for SHA-3", "bitLength");
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x001629A7 File Offset: 0x00160BA7
		public Sha3Digest() : this(256)
		{
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x001629B4 File Offset: 0x00160BB4
		public Sha3Digest(int bitLength) : base(Sha3Digest.CheckBitLength(bitLength))
		{
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x001629C2 File Offset: 0x00160BC2
		public Sha3Digest(Sha3Digest source) : base(source)
		{
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x001629CB File Offset: 0x00160BCB
		public override string AlgorithmName
		{
			get
			{
				return "SHA3-" + this.fixedOutputLength;
			}
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x001629E2 File Offset: 0x00160BE2
		public override int DoFinal(byte[] output, int outOff)
		{
			base.AbsorbBits(2, 2);
			return base.DoFinal(output, outOff);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x001629F4 File Offset: 0x00160BF4
		protected override int DoFinal(byte[] output, int outOff, byte partialByte, int partialBits)
		{
			if (partialBits < 0 || partialBits > 7)
			{
				throw new ArgumentException("must be in the range [0,7]", "partialBits");
			}
			int num = ((int)partialByte & (1 << partialBits) - 1) | 2 << partialBits;
			int num2 = partialBits + 2;
			if (num2 >= 8)
			{
				base.Absorb(new byte[]
				{
					(byte)num
				}, 0, 1);
				num2 -= 8;
				num >>= 8;
			}
			return base.DoFinal(output, outOff, (byte)num, num2);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x00162A5E File Offset: 0x00160C5E
		public override IMemoable Copy()
		{
			return new Sha3Digest(this);
		}
	}
}
