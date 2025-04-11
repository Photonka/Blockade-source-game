using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000476 RID: 1142
	internal class TlsStream : Stream
	{
		// Token: 0x06002CEC RID: 11500 RVA: 0x0011D33F File Offset: 0x0011B53F
		internal TlsStream(TlsProtocol handler)
		{
			this.handler = handler;
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x0011D34E File Offset: 0x0011B54E
		public override bool CanRead
		{
			get
			{
				return !this.handler.IsClosed;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x0011D34E File Offset: 0x0011B54E
		public override bool CanWrite
		{
			get
			{
				return !this.handler.IsClosed;
			}
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x0011D35E File Offset: 0x0011B55E
		public override void Close()
		{
			this.handler.Close();
			base.Close();
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x0011D371 File Offset: 0x0011B571
		public override void Flush()
		{
			this.handler.Flush();
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x00092231 File Offset: 0x00090431
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x00092231 File Offset: 0x00090431
		// (set) Token: 0x06002CF4 RID: 11508 RVA: 0x00092231 File Offset: 0x00090431
		public override long Position
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

		// Token: 0x06002CF5 RID: 11509 RVA: 0x0011D37E File Offset: 0x0011B57E
		public override int Read(byte[] buf, int off, int len)
		{
			return this.handler.ReadApplicationData(buf, off, len);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x0011D390 File Offset: 0x0011B590
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x00092231 File Offset: 0x00090431
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x00092231 File Offset: 0x00090431
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0011D3B5 File Offset: 0x0011B5B5
		public override void Write(byte[] buf, int off, int len)
		{
			this.handler.WriteData(buf, off, len);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x0011D3C5 File Offset: 0x0011B5C5
		public override void WriteByte(byte b)
		{
			this.handler.WriteData(new byte[]
			{
				b
			}, 0, 1);
		}

		// Token: 0x04001D71 RID: 7537
		private readonly TlsProtocol handler;
	}
}
