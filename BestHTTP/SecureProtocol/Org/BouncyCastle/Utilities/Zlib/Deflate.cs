using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000254 RID: 596
	public sealed class Deflate
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x000B2820 File Offset: 0x000B0A20
		static Deflate()
		{
			Deflate.config_table = new Deflate.Config[10];
			Deflate.config_table[0] = new Deflate.Config(0, 0, 0, 0, 0);
			Deflate.config_table[1] = new Deflate.Config(4, 4, 8, 4, 1);
			Deflate.config_table[2] = new Deflate.Config(4, 5, 16, 8, 1);
			Deflate.config_table[3] = new Deflate.Config(4, 6, 32, 32, 1);
			Deflate.config_table[4] = new Deflate.Config(4, 4, 16, 16, 2);
			Deflate.config_table[5] = new Deflate.Config(8, 16, 32, 32, 2);
			Deflate.config_table[6] = new Deflate.Config(8, 16, 128, 128, 2);
			Deflate.config_table[7] = new Deflate.Config(8, 32, 128, 256, 2);
			Deflate.config_table[8] = new Deflate.Config(32, 128, 258, 1024, 2);
			Deflate.config_table[9] = new Deflate.Config(32, 258, 258, 4096, 2);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000B2978 File Offset: 0x000B0B78
		internal Deflate()
		{
			this.dyn_ltree = new short[1146];
			this.dyn_dtree = new short[122];
			this.bl_tree = new short[78];
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000B2A04 File Offset: 0x000B0C04
		internal void lm_init()
		{
			this.window_size = 2 * this.w_size;
			this.head[this.hash_size - 1] = 0;
			for (int i = 0; i < this.hash_size - 1; i++)
			{
				this.head[i] = 0;
			}
			this.max_lazy_match = Deflate.config_table[this.level].max_lazy;
			this.good_match = Deflate.config_table[this.level].good_length;
			this.nice_match = Deflate.config_table[this.level].nice_length;
			this.max_chain_length = Deflate.config_table[this.level].max_chain;
			this.strstart = 0;
			this.block_start = 0;
			this.lookahead = 0;
			this.match_length = (this.prev_length = 2);
			this.match_available = 0;
			this.ins_h = 0;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000B2ADC File Offset: 0x000B0CDC
		internal void tr_init()
		{
			this.l_desc.dyn_tree = this.dyn_ltree;
			this.l_desc.stat_desc = StaticTree.static_l_desc;
			this.d_desc.dyn_tree = this.dyn_dtree;
			this.d_desc.stat_desc = StaticTree.static_d_desc;
			this.bl_desc.dyn_tree = this.bl_tree;
			this.bl_desc.stat_desc = StaticTree.static_bl_desc;
			this.bi_buf = 0U;
			this.bi_valid = 0;
			this.last_eob_len = 8;
			this.init_block();
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x000B2B68 File Offset: 0x000B0D68
		internal void init_block()
		{
			for (int i = 0; i < 286; i++)
			{
				this.dyn_ltree[i * 2] = 0;
			}
			for (int j = 0; j < 30; j++)
			{
				this.dyn_dtree[j * 2] = 0;
			}
			for (int k = 0; k < 19; k++)
			{
				this.bl_tree[k * 2] = 0;
			}
			this.dyn_ltree[512] = 1;
			this.opt_len = (this.static_len = 0);
			this.last_lit = (this.matches = 0);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000B2BF0 File Offset: 0x000B0DF0
		internal void pqdownheap(short[] tree, int k)
		{
			int num = this.heap[k];
			for (int i = k << 1; i <= this.heap_len; i <<= 1)
			{
				if (i < this.heap_len && Deflate.smaller(tree, this.heap[i + 1], this.heap[i], this.depth))
				{
					i++;
				}
				if (Deflate.smaller(tree, num, this.heap[i], this.depth))
				{
					break;
				}
				this.heap[k] = this.heap[i];
				k = i;
			}
			this.heap[k] = num;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000B2C7C File Offset: 0x000B0E7C
		internal static bool smaller(short[] tree, int n, int m, byte[] depth)
		{
			short num = tree[n * 2];
			short num2 = tree[m * 2];
			return num < num2 || (num == num2 && depth[n] <= depth[m]);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000B2CAC File Offset: 0x000B0EAC
		internal void scan_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			tree[(max_code + 1) * 2 + 1] = -1;
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				if (++num3 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						short[] array = this.bl_tree;
						int num7 = num6 * 2;
						array[num7] += (short)num3;
					}
					else if (num6 != 0)
					{
						if (num6 != num)
						{
							short[] array2 = this.bl_tree;
							int num8 = num6 * 2;
							array2[num8] += 1;
						}
						short[] array3 = this.bl_tree;
						int num9 = 32;
						array3[num9] += 1;
					}
					else if (num3 <= 10)
					{
						short[] array4 = this.bl_tree;
						int num10 = 34;
						array4[num10] += 1;
					}
					else
					{
						short[] array5 = this.bl_tree;
						int num11 = 36;
						array5[num11] += 1;
					}
					num3 = 0;
					num = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 == num2)
					{
						num4 = 6;
						num5 = 3;
					}
					else
					{
						num4 = 7;
						num5 = 4;
					}
				}
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000B2DB4 File Offset: 0x000B0FB4
		internal int build_bl_tree()
		{
			this.scan_tree(this.dyn_ltree, this.l_desc.max_code);
			this.scan_tree(this.dyn_dtree, this.d_desc.max_code);
			this.bl_desc.build_tree(this);
			int num = 18;
			while (num >= 3 && this.bl_tree[(int)(ZTree.bl_order[num] * 2 + 1)] == 0)
			{
				num--;
			}
			this.opt_len += 3 * (num + 1) + 5 + 5 + 4;
			return num;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000B2E38 File Offset: 0x000B1038
		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{
			this.send_bits(lcodes - 257, 5);
			this.send_bits(dcodes - 1, 5);
			this.send_bits(blcodes - 4, 4);
			for (int i = 0; i < blcodes; i++)
			{
				this.send_bits((int)this.bl_tree[(int)(ZTree.bl_order[i] * 2 + 1)], 3);
			}
			this.send_tree(this.dyn_ltree, lcodes - 1);
			this.send_tree(this.dyn_dtree, dcodes - 1);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000B2EAC File Offset: 0x000B10AC
		internal void send_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				if (++num3 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						do
						{
							this.send_code(num6, this.bl_tree);
						}
						while (--num3 != 0);
					}
					else if (num6 != 0)
					{
						if (num6 != num)
						{
							this.send_code(num6, this.bl_tree);
							num3--;
						}
						this.send_code(16, this.bl_tree);
						this.send_bits(num3 - 3, 2);
					}
					else if (num3 <= 10)
					{
						this.send_code(17, this.bl_tree);
						this.send_bits(num3 - 3, 3);
					}
					else
					{
						this.send_code(18, this.bl_tree);
						this.send_bits(num3 - 11, 7);
					}
					num3 = 0;
					num = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 == num2)
					{
						num4 = 6;
						num5 = 3;
					}
					else
					{
						num4 = 7;
						num5 = 4;
					}
				}
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000B2FB9 File Offset: 0x000B11B9
		internal void put_byte(byte[] p, int start, int len)
		{
			Array.Copy(p, start, this.pending_buf, this.pending, len);
			this.pending += len;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000B2FE0 File Offset: 0x000B11E0
		internal void put_byte(byte c)
		{
			byte[] array = this.pending_buf;
			int num = this.pending;
			this.pending = num + 1;
			array[num] = c;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000B3008 File Offset: 0x000B1208
		internal void put_short(int w)
		{
			byte[] array = this.pending_buf;
			int num = this.pending;
			this.pending = num + 1;
			array[num] = (byte)w;
			byte[] array2 = this.pending_buf;
			num = this.pending;
			this.pending = num + 1;
			array2[num] = (byte)(w >> 8);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000B304C File Offset: 0x000B124C
		internal void putShortMSB(int b)
		{
			byte[] array = this.pending_buf;
			int num = this.pending;
			this.pending = num + 1;
			array[num] = (byte)(b >> 8);
			byte[] array2 = this.pending_buf;
			num = this.pending;
			this.pending = num + 1;
			array2[num] = (byte)b;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000B3090 File Offset: 0x000B1290
		internal void send_code(int c, short[] tree)
		{
			int num = c * 2;
			this.send_bits((int)tree[num] & 65535, (int)tree[num + 1] & 65535);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000B30BC File Offset: 0x000B12BC
		internal void send_bits(int val, int length)
		{
			if (this.bi_valid > 16 - length)
			{
				this.bi_buf |= (uint)((uint)val << this.bi_valid);
				byte[] array = this.pending_buf;
				int num = this.pending;
				this.pending = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending_buf;
				num = this.pending;
				this.pending = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
				this.bi_buf = (uint)val >> 16 - this.bi_valid;
				this.bi_valid += length - 16;
				return;
			}
			this.bi_buf |= (uint)((uint)val << this.bi_valid);
			this.bi_valid += length;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000B317C File Offset: 0x000B137C
		internal void _tr_align()
		{
			this.send_bits(2, 3);
			this.send_code(256, StaticTree.static_ltree);
			this.bi_flush();
			if (1 + this.last_eob_len + 10 - this.bi_valid < 9)
			{
				this.send_bits(2, 3);
				this.send_code(256, StaticTree.static_ltree);
				this.bi_flush();
			}
			this.last_eob_len = 7;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000B31E4 File Offset: 0x000B13E4
		internal bool _tr_tally(int dist, int lc)
		{
			this.pending_buf[this.d_buf + this.last_lit * 2] = (byte)(dist >> 8);
			this.pending_buf[this.d_buf + this.last_lit * 2 + 1] = (byte)dist;
			this.pending_buf[this.l_buf + this.last_lit] = (byte)lc;
			this.last_lit++;
			if (dist == 0)
			{
				short[] array = this.dyn_ltree;
				int num = lc * 2;
				array[num] += 1;
			}
			else
			{
				this.matches++;
				dist--;
				short[] array2 = this.dyn_ltree;
				int num2 = ((int)ZTree._length_code[lc] + 256 + 1) * 2;
				array2[num2] += 1;
				short[] array3 = this.dyn_dtree;
				int num3 = ZTree.d_code(dist) * 2;
				array3[num3] += 1;
			}
			if ((this.last_lit & 8191) == 0 && this.level > 2)
			{
				int num4 = this.last_lit * 8;
				int num5 = this.strstart - this.block_start;
				for (int i = 0; i < 30; i++)
				{
					num4 += (int)((long)this.dyn_dtree[i * 2] * (5L + (long)ZTree.extra_dbits[i]));
				}
				num4 >>= 3;
				if (this.matches < this.last_lit / 2 && num4 < num5 / 2)
				{
					return true;
				}
			}
			return this.last_lit == this.lit_bufsize - 1;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000B3334 File Offset: 0x000B1534
		internal void compress_block(short[] ltree, short[] dtree)
		{
			int num = 0;
			if (this.last_lit != 0)
			{
				do
				{
					int num2 = ((int)this.pending_buf[this.d_buf + num * 2] << 8 & 65280) | (int)(this.pending_buf[this.d_buf + num * 2 + 1] & byte.MaxValue);
					int num3 = (int)(this.pending_buf[this.l_buf + num] & byte.MaxValue);
					num++;
					if (num2 == 0)
					{
						this.send_code(num3, ltree);
					}
					else
					{
						int num4 = (int)ZTree._length_code[num3];
						this.send_code(num4 + 256 + 1, ltree);
						int num5 = ZTree.extra_lbits[num4];
						if (num5 != 0)
						{
							num3 -= ZTree.base_length[num4];
							this.send_bits(num3, num5);
						}
						num2--;
						num4 = ZTree.d_code(num2);
						this.send_code(num4, dtree);
						num5 = ZTree.extra_dbits[num4];
						if (num5 != 0)
						{
							num2 -= ZTree.base_dist[num4];
							this.send_bits(num2, num5);
						}
					}
				}
				while (num < this.last_lit);
			}
			this.send_code(256, ltree);
			this.last_eob_len = (int)ltree[513];
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000B343C File Offset: 0x000B163C
		internal void set_data_type()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < 7)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < 128)
			{
				num += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < 256)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			this.data_type = ((num2 > num >> 2) ? 0 : 1);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000B34B0 File Offset: 0x000B16B0
		internal void bi_flush()
		{
			if (this.bi_valid == 16)
			{
				byte[] array = this.pending_buf;
				int num = this.pending;
				this.pending = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending_buf;
				num = this.pending;
				this.pending = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
				this.bi_buf = 0U;
				this.bi_valid = 0;
				return;
			}
			if (this.bi_valid >= 8)
			{
				byte[] array3 = this.pending_buf;
				int num = this.pending;
				this.pending = num + 1;
				array3[num] = (byte)this.bi_buf;
				this.bi_buf >>= 8;
				this.bi_buf &= 255U;
				this.bi_valid -= 8;
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000B356C File Offset: 0x000B176C
		internal void bi_windup()
		{
			if (this.bi_valid > 8)
			{
				byte[] array = this.pending_buf;
				int num = this.pending;
				this.pending = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending_buf;
				num = this.pending;
				this.pending = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
			}
			else if (this.bi_valid > 0)
			{
				byte[] array3 = this.pending_buf;
				int num = this.pending;
				this.pending = num + 1;
				array3[num] = (byte)this.bi_buf;
			}
			this.bi_buf = 0U;
			this.bi_valid = 0;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000B35FA File Offset: 0x000B17FA
		internal void copy_block(int buf, int len, bool header)
		{
			this.bi_windup();
			this.last_eob_len = 8;
			if (header)
			{
				this.put_short((int)((short)len));
				this.put_short((int)((short)(~(short)len)));
			}
			this.put_byte(this.window, buf, len);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000B362B File Offset: 0x000B182B
		internal void flush_block_only(bool eof)
		{
			this._tr_flush_block((this.block_start >= 0) ? this.block_start : -1, this.strstart - this.block_start, eof);
			this.block_start = this.strstart;
			this.strm.flush_pending();
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000B366C File Offset: 0x000B186C
		internal int deflate_stored(int flush)
		{
			int num = 65535;
			if (num > this.pending_buf_size - 5)
			{
				num = this.pending_buf_size - 5;
			}
			for (;;)
			{
				if (this.lookahead <= 1)
				{
					this.fill_window();
					if (this.lookahead == 0 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_D7;
					}
				}
				this.strstart += this.lookahead;
				this.lookahead = 0;
				int num2 = this.block_start + num;
				if (this.strstart == 0 || this.strstart >= num2)
				{
					this.lookahead = this.strstart - num2;
					this.strstart = num2;
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
				if (this.strstart - this.block_start >= this.w_size - 262)
				{
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
			}
			return 0;
			IL_D7:
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush != 4)
				{
					return 0;
				}
				return 2;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000B3776 File Offset: 0x000B1976
		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{
			this.send_bits(eof ? 1 : 0, 3);
			this.copy_block(buf, stored_len, true);
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000B3790 File Offset: 0x000B1990
		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{
			int num = 0;
			int num2;
			int num3;
			if (this.level > 0)
			{
				if (this.data_type == 2)
				{
					this.set_data_type();
				}
				this.l_desc.build_tree(this);
				this.d_desc.build_tree(this);
				num = this.build_bl_tree();
				num2 = this.opt_len + 3 + 7 >> 3;
				num3 = this.static_len + 3 + 7 >> 3;
				if (num3 <= num2)
				{
					num2 = num3;
				}
			}
			else
			{
				num3 = (num2 = stored_len + 5);
			}
			if (stored_len + 4 <= num2 && buf != -1)
			{
				this._tr_stored_block(buf, stored_len, eof);
			}
			else if (num3 == num2)
			{
				this.send_bits(2 + (eof ? 1 : 0), 3);
				this.compress_block(StaticTree.static_ltree, StaticTree.static_dtree);
			}
			else
			{
				this.send_bits(4 + (eof ? 1 : 0), 3);
				this.send_all_trees(this.l_desc.max_code + 1, this.d_desc.max_code + 1, num + 1);
				this.compress_block(this.dyn_ltree, this.dyn_dtree);
			}
			this.init_block();
			if (eof)
			{
				this.bi_windup();
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000B3890 File Offset: 0x000B1A90
		internal void fill_window()
		{
			for (;;)
			{
				int num = this.window_size - this.lookahead - this.strstart;
				int num2;
				if (num == 0 && this.strstart == 0 && this.lookahead == 0)
				{
					num = this.w_size;
				}
				else if (num == -1)
				{
					num--;
				}
				else if (this.strstart >= this.w_size + this.w_size - 262)
				{
					Array.Copy(this.window, this.w_size, this.window, 0, this.w_size);
					this.match_start -= this.w_size;
					this.strstart -= this.w_size;
					this.block_start -= this.w_size;
					num2 = this.hash_size;
					int num3 = num2;
					do
					{
						int num4 = (int)this.head[--num3] & 65535;
						this.head[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
					}
					while (--num2 != 0);
					num2 = this.w_size;
					num3 = num2;
					do
					{
						int num4 = (int)this.prev[--num3] & 65535;
						this.prev[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
					}
					while (--num2 != 0);
					num += this.w_size;
				}
				if (this.strm.avail_in == 0)
				{
					break;
				}
				num2 = this.strm.read_buf(this.window, this.strstart + this.lookahead, num);
				this.lookahead += num2;
				if (this.lookahead >= 3)
				{
					this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask);
				}
				if (this.lookahead >= 262 || this.strm.avail_in == 0)
				{
					return;
				}
			}
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000B3A8C File Offset: 0x000B1C8C
		internal int deflate_fast(int flush)
		{
			int num = 0;
			for (;;)
			{
				if (this.lookahead < 262)
				{
					this.fill_window();
					if (this.lookahead < 262 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_2C4;
					}
				}
				if (this.lookahead >= 3)
				{
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
					num = ((int)this.head[this.ins_h] & 65535);
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				if ((long)num != 0L && (this.strstart - num & 65535) <= this.w_size - 262 && this.strategy != 2)
				{
					this.match_length = this.longest_match(num);
				}
				bool flag;
				if (this.match_length >= 3)
				{
					flag = this._tr_tally(this.strstart - this.match_start, this.match_length - 3);
					this.lookahead -= this.match_length;
					if (this.match_length <= this.max_lazy_match && this.lookahead >= 3)
					{
						this.match_length--;
						int num2;
						do
						{
							this.strstart++;
							this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
							num = ((int)this.head[this.ins_h] & 65535);
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
							num2 = this.match_length - 1;
							this.match_length = num2;
						}
						while (num2 != 0);
						this.strstart++;
					}
					else
					{
						this.strstart += this.match_length;
						this.match_length = 0;
						this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
						this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask);
					}
				}
				else
				{
					flag = this._tr_tally(0, (int)(this.window[this.strstart] & byte.MaxValue));
					this.lookahead--;
					this.strstart++;
				}
				if (flag)
				{
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
			}
			return 0;
			IL_2C4:
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush == 4)
				{
					return 2;
				}
				return 0;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000B3D84 File Offset: 0x000B1F84
		internal int deflate_slow(int flush)
		{
			int num = 0;
			for (;;)
			{
				if (this.lookahead < 262)
				{
					this.fill_window();
					if (this.lookahead < 262 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_323;
					}
				}
				if (this.lookahead >= 3)
				{
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
					num = ((int)this.head[this.ins_h] & 65535);
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				this.prev_length = this.match_length;
				this.prev_match = this.match_start;
				this.match_length = 2;
				if (num != 0 && this.prev_length < this.max_lazy_match && (this.strstart - num & 65535) <= this.w_size - 262)
				{
					if (this.strategy != 2)
					{
						this.match_length = this.longest_match(num);
					}
					if (this.match_length <= 5 && (this.strategy == 1 || (this.match_length == 3 && this.strstart - this.match_start > 4096)))
					{
						this.match_length = 2;
					}
				}
				if (this.prev_length >= 3 && this.match_length <= this.prev_length)
				{
					int num2 = this.strstart + this.lookahead - 3;
					bool flag = this._tr_tally(this.strstart - 1 - this.prev_match, this.prev_length - 3);
					this.lookahead -= this.prev_length - 1;
					this.prev_length -= 2;
					int num3;
					do
					{
						num3 = this.strstart + 1;
						this.strstart = num3;
						if (num3 <= num2)
						{
							this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
							num = ((int)this.head[this.ins_h] & 65535);
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
						}
						num3 = this.prev_length - 1;
						this.prev_length = num3;
					}
					while (num3 != 0);
					this.match_available = 0;
					this.match_length = 2;
					this.strstart++;
					if (flag)
					{
						this.flush_block_only(false);
						if (this.strm.avail_out == 0)
						{
							return 0;
						}
					}
				}
				else if (this.match_available != 0)
				{
					bool flag = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
					if (flag)
					{
						this.flush_block_only(false);
					}
					this.strstart++;
					this.lookahead--;
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
				else
				{
					this.match_available = 1;
					this.strstart++;
					this.lookahead--;
				}
			}
			return 0;
			IL_323:
			if (this.match_available != 0)
			{
				bool flag = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
				this.match_available = 0;
			}
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush == 4)
				{
					return 2;
				}
				return 0;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000B4108 File Offset: 0x000B2308
		internal int longest_match(int cur_match)
		{
			int num = this.max_chain_length;
			int num2 = this.strstart;
			int num3 = this.prev_length;
			int num4 = (this.strstart > this.w_size - 262) ? (this.strstart - (this.w_size - 262)) : 0;
			int num5 = this.nice_match;
			int num6 = this.w_mask;
			int num7 = this.strstart + 258;
			byte b = this.window[num2 + num3 - 1];
			byte b2 = this.window[num2 + num3];
			if (this.prev_length >= this.good_match)
			{
				num >>= 2;
			}
			if (num5 > this.lookahead)
			{
				num5 = this.lookahead;
			}
			do
			{
				int num8 = cur_match;
				if (this.window[num8 + num3] == b2 && this.window[num8 + num3 - 1] == b && this.window[num8] == this.window[num2] && this.window[++num8] == this.window[num2 + 1])
				{
					num2 += 2;
					num8++;
					while (this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && num2 < num7)
					{
					}
					int num9 = 258 - (num7 - num2);
					num2 = num7 - 258;
					if (num9 > num3)
					{
						this.match_start = cur_match;
						num3 = num9;
						if (num9 >= num5)
						{
							break;
						}
						b = this.window[num2 + num3 - 1];
						b2 = this.window[num2 + num3];
					}
				}
			}
			while ((cur_match = ((int)this.prev[cur_match & num6] & 65535)) > num4 && --num != 0);
			if (num3 <= this.lookahead)
			{
				return num3;
			}
			return this.lookahead;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000B436F File Offset: 0x000B256F
		internal int deflateInit(ZStream strm, int level, int bits)
		{
			return this.deflateInit2(strm, level, 8, bits, 8, 0);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000B437D File Offset: 0x000B257D
		internal int deflateInit(ZStream strm, int level)
		{
			return this.deflateInit(strm, level, 15);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000B438C File Offset: 0x000B258C
		internal int deflateInit2(ZStream strm, int level, int method, int windowBits, int memLevel, int strategy)
		{
			int num = 0;
			strm.msg = null;
			if (level == -1)
			{
				level = 6;
			}
			if (windowBits < 0)
			{
				num = 1;
				windowBits = -windowBits;
			}
			if (memLevel < 1 || memLevel > 9 || method != 8 || windowBits < 9 || windowBits > 15 || level < 0 || level > 9 || strategy < 0 || strategy > 2)
			{
				return -2;
			}
			strm.dstate = this;
			this.noheader = num;
			this.w_bits = windowBits;
			this.w_size = 1 << this.w_bits;
			this.w_mask = this.w_size - 1;
			this.hash_bits = memLevel + 7;
			this.hash_size = 1 << this.hash_bits;
			this.hash_mask = this.hash_size - 1;
			this.hash_shift = (this.hash_bits + 3 - 1) / 3;
			this.window = new byte[this.w_size * 2];
			this.prev = new short[this.w_size];
			this.head = new short[this.hash_size];
			this.lit_bufsize = 1 << memLevel + 6;
			this.pending_buf = new byte[this.lit_bufsize * 4];
			this.pending_buf_size = this.lit_bufsize * 4;
			this.d_buf = this.lit_bufsize / 2;
			this.l_buf = 3 * this.lit_bufsize;
			this.level = level;
			this.strategy = strategy;
			this.method = (byte)method;
			return this.deflateReset(strm);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000B44F8 File Offset: 0x000B26F8
		internal int deflateReset(ZStream strm)
		{
			strm.total_in = (strm.total_out = 0L);
			strm.msg = null;
			strm.data_type = 2;
			this.pending = 0;
			this.pending_out = 0;
			if (this.noheader < 0)
			{
				this.noheader = 0;
			}
			this.status = ((this.noheader != 0) ? 113 : 42);
			strm.adler = strm._adler.adler32(0L, null, 0, 0);
			this.last_flush = 0;
			this.tr_init();
			this.lm_init();
			return 0;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x000B4580 File Offset: 0x000B2780
		internal int deflateEnd()
		{
			if (this.status != 42 && this.status != 113 && this.status != 666)
			{
				return -2;
			}
			this.pending_buf = null;
			this.head = null;
			this.prev = null;
			this.window = null;
			if (this.status != 113)
			{
				return 0;
			}
			return -3;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000B45DC File Offset: 0x000B27DC
		internal int deflateParams(ZStream strm, int _level, int _strategy)
		{
			int result = 0;
			if (_level == -1)
			{
				_level = 6;
			}
			if (_level < 0 || _level > 9 || _strategy < 0 || _strategy > 2)
			{
				return -2;
			}
			if (Deflate.config_table[this.level].func != Deflate.config_table[_level].func && strm.total_in != 0L)
			{
				result = strm.deflate(1);
			}
			if (this.level != _level)
			{
				this.level = _level;
				this.max_lazy_match = Deflate.config_table[this.level].max_lazy;
				this.good_match = Deflate.config_table[this.level].good_length;
				this.nice_match = Deflate.config_table[this.level].nice_length;
				this.max_chain_length = Deflate.config_table[this.level].max_chain;
			}
			this.strategy = _strategy;
			return result;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000B46AC File Offset: 0x000B28AC
		internal int deflateSetDictionary(ZStream strm, byte[] dictionary, int dictLength)
		{
			int num = dictLength;
			int sourceIndex = 0;
			if (dictionary == null || this.status != 42)
			{
				return -2;
			}
			strm.adler = strm._adler.adler32(strm.adler, dictionary, 0, dictLength);
			if (num < 3)
			{
				return 0;
			}
			if (num > this.w_size - 262)
			{
				num = this.w_size - 262;
				sourceIndex = dictLength - num;
			}
			Array.Copy(dictionary, sourceIndex, this.window, 0, num);
			this.strstart = num;
			this.block_start = num;
			this.ins_h = (int)(this.window[0] & byte.MaxValue);
			this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[1] & byte.MaxValue)) & this.hash_mask);
			for (int i = 0; i <= num - 3; i++)
			{
				this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[i + 2] & byte.MaxValue)) & this.hash_mask);
				this.prev[i & this.w_mask] = this.head[this.ins_h];
				this.head[this.ins_h] = (short)i;
			}
			return 0;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000B47D4 File Offset: 0x000B29D4
		internal int deflate(ZStream strm, int flush)
		{
			if (flush > 4 || flush < 0)
			{
				return -2;
			}
			if (strm.next_out == null || (strm.next_in == null && strm.avail_in != 0) || (this.status == 666 && flush != 4))
			{
				strm.msg = Deflate.z_errmsg[4];
				return -2;
			}
			if (strm.avail_out == 0)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			this.strm = strm;
			int num = this.last_flush;
			this.last_flush = flush;
			if (this.status == 42)
			{
				int num2 = 8 + (this.w_bits - 8 << 4) << 8;
				int num3 = (this.level - 1 & 255) >> 1;
				if (num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if (this.strstart != 0)
				{
					num2 |= 32;
				}
				num2 += 31 - num2 % 31;
				this.status = 113;
				this.putShortMSB(num2);
				if (this.strstart != 0)
				{
					this.putShortMSB((int)(strm.adler >> 16));
					this.putShortMSB((int)(strm.adler & 65535L));
				}
				strm.adler = strm._adler.adler32(0L, null, 0, 0);
			}
			if (this.pending != 0)
			{
				strm.flush_pending();
				if (strm.avail_out == 0)
				{
					this.last_flush = -1;
					return 0;
				}
			}
			else if (strm.avail_in == 0 && flush <= num && flush != 4)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			if (this.status == 666 && strm.avail_in != 0)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			if (strm.avail_in != 0 || this.lookahead != 0 || (flush != 0 && this.status != 666))
			{
				int num4 = -1;
				switch (Deflate.config_table[this.level].func)
				{
				case 0:
					num4 = this.deflate_stored(flush);
					break;
				case 1:
					num4 = this.deflate_fast(flush);
					break;
				case 2:
					num4 = this.deflate_slow(flush);
					break;
				}
				if (num4 == 2 || num4 == 3)
				{
					this.status = 666;
				}
				if (num4 == 0 || num4 == 2)
				{
					if (strm.avail_out == 0)
					{
						this.last_flush = -1;
					}
					return 0;
				}
				if (num4 == 1)
				{
					if (flush == 1)
					{
						this._tr_align();
					}
					else
					{
						this._tr_stored_block(0, 0, false);
						if (flush == 3)
						{
							for (int i = 0; i < this.hash_size; i++)
							{
								this.head[i] = 0;
							}
						}
					}
					strm.flush_pending();
					if (strm.avail_out == 0)
					{
						this.last_flush = -1;
						return 0;
					}
				}
			}
			if (flush != 4)
			{
				return 0;
			}
			if (this.noheader != 0)
			{
				return 1;
			}
			this.putShortMSB((int)(strm.adler >> 16));
			this.putShortMSB((int)(strm.adler & 65535L));
			strm.flush_pending();
			this.noheader = -1;
			if (this.pending == 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0400154F RID: 5455
		private const int MAX_MEM_LEVEL = 9;

		// Token: 0x04001550 RID: 5456
		private const int Z_DEFAULT_COMPRESSION = -1;

		// Token: 0x04001551 RID: 5457
		private const int MAX_WBITS = 15;

		// Token: 0x04001552 RID: 5458
		private const int DEF_MEM_LEVEL = 8;

		// Token: 0x04001553 RID: 5459
		private const int STORED = 0;

		// Token: 0x04001554 RID: 5460
		private const int FAST = 1;

		// Token: 0x04001555 RID: 5461
		private const int SLOW = 2;

		// Token: 0x04001556 RID: 5462
		private static readonly Deflate.Config[] config_table;

		// Token: 0x04001557 RID: 5463
		private static readonly string[] z_errmsg = new string[]
		{
			"need dictionary",
			"stream end",
			"",
			"file error",
			"stream error",
			"data error",
			"insufficient memory",
			"buffer error",
			"incompatible version",
			""
		};

		// Token: 0x04001558 RID: 5464
		private const int NeedMore = 0;

		// Token: 0x04001559 RID: 5465
		private const int BlockDone = 1;

		// Token: 0x0400155A RID: 5466
		private const int FinishStarted = 2;

		// Token: 0x0400155B RID: 5467
		private const int FinishDone = 3;

		// Token: 0x0400155C RID: 5468
		private const int PRESET_DICT = 32;

		// Token: 0x0400155D RID: 5469
		private const int Z_FILTERED = 1;

		// Token: 0x0400155E RID: 5470
		private const int Z_HUFFMAN_ONLY = 2;

		// Token: 0x0400155F RID: 5471
		private const int Z_DEFAULT_STRATEGY = 0;

		// Token: 0x04001560 RID: 5472
		private const int Z_NO_FLUSH = 0;

		// Token: 0x04001561 RID: 5473
		private const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04001562 RID: 5474
		private const int Z_SYNC_FLUSH = 2;

		// Token: 0x04001563 RID: 5475
		private const int Z_FULL_FLUSH = 3;

		// Token: 0x04001564 RID: 5476
		private const int Z_FINISH = 4;

		// Token: 0x04001565 RID: 5477
		private const int Z_OK = 0;

		// Token: 0x04001566 RID: 5478
		private const int Z_STREAM_END = 1;

		// Token: 0x04001567 RID: 5479
		private const int Z_NEED_DICT = 2;

		// Token: 0x04001568 RID: 5480
		private const int Z_ERRNO = -1;

		// Token: 0x04001569 RID: 5481
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x0400156A RID: 5482
		private const int Z_DATA_ERROR = -3;

		// Token: 0x0400156B RID: 5483
		private const int Z_MEM_ERROR = -4;

		// Token: 0x0400156C RID: 5484
		private const int Z_BUF_ERROR = -5;

		// Token: 0x0400156D RID: 5485
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x0400156E RID: 5486
		private const int INIT_STATE = 42;

		// Token: 0x0400156F RID: 5487
		private const int BUSY_STATE = 113;

		// Token: 0x04001570 RID: 5488
		private const int FINISH_STATE = 666;

		// Token: 0x04001571 RID: 5489
		private const int Z_DEFLATED = 8;

		// Token: 0x04001572 RID: 5490
		private const int STORED_BLOCK = 0;

		// Token: 0x04001573 RID: 5491
		private const int STATIC_TREES = 1;

		// Token: 0x04001574 RID: 5492
		private const int DYN_TREES = 2;

		// Token: 0x04001575 RID: 5493
		private const int Z_BINARY = 0;

		// Token: 0x04001576 RID: 5494
		private const int Z_ASCII = 1;

		// Token: 0x04001577 RID: 5495
		private const int Z_UNKNOWN = 2;

		// Token: 0x04001578 RID: 5496
		private const int Buf_size = 16;

		// Token: 0x04001579 RID: 5497
		private const int REP_3_6 = 16;

		// Token: 0x0400157A RID: 5498
		private const int REPZ_3_10 = 17;

		// Token: 0x0400157B RID: 5499
		private const int REPZ_11_138 = 18;

		// Token: 0x0400157C RID: 5500
		private const int MIN_MATCH = 3;

		// Token: 0x0400157D RID: 5501
		private const int MAX_MATCH = 258;

		// Token: 0x0400157E RID: 5502
		private const int MIN_LOOKAHEAD = 262;

		// Token: 0x0400157F RID: 5503
		private const int MAX_BITS = 15;

		// Token: 0x04001580 RID: 5504
		private const int D_CODES = 30;

		// Token: 0x04001581 RID: 5505
		private const int BL_CODES = 19;

		// Token: 0x04001582 RID: 5506
		private const int LENGTH_CODES = 29;

		// Token: 0x04001583 RID: 5507
		private const int LITERALS = 256;

		// Token: 0x04001584 RID: 5508
		private const int L_CODES = 286;

		// Token: 0x04001585 RID: 5509
		private const int HEAP_SIZE = 573;

		// Token: 0x04001586 RID: 5510
		private const int END_BLOCK = 256;

		// Token: 0x04001587 RID: 5511
		internal ZStream strm;

		// Token: 0x04001588 RID: 5512
		internal int status;

		// Token: 0x04001589 RID: 5513
		internal byte[] pending_buf;

		// Token: 0x0400158A RID: 5514
		internal int pending_buf_size;

		// Token: 0x0400158B RID: 5515
		internal int pending_out;

		// Token: 0x0400158C RID: 5516
		internal int pending;

		// Token: 0x0400158D RID: 5517
		internal int noheader;

		// Token: 0x0400158E RID: 5518
		internal byte data_type;

		// Token: 0x0400158F RID: 5519
		internal byte method;

		// Token: 0x04001590 RID: 5520
		internal int last_flush;

		// Token: 0x04001591 RID: 5521
		internal int w_size;

		// Token: 0x04001592 RID: 5522
		internal int w_bits;

		// Token: 0x04001593 RID: 5523
		internal int w_mask;

		// Token: 0x04001594 RID: 5524
		internal byte[] window;

		// Token: 0x04001595 RID: 5525
		internal int window_size;

		// Token: 0x04001596 RID: 5526
		internal short[] prev;

		// Token: 0x04001597 RID: 5527
		internal short[] head;

		// Token: 0x04001598 RID: 5528
		internal int ins_h;

		// Token: 0x04001599 RID: 5529
		internal int hash_size;

		// Token: 0x0400159A RID: 5530
		internal int hash_bits;

		// Token: 0x0400159B RID: 5531
		internal int hash_mask;

		// Token: 0x0400159C RID: 5532
		internal int hash_shift;

		// Token: 0x0400159D RID: 5533
		internal int block_start;

		// Token: 0x0400159E RID: 5534
		internal int match_length;

		// Token: 0x0400159F RID: 5535
		internal int prev_match;

		// Token: 0x040015A0 RID: 5536
		internal int match_available;

		// Token: 0x040015A1 RID: 5537
		internal int strstart;

		// Token: 0x040015A2 RID: 5538
		internal int match_start;

		// Token: 0x040015A3 RID: 5539
		internal int lookahead;

		// Token: 0x040015A4 RID: 5540
		internal int prev_length;

		// Token: 0x040015A5 RID: 5541
		internal int max_chain_length;

		// Token: 0x040015A6 RID: 5542
		internal int max_lazy_match;

		// Token: 0x040015A7 RID: 5543
		internal int level;

		// Token: 0x040015A8 RID: 5544
		internal int strategy;

		// Token: 0x040015A9 RID: 5545
		internal int good_match;

		// Token: 0x040015AA RID: 5546
		internal int nice_match;

		// Token: 0x040015AB RID: 5547
		internal short[] dyn_ltree;

		// Token: 0x040015AC RID: 5548
		internal short[] dyn_dtree;

		// Token: 0x040015AD RID: 5549
		internal short[] bl_tree;

		// Token: 0x040015AE RID: 5550
		internal ZTree l_desc = new ZTree();

		// Token: 0x040015AF RID: 5551
		internal ZTree d_desc = new ZTree();

		// Token: 0x040015B0 RID: 5552
		internal ZTree bl_desc = new ZTree();

		// Token: 0x040015B1 RID: 5553
		internal short[] bl_count = new short[16];

		// Token: 0x040015B2 RID: 5554
		internal int[] heap = new int[573];

		// Token: 0x040015B3 RID: 5555
		internal int heap_len;

		// Token: 0x040015B4 RID: 5556
		internal int heap_max;

		// Token: 0x040015B5 RID: 5557
		internal byte[] depth = new byte[573];

		// Token: 0x040015B6 RID: 5558
		internal int l_buf;

		// Token: 0x040015B7 RID: 5559
		internal int lit_bufsize;

		// Token: 0x040015B8 RID: 5560
		internal int last_lit;

		// Token: 0x040015B9 RID: 5561
		internal int d_buf;

		// Token: 0x040015BA RID: 5562
		internal int opt_len;

		// Token: 0x040015BB RID: 5563
		internal int static_len;

		// Token: 0x040015BC RID: 5564
		internal int matches;

		// Token: 0x040015BD RID: 5565
		internal int last_eob_len;

		// Token: 0x040015BE RID: 5566
		internal uint bi_buf;

		// Token: 0x040015BF RID: 5567
		internal int bi_valid;

		// Token: 0x020008D6 RID: 2262
		internal class Config
		{
			// Token: 0x06004D5B RID: 19803 RVA: 0x001B0925 File Offset: 0x001AEB25
			internal Config(int good_length, int max_lazy, int nice_length, int max_chain, int func)
			{
				this.good_length = good_length;
				this.max_lazy = max_lazy;
				this.nice_length = nice_length;
				this.max_chain = max_chain;
				this.func = func;
			}

			// Token: 0x0400337A RID: 13178
			internal int good_length;

			// Token: 0x0400337B RID: 13179
			internal int max_lazy;

			// Token: 0x0400337C RID: 13180
			internal int nice_length;

			// Token: 0x0400337D RID: 13181
			internal int max_chain;

			// Token: 0x0400337E RID: 13182
			internal int func;
		}
	}
}
