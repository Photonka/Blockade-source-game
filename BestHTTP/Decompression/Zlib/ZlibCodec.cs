using System;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007F7 RID: 2039
	internal sealed class ZlibCodec
	{
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060048BA RID: 18618 RVA: 0x001A2086 File Offset: 0x001A0286
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x001A208E File Offset: 0x001A028E
		public ZlibCodec()
		{
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x001A20A8 File Offset: 0x001A02A8
		public ZlibCodec(CompressionMode mode)
		{
			if (mode == CompressionMode.Compress)
			{
				if (this.InitializeDeflate() != 0)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
			}
			else
			{
				if (mode != CompressionMode.Decompress)
				{
					throw new ZlibException("Invalid ZlibStreamFlavor.");
				}
				if (this.InitializeInflate() != 0)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
			}
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x001A2102 File Offset: 0x001A0302
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x001A2110 File Offset: 0x001A0310
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x001A211F File Offset: 0x001A031F
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x001A2130 File Offset: 0x001A0330
		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			if (this.dstate != null)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x001A2165 File Offset: 0x001A0365
		public int Inflate(FlushType flush)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x001A2186 File Offset: 0x001A0386
		public int EndInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int result = this.istate.End();
			this.istate = null;
			return result;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x001A21AD File Offset: 0x001A03AD
		public int SyncInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x001A21CD File Offset: 0x001A03CD
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x001A21D6 File Offset: 0x001A03D6
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x001A21E6 File Offset: 0x001A03E6
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x001A21F6 File Offset: 0x001A03F6
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x001A220D File Offset: 0x001A040D
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x001A2224 File Offset: 0x001A0424
		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			if (this.istate != null)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			this.dstate = new DeflateManager();
			this.dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return this.dstate.Initialize(this, this.CompressLevel, this.WindowBits, this.Strategy);
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x001A2279 File Offset: 0x001A0479
		public int Deflate(FlushType flush)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x001A229A File Offset: 0x001A049A
		public int EndDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x001A22B7 File Offset: 0x001A04B7
		public void ResetDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset();
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x001A22D7 File Offset: 0x001A04D7
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x001A22F9 File Offset: 0x001A04F9
		public int SetDictionary(byte[] dictionary)
		{
			if (this.istate != null)
			{
				return this.istate.SetDictionary(dictionary);
			}
			if (this.dstate != null)
			{
				return this.dstate.SetDictionary(dictionary);
			}
			throw new ZlibException("No Inflate or Deflate state!");
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x001A2330 File Offset: 0x001A0530
		internal void flush_pending()
		{
			int num = this.dstate.pendingCount;
			if (num > this.AvailableBytesOut)
			{
				num = this.AvailableBytesOut;
			}
			if (num == 0)
			{
				return;
			}
			if (this.dstate.pending.Length <= this.dstate.nextPending || this.OutputBuffer.Length <= this.NextOut || this.dstate.pending.Length < this.dstate.nextPending + num || this.OutputBuffer.Length < this.NextOut + num)
			{
				throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", this.dstate.pending.Length, this.dstate.pendingCount));
			}
			Array.Copy(this.dstate.pending, this.dstate.nextPending, this.OutputBuffer, this.NextOut, num);
			this.NextOut += num;
			this.dstate.nextPending += num;
			this.TotalBytesOut += (long)num;
			this.AvailableBytesOut -= num;
			this.dstate.pendingCount -= num;
			if (this.dstate.pendingCount == 0)
			{
				this.dstate.nextPending = 0;
			}
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x001A247C File Offset: 0x001A067C
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.AvailableBytesIn;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.AvailableBytesIn -= num;
			if (this.dstate.WantRfc1950HeaderBytes)
			{
				this._Adler32 = Adler.Adler32(this._Adler32, this.InputBuffer, this.NextIn, num);
			}
			Array.Copy(this.InputBuffer, this.NextIn, buf, start, num);
			this.NextIn += num;
			this.TotalBytesIn += (long)num;
			return num;
		}

		// Token: 0x04002EE4 RID: 12004
		public byte[] InputBuffer;

		// Token: 0x04002EE5 RID: 12005
		public int NextIn;

		// Token: 0x04002EE6 RID: 12006
		public int AvailableBytesIn;

		// Token: 0x04002EE7 RID: 12007
		public long TotalBytesIn;

		// Token: 0x04002EE8 RID: 12008
		public byte[] OutputBuffer;

		// Token: 0x04002EE9 RID: 12009
		public int NextOut;

		// Token: 0x04002EEA RID: 12010
		public int AvailableBytesOut;

		// Token: 0x04002EEB RID: 12011
		public long TotalBytesOut;

		// Token: 0x04002EEC RID: 12012
		public string Message;

		// Token: 0x04002EED RID: 12013
		internal DeflateManager dstate;

		// Token: 0x04002EEE RID: 12014
		internal InflateManager istate;

		// Token: 0x04002EEF RID: 12015
		internal uint _Adler32;

		// Token: 0x04002EF0 RID: 12016
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x04002EF1 RID: 12017
		public int WindowBits = 15;

		// Token: 0x04002EF2 RID: 12018
		public CompressionStrategy Strategy;
	}
}
