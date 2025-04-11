using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000282 RID: 642
	public sealed class EnumerableProxy : IEnumerable
	{
		// Token: 0x060017A2 RID: 6050 RVA: 0x000BB12B File Offset: 0x000B932B
		public EnumerableProxy(IEnumerable inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			this.inner = inner;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000BB148 File Offset: 0x000B9348
		public IEnumerator GetEnumerator()
		{
			return this.inner.GetEnumerator();
		}

		// Token: 0x040016F0 RID: 5872
		private readonly IEnumerable inner;
	}
}
