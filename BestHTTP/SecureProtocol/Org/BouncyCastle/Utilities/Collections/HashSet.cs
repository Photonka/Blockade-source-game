using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000283 RID: 643
	public class HashSet : ISet, ICollection, IEnumerable
	{
		// Token: 0x060017A4 RID: 6052 RVA: 0x000BB155 File Offset: 0x000B9355
		public HashSet()
		{
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x000BB168 File Offset: 0x000B9368
		public HashSet(IEnumerable s)
		{
			foreach (object o in s)
			{
				this.Add(o);
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000BB1C8 File Offset: 0x000B93C8
		public virtual void Add(object o)
		{
			this.impl[o] = null;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000BB1D8 File Offset: 0x000B93D8
		public virtual void AddAll(IEnumerable e)
		{
			foreach (object o in e)
			{
				this.Add(o);
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x000BB228 File Offset: 0x000B9428
		public virtual void Clear()
		{
			this.impl.Clear();
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000BB235 File Offset: 0x000B9435
		public virtual bool Contains(object o)
		{
			return this.impl.Contains(o);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000BB243 File Offset: 0x000B9443
		public virtual void CopyTo(Array array, int index)
		{
			this.impl.Keys.CopyTo(array, index);
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x000BB257 File Offset: 0x000B9457
		public virtual int Count
		{
			get
			{
				return this.impl.Count;
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000BB264 File Offset: 0x000B9464
		public virtual IEnumerator GetEnumerator()
		{
			return this.impl.Keys.GetEnumerator();
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x000BB276 File Offset: 0x000B9476
		public virtual bool IsEmpty
		{
			get
			{
				return this.impl.Count == 0;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000BB286 File Offset: 0x000B9486
		public virtual bool IsFixedSize
		{
			get
			{
				return this.impl.IsFixedSize;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x000BB293 File Offset: 0x000B9493
		public virtual bool IsReadOnly
		{
			get
			{
				return this.impl.IsReadOnly;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000BB2A0 File Offset: 0x000B94A0
		public virtual bool IsSynchronized
		{
			get
			{
				return this.impl.IsSynchronized;
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000BB2AD File Offset: 0x000B94AD
		public virtual void Remove(object o)
		{
			this.impl.Remove(o);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000BB2BC File Offset: 0x000B94BC
		public virtual void RemoveAll(IEnumerable e)
		{
			foreach (object o in e)
			{
				this.Remove(o);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000BB30C File Offset: 0x000B950C
		public virtual object SyncRoot
		{
			get
			{
				return this.impl.SyncRoot;
			}
		}

		// Token: 0x040016F1 RID: 5873
		private readonly IDictionary impl = Platform.CreateHashtable();
	}
}
