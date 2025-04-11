using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D6 RID: 1494
	public class CmsCompressedDataStreamGenerator
	{
		// Token: 0x06003940 RID: 14656 RVA: 0x001691B8 File Offset: 0x001673B8
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x001691C1 File Offset: 0x001673C1
		public Stream Open(Stream outStream, string compressionOID)
		{
			return this.Open(outStream, CmsObjectIdentifiers.Data.Id, compressionOID);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x001691D8 File Offset: 0x001673D8
		public Stream Open(Stream outStream, string contentOID, string compressionOID)
		{
			BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
			berSequenceGenerator.AddObject(CmsObjectIdentifiers.CompressedData);
			BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
			berSequenceGenerator2.AddObject(new DerInteger(0));
			berSequenceGenerator2.AddObject(new AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.9.16.3.8")));
			BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(berSequenceGenerator2.GetRawOutputStream());
			berSequenceGenerator3.AddObject(new DerObjectIdentifier(contentOID));
			return new CmsCompressedDataStreamGenerator.CmsCompressedOutputStream(new ZOutputStream(CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, true, this._bufferSize), -1), berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
		}

		// Token: 0x0400249B RID: 9371
		public const string ZLib = "1.2.840.113549.1.9.16.3.8";

		// Token: 0x0400249C RID: 9372
		private int _bufferSize;

		// Token: 0x0200095A RID: 2394
		private class CmsCompressedOutputStream : BaseOutputStream
		{
			// Token: 0x06004EE6 RID: 20198 RVA: 0x001B6AE8 File Offset: 0x001B4CE8
			internal CmsCompressedOutputStream(ZOutputStream outStream, BerSequenceGenerator sGen, BerSequenceGenerator cGen, BerSequenceGenerator eiGen)
			{
				this._out = outStream;
				this._sGen = sGen;
				this._cGen = cGen;
				this._eiGen = eiGen;
			}

			// Token: 0x06004EE7 RID: 20199 RVA: 0x001B6B0D File Offset: 0x001B4D0D
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x06004EE8 RID: 20200 RVA: 0x001B6B1B File Offset: 0x001B4D1B
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x06004EE9 RID: 20201 RVA: 0x001B6B2B File Offset: 0x001B4D2B
			public override void Close()
			{
				Platform.Dispose(this._out);
				this._eiGen.Close();
				this._cGen.Close();
				this._sGen.Close();
				base.Close();
			}

			// Token: 0x040035B0 RID: 13744
			private ZOutputStream _out;

			// Token: 0x040035B1 RID: 13745
			private BerSequenceGenerator _sGen;

			// Token: 0x040035B2 RID: 13746
			private BerSequenceGenerator _cGen;

			// Token: 0x040035B3 RID: 13747
			private BerSequenceGenerator _eiGen;
		}
	}
}
