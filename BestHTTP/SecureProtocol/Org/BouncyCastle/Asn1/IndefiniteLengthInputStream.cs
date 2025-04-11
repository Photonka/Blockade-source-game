using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000658 RID: 1624
	internal class IndefiniteLengthInputStream : LimitedInputStream
	{
		// Token: 0x06003CD7 RID: 15575 RVA: 0x00174F77 File Offset: 0x00173177
		internal IndefiniteLengthInputStream(Stream inStream, int limit) : base(inStream, limit)
		{
			this._lookAhead = this.RequireByte();
			this.CheckForEof();
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x00174F9B File Offset: 0x0017319B
		internal void SetEofOn00(bool eofOn00)
		{
			this._eofOn00 = eofOn00;
			if (this._eofOn00)
			{
				this.CheckForEof();
			}
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x00174FB3 File Offset: 0x001731B3
		private bool CheckForEof()
		{
			if (this._lookAhead != 0)
			{
				return this._lookAhead < 0;
			}
			if (this.RequireByte() != 0)
			{
				throw new IOException("malformed end-of-contents marker");
			}
			this._lookAhead = -1;
			this.SetParentEofDetect(true);
			return true;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x00174FEC File Offset: 0x001731EC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._eofOn00 || count <= 1)
			{
				return base.Read(buffer, offset, count);
			}
			if (this._lookAhead < 0)
			{
				return 0;
			}
			int num = this._in.Read(buffer, offset + 1, count - 1);
			if (num <= 0)
			{
				throw new EndOfStreamException();
			}
			buffer[offset] = (byte)this._lookAhead;
			this._lookAhead = this.RequireByte();
			return num + 1;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x0017504E File Offset: 0x0017324E
		public override int ReadByte()
		{
			if (this._eofOn00 && this.CheckForEof())
			{
				return -1;
			}
			int lookAhead = this._lookAhead;
			this._lookAhead = this.RequireByte();
			return lookAhead;
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x00175074 File Offset: 0x00173274
		private int RequireByte()
		{
			int num = this._in.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return num;
		}

		// Token: 0x040025CB RID: 9675
		private int _lookAhead;

		// Token: 0x040025CC RID: 9676
		private bool _eofOn00 = true;
	}
}
