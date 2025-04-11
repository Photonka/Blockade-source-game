using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000285 RID: 645
	public class LinkedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060017BE RID: 6078 RVA: 0x000BB337 File Offset: 0x000B9537
		public virtual void Add(object k, object v)
		{
			this.hash.Add(k, v);
			this.keys.Add(k);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000BB353 File Offset: 0x000B9553
		public virtual void Clear()
		{
			this.hash.Clear();
			this.keys.Clear();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000BB36B File Offset: 0x000B956B
		public virtual bool Contains(object k)
		{
			return this.hash.Contains(k);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000BB37C File Offset: 0x000B957C
		public virtual void CopyTo(Array array, int index)
		{
			foreach (object key in this.keys)
			{
				array.SetValue(this.hash[key], index++);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x000BB3E4 File Offset: 0x000B95E4
		public virtual int Count
		{
			get
			{
				return this.hash.Count;
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000BB3F1 File Offset: 0x000B95F1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x000BB3F9 File Offset: 0x000B95F9
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new LinkedDictionaryEnumerator(this);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000BB401 File Offset: 0x000B9601
		public virtual void Remove(object k)
		{
			this.hash.Remove(k);
			this.keys.Remove(k);
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x000BB41B File Offset: 0x000B961B
		public virtual object SyncRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000BB423 File Offset: 0x000B9623
		public virtual ICollection Keys
		{
			get
			{
				return Platform.CreateArrayList(this.keys);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000BB430 File Offset: 0x000B9630
		public virtual ICollection Values
		{
			get
			{
				IList list = Platform.CreateArrayList(this.keys.Count);
				foreach (object key in this.keys)
				{
					list.Add(this.hash[key]);
				}
				return list;
			}
		}

		// Token: 0x170002E8 RID: 744
		public virtual object this[object k]
		{
			get
			{
				return this.hash[k];
			}
			set
			{
				if (!this.hash.Contains(k))
				{
					this.keys.Add(k);
				}
				this.hash[k] = value;
			}
		}

		// Token: 0x040016F2 RID: 5874
		internal readonly IDictionary hash = Platform.CreateHashtable();

		// Token: 0x040016F3 RID: 5875
		internal readonly IList keys = Platform.CreateArrayList();
	}
}
