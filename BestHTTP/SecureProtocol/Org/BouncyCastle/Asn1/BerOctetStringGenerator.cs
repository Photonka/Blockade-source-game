using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000625 RID: 1573
	public class BerOctetStringGenerator : BerGenerator
	{
		// Token: 0x06003B6C RID: 15212 RVA: 0x00171428 File Offset: 0x0016F628
		public BerOctetStringGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(36);
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x00171439 File Offset: 0x0016F639
		public BerOctetStringGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(36);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x0017144C File Offset: 0x0016F64C
		public Stream GetOctetOutputStream()
		{
			return this.GetOctetOutputStream(new byte[1000]);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x0017145E File Offset: 0x0016F65E
		public Stream GetOctetOutputStream(int bufSize)
		{
			if (bufSize >= 1)
			{
				return this.GetOctetOutputStream(new byte[bufSize]);
			}
			return this.GetOctetOutputStream();
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x00171477 File Offset: 0x0016F677
		public Stream GetOctetOutputStream(byte[] buf)
		{
			return new BerOctetStringGenerator.BufferedBerOctetStream(this, buf);
		}

		// Token: 0x02000966 RID: 2406
		private class BufferedBerOctetStream : BaseOutputStream
		{
			// Token: 0x06004F11 RID: 20241 RVA: 0x001B7991 File Offset: 0x001B5B91
			internal BufferedBerOctetStream(BerOctetStringGenerator gen, byte[] buf)
			{
				this._gen = gen;
				this._buf = buf;
				this._off = 0;
				this._derOut = new DerOutputStream(this._gen.Out);
			}

			// Token: 0x06004F12 RID: 20242 RVA: 0x001B79C4 File Offset: 0x001B5BC4
			public override void WriteByte(byte b)
			{
				byte[] buf = this._buf;
				int off = this._off;
				this._off = off + 1;
				buf[off] = b;
				if (this._off == this._buf.Length)
				{
					DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
					this._off = 0;
				}
			}

			// Token: 0x06004F13 RID: 20243 RVA: 0x001B7A1C File Offset: 0x001B5C1C
			public override void Write(byte[] buf, int offset, int len)
			{
				while (len > 0)
				{
					int num = Math.Min(len, this._buf.Length - this._off);
					if (num == this._buf.Length)
					{
						DerOctetString.Encode(this._derOut, buf, offset, num);
					}
					else
					{
						Array.Copy(buf, offset, this._buf, this._off, num);
						this._off += num;
						if (this._off < this._buf.Length)
						{
							break;
						}
						DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
						this._off = 0;
					}
					offset += num;
					len -= num;
				}
			}

			// Token: 0x06004F14 RID: 20244 RVA: 0x001B7AC1 File Offset: 0x001B5CC1
			public override void Close()
			{
				if (this._off != 0)
				{
					DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
				}
				this._gen.WriteBerEnd();
				base.Close();
			}

			// Token: 0x040035DD RID: 13789
			private byte[] _buf;

			// Token: 0x040035DE RID: 13790
			private int _off;

			// Token: 0x040035DF RID: 13791
			private readonly BerOctetStringGenerator _gen;

			// Token: 0x040035E0 RID: 13792
			private readonly DerOutputStream _derOut;
		}
	}
}
