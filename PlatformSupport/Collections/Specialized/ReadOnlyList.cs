using System;
using System.Collections;

namespace PlatformSupport.Collections.Specialized
{
	// Token: 0x02000167 RID: 359
	internal sealed class ReadOnlyList : IList, ICollection, IEnumerable
	{
		// Token: 0x06000CA3 RID: 3235 RVA: 0x000921FA File Offset: 0x000903FA
		internal ReadOnlyList(IList list)
		{
			this._list = list;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00092209 File Offset: 0x00090409
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00092216 File Offset: 0x00090416
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x170000CC RID: 204
		public object this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00092238 File Offset: 0x00090438
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00092231 File Offset: 0x00090431
		public int Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00092231 File Offset: 0x00090431
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00092245 File Offset: 0x00090445
		public bool Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00092253 File Offset: 0x00090453
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00092262 File Offset: 0x00090462
		public IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0009226F File Offset: 0x0009046F
		public int IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00092231 File Offset: 0x00090431
		public void Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00092231 File Offset: 0x00090431
		public void Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00092231 File Offset: 0x00090431
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001170 RID: 4464
		private readonly IList _list;
	}
}
