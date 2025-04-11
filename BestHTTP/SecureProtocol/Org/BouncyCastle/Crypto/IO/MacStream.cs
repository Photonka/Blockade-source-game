using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO
{
	// Token: 0x0200052B RID: 1323
	public class MacStream : Stream
	{
		// Token: 0x06003288 RID: 12936 RVA: 0x00134144 File Offset: 0x00132344
		public MacStream(Stream stream, IMac readMac, IMac writeMac)
		{
			this.stream = stream;
			this.inMac = readMac;
			this.outMac = writeMac;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x00134161 File Offset: 0x00132361
		public virtual IMac ReadMac()
		{
			return this.inMac;
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x00134169 File Offset: 0x00132369
		public virtual IMac WriteMac()
		{
			return this.outMac;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x00134174 File Offset: 0x00132374
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inMac != null && num > 0)
			{
				this.inMac.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x001341AC File Offset: 0x001323AC
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inMac != null && num >= 0)
			{
				this.inMac.Update((byte)num);
			}
			return num;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x001341DF File Offset: 0x001323DF
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outMac != null && count > 0)
			{
				this.outMac.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x00134209 File Offset: 0x00132409
		public override void WriteByte(byte b)
		{
			if (this.outMac != null)
			{
				this.outMac.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x0013422B File Offset: 0x0013242B
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x00134238 File Offset: 0x00132438
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x00134245 File Offset: 0x00132445
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x00134252 File Offset: 0x00132452
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x0013425F File Offset: 0x0013245F
		// (set) Token: 0x06003294 RID: 12948 RVA: 0x0013426C File Offset: 0x0013246C
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

		// Token: 0x06003295 RID: 12949 RVA: 0x0013427A File Offset: 0x0013247A
		public override void Close()
		{
			Platform.Dispose(this.stream);
			base.Close();
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x0013428D File Offset: 0x0013248D
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x0013429A File Offset: 0x0013249A
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x001342A9 File Offset: 0x001324A9
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x04002037 RID: 8247
		protected readonly Stream stream;

		// Token: 0x04002038 RID: 8248
		protected readonly IMac inMac;

		// Token: 0x04002039 RID: 8249
		protected readonly IMac outMac;
	}
}
