using System;

namespace LitJson
{
	// Token: 0x02000156 RID: 342
	public enum JsonToken
	{
		// Token: 0x04001108 RID: 4360
		None,
		// Token: 0x04001109 RID: 4361
		ObjectStart,
		// Token: 0x0400110A RID: 4362
		PropertyName,
		// Token: 0x0400110B RID: 4363
		ObjectEnd,
		// Token: 0x0400110C RID: 4364
		ArrayStart,
		// Token: 0x0400110D RID: 4365
		ArrayEnd,
		// Token: 0x0400110E RID: 4366
		Int,
		// Token: 0x0400110F RID: 4367
		Long,
		// Token: 0x04001110 RID: 4368
		Double,
		// Token: 0x04001111 RID: 4369
		String,
		// Token: 0x04001112 RID: 4370
		Boolean,
		// Token: 0x04001113 RID: 4371
		Null
	}
}
