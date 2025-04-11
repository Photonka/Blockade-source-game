using System;
using System.Collections;

namespace LitJson
{
	// Token: 0x02000155 RID: 341
	public sealed class JsonMockWrapper : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsBoolean
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsDouble
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsInt
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsLong
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsString
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool GetBoolean()
		{
			return false;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0008F858 File Offset: 0x0008DA58
		public double GetDouble()
		{
			return 0.0;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public int GetInt()
		{
			return 0;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public JsonType GetJsonType()
		{
			return JsonType.None;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0008F863 File Offset: 0x0008DA63
		public long GetLong()
		{
			return 0L;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0008F867 File Offset: 0x0008DA67
		public string GetString()
		{
			return "";
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00002B75 File Offset: 0x00000D75
		public void SetBoolean(bool val)
		{
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00002B75 File Offset: 0x00000D75
		public void SetDouble(double val)
		{
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00002B75 File Offset: 0x00000D75
		public void SetInt(int val)
		{
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00002B75 File Offset: 0x00000D75
		public void SetJsonType(JsonType type)
		{
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00002B75 File Offset: 0x00000D75
		public void SetLong(long val)
		{
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00002B75 File Offset: 0x00000D75
		public void SetString(string val)
		{
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0008F867 File Offset: 0x0008DA67
		public string ToJson()
		{
			return "";
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00002B75 File Offset: 0x00000D75
		public void ToJson(JsonWriter writer)
		{
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0006CF70 File Offset: 0x0006B170
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0006CF70 File Offset: 0x0006B170
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A9 RID: 169
		object IList.this[int index]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		int IList.Add(object value)
		{
			return 0;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00002B75 File Offset: 0x00000D75
		void IList.Clear()
		{
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		bool IList.Contains(object value)
		{
			return false;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0008F871 File Offset: 0x0008DA71
		int IList.IndexOf(object value)
		{
			return -1;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00002B75 File Offset: 0x00000D75
		void IList.Insert(int i, object v)
		{
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00002B75 File Offset: 0x00000D75
		void IList.Remove(object value)
		{
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00002B75 File Offset: 0x00000D75
		void IList.RemoveAt(int index)
		{
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		int ICollection.Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0008F86E File Offset: 0x0008DA6E
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00002B75 File Offset: 0x00000D75
		void ICollection.CopyTo(Array array, int index)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0008F86E File Offset: 0x0008DA6E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0006CF70 File Offset: 0x0006B170
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0006CF70 File Offset: 0x0006B170
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0008F86E File Offset: 0x0008DA6E
		ICollection IDictionary.Keys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0008F86E File Offset: 0x0008DA6E
		ICollection IDictionary.Values
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B1 RID: 177
		object IDictionary.this[object key]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00002B75 File Offset: 0x00000D75
		void IDictionary.Add(object k, object v)
		{
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00002B75 File Offset: 0x00000D75
		void IDictionary.Clear()
		{
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		bool IDictionary.Contains(object key)
		{
			return false;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00002B75 File Offset: 0x00000D75
		void IDictionary.Remove(object key)
		{
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0008F86E File Offset: 0x0008DA6E
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return null;
		}

		// Token: 0x170000B2 RID: 178
		object IOrderedDictionary.this[int idx]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0008F86E File Offset: 0x0008DA6E
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			return null;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00002B75 File Offset: 0x00000D75
		void IOrderedDictionary.Insert(int i, object k, object v)
		{
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00002B75 File Offset: 0x00000D75
		void IOrderedDictionary.RemoveAt(int i)
		{
		}
	}
}
