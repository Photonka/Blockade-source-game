using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000523 RID: 1315
	public class Poly1305 : IMac
	{
		// Token: 0x06003231 RID: 12849 RVA: 0x00132537 File Offset: 0x00130737
		public Poly1305()
		{
			this.cipher = null;
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x0013255F File Offset: 0x0013075F
		public Poly1305(IBlockCipher cipher)
		{
			if (cipher.GetBlockSize() != 16)
			{
				throw new ArgumentException("Poly1305 requires a 128 bit block cipher.");
			}
			this.cipher = cipher;
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x0013259C File Offset: 0x0013079C
		public void Init(ICipherParameters parameters)
		{
			byte[] nonce = null;
			if (this.cipher != null)
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("Poly1305 requires an IV when used with a block cipher.", "parameters");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				nonce = parametersWithIV.GetIV();
				parameters = parametersWithIV.Parameters;
			}
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("Poly1305 requires a key.");
			}
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.SetKey(keyParameter.GetKey(), nonce);
			this.Reset();
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x0013260C File Offset: 0x0013080C
		private void SetKey(byte[] key, byte[] nonce)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException("Poly1305 key must be 256 bits.");
			}
			if (this.cipher != null && (nonce == null || nonce.Length != 16))
			{
				throw new ArgumentException("Poly1305 requires a 128 bit IV.");
			}
			uint num = Pack.LE_To_UInt32(key, 0);
			uint num2 = Pack.LE_To_UInt32(key, 4);
			uint num3 = Pack.LE_To_UInt32(key, 8);
			uint num4 = Pack.LE_To_UInt32(key, 12);
			this.r0 = (num & 67108863U);
			this.r1 = ((num >> 26 | num2 << 6) & 67108611U);
			this.r2 = ((num2 >> 20 | num3 << 12) & 67092735U);
			this.r3 = ((num3 >> 14 | num4 << 18) & 66076671U);
			this.r4 = (num4 >> 8 & 1048575U);
			this.s1 = this.r1 * 5U;
			this.s2 = this.r2 * 5U;
			this.s3 = this.r3 * 5U;
			this.s4 = this.r4 * 5U;
			byte[] array;
			int num5;
			if (this.cipher == null)
			{
				array = key;
				num5 = 16;
			}
			else
			{
				array = new byte[16];
				num5 = 0;
				this.cipher.Init(true, new KeyParameter(key, 16, 16));
				this.cipher.ProcessBlock(nonce, 0, array, 0);
			}
			this.k0 = Pack.LE_To_UInt32(array, num5);
			this.k1 = Pack.LE_To_UInt32(array, num5 + 4);
			this.k2 = Pack.LE_To_UInt32(array, num5 + 8);
			this.k3 = Pack.LE_To_UInt32(array, num5 + 12);
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x00132782 File Offset: 0x00130982
		public string AlgorithmName
		{
			get
			{
				if (this.cipher != null)
				{
					return "Poly1305-" + this.cipher.AlgorithmName;
				}
				return "Poly1305";
			}
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x0012C4C1 File Offset: 0x0012A6C1
		public int GetMacSize()
		{
			return 16;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x001327A7 File Offset: 0x001309A7
		public void Update(byte input)
		{
			this.singleByte[0] = input;
			this.BlockUpdate(this.singleByte, 0, 1);
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x001327C0 File Offset: 0x001309C0
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			int num = 0;
			while (len > num)
			{
				if (this.currentBlockOffset == 16)
				{
					this.ProcessBlock();
					this.currentBlockOffset = 0;
				}
				int num2 = Math.Min(len - num, 16 - this.currentBlockOffset);
				Array.Copy(input, num + inOff, this.currentBlock, this.currentBlockOffset, num2);
				num += num2;
				this.currentBlockOffset += num2;
			}
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x00132828 File Offset: 0x00130A28
		private void ProcessBlock()
		{
			if (this.currentBlockOffset < 16)
			{
				this.currentBlock[this.currentBlockOffset] = 1;
				for (int i = this.currentBlockOffset + 1; i < 16; i++)
				{
					this.currentBlock[i] = 0;
				}
			}
			ulong num = (ulong)Pack.LE_To_UInt32(this.currentBlock, 0);
			ulong num2 = (ulong)Pack.LE_To_UInt32(this.currentBlock, 4);
			ulong num3 = (ulong)Pack.LE_To_UInt32(this.currentBlock, 8);
			ulong num4 = (ulong)Pack.LE_To_UInt32(this.currentBlock, 12);
			this.h0 += (uint)(num & 67108863UL);
			this.h1 += (uint)((num2 << 32 | num) >> 26 & 67108863UL);
			this.h2 += (uint)((num3 << 32 | num2) >> 20 & 67108863UL);
			this.h3 += (uint)((num4 << 32 | num3) >> 14 & 67108863UL);
			this.h4 += (uint)(num4 >> 8);
			if (this.currentBlockOffset == 16)
			{
				this.h4 += 16777216U;
			}
			ulong num5 = Poly1305.mul32x32_64(this.h0, this.r0) + Poly1305.mul32x32_64(this.h1, this.s4) + Poly1305.mul32x32_64(this.h2, this.s3) + Poly1305.mul32x32_64(this.h3, this.s2) + Poly1305.mul32x32_64(this.h4, this.s1);
			ulong num6 = Poly1305.mul32x32_64(this.h0, this.r1) + Poly1305.mul32x32_64(this.h1, this.r0) + Poly1305.mul32x32_64(this.h2, this.s4) + Poly1305.mul32x32_64(this.h3, this.s3) + Poly1305.mul32x32_64(this.h4, this.s2);
			ulong num7 = Poly1305.mul32x32_64(this.h0, this.r2) + Poly1305.mul32x32_64(this.h1, this.r1) + Poly1305.mul32x32_64(this.h2, this.r0) + Poly1305.mul32x32_64(this.h3, this.s4) + Poly1305.mul32x32_64(this.h4, this.s3);
			ulong num8 = Poly1305.mul32x32_64(this.h0, this.r3) + Poly1305.mul32x32_64(this.h1, this.r2) + Poly1305.mul32x32_64(this.h2, this.r1) + Poly1305.mul32x32_64(this.h3, this.r0) + Poly1305.mul32x32_64(this.h4, this.s4);
			ulong num9 = Poly1305.mul32x32_64(this.h0, this.r4) + Poly1305.mul32x32_64(this.h1, this.r3) + Poly1305.mul32x32_64(this.h2, this.r2) + Poly1305.mul32x32_64(this.h3, this.r1) + Poly1305.mul32x32_64(this.h4, this.r0);
			this.h0 = ((uint)num5 & 67108863U);
			num6 += num5 >> 26;
			this.h1 = ((uint)num6 & 67108863U);
			num7 += num6 >> 26;
			this.h2 = ((uint)num7 & 67108863U);
			num8 += num7 >> 26;
			this.h3 = ((uint)num8 & 67108863U);
			num9 += num8 >> 26;
			this.h4 = ((uint)num9 & 67108863U);
			this.h0 += (uint)(num9 >> 26) * 5U;
			this.h1 += this.h0 >> 26;
			this.h0 &= 67108863U;
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x00132BBC File Offset: 0x00130DBC
		public int DoFinal(byte[] output, int outOff)
		{
			Check.DataLength(output, outOff, 16, "Output buffer is too short.");
			if (this.currentBlockOffset > 0)
			{
				this.ProcessBlock();
			}
			this.h1 += this.h0 >> 26;
			this.h0 &= 67108863U;
			this.h2 += this.h1 >> 26;
			this.h1 &= 67108863U;
			this.h3 += this.h2 >> 26;
			this.h2 &= 67108863U;
			this.h4 += this.h3 >> 26;
			this.h3 &= 67108863U;
			this.h0 += (this.h4 >> 26) * 5U;
			this.h4 &= 67108863U;
			this.h1 += this.h0 >> 26;
			this.h0 &= 67108863U;
			uint num = this.h0 + 5U;
			uint num2 = num >> 26;
			num &= 67108863U;
			uint num3 = this.h1 + num2;
			num2 = num3 >> 26;
			num3 &= 67108863U;
			uint num4 = this.h2 + num2;
			num2 = num4 >> 26;
			num4 &= 67108863U;
			uint num5 = this.h3 + num2;
			num2 = num5 >> 26;
			num5 &= 67108863U;
			uint num6 = this.h4 + num2 - 67108864U;
			num2 = (num6 >> 31) - 1U;
			uint num7 = ~num2;
			this.h0 = ((this.h0 & num7) | (num & num2));
			this.h1 = ((this.h1 & num7) | (num3 & num2));
			this.h2 = ((this.h2 & num7) | (num4 & num2));
			this.h3 = ((this.h3 & num7) | (num5 & num2));
			this.h4 = ((this.h4 & num7) | (num6 & num2));
			ulong num8 = (ulong)(this.h0 | this.h1 << 26) + (ulong)this.k0;
			ulong num9 = (ulong)(this.h1 >> 6 | this.h2 << 20) + (ulong)this.k1;
			ulong num10 = (ulong)(this.h2 >> 12 | this.h3 << 14) + (ulong)this.k2;
			ulong num11 = (ulong)(this.h3 >> 18 | this.h4 << 8) + (ulong)this.k3;
			Pack.UInt32_To_LE((uint)num8, output, outOff);
			num9 += num8 >> 32;
			Pack.UInt32_To_LE((uint)num9, output, outOff + 4);
			num10 += num9 >> 32;
			Pack.UInt32_To_LE((uint)num10, output, outOff + 8);
			Pack.UInt32_To_LE((uint)(num11 + (num10 >> 32)), output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x00132E7C File Offset: 0x0013107C
		public void Reset()
		{
			this.currentBlockOffset = 0;
			this.h0 = (this.h1 = (this.h2 = (this.h3 = (this.h4 = 0U))));
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x00132EBB File Offset: 0x001310BB
		private static ulong mul32x32_64(uint i1, uint i2)
		{
			return (ulong)i1 * (ulong)i2;
		}

		// Token: 0x04001FFB RID: 8187
		private const int BlockSize = 16;

		// Token: 0x04001FFC RID: 8188
		private readonly IBlockCipher cipher;

		// Token: 0x04001FFD RID: 8189
		private readonly byte[] singleByte = new byte[1];

		// Token: 0x04001FFE RID: 8190
		private uint r0;

		// Token: 0x04001FFF RID: 8191
		private uint r1;

		// Token: 0x04002000 RID: 8192
		private uint r2;

		// Token: 0x04002001 RID: 8193
		private uint r3;

		// Token: 0x04002002 RID: 8194
		private uint r4;

		// Token: 0x04002003 RID: 8195
		private uint s1;

		// Token: 0x04002004 RID: 8196
		private uint s2;

		// Token: 0x04002005 RID: 8197
		private uint s3;

		// Token: 0x04002006 RID: 8198
		private uint s4;

		// Token: 0x04002007 RID: 8199
		private uint k0;

		// Token: 0x04002008 RID: 8200
		private uint k1;

		// Token: 0x04002009 RID: 8201
		private uint k2;

		// Token: 0x0400200A RID: 8202
		private uint k3;

		// Token: 0x0400200B RID: 8203
		private byte[] currentBlock = new byte[16];

		// Token: 0x0400200C RID: 8204
		private int currentBlockOffset;

		// Token: 0x0400200D RID: 8205
		private uint h0;

		// Token: 0x0400200E RID: 8206
		private uint h1;

		// Token: 0x0400200F RID: 8207
		private uint h2;

		// Token: 0x04002010 RID: 8208
		private uint h3;

		// Token: 0x04002011 RID: 8209
		private uint h4;
	}
}
