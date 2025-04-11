using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000622 RID: 1570
	public class BerGenerator : Asn1Generator
	{
		// Token: 0x06003B54 RID: 15188 RVA: 0x0017109D File Offset: 0x0016F29D
		protected BerGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x001710A6 File Offset: 0x0016F2A6
		public BerGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream)
		{
			this._tagged = true;
			this._isExplicit = isExplicit;
			this._tagNo = tagNo;
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x001710C4 File Offset: 0x0016F2C4
		public override void AddObject(Asn1Encodable obj)
		{
			new BerOutputStream(base.Out).WriteObject(obj);
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x001710D7 File Offset: 0x0016F2D7
		public override Stream GetRawOutputStream()
		{
			return base.Out;
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x001710DF File Offset: 0x0016F2DF
		public override void Close()
		{
			this.WriteBerEnd();
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x001710E7 File Offset: 0x0016F2E7
		private void WriteHdr(int tag)
		{
			base.Out.WriteByte((byte)tag);
			base.Out.WriteByte(128);
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x00171108 File Offset: 0x0016F308
		protected void WriteBerHeader(int tag)
		{
			if (!this._tagged)
			{
				this.WriteHdr(tag);
				return;
			}
			int num = this._tagNo | 128;
			if (this._isExplicit)
			{
				this.WriteHdr(num | 32);
				this.WriteHdr(tag);
				return;
			}
			if ((tag & 32) != 0)
			{
				this.WriteHdr(num | 32);
				return;
			}
			this.WriteHdr(num);
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x00171164 File Offset: 0x0016F364
		protected void WriteBerBody(Stream contentStream)
		{
			Streams.PipeAll(contentStream, base.Out);
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x00171174 File Offset: 0x0016F374
		protected void WriteBerEnd()
		{
			base.Out.WriteByte(0);
			base.Out.WriteByte(0);
			if (this._tagged && this._isExplicit)
			{
				base.Out.WriteByte(0);
				base.Out.WriteByte(0);
			}
		}

		// Token: 0x04002586 RID: 9606
		private bool _tagged;

		// Token: 0x04002587 RID: 9607
		private bool _isExplicit;

		// Token: 0x04002588 RID: 9608
		private int _tagNo;
	}
}
