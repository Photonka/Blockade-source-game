using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000288 RID: 648
	public class UnmodifiableDictionaryProxy : UnmodifiableDictionary
	{
		// Token: 0x060017E8 RID: 6120 RVA: 0x000BB5EF File Offset: 0x000B97EF
		public UnmodifiableDictionaryProxy(IDictionary d)
		{
			this.d = d;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000BB5FE File Offset: 0x000B97FE
		public override bool Contains(object k)
		{
			return this.d.Contains(k);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000BB60C File Offset: 0x000B980C
		public override void CopyTo(Array array, int index)
		{
			this.d.CopyTo(array, index);
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x000BB61B File Offset: 0x000B981B
		public override int Count
		{
			get
			{
				return this.d.Count;
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000BB628 File Offset: 0x000B9828
		public override IDictionaryEnumerator GetEnumerator()
		{
			return this.d.GetEnumerator();
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x000BB635 File Offset: 0x000B9835
		public override bool IsFixedSize
		{
			get
			{
				return this.d.IsFixedSize;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x000BB642 File Offset: 0x000B9842
		public override bool IsSynchronized
		{
			get
			{
				return this.d.IsSynchronized;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x000BB64F File Offset: 0x000B984F
		public override object SyncRoot
		{
			get
			{
				return this.d.SyncRoot;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000BB65C File Offset: 0x000B985C
		public override ICollection Keys
		{
			get
			{
				return this.d.Keys;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000BB669 File Offset: 0x000B9869
		public override ICollection Values
		{
			get
			{
				return this.d.Values;
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000BB676 File Offset: 0x000B9876
		protected override object GetValue(object k)
		{
			return this.d[k];
		}

		// Token: 0x040016F6 RID: 5878
		private readonly IDictionary d;
	}
}
