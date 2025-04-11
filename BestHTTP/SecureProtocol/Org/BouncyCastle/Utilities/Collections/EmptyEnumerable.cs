using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000280 RID: 640
	public sealed class EmptyEnumerable : IEnumerable
	{
		// Token: 0x0600179A RID: 6042 RVA: 0x00023EF4 File Offset: 0x000220F4
		private EmptyEnumerable()
		{
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x000BB100 File Offset: 0x000B9300
		public IEnumerator GetEnumerator()
		{
			return EmptyEnumerator.Instance;
		}

		// Token: 0x040016EE RID: 5870
		public static readonly IEnumerable Instance = new EmptyEnumerable();
	}
}
