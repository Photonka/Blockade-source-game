using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000594 RID: 1428
	public class KeccakDigest : IDigest, IMemoable
	{
		// Token: 0x060036C0 RID: 14016 RVA: 0x00158059 File Offset: 0x00156259
		public KeccakDigest() : this(288)
		{
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x00158066 File Offset: 0x00156266
		public KeccakDigest(int bitLength)
		{
			this.state = new ulong[25];
			this.dataQueue = new byte[192];
			base..ctor();
			this.Init(bitLength);
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x00158092 File Offset: 0x00156292
		public KeccakDigest(KeccakDigest source)
		{
			this.state = new ulong[25];
			this.dataQueue = new byte[192];
			base..ctor();
			this.CopyIn(source);
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x001580C0 File Offset: 0x001562C0
		private void CopyIn(KeccakDigest source)
		{
			Array.Copy(source.state, 0, this.state, 0, source.state.Length);
			Array.Copy(source.dataQueue, 0, this.dataQueue, 0, source.dataQueue.Length);
			this.rate = source.rate;
			this.bitsInQueue = source.bitsInQueue;
			this.fixedOutputLength = source.fixedOutputLength;
			this.squeezing = source.squeezing;
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x00158133 File Offset: 0x00156333
		public virtual string AlgorithmName
		{
			get
			{
				return "Keccak-" + this.fixedOutputLength;
			}
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x0015814A File Offset: 0x0015634A
		public virtual int GetDigestSize()
		{
			return this.fixedOutputLength >> 3;
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x00158154 File Offset: 0x00156354
		public virtual void Update(byte input)
		{
			this.Absorb(new byte[]
			{
				input
			}, 0, 1);
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x00158168 File Offset: 0x00156368
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.Absorb(input, inOff, len);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x00158173 File Offset: 0x00156373
		public virtual int DoFinal(byte[] output, int outOff)
		{
			this.Squeeze(output, outOff, (long)this.fixedOutputLength);
			this.Reset();
			return this.GetDigestSize();
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x00158190 File Offset: 0x00156390
		protected virtual int DoFinal(byte[] output, int outOff, byte partialByte, int partialBits)
		{
			if (partialBits > 0)
			{
				this.AbsorbBits((int)partialByte, partialBits);
			}
			this.Squeeze(output, outOff, (long)this.fixedOutputLength);
			this.Reset();
			return this.GetDigestSize();
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x001581BB File Offset: 0x001563BB
		public virtual void Reset()
		{
			this.Init(this.fixedOutputLength);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x001581C9 File Offset: 0x001563C9
		public virtual int GetByteLength()
		{
			return this.rate >> 3;
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x001581D4 File Offset: 0x001563D4
		private void Init(int bitLength)
		{
			if (bitLength <= 256)
			{
				if (bitLength != 128 && bitLength != 224 && bitLength != 256)
				{
					goto IL_4A;
				}
			}
			else if (bitLength != 288 && bitLength != 384 && bitLength != 512)
			{
				goto IL_4A;
			}
			this.InitSponge(1600 - (bitLength << 1));
			return;
			IL_4A:
			throw new ArgumentException("must be one of 128, 224, 256, 288, 384, or 512.", "bitLength");
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x0015823C File Offset: 0x0015643C
		private void InitSponge(int rate)
		{
			if (rate <= 0 || rate >= 1600 || (rate & 63) != 0)
			{
				throw new InvalidOperationException("invalid rate value");
			}
			this.rate = rate;
			Array.Clear(this.state, 0, this.state.Length);
			Arrays.Fill(this.dataQueue, 0);
			this.bitsInQueue = 0;
			this.squeezing = false;
			this.fixedOutputLength = 1600 - rate >> 1;
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x001582AC File Offset: 0x001564AC
		protected void Absorb(byte[] data, int off, int len)
		{
			if ((this.bitsInQueue & 7) != 0)
			{
				throw new InvalidOperationException("attempt to absorb with odd length queue");
			}
			if (this.squeezing)
			{
				throw new InvalidOperationException("attempt to absorb while squeezing");
			}
			int num = this.bitsInQueue >> 3;
			int num2 = this.rate >> 3;
			int i = 0;
			while (i < len)
			{
				if (num == 0 && i <= len - num2)
				{
					do
					{
						this.KeccakAbsorb(data, off + i);
						i += num2;
					}
					while (i <= len - num2);
				}
				else
				{
					int num3 = Math.Min(num2 - num, len - i);
					Array.Copy(data, off + i, this.dataQueue, num, num3);
					num += num3;
					i += num3;
					if (num == num2)
					{
						this.KeccakAbsorb(this.dataQueue, 0);
						num = 0;
					}
				}
			}
			this.bitsInQueue = num << 3;
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x0015835C File Offset: 0x0015655C
		protected void AbsorbBits(int data, int bits)
		{
			if (bits < 1 || bits > 7)
			{
				throw new ArgumentException("must be in the range 1 to 7", "bits");
			}
			if ((this.bitsInQueue & 7) != 0)
			{
				throw new InvalidOperationException("attempt to absorb with odd length queue");
			}
			if (this.squeezing)
			{
				throw new InvalidOperationException("attempt to absorb while squeezing");
			}
			int num = (1 << bits) - 1;
			this.dataQueue[this.bitsInQueue >> 3] = (byte)(data & num);
			this.bitsInQueue += bits;
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x001583D4 File Offset: 0x001565D4
		private void PadAndSwitchToSqueezingPhase()
		{
			byte[] array = this.dataQueue;
			int num = this.bitsInQueue >> 3;
			array[num] |= (byte)(1 << (this.bitsInQueue & 7));
			int num2 = this.bitsInQueue + 1;
			this.bitsInQueue = num2;
			if (num2 == this.rate)
			{
				this.KeccakAbsorb(this.dataQueue, 0);
				this.bitsInQueue = 0;
			}
			int num3 = this.bitsInQueue >> 6;
			int num4 = this.bitsInQueue & 63;
			int num5 = 0;
			for (int i = 0; i < num3; i++)
			{
				this.state[i] ^= Pack.LE_To_UInt64(this.dataQueue, num5);
				num5 += 8;
			}
			if (num4 > 0)
			{
				ulong num6 = (1UL << num4) - 1UL;
				this.state[num3] ^= (Pack.LE_To_UInt64(this.dataQueue, num5) & num6);
			}
			this.state[this.rate - 1 >> 6] ^= 9223372036854775808UL;
			this.KeccakPermutation();
			this.KeccakExtract();
			this.bitsInQueue = this.rate;
			this.squeezing = true;
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x001584EC File Offset: 0x001566EC
		protected void Squeeze(byte[] output, int offset, long outputLength)
		{
			if (!this.squeezing)
			{
				this.PadAndSwitchToSqueezingPhase();
			}
			if ((outputLength & 7L) != 0L)
			{
				throw new InvalidOperationException("outputLength not a multiple of 8");
			}
			int num2;
			for (long num = 0L; num < outputLength; num += (long)num2)
			{
				if (this.bitsInQueue == 0)
				{
					this.KeccakPermutation();
					this.KeccakExtract();
					this.bitsInQueue = this.rate;
				}
				num2 = (int)Math.Min((long)this.bitsInQueue, outputLength - num);
				Array.Copy(this.dataQueue, this.rate - this.bitsInQueue >> 3, output, offset + (int)(num >> 3), num2 >> 3);
				this.bitsInQueue -= num2;
			}
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x0015858C File Offset: 0x0015678C
		private void KeccakAbsorb(byte[] data, int off)
		{
			int num = this.rate >> 6;
			for (int i = 0; i < num; i++)
			{
				this.state[i] ^= Pack.LE_To_UInt64(data, off);
				off += 8;
			}
			this.KeccakPermutation();
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x001585D0 File Offset: 0x001567D0
		private void KeccakExtract()
		{
			Pack.UInt64_To_LE(this.state, 0, this.rate >> 6, this.dataQueue, 0);
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x001585F0 File Offset: 0x001567F0
		private void KeccakPermutation()
		{
			ulong[] array = this.state;
			ulong num = array[0];
			ulong num2 = array[1];
			ulong num3 = array[2];
			ulong num4 = array[3];
			ulong num5 = array[4];
			ulong num6 = array[5];
			ulong num7 = array[6];
			ulong num8 = array[7];
			ulong num9 = array[8];
			ulong num10 = array[9];
			ulong num11 = array[10];
			ulong num12 = array[11];
			ulong num13 = array[12];
			ulong num14 = array[13];
			ulong num15 = array[14];
			ulong num16 = array[15];
			ulong num17 = array[16];
			ulong num18 = array[17];
			ulong num19 = array[18];
			ulong num20 = array[19];
			ulong num21 = array[20];
			ulong num22 = array[21];
			ulong num23 = array[22];
			ulong num24 = array[23];
			ulong num25 = array[24];
			for (int i = 0; i < 24; i++)
			{
				ulong num26 = num ^ num6 ^ num11 ^ num16 ^ num21;
				ulong num27 = num2 ^ num7 ^ num12 ^ num17 ^ num22;
				ulong num28 = num3 ^ num8 ^ num13 ^ num18 ^ num23;
				ulong num29 = num4 ^ num9 ^ num14 ^ num19 ^ num24;
				ulong num30 = num5 ^ num10 ^ num15 ^ num20 ^ num25;
				ulong num31 = (num27 << 1 | num27 >> 63) ^ num30;
				ulong num32 = (num28 << 1 | num28 >> 63) ^ num26;
				ulong num33 = (num29 << 1 | num29 >> 63) ^ num27;
				ulong num34 = (num30 << 1 | num30 >> 63) ^ num28;
				ulong num35 = (num26 << 1 | num26 >> 63) ^ num29;
				num ^= num31;
				num6 ^= num31;
				num11 ^= num31;
				num16 ^= num31;
				num21 ^= num31;
				num2 ^= num32;
				num7 ^= num32;
				num12 ^= num32;
				num17 ^= num32;
				num22 ^= num32;
				num3 ^= num33;
				num8 ^= num33;
				num13 ^= num33;
				num18 ^= num33;
				num23 ^= num33;
				num4 ^= num34;
				num9 ^= num34;
				num14 ^= num34;
				num19 ^= num34;
				num24 ^= num34;
				num5 ^= num35;
				num10 ^= num35;
				num15 ^= num35;
				num20 ^= num35;
				num25 ^= num35;
				num27 = (num2 << 1 | num2 >> 63);
				num2 = (num7 << 44 | num7 >> 20);
				num7 = (num10 << 20 | num10 >> 44);
				num10 = (num23 << 61 | num23 >> 3);
				num23 = (num15 << 39 | num15 >> 25);
				num15 = (num21 << 18 | num21 >> 46);
				num21 = (num3 << 62 | num3 >> 2);
				num3 = (num13 << 43 | num13 >> 21);
				num13 = (num14 << 25 | num14 >> 39);
				num14 = (num20 << 8 | num20 >> 56);
				num20 = (num24 << 56 | num24 >> 8);
				num24 = (num16 << 41 | num16 >> 23);
				num16 = (num5 << 27 | num5 >> 37);
				num5 = (num25 << 14 | num25 >> 50);
				num25 = (num22 << 2 | num22 >> 62);
				num22 = (num9 << 55 | num9 >> 9);
				num9 = (num17 << 45 | num17 >> 19);
				num17 = (num6 << 36 | num6 >> 28);
				num6 = (num4 << 28 | num4 >> 36);
				num4 = (num19 << 21 | num19 >> 43);
				num19 = (num18 << 15 | num18 >> 49);
				num18 = (num12 << 10 | num12 >> 54);
				num12 = (num8 << 6 | num8 >> 58);
				num8 = (num11 << 3 | num11 >> 61);
				num11 = num27;
				num26 = (num ^ (~num2 & num3));
				num27 = (num2 ^ (~num3 & num4));
				num3 ^= (~num4 & num5);
				num4 ^= (~num5 & num);
				num5 ^= (~num & num2);
				num = num26;
				num2 = num27;
				num26 = (num6 ^ (~num7 & num8));
				num27 = (num7 ^ (~num8 & num9));
				num8 ^= (~num9 & num10);
				num9 ^= (~num10 & num6);
				num10 ^= (~num6 & num7);
				num6 = num26;
				num7 = num27;
				num26 = (num11 ^ (~num12 & num13));
				num27 = (num12 ^ (~num13 & num14));
				num13 ^= (~num14 & num15);
				num14 ^= (~num15 & num11);
				num15 ^= (~num11 & num12);
				num11 = num26;
				num12 = num27;
				num26 = (num16 ^ (~num17 & num18));
				num27 = (num17 ^ (~num18 & num19));
				num18 ^= (~num19 & num20);
				num19 ^= (~num20 & num16);
				num20 ^= (~num16 & num17);
				num16 = num26;
				num17 = num27;
				num26 = (num21 ^ (~num22 & num23));
				num27 = (num22 ^ (~num23 & num24));
				num23 ^= (~num24 & num25);
				num24 ^= (~num25 & num21);
				num25 ^= (~num21 & num22);
				num21 = num26;
				num22 = num27;
				num ^= KeccakDigest.KeccakRoundConstants[i];
			}
			array[0] = num;
			array[1] = num2;
			array[2] = num3;
			array[3] = num4;
			array[4] = num5;
			array[5] = num6;
			array[6] = num7;
			array[7] = num8;
			array[8] = num9;
			array[9] = num10;
			array[10] = num11;
			array[11] = num12;
			array[12] = num13;
			array[13] = num14;
			array[14] = num15;
			array[15] = num16;
			array[16] = num17;
			array[17] = num18;
			array[18] = num19;
			array[19] = num20;
			array[20] = num21;
			array[21] = num22;
			array[22] = num23;
			array[23] = num24;
			array[24] = num25;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x00158AD8 File Offset: 0x00156CD8
		public virtual IMemoable Copy()
		{
			return new KeccakDigest(this);
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x00158AE0 File Offset: 0x00156CE0
		public virtual void Reset(IMemoable other)
		{
			this.CopyIn((KeccakDigest)other);
		}

		// Token: 0x040022D8 RID: 8920
		private static readonly ulong[] KeccakRoundConstants = new ulong[]
		{
			1UL,
			32898UL,
			9223372036854808714UL,
			9223372039002292224UL,
			32907UL,
			2147483649UL,
			9223372039002292353UL,
			9223372036854808585UL,
			138UL,
			136UL,
			2147516425UL,
			2147483658UL,
			2147516555UL,
			9223372036854775947UL,
			9223372036854808713UL,
			9223372036854808579UL,
			9223372036854808578UL,
			9223372036854775936UL,
			32778UL,
			9223372039002259466UL,
			9223372039002292353UL,
			9223372036854808704UL,
			2147483649UL,
			9223372039002292232UL
		};

		// Token: 0x040022D9 RID: 8921
		private ulong[] state;

		// Token: 0x040022DA RID: 8922
		protected byte[] dataQueue;

		// Token: 0x040022DB RID: 8923
		protected int rate;

		// Token: 0x040022DC RID: 8924
		protected int bitsInQueue;

		// Token: 0x040022DD RID: 8925
		protected int fixedOutputLength;

		// Token: 0x040022DE RID: 8926
		protected bool squeezing;
	}
}
