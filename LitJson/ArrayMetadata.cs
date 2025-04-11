using System;

namespace LitJson
{
	// Token: 0x0200014D RID: 333
	internal struct ArrayMetadata
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0008E298 File Offset: 0x0008C498
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0008E2B9 File Offset: 0x0008C4B9
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0008E2C2 File Offset: 0x0008C4C2
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0008E2CA File Offset: 0x0008C4CA
		public bool IsArray
		{
			get
			{
				return this.is_array;
			}
			set
			{
				this.is_array = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0008E2D3 File Offset: 0x0008C4D3
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0008E2DB File Offset: 0x0008C4DB
		public bool IsList
		{
			get
			{
				return this.is_list;
			}
			set
			{
				this.is_list = value;
			}
		}

		// Token: 0x040010F1 RID: 4337
		private Type element_type;

		// Token: 0x040010F2 RID: 4338
		private bool is_array;

		// Token: 0x040010F3 RID: 4339
		private bool is_list;
	}
}
