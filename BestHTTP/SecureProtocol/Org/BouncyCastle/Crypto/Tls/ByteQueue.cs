using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E9 RID: 1001
	public class ByteQueue
	{
		// Token: 0x060028E4 RID: 10468 RVA: 0x0010F71A File Offset: 0x0010D91A
		public static int NextTwoPow(int i)
		{
			i |= i >> 1;
			i |= i >> 2;
			i |= i >> 4;
			i |= i >> 8;
			i |= i >> 16;
			return i + 1;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0010F743 File Offset: 0x0010D943
		public ByteQueue() : this(1024)
		{
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0010F750 File Offset: 0x0010D950
		public ByteQueue(int capacity)
		{
			this.databuf = ((capacity == 0) ? TlsUtilities.EmptyBytes : new byte[capacity]);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0010F76E File Offset: 0x0010D96E
		public ByteQueue(byte[] buf, int off, int len)
		{
			this.databuf = buf;
			this.skipped = off;
			this.available = len;
			this.readOnlyBuf = true;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0010F794 File Offset: 0x0010D994
		public void AddData(byte[] data, int offset, int len)
		{
			if (this.readOnlyBuf)
			{
				throw new InvalidOperationException("Cannot add data to read-only buffer");
			}
			if (this.skipped + this.available + len > this.databuf.Length)
			{
				int num = ByteQueue.NextTwoPow(this.available + len);
				if (num > this.databuf.Length)
				{
					byte[] destinationArray = new byte[num];
					Array.Copy(this.databuf, this.skipped, destinationArray, 0, this.available);
					this.databuf = destinationArray;
				}
				else
				{
					Array.Copy(this.databuf, this.skipped, this.databuf, 0, this.available);
				}
				this.skipped = 0;
			}
			Array.Copy(data, offset, this.databuf, this.skipped + this.available, len);
			this.available += len;
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x0010F85D File Offset: 0x0010DA5D
		public int Available
		{
			get
			{
				return this.available;
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0010F868 File Offset: 0x0010DA68
		public void CopyTo(Stream output, int length)
		{
			if (length > this.available)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Cannot copy ",
					length,
					" bytes, only got ",
					this.available
				}));
			}
			output.Write(this.databuf, this.skipped, length);
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0010F8CC File Offset: 0x0010DACC
		public void Read(byte[] buf, int offset, int len, int skip)
		{
			if (buf.Length - offset < len)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Buffer size of ",
					buf.Length,
					" is too small for a read of ",
					len,
					" bytes"
				}));
			}
			if (this.available - skip < len)
			{
				throw new InvalidOperationException("Not enough data to read");
			}
			Array.Copy(this.databuf, this.skipped + skip, buf, offset, len);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0010F94C File Offset: 0x0010DB4C
		public MemoryStream ReadFrom(int length)
		{
			if (length > this.available)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Cannot read ",
					length,
					" bytes, only got ",
					this.available
				}));
			}
			int index = this.skipped;
			this.available -= length;
			this.skipped += length;
			return new MemoryStream(this.databuf, index, length, false);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x0010F9CC File Offset: 0x0010DBCC
		public void RemoveData(int i)
		{
			if (i > this.available)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Cannot remove ",
					i,
					" bytes, only got ",
					this.available
				}));
			}
			this.available -= i;
			this.skipped += i;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0010FA36 File Offset: 0x0010DC36
		public void RemoveData(byte[] buf, int off, int len, int skip)
		{
			this.Read(buf, off, len, skip);
			this.RemoveData(skip + len);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0010FA50 File Offset: 0x0010DC50
		public byte[] RemoveData(int len, int skip)
		{
			byte[] array = new byte[len];
			this.RemoveData(array, 0, len, skip);
			return array;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0010FA70 File Offset: 0x0010DC70
		public void Shrink()
		{
			if (this.available == 0)
			{
				this.databuf = TlsUtilities.EmptyBytes;
				this.skipped = 0;
				return;
			}
			int num = ByteQueue.NextTwoPow(this.available);
			if (num < this.databuf.Length)
			{
				byte[] destinationArray = new byte[num];
				Array.Copy(this.databuf, this.skipped, destinationArray, 0, this.available);
				this.databuf = destinationArray;
				this.skipped = 0;
			}
		}

		// Token: 0x04001A23 RID: 6691
		private const int DefaultCapacity = 1024;

		// Token: 0x04001A24 RID: 6692
		private byte[] databuf;

		// Token: 0x04001A25 RID: 6693
		private int skipped;

		// Token: 0x04001A26 RID: 6694
		private int available;

		// Token: 0x04001A27 RID: 6695
		private bool readOnlyBuf;
	}
}
