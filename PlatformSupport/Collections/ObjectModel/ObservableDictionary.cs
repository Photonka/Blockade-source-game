using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PlatformSupport.Collections.Specialized;

namespace PlatformSupport.Collections.ObjectModel
{
	// Token: 0x02000168 RID: 360
	public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0009227D File Offset: 0x0009047D
		protected IDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this._Dictionary;
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00092285 File Offset: 0x00090485
		public ObservableDictionary()
		{
			this._Dictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00092298 File Offset: 0x00090498
		public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this._Dictionary = new Dictionary<TKey, TValue>(dictionary);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000922AC File Offset: 0x000904AC
		public ObservableDictionary(IEqualityComparer<TKey> comparer)
		{
			this._Dictionary = new Dictionary<TKey, TValue>(comparer);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000922C0 File Offset: 0x000904C0
		public ObservableDictionary(int capacity)
		{
			this._Dictionary = new Dictionary<TKey, TValue>(capacity);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000922D4 File Offset: 0x000904D4
		public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
		{
			this._Dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000922E9 File Offset: 0x000904E9
		public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			this._Dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000922FE File Offset: 0x000904FE
		public void Add(TKey key, TValue value)
		{
			this.Insert(key, value, true);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00092309 File Offset: 0x00090509
		public bool ContainsKey(TKey key)
		{
			return this.Dictionary.ContainsKey(key);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00092317 File Offset: 0x00090517
		public ICollection<TKey> Keys
		{
			get
			{
				return this.Dictionary.Keys;
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00092324 File Offset: 0x00090524
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			this.Dictionary.TryGetValue(key, out tvalue);
			bool flag = this.Dictionary.Remove(key);
			if (flag)
			{
				this.OnCollectionChanged();
			}
			return flag;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00092368 File Offset: 0x00090568
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.Dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00092377 File Offset: 0x00090577
		public ICollection<TValue> Values
		{
			get
			{
				return this.Dictionary.Values;
			}
		}

		// Token: 0x170000D1 RID: 209
		public TValue this[TKey key]
		{
			get
			{
				return this.Dictionary[key];
			}
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0009239D File Offset: 0x0009059D
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Insert(item.Key, item.Value, true);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000923B4 File Offset: 0x000905B4
		public void Clear()
		{
			if (this.Dictionary.Count > 0)
			{
				this.Dictionary.Clear();
				this.OnCollectionChanged();
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000923D5 File Offset: 0x000905D5
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.Dictionary.Contains(item);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000923E3 File Offset: 0x000905E3
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.Dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x000923F2 File Offset: 0x000905F2
		public int Count
		{
			get
			{
				return this.Dictionary.Count;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x000923FF File Offset: 0x000905FF
		public bool IsReadOnly
		{
			get
			{
				return this.Dictionary.IsReadOnly;
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0009240C File Offset: 0x0009060C
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return this.Remove(item.Key);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0009241B File Offset: 0x0009061B
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00092428 File Offset: 0x00090628
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000CCC RID: 3276 RVA: 0x00092438 File Offset: 0x00090638
		// (remove) Token: 0x06000CCD RID: 3277 RVA: 0x00092470 File Offset: 0x00090670
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000CCE RID: 3278 RVA: 0x000924A8 File Offset: 0x000906A8
		// (remove) Token: 0x06000CCF RID: 3279 RVA: 0x000924E0 File Offset: 0x000906E0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00092518 File Offset: 0x00090718
		public void AddRange(IDictionary<TKey, TValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (items.Count > 0)
			{
				if (this.Dictionary.Count > 0)
				{
					if (items.Keys.Any((TKey k) => this.Dictionary.ContainsKey(k)))
					{
						throw new ArgumentException("An item with the same key has already been added.");
					}
					using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<TKey, TValue> item = enumerator.Current;
							this.Dictionary.Add(item);
						}
						goto IL_85;
					}
				}
				this._Dictionary = new Dictionary<TKey, TValue>(items);
				IL_85:
				this.OnCollectionChanged(NotifyCollectionChangedAction.Add, items.ToArray<KeyValuePair<TKey, TValue>>());
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000925C8 File Offset: 0x000907C8
		private void Insert(TKey key, TValue value, bool add)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			if (!this.Dictionary.TryGetValue(key, out tvalue))
			{
				this.Dictionary[key] = value;
				this.OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
				return;
			}
			if (add)
			{
				throw new ArgumentException("An item with the same key has already been added.");
			}
			if (object.Equals(tvalue, value))
			{
				return;
			}
			this.Dictionary[key] = value;
			this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, value), new KeyValuePair<TKey, TValue>(key, tvalue));
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00092658 File Offset: 0x00090858
		private void OnPropertyChanged()
		{
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnPropertyChanged("Keys");
			this.OnPropertyChanged("Values");
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00092686 File Offset: 0x00090886
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000926A2 File Offset: 0x000908A2
		private void OnCollectionChanged()
		{
			this.OnPropertyChanged();
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000926C4 File Offset: 0x000908C4
		private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem)
		{
			this.OnPropertyChanged();
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItem));
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000926EC File Offset: 0x000908EC
		private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> newItem, KeyValuePair<TKey, TValue> oldItem)
		{
			this.OnPropertyChanged();
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0009271A File Offset: 0x0009091A
		private void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
		{
			this.OnPropertyChanged();
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, newItems));
			}
		}

		// Token: 0x04001171 RID: 4465
		private const string CountString = "Count";

		// Token: 0x04001172 RID: 4466
		private const string IndexerName = "Item[]";

		// Token: 0x04001173 RID: 4467
		private const string KeysName = "Keys";

		// Token: 0x04001174 RID: 4468
		private const string ValuesName = "Values";

		// Token: 0x04001175 RID: 4469
		private IDictionary<TKey, TValue> _Dictionary;
	}
}
