using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020005A6 RID: 1446
	public class ShakeDigest : KeccakDigest, IXof, IDigest
	{
		// Token: 0x060037E3 RID: 14307 RVA: 0x0016304C File Offset: 0x0016124C
		private static int CheckBitLength(int bitLength)
		{
			if (bitLength == 128 || bitLength == 256)
			{
				return bitLength;
			}
			throw new ArgumentException(bitLength + " not supported for SHAKE", "bitLength");
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0016307A File Offset: 0x0016127A
		public ShakeDigest() : this(128)
		{
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x00163087 File Offset: 0x00161287
		public ShakeDigest(int bitLength) : base(ShakeDigest.CheckBitLength(bitLength))
		{
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x001629C2 File Offset: 0x00160BC2
		public ShakeDigest(ShakeDigest source) : base(source)
		{
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x00163095 File Offset: 0x00161295
		public override string AlgorithmName
		{
			get
			{
				return "SHAKE" + this.fixedOutputLength;
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x001630AC File Offset: 0x001612AC
		public override int DoFinal(byte[] output, int outOff)
		{
			return this.DoFinal(output, outOff, this.GetDigestSize());
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x001630BC File Offset: 0x001612BC
		public virtual int DoFinal(byte[] output, int outOff, int outLen)
		{
			this.DoOutput(output, outOff, outLen);
			this.Reset();
			return outLen;
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x001630CF File Offset: 0x001612CF
		public virtual int DoOutput(byte[] output, int outOff, int outLen)
		{
			if (!this.squeezing)
			{
				base.AbsorbBits(15, 4);
			}
			base.Squeeze(output, outOff, (long)outLen << 3);
			return outLen;
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x001630EF File Offset: 0x001612EF
		protected override int DoFinal(byte[] output, int outOff, byte partialByte, int partialBits)
		{
			return this.DoFinal(output, outOff, this.GetDigestSize(), partialByte, partialBits);
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x00163104 File Offset: 0x00161304
		protected virtual int DoFinal(byte[] output, int outOff, int outLen, byte partialByte, int partialBits)
		{
			if (partialBits < 0 || partialBits > 7)
			{
				throw new ArgumentException("must be in the range [0,7]", "partialBits");
			}
			int num = ((int)partialByte & (1 << partialBits) - 1) | 15 << partialBits;
			int num2 = partialBits + 4;
			if (num2 >= 8)
			{
				base.Absorb(new byte[]
				{
					(byte)num
				}, 0, 1);
				num2 -= 8;
				num >>= 8;
			}
			if (num2 > 0)
			{
				base.AbsorbBits(num, num2);
			}
			base.Squeeze(output, outOff, (long)outLen << 3);
			this.Reset();
			return outLen;
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x00163184 File Offset: 0x00161384
		public override IMemoable Copy()
		{
			return new ShakeDigest(this);
		}
	}
}
