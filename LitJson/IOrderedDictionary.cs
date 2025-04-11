using System;
using System.Collections;

namespace LitJson
{
	// Token: 0x02000147 RID: 327
	public interface IOrderedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000B1F RID: 2847
		IDictionaryEnumerator GetEnumerator();

		// Token: 0x06000B20 RID: 2848
		void Insert(int index, object key, object value);

		// Token: 0x06000B21 RID: 2849
		void RemoveAt(int index);

		// Token: 0x17000070 RID: 112
		object this[int index]
		{
			get;
			set;
		}
	}
}
