using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000478 RID: 1144
	public abstract class TlsUtilities
	{
		// Token: 0x06002D01 RID: 11521 RVA: 0x0011D682 File Offset: 0x0011B882
		public static void CheckUint8(int i)
		{
			if (!TlsUtilities.IsValidUint8(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0011D694 File Offset: 0x0011B894
		public static void CheckUint8(long i)
		{
			if (!TlsUtilities.IsValidUint8(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0011D6A6 File Offset: 0x0011B8A6
		public static void CheckUint16(int i)
		{
			if (!TlsUtilities.IsValidUint16(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0011D6B8 File Offset: 0x0011B8B8
		public static void CheckUint16(long i)
		{
			if (!TlsUtilities.IsValidUint16(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0011D6CA File Offset: 0x0011B8CA
		public static void CheckUint24(int i)
		{
			if (!TlsUtilities.IsValidUint24(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0011D6DC File Offset: 0x0011B8DC
		public static void CheckUint24(long i)
		{
			if (!TlsUtilities.IsValidUint24(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0011D6EE File Offset: 0x0011B8EE
		public static void CheckUint32(long i)
		{
			if (!TlsUtilities.IsValidUint32(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x0011D700 File Offset: 0x0011B900
		public static void CheckUint48(long i)
		{
			if (!TlsUtilities.IsValidUint48(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0011D712 File Offset: 0x0011B912
		public static void CheckUint64(long i)
		{
			if (!TlsUtilities.IsValidUint64(i))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x0011D724 File Offset: 0x0011B924
		public static bool IsValidUint8(int i)
		{
			return (i & 255) == i;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0011D730 File Offset: 0x0011B930
		public static bool IsValidUint8(long i)
		{
			return (i & 255L) == i;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0011D73D File Offset: 0x0011B93D
		public static bool IsValidUint16(int i)
		{
			return (i & 65535) == i;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0011D749 File Offset: 0x0011B949
		public static bool IsValidUint16(long i)
		{
			return (i & 65535L) == i;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0011D756 File Offset: 0x0011B956
		public static bool IsValidUint24(int i)
		{
			return (i & 16777215) == i;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0011D762 File Offset: 0x0011B962
		public static bool IsValidUint24(long i)
		{
			return (i & 16777215L) == i;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0011D76F File Offset: 0x0011B96F
		public static bool IsValidUint32(long i)
		{
			return (i & (long)((ulong)-1)) == i;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x0011D778 File Offset: 0x0011B978
		public static bool IsValidUint48(long i)
		{
			return (i & 281474976710655L) == i;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x0006CF70 File Offset: 0x0006B170
		public static bool IsValidUint64(long i)
		{
			return true;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0011D788 File Offset: 0x0011B988
		public static bool IsSsl(TlsContext context)
		{
			return context.ServerVersion.IsSsl;
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0011D795 File Offset: 0x0011B995
		public static bool IsTlsV11(ProtocolVersion version)
		{
			return ProtocolVersion.TLSv11.IsEqualOrEarlierVersionOf(version.GetEquivalentTLSVersion());
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x0011D7A7 File Offset: 0x0011B9A7
		public static bool IsTlsV11(TlsContext context)
		{
			return TlsUtilities.IsTlsV11(context.ServerVersion);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0011D7B4 File Offset: 0x0011B9B4
		public static bool IsTlsV12(ProtocolVersion version)
		{
			return ProtocolVersion.TLSv12.IsEqualOrEarlierVersionOf(version.GetEquivalentTLSVersion());
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x0011D7C6 File Offset: 0x0011B9C6
		public static bool IsTlsV12(TlsContext context)
		{
			return TlsUtilities.IsTlsV12(context.ServerVersion);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x0011D7D3 File Offset: 0x0011B9D3
		public static void WriteUint8(byte i, Stream output)
		{
			output.WriteByte(i);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x0011D7DC File Offset: 0x0011B9DC
		public static void WriteUint8(byte i, byte[] buf, int offset)
		{
			buf[offset] = i;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x0011D7E2 File Offset: 0x0011B9E2
		public static void WriteUint16(int i, Stream output)
		{
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x0010E4EB File Offset: 0x0010C6EB
		public static void WriteUint16(int i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 8);
			buf[offset + 1] = (byte)i;
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x0011D7F6 File Offset: 0x0011B9F6
		public static void WriteUint24(int i, Stream output)
		{
			output.WriteByte((byte)(i >> 16));
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0011D815 File Offset: 0x0011BA15
		public static void WriteUint24(int i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 16);
			buf[offset + 1] = (byte)(i >> 8);
			buf[offset + 2] = (byte)i;
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x0011D82F File Offset: 0x0011BA2F
		public static void WriteUint32(long i, Stream output)
		{
			output.WriteByte((byte)(i >> 24));
			output.WriteByte((byte)(i >> 16));
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x0011D859 File Offset: 0x0011BA59
		public static void WriteUint32(long i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 24);
			buf[offset + 1] = (byte)(i >> 16);
			buf[offset + 2] = (byte)(i >> 8);
			buf[offset + 3] = (byte)i;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x0011D87D File Offset: 0x0011BA7D
		public static void WriteUint48(long i, Stream output)
		{
			output.WriteByte((byte)(i >> 40));
			output.WriteByte((byte)(i >> 32));
			output.WriteByte((byte)(i >> 24));
			output.WriteByte((byte)(i >> 16));
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x0011D8BD File Offset: 0x0011BABD
		public static void WriteUint48(long i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 40);
			buf[offset + 1] = (byte)(i >> 32);
			buf[offset + 2] = (byte)(i >> 24);
			buf[offset + 3] = (byte)(i >> 16);
			buf[offset + 4] = (byte)(i >> 8);
			buf[offset + 5] = (byte)i;
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x0011D8F8 File Offset: 0x0011BAF8
		public static void WriteUint64(long i, Stream output)
		{
			output.WriteByte((byte)(i >> 56));
			output.WriteByte((byte)(i >> 48));
			output.WriteByte((byte)(i >> 40));
			output.WriteByte((byte)(i >> 32));
			output.WriteByte((byte)(i >> 24));
			output.WriteByte((byte)(i >> 16));
			output.WriteByte((byte)(i >> 8));
			output.WriteByte((byte)i);
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x0011D95C File Offset: 0x0011BB5C
		public static void WriteUint64(long i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 56);
			buf[offset + 1] = (byte)(i >> 48);
			buf[offset + 2] = (byte)(i >> 40);
			buf[offset + 3] = (byte)(i >> 32);
			buf[offset + 4] = (byte)(i >> 24);
			buf[offset + 5] = (byte)(i >> 16);
			buf[offset + 6] = (byte)(i >> 8);
			buf[offset + 7] = (byte)i;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x0011D9B3 File Offset: 0x0011BBB3
		public static void WriteOpaque8(byte[] buf, Stream output)
		{
			TlsUtilities.WriteUint8((byte)buf.Length, output);
			output.Write(buf, 0, buf.Length);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x0011D9CA File Offset: 0x0011BBCA
		public static void WriteOpaque16(byte[] buf, Stream output)
		{
			TlsUtilities.WriteUint16(buf.Length, output);
			output.Write(buf, 0, buf.Length);
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x0011D9E0 File Offset: 0x0011BBE0
		public static void WriteOpaque24(byte[] buf, Stream output)
		{
			TlsUtilities.WriteUint24(buf.Length, output);
			output.Write(buf, 0, buf.Length);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x0011D9F6 File Offset: 0x0011BBF6
		public static void WriteUint8Array(byte[] uints, Stream output)
		{
			output.Write(uints, 0, uints.Length);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x0011DA04 File Offset: 0x0011BC04
		public static void WriteUint8Array(byte[] uints, byte[] buf, int offset)
		{
			for (int i = 0; i < uints.Length; i++)
			{
				TlsUtilities.WriteUint8(uints[i], buf, offset);
				offset++;
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x0011DA2E File Offset: 0x0011BC2E
		public static void WriteUint8ArrayWithUint8Length(byte[] uints, Stream output)
		{
			TlsUtilities.CheckUint8(uints.Length);
			TlsUtilities.WriteUint8((byte)uints.Length, output);
			TlsUtilities.WriteUint8Array(uints, output);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x0011DA49 File Offset: 0x0011BC49
		public static void WriteUint8ArrayWithUint8Length(byte[] uints, byte[] buf, int offset)
		{
			TlsUtilities.CheckUint8(uints.Length);
			TlsUtilities.WriteUint8((byte)uints.Length, buf, offset);
			TlsUtilities.WriteUint8Array(uints, buf, offset + 1);
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x0011DA68 File Offset: 0x0011BC68
		public static void WriteUint16Array(int[] uints, Stream output)
		{
			for (int i = 0; i < uints.Length; i++)
			{
				TlsUtilities.WriteUint16(uints[i], output);
			}
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x0011DA8C File Offset: 0x0011BC8C
		public static void WriteUint16Array(int[] uints, byte[] buf, int offset)
		{
			for (int i = 0; i < uints.Length; i++)
			{
				TlsUtilities.WriteUint16(uints[i], buf, offset);
				offset += 2;
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x0011DAB6 File Offset: 0x0011BCB6
		public static void WriteUint16ArrayWithUint16Length(int[] uints, Stream output)
		{
			int i = 2 * uints.Length;
			TlsUtilities.CheckUint16(i);
			TlsUtilities.WriteUint16(i, output);
			TlsUtilities.WriteUint16Array(uints, output);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x0011DAD0 File Offset: 0x0011BCD0
		public static void WriteUint16ArrayWithUint16Length(int[] uints, byte[] buf, int offset)
		{
			int i = 2 * uints.Length;
			TlsUtilities.CheckUint16(i);
			TlsUtilities.WriteUint16(i, buf, offset);
			TlsUtilities.WriteUint16Array(uints, buf, offset + 2);
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x0011DAEE File Offset: 0x0011BCEE
		public static byte DecodeUint8(byte[] buf)
		{
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			if (buf.Length != 1)
			{
				throw new TlsFatalAlert(50);
			}
			return TlsUtilities.ReadUint8(buf, 0);
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x0011DB14 File Offset: 0x0011BD14
		public static byte[] DecodeUint8ArrayWithUint8Length(byte[] buf)
		{
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			int num = (int)TlsUtilities.ReadUint8(buf, 0);
			if (buf.Length != num + 1)
			{
				throw new TlsFatalAlert(50);
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = TlsUtilities.ReadUint8(buf, i + 1);
			}
			return array;
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x0011DB67 File Offset: 0x0011BD67
		public static byte[] EncodeOpaque8(byte[] buf)
		{
			TlsUtilities.CheckUint8(buf.Length);
			return Arrays.Prepend(buf, (byte)buf.Length);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x0011DB7C File Offset: 0x0011BD7C
		public static byte[] EncodeUint8(byte val)
		{
			TlsUtilities.CheckUint8((int)val);
			byte[] array = new byte[1];
			TlsUtilities.WriteUint8(val, array, 0);
			return array;
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0011DBA0 File Offset: 0x0011BDA0
		public static byte[] EncodeUint8ArrayWithUint8Length(byte[] uints)
		{
			byte[] array = new byte[1 + uints.Length];
			TlsUtilities.WriteUint8ArrayWithUint8Length(uints, array, 0);
			return array;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0011DBC4 File Offset: 0x0011BDC4
		public static byte[] EncodeUint16ArrayWithUint16Length(int[] uints)
		{
			int num = 2 * uints.Length;
			byte[] array = new byte[2 + num];
			TlsUtilities.WriteUint16ArrayWithUint16Length(uints, array, 0);
			return array;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x0011DBE9 File Offset: 0x0011BDE9
		public static byte ReadUint8(Stream input)
		{
			int num = input.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return (byte)num;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x0011DBFC File Offset: 0x0011BDFC
		public static byte ReadUint8(byte[] buf, int offset)
		{
			return buf[offset];
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x0011DC04 File Offset: 0x0011BE04
		public static int ReadUint16(Stream input)
		{
			int num = input.ReadByte();
			int num2 = input.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException();
			}
			return num << 8 | num2;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x0011DC2C File Offset: 0x0011BE2C
		public static int ReadUint16(byte[] buf, int offset)
		{
			return (int)buf[offset] << 8 | (int)buf[++offset];
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x0011DC3C File Offset: 0x0011BE3C
		public static int ReadUint24(Stream input)
		{
			int num = input.ReadByte();
			int num2 = input.ReadByte();
			int num3 = input.ReadByte();
			if (num3 < 0)
			{
				throw new EndOfStreamException();
			}
			return num << 16 | num2 << 8 | num3;
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0011DC70 File Offset: 0x0011BE70
		public static int ReadUint24(byte[] buf, int offset)
		{
			return (int)buf[offset] << 16 | (int)buf[++offset] << 8 | (int)buf[++offset];
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x0011DC8C File Offset: 0x0011BE8C
		public static long ReadUint32(Stream input)
		{
			int num = input.ReadByte();
			int num2 = input.ReadByte();
			int num3 = input.ReadByte();
			int num4 = input.ReadByte();
			if (num4 < 0)
			{
				throw new EndOfStreamException();
			}
			return (long)((ulong)(num << 24 | num2 << 16 | num3 << 8 | num4));
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0011DCCD File Offset: 0x0011BECD
		public static long ReadUint32(byte[] buf, int offset)
		{
			return (long)((ulong)((int)buf[offset] << 24 | (int)buf[++offset] << 16 | (int)buf[++offset] << 8 | (int)buf[++offset]));
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x0011DCF8 File Offset: 0x0011BEF8
		public static long ReadUint48(Stream input)
		{
			long num = (long)TlsUtilities.ReadUint24(input);
			int num2 = TlsUtilities.ReadUint24(input);
			return (num & (long)((ulong)-1)) << 24 | ((long)num2 & (long)((ulong)-1));
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x0011DD20 File Offset: 0x0011BF20
		public static long ReadUint48(byte[] buf, int offset)
		{
			long num = (long)TlsUtilities.ReadUint24(buf, offset);
			int num2 = TlsUtilities.ReadUint24(buf, offset + 3);
			return (num & (long)((ulong)-1)) << 24 | ((long)num2 & (long)((ulong)-1));
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x0011DD4C File Offset: 0x0011BF4C
		public static byte[] ReadAllOrNothing(int length, Stream input)
		{
			if (length < 1)
			{
				return TlsUtilities.EmptyBytes;
			}
			byte[] array = new byte[length];
			int num = Streams.ReadFully(input, array);
			if (num == 0)
			{
				return null;
			}
			if (num != length)
			{
				throw new EndOfStreamException();
			}
			return array;
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x0011DD84 File Offset: 0x0011BF84
		public static byte[] ReadFully(int length, Stream input)
		{
			if (length < 1)
			{
				return TlsUtilities.EmptyBytes;
			}
			byte[] array = new byte[length];
			if (length != Streams.ReadFully(input, array))
			{
				throw new EndOfStreamException();
			}
			return array;
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0011DDB3 File Offset: 0x0011BFB3
		public static void ReadFully(byte[] buf, Stream input)
		{
			if (Streams.ReadFully(input, buf, 0, buf.Length) < buf.Length)
			{
				throw new EndOfStreamException();
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x0011DDCB File Offset: 0x0011BFCB
		public static byte[] ReadOpaque8(Stream input)
		{
			byte[] array = new byte[(int)TlsUtilities.ReadUint8(input)];
			TlsUtilities.ReadFully(array, input);
			return array;
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x0011DDDF File Offset: 0x0011BFDF
		public static byte[] ReadOpaque16(Stream input)
		{
			byte[] array = new byte[TlsUtilities.ReadUint16(input)];
			TlsUtilities.ReadFully(array, input);
			return array;
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x0011DDF3 File Offset: 0x0011BFF3
		public static byte[] ReadOpaque24(Stream input)
		{
			return TlsUtilities.ReadFully(TlsUtilities.ReadUint24(input), input);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x0011DE04 File Offset: 0x0011C004
		public static byte[] ReadUint8Array(int count, Stream input)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = TlsUtilities.ReadUint8(input);
			}
			return array;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x0011DE30 File Offset: 0x0011C030
		public static int[] ReadUint16Array(int count, Stream input)
		{
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = TlsUtilities.ReadUint16(input);
			}
			return array;
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x0011DE5A File Offset: 0x0011C05A
		public static ProtocolVersion ReadVersion(byte[] buf, int offset)
		{
			return ProtocolVersion.Get((int)buf[offset], (int)buf[offset + 1]);
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0011DE6C File Offset: 0x0011C06C
		public static ProtocolVersion ReadVersion(Stream input)
		{
			int major = input.ReadByte();
			int num = input.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return ProtocolVersion.Get(major, num);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0011DE96 File Offset: 0x0011C096
		public static int ReadVersionRaw(byte[] buf, int offset)
		{
			return (int)buf[offset] << 8 | (int)buf[offset + 1];
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x0011DEA4 File Offset: 0x0011C0A4
		public static int ReadVersionRaw(Stream input)
		{
			int num = input.ReadByte();
			int num2 = input.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException();
			}
			return num << 8 | num2;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0011DECC File Offset: 0x0011C0CC
		public static Asn1Object ReadAsn1Object(byte[] encoding)
		{
			MemoryStream memoryStream = new MemoryStream(encoding, false);
			Asn1Object asn1Object = new Asn1InputStream(memoryStream, encoding.Length).ReadObject();
			if (asn1Object == null)
			{
				throw new TlsFatalAlert(50);
			}
			if (memoryStream.Position != memoryStream.Length)
			{
				throw new TlsFatalAlert(50);
			}
			return asn1Object;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x0011DF12 File Offset: 0x0011C112
		public static Asn1Object ReadDerObject(byte[] encoding)
		{
			Asn1Object asn1Object = TlsUtilities.ReadAsn1Object(encoding);
			if (!Arrays.AreEqual(asn1Object.GetEncoded("DER"), encoding))
			{
				throw new TlsFatalAlert(50);
			}
			return asn1Object;
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x0011DF38 File Offset: 0x0011C138
		public static void WriteGmtUnixTime(byte[] buf, int offset)
		{
			int num = (int)(DateTimeUtilities.CurrentUnixMs() / 1000L);
			buf[offset] = (byte)(num >> 24);
			buf[offset + 1] = (byte)(num >> 16);
			buf[offset + 2] = (byte)(num >> 8);
			buf[offset + 3] = (byte)num;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x0011DF75 File Offset: 0x0011C175
		public static void WriteVersion(ProtocolVersion version, Stream output)
		{
			output.WriteByte((byte)version.MajorVersion);
			output.WriteByte((byte)version.MinorVersion);
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x0011DF91 File Offset: 0x0011C191
		public static void WriteVersion(ProtocolVersion version, byte[] buf, int offset)
		{
			buf[offset] = (byte)version.MajorVersion;
			buf[offset + 1] = (byte)version.MinorVersion;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x0011DFA9 File Offset: 0x0011C1A9
		public static IList GetAllSignatureAlgorithms()
		{
			IList list = Platform.CreateArrayList(4);
			list.Add(0);
			list.Add(1);
			list.Add(2);
			list.Add(3);
			return list;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0011DFE5 File Offset: 0x0011C1E5
		public static IList GetDefaultDssSignatureAlgorithms()
		{
			return TlsUtilities.VectorOfOne(new SignatureAndHashAlgorithm(2, 2));
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x0011DFF3 File Offset: 0x0011C1F3
		public static IList GetDefaultECDsaSignatureAlgorithms()
		{
			return TlsUtilities.VectorOfOne(new SignatureAndHashAlgorithm(2, 3));
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0011E001 File Offset: 0x0011C201
		public static IList GetDefaultRsaSignatureAlgorithms()
		{
			return TlsUtilities.VectorOfOne(new SignatureAndHashAlgorithm(2, 1));
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0011E00F File Offset: 0x0011C20F
		public static byte[] GetExtensionData(IDictionary extensions, int extensionType)
		{
			if (extensions != null)
			{
				return (byte[])extensions[extensionType];
			}
			return null;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x0011E028 File Offset: 0x0011C228
		public static IList GetDefaultSupportedSignatureAlgorithms()
		{
			byte[] array = new byte[]
			{
				2,
				3,
				4,
				5,
				6
			};
			byte[] array2 = new byte[]
			{
				1,
				2,
				3
			};
			IList list = Platform.CreateArrayList();
			for (int i = 0; i < array2.Length; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					list.Add(new SignatureAndHashAlgorithm(array[j], array2[i]));
				}
			}
			return list;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x0011E094 File Offset: 0x0011C294
		public static SignatureAndHashAlgorithm GetSignatureAndHashAlgorithm(TlsContext context, TlsSignerCredentials signerCredentials)
		{
			SignatureAndHashAlgorithm signatureAndHashAlgorithm = null;
			if (TlsUtilities.IsTlsV12(context))
			{
				signatureAndHashAlgorithm = signerCredentials.SignatureAndHashAlgorithm;
				if (signatureAndHashAlgorithm == null)
				{
					throw new TlsFatalAlert(80);
				}
			}
			return signatureAndHashAlgorithm;
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x0011E0C0 File Offset: 0x0011C2C0
		public static bool HasExpectedEmptyExtensionData(IDictionary extensions, int extensionType, byte alertDescription)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, extensionType);
			if (extensionData == null)
			{
				return false;
			}
			if (extensionData.Length != 0)
			{
				throw new TlsFatalAlert(alertDescription);
			}
			return true;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x0011E0E6 File Offset: 0x0011C2E6
		public static TlsSession ImportSession(byte[] sessionID, SessionParameters sessionParameters)
		{
			return new TlsSessionImpl(sessionID, sessionParameters);
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x0011D7B4 File Offset: 0x0011B9B4
		public static bool IsSignatureAlgorithmsExtensionAllowed(ProtocolVersion clientVersion)
		{
			return ProtocolVersion.TLSv12.IsEqualOrEarlierVersionOf(clientVersion.GetEquivalentTLSVersion());
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x0011E0EF File Offset: 0x0011C2EF
		public static void AddSignatureAlgorithmsExtension(IDictionary extensions, IList supportedSignatureAlgorithms)
		{
			extensions[13] = TlsUtilities.CreateSignatureAlgorithmsExtension(supportedSignatureAlgorithms);
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x0011E104 File Offset: 0x0011C304
		public static IList GetSignatureAlgorithmsExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 13);
			if (extensionData != null)
			{
				return TlsUtilities.ReadSignatureAlgorithmsExtension(extensionData);
			}
			return null;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x0011E128 File Offset: 0x0011C328
		public static byte[] CreateSignatureAlgorithmsExtension(IList supportedSignatureAlgorithms)
		{
			MemoryStream memoryStream = new MemoryStream();
			TlsUtilities.EncodeSupportedSignatureAlgorithms(supportedSignatureAlgorithms, false, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x0011E14C File Offset: 0x0011C34C
		public static IList ReadSignatureAlgorithmsExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			IList result = TlsUtilities.ParseSupportedSignatureAlgorithms(false, memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x0011E17C File Offset: 0x0011C37C
		public static void EncodeSupportedSignatureAlgorithms(IList supportedSignatureAlgorithms, bool allowAnonymous, Stream output)
		{
			if (supportedSignatureAlgorithms == null)
			{
				throw new ArgumentNullException("supportedSignatureAlgorithms");
			}
			if (supportedSignatureAlgorithms.Count < 1 || supportedSignatureAlgorithms.Count >= 32768)
			{
				throw new ArgumentException("must have length from 1 to (2^15 - 1)", "supportedSignatureAlgorithms");
			}
			int i = 2 * supportedSignatureAlgorithms.Count;
			TlsUtilities.CheckUint16(i);
			TlsUtilities.WriteUint16(i, output);
			foreach (object obj in supportedSignatureAlgorithms)
			{
				SignatureAndHashAlgorithm signatureAndHashAlgorithm = (SignatureAndHashAlgorithm)obj;
				if (!allowAnonymous && signatureAndHashAlgorithm.Signature == 0)
				{
					throw new ArgumentException("SignatureAlgorithm.anonymous MUST NOT appear in the signature_algorithms extension");
				}
				signatureAndHashAlgorithm.Encode(output);
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x0011E230 File Offset: 0x0011C430
		public static IList ParseSupportedSignatureAlgorithms(bool allowAnonymous, Stream input)
		{
			int num = TlsUtilities.ReadUint16(input);
			if (num < 2 || (num & 1) != 0)
			{
				throw new TlsFatalAlert(50);
			}
			int num2 = num / 2;
			IList list = Platform.CreateArrayList(num2);
			for (int i = 0; i < num2; i++)
			{
				SignatureAndHashAlgorithm signatureAndHashAlgorithm = SignatureAndHashAlgorithm.Parse(input);
				if (!allowAnonymous && signatureAndHashAlgorithm.Signature == 0)
				{
					throw new TlsFatalAlert(47);
				}
				list.Add(signatureAndHashAlgorithm);
			}
			return list;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x0011E294 File Offset: 0x0011C494
		public static void VerifySupportedSignatureAlgorithm(IList supportedSignatureAlgorithms, SignatureAndHashAlgorithm signatureAlgorithm)
		{
			if (supportedSignatureAlgorithms == null)
			{
				throw new ArgumentNullException("supportedSignatureAlgorithms");
			}
			if (supportedSignatureAlgorithms.Count < 1 || supportedSignatureAlgorithms.Count >= 32768)
			{
				throw new ArgumentException("must have length from 1 to (2^15 - 1)", "supportedSignatureAlgorithms");
			}
			if (signatureAlgorithm == null)
			{
				throw new ArgumentNullException("signatureAlgorithm");
			}
			if (signatureAlgorithm.Signature != 0)
			{
				foreach (object obj in supportedSignatureAlgorithms)
				{
					SignatureAndHashAlgorithm signatureAndHashAlgorithm = (SignatureAndHashAlgorithm)obj;
					if (signatureAndHashAlgorithm.Hash == signatureAlgorithm.Hash && signatureAndHashAlgorithm.Signature == signatureAlgorithm.Signature)
					{
						return;
					}
				}
			}
			throw new TlsFatalAlert(47);
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x0011E354 File Offset: 0x0011C554
		public static byte[] PRF(TlsContext context, byte[] secret, string asciiLabel, byte[] seed, int size)
		{
			if (context.ServerVersion.IsSsl)
			{
				throw new InvalidOperationException("No PRF available for SSLv3 session");
			}
			byte[] array = Strings.ToByteArray(asciiLabel);
			byte[] array2 = TlsUtilities.Concat(array, seed);
			int prfAlgorithm = context.SecurityParameters.PrfAlgorithm;
			if (prfAlgorithm == 0)
			{
				return TlsUtilities.PRF_legacy(secret, array, array2, size);
			}
			IDigest digest = TlsUtilities.CreatePrfHash(prfAlgorithm);
			byte[] array3 = new byte[size];
			TlsUtilities.HMacHash(digest, secret, array2, array3);
			return array3;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x0011E3BC File Offset: 0x0011C5BC
		public static byte[] PRF_legacy(byte[] secret, string asciiLabel, byte[] seed, int size)
		{
			byte[] array = Strings.ToByteArray(asciiLabel);
			byte[] labelSeed = TlsUtilities.Concat(array, seed);
			return TlsUtilities.PRF_legacy(secret, array, labelSeed, size);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x0011E3E4 File Offset: 0x0011C5E4
		internal static byte[] PRF_legacy(byte[] secret, byte[] label, byte[] labelSeed, int size)
		{
			int num = (secret.Length + 1) / 2;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			Array.Copy(secret, 0, array, 0, num);
			Array.Copy(secret, secret.Length - num, array2, 0, num);
			byte[] array3 = new byte[size];
			byte[] array4 = new byte[size];
			TlsUtilities.HMacHash(TlsUtilities.CreateHash(1), array, labelSeed, array3);
			TlsUtilities.HMacHash(TlsUtilities.CreateHash(2), array2, labelSeed, array4);
			for (int i = 0; i < size; i++)
			{
				byte[] array5 = array3;
				int num2 = i;
				array5[num2] ^= array4[i];
			}
			return array3;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x0011E470 File Offset: 0x0011C670
		internal static byte[] Concat(byte[] a, byte[] b)
		{
			byte[] array = new byte[a.Length + b.Length];
			Array.Copy(a, 0, array, 0, a.Length);
			Array.Copy(b, 0, array, a.Length, b.Length);
			return array;
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x0011E4A8 File Offset: 0x0011C6A8
		internal static void HMacHash(IDigest digest, byte[] secret, byte[] seed, byte[] output)
		{
			HMac hmac = new HMac(digest);
			hmac.Init(new KeyParameter(secret));
			byte[] array = seed;
			int digestSize = digest.GetDigestSize();
			int num = (output.Length + digestSize - 1) / digestSize;
			byte[] array2 = new byte[hmac.GetMacSize()];
			byte[] array3 = new byte[hmac.GetMacSize()];
			for (int i = 0; i < num; i++)
			{
				hmac.BlockUpdate(array, 0, array.Length);
				hmac.DoFinal(array2, 0);
				array = array2;
				hmac.BlockUpdate(array, 0, array.Length);
				hmac.BlockUpdate(seed, 0, seed.Length);
				hmac.DoFinal(array3, 0);
				Array.Copy(array3, 0, output, digestSize * i, Math.Min(digestSize, output.Length - digestSize * i));
			}
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x0011E558 File Offset: 0x0011C758
		internal static void ValidateKeyUsage(X509CertificateStructure c, int keyUsageBits)
		{
			X509Extensions extensions = c.TbsCertificate.Extensions;
			if (extensions != null)
			{
				X509Extension extension = extensions.GetExtension(X509Extensions.KeyUsage);
				if (extension != null && ((int)KeyUsage.GetInstance(extension).GetBytes()[0] & keyUsageBits) != keyUsageBits)
				{
					throw new TlsFatalAlert(46);
				}
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x0011E5A0 File Offset: 0x0011C7A0
		internal static byte[] CalculateKeyBlock(TlsContext context, int size)
		{
			SecurityParameters securityParameters = context.SecurityParameters;
			byte[] masterSecret = securityParameters.MasterSecret;
			byte[] array = TlsUtilities.Concat(securityParameters.ServerRandom, securityParameters.ClientRandom);
			if (TlsUtilities.IsSsl(context))
			{
				return TlsUtilities.CalculateKeyBlock_Ssl(masterSecret, array, size);
			}
			return TlsUtilities.PRF(context, masterSecret, "key expansion", array, size);
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x0011E5EC File Offset: 0x0011C7EC
		internal static byte[] CalculateKeyBlock_Ssl(byte[] master_secret, byte[] random, int size)
		{
			IDigest digest = TlsUtilities.CreateHash(1);
			IDigest digest2 = TlsUtilities.CreateHash(2);
			int digestSize = digest.GetDigestSize();
			byte[] array = new byte[digest2.GetDigestSize()];
			byte[] array2 = new byte[size + digestSize];
			int num = 0;
			int i = 0;
			while (i < size)
			{
				byte[] array3 = TlsUtilities.SSL3_CONST[num];
				digest2.BlockUpdate(array3, 0, array3.Length);
				digest2.BlockUpdate(master_secret, 0, master_secret.Length);
				digest2.BlockUpdate(random, 0, random.Length);
				digest2.DoFinal(array, 0);
				digest.BlockUpdate(master_secret, 0, master_secret.Length);
				digest.BlockUpdate(array, 0, array.Length);
				digest.DoFinal(array2, i);
				i += digestSize;
				num++;
			}
			return Arrays.CopyOfRange(array2, 0, size);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x0011E6A0 File Offset: 0x0011C8A0
		internal static byte[] CalculateMasterSecret(TlsContext context, byte[] pre_master_secret)
		{
			SecurityParameters securityParameters = context.SecurityParameters;
			byte[] array = securityParameters.IsExtendedMasterSecret ? securityParameters.SessionHash : TlsUtilities.Concat(securityParameters.ClientRandom, securityParameters.ServerRandom);
			if (TlsUtilities.IsSsl(context))
			{
				return TlsUtilities.CalculateMasterSecret_Ssl(pre_master_secret, array);
			}
			string asciiLabel = securityParameters.IsExtendedMasterSecret ? ExporterLabel.extended_master_secret : "master secret";
			return TlsUtilities.PRF(context, pre_master_secret, asciiLabel, array, 48);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x0011E708 File Offset: 0x0011C908
		internal static byte[] CalculateMasterSecret_Ssl(byte[] pre_master_secret, byte[] random)
		{
			IDigest digest = TlsUtilities.CreateHash(1);
			IDigest digest2 = TlsUtilities.CreateHash(2);
			int digestSize = digest.GetDigestSize();
			byte[] array = new byte[digest2.GetDigestSize()];
			byte[] array2 = new byte[digestSize * 3];
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				byte[] array3 = TlsUtilities.SSL3_CONST[i];
				digest2.BlockUpdate(array3, 0, array3.Length);
				digest2.BlockUpdate(pre_master_secret, 0, pre_master_secret.Length);
				digest2.BlockUpdate(random, 0, random.Length);
				digest2.DoFinal(array, 0);
				digest.BlockUpdate(pre_master_secret, 0, pre_master_secret.Length);
				digest.BlockUpdate(array, 0, array.Length);
				digest.DoFinal(array2, num);
				num += digestSize;
			}
			return array2;
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x0011E7B4 File Offset: 0x0011C9B4
		internal static byte[] CalculateVerifyData(TlsContext context, string asciiLabel, byte[] handshakeHash)
		{
			if (TlsUtilities.IsSsl(context))
			{
				return handshakeHash;
			}
			SecurityParameters securityParameters = context.SecurityParameters;
			byte[] masterSecret = securityParameters.MasterSecret;
			int verifyDataLength = securityParameters.VerifyDataLength;
			return TlsUtilities.PRF(context, masterSecret, asciiLabel, handshakeHash, verifyDataLength);
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x0011E7E8 File Offset: 0x0011C9E8
		public static IDigest CreateHash(byte hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case 1:
				return new MD5Digest();
			case 2:
				return new Sha1Digest();
			case 3:
				return new Sha224Digest();
			case 4:
				return new Sha256Digest();
			case 5:
				return new Sha384Digest();
			case 6:
				return new Sha512Digest();
			default:
				throw new ArgumentException("unknown HashAlgorithm", "hashAlgorithm");
			}
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x0011E84C File Offset: 0x0011CA4C
		public static IDigest CreateHash(SignatureAndHashAlgorithm signatureAndHashAlgorithm)
		{
			if (signatureAndHashAlgorithm != null)
			{
				return TlsUtilities.CreateHash(signatureAndHashAlgorithm.Hash);
			}
			return new CombinedHash();
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x0011E870 File Offset: 0x0011CA70
		public static IDigest CloneHash(byte hashAlgorithm, IDigest hash)
		{
			switch (hashAlgorithm)
			{
			case 1:
				return new MD5Digest((MD5Digest)hash);
			case 2:
				return new Sha1Digest((Sha1Digest)hash);
			case 3:
				return new Sha224Digest((Sha224Digest)hash);
			case 4:
				return new Sha256Digest((Sha256Digest)hash);
			case 5:
				return new Sha384Digest((Sha384Digest)hash);
			case 6:
				return new Sha512Digest((Sha512Digest)hash);
			default:
				throw new ArgumentException("unknown HashAlgorithm", "hashAlgorithm");
			}
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x0011E8F6 File Offset: 0x0011CAF6
		public static IDigest CreatePrfHash(int prfAlgorithm)
		{
			if (prfAlgorithm == 0)
			{
				return new CombinedHash();
			}
			return TlsUtilities.CreateHash(TlsUtilities.GetHashAlgorithmForPrfAlgorithm(prfAlgorithm));
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x0011E90C File Offset: 0x0011CB0C
		public static IDigest ClonePrfHash(int prfAlgorithm, IDigest hash)
		{
			if (prfAlgorithm == 0)
			{
				return new CombinedHash((CombinedHash)hash);
			}
			return TlsUtilities.CloneHash(TlsUtilities.GetHashAlgorithmForPrfAlgorithm(prfAlgorithm), hash);
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x0011E929 File Offset: 0x0011CB29
		public static byte GetHashAlgorithmForPrfAlgorithm(int prfAlgorithm)
		{
			switch (prfAlgorithm)
			{
			case 0:
				throw new ArgumentException("legacy PRF not a valid algorithm", "prfAlgorithm");
			case 1:
				return 4;
			case 2:
				return 5;
			default:
				throw new ArgumentException("unknown PrfAlgorithm", "prfAlgorithm");
			}
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x0011E964 File Offset: 0x0011CB64
		public static DerObjectIdentifier GetOidForHashAlgorithm(byte hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case 1:
				return PkcsObjectIdentifiers.MD5;
			case 2:
				return X509ObjectIdentifiers.IdSha1;
			case 3:
				return NistObjectIdentifiers.IdSha224;
			case 4:
				return NistObjectIdentifiers.IdSha256;
			case 5:
				return NistObjectIdentifiers.IdSha384;
			case 6:
				return NistObjectIdentifiers.IdSha512;
			default:
				throw new ArgumentException("unknown HashAlgorithm", "hashAlgorithm");
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x0011E9C8 File Offset: 0x0011CBC8
		internal static short GetClientCertificateType(Certificate clientCertificate, Certificate serverCertificate)
		{
			if (clientCertificate.IsEmpty)
			{
				return -1;
			}
			X509CertificateStructure certificateAt = clientCertificate.GetCertificateAt(0);
			SubjectPublicKeyInfo subjectPublicKeyInfo = certificateAt.SubjectPublicKeyInfo;
			short result;
			try
			{
				AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(subjectPublicKeyInfo);
				if (asymmetricKeyParameter.IsPrivate)
				{
					throw new TlsFatalAlert(80);
				}
				if (asymmetricKeyParameter is RsaKeyParameters)
				{
					TlsUtilities.ValidateKeyUsage(certificateAt, 128);
					result = 1;
				}
				else if (asymmetricKeyParameter is DsaPublicKeyParameters)
				{
					TlsUtilities.ValidateKeyUsage(certificateAt, 128);
					result = 2;
				}
				else
				{
					if (!(asymmetricKeyParameter is ECPublicKeyParameters))
					{
						throw new TlsFatalAlert(43);
					}
					TlsUtilities.ValidateKeyUsage(certificateAt, 128);
					result = 64;
				}
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(43, alertCause);
			}
			return result;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x0011EA70 File Offset: 0x0011CC70
		internal static void TrackHashAlgorithms(TlsHandshakeHash handshakeHash, IList supportedSignatureAlgorithms)
		{
			if (supportedSignatureAlgorithms != null)
			{
				foreach (object obj in supportedSignatureAlgorithms)
				{
					byte hash = ((SignatureAndHashAlgorithm)obj).Hash;
					if (HashAlgorithm.IsRecognized(hash))
					{
						handshakeHash.TrackHashAlgorithm(hash);
					}
				}
			}
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x0011EAD4 File Offset: 0x0011CCD4
		public static bool HasSigningCapability(byte clientCertificateType)
		{
			return clientCertificateType - 1 <= 1 || clientCertificateType == 64;
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x0011EAE4 File Offset: 0x0011CCE4
		public static TlsSigner CreateTlsSigner(byte clientCertificateType)
		{
			if (clientCertificateType == 1)
			{
				return new TlsRsaSigner();
			}
			if (clientCertificateType == 2)
			{
				return new TlsDssSigner();
			}
			if (clientCertificateType != 64)
			{
				throw new ArgumentException("not a type with signing capability", "clientCertificateType");
			}
			return new TlsECDsaSigner();
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x0011EB18 File Offset: 0x0011CD18
		private static byte[][] GenSsl3Const()
		{
			int num = 10;
			byte[][] array = new byte[num][];
			for (int i = 0; i < num; i++)
			{
				byte[] array2 = new byte[i + 1];
				Arrays.Fill(array2, (byte)(65 + i));
				array[i] = array2;
			}
			return array;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x0011EB54 File Offset: 0x0011CD54
		private static IList VectorOfOne(object obj)
		{
			IList list = Platform.CreateArrayList(1);
			list.Add(obj);
			return list;
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x0011EB64 File Offset: 0x0011CD64
		public static int GetCipherType(int ciphersuite)
		{
			int encryptionAlgorithm = TlsUtilities.GetEncryptionAlgorithm(ciphersuite);
			switch (encryptionAlgorithm)
			{
			case 0:
			case 1:
			case 2:
				return 0;
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 12:
			case 13:
			case 14:
				return 1;
			case 10:
			case 11:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
				break;
			default:
				if (encryptionAlgorithm - 103 > 1)
				{
					throw new TlsFatalAlert(80);
				}
				break;
			}
			return 2;
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x0011EBEC File Offset: 0x0011CDEC
		public static int GetEncryptionAlgorithm(int ciphersuite)
		{
			if (ciphersuite <= 49327)
			{
				switch (ciphersuite)
				{
				case 1:
					return 0;
				case 2:
				case 44:
				case 45:
				case 46:
					return 0;
				case 3:
				case 6:
				case 7:
				case 8:
				case 9:
				case 11:
				case 12:
				case 14:
				case 15:
				case 17:
				case 18:
				case 20:
				case 21:
				case 23:
				case 25:
				case 26:
				case 28:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
				case 126:
				case 127:
				case 128:
				case 129:
				case 130:
				case 131:
					goto IL_6A4;
				case 4:
				case 24:
					return 2;
				case 5:
				case 138:
				case 142:
				case 146:
					return 2;
				case 10:
				case 13:
				case 16:
				case 19:
				case 22:
				case 27:
				case 139:
				case 143:
				case 147:
					break;
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 60:
				case 62:
				case 63:
				case 64:
				case 103:
				case 108:
				case 140:
				case 144:
				case 148:
				case 174:
				case 178:
				case 182:
					return 8;
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				case 58:
				case 61:
				case 104:
				case 105:
				case 106:
				case 107:
				case 109:
				case 141:
				case 145:
				case 149:
				case 175:
				case 179:
				case 183:
					return 9;
				case 59:
				case 176:
				case 180:
				case 184:
					return 0;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 186:
				case 187:
				case 188:
				case 189:
				case 190:
				case 191:
					return 12;
				case 132:
				case 133:
				case 134:
				case 135:
				case 136:
				case 137:
				case 192:
				case 193:
				case 194:
				case 195:
				case 196:
				case 197:
					return 13;
				case 150:
				case 151:
				case 152:
				case 153:
				case 154:
				case 155:
					return 14;
				case 156:
				case 158:
				case 160:
				case 162:
				case 164:
				case 166:
				case 168:
				case 170:
				case 172:
					return 10;
				case 157:
				case 159:
				case 161:
				case 163:
				case 165:
				case 167:
				case 169:
				case 171:
				case 173:
					return 11;
				case 177:
				case 181:
				case 185:
					return 0;
				default:
					switch (ciphersuite)
					{
					case 49153:
					case 49158:
					case 49163:
					case 49168:
					case 49173:
					case 49209:
						return 0;
					case 49154:
					case 49159:
					case 49164:
					case 49169:
					case 49174:
					case 49203:
						return 2;
					case 49155:
					case 49160:
					case 49165:
					case 49170:
					case 49175:
					case 49178:
					case 49179:
					case 49180:
					case 49204:
						break;
					case 49156:
					case 49161:
					case 49166:
					case 49171:
					case 49176:
					case 49181:
					case 49182:
					case 49183:
					case 49187:
					case 49189:
					case 49191:
					case 49193:
					case 49205:
					case 49207:
						return 8;
					case 49157:
					case 49162:
					case 49167:
					case 49172:
					case 49177:
					case 49184:
					case 49185:
					case 49186:
					case 49188:
					case 49190:
					case 49192:
					case 49194:
					case 49206:
					case 49208:
						return 9;
					case 49195:
					case 49197:
					case 49199:
					case 49201:
						return 10;
					case 49196:
					case 49198:
					case 49200:
					case 49202:
						return 11;
					case 49210:
						return 0;
					case 49211:
						return 0;
					case 49212:
					case 49213:
					case 49214:
					case 49215:
					case 49216:
					case 49217:
					case 49218:
					case 49219:
					case 49220:
					case 49221:
					case 49222:
					case 49223:
					case 49224:
					case 49225:
					case 49226:
					case 49227:
					case 49228:
					case 49229:
					case 49230:
					case 49231:
					case 49232:
					case 49233:
					case 49234:
					case 49235:
					case 49236:
					case 49237:
					case 49238:
					case 49239:
					case 49240:
					case 49241:
					case 49242:
					case 49243:
					case 49244:
					case 49245:
					case 49246:
					case 49247:
					case 49248:
					case 49249:
					case 49250:
					case 49251:
					case 49252:
					case 49253:
					case 49254:
					case 49255:
					case 49256:
					case 49257:
					case 49258:
					case 49259:
					case 49260:
					case 49261:
					case 49262:
					case 49263:
					case 49264:
					case 49265:
						goto IL_6A4;
					case 49266:
					case 49268:
					case 49270:
					case 49272:
					case 49300:
					case 49302:
					case 49304:
					case 49306:
						return 12;
					case 49267:
					case 49269:
					case 49271:
					case 49273:
					case 49301:
					case 49303:
					case 49305:
					case 49307:
						return 13;
					case 49274:
					case 49276:
					case 49278:
					case 49280:
					case 49282:
					case 49284:
					case 49286:
					case 49288:
					case 49290:
					case 49292:
					case 49294:
					case 49296:
					case 49298:
						return 19;
					case 49275:
					case 49277:
					case 49279:
					case 49281:
					case 49283:
					case 49285:
					case 49287:
					case 49289:
					case 49291:
					case 49293:
					case 49295:
					case 49297:
					case 49299:
						return 20;
					case 49308:
					case 49310:
					case 49316:
					case 49318:
					case 49324:
						return 15;
					case 49309:
					case 49311:
					case 49317:
					case 49319:
					case 49325:
						return 17;
					case 49312:
					case 49314:
					case 49320:
					case 49322:
					case 49326:
						return 16;
					case 49313:
					case 49315:
					case 49321:
					case 49323:
					case 49327:
						return 18;
					default:
						goto IL_6A4;
					}
					break;
				}
				return 7;
			}
			if (ciphersuite - 52392 <= 6)
			{
				return 21;
			}
			switch (ciphersuite)
			{
			case 65280:
			case 65282:
			case 65284:
			case 65296:
			case 65298:
			case 65300:
				return 103;
			case 65281:
			case 65283:
			case 65285:
			case 65297:
			case 65299:
			case 65301:
				return 104;
			}
			IL_6A4:
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x0011F2A4 File Offset: 0x0011D4A4
		public static int GetKeyExchangeAlgorithm(int ciphersuite)
		{
			if (ciphersuite <= 49327)
			{
				switch (ciphersuite)
				{
				case 1:
				case 2:
				case 4:
				case 5:
				case 10:
				case 47:
				case 53:
				case 59:
				case 60:
				case 61:
				case 65:
				case 132:
				case 150:
				case 156:
				case 157:
				case 186:
				case 192:
					return 1;
				case 3:
				case 6:
				case 7:
				case 8:
				case 9:
				case 11:
				case 12:
				case 14:
				case 15:
				case 17:
				case 18:
				case 20:
				case 21:
				case 23:
				case 25:
				case 26:
				case 28:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
				case 126:
				case 127:
				case 128:
				case 129:
				case 130:
				case 131:
					goto IL_6B4;
				case 13:
				case 48:
				case 54:
				case 62:
				case 66:
				case 104:
				case 133:
				case 151:
				case 164:
				case 165:
				case 187:
				case 193:
					return 7;
				case 16:
				case 49:
				case 55:
				case 63:
				case 67:
				case 105:
				case 134:
				case 152:
				case 160:
				case 161:
				case 188:
				case 194:
					return 9;
				case 19:
				case 50:
				case 56:
				case 64:
				case 68:
				case 106:
				case 135:
				case 153:
				case 162:
				case 163:
				case 189:
				case 195:
					return 3;
				case 22:
				case 51:
				case 57:
				case 69:
				case 103:
				case 107:
				case 136:
				case 154:
				case 158:
				case 159:
				case 190:
				case 196:
					return 5;
				case 24:
				case 27:
				case 52:
				case 58:
				case 70:
				case 108:
				case 109:
				case 137:
				case 155:
				case 166:
				case 167:
				case 191:
				case 197:
					break;
				case 44:
				case 138:
				case 139:
				case 140:
				case 141:
				case 168:
				case 169:
				case 174:
				case 175:
				case 176:
				case 177:
					return 13;
				case 45:
				case 142:
				case 143:
				case 144:
				case 145:
				case 170:
				case 171:
				case 178:
				case 179:
				case 180:
				case 181:
					return 14;
				case 46:
				case 146:
				case 147:
				case 148:
				case 149:
				case 172:
				case 173:
				case 182:
				case 183:
				case 184:
				case 185:
					return 15;
				default:
					switch (ciphersuite)
					{
					case 49153:
					case 49154:
					case 49155:
					case 49156:
					case 49157:
					case 49189:
					case 49190:
					case 49197:
					case 49198:
					case 49268:
					case 49269:
					case 49288:
					case 49289:
						return 16;
					case 49158:
					case 49159:
					case 49160:
					case 49161:
					case 49162:
					case 49187:
					case 49188:
					case 49195:
					case 49196:
					case 49266:
					case 49267:
					case 49286:
					case 49287:
					case 49324:
					case 49325:
					case 49326:
					case 49327:
						return 17;
					case 49163:
					case 49164:
					case 49165:
					case 49166:
					case 49167:
					case 49193:
					case 49194:
					case 49201:
					case 49202:
					case 49272:
					case 49273:
					case 49292:
					case 49293:
						return 18;
					case 49168:
					case 49169:
					case 49170:
					case 49171:
					case 49172:
					case 49191:
					case 49192:
					case 49199:
					case 49200:
					case 49270:
					case 49271:
					case 49290:
					case 49291:
						return 19;
					case 49173:
					case 49174:
					case 49175:
					case 49176:
					case 49177:
						return 20;
					case 49178:
					case 49181:
					case 49184:
						return 21;
					case 49179:
					case 49182:
					case 49185:
						return 23;
					case 49180:
					case 49183:
					case 49186:
						return 22;
					case 49203:
					case 49204:
					case 49205:
					case 49206:
					case 49207:
					case 49208:
					case 49209:
					case 49210:
					case 49211:
					case 49306:
					case 49307:
						return 24;
					case 49212:
					case 49213:
					case 49214:
					case 49215:
					case 49216:
					case 49217:
					case 49218:
					case 49219:
					case 49220:
					case 49221:
					case 49222:
					case 49223:
					case 49224:
					case 49225:
					case 49226:
					case 49227:
					case 49228:
					case 49229:
					case 49230:
					case 49231:
					case 49232:
					case 49233:
					case 49234:
					case 49235:
					case 49236:
					case 49237:
					case 49238:
					case 49239:
					case 49240:
					case 49241:
					case 49242:
					case 49243:
					case 49244:
					case 49245:
					case 49246:
					case 49247:
					case 49248:
					case 49249:
					case 49250:
					case 49251:
					case 49252:
					case 49253:
					case 49254:
					case 49255:
					case 49256:
					case 49257:
					case 49258:
					case 49259:
					case 49260:
					case 49261:
					case 49262:
					case 49263:
					case 49264:
					case 49265:
						goto IL_6B4;
					case 49274:
					case 49275:
					case 49308:
					case 49309:
					case 49312:
					case 49313:
						return 1;
					case 49276:
					case 49277:
					case 49310:
					case 49311:
					case 49314:
					case 49315:
						return 5;
					case 49278:
					case 49279:
						return 9;
					case 49280:
					case 49281:
						return 3;
					case 49282:
					case 49283:
						return 7;
					case 49284:
					case 49285:
						break;
					case 49294:
					case 49295:
					case 49300:
					case 49301:
					case 49316:
					case 49317:
					case 49320:
					case 49321:
						return 13;
					case 49296:
					case 49297:
					case 49302:
					case 49303:
					case 49318:
					case 49319:
					case 49322:
					case 49323:
						return 14;
					case 49298:
					case 49299:
					case 49304:
					case 49305:
						return 15;
					default:
						goto IL_6B4;
					}
					break;
				}
				return 11;
			}
			switch (ciphersuite)
			{
			case 52392:
				return 19;
			case 52393:
				return 17;
			case 52394:
				return 5;
			case 52395:
				return 13;
			case 52396:
				return 24;
			case 52397:
				break;
			case 52398:
				return 1;
			default:
				switch (ciphersuite)
				{
				case 65280:
				case 65281:
					return 5;
				case 65282:
				case 65283:
					return 19;
				case 65284:
				case 65285:
					return 16;
				case 65286:
				case 65287:
				case 65288:
				case 65289:
				case 65290:
				case 65291:
				case 65292:
				case 65293:
				case 65294:
				case 65295:
					goto IL_6B4;
				case 65296:
				case 65297:
					return 13;
				case 65298:
				case 65299:
					break;
				case 65300:
				case 65301:
					return 24;
				default:
					goto IL_6B4;
				}
				break;
			}
			return 14;
			IL_6B4:
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x0011F96C File Offset: 0x0011DB6C
		public static int GetMacAlgorithm(int ciphersuite)
		{
			if (ciphersuite <= 49327)
			{
				switch (ciphersuite)
				{
				case 1:
				case 4:
				case 24:
					return 1;
				case 2:
				case 5:
				case 10:
				case 13:
				case 16:
				case 19:
				case 22:
				case 27:
				case 44:
				case 45:
				case 46:
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				case 58:
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 132:
				case 133:
				case 134:
				case 135:
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
				case 141:
				case 142:
				case 143:
				case 144:
				case 145:
				case 146:
				case 147:
				case 148:
				case 149:
				case 150:
				case 151:
				case 152:
				case 153:
				case 154:
				case 155:
					break;
				case 3:
				case 6:
				case 7:
				case 8:
				case 9:
				case 11:
				case 12:
				case 14:
				case 15:
				case 17:
				case 18:
				case 20:
				case 21:
				case 23:
				case 25:
				case 26:
				case 28:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
				case 126:
				case 127:
				case 128:
				case 129:
				case 130:
				case 131:
					goto IL_619;
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
				case 103:
				case 104:
				case 105:
				case 106:
				case 107:
				case 108:
				case 109:
				case 174:
				case 176:
				case 178:
				case 180:
				case 182:
				case 184:
				case 186:
				case 187:
				case 188:
				case 189:
				case 190:
				case 191:
				case 192:
				case 193:
				case 194:
				case 195:
				case 196:
				case 197:
					return 3;
				case 156:
				case 157:
				case 158:
				case 159:
				case 160:
				case 161:
				case 162:
				case 163:
				case 164:
				case 165:
				case 166:
				case 167:
				case 168:
				case 169:
				case 170:
				case 171:
				case 172:
				case 173:
					return 0;
				case 175:
				case 177:
				case 179:
				case 181:
				case 183:
				case 185:
					return 4;
				default:
					switch (ciphersuite)
					{
					case 49153:
					case 49154:
					case 49155:
					case 49156:
					case 49157:
					case 49158:
					case 49159:
					case 49160:
					case 49161:
					case 49162:
					case 49163:
					case 49164:
					case 49165:
					case 49166:
					case 49167:
					case 49168:
					case 49169:
					case 49170:
					case 49171:
					case 49172:
					case 49173:
					case 49174:
					case 49175:
					case 49176:
					case 49177:
					case 49178:
					case 49179:
					case 49180:
					case 49181:
					case 49182:
					case 49183:
					case 49184:
					case 49185:
					case 49186:
					case 49203:
					case 49204:
					case 49205:
					case 49206:
					case 49209:
						break;
					case 49187:
					case 49189:
					case 49191:
					case 49193:
					case 49207:
					case 49210:
					case 49266:
					case 49268:
					case 49270:
					case 49272:
					case 49300:
					case 49302:
					case 49304:
					case 49306:
						return 3;
					case 49188:
					case 49190:
					case 49192:
					case 49194:
					case 49208:
					case 49211:
					case 49267:
					case 49269:
					case 49271:
					case 49273:
					case 49301:
					case 49303:
					case 49305:
					case 49307:
						return 4;
					case 49195:
					case 49196:
					case 49197:
					case 49198:
					case 49199:
					case 49200:
					case 49201:
					case 49202:
					case 49274:
					case 49275:
					case 49276:
					case 49277:
					case 49278:
					case 49279:
					case 49280:
					case 49281:
					case 49282:
					case 49283:
					case 49284:
					case 49285:
					case 49286:
					case 49287:
					case 49288:
					case 49289:
					case 49290:
					case 49291:
					case 49292:
					case 49293:
					case 49294:
					case 49295:
					case 49296:
					case 49297:
					case 49298:
					case 49299:
					case 49308:
					case 49309:
					case 49310:
					case 49311:
					case 49312:
					case 49313:
					case 49314:
					case 49315:
					case 49316:
					case 49317:
					case 49318:
					case 49319:
					case 49320:
					case 49321:
					case 49322:
					case 49323:
					case 49324:
					case 49325:
					case 49326:
					case 49327:
						return 0;
					case 49212:
					case 49213:
					case 49214:
					case 49215:
					case 49216:
					case 49217:
					case 49218:
					case 49219:
					case 49220:
					case 49221:
					case 49222:
					case 49223:
					case 49224:
					case 49225:
					case 49226:
					case 49227:
					case 49228:
					case 49229:
					case 49230:
					case 49231:
					case 49232:
					case 49233:
					case 49234:
					case 49235:
					case 49236:
					case 49237:
					case 49238:
					case 49239:
					case 49240:
					case 49241:
					case 49242:
					case 49243:
					case 49244:
					case 49245:
					case 49246:
					case 49247:
					case 49248:
					case 49249:
					case 49250:
					case 49251:
					case 49252:
					case 49253:
					case 49254:
					case 49255:
					case 49256:
					case 49257:
					case 49258:
					case 49259:
					case 49260:
					case 49261:
					case 49262:
					case 49263:
					case 49264:
					case 49265:
						goto IL_619;
					default:
						goto IL_619;
					}
					break;
				}
				return 2;
			}
			if (ciphersuite - 52392 > 6 && ciphersuite - 65280 > 5 && ciphersuite - 65296 > 5)
			{
				goto IL_619;
			}
			return 0;
			IL_619:
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x0011FF9C File Offset: 0x0011E19C
		public static ProtocolVersion GetMinimumVersion(int ciphersuite)
		{
			if (ciphersuite <= 49202)
			{
				if (ciphersuite <= 109)
				{
					if (ciphersuite - 59 > 5 && ciphersuite - 103 > 6)
					{
						goto IL_84;
					}
				}
				else if (ciphersuite - 156 > 17 && ciphersuite - 186 > 11 && ciphersuite - 49187 > 15)
				{
					goto IL_84;
				}
			}
			else if (ciphersuite <= 49327)
			{
				if (ciphersuite - 49266 > 33 && ciphersuite - 49308 > 19)
				{
					goto IL_84;
				}
			}
			else if (ciphersuite - 52392 > 6 && ciphersuite - 65280 > 5 && ciphersuite - 65296 > 5)
			{
				goto IL_84;
			}
			return ProtocolVersion.TLSv12;
			IL_84:
			return ProtocolVersion.SSLv3;
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x00120032 File Offset: 0x0011E232
		public static bool IsAeadCipherSuite(int ciphersuite)
		{
			return 2 == TlsUtilities.GetCipherType(ciphersuite);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x0012003D File Offset: 0x0011E23D
		public static bool IsBlockCipherSuite(int ciphersuite)
		{
			return 1 == TlsUtilities.GetCipherType(ciphersuite);
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00120048 File Offset: 0x0011E248
		public static bool IsStreamCipherSuite(int ciphersuite)
		{
			return TlsUtilities.GetCipherType(ciphersuite) == 0;
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x00120054 File Offset: 0x0011E254
		public static bool IsValidCipherSuiteForSignatureAlgorithms(int cipherSuite, IList sigAlgs)
		{
			int keyExchangeAlgorithm;
			try
			{
				keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(cipherSuite);
			}
			catch (IOException)
			{
				return true;
			}
			switch (keyExchangeAlgorithm)
			{
			case 3:
			case 4:
			case 22:
				return sigAlgs.Contains(2);
			case 5:
			case 6:
			case 19:
			case 23:
				return sigAlgs.Contains(1);
			case 11:
			case 12:
			case 20:
				return sigAlgs.Contains(0);
			case 17:
				return sigAlgs.Contains(3);
			}
			return true;
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x00120118 File Offset: 0x0011E318
		public static bool IsValidCipherSuiteForVersion(int cipherSuite, ProtocolVersion serverVersion)
		{
			return TlsUtilities.GetMinimumVersion(cipherSuite).IsEqualOrEarlierVersionOf(serverVersion.GetEquivalentTLSVersion());
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x0012012C File Offset: 0x0011E32C
		public static IList GetUsableSignatureAlgorithms(IList sigHashAlgs)
		{
			if (sigHashAlgs == null)
			{
				return TlsUtilities.GetAllSignatureAlgorithms();
			}
			IList list = Platform.CreateArrayList(4);
			list.Add(0);
			foreach (object obj in sigHashAlgs)
			{
				byte signature = ((SignatureAndHashAlgorithm)obj).Signature;
				if (!list.Contains(signature))
				{
					list.Add(signature);
				}
			}
			return list;
		}

		// Token: 0x04001D78 RID: 7544
		public static readonly byte[] EmptyBytes = new byte[0];

		// Token: 0x04001D79 RID: 7545
		public static readonly short[] EmptyShorts = new short[0];

		// Token: 0x04001D7A RID: 7546
		public static readonly int[] EmptyInts = new int[0];

		// Token: 0x04001D7B RID: 7547
		public static readonly long[] EmptyLongs = new long[0];

		// Token: 0x04001D7C RID: 7548
		internal static readonly byte[] SSL_CLIENT = new byte[]
		{
			67,
			76,
			78,
			84
		};

		// Token: 0x04001D7D RID: 7549
		internal static readonly byte[] SSL_SERVER = new byte[]
		{
			83,
			82,
			86,
			82
		};

		// Token: 0x04001D7E RID: 7550
		internal static readonly byte[][] SSL3_CONST = TlsUtilities.GenSsl3Const();
	}
}
