using System;
using System.Collections.Generic;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001D3 RID: 467
	public class StreamItemContainer<T>
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x000A47B5 File Offset: 0x000A29B5
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x000A47BD File Offset: 0x000A29BD
		public List<T> Items { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x000A47C6 File Offset: 0x000A29C6
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x000A47CE File Offset: 0x000A29CE
		public T LastAdded { get; private set; }

		// Token: 0x06001178 RID: 4472 RVA: 0x000A47D7 File Offset: 0x000A29D7
		public StreamItemContainer(long _id)
		{
			this.id = _id;
			this.Items = new List<T>();
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000A47F1 File Offset: 0x000A29F1
		public void AddItem(T item)
		{
			if (this.Items == null)
			{
				this.Items = new List<T>();
			}
			this.Items.Add(item);
			this.LastAdded = item;
		}

		// Token: 0x040013D1 RID: 5073
		public readonly long id;

		// Token: 0x040013D4 RID: 5076
		public bool IsCanceled;
	}
}
