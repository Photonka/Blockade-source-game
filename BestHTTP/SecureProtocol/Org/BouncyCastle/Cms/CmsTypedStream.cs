using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005EE RID: 1518
	public class CmsTypedStream
	{
		// Token: 0x06003A22 RID: 14882 RVA: 0x0016CD97 File Offset: 0x0016AF97
		public CmsTypedStream(Stream inStream) : this(PkcsObjectIdentifiers.Data.Id, inStream, 32768)
		{
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x0016CDAF File Offset: 0x0016AFAF
		public CmsTypedStream(string oid, Stream inStream) : this(oid, inStream, 32768)
		{
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x0016CDBE File Offset: 0x0016AFBE
		public CmsTypedStream(string oid, Stream inStream, int bufSize)
		{
			this._oid = oid;
			this._in = new CmsTypedStream.FullReaderStream(new BufferedStream(inStream, bufSize));
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x0016CDDF File Offset: 0x0016AFDF
		public string ContentType
		{
			get
			{
				return this._oid;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x0016CDE7 File Offset: 0x0016AFE7
		public Stream ContentStream
		{
			get
			{
				return this._in;
			}
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x0016CDEF File Offset: 0x0016AFEF
		public void Drain()
		{
			Streams.Drain(this._in);
			Platform.Dispose(this._in);
		}

		// Token: 0x04002515 RID: 9493
		private const int BufferSize = 32768;

		// Token: 0x04002516 RID: 9494
		private readonly string _oid;

		// Token: 0x04002517 RID: 9495
		private readonly Stream _in;

		// Token: 0x02000962 RID: 2402
		private class FullReaderStream : FilterStream
		{
			// Token: 0x06004F06 RID: 20230 RVA: 0x00173F24 File Offset: 0x00172124
			internal FullReaderStream(Stream input) : base(input)
			{
			}

			// Token: 0x06004F07 RID: 20231 RVA: 0x001B77E0 File Offset: 0x001B59E0
			public override int Read(byte[] buf, int off, int len)
			{
				return Streams.ReadFully(this.s, buf, off, len);
			}
		}
	}
}
