using System;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x0200014E RID: 334
	internal struct ObjectMetadata
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0008E2E4 File Offset: 0x0008C4E4
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x0008E305 File Offset: 0x0008C505
		public Type ElementType
		{
			get
			{
				if (this.element_type == null)
				{
					return typeof(JsonData);
				}
				return this.element_type;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0008E30E File Offset: 0x0008C50E
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x0008E316 File Offset: 0x0008C516
		public bool IsDictionary
		{
			get
			{
				return this.is_dictionary;
			}
			set
			{
				this.is_dictionary = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0008E31F File Offset: 0x0008C51F
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x0008E327 File Offset: 0x0008C527
		public IDictionary<string, PropertyMetadata> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x040010F4 RID: 4340
		private Type element_type;

		// Token: 0x040010F5 RID: 4341
		private bool is_dictionary;

		// Token: 0x040010F6 RID: 4342
		private IDictionary<string, PropertyMetadata> properties;
	}
}
