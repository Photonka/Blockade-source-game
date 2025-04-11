using System;
using System.Collections.Generic;

namespace BestHTTP.Extensions
{
	// Token: 0x020007DB RID: 2011
	public class KeyValuePairList
	{
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x0019A436 File Offset: 0x00198636
		// (set) Token: 0x060047E4 RID: 18404 RVA: 0x0019A43E File Offset: 0x0019863E
		public List<HeaderValue> Values { get; protected set; }

		// Token: 0x060047E5 RID: 18405 RVA: 0x0019A448 File Offset: 0x00198648
		public bool TryGet(string valueKeyName, out HeaderValue param)
		{
			param = null;
			for (int i = 0; i < this.Values.Count; i++)
			{
				if (string.CompareOrdinal(this.Values[i].Key, valueKeyName) == 0)
				{
					param = this.Values[i];
					return true;
				}
			}
			return false;
		}
	}
}
