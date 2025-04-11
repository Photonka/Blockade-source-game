using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x0200031C RID: 796
	public class SimpleLookupTable : ECLookupTable
	{
		// Token: 0x06001F08 RID: 7944 RVA: 0x000E9AB4 File Offset: 0x000E7CB4
		private static ECPoint[] Copy(ECPoint[] points, int off, int len)
		{
			ECPoint[] array = new ECPoint[len];
			for (int i = 0; i < len; i++)
			{
				array[i] = points[off + i];
			}
			return array;
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000E9ADD File Offset: 0x000E7CDD
		public SimpleLookupTable(ECPoint[] points, int off, int len)
		{
			this.points = SimpleLookupTable.Copy(points, off, len);
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000E9AF3 File Offset: 0x000E7CF3
		public virtual int Size
		{
			get
			{
				return this.points.Length;
			}
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000E9AFD File Offset: 0x000E7CFD
		public virtual ECPoint Lookup(int index)
		{
			return this.points[index];
		}

		// Token: 0x04001847 RID: 6215
		private readonly ECPoint[] points;
	}
}
