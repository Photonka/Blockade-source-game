using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000286 RID: 646
	internal class LinkedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x060017CE RID: 6094 RVA: 0x000BB4DC File Offset: 0x000B96DC
		internal LinkedDictionaryEnumerator(LinkedDictionary parent)
		{
			this.parent = parent;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x000BB4F2 File Offset: 0x000B96F2
		public virtual object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000BB500 File Offset: 0x000B9700
		public virtual DictionaryEntry Entry
		{
			get
			{
				object currentKey = this.CurrentKey;
				return new DictionaryEntry(currentKey, this.parent.hash[currentKey]);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x000BB52B File Offset: 0x000B972B
		public virtual object Key
		{
			get
			{
				return this.CurrentKey;
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000BB534 File Offset: 0x000B9734
		public virtual bool MoveNext()
		{
			if (this.pos >= this.parent.keys.Count)
			{
				return false;
			}
			int num = this.pos + 1;
			this.pos = num;
			return num < this.parent.keys.Count;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000BB57E File Offset: 0x000B977E
		public virtual void Reset()
		{
			this.pos = -1;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000BB587 File Offset: 0x000B9787
		public virtual object Value
		{
			get
			{
				return this.parent.hash[this.CurrentKey];
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x000BB59F File Offset: 0x000B979F
		private object CurrentKey
		{
			get
			{
				if (this.pos < 0 || this.pos >= this.parent.keys.Count)
				{
					throw new InvalidOperationException();
				}
				return this.parent.keys[this.pos];
			}
		}

		// Token: 0x040016F4 RID: 5876
		private readonly LinkedDictionary parent;

		// Token: 0x040016F5 RID: 5877
		private int pos = -1;
	}
}
