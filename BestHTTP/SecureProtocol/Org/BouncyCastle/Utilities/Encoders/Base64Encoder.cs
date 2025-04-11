using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000273 RID: 627
	public class Base64Encoder : IEncoder
	{
		// Token: 0x06001754 RID: 5972 RVA: 0x000B9FD4 File Offset: 0x000B81D4
		protected void InitialiseDecodingTable()
		{
			Arrays.Fill(this.decodingTable, byte.MaxValue);
			for (int i = 0; i < this.encodingTable.Length; i++)
			{
				this.decodingTable[(int)this.encodingTable[i]] = (byte)i;
			}
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x000BA015 File Offset: 0x000B8215
		public Base64Encoder()
		{
			this.InitialiseDecodingTable();
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000BA054 File Offset: 0x000B8254
		public int Encode(byte[] data, int off, int length, Stream outStream)
		{
			int num = length % 3;
			int num2 = length - num;
			for (int i = off; i < off + num2; i += 3)
			{
				int num3 = (int)(data[i] & byte.MaxValue);
				int num4 = (int)(data[i + 1] & byte.MaxValue);
				int num5 = (int)(data[i + 2] & byte.MaxValue);
				outStream.WriteByte(this.encodingTable[(int)((uint)num3 >> 2 & 63U)]);
				outStream.WriteByte(this.encodingTable[(num3 << 4 | (int)((uint)num4 >> 4)) & 63]);
				outStream.WriteByte(this.encodingTable[(num4 << 2 | (int)((uint)num5 >> 6)) & 63]);
				outStream.WriteByte(this.encodingTable[num5 & 63]);
			}
			switch (num)
			{
			case 1:
			{
				byte b = data[off + num2] & byte.MaxValue;
				int num6 = b >> 2 & 63;
				int num7 = (int)b << 4 & 63;
				outStream.WriteByte(this.encodingTable[num6]);
				outStream.WriteByte(this.encodingTable[num7]);
				outStream.WriteByte(this.padding);
				outStream.WriteByte(this.padding);
				break;
			}
			case 2:
			{
				byte b2 = data[off + num2] & byte.MaxValue;
				int num8 = (int)(data[off + num2 + 1] & byte.MaxValue);
				int num6 = b2 >> 2 & 63;
				int num7 = ((int)b2 << 4 | num8 >> 4) & 63;
				int num9 = num8 << 2 & 63;
				outStream.WriteByte(this.encodingTable[num6]);
				outStream.WriteByte(this.encodingTable[num7]);
				outStream.WriteByte(this.encodingTable[num9]);
				outStream.WriteByte(this.padding);
				break;
			}
			}
			return num2 / 3 * 4 + ((num == 0) ? 0 : 4);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000BA1ED File Offset: 0x000B83ED
		private bool ignore(char c)
		{
			return c == '\n' || c == '\r' || c == '\t' || c == ' ';
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000BA208 File Offset: 0x000B8408
		public int Decode(byte[] data, int off, int length, Stream outStream)
		{
			int num = 0;
			int num2 = off + length;
			while (num2 > off && this.ignore((char)data[num2 - 1]))
			{
				num2--;
			}
			int num3 = num2 - 4;
			for (int i = this.nextI(data, off, num3); i < num3; i = this.nextI(data, i, num3))
			{
				byte b = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b2 = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b3 = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b4 = this.decodingTable[(int)data[i++]];
				if ((b | b2 | b3 | b4) >= 128)
				{
					throw new IOException("invalid characters encountered in base64 data");
				}
				outStream.WriteByte((byte)((int)b << 2 | b2 >> 4));
				outStream.WriteByte((byte)((int)b2 << 4 | b3 >> 2));
				outStream.WriteByte((byte)((int)b3 << 6 | (int)b4));
				num += 3;
			}
			return num + this.decodeLastBlock(outStream, (char)data[num2 - 4], (char)data[num2 - 3], (char)data[num2 - 2], (char)data[num2 - 1]);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000BA345 File Offset: 0x000B8545
		private int nextI(byte[] data, int i, int finish)
		{
			while (i < finish && this.ignore((char)data[i]))
			{
				i++;
			}
			return i;
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000BA360 File Offset: 0x000B8560
		public int DecodeString(string data, Stream outStream)
		{
			int num = 0;
			int num2 = data.Length;
			while (num2 > 0 && this.ignore(data[num2 - 1]))
			{
				num2--;
			}
			int i = 0;
			int num3 = num2 - 4;
			for (i = this.nextI(data, i, num3); i < num3; i = this.nextI(data, i, num3))
			{
				byte b = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b2 = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b3 = this.decodingTable[(int)data[i++]];
				i = this.nextI(data, i, num3);
				byte b4 = this.decodingTable[(int)data[i++]];
				if ((b | b2 | b3 | b4) >= 128)
				{
					throw new IOException("invalid characters encountered in base64 data");
				}
				outStream.WriteByte((byte)((int)b << 2 | b2 >> 4));
				outStream.WriteByte((byte)((int)b2 << 4 | b3 >> 2));
				outStream.WriteByte((byte)((int)b3 << 6 | (int)b4));
				num += 3;
			}
			return num + this.decodeLastBlock(outStream, data[num2 - 4], data[num2 - 3], data[num2 - 2], data[num2 - 1]);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000BA4C0 File Offset: 0x000B86C0
		private int decodeLastBlock(Stream outStream, char c1, char c2, char c3, char c4)
		{
			if (c3 == (char)this.padding)
			{
				if (c4 != (char)this.padding)
				{
					throw new IOException("invalid characters encountered at end of base64 data");
				}
				byte b = this.decodingTable[(int)c1];
				byte b2 = this.decodingTable[(int)c2];
				if ((b | b2) >= 128)
				{
					throw new IOException("invalid characters encountered at end of base64 data");
				}
				outStream.WriteByte((byte)((int)b << 2 | b2 >> 4));
				return 1;
			}
			else if (c4 == (char)this.padding)
			{
				byte b3 = this.decodingTable[(int)c1];
				byte b4 = this.decodingTable[(int)c2];
				byte b5 = this.decodingTable[(int)c3];
				if ((b3 | b4 | b5) >= 128)
				{
					throw new IOException("invalid characters encountered at end of base64 data");
				}
				outStream.WriteByte((byte)((int)b3 << 2 | b4 >> 4));
				outStream.WriteByte((byte)((int)b4 << 4 | b5 >> 2));
				return 2;
			}
			else
			{
				byte b6 = this.decodingTable[(int)c1];
				byte b7 = this.decodingTable[(int)c2];
				byte b8 = this.decodingTable[(int)c3];
				byte b9 = this.decodingTable[(int)c4];
				if ((b6 | b7 | b8 | b9) >= 128)
				{
					throw new IOException("invalid characters encountered at end of base64 data");
				}
				outStream.WriteByte((byte)((int)b6 << 2 | b7 >> 4));
				outStream.WriteByte((byte)((int)b7 << 4 | b8 >> 2));
				outStream.WriteByte((byte)((int)b8 << 6 | (int)b9));
				return 3;
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x000BA5F7 File Offset: 0x000B87F7
		private int nextI(string data, int i, int finish)
		{
			while (i < finish && this.ignore(data[i]))
			{
				i++;
			}
			return i;
		}

		// Token: 0x040016DE RID: 5854
		protected readonly byte[] encodingTable = new byte[]
		{
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			43,
			47
		};

		// Token: 0x040016DF RID: 5855
		protected byte padding = 61;

		// Token: 0x040016E0 RID: 5856
		protected readonly byte[] decodingTable = new byte[128];
	}
}
