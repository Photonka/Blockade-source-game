using System;
using System.Collections.Generic;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001D5 RID: 469
	internal sealed class Subscription
	{
		// Token: 0x0600117B RID: 4475 RVA: 0x000A482C File Offset: 0x000A2A2C
		public void Add(Type[] paramTypes, Action<object[]> callback)
		{
			List<CallbackDescriptor> obj = this.callbacks;
			lock (obj)
			{
				this.callbacks.Add(new CallbackDescriptor(paramTypes, callback));
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000A4878 File Offset: 0x000A2A78
		public void Remove(Action<object[]> callback)
		{
			List<CallbackDescriptor> obj = this.callbacks;
			lock (obj)
			{
				int num = -1;
				int num2 = 0;
				while (num2 < this.callbacks.Count && num == -1)
				{
					if (this.callbacks[num2].Callback == callback)
					{
						num = num2;
					}
					num2++;
				}
				if (num != -1)
				{
					this.callbacks.RemoveAt(num);
				}
			}
		}

		// Token: 0x040013D7 RID: 5079
		public List<CallbackDescriptor> callbacks = new List<CallbackDescriptor>(1);
	}
}
