using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000289 RID: 649
	public abstract class UnmodifiableList : IList, ICollection, IEnumerable
	{
		// Token: 0x060017F4 RID: 6132 RVA: 0x00092231 File Offset: 0x00090431
		public virtual int Add(object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017F6 RID: 6134
		public abstract bool Contains(object o);

		// Token: 0x060017F7 RID: 6135
		public abstract void CopyTo(Array array, int index);

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060017F8 RID: 6136
		public abstract int Count { get; }

		// Token: 0x060017F9 RID: 6137
		public abstract IEnumerator GetEnumerator();

		// Token: 0x060017FA RID: 6138
		public abstract int IndexOf(object o);

		// Token: 0x060017FB RID: 6139 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Insert(int i, object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060017FC RID: 6140
		public abstract bool IsFixedSize { get; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060017FE RID: 6142
		public abstract bool IsSynchronized { get; }

		// Token: 0x060017FF RID: 6143 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void Remove(object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00092231 File Offset: 0x00090431
		public virtual void RemoveAt(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001801 RID: 6145
		public abstract object SyncRoot { get; }

		// Token: 0x17000301 RID: 769
		public virtual object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001804 RID: 6148
		protected abstract object GetValue(int i);
	}
}
