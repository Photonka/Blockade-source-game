using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Utilities
{
	// Token: 0x020006C1 RID: 1729
	[Obsolete("Use BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.FilterStream")]
	public class FilterStream : Stream
	{
		// Token: 0x06004001 RID: 16385 RVA: 0x00180905 File Offset: 0x0017EB05
		[Obsolete("Use BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.FilterStream")]
		public FilterStream(Stream s)
		{
			this.s = s;
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x00180914 File Offset: 0x0017EB14
		public override bool CanRead
		{
			get
			{
				return this.s.CanRead;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x00180921 File Offset: 0x0017EB21
		public override bool CanSeek
		{
			get
			{
				return this.s.CanSeek;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x0018092E File Offset: 0x0017EB2E
		public override bool CanWrite
		{
			get
			{
				return this.s.CanWrite;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x0018093B File Offset: 0x0017EB3B
		public override long Length
		{
			get
			{
				return this.s.Length;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x00180948 File Offset: 0x0017EB48
		// (set) Token: 0x06004007 RID: 16391 RVA: 0x00180955 File Offset: 0x0017EB55
		public override long Position
		{
			get
			{
				return this.s.Position;
			}
			set
			{
				this.s.Position = value;
			}
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x00180963 File Offset: 0x0017EB63
		public override void Close()
		{
			Platform.Dispose(this.s);
			base.Close();
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x00180976 File Offset: 0x0017EB76
		public override void Flush()
		{
			this.s.Flush();
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x00180983 File Offset: 0x0017EB83
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.s.Seek(offset, origin);
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x00180992 File Offset: 0x0017EB92
		public override void SetLength(long value)
		{
			this.s.SetLength(value);
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x001809A0 File Offset: 0x0017EBA0
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.s.Read(buffer, offset, count);
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x001809B0 File Offset: 0x0017EBB0
		public override int ReadByte()
		{
			return this.s.ReadByte();
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x001809BD File Offset: 0x0017EBBD
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.s.Write(buffer, offset, count);
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x001809CD File Offset: 0x0017EBCD
		public override void WriteByte(byte value)
		{
			this.s.WriteByte(value);
		}

		// Token: 0x040027C0 RID: 10176
		protected readonly Stream s;
	}
}
