using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200051D RID: 1309
	public class Dstu7564Mac : IMac
	{
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x00131112 File Offset: 0x0012F312
		public string AlgorithmName
		{
			get
			{
				return "DSTU7564Mac";
			}
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00131119 File Offset: 0x0012F319
		public Dstu7564Mac(int macSizeBits)
		{
			this.engine = new Dstu7564Digest(macSizeBits);
			this.macSize = macSizeBits / 8;
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00131138 File Offset: 0x0012F338
		public void Init(ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				byte[] key = ((KeyParameter)parameters).GetKey();
				this.invertedKey = new byte[key.Length];
				this.paddedKey = this.PadKey(key);
				for (int i = 0; i < this.invertedKey.Length; i++)
				{
					this.invertedKey[i] = (key[i] ^ byte.MaxValue);
				}
				this.engine.BlockUpdate(this.paddedKey, 0, this.paddedKey.Length);
				return;
			}
			throw new ArgumentException("Bad parameter passed");
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x001311C0 File Offset: 0x0012F3C0
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x001311C8 File Offset: 0x0012F3C8
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			Check.DataLength(input, inOff, len, "Input buffer too short");
			if (this.paddedKey == null)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			this.engine.BlockUpdate(input, inOff, len);
			this.inputLength += (ulong)((long)len);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x0013121D File Offset: 0x0012F41D
		public void Update(byte input)
		{
			this.engine.Update(input);
			this.inputLength += 1UL;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x0013123C File Offset: 0x0012F43C
		public int DoFinal(byte[] output, int outOff)
		{
			Check.OutputLength(output, outOff, this.macSize, "Output buffer too short");
			if (this.paddedKey == null)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			this.Pad();
			this.engine.BlockUpdate(this.invertedKey, 0, this.invertedKey.Length);
			this.inputLength = 0UL;
			return this.engine.DoFinal(output, outOff);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x001312AE File Offset: 0x0012F4AE
		public void Reset()
		{
			this.inputLength = 0UL;
			this.engine.Reset();
			if (this.paddedKey != null)
			{
				this.engine.BlockUpdate(this.paddedKey, 0, this.paddedKey.Length);
			}
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x001312E8 File Offset: 0x0012F4E8
		private void Pad()
		{
			int num = this.engine.GetByteLength() - (int)(this.inputLength % (ulong)((long)this.engine.GetByteLength()));
			if (num < 13)
			{
				num += this.engine.GetByteLength();
			}
			byte[] array = new byte[num];
			array[0] = 128;
			Pack.UInt64_To_LE(this.inputLength * 8UL, array, array.Length - 12);
			this.engine.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00131360 File Offset: 0x0012F560
		private byte[] PadKey(byte[] input)
		{
			int num = (input.Length + this.engine.GetByteLength() - 1) / this.engine.GetByteLength() * this.engine.GetByteLength();
			if (this.engine.GetByteLength() - input.Length % this.engine.GetByteLength() < 13)
			{
				num += this.engine.GetByteLength();
			}
			byte[] array = new byte[num];
			Array.Copy(input, 0, array, 0, input.Length);
			array[input.Length] = 128;
			Pack.UInt32_To_LE((uint)(input.Length * 8), array, array.Length - 12);
			return array;
		}

		// Token: 0x04001FD2 RID: 8146
		private Dstu7564Digest engine;

		// Token: 0x04001FD3 RID: 8147
		private int macSize;

		// Token: 0x04001FD4 RID: 8148
		private ulong inputLength;

		// Token: 0x04001FD5 RID: 8149
		private byte[] paddedKey;

		// Token: 0x04001FD6 RID: 8150
		private byte[] invertedKey;
	}
}
