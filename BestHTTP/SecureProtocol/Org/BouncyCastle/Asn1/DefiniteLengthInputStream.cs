using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000631 RID: 1585
	internal class DefiniteLengthInputStream : LimitedInputStream
	{
		// Token: 0x06003B9C RID: 15260 RVA: 0x00171B04 File Offset: 0x0016FD04
		internal DefiniteLengthInputStream(Stream inStream, int length) : base(inStream, length)
		{
			if (length < 0)
			{
				throw new ArgumentException("negative lengths not allowed", "length");
			}
			this._originalLength = length;
			this._remaining = length;
			if (length == 0)
			{
				this.SetParentEofDetect(true);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x00171B3A File Offset: 0x0016FD3A
		internal int Remaining
		{
			get
			{
				return this._remaining;
			}
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x00171B44 File Offset: 0x0016FD44
		public override int ReadByte()
		{
			if (this._remaining == 0)
			{
				return -1;
			}
			int num = this._in.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			int num2 = this._remaining - 1;
			this._remaining = num2;
			if (num2 == 0)
			{
				this.SetParentEofDetect(true);
			}
			return num;
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x00171BC4 File Offset: 0x0016FDC4
		public override int Read(byte[] buf, int off, int len)
		{
			if (this._remaining == 0)
			{
				return 0;
			}
			int count = Math.Min(len, this._remaining);
			int num = this._in.Read(buf, off, count);
			if (num < 1)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			if ((this._remaining -= num) == 0)
			{
				this.SetParentEofDetect(true);
			}
			return num;
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x00171C54 File Offset: 0x0016FE54
		internal void ReadAllIntoByteArray(byte[] buf)
		{
			if (this._remaining != buf.Length)
			{
				throw new ArgumentException("buffer length not right for data");
			}
			if ((this._remaining -= Streams.ReadFully(this._in, buf)) != 0)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			this.SetParentEofDetect(true);
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x00171CDC File Offset: 0x0016FEDC
		internal byte[] ToArray()
		{
			if (this._remaining == 0)
			{
				return DefiniteLengthInputStream.EmptyBytes;
			}
			byte[] array = new byte[this._remaining];
			if ((this._remaining -= Streams.ReadFully(this._in, array)) != 0)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			this.SetParentEofDetect(true);
			return array;
		}

		// Token: 0x04002597 RID: 9623
		private static readonly byte[] EmptyBytes = new byte[0];

		// Token: 0x04002598 RID: 9624
		private readonly int _originalLength;

		// Token: 0x04002599 RID: 9625
		private int _remaining;
	}
}
