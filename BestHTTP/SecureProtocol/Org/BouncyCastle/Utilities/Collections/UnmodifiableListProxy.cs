using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200028A RID: 650
	public class UnmodifiableListProxy : UnmodifiableList
	{
		// Token: 0x06001805 RID: 6149 RVA: 0x000BB68D File Offset: 0x000B988D
		public UnmodifiableListProxy(IList l)
		{
			this.l = l;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000BB69C File Offset: 0x000B989C
		public override bool Contains(object o)
		{
			return this.l.Contains(o);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000BB6AA File Offset: 0x000B98AA
		public override void CopyTo(Array array, int index)
		{
			this.l.CopyTo(array, index);
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x000BB6B9 File Offset: 0x000B98B9
		public override int Count
		{
			get
			{
				return this.l.Count;
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000BB6C6 File Offset: 0x000B98C6
		public override IEnumerator GetEnumerator()
		{
			return this.l.GetEnumerator();
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x000BB6D3 File Offset: 0x000B98D3
		public override int IndexOf(object o)
		{
			return this.l.IndexOf(o);
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000BB6E1 File Offset: 0x000B98E1
		public override bool IsFixedSize
		{
			get
			{
				return this.l.IsFixedSize;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000BB6EE File Offset: 0x000B98EE
		public override bool IsSynchronized
		{
			get
			{
				return this.l.IsSynchronized;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000BB6FB File Offset: 0x000B98FB
		public override object SyncRoot
		{
			get
			{
				return this.l.SyncRoot;
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000BB708 File Offset: 0x000B9908
		protected override object GetValue(int i)
		{
			return this.l[i];
		}

		// Token: 0x040016F7 RID: 5879
		private readonly IList l;
	}
}
