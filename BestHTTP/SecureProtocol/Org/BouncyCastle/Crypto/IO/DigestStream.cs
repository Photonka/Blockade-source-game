using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO
{
	// Token: 0x02000529 RID: 1321
	public class DigestStream : Stream
	{
		// Token: 0x06003273 RID: 12915 RVA: 0x00133F9B File Offset: 0x0013219B
		public DigestStream(Stream stream, IDigest readDigest, IDigest writeDigest)
		{
			this.stream = stream;
			this.inDigest = readDigest;
			this.outDigest = writeDigest;
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x00133FB8 File Offset: 0x001321B8
		public virtual IDigest ReadDigest()
		{
			return this.inDigest;
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x00133FC0 File Offset: 0x001321C0
		public virtual IDigest WriteDigest()
		{
			return this.outDigest;
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x00133FC8 File Offset: 0x001321C8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inDigest != null && num > 0)
			{
				this.inDigest.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x00134000 File Offset: 0x00132200
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inDigest != null && num >= 0)
			{
				this.inDigest.Update((byte)num);
			}
			return num;
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x00134033 File Offset: 0x00132233
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outDigest != null && count > 0)
			{
				this.outDigest.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x0013405D File Offset: 0x0013225D
		public override void WriteByte(byte b)
		{
			if (this.outDigest != null)
			{
				this.outDigest.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x0013407F File Offset: 0x0013227F
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x0013408C File Offset: 0x0013228C
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x00134099 File Offset: 0x00132299
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x001340A6 File Offset: 0x001322A6
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x001340B3 File Offset: 0x001322B3
		// (set) Token: 0x0600327F RID: 12927 RVA: 0x001340C0 File Offset: 0x001322C0
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x001340CE File Offset: 0x001322CE
		public override void Close()
		{
			Platform.Dispose(this.stream);
			base.Close();
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x001340E1 File Offset: 0x001322E1
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x001340EE File Offset: 0x001322EE
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x001340FD File Offset: 0x001322FD
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x04002033 RID: 8243
		protected readonly Stream stream;

		// Token: 0x04002034 RID: 8244
		protected readonly IDigest inDigest;

		// Token: 0x04002035 RID: 8245
		protected readonly IDigest outDigest;
	}
}
