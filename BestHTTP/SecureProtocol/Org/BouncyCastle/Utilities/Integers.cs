using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities
{
	// Token: 0x0200024E RID: 590
	public abstract class Integers
	{
		// Token: 0x060015F8 RID: 5624 RVA: 0x000B2335 File Offset: 0x000B0535
		public static int RotateLeft(int i, int distance)
		{
			return i << distance ^ (int)((uint)i >> -distance);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000B2335 File Offset: 0x000B0535
		[CLSCompliant(false)]
		public static uint RotateLeft(uint i, int distance)
		{
			return i << distance ^ i >> -distance;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000B2345 File Offset: 0x000B0545
		public static int RotateRight(int i, int distance)
		{
			return (int)((uint)i >> distance ^ (uint)((uint)i << -distance));
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000B2345 File Offset: 0x000B0545
		[CLSCompliant(false)]
		public static uint RotateRight(uint i, int distance)
		{
			return i >> distance ^ i << -distance;
		}
	}
}
