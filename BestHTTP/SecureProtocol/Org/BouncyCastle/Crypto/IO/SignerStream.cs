using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO
{
	// Token: 0x0200052D RID: 1325
	public class SignerStream : Stream
	{
		// Token: 0x0600329D RID: 12957 RVA: 0x001342F0 File Offset: 0x001324F0
		public SignerStream(Stream stream, ISigner readSigner, ISigner writeSigner)
		{
			this.stream = stream;
			this.inSigner = readSigner;
			this.outSigner = writeSigner;
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x0013430D File Offset: 0x0013250D
		public virtual ISigner ReadSigner()
		{
			return this.inSigner;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x00134315 File Offset: 0x00132515
		public virtual ISigner WriteSigner()
		{
			return this.outSigner;
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x00134320 File Offset: 0x00132520
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inSigner != null && num > 0)
			{
				this.inSigner.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x00134358 File Offset: 0x00132558
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inSigner != null && num >= 0)
			{
				this.inSigner.Update((byte)num);
			}
			return num;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x0013438B File Offset: 0x0013258B
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outSigner != null && count > 0)
			{
				this.outSigner.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x001343B5 File Offset: 0x001325B5
		public override void WriteByte(byte b)
		{
			if (this.outSigner != null)
			{
				this.outSigner.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x001343D7 File Offset: 0x001325D7
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x001343E4 File Offset: 0x001325E4
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x001343F1 File Offset: 0x001325F1
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x001343FE File Offset: 0x001325FE
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x0013440B File Offset: 0x0013260B
		// (set) Token: 0x060032A9 RID: 12969 RVA: 0x00134418 File Offset: 0x00132618
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

		// Token: 0x060032AA RID: 12970 RVA: 0x00134426 File Offset: 0x00132626
		public override void Close()
		{
			Platform.Dispose(this.stream);
			base.Close();
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x00134439 File Offset: 0x00132639
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x00134446 File Offset: 0x00132646
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x00134455 File Offset: 0x00132655
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x0400203B RID: 8251
		protected readonly Stream stream;

		// Token: 0x0400203C RID: 8252
		protected readonly ISigner inSigner;

		// Token: 0x0400203D RID: 8253
		protected readonly ISigner outSigner;
	}
}
