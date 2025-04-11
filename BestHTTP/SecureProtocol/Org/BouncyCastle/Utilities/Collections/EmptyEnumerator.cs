using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000281 RID: 641
	public sealed class EmptyEnumerator : IEnumerator
	{
		// Token: 0x0600179D RID: 6045 RVA: 0x00023EF4 File Offset: 0x000220F4
		private EmptyEnumerator()
		{
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00002B75 File Offset: 0x00000D75
		public void Reset()
		{
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000BB113 File Offset: 0x000B9313
		public object Current
		{
			get
			{
				throw new InvalidOperationException("No elements");
			}
		}

		// Token: 0x040016EF RID: 5871
		public static readonly IEnumerator Instance = new EmptyEnumerator();
	}
}
