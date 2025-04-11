using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000257 RID: 599
	internal sealed class Inflate
	{
		// Token: 0x0600165A RID: 5722 RVA: 0x000B6C78 File Offset: 0x000B4E78
		internal int inflateReset(ZStream z)
		{
			if (z == null || z.istate == null)
			{
				return -2;
			}
			z.total_in = (z.total_out = 0L);
			z.msg = null;
			z.istate.mode = ((z.istate.nowrap != 0) ? 7 : 0);
			z.istate.blocks.reset(z, null);
			return 0;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000B6CDA File Offset: 0x000B4EDA
		internal int inflateEnd(ZStream z)
		{
			if (this.blocks != null)
			{
				this.blocks.free(z);
			}
			this.blocks = null;
			return 0;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000B6CF8 File Offset: 0x000B4EF8
		internal int inflateInit(ZStream z, int w)
		{
			z.msg = null;
			this.blocks = null;
			this.nowrap = 0;
			if (w < 0)
			{
				w = -w;
				this.nowrap = 1;
			}
			if (w < 8 || w > 15)
			{
				this.inflateEnd(z);
				return -2;
			}
			this.wbits = w;
			z.istate.blocks = new InfBlocks(z, (z.istate.nowrap != 0) ? null : this, 1 << w);
			this.inflateReset(z);
			return 0;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000B6D78 File Offset: 0x000B4F78
		internal int inflate(ZStream z, int f)
		{
			if (z == null || z.istate == null || z.next_in == null)
			{
				return -2;
			}
			f = ((f == 4) ? -5 : 0);
			int num = -5;
			int next_in_index;
			for (;;)
			{
				switch (z.istate.mode)
				{
				case 0:
				{
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in += 1L;
					Inflate istate = z.istate;
					byte[] next_in = z.next_in;
					next_in_index = z.next_in_index;
					z.next_in_index = next_in_index + 1;
					if (((istate.method = next_in[next_in_index]) & 15) != 8)
					{
						z.istate.mode = 13;
						z.msg = "unknown compression method";
						z.istate.marker = 5;
						continue;
					}
					if ((z.istate.method >> 4) + 8 > z.istate.wbits)
					{
						z.istate.mode = 13;
						z.msg = "invalid window size";
						z.istate.marker = 5;
						continue;
					}
					z.istate.mode = 1;
					goto IL_142;
				}
				case 1:
					goto IL_142;
				case 2:
					goto IL_1EA;
				case 3:
					goto IL_253;
				case 4:
					goto IL_2C3;
				case 5:
					goto IL_332;
				case 6:
					goto IL_3AC;
				case 7:
					num = z.istate.blocks.proc(z, num);
					if (num == -3)
					{
						z.istate.mode = 13;
						z.istate.marker = 0;
						continue;
					}
					if (num == 0)
					{
						num = f;
					}
					if (num != 1)
					{
						return num;
					}
					num = f;
					z.istate.blocks.reset(z, z.istate.was);
					if (z.istate.nowrap != 0)
					{
						z.istate.mode = 12;
						continue;
					}
					z.istate.mode = 8;
					goto IL_45D;
				case 8:
					goto IL_45D;
				case 9:
					goto IL_4C7;
				case 10:
					goto IL_538;
				case 11:
					goto IL_5A8;
				case 12:
					return 1;
				case 13:
					return -3;
				}
				break;
				IL_142:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				byte[] next_in2 = z.next_in;
				next_in_index = z.next_in_index;
				z.next_in_index = next_in_index + 1;
				int num2 = next_in2[next_in_index] & 255;
				if (((z.istate.method << 8) + num2) % 31 != 0)
				{
					z.istate.mode = 13;
					z.msg = "incorrect header check";
					z.istate.marker = 5;
					continue;
				}
				if ((num2 & 32) == 0)
				{
					z.istate.mode = 7;
					continue;
				}
				goto IL_1DE;
				IL_5A8:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				Inflate istate2 = z.istate;
				long num3 = istate2.need;
				byte[] next_in3 = z.next_in;
				next_in_index = z.next_in_index;
				z.next_in_index = next_in_index + 1;
				istate2.need = num3 + (long)(next_in3[next_in_index] & 255UL);
				if ((int)z.istate.was[0] != (int)z.istate.need)
				{
					z.istate.mode = 13;
					z.msg = "incorrect data check";
					z.istate.marker = 5;
					continue;
				}
				goto IL_648;
				IL_538:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				Inflate istate3 = z.istate;
				long num4 = istate3.need;
				byte[] next_in4 = z.next_in;
				next_in_index = z.next_in_index;
				z.next_in_index = next_in_index + 1;
				istate3.need = num4 + ((next_in4[next_in_index] & 255L) << 8 & 65280L);
				z.istate.mode = 11;
				goto IL_5A8;
				IL_4C7:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				Inflate istate4 = z.istate;
				long num5 = istate4.need;
				byte[] next_in5 = z.next_in;
				next_in_index = z.next_in_index;
				z.next_in_index = next_in_index + 1;
				istate4.need = num5 + ((next_in5[next_in_index] & 255L) << 16 & 16711680L);
				z.istate.mode = 10;
				goto IL_538;
				IL_45D:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				Inflate istate5 = z.istate;
				byte[] next_in6 = z.next_in;
				next_in_index = z.next_in_index;
				z.next_in_index = next_in_index + 1;
				istate5.need = ((next_in6[next_in_index] & 255L) << 24 & (long)((ulong)-16777216));
				z.istate.mode = 9;
				goto IL_4C7;
			}
			return -2;
			IL_1DE:
			z.istate.mode = 2;
			IL_1EA:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			Inflate istate6 = z.istate;
			byte[] next_in7 = z.next_in;
			next_in_index = z.next_in_index;
			z.next_in_index = next_in_index + 1;
			istate6.need = ((next_in7[next_in_index] & 255L) << 24 & (long)((ulong)-16777216));
			z.istate.mode = 3;
			IL_253:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			Inflate istate7 = z.istate;
			long num6 = istate7.need;
			byte[] next_in8 = z.next_in;
			next_in_index = z.next_in_index;
			z.next_in_index = next_in_index + 1;
			istate7.need = num6 + ((next_in8[next_in_index] & 255L) << 16 & 16711680L);
			z.istate.mode = 4;
			IL_2C3:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			Inflate istate8 = z.istate;
			long num7 = istate8.need;
			byte[] next_in9 = z.next_in;
			next_in_index = z.next_in_index;
			z.next_in_index = next_in_index + 1;
			istate8.need = num7 + ((next_in9[next_in_index] & 255L) << 8 & 65280L);
			z.istate.mode = 5;
			IL_332:
			if (z.avail_in == 0)
			{
				return num;
			}
			z.avail_in--;
			z.total_in += 1L;
			Inflate istate9 = z.istate;
			long num8 = istate9.need;
			byte[] next_in10 = z.next_in;
			next_in_index = z.next_in_index;
			z.next_in_index = next_in_index + 1;
			istate9.need = num8 + (long)(next_in10[next_in_index] & 255UL);
			z.adler = z.istate.need;
			z.istate.mode = 6;
			return 2;
			IL_3AC:
			z.istate.mode = 13;
			z.msg = "need dictionary";
			z.istate.marker = 0;
			return -2;
			IL_648:
			z.istate.mode = 12;
			return 1;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000B73E4 File Offset: 0x000B55E4
		internal int inflateSetDictionary(ZStream z, byte[] dictionary, int dictLength)
		{
			int start = 0;
			int num = dictLength;
			if (z == null || z.istate == null || z.istate.mode != 6)
			{
				return -2;
			}
			if (z._adler.adler32(1L, dictionary, 0, dictLength) != z.adler)
			{
				return -3;
			}
			z.adler = z._adler.adler32(0L, null, 0, 0);
			if (num >= 1 << z.istate.wbits)
			{
				num = (1 << z.istate.wbits) - 1;
				start = dictLength - num;
			}
			z.istate.blocks.set_dictionary(dictionary, start, num);
			z.istate.mode = 7;
			return 0;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000B748C File Offset: 0x000B568C
		internal int inflateSync(ZStream z)
		{
			if (z == null || z.istate == null)
			{
				return -2;
			}
			if (z.istate.mode != 13)
			{
				z.istate.mode = 13;
				z.istate.marker = 0;
			}
			int num;
			if ((num = z.avail_in) == 0)
			{
				return -5;
			}
			int num2 = z.next_in_index;
			int num3 = z.istate.marker;
			while (num != 0 && num3 < 4)
			{
				if (z.next_in[num2] == Inflate.mark[num3])
				{
					num3++;
				}
				else if (z.next_in[num2] != 0)
				{
					num3 = 0;
				}
				else
				{
					num3 = 4 - num3;
				}
				num2++;
				num--;
			}
			z.total_in += (long)(num2 - z.next_in_index);
			z.next_in_index = num2;
			z.avail_in = num;
			z.istate.marker = num3;
			if (num3 != 4)
			{
				return -3;
			}
			long total_in = z.total_in;
			long total_out = z.total_out;
			this.inflateReset(z);
			z.total_in = total_in;
			z.total_out = total_out;
			z.istate.mode = 7;
			return 0;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000B7593 File Offset: 0x000B5793
		internal int inflateSyncPoint(ZStream z)
		{
			if (z == null || z.istate == null || z.istate.blocks == null)
			{
				return -2;
			}
			return z.istate.blocks.sync_point();
		}

		// Token: 0x0400160B RID: 5643
		private const int MAX_WBITS = 15;

		// Token: 0x0400160C RID: 5644
		private const int PRESET_DICT = 32;

		// Token: 0x0400160D RID: 5645
		internal const int Z_NO_FLUSH = 0;

		// Token: 0x0400160E RID: 5646
		internal const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x0400160F RID: 5647
		internal const int Z_SYNC_FLUSH = 2;

		// Token: 0x04001610 RID: 5648
		internal const int Z_FULL_FLUSH = 3;

		// Token: 0x04001611 RID: 5649
		internal const int Z_FINISH = 4;

		// Token: 0x04001612 RID: 5650
		private const int Z_DEFLATED = 8;

		// Token: 0x04001613 RID: 5651
		private const int Z_OK = 0;

		// Token: 0x04001614 RID: 5652
		private const int Z_STREAM_END = 1;

		// Token: 0x04001615 RID: 5653
		private const int Z_NEED_DICT = 2;

		// Token: 0x04001616 RID: 5654
		private const int Z_ERRNO = -1;

		// Token: 0x04001617 RID: 5655
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04001618 RID: 5656
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04001619 RID: 5657
		private const int Z_MEM_ERROR = -4;

		// Token: 0x0400161A RID: 5658
		private const int Z_BUF_ERROR = -5;

		// Token: 0x0400161B RID: 5659
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x0400161C RID: 5660
		private const int METHOD = 0;

		// Token: 0x0400161D RID: 5661
		private const int FLAG = 1;

		// Token: 0x0400161E RID: 5662
		private const int DICT4 = 2;

		// Token: 0x0400161F RID: 5663
		private const int DICT3 = 3;

		// Token: 0x04001620 RID: 5664
		private const int DICT2 = 4;

		// Token: 0x04001621 RID: 5665
		private const int DICT1 = 5;

		// Token: 0x04001622 RID: 5666
		private const int DICT0 = 6;

		// Token: 0x04001623 RID: 5667
		private const int BLOCKS = 7;

		// Token: 0x04001624 RID: 5668
		private const int CHECK4 = 8;

		// Token: 0x04001625 RID: 5669
		private const int CHECK3 = 9;

		// Token: 0x04001626 RID: 5670
		private const int CHECK2 = 10;

		// Token: 0x04001627 RID: 5671
		private const int CHECK1 = 11;

		// Token: 0x04001628 RID: 5672
		private const int DONE = 12;

		// Token: 0x04001629 RID: 5673
		private const int BAD = 13;

		// Token: 0x0400162A RID: 5674
		internal int mode;

		// Token: 0x0400162B RID: 5675
		internal int method;

		// Token: 0x0400162C RID: 5676
		internal long[] was = new long[1];

		// Token: 0x0400162D RID: 5677
		internal long need;

		// Token: 0x0400162E RID: 5678
		internal int marker;

		// Token: 0x0400162F RID: 5679
		internal int nowrap;

		// Token: 0x04001630 RID: 5680
		internal int wbits;

		// Token: 0x04001631 RID: 5681
		internal InfBlocks blocks;

		// Token: 0x04001632 RID: 5682
		private static readonly byte[] mark = new byte[]
		{
			0,
			0,
			byte.MaxValue,
			byte.MaxValue
		};
	}
}
