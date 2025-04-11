using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000521 RID: 1313
	public class HMac : IMac
	{
		// Token: 0x0600321C RID: 12828 RVA: 0x00131E34 File Offset: 0x00130034
		public HMac(IDigest digest)
		{
			this.digest = digest;
			this.digestSize = digest.GetDigestSize();
			this.blockLength = digest.GetByteLength();
			this.inputPad = new byte[this.blockLength];
			this.outputBuf = new byte[this.blockLength + this.digestSize];
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x00131E8F File Offset: 0x0013008F
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "/HMAC";
			}
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x00131EA6 File Offset: 0x001300A6
		public virtual IDigest GetUnderlyingDigest()
		{
			return this.digest;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x00131EB0 File Offset: 0x001300B0
		public virtual void Init(ICipherParameters parameters)
		{
			this.digest.Reset();
			byte[] key = ((KeyParameter)parameters).GetKey();
			int num = key.Length;
			if (num > this.blockLength)
			{
				this.digest.BlockUpdate(key, 0, num);
				this.digest.DoFinal(this.inputPad, 0);
				num = this.digestSize;
			}
			else
			{
				Array.Copy(key, 0, this.inputPad, 0, num);
			}
			Array.Clear(this.inputPad, num, this.blockLength - num);
			Array.Copy(this.inputPad, 0, this.outputBuf, 0, this.blockLength);
			HMac.XorPad(this.inputPad, this.blockLength, 54);
			HMac.XorPad(this.outputBuf, this.blockLength, 92);
			if (this.digest is IMemoable)
			{
				this.opadState = ((IMemoable)this.digest).Copy();
				((IDigest)this.opadState).BlockUpdate(this.outputBuf, 0, this.blockLength);
			}
			this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
			if (this.digest is IMemoable)
			{
				this.ipadState = ((IMemoable)this.digest).Copy();
			}
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x00131FEA File Offset: 0x001301EA
		public virtual int GetMacSize()
		{
			return this.digestSize;
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x00131FF2 File Offset: 0x001301F2
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x00132000 File Offset: 0x00130200
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.digest.BlockUpdate(input, inOff, len);
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x00132010 File Offset: 0x00130210
		public virtual int DoFinal(byte[] output, int outOff)
		{
			this.digest.DoFinal(this.outputBuf, this.blockLength);
			if (this.opadState != null)
			{
				((IMemoable)this.digest).Reset(this.opadState);
				this.digest.BlockUpdate(this.outputBuf, this.blockLength, this.digest.GetDigestSize());
			}
			else
			{
				this.digest.BlockUpdate(this.outputBuf, 0, this.outputBuf.Length);
			}
			int result = this.digest.DoFinal(output, outOff);
			Array.Clear(this.outputBuf, this.blockLength, this.digestSize);
			if (this.ipadState != null)
			{
				((IMemoable)this.digest).Reset(this.ipadState);
				return result;
			}
			this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
			return result;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x001320EE File Offset: 0x001302EE
		public virtual void Reset()
		{
			this.digest.Reset();
			this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x00132118 File Offset: 0x00130318
		private static void XorPad(byte[] pad, int len, byte n)
		{
			for (int i = 0; i < len; i++)
			{
				int num = i;
				pad[num] ^= n;
			}
		}

		// Token: 0x04001FEA RID: 8170
		private const byte IPAD = 54;

		// Token: 0x04001FEB RID: 8171
		private const byte OPAD = 92;

		// Token: 0x04001FEC RID: 8172
		private readonly IDigest digest;

		// Token: 0x04001FED RID: 8173
		private readonly int digestSize;

		// Token: 0x04001FEE RID: 8174
		private readonly int blockLength;

		// Token: 0x04001FEF RID: 8175
		private IMemoable ipadState;

		// Token: 0x04001FF0 RID: 8176
		private IMemoable opadState;

		// Token: 0x04001FF1 RID: 8177
		private readonly byte[] inputPad;

		// Token: 0x04001FF2 RID: 8178
		private readonly byte[] outputBuf;
	}
}
