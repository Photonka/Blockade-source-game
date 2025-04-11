using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000630 RID: 1584
	internal class ConstructedOctetStream : BaseInputStream
	{
		// Token: 0x06003B99 RID: 15257 RVA: 0x001719D0 File Offset: 0x0016FBD0
		internal ConstructedOctetStream(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x001719E8 File Offset: 0x0016FBE8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._currentStream == null)
			{
				if (!this._first)
				{
					return 0;
				}
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)this._parser.ReadObject();
				if (asn1OctetStringParser == null)
				{
					return 0;
				}
				this._first = false;
				this._currentStream = asn1OctetStringParser.GetOctetStream();
			}
			int num = 0;
			for (;;)
			{
				int num2 = this._currentStream.Read(buffer, offset + num, count - num);
				if (num2 > 0)
				{
					num += num2;
					if (num == count)
					{
						break;
					}
				}
				else
				{
					Asn1OctetStringParser asn1OctetStringParser2 = (Asn1OctetStringParser)this._parser.ReadObject();
					if (asn1OctetStringParser2 == null)
					{
						goto Block_6;
					}
					this._currentStream = asn1OctetStringParser2.GetOctetStream();
				}
			}
			return num;
			Block_6:
			this._currentStream = null;
			return num;
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x00171A80 File Offset: 0x0016FC80
		public override int ReadByte()
		{
			if (this._currentStream == null)
			{
				if (!this._first)
				{
					return 0;
				}
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)this._parser.ReadObject();
				if (asn1OctetStringParser == null)
				{
					return 0;
				}
				this._first = false;
				this._currentStream = asn1OctetStringParser.GetOctetStream();
			}
			int num;
			for (;;)
			{
				num = this._currentStream.ReadByte();
				if (num >= 0)
				{
					break;
				}
				Asn1OctetStringParser asn1OctetStringParser2 = (Asn1OctetStringParser)this._parser.ReadObject();
				if (asn1OctetStringParser2 == null)
				{
					goto Block_5;
				}
				this._currentStream = asn1OctetStringParser2.GetOctetStream();
			}
			return num;
			Block_5:
			this._currentStream = null;
			return -1;
		}

		// Token: 0x04002594 RID: 9620
		private readonly Asn1StreamParser _parser;

		// Token: 0x04002595 RID: 9621
		private bool _first = true;

		// Token: 0x04002596 RID: 9622
		private Stream _currentStream;
	}
}
