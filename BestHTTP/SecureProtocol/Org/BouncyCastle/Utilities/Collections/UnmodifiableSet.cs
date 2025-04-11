using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200028B RID: 651
	public abstract class UnmodifiableSet : ISet, ICollection, IEnumerable
	{
		// Token: 0x06001810 RID: 6160 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Add(object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void AddAll(IEnumerable e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001813 RID: 6163
		public abstract bool Contains(object o);

		// Token: 0x06001814 RID: 6164
		public abstract void CopyTo(Array array, int index);

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001815 RID: 6165
		public abstract int Count { get; }

		// Token: 0x06001816 RID: 6166
		public abstract IEnumerator GetEnumerator();

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001817 RID: 6167
		public abstract bool IsEmpty { get; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001818 RID: 6168
		public abstract bool IsFixedSize { get; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600181A RID: 6170
		public abstract bool IsSynchronized { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600181B RID: 6171
		public abstract object SyncRoot { get; }

		// Token: 0x0600181C RID: 6172 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Remove(object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void RemoveAll(IEnumerable e)
		{
			throw new NotSupportedException();
		}
	}
}
