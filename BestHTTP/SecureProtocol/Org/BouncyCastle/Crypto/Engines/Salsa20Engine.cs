﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000578 RID: 1400
	public class Salsa20Engine : IStreamCipher
	{
		// Token: 0x06003544 RID: 13636 RVA: 0x00149F0C File Offset: 0x0014810C
		internal void PackTauOrSigma(int keyLength, uint[] state, int stateOffset)
		{
			int num = (keyLength - 16) / 4;
			state[stateOffset] = Salsa20Engine.TAU_SIGMA[num];
			state[stateOffset + 1] = Salsa20Engine.TAU_SIGMA[num + 1];
			state[stateOffset + 2] = Salsa20Engine.TAU_SIGMA[num + 2];
			state[stateOffset + 3] = Salsa20Engine.TAU_SIGMA[num + 3];
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x00149F54 File Offset: 0x00148154
		public Salsa20Engine() : this(Salsa20Engine.DEFAULT_ROUNDS)
		{
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x00149F64 File Offset: 0x00148164
		public Salsa20Engine(int rounds)
		{
			if (rounds <= 0 || (rounds & 1) != 0)
			{
				throw new ArgumentException("'rounds' must be a positive, even number");
			}
			this.rounds = rounds;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x00149FBC File Offset: 0x001481BC
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			ParametersWithIV parametersWithIV = parameters as ParametersWithIV;
			if (parametersWithIV == null)
			{
				throw new ArgumentException(this.AlgorithmName + " Init requires an IV", "parameters");
			}
			byte[] iv = parametersWithIV.GetIV();
			if (iv == null || iv.Length != this.NonceSize)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					this.AlgorithmName,
					" requires exactly ",
					this.NonceSize,
					" bytes of IV"
				}));
			}
			ICipherParameters parameters2 = parametersWithIV.Parameters;
			if (parameters2 == null)
			{
				if (!this.initialised)
				{
					throw new InvalidOperationException(this.AlgorithmName + " KeyParameter can not be null for first initialisation");
				}
				this.SetKey(null, iv);
			}
			else
			{
				if (!(parameters2 is KeyParameter))
				{
					throw new ArgumentException(this.AlgorithmName + " Init parameters must contain a KeyParameter (or null for re-init)");
				}
				this.SetKey(((KeyParameter)parameters2).GetKey(), iv);
			}
			this.Reset();
			this.initialised = true;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000FE681 File Offset: 0x000FC881
		protected virtual int NonceSize
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x0014A0AC File Offset: 0x001482AC
		public virtual string AlgorithmName
		{
			get
			{
				string text = "Salsa20";
				if (this.rounds != Salsa20Engine.DEFAULT_ROUNDS)
				{
					text = text + "/" + this.rounds;
				}
				return text;
			}
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x0014A0E4 File Offset: 0x001482E4
		public virtual byte ReturnByte(byte input)
		{
			if (this.LimitExceeded())
			{
				throw new MaxBytesExceededException("2^70 byte limit per IV; Change IV");
			}
			if (this.index == 0)
			{
				this.GenerateKeyStream(this.keyStream);
				this.AdvanceCounter();
			}
			byte result = this.keyStream[this.index] ^ input;
			this.index = (this.index + 1 & 63);
			return result;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x0014A140 File Offset: 0x00148340
		protected virtual void AdvanceCounter()
		{
			uint[] array = this.engineState;
			int num = 8;
			uint num2 = array[num] + 1U;
			array[num] = num2;
			if (num2 == 0U)
			{
				this.engineState[9] += 1U;
			}
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x0014A178 File Offset: 0x00148378
		public virtual void ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(inBytes, inOff, len, "input buffer too short");
			Check.OutputLength(outBytes, outOff, len, "output buffer too short");
			if (this.LimitExceeded((uint)len))
			{
				throw new MaxBytesExceededException("2^70 byte limit per IV would be exceeded; Change IV");
			}
			for (int i = 0; i < len; i++)
			{
				if (this.index == 0)
				{
					this.GenerateKeyStream(this.keyStream);
					this.AdvanceCounter();
				}
				outBytes[i + outOff] = (this.keyStream[this.index] ^ inBytes[i + inOff]);
				this.index = (this.index + 1 & 63);
			}
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x0014A225 File Offset: 0x00148425
		public virtual void Reset()
		{
			this.index = 0;
			this.ResetLimitCounter();
			this.ResetCounter();
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x0014A23C File Offset: 0x0014843C
		protected virtual void ResetCounter()
		{
			this.engineState[8] = (this.engineState[9] = 0U);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x0014A260 File Offset: 0x00148460
		protected virtual void SetKey(byte[] keyBytes, byte[] ivBytes)
		{
			if (keyBytes != null)
			{
				if (keyBytes.Length != 16 && keyBytes.Length != 32)
				{
					throw new ArgumentException(this.AlgorithmName + " requires 128 bit or 256 bit key");
				}
				int num = (keyBytes.Length - 16) / 4;
				this.engineState[0] = Salsa20Engine.TAU_SIGMA[num];
				this.engineState[5] = Salsa20Engine.TAU_SIGMA[num + 1];
				this.engineState[10] = Salsa20Engine.TAU_SIGMA[num + 2];
				this.engineState[15] = Salsa20Engine.TAU_SIGMA[num + 3];
				Pack.LE_To_UInt32(keyBytes, 0, this.engineState, 1, 4);
				Pack.LE_To_UInt32(keyBytes, keyBytes.Length - 16, this.engineState, 11, 4);
			}
			Pack.LE_To_UInt32(ivBytes, 0, this.engineState, 6, 2);
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x0014A317 File Offset: 0x00148517
		protected virtual void GenerateKeyStream(byte[] output)
		{
			Salsa20Engine.SalsaCore(this.rounds, this.engineState, this.x);
			Pack.UInt32_To_LE(this.x, output, 0);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x0014A340 File Offset: 0x00148540
		internal static void SalsaCore(int rounds, uint[] input, uint[] x)
		{
			if (input.Length != 16)
			{
				throw new ArgumentException();
			}
			if (x.Length != 16)
			{
				throw new ArgumentException();
			}
			if (rounds % 2 != 0)
			{
				throw new ArgumentException("Number of rounds must be even");
			}
			uint num = input[0];
			uint num2 = input[1];
			uint num3 = input[2];
			uint num4 = input[3];
			uint num5 = input[4];
			uint num6 = input[5];
			uint num7 = input[6];
			uint num8 = input[7];
			uint num9 = input[8];
			uint num10 = input[9];
			uint num11 = input[10];
			uint num12 = input[11];
			uint num13 = input[12];
			uint num14 = input[13];
			uint num15 = input[14];
			uint num16 = input[15];
			for (int i = rounds; i > 0; i -= 2)
			{
				num5 ^= Salsa20Engine.R(num + num13, 7);
				num9 ^= Salsa20Engine.R(num5 + num, 9);
				num13 ^= Salsa20Engine.R(num9 + num5, 13);
				num ^= Salsa20Engine.R(num13 + num9, 18);
				num10 ^= Salsa20Engine.R(num6 + num2, 7);
				num14 ^= Salsa20Engine.R(num10 + num6, 9);
				num2 ^= Salsa20Engine.R(num14 + num10, 13);
				num6 ^= Salsa20Engine.R(num2 + num14, 18);
				num15 ^= Salsa20Engine.R(num11 + num7, 7);
				num3 ^= Salsa20Engine.R(num15 + num11, 9);
				num7 ^= Salsa20Engine.R(num3 + num15, 13);
				num11 ^= Salsa20Engine.R(num7 + num3, 18);
				num4 ^= Salsa20Engine.R(num16 + num12, 7);
				num8 ^= Salsa20Engine.R(num4 + num16, 9);
				num12 ^= Salsa20Engine.R(num8 + num4, 13);
				num16 ^= Salsa20Engine.R(num12 + num8, 18);
				num2 ^= Salsa20Engine.R(num + num4, 7);
				num3 ^= Salsa20Engine.R(num2 + num, 9);
				num4 ^= Salsa20Engine.R(num3 + num2, 13);
				num ^= Salsa20Engine.R(num4 + num3, 18);
				num7 ^= Salsa20Engine.R(num6 + num5, 7);
				num8 ^= Salsa20Engine.R(num7 + num6, 9);
				num5 ^= Salsa20Engine.R(num8 + num7, 13);
				num6 ^= Salsa20Engine.R(num5 + num8, 18);
				num12 ^= Salsa20Engine.R(num11 + num10, 7);
				num9 ^= Salsa20Engine.R(num12 + num11, 9);
				num10 ^= Salsa20Engine.R(num9 + num12, 13);
				num11 ^= Salsa20Engine.R(num10 + num9, 18);
				num13 ^= Salsa20Engine.R(num16 + num15, 7);
				num14 ^= Salsa20Engine.R(num13 + num16, 9);
				num15 ^= Salsa20Engine.R(num14 + num13, 13);
				num16 ^= Salsa20Engine.R(num15 + num14, 18);
			}
			x[0] = num + input[0];
			x[1] = num2 + input[1];
			x[2] = num3 + input[2];
			x[3] = num4 + input[3];
			x[4] = num5 + input[4];
			x[5] = num6 + input[5];
			x[6] = num7 + input[6];
			x[7] = num8 + input[7];
			x[8] = num9 + input[8];
			x[9] = num10 + input[9];
			x[10] = num11 + input[10];
			x[11] = num12 + input[11];
			x[12] = num13 + input[12];
			x[13] = num14 + input[13];
			x[14] = num15 + input[14];
			x[15] = num16 + input[15];
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x0014A672 File Offset: 0x00148872
		internal static uint R(uint x, int y)
		{
			return x << y | x >> 32 - y;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x0014A684 File Offset: 0x00148884
		private void ResetLimitCounter()
		{
			this.cW0 = 0U;
			this.cW1 = 0U;
			this.cW2 = 0U;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x0014A69C File Offset: 0x0014889C
		private bool LimitExceeded()
		{
			uint num = this.cW0 + 1U;
			this.cW0 = num;
			if (num == 0U)
			{
				num = this.cW1 + 1U;
				this.cW1 = num;
				if (num == 0U)
				{
					num = this.cW2 + 1U;
					this.cW2 = num;
					return (num & 32U) > 0U;
				}
			}
			return false;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x0014A6E8 File Offset: 0x001488E8
		private bool LimitExceeded(uint len)
		{
			uint num = this.cW0;
			this.cW0 += len;
			if (this.cW0 < num)
			{
				uint num2 = this.cW1 + 1U;
				this.cW1 = num2;
				if (num2 == 0U)
				{
					num2 = this.cW2 + 1U;
					this.cW2 = num2;
					return (num2 & 32U) > 0U;
				}
			}
			return false;
		}

		// Token: 0x040021D8 RID: 8664
		public static readonly int DEFAULT_ROUNDS = 20;

		// Token: 0x040021D9 RID: 8665
		private const int StateSize = 16;

		// Token: 0x040021DA RID: 8666
		private static readonly uint[] TAU_SIGMA = Pack.LE_To_UInt32(Strings.ToAsciiByteArray("expand 16-byte kexpand 32-byte k"), 0, 8);

		// Token: 0x040021DB RID: 8667
		[Obsolete]
		protected static readonly byte[] sigma = Strings.ToAsciiByteArray("expand 32-byte k");

		// Token: 0x040021DC RID: 8668
		[Obsolete]
		protected static readonly byte[] tau = Strings.ToAsciiByteArray("expand 16-byte k");

		// Token: 0x040021DD RID: 8669
		protected int rounds;

		// Token: 0x040021DE RID: 8670
		private int index;

		// Token: 0x040021DF RID: 8671
		internal uint[] engineState = new uint[16];

		// Token: 0x040021E0 RID: 8672
		internal uint[] x = new uint[16];

		// Token: 0x040021E1 RID: 8673
		private byte[] keyStream = new byte[64];

		// Token: 0x040021E2 RID: 8674
		private bool initialised;

		// Token: 0x040021E3 RID: 8675
		private uint cW0;

		// Token: 0x040021E4 RID: 8676
		private uint cW1;

		// Token: 0x040021E5 RID: 8677
		private uint cW2;
	}
}
