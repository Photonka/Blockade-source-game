using System;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001D4 RID: 468
	internal struct CallbackDescriptor
	{
		// Token: 0x0600117A RID: 4474 RVA: 0x000A4819 File Offset: 0x000A2A19
		public CallbackDescriptor(Type[] paramTypes, Action<object[]> callback)
		{
			this.ParamTypes = paramTypes;
			this.Callback = callback;
		}

		// Token: 0x040013D5 RID: 5077
		public readonly Type[] ParamTypes;

		// Token: 0x040013D6 RID: 5078
		public readonly Action<object[]> Callback;
	}
}
