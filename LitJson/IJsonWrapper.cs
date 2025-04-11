using System;
using System.Collections;

namespace LitJson
{
	// Token: 0x02000148 RID: 328
	public interface IJsonWrapper : IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000B24 RID: 2852
		bool IsArray { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000B25 RID: 2853
		bool IsBoolean { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000B26 RID: 2854
		bool IsDouble { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000B27 RID: 2855
		bool IsInt { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000B28 RID: 2856
		bool IsLong { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000B29 RID: 2857
		bool IsObject { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000B2A RID: 2858
		bool IsString { get; }

		// Token: 0x06000B2B RID: 2859
		bool GetBoolean();

		// Token: 0x06000B2C RID: 2860
		double GetDouble();

		// Token: 0x06000B2D RID: 2861
		int GetInt();

		// Token: 0x06000B2E RID: 2862
		JsonType GetJsonType();

		// Token: 0x06000B2F RID: 2863
		long GetLong();

		// Token: 0x06000B30 RID: 2864
		string GetString();

		// Token: 0x06000B31 RID: 2865
		void SetBoolean(bool val);

		// Token: 0x06000B32 RID: 2866
		void SetDouble(double val);

		// Token: 0x06000B33 RID: 2867
		void SetInt(int val);

		// Token: 0x06000B34 RID: 2868
		void SetJsonType(JsonType type);

		// Token: 0x06000B35 RID: 2869
		void SetLong(long val);

		// Token: 0x06000B36 RID: 2870
		void SetString(string val);

		// Token: 0x06000B37 RID: 2871
		string ToJson();

		// Token: 0x06000B38 RID: 2872
		void ToJson(JsonWriter writer);
	}
}
