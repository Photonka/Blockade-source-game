using System;
using System.Collections;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x0200014A RID: 330
	internal class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0008E176 File Offset: 0x0008C376
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0008E184 File Offset: 0x0008C384
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0008E1B0 File Offset: 0x0008C3B0
		public object Key
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0008E1D0 File Offset: 0x0008C3D0
		public object Value
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0008E1F0 File Offset: 0x0008C3F0
		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
		{
			this.list_enumerator = enumerator;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0008E1FF File Offset: 0x0008C3FF
		public bool MoveNext()
		{
			return this.list_enumerator.MoveNext();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0008E20C File Offset: 0x0008C40C
		public void Reset()
		{
			this.list_enumerator.Reset();
		}

		// Token: 0x040010ED RID: 4333
		private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;
	}
}
