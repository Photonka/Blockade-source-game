using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO
{
	// Token: 0x02000527 RID: 1319
	public class CipherStream : Stream
	{
		// Token: 0x0600325C RID: 12892 RVA: 0x00133C5F File Offset: 0x00131E5F
		public CipherStream(Stream stream, IBufferedCipher readCipher, IBufferedCipher writeCipher)
		{
			this.stream = stream;
			if (readCipher != null)
			{
				this.inCipher = readCipher;
				this.mInBuf = null;
			}
			if (writeCipher != null)
			{
				this.outCipher = writeCipher;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x00133C89 File Offset: 0x00131E89
		public IBufferedCipher ReadCipher
		{
			get
			{
				return this.inCipher;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x00133C91 File Offset: 0x00131E91
		public IBufferedCipher WriteCipher
		{
			get
			{
				return this.outCipher;
			}
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x00133C9C File Offset: 0x00131E9C
		public override int ReadByte()
		{
			if (this.inCipher == null)
			{
				return this.stream.ReadByte();
			}
			if ((this.mInBuf == null || this.mInPos >= this.mInBuf.Length) && !this.FillInBuf())
			{
				return -1;
			}
			byte[] array = this.mInBuf;
			int num = this.mInPos;
			this.mInPos = num + 1;
			return array[num];
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x00133CF8 File Offset: 0x00131EF8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.inCipher == null)
			{
				return this.stream.Read(buffer, offset, count);
			}
			int num = 0;
			while (num < count && ((this.mInBuf != null && this.mInPos < this.mInBuf.Length) || this.FillInBuf()))
			{
				int num2 = Math.Min(count - num, this.mInBuf.Length - this.mInPos);
				Array.Copy(this.mInBuf, this.mInPos, buffer, offset + num, num2);
				this.mInPos += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x00133D85 File Offset: 0x00131F85
		private bool FillInBuf()
		{
			if (this.inStreamEnded)
			{
				return false;
			}
			this.mInPos = 0;
			do
			{
				this.mInBuf = this.ReadAndProcessBlock();
			}
			while (!this.inStreamEnded && this.mInBuf == null);
			return this.mInBuf != null;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x00133DC0 File Offset: 0x00131FC0
		private byte[] ReadAndProcessBlock()
		{
			int blockSize = this.inCipher.GetBlockSize();
			byte[] array = new byte[(blockSize == 0) ? 256 : blockSize];
			int num = 0;
			for (;;)
			{
				int num2 = this.stream.Read(array, num, array.Length - num);
				if (num2 < 1)
				{
					break;
				}
				num += num2;
				if (num >= array.Length)
				{
					goto IL_4C;
				}
			}
			this.inStreamEnded = true;
			IL_4C:
			byte[] array2 = this.inStreamEnded ? this.inCipher.DoFinal(array, 0, num) : this.inCipher.ProcessBytes(array);
			if (array2 != null && array2.Length == 0)
			{
				array2 = null;
			}
			return array2;
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x00133E48 File Offset: 0x00132048
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outCipher == null)
			{
				this.stream.Write(buffer, offset, count);
				return;
			}
			byte[] array = this.outCipher.ProcessBytes(buffer, offset, count);
			if (array != null)
			{
				this.stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x00133E90 File Offset: 0x00132090
		public override void WriteByte(byte b)
		{
			if (this.outCipher == null)
			{
				this.stream.WriteByte(b);
				return;
			}
			byte[] array = this.outCipher.ProcessByte(b);
			if (array != null)
			{
				this.stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x00133ED2 File Offset: 0x001320D2
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead && this.inCipher != null;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x00133EEC File Offset: 0x001320EC
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite && this.outCipher != null;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x00092231 File Offset: 0x00090431
		// (set) Token: 0x0600326A RID: 12906 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x00133F08 File Offset: 0x00132108
		public override void Close()
		{
			if (this.outCipher != null)
			{
				byte[] array = this.outCipher.DoFinal();
				this.stream.Write(array, 0, array.Length);
				this.stream.Flush();
			}
			Platform.Dispose(this.stream);
			base.Close();
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x00133F55 File Offset: 0x00132155
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x00092231 File Offset: 0x00090431
		public sealed override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400202C RID: 8236
		internal Stream stream;

		// Token: 0x0400202D RID: 8237
		internal IBufferedCipher inCipher;

		// Token: 0x0400202E RID: 8238
		internal IBufferedCipher outCipher;

		// Token: 0x0400202F RID: 8239
		private byte[] mInBuf;

		// Token: 0x04002030 RID: 8240
		private int mInPos;

		// Token: 0x04002031 RID: 8241
		private bool inStreamEnded;
	}
}
