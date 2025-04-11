using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200063B RID: 1595
	public abstract class DerGenerator : Asn1Generator
	{
		// Token: 0x06003C0E RID: 15374 RVA: 0x0017109D File Offset: 0x0016F29D
		protected DerGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x00173222 File Offset: 0x00171422
		protected DerGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream)
		{
			this._tagged = true;
			this._isExplicit = isExplicit;
			this._tagNo = tagNo;
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x00173240 File Offset: 0x00171440
		private static void WriteLength(Stream outStr, int length)
		{
			if (length > 127)
			{
				int num = 1;
				int num2 = length;
				while ((num2 >>= 8) != 0)
				{
					num++;
				}
				outStr.WriteByte((byte)(num | 128));
				for (int i = (num - 1) * 8; i >= 0; i -= 8)
				{
					outStr.WriteByte((byte)(length >> i));
				}
				return;
			}
			outStr.WriteByte((byte)length);
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00173297 File Offset: 0x00171497
		internal static void WriteDerEncoded(Stream outStream, int tag, byte[] bytes)
		{
			outStream.WriteByte((byte)tag);
			DerGenerator.WriteLength(outStream, bytes.Length);
			outStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x001732B8 File Offset: 0x001714B8
		internal void WriteDerEncoded(int tag, byte[] bytes)
		{
			if (!this._tagged)
			{
				DerGenerator.WriteDerEncoded(base.Out, tag, bytes);
				return;
			}
			int num = this._tagNo | 128;
			if (this._isExplicit)
			{
				int tag2 = this._tagNo | 32 | 128;
				MemoryStream memoryStream = new MemoryStream();
				DerGenerator.WriteDerEncoded(memoryStream, tag, bytes);
				DerGenerator.WriteDerEncoded(base.Out, tag2, memoryStream.ToArray());
				return;
			}
			if ((tag & 32) != 0)
			{
				num |= 32;
			}
			DerGenerator.WriteDerEncoded(base.Out, num, bytes);
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00173339 File Offset: 0x00171539
		internal static void WriteDerEncoded(Stream outStr, int tag, Stream inStr)
		{
			DerGenerator.WriteDerEncoded(outStr, tag, Streams.ReadAll(inStr));
		}

		// Token: 0x040025AE RID: 9646
		private bool _tagged;

		// Token: 0x040025AF RID: 9647
		private bool _isExplicit;

		// Token: 0x040025B0 RID: 9648
		private int _tagNo;
	}
}
