using System;

namespace PlatformSupport.Collections.Specialized
{
	// Token: 0x02000164 RID: 356
	public interface INotifyCollectionChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000C8C RID: 3212
		// (remove) Token: 0x06000C8D RID: 3213
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
