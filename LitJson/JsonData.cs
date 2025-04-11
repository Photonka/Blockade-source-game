using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace LitJson
{
	// Token: 0x02000149 RID: 329
	public sealed class JsonData : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary, IEquatable<JsonData>
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0008D35A File Offset: 0x0008B55A
		public int Count
		{
			get
			{
				return this.EnsureCollection().Count;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0008D367 File Offset: 0x0008B567
		public bool IsArray
		{
			get
			{
				return this.type == JsonType.Array;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0008D372 File Offset: 0x0008B572
		public bool IsBoolean
		{
			get
			{
				return this.type == JsonType.Boolean;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0008D37D File Offset: 0x0008B57D
		public bool IsDouble
		{
			get
			{
				return this.type == JsonType.Double;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0008D388 File Offset: 0x0008B588
		public bool IsInt
		{
			get
			{
				return this.type == JsonType.Int;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0008D393 File Offset: 0x0008B593
		public bool IsLong
		{
			get
			{
				return this.type == JsonType.Long;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0008D39E File Offset: 0x0008B59E
		public bool IsObject
		{
			get
			{
				return this.type == JsonType.Object;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0008D3A9 File Offset: 0x0008B5A9
		public bool IsString
		{
			get
			{
				return this.type == JsonType.String;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0008D3B4 File Offset: 0x0008B5B4
		public ICollection<string> Keys
		{
			get
			{
				this.EnsureDictionary();
				return this.inst_object.Keys;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0008D3C8 File Offset: 0x0008B5C8
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0008D3D0 File Offset: 0x0008B5D0
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.EnsureCollection().IsSynchronized;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0008D3DD File Offset: 0x0008B5DD
		object ICollection.SyncRoot
		{
			get
			{
				return this.EnsureCollection().SyncRoot;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0008D3EA File Offset: 0x0008B5EA
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.EnsureDictionary().IsFixedSize;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0008D3F7 File Offset: 0x0008B5F7
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.EnsureDictionary().IsReadOnly;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0008D404 File Offset: 0x0008B604
		ICollection IDictionary.Keys
		{
			get
			{
				this.EnsureDictionary();
				IList<string> list = new List<string>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Key);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0008D46C File Offset: 0x0008B66C
		ICollection IDictionary.Values
		{
			get
			{
				this.EnsureDictionary();
				IList<JsonData> list = new List<JsonData>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Value);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0008D4D4 File Offset: 0x0008B6D4
		bool IJsonWrapper.IsArray
		{
			get
			{
				return this.IsArray;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0008D4DC File Offset: 0x0008B6DC
		bool IJsonWrapper.IsBoolean
		{
			get
			{
				return this.IsBoolean;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0008D4E4 File Offset: 0x0008B6E4
		bool IJsonWrapper.IsDouble
		{
			get
			{
				return this.IsDouble;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0008D4EC File Offset: 0x0008B6EC
		bool IJsonWrapper.IsInt
		{
			get
			{
				return this.IsInt;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0008D4F4 File Offset: 0x0008B6F4
		bool IJsonWrapper.IsLong
		{
			get
			{
				return this.IsLong;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0008D4FC File Offset: 0x0008B6FC
		bool IJsonWrapper.IsObject
		{
			get
			{
				return this.IsObject;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0008D504 File Offset: 0x0008B704
		bool IJsonWrapper.IsString
		{
			get
			{
				return this.IsString;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0008D50C File Offset: 0x0008B70C
		bool IList.IsFixedSize
		{
			get
			{
				return this.EnsureList().IsFixedSize;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0008D519 File Offset: 0x0008B719
		bool IList.IsReadOnly
		{
			get
			{
				return this.EnsureList().IsReadOnly;
			}
		}

		// Token: 0x17000091 RID: 145
		object IDictionary.this[object key]
		{
			get
			{
				return this.EnsureDictionary()[key];
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException("The key has to be a string");
				}
				JsonData value2 = this.ToJsonData(value);
				this[(string)key] = value2;
			}
		}

		// Token: 0x17000092 RID: 146
		object IOrderedDictionary.this[int idx]
		{
			get
			{
				this.EnsureDictionary();
				return this.object_list[idx].Value;
			}
			set
			{
				this.EnsureDictionary();
				JsonData value2 = this.ToJsonData(value);
				KeyValuePair<string, JsonData> keyValuePair = this.object_list[idx];
				this.inst_object[keyValuePair.Key] = value2;
				KeyValuePair<string, JsonData> value3 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value2);
				this.object_list[idx] = value3;
			}
		}

		// Token: 0x17000093 RID: 147
		object IList.this[int index]
		{
			get
			{
				return this.EnsureList()[index];
			}
			set
			{
				this.EnsureList();
				JsonData value2 = this.ToJsonData(value);
				this[index] = value2;
			}
		}

		// Token: 0x17000094 RID: 148
		public JsonData this[string prop_name]
		{
			get
			{
				this.EnsureDictionary();
				return this.inst_object[prop_name];
			}
			set
			{
				this.EnsureDictionary();
				KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(prop_name, value);
				if (this.inst_object.ContainsKey(prop_name))
				{
					for (int i = 0; i < this.object_list.Count; i++)
					{
						if (this.object_list[i].Key == prop_name)
						{
							this.object_list[i] = keyValuePair;
							break;
						}
					}
				}
				else
				{
					this.object_list.Add(keyValuePair);
				}
				this.inst_object[prop_name] = value;
				this.json = null;
			}
		}

		// Token: 0x17000095 RID: 149
		public JsonData this[int index]
		{
			get
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					return this.inst_array[index];
				}
				return this.object_list[index].Value;
			}
			set
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					this.inst_array[index] = value;
				}
				else
				{
					KeyValuePair<string, JsonData> keyValuePair = this.object_list[index];
					KeyValuePair<string, JsonData> value2 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value);
					this.object_list[index] = value2;
					this.inst_object[keyValuePair.Key] = value;
				}
				this.json = null;
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00023EF4 File Offset: 0x000220F4
		public JsonData()
		{
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0008D777 File Offset: 0x0008B977
		public JsonData(bool boolean)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = boolean;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0008D78D File Offset: 0x0008B98D
		public JsonData(double number)
		{
			this.type = JsonType.Double;
			this.inst_double = number;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0008D7A3 File Offset: 0x0008B9A3
		public JsonData(int number)
		{
			this.type = JsonType.Int;
			this.inst_int = number;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0008D7B9 File Offset: 0x0008B9B9
		public JsonData(long number)
		{
			this.type = JsonType.Long;
			this.inst_long = number;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0008D7D0 File Offset: 0x0008B9D0
		public JsonData(object obj)
		{
			if (obj is bool)
			{
				this.type = JsonType.Boolean;
				this.inst_boolean = (bool)obj;
				return;
			}
			if (obj is double)
			{
				this.type = JsonType.Double;
				this.inst_double = (double)obj;
				return;
			}
			if (obj is int)
			{
				this.type = JsonType.Int;
				this.inst_int = (int)obj;
				return;
			}
			if (obj is long)
			{
				this.type = JsonType.Long;
				this.inst_long = (long)obj;
				return;
			}
			if (obj is string)
			{
				this.type = JsonType.String;
				this.inst_string = (string)obj;
				return;
			}
			throw new ArgumentException("Unable to wrap the given object with JsonData");
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0008D879 File Offset: 0x0008BA79
		public JsonData(string str)
		{
			this.type = JsonType.String;
			this.inst_string = str;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0008D88F File Offset: 0x0008BA8F
		public static implicit operator JsonData(bool data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0008D897 File Offset: 0x0008BA97
		public static implicit operator JsonData(double data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0008D89F File Offset: 0x0008BA9F
		public static implicit operator JsonData(int data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0008D8A7 File Offset: 0x0008BAA7
		public static implicit operator JsonData(long data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0008D8AF File Offset: 0x0008BAAF
		public static implicit operator JsonData(string data)
		{
			return new JsonData(data);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0008D8B7 File Offset: 0x0008BAB7
		public static explicit operator bool(JsonData data)
		{
			if (data.type != JsonType.Boolean)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_boolean;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0008D8D3 File Offset: 0x0008BAD3
		public static explicit operator double(JsonData data)
		{
			if (data.type != JsonType.Double)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_double;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0008D8EF File Offset: 0x0008BAEF
		public static explicit operator int(JsonData data)
		{
			if (data.type != JsonType.Int)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_int;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0008D90B File Offset: 0x0008BB0B
		public static explicit operator long(JsonData data)
		{
			if (data.type != JsonType.Long)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_long;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0008D927 File Offset: 0x0008BB27
		public static explicit operator string(JsonData data)
		{
			if (data.type != JsonType.String)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a string");
			}
			return data.inst_string;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0008D943 File Offset: 0x0008BB43
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureCollection().CopyTo(array, index);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0008D954 File Offset: 0x0008BB54
		void IDictionary.Add(object key, object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.EnsureDictionary().Add(key, value2);
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>((string)key, value2);
			this.object_list.Add(item);
			this.json = null;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0008D997 File Offset: 0x0008BB97
		void IDictionary.Clear()
		{
			this.EnsureDictionary().Clear();
			this.object_list.Clear();
			this.json = null;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0008D9B6 File Offset: 0x0008BBB6
		bool IDictionary.Contains(object key)
		{
			return this.EnsureDictionary().Contains(key);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0008D9C4 File Offset: 0x0008BBC4
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IOrderedDictionary)this).GetEnumerator();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0008D9CC File Offset: 0x0008BBCC
		void IDictionary.Remove(object key)
		{
			this.EnsureDictionary().Remove(key);
			for (int i = 0; i < this.object_list.Count; i++)
			{
				if (this.object_list[i].Key == (string)key)
				{
					this.object_list.RemoveAt(i);
					break;
				}
			}
			this.json = null;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0008DA31 File Offset: 0x0008BC31
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.EnsureCollection().GetEnumerator();
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0008DA3E File Offset: 0x0008BC3E
		bool IJsonWrapper.GetBoolean()
		{
			if (this.type != JsonType.Boolean)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a boolean");
			}
			return this.inst_boolean;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0008DA5A File Offset: 0x0008BC5A
		double IJsonWrapper.GetDouble()
		{
			if (this.type != JsonType.Double)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a double");
			}
			return this.inst_double;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0008DA76 File Offset: 0x0008BC76
		int IJsonWrapper.GetInt()
		{
			if (this.type != JsonType.Int)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold an int");
			}
			return this.inst_int;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0008DA92 File Offset: 0x0008BC92
		long IJsonWrapper.GetLong()
		{
			if (this.type != JsonType.Long)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a long");
			}
			return this.inst_long;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0008DAAE File Offset: 0x0008BCAE
		string IJsonWrapper.GetString()
		{
			if (this.type != JsonType.String)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a string");
			}
			return this.inst_string;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0008DACA File Offset: 0x0008BCCA
		void IJsonWrapper.SetBoolean(bool val)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = val;
			this.json = null;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0008DAE1 File Offset: 0x0008BCE1
		void IJsonWrapper.SetDouble(double val)
		{
			this.type = JsonType.Double;
			this.inst_double = val;
			this.json = null;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0008DAF8 File Offset: 0x0008BCF8
		void IJsonWrapper.SetInt(int val)
		{
			this.type = JsonType.Int;
			this.inst_int = val;
			this.json = null;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0008DB0F File Offset: 0x0008BD0F
		void IJsonWrapper.SetLong(long val)
		{
			this.type = JsonType.Long;
			this.inst_long = val;
			this.json = null;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0008DB26 File Offset: 0x0008BD26
		void IJsonWrapper.SetString(string val)
		{
			this.type = JsonType.String;
			this.inst_string = val;
			this.json = null;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0008DB3D File Offset: 0x0008BD3D
		string IJsonWrapper.ToJson()
		{
			return this.ToJson();
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0008DB45 File Offset: 0x0008BD45
		void IJsonWrapper.ToJson(JsonWriter writer)
		{
			this.ToJson(writer);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0008DB4E File Offset: 0x0008BD4E
		int IList.Add(object value)
		{
			return this.Add(value);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0008DB57 File Offset: 0x0008BD57
		void IList.Clear()
		{
			this.EnsureList().Clear();
			this.json = null;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0008DB6B File Offset: 0x0008BD6B
		bool IList.Contains(object value)
		{
			return this.EnsureList().Contains(value);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0008DB79 File Offset: 0x0008BD79
		int IList.IndexOf(object value)
		{
			return this.EnsureList().IndexOf(value);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0008DB87 File Offset: 0x0008BD87
		void IList.Insert(int index, object value)
		{
			this.EnsureList().Insert(index, value);
			this.json = null;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0008DB9D File Offset: 0x0008BD9D
		void IList.Remove(object value)
		{
			this.EnsureList().Remove(value);
			this.json = null;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0008DBB2 File Offset: 0x0008BDB2
		void IList.RemoveAt(int index)
		{
			this.EnsureList().RemoveAt(index);
			this.json = null;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0008DBC7 File Offset: 0x0008BDC7
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			this.EnsureDictionary();
			return new OrderedDictionaryEnumerator(this.object_list.GetEnumerator());
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0008DBE0 File Offset: 0x0008BDE0
		void IOrderedDictionary.Insert(int idx, object key, object value)
		{
			string text = (string)key;
			JsonData value2 = this.ToJsonData(value);
			this[text] = value2;
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>(text, value2);
			this.object_list.Insert(idx, item);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0008DC1C File Offset: 0x0008BE1C
		void IOrderedDictionary.RemoveAt(int idx)
		{
			this.EnsureDictionary();
			this.inst_object.Remove(this.object_list[idx].Key);
			this.object_list.RemoveAt(idx);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0008DC5C File Offset: 0x0008BE5C
		private ICollection EnsureCollection()
		{
			if (this.type == JsonType.Array)
			{
				return (ICollection)this.inst_array;
			}
			if (this.type == JsonType.Object)
			{
				return (ICollection)this.inst_object;
			}
			throw new InvalidOperationException("The JsonData instance has to be initialized first");
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0008DC94 File Offset: 0x0008BE94
		private IDictionary EnsureDictionary()
		{
			if (this.type == JsonType.Object)
			{
				return (IDictionary)this.inst_object;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a dictionary");
			}
			this.type = JsonType.Object;
			this.inst_object = new Dictionary<string, JsonData>();
			this.object_list = new List<KeyValuePair<string, JsonData>>();
			return (IDictionary)this.inst_object;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0008DCF4 File Offset: 0x0008BEF4
		private IList EnsureList()
		{
			if (this.type == JsonType.Array)
			{
				return (IList)this.inst_array;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a list");
			}
			this.type = JsonType.Array;
			this.inst_array = new List<JsonData>();
			return (IList)this.inst_array;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0008DD46 File Offset: 0x0008BF46
		private JsonData ToJsonData(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is JsonData)
			{
				return (JsonData)obj;
			}
			return new JsonData(obj);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0008DD64 File Offset: 0x0008BF64
		private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
		{
			if (obj == null)
			{
				writer.Write(null);
				return;
			}
			if (obj.IsString)
			{
				writer.Write(obj.GetString());
				return;
			}
			if (obj.IsBoolean)
			{
				writer.Write(obj.GetBoolean());
				return;
			}
			if (obj.IsDouble)
			{
				writer.Write(obj.GetDouble());
				return;
			}
			if (obj.IsInt)
			{
				writer.Write(obj.GetInt());
				return;
			}
			if (obj.IsLong)
			{
				writer.Write(obj.GetLong());
				return;
			}
			if (obj.IsArray)
			{
				writer.WriteArrayStart();
				foreach (object obj2 in obj)
				{
					JsonData.WriteJson((JsonData)obj2, writer);
				}
				writer.WriteArrayEnd();
				return;
			}
			if (obj.IsObject)
			{
				writer.WriteObjectStart();
				foreach (object obj3 in obj)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					writer.WritePropertyName((string)dictionaryEntry.Key);
					JsonData.WriteJson((JsonData)dictionaryEntry.Value, writer);
				}
				writer.WriteObjectEnd();
				return;
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0008DEB4 File Offset: 0x0008C0B4
		public int Add(object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.json = null;
			return this.EnsureList().Add(value2);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0008DEDC File Offset: 0x0008C0DC
		public void Clear()
		{
			if (this.IsObject)
			{
				((IDictionary)this).Clear();
				return;
			}
			if (this.IsArray)
			{
				((IList)this).Clear();
				return;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0008DEFC File Offset: 0x0008C0FC
		public bool Equals(JsonData x)
		{
			if (x == null)
			{
				return false;
			}
			if (x.type != this.type)
			{
				return false;
			}
			switch (this.type)
			{
			case JsonType.None:
				return true;
			case JsonType.Object:
				return this.inst_object.Equals(x.inst_object);
			case JsonType.Array:
				return this.inst_array.Equals(x.inst_array);
			case JsonType.String:
				return this.inst_string.Equals(x.inst_string);
			case JsonType.Int:
				return this.inst_int.Equals(x.inst_int);
			case JsonType.Long:
				return this.inst_long.Equals(x.inst_long);
			case JsonType.Double:
				return this.inst_double.Equals(x.inst_double);
			case JsonType.Boolean:
				return this.inst_boolean.Equals(x.inst_boolean);
			default:
				return false;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0008DFD1 File Offset: 0x0008C1D1
		public JsonType GetJsonType()
		{
			return this.type;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0008DFDC File Offset: 0x0008C1DC
		public void SetJsonType(JsonType type)
		{
			if (this.type == type)
			{
				return;
			}
			switch (type)
			{
			case JsonType.Object:
				this.inst_object = new Dictionary<string, JsonData>();
				this.object_list = new List<KeyValuePair<string, JsonData>>();
				break;
			case JsonType.Array:
				this.inst_array = new List<JsonData>();
				break;
			case JsonType.String:
				this.inst_string = null;
				break;
			case JsonType.Int:
				this.inst_int = 0;
				break;
			case JsonType.Long:
				this.inst_long = 0L;
				break;
			case JsonType.Double:
				this.inst_double = 0.0;
				break;
			case JsonType.Boolean:
				this.inst_boolean = false;
				break;
			}
			this.type = type;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0008E07C File Offset: 0x0008C27C
		public string ToJson()
		{
			if (this.json != null)
			{
				return this.json;
			}
			StringWriter stringWriter = new StringWriter();
			JsonData.WriteJson(this, new JsonWriter(stringWriter)
			{
				Validate = false
			});
			this.json = stringWriter.ToString();
			return this.json;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0008E0C8 File Offset: 0x0008C2C8
		public void ToJson(JsonWriter writer)
		{
			bool validate = writer.Validate;
			writer.Validate = false;
			JsonData.WriteJson(this, writer);
			writer.Validate = validate;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0008E0F4 File Offset: 0x0008C2F4
		public override string ToString()
		{
			switch (this.type)
			{
			case JsonType.Object:
				return "JsonData object";
			case JsonType.Array:
				return "JsonData array";
			case JsonType.String:
				return this.inst_string;
			case JsonType.Int:
				return this.inst_int.ToString();
			case JsonType.Long:
				return this.inst_long.ToString();
			case JsonType.Double:
				return this.inst_double.ToString();
			case JsonType.Boolean:
				return this.inst_boolean.ToString();
			default:
				return "Uninitialized JsonData";
			}
		}

		// Token: 0x040010E3 RID: 4323
		private IList<JsonData> inst_array;

		// Token: 0x040010E4 RID: 4324
		private bool inst_boolean;

		// Token: 0x040010E5 RID: 4325
		private double inst_double;

		// Token: 0x040010E6 RID: 4326
		private int inst_int;

		// Token: 0x040010E7 RID: 4327
		private long inst_long;

		// Token: 0x040010E8 RID: 4328
		private IDictionary<string, JsonData> inst_object;

		// Token: 0x040010E9 RID: 4329
		private string inst_string;

		// Token: 0x040010EA RID: 4330
		private string json;

		// Token: 0x040010EB RID: 4331
		private JsonType type;

		// Token: 0x040010EC RID: 4332
		private IList<KeyValuePair<string, JsonData>> object_list;
	}
}
