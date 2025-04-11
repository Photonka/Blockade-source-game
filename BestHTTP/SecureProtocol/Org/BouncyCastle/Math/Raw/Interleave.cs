using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002F1 RID: 753
	internal abstract class Interleave
	{
		// Token: 0x06001C03 RID: 7171 RVA: 0x000D79E0 File Offset: 0x000D5BE0
		internal static uint Expand8to16(uint x)
		{
			x &= 255U;
			x = ((x | x << 4) & 3855U);
			x = ((x | x << 2) & 13107U);
			x = ((x | x << 1) & 21845U);
			return x;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000D7A13 File Offset: 0x000D5C13
		internal static uint Expand16to32(uint x)
		{
			x &= 65535U;
			x = ((x | x << 8) & 16711935U);
			x = ((x | x << 4) & 252645135U);
			x = ((x | x << 2) & 858993459U);
			x = ((x | x << 1) & 1431655765U);
			return x;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000D7A54 File Offset: 0x000D5C54
		internal static ulong Expand32to64(uint x)
		{
			uint num = (x ^ x >> 8) & 65280U;
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 202116108U);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 572662306U);
			x ^= (num ^ num << 1);
			return ((ulong)(x >> 1) & 1431655765UL) << 32 | ((ulong)x & 1431655765UL);
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000D7AD0 File Offset: 0x000D5CD0
		internal static void Expand64To128(ulong x, ulong[] z, int zOff)
		{
			ulong num = (x ^ x >> 16) & (ulong)-65536;
			x ^= (num ^ num << 16);
			num = ((x ^ x >> 8) & 280375465148160UL);
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 67555025218437360UL);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 868082074056920076UL);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 2459565876494606882UL);
			x ^= (num ^ num << 1);
			z[zOff] = (x & 6148914691236517205UL);
			z[zOff + 1] = (x >> 1 & 6148914691236517205UL);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000D7B7C File Offset: 0x000D5D7C
		internal static void Expand64To128Rev(ulong x, ulong[] z, int zOff)
		{
			ulong num = (x ^ x >> 16) & (ulong)-65536;
			x ^= (num ^ num << 16);
			num = ((x ^ x >> 8) & 280375465148160UL);
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 67555025218437360UL);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 868082074056920076UL);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 2459565876494606882UL);
			x ^= (num ^ num << 1);
			z[zOff] = (x & 12297829382473034410UL);
			z[zOff + 1] = (x << 1 & 12297829382473034410UL);
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000D7C28 File Offset: 0x000D5E28
		internal static uint Shuffle(uint x)
		{
			uint num = (x ^ x >> 8) & 65280U;
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 202116108U);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 572662306U);
			x ^= (num ^ num << 1);
			return x;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000D7C8C File Offset: 0x000D5E8C
		internal static ulong Shuffle(ulong x)
		{
			ulong num = (x ^ x >> 16) & (ulong)-65536;
			x ^= (num ^ num << 16);
			num = ((x ^ x >> 8) & 280375465148160UL);
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 67555025218437360UL);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 868082074056920076UL);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 2459565876494606882UL);
			x ^= (num ^ num << 1);
			return x;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000D7D18 File Offset: 0x000D5F18
		internal static uint Shuffle2(uint x)
		{
			uint num = (x ^ x >> 7) & 11141290U;
			x ^= (num ^ num << 7);
			num = ((x ^ x >> 14) & 52428U);
			x ^= (num ^ num << 14);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 8) & 65280U);
			x ^= (num ^ num << 8);
			return x;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000D7D7C File Offset: 0x000D5F7C
		internal static uint Unshuffle(uint x)
		{
			uint num = (x ^ x >> 1) & 572662306U;
			x ^= (num ^ num << 1);
			num = ((x ^ x >> 2) & 202116108U);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 8) & 65280U);
			x ^= (num ^ num << 8);
			return x;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000D7DE0 File Offset: 0x000D5FE0
		internal static ulong Unshuffle(ulong x)
		{
			ulong num = (x ^ x >> 1) & 2459565876494606882UL;
			x ^= (num ^ num << 1);
			num = ((x ^ x >> 2) & 868082074056920076UL);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 4) & 67555025218437360UL);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 8) & 280375465148160UL);
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 16) & (ulong)-65536);
			x ^= (num ^ num << 16);
			return x;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000D7E6C File Offset: 0x000D606C
		internal static uint Unshuffle2(uint x)
		{
			uint num = (x ^ x >> 8) & 65280U;
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 14) & 52428U);
			x ^= (num ^ num << 14);
			num = ((x ^ x >> 7) & 11141290U);
			x ^= (num ^ num << 7);
			return x;
		}

		// Token: 0x04001801 RID: 6145
		private const ulong M32 = 1431655765UL;

		// Token: 0x04001802 RID: 6146
		private const ulong M64 = 6148914691236517205UL;

		// Token: 0x04001803 RID: 6147
		private const ulong M64R = 12297829382473034410UL;
	}
}
