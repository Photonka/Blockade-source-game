using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities
{
	// Token: 0x020003D9 RID: 985
	internal sealed class Pack
	{
		// Token: 0x06002831 RID: 10289 RVA: 0x00023EF4 File Offset: 0x000220F4
		private Pack()
		{
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0010E4DD File Offset: 0x0010C6DD
		internal static void UInt16_To_BE(ushort n, byte[] bs)
		{
			bs[0] = (byte)(n >> 8);
			bs[1] = (byte)n;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0010E4EB File Offset: 0x0010C6EB
		internal static void UInt16_To_BE(ushort n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 8);
			bs[off + 1] = (byte)n;
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0010E4FB File Offset: 0x0010C6FB
		internal static ushort BE_To_UInt16(byte[] bs)
		{
			return (ushort)((int)bs[0] << 8 | (int)bs[1]);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0010E507 File Offset: 0x0010C707
		internal static ushort BE_To_UInt16(byte[] bs, int off)
		{
			return (ushort)((int)bs[off] << 8 | (int)bs[off + 1]);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0010E518 File Offset: 0x0010C718
		internal static byte[] UInt32_To_BE(uint n)
		{
			byte[] array = new byte[4];
			Pack.UInt32_To_BE(n, array, 0);
			return array;
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0010E535 File Offset: 0x0010C735
		internal static void UInt32_To_BE(uint n, byte[] bs)
		{
			bs[0] = (byte)(n >> 24);
			bs[1] = (byte)(n >> 16);
			bs[2] = (byte)(n >> 8);
			bs[3] = (byte)n;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x0010E553 File Offset: 0x0010C753
		internal static void UInt32_To_BE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 24);
			bs[off + 1] = (byte)(n >> 16);
			bs[off + 2] = (byte)(n >> 8);
			bs[off + 3] = (byte)n;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x0010E578 File Offset: 0x0010C778
		internal static byte[] UInt32_To_BE(uint[] ns)
		{
			byte[] array = new byte[4 * ns.Length];
			Pack.UInt32_To_BE(ns, array, 0);
			return array;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x0010E59C File Offset: 0x0010C79C
		internal static void UInt32_To_BE(uint[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt32_To_BE(ns[i], bs, off);
				off += 4;
			}
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0010E5C6 File Offset: 0x0010C7C6
		internal static uint BE_To_UInt32(byte[] bs)
		{
			return (uint)((int)bs[0] << 24 | (int)bs[1] << 16 | (int)bs[2] << 8 | (int)bs[3]);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0010E5DF File Offset: 0x0010C7DF
		internal static uint BE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] << 24 | (int)bs[off + 1] << 16 | (int)bs[off + 2] << 8 | (int)bs[off + 3]);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0010E600 File Offset: 0x0010C800
		internal static void BE_To_UInt32(byte[] bs, int off, uint[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.BE_To_UInt32(bs, off);
				off += 4;
			}
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0010E62C File Offset: 0x0010C82C
		internal static byte[] UInt64_To_BE(ulong n)
		{
			byte[] array = new byte[8];
			Pack.UInt64_To_BE(n, array, 0);
			return array;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0010E649 File Offset: 0x0010C849
		internal static void UInt64_To_BE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs);
			Pack.UInt32_To_BE((uint)n, bs, 4);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0010E65F File Offset: 0x0010C85F
		internal static void UInt64_To_BE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs, off);
			Pack.UInt32_To_BE((uint)n, bs, off + 4);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0010E678 File Offset: 0x0010C878
		internal static byte[] UInt64_To_BE(ulong[] ns)
		{
			byte[] array = new byte[8 * ns.Length];
			Pack.UInt64_To_BE(ns, array, 0);
			return array;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0010E69C File Offset: 0x0010C89C
		internal static void UInt64_To_BE(ulong[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt64_To_BE(ns[i], bs, off);
				off += 8;
			}
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x0010E6C8 File Offset: 0x0010C8C8
		internal static ulong BE_To_UInt64(byte[] bs)
		{
			ulong num = (ulong)Pack.BE_To_UInt32(bs);
			uint num2 = Pack.BE_To_UInt32(bs, 4);
			return num << 32 | (ulong)num2;
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x0010E6EC File Offset: 0x0010C8EC
		internal static ulong BE_To_UInt64(byte[] bs, int off)
		{
			ulong num = (ulong)Pack.BE_To_UInt32(bs, off);
			uint num2 = Pack.BE_To_UInt32(bs, off + 4);
			return num << 32 | (ulong)num2;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x0010E714 File Offset: 0x0010C914
		internal static void BE_To_UInt64(byte[] bs, int off, ulong[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.BE_To_UInt64(bs, off);
				off += 8;
			}
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x0010E73E File Offset: 0x0010C93E
		internal static void UInt16_To_LE(ushort n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x0010E74C File Offset: 0x0010C94C
		internal static void UInt16_To_LE(ushort n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[off + 1] = (byte)(n >> 8);
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x0010E75C File Offset: 0x0010C95C
		internal static ushort LE_To_UInt16(byte[] bs)
		{
			return (ushort)((int)bs[0] | (int)bs[1] << 8);
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0010E768 File Offset: 0x0010C968
		internal static ushort LE_To_UInt16(byte[] bs, int off)
		{
			return (ushort)((int)bs[off] | (int)bs[off + 1] << 8);
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0010E778 File Offset: 0x0010C978
		internal static byte[] UInt32_To_LE(uint n)
		{
			byte[] array = new byte[4];
			Pack.UInt32_To_LE(n, array, 0);
			return array;
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0010E795 File Offset: 0x0010C995
		internal static void UInt32_To_LE(uint n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
			bs[2] = (byte)(n >> 16);
			bs[3] = (byte)(n >> 24);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0010E7B3 File Offset: 0x0010C9B3
		internal static void UInt32_To_LE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[off + 1] = (byte)(n >> 8);
			bs[off + 2] = (byte)(n >> 16);
			bs[off + 3] = (byte)(n >> 24);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0010E7D8 File Offset: 0x0010C9D8
		internal static byte[] UInt32_To_LE(uint[] ns)
		{
			byte[] array = new byte[4 * ns.Length];
			Pack.UInt32_To_LE(ns, array, 0);
			return array;
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0010E7FC File Offset: 0x0010C9FC
		internal static void UInt32_To_LE(uint[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt32_To_LE(ns[i], bs, off);
				off += 4;
			}
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x0010E826 File Offset: 0x0010CA26
		internal static uint LE_To_UInt32(byte[] bs)
		{
			return (uint)((int)bs[0] | (int)bs[1] << 8 | (int)bs[2] << 16 | (int)bs[3] << 24);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0010E83F File Offset: 0x0010CA3F
		internal static uint LE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] | (int)bs[off + 1] << 8 | (int)bs[off + 2] << 16 | (int)bs[off + 3] << 24);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x0010E860 File Offset: 0x0010CA60
		internal static void LE_To_UInt32(byte[] bs, int off, uint[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.LE_To_UInt32(bs, off);
				off += 4;
			}
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x0010E88C File Offset: 0x0010CA8C
		internal static void LE_To_UInt32(byte[] bs, int bOff, uint[] ns, int nOff, int count)
		{
			for (int i = 0; i < count; i++)
			{
				ns[nOff + i] = Pack.LE_To_UInt32(bs, bOff);
				bOff += 4;
			}
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x0010E8B8 File Offset: 0x0010CAB8
		internal static uint[] LE_To_UInt32(byte[] bs, int off, int count)
		{
			uint[] array = new uint[count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Pack.LE_To_UInt32(bs, off);
				off += 4;
			}
			return array;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x0010E8EC File Offset: 0x0010CAEC
		internal static byte[] UInt64_To_LE(ulong n)
		{
			byte[] array = new byte[8];
			Pack.UInt64_To_LE(n, array, 0);
			return array;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0010E909 File Offset: 0x0010CB09
		internal static void UInt64_To_LE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_LE((uint)n, bs);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, 4);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0010E91F File Offset: 0x0010CB1F
		internal static void UInt64_To_LE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_LE((uint)n, bs, off);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, off + 4);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x0010E938 File Offset: 0x0010CB38
		internal static byte[] UInt64_To_LE(ulong[] ns)
		{
			byte[] array = new byte[8 * ns.Length];
			Pack.UInt64_To_LE(ns, array, 0);
			return array;
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x0010E95C File Offset: 0x0010CB5C
		internal static void UInt64_To_LE(ulong[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt64_To_LE(ns[i], bs, off);
				off += 8;
			}
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x0010E988 File Offset: 0x0010CB88
		internal static void UInt64_To_LE(ulong[] ns, int nsOff, int nsLen, byte[] bs, int bsOff)
		{
			for (int i = 0; i < nsLen; i++)
			{
				Pack.UInt64_To_LE(ns[nsOff + i], bs, bsOff);
				bsOff += 8;
			}
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0010E9B4 File Offset: 0x0010CBB4
		internal static ulong LE_To_UInt64(byte[] bs)
		{
			uint num = Pack.LE_To_UInt32(bs);
			return (ulong)Pack.LE_To_UInt32(bs, 4) << 32 | (ulong)num;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0010E9D8 File Offset: 0x0010CBD8
		internal static ulong LE_To_UInt64(byte[] bs, int off)
		{
			uint num = Pack.LE_To_UInt32(bs, off);
			return (ulong)Pack.LE_To_UInt32(bs, off + 4) << 32 | (ulong)num;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0010EA00 File Offset: 0x0010CC00
		internal static void LE_To_UInt64(byte[] bs, int off, ulong[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.LE_To_UInt64(bs, off);
				off += 8;
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x0010EA2C File Offset: 0x0010CC2C
		internal static void LE_To_UInt64(byte[] bs, int bsOff, ulong[] ns, int nsOff, int nsLen)
		{
			for (int i = 0; i < nsLen; i++)
			{
				ns[nsOff + i] = Pack.LE_To_UInt64(bs, bsOff);
				bsOff += 8;
			}
		}
	}
}
