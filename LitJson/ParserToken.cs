using System;

namespace LitJson
{
	// Token: 0x0200015D RID: 349
	internal enum ParserToken
	{
		// Token: 0x0400114C RID: 4428
		None = 65536,
		// Token: 0x0400114D RID: 4429
		Number,
		// Token: 0x0400114E RID: 4430
		True,
		// Token: 0x0400114F RID: 4431
		False,
		// Token: 0x04001150 RID: 4432
		Null,
		// Token: 0x04001151 RID: 4433
		CharSeq,
		// Token: 0x04001152 RID: 4434
		Char,
		// Token: 0x04001153 RID: 4435
		Text,
		// Token: 0x04001154 RID: 4436
		Object,
		// Token: 0x04001155 RID: 4437
		ObjectPrime,
		// Token: 0x04001156 RID: 4438
		Pair,
		// Token: 0x04001157 RID: 4439
		PairRest,
		// Token: 0x04001158 RID: 4440
		Array,
		// Token: 0x04001159 RID: 4441
		ArrayPrime,
		// Token: 0x0400115A RID: 4442
		Value,
		// Token: 0x0400115B RID: 4443
		ValueRest,
		// Token: 0x0400115C RID: 4444
		String,
		// Token: 0x0400115D RID: 4445
		End,
		// Token: 0x0400115E RID: 4446
		Epsilon
	}
}
