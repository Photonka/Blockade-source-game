using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000287 RID: 647
	public abstract class UnmodifiableDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060017D7 RID: 6103 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Add(object k, object v)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017D9 RID: 6105
		public abstract bool Contains(object k);

		// Token: 0x060017DA RID: 6106
		public abstract void CopyTo(Array array, int index);

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060017DB RID: 6107
		public abstract int Count { get; }

		// Token: 0x060017DC RID: 6108 RVA: 0x000BB5DE File Offset: 0x000B97DE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060017DD RID: 6109
		public abstract IDictionaryEnumerator GetEnumerator();

		// Token: 0x060017DE RID: 6110 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Remove(object k)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060017DF RID: 6111
		public abstract bool IsFixedSize { get; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060017E1 RID: 6113
		public abstract bool IsSynchronized { get; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060017E2 RID: 6114
		public abstract object SyncRoot { get; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060017E3 RID: 6115
		public abstract ICollection Keys { get; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060017E4 RID: 6116
		public abstract ICollection Values { get; }

		// Token: 0x170002F5 RID: 757
		public virtual object this[object k]
		{
			get
			{
				return this.GetValue(k);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060017E7 RID: 6119
		protected abstract object GetValue(object k);
	}
}
