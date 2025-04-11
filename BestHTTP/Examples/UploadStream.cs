using System;
using System.IO;
using System.Threading;

namespace BestHTTP.Examples
{
	// Token: 0x0200018D RID: 397
	public sealed class UploadStream : Stream
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x000998F0 File Offset: 0x00097AF0
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x000998F8 File Offset: 0x00097AF8
		public string Name { get; private set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00099904 File Offset: 0x00097B04
		private bool IsReadBufferEmpty
		{
			get
			{
				object obj = this.locker;
				bool result;
				lock (obj)
				{
					result = (this.ReadBuffer.Position == this.ReadBuffer.Length);
				}
				return result;
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00099958 File Offset: 0x00097B58
		public UploadStream(string name) : this()
		{
			this.Name = name;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00099968 File Offset: 0x00097B68
		public UploadStream()
		{
			this.ReadBuffer = new MemoryStream();
			this.WriteBuffer = new MemoryStream();
			this.Name = string.Empty;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000999CC File Offset: 0x00097BCC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.noMoreData)
			{
				if (this.ReadBuffer.Position != this.ReadBuffer.Length)
				{
					return this.ReadBuffer.Read(buffer, offset, count);
				}
				if (this.WriteBuffer.Length <= 0L)
				{
					HTTPManager.Logger.Information("UploadStream", string.Format("{0} - Read - End Of Stream", this.Name));
					return -1;
				}
				this.SwitchBuffers();
			}
			object obj;
			if (this.IsReadBufferEmpty)
			{
				this.ARE.WaitOne();
				obj = this.locker;
				lock (obj)
				{
					if (this.IsReadBufferEmpty && this.WriteBuffer.Length > 0L)
					{
						this.SwitchBuffers();
					}
				}
			}
			int result = -1;
			obj = this.locker;
			lock (obj)
			{
				result = this.ReadBuffer.Read(buffer, offset, count);
			}
			return result;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00099ADC File Offset: 0x00097CDC
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.noMoreData)
			{
				throw new ArgumentException("noMoreData already set!");
			}
			object obj = this.locker;
			lock (obj)
			{
				this.WriteBuffer.Write(buffer, offset, count);
				this.SwitchBuffers();
			}
			this.ARE.Set();
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00099B4C File Offset: 0x00097D4C
		public override void Flush()
		{
			this.Finish();
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00099B54 File Offset: 0x00097D54
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				HTTPManager.Logger.Information("UploadStream", string.Format("{0} - Dispose", this.Name));
				this.ReadBuffer.Dispose();
				this.ReadBuffer = null;
				this.WriteBuffer.Dispose();
				this.WriteBuffer = null;
				this.ARE.Close();
				this.ARE = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00099BC0 File Offset: 0x00097DC0
		public void Finish()
		{
			if (this.noMoreData)
			{
				throw new ArgumentException("noMoreData already set!");
			}
			HTTPManager.Logger.Information("UploadStream", string.Format("{0} - Finish", this.Name));
			this.noMoreData = true;
			this.ARE.Set();
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00099C14 File Offset: 0x00097E14
		private bool SwitchBuffers()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.ReadBuffer.Position == this.ReadBuffer.Length)
				{
					this.WriteBuffer.Seek(0L, SeekOrigin.Begin);
					this.ReadBuffer.SetLength(0L);
					MemoryStream writeBuffer = this.WriteBuffer;
					this.WriteBuffer = this.ReadBuffer;
					this.ReadBuffer = writeBuffer;
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override bool CanRead
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override bool CanSeek
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override bool CanWrite
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00096B9B File Offset: 0x00094D9B
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00096B9B File Offset: 0x00094D9B
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001264 RID: 4708
		private MemoryStream ReadBuffer = new MemoryStream();

		// Token: 0x04001265 RID: 4709
		private MemoryStream WriteBuffer = new MemoryStream();

		// Token: 0x04001266 RID: 4710
		private bool noMoreData;

		// Token: 0x04001267 RID: 4711
		private AutoResetEvent ARE = new AutoResetEvent(false);

		// Token: 0x04001268 RID: 4712
		private object locker = new object();
	}
}
