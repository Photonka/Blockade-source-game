using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000255 RID: 597
	internal sealed class InfBlocks
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x000B4A90 File Offset: 0x000B2C90
		internal InfBlocks(ZStream z, object checkfn, int w)
		{
			this.hufts = new int[4320];
			this.window = new byte[w];
			this.end = w;
			this.checkfn = checkfn;
			this.mode = 0;
			this.reset(z, null);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x000B4B0C File Offset: 0x000B2D0C
		internal void reset(ZStream z, long[] c)
		{
			if (c != null)
			{
				c[0] = this.check;
			}
			if (this.mode != 4)
			{
				int num = this.mode;
			}
			if (this.mode == 6)
			{
				this.codes.free(z);
			}
			this.mode = 0;
			this.bitk = 0;
			this.bitb = 0;
			this.read = (this.write = 0);
			if (this.checkfn != null)
			{
				z.adler = (this.check = z._adler.adler32(0L, null, 0, 0));
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x000B4B98 File Offset: 0x000B2D98
		internal int proc(ZStream z, int r)
		{
			int num = z.next_in_index;
			int num2 = z.avail_in;
			int num3 = this.bitb;
			int i = this.bitk;
			int num4 = this.write;
			int num5 = (num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4);
			int num6;
			for (;;)
			{
				switch (this.mode)
				{
				case 0:
					while (i < 3)
					{
						if (num2 == 0)
						{
							goto IL_8C;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					num6 = (num3 & 7);
					this.last = (num6 & 1);
					switch (num6 >> 1)
					{
					case 0:
						num3 >>= 3;
						i -= 3;
						num6 = (i & 7);
						num3 >>= num6;
						i -= num6;
						this.mode = 1;
						continue;
					case 1:
					{
						int[] array = new int[1];
						int[] array2 = new int[1];
						int[][] array3 = new int[1][];
						int[][] array4 = new int[1][];
						InfTree.inflate_trees_fixed(array, array2, array3, array4, z);
						this.codes.init(array[0], array2[0], array3[0], 0, array4[0], 0, z);
						num3 >>= 3;
						i -= 3;
						this.mode = 6;
						continue;
					}
					case 2:
						num3 >>= 3;
						i -= 3;
						this.mode = 3;
						continue;
					case 3:
						goto IL_1BE;
					default:
						continue;
					}
					break;
				case 1:
					while (i < 32)
					{
						if (num2 == 0)
						{
							goto IL_22A;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					if ((~num3 >> 16 & 65535) != (num3 & 65535))
					{
						goto Block_8;
					}
					this.left = (num3 & 65535);
					i = (num3 = 0);
					this.mode = ((this.left != 0) ? 2 : ((this.last != 0) ? 7 : 0));
					continue;
				case 2:
					if (num2 == 0)
					{
						goto Block_11;
					}
					if (num5 == 0)
					{
						if (num4 == this.end && this.read != 0)
						{
							num4 = 0;
							num5 = ((num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4));
						}
						if (num5 == 0)
						{
							this.write = num4;
							r = this.inflate_flush(z, r);
							num4 = this.write;
							num5 = ((num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4));
							if (num4 == this.end && this.read != 0)
							{
								num4 = 0;
								num5 = ((num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4));
							}
							if (num5 == 0)
							{
								goto Block_21;
							}
						}
					}
					r = 0;
					num6 = this.left;
					if (num6 > num2)
					{
						num6 = num2;
					}
					if (num6 > num5)
					{
						num6 = num5;
					}
					Array.Copy(z.next_in, num, this.window, num4, num6);
					num += num6;
					num2 -= num6;
					num4 += num6;
					num5 -= num6;
					if ((this.left -= num6) == 0)
					{
						this.mode = ((this.last != 0) ? 7 : 0);
						continue;
					}
					continue;
				case 3:
					while (i < 14)
					{
						if (num2 == 0)
						{
							goto IL_4FE;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					num6 = (this.table = (num3 & 16383));
					if ((num6 & 31) > 29 || (num6 >> 5 & 31) > 29)
					{
						goto IL_58C;
					}
					num6 = 258 + (num6 & 31) + (num6 >> 5 & 31);
					if (this.blens == null || this.blens.Length < num6)
					{
						this.blens = new int[num6];
					}
					else
					{
						for (int j = 0; j < num6; j++)
						{
							this.blens[j] = 0;
						}
					}
					num3 >>= 14;
					i -= 14;
					this.index = 0;
					this.mode = 4;
					goto IL_6F2;
				case 4:
					goto IL_6F2;
				case 5:
					goto IL_7CD;
				case 6:
					goto IL_B74;
				case 7:
					goto IL_C3D;
				case 8:
					goto IL_CD2;
				case 9:
					goto IL_D19;
				}
				break;
				IL_6F2:
				while (this.index < 4 + (this.table >> 10))
				{
					while (i < 3)
					{
						if (num2 == 0)
						{
							goto IL_65A;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					int[] array5 = this.blens;
					int[] array6 = InfBlocks.border;
					int num7 = this.index;
					this.index = num7 + 1;
					array5[array6[num7]] = (num3 & 7);
					num3 >>= 3;
					i -= 3;
				}
				while (this.index < 19)
				{
					int[] array7 = this.blens;
					int[] array8 = InfBlocks.border;
					int num7 = this.index;
					this.index = num7 + 1;
					array7[array8[num7]] = 0;
				}
				this.bb[0] = 7;
				num6 = this.inftree.inflate_trees_bits(this.blens, this.bb, this.tb, this.hufts, z);
				if (num6 != 0)
				{
					goto Block_34;
				}
				this.index = 0;
				this.mode = 5;
				for (;;)
				{
					IL_7CD:
					num6 = this.table;
					if (this.index >= 258 + (num6 & 31) + (num6 >> 5 & 31))
					{
						break;
					}
					num6 = this.bb[0];
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_804;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					int num8 = this.tb[0];
					num6 = this.hufts[(this.tb[0] + (num3 & InfBlocks.inflate_mask[num6])) * 3 + 1];
					int num9 = this.hufts[(this.tb[0] + (num3 & InfBlocks.inflate_mask[num6])) * 3 + 2];
					if (num9 < 16)
					{
						num3 >>= num6;
						i -= num6;
						int[] array9 = this.blens;
						int num7 = this.index;
						this.index = num7 + 1;
						array9[num7] = num9;
					}
					else
					{
						int num10 = (num9 == 18) ? 7 : (num9 - 14);
						int num11 = (num9 == 18) ? 11 : 3;
						while (i < num6 + num10)
						{
							if (num2 == 0)
							{
								goto IL_913;
							}
							r = 0;
							num2--;
							num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
							i += 8;
						}
						num3 >>= num6;
						i -= num6;
						num11 += (num3 & InfBlocks.inflate_mask[num10]);
						num3 >>= num10;
						i -= num10;
						num10 = this.index;
						num6 = this.table;
						if (num10 + num11 > 258 + (num6 & 31) + (num6 >> 5 & 31) || (num9 == 16 && num10 < 1))
						{
							goto IL_9DB;
						}
						num9 = ((num9 == 16) ? this.blens[num10 - 1] : 0);
						do
						{
							this.blens[num10++] = num9;
						}
						while (--num11 != 0);
						this.index = num10;
					}
				}
				this.tb[0] = -1;
				int[] array10 = new int[1];
				int[] array11 = new int[1];
				int[] array12 = new int[1];
				int[] array13 = new int[1];
				array10[0] = 9;
				array11[0] = 6;
				num6 = this.table;
				num6 = this.inftree.inflate_trees_dynamic(257 + (num6 & 31), 1 + (num6 >> 5 & 31), this.blens, array10, array11, array12, array13, this.hufts, z);
				if (num6 != 0)
				{
					goto Block_48;
				}
				this.codes.init(array10[0], array11[0], this.hufts, array12[0], this.hufts, array13[0], z);
				this.mode = 6;
				IL_B74:
				this.bitb = num3;
				this.bitk = i;
				z.avail_in = num2;
				z.total_in += (long)(num - z.next_in_index);
				z.next_in_index = num;
				this.write = num4;
				if ((r = this.codes.proc(this, z, r)) != 1)
				{
					goto Block_50;
				}
				r = 0;
				this.codes.free(z);
				num = z.next_in_index;
				num2 = z.avail_in;
				num3 = this.bitb;
				i = this.bitk;
				num4 = this.write;
				num5 = ((num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4));
				if (this.last != 0)
				{
					goto IL_C36;
				}
				this.mode = 0;
			}
			r = -2;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_8C:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_1BE:
			num3 >>= 3;
			i -= 3;
			this.mode = 9;
			z.msg = "invalid block type";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_22A:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_8:
			this.mode = 9;
			z.msg = "invalid stored block lengths";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_11:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_21:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_4FE:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_58C:
			this.mode = 9;
			z.msg = "too many length or distance symbols";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_65A:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_34:
			r = num6;
			if (r == -3)
			{
				this.blens = null;
				this.mode = 9;
			}
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_804:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_913:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_9DB:
			this.blens = null;
			this.mode = 9;
			z.msg = "invalid bit length repeat";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_48:
			if (num6 == -3)
			{
				this.blens = null;
				this.mode = 9;
			}
			r = num6;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_50:
			return this.inflate_flush(z, r);
			IL_C36:
			this.mode = 7;
			IL_C3D:
			this.write = num4;
			r = this.inflate_flush(z, r);
			num4 = this.write;
			int num12 = (num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4);
			if (this.read != this.write)
			{
				this.bitb = num3;
				this.bitk = i;
				z.avail_in = num2;
				z.total_in += (long)(num - z.next_in_index);
				z.next_in_index = num;
				this.write = num4;
				return this.inflate_flush(z, r);
			}
			this.mode = 8;
			IL_CD2:
			r = 1;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_D19:
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x000B594D File Offset: 0x000B3B4D
		internal void free(ZStream z)
		{
			this.reset(z, null);
			this.window = null;
			this.hufts = null;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x000B5968 File Offset: 0x000B3B68
		internal void set_dictionary(byte[] d, int start, int n)
		{
			Array.Copy(d, start, this.window, 0, n);
			this.write = n;
			this.read = n;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x000B5994 File Offset: 0x000B3B94
		internal int sync_point()
		{
			if (this.mode != 1)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x000B59A4 File Offset: 0x000B3BA4
		internal int inflate_flush(ZStream z, int r)
		{
			int num = z.next_out_index;
			int num2 = this.read;
			int num3 = ((num2 <= this.write) ? this.write : this.end) - num2;
			if (num3 > z.avail_out)
			{
				num3 = z.avail_out;
			}
			if (num3 != 0 && r == -5)
			{
				r = 0;
			}
			z.avail_out -= num3;
			z.total_out += (long)num3;
			if (this.checkfn != null)
			{
				z.adler = (this.check = z._adler.adler32(this.check, this.window, num2, num3));
			}
			Array.Copy(this.window, num2, z.next_out, num, num3);
			num += num3;
			num2 += num3;
			if (num2 == this.end)
			{
				num2 = 0;
				if (this.write == this.end)
				{
					this.write = 0;
				}
				num3 = this.write - num2;
				if (num3 > z.avail_out)
				{
					num3 = z.avail_out;
				}
				if (num3 != 0 && r == -5)
				{
					r = 0;
				}
				z.avail_out -= num3;
				z.total_out += (long)num3;
				if (this.checkfn != null)
				{
					z.adler = (this.check = z._adler.adler32(this.check, this.window, num2, num3));
				}
				Array.Copy(this.window, num2, z.next_out, num, num3);
				num += num3;
				num2 += num3;
			}
			z.next_out_index = num;
			this.read = num2;
			return r;
		}

		// Token: 0x040015C0 RID: 5568
		private const int MANY = 1440;

		// Token: 0x040015C1 RID: 5569
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

		// Token: 0x040015C2 RID: 5570
		private static readonly int[] border = new int[]
		{
			16,
			17,
			18,
			0,
			8,
			7,
			9,
			6,
			10,
			5,
			11,
			4,
			12,
			3,
			13,
			2,
			14,
			1,
			15
		};

		// Token: 0x040015C3 RID: 5571
		private const int Z_OK = 0;

		// Token: 0x040015C4 RID: 5572
		private const int Z_STREAM_END = 1;

		// Token: 0x040015C5 RID: 5573
		private const int Z_NEED_DICT = 2;

		// Token: 0x040015C6 RID: 5574
		private const int Z_ERRNO = -1;

		// Token: 0x040015C7 RID: 5575
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x040015C8 RID: 5576
		private const int Z_DATA_ERROR = -3;

		// Token: 0x040015C9 RID: 5577
		private const int Z_MEM_ERROR = -4;

		// Token: 0x040015CA RID: 5578
		private const int Z_BUF_ERROR = -5;

		// Token: 0x040015CB RID: 5579
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x040015CC RID: 5580
		private const int TYPE = 0;

		// Token: 0x040015CD RID: 5581
		private const int LENS = 1;

		// Token: 0x040015CE RID: 5582
		private const int STORED = 2;

		// Token: 0x040015CF RID: 5583
		private const int TABLE = 3;

		// Token: 0x040015D0 RID: 5584
		private const int BTREE = 4;

		// Token: 0x040015D1 RID: 5585
		private const int DTREE = 5;

		// Token: 0x040015D2 RID: 5586
		private const int CODES = 6;

		// Token: 0x040015D3 RID: 5587
		private const int DRY = 7;

		// Token: 0x040015D4 RID: 5588
		private const int DONE = 8;

		// Token: 0x040015D5 RID: 5589
		private const int BAD = 9;

		// Token: 0x040015D6 RID: 5590
		internal int mode;

		// Token: 0x040015D7 RID: 5591
		internal int left;

		// Token: 0x040015D8 RID: 5592
		internal int table;

		// Token: 0x040015D9 RID: 5593
		internal int index;

		// Token: 0x040015DA RID: 5594
		internal int[] blens;

		// Token: 0x040015DB RID: 5595
		internal int[] bb = new int[1];

		// Token: 0x040015DC RID: 5596
		internal int[] tb = new int[1];

		// Token: 0x040015DD RID: 5597
		internal InfCodes codes = new InfCodes();

		// Token: 0x040015DE RID: 5598
		private int last;

		// Token: 0x040015DF RID: 5599
		internal int bitk;

		// Token: 0x040015E0 RID: 5600
		internal int bitb;

		// Token: 0x040015E1 RID: 5601
		internal int[] hufts;

		// Token: 0x040015E2 RID: 5602
		internal byte[] window;

		// Token: 0x040015E3 RID: 5603
		internal int end;

		// Token: 0x040015E4 RID: 5604
		internal int read;

		// Token: 0x040015E5 RID: 5605
		internal int write;

		// Token: 0x040015E6 RID: 5606
		internal object checkfn;

		// Token: 0x040015E7 RID: 5607
		internal long check;

		// Token: 0x040015E8 RID: 5608
		internal InfTree inftree = new InfTree();
	}
}
