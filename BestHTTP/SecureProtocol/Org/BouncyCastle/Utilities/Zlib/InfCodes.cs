﻿using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000256 RID: 598
	internal sealed class InfCodes
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x00023EF4 File Offset: 0x000220F4
		internal InfCodes()
		{
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000B5B4C File Offset: 0x000B3D4C
		internal void init(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, ZStream z)
		{
			this.mode = 0;
			this.lbits = (byte)bl;
			this.dbits = (byte)bd;
			this.ltree = tl;
			this.ltree_index = tl_index;
			this.dtree = td;
			this.dtree_index = td_index;
			this.tree = null;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000B5B8C File Offset: 0x000B3D8C
		internal int proc(InfBlocks s, ZStream z, int r)
		{
			int num = z.next_in_index;
			int num2 = z.avail_in;
			int num3 = s.bitb;
			int i = s.bitk;
			int num4 = s.write;
			int num5 = (num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4);
			for (;;)
			{
				int num6;
				switch (this.mode)
				{
				case 0:
					if (num5 >= 258 && num2 >= 10)
					{
						s.bitb = num3;
						s.bitk = i;
						z.avail_in = num2;
						z.total_in += (long)(num - z.next_in_index);
						z.next_in_index = num;
						s.write = num4;
						r = this.inflate_fast((int)this.lbits, (int)this.dbits, this.ltree, this.ltree_index, this.dtree, this.dtree_index, s, z);
						num = z.next_in_index;
						num2 = z.avail_in;
						num3 = s.bitb;
						i = s.bitk;
						num4 = s.write;
						num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
						if (r != 0)
						{
							this.mode = ((r == 1) ? 7 : 9);
							continue;
						}
					}
					this.need = (int)this.lbits;
					this.tree = this.ltree;
					this.tree_index = this.ltree_index;
					this.mode = 1;
					goto IL_199;
				case 1:
					goto IL_199;
				case 2:
					num6 = this.get;
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_34E;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.len += (num3 & InfCodes.inflate_mask[num6]);
					num3 >>= num6;
					i -= num6;
					this.need = (int)this.dbits;
					this.tree = this.dtree;
					this.tree_index = this.dtree_index;
					this.mode = 3;
					goto IL_411;
				case 3:
					goto IL_411;
				case 4:
					num6 = this.get;
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_595;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.dist += (num3 & InfCodes.inflate_mask[num6]);
					num3 >>= num6;
					i -= num6;
					this.mode = 5;
					goto IL_634;
				case 5:
					goto IL_634;
				case 6:
					if (num5 == 0)
					{
						if (num4 == s.end && s.read != 0)
						{
							num4 = 0;
							num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
						}
						if (num5 == 0)
						{
							s.write = num4;
							r = s.inflate_flush(z, r);
							num4 = s.write;
							num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
							if (num4 == s.end && s.read != 0)
							{
								num4 = 0;
								num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
							}
							if (num5 == 0)
							{
								goto Block_44;
							}
						}
					}
					r = 0;
					s.window[num4++] = (byte)this.lit;
					num5--;
					this.mode = 0;
					continue;
				case 7:
					goto IL_8DA;
				case 8:
					goto IL_989;
				case 9:
					goto IL_9D3;
				}
				break;
				IL_199:
				num6 = this.need;
				while (i < num6)
				{
					if (num2 == 0)
					{
						goto IL_1AB;
					}
					r = 0;
					num2--;
					num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
					i += 8;
				}
				int num7 = (this.tree_index + (num3 & InfCodes.inflate_mask[num6])) * 3;
				num3 >>= this.tree[num7 + 1];
				i -= this.tree[num7 + 1];
				int num8 = this.tree[num7];
				if (num8 == 0)
				{
					this.lit = this.tree[num7 + 2];
					this.mode = 6;
					continue;
				}
				if ((num8 & 16) != 0)
				{
					this.get = (num8 & 15);
					this.len = this.tree[num7 + 2];
					this.mode = 2;
					continue;
				}
				if ((num8 & 64) == 0)
				{
					this.need = num8;
					this.tree_index = num7 / 3 + this.tree[num7 + 2];
					continue;
				}
				if ((num8 & 32) != 0)
				{
					this.mode = 7;
					continue;
				}
				goto IL_2DE;
				IL_411:
				num6 = this.need;
				while (i < num6)
				{
					if (num2 == 0)
					{
						goto IL_423;
					}
					r = 0;
					num2--;
					num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
					i += 8;
				}
				num7 = (this.tree_index + (num3 & InfCodes.inflate_mask[num6])) * 3;
				num3 >>= this.tree[num7 + 1];
				i -= this.tree[num7 + 1];
				num8 = this.tree[num7];
				if ((num8 & 16) != 0)
				{
					this.get = (num8 & 15);
					this.dist = this.tree[num7 + 2];
					this.mode = 4;
					continue;
				}
				if ((num8 & 64) == 0)
				{
					this.need = num8;
					this.tree_index = num7 / 3 + this.tree[num7 + 2];
					continue;
				}
				goto IL_525;
				IL_634:
				int j;
				for (j = num4 - this.dist; j < 0; j += s.end)
				{
				}
				while (this.len != 0)
				{
					if (num5 == 0)
					{
						if (num4 == s.end && s.read != 0)
						{
							num4 = 0;
							num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
						}
						if (num5 == 0)
						{
							s.write = num4;
							r = s.inflate_flush(z, r);
							num4 = s.write;
							num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
							if (num4 == s.end && s.read != 0)
							{
								num4 = 0;
								num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
							}
							if (num5 == 0)
							{
								goto Block_32;
							}
						}
					}
					s.window[num4++] = s.window[j++];
					num5--;
					if (j == s.end)
					{
						j = 0;
					}
					this.len--;
				}
				this.mode = 0;
			}
			r = -2;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_1AB:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_2DE:
			this.mode = 9;
			z.msg = "invalid literal/length code";
			r = -3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_34E:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_423:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_525:
			this.mode = 9;
			z.msg = "invalid distance code";
			r = -3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_595:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			Block_32:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			Block_44:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_8DA:
			if (i > 7)
			{
				i -= 8;
				num2++;
				num--;
			}
			s.write = num4;
			r = s.inflate_flush(z, r);
			num4 = s.write;
			int num9 = (num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4);
			if (s.read != s.write)
			{
				s.bitb = num3;
				s.bitk = i;
				z.avail_in = num2;
				z.total_in += (long)(num - z.next_in_index);
				z.next_in_index = num;
				s.write = num4;
				return s.inflate_flush(z, r);
			}
			this.mode = 8;
			IL_989:
			r = 1;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_9D3:
			r = -3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00002B75 File Offset: 0x00000D75
		internal void free(ZStream z)
		{
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000B6604 File Offset: 0x000B4804
		internal int inflate_fast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InfBlocks s, ZStream z)
		{
			int num = z.next_in_index;
			int num2 = z.avail_in;
			int num3 = s.bitb;
			int i = s.bitk;
			int num4 = s.write;
			int num5 = (num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4);
			int num6 = InfCodes.inflate_mask[bl];
			int num7 = InfCodes.inflate_mask[bd];
			int num10;
			int num11;
			for (;;)
			{
				if (i >= 20)
				{
					int num8 = num3 & num6;
					int num9 = (tl_index + num8) * 3;
					if ((num10 = tl[num9]) == 0)
					{
						num3 >>= tl[num9 + 1];
						i -= tl[num9 + 1];
						s.window[num4++] = (byte)tl[num9 + 2];
						num5--;
					}
					else
					{
						for (;;)
						{
							num3 >>= tl[num9 + 1];
							i -= tl[num9 + 1];
							if ((num10 & 16) != 0)
							{
								break;
							}
							if ((num10 & 64) != 0)
							{
								goto IL_4B3;
							}
							num8 += tl[num9 + 2];
							num8 += (num3 & InfCodes.inflate_mask[num10]);
							num9 = (tl_index + num8) * 3;
							if ((num10 = tl[num9]) == 0)
							{
								goto Block_20;
							}
						}
						num10 &= 15;
						num11 = tl[num9 + 2] + (num3 & InfCodes.inflate_mask[num10]);
						num3 >>= num10;
						for (i -= num10; i < 15; i += 8)
						{
							num2--;
							num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						}
						num8 = (num3 & num7);
						num9 = (td_index + num8) * 3;
						num10 = td[num9];
						for (;;)
						{
							num3 >>= td[num9 + 1];
							i -= td[num9 + 1];
							if ((num10 & 16) != 0)
							{
								break;
							}
							if ((num10 & 64) != 0)
							{
								goto IL_3C1;
							}
							num8 += td[num9 + 2];
							num8 += (num3 & InfCodes.inflate_mask[num10]);
							num9 = (td_index + num8) * 3;
							num10 = td[num9];
						}
						num10 &= 15;
						while (i < num10)
						{
							num2--;
							num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
							i += 8;
						}
						int num12 = td[num9 + 2] + (num3 & InfCodes.inflate_mask[num10]);
						num3 >>= num10;
						i -= num10;
						num5 -= num11;
						int num13;
						if (num4 >= num12)
						{
							num13 = num4 - num12;
							if (num4 - num13 > 0 && 2 > num4 - num13)
							{
								s.window[num4++] = s.window[num13++];
								s.window[num4++] = s.window[num13++];
								num11 -= 2;
							}
							else
							{
								Array.Copy(s.window, num13, s.window, num4, 2);
								num4 += 2;
								num13 += 2;
								num11 -= 2;
							}
						}
						else
						{
							num13 = num4 - num12;
							do
							{
								num13 += s.end;
							}
							while (num13 < 0);
							num10 = s.end - num13;
							if (num11 > num10)
							{
								num11 -= num10;
								if (num4 - num13 > 0 && num10 > num4 - num13)
								{
									do
									{
										s.window[num4++] = s.window[num13++];
									}
									while (--num10 != 0);
								}
								else
								{
									Array.Copy(s.window, num13, s.window, num4, num10);
									num4 += num10;
									num13 += num10;
								}
								num13 = 0;
							}
						}
						if (num4 - num13 > 0 && num11 > num4 - num13)
						{
							do
							{
								s.window[num4++] = s.window[num13++];
							}
							while (--num11 != 0);
							goto IL_5C0;
						}
						Array.Copy(s.window, num13, s.window, num4, num11);
						num4 += num11;
						num13 += num11;
						goto IL_5C0;
						Block_20:
						num3 >>= tl[num9 + 1];
						i -= tl[num9 + 1];
						s.window[num4++] = (byte)tl[num9 + 2];
						num5--;
					}
					IL_5C0:
					if (num5 < 258 || num2 < 10)
					{
						goto IL_5D2;
					}
				}
				else
				{
					num2--;
					num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
					i += 8;
				}
			}
			IL_3C1:
			z.msg = "invalid distance code";
			num11 = z.avail_in - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return -3;
			IL_4B3:
			if ((num10 & 32) != 0)
			{
				num11 = z.avail_in - num2;
				num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
				num2 += num11;
				num -= num11;
				i -= num11 << 3;
				s.bitb = num3;
				s.bitk = i;
				z.avail_in = num2;
				z.total_in += (long)(num - z.next_in_index);
				z.next_in_index = num;
				s.write = num4;
				return 1;
			}
			z.msg = "invalid literal/length code";
			num11 = z.avail_in - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return -3;
			IL_5D2:
			num11 = z.avail_in - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return 0;
		}

		// Token: 0x040015E9 RID: 5609
		private static readonly int[] inflate_mask = new int[]
		{
			0,
			1,
			3,
			7,
			15,
			31,
			63,
			127,
			255,
			511,
			1023,
			2047,
			4095,
			8191,
			16383,
			32767,
			65535
		};

		// Token: 0x040015EA RID: 5610
		private const int Z_OK = 0;

		// Token: 0x040015EB RID: 5611
		private const int Z_STREAM_END = 1;

		// Token: 0x040015EC RID: 5612
		private const int Z_NEED_DICT = 2;

		// Token: 0x040015ED RID: 5613
		private const int Z_ERRNO = -1;

		// Token: 0x040015EE RID: 5614
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x040015EF RID: 5615
		private const int Z_DATA_ERROR = -3;

		// Token: 0x040015F0 RID: 5616
		private const int Z_MEM_ERROR = -4;

		// Token: 0x040015F1 RID: 5617
		private const int Z_BUF_ERROR = -5;

		// Token: 0x040015F2 RID: 5618
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x040015F3 RID: 5619
		private const int START = 0;

		// Token: 0x040015F4 RID: 5620
		private const int LEN = 1;

		// Token: 0x040015F5 RID: 5621
		private const int LENEXT = 2;

		// Token: 0x040015F6 RID: 5622
		private const int DIST = 3;

		// Token: 0x040015F7 RID: 5623
		private const int DISTEXT = 4;

		// Token: 0x040015F8 RID: 5624
		private const int COPY = 5;

		// Token: 0x040015F9 RID: 5625
		private const int LIT = 6;

		// Token: 0x040015FA RID: 5626
		private const int WASH = 7;

		// Token: 0x040015FB RID: 5627
		private const int END = 8;

		// Token: 0x040015FC RID: 5628
		private const int BADCODE = 9;

		// Token: 0x040015FD RID: 5629
		private int mode;

		// Token: 0x040015FE RID: 5630
		private int len;

		// Token: 0x040015FF RID: 5631
		private int[] tree;

		// Token: 0x04001600 RID: 5632
		private int tree_index;

		// Token: 0x04001601 RID: 5633
		private int need;

		// Token: 0x04001602 RID: 5634
		private int lit;

		// Token: 0x04001603 RID: 5635
		private int get;

		// Token: 0x04001604 RID: 5636
		private int dist;

		// Token: 0x04001605 RID: 5637
		private byte lbits;

		// Token: 0x04001606 RID: 5638
		private byte dbits;

		// Token: 0x04001607 RID: 5639
		private int[] ltree;

		// Token: 0x04001608 RID: 5640
		private int ltree_index;

		// Token: 0x04001609 RID: 5641
		private int[] dtree;

		// Token: 0x0400160A RID: 5642
		private int dtree_index;
	}
}
