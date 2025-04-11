using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200028C RID: 652
	public class UnmodifiableSetProxy : UnmodifiableSet
	{
		// Token: 0x0600181E RID: 6174 RVA: 0x000BB716 File Offset: 0x000B9916
		public UnmodifiableSetProxy(ISet s)
		{
			this.s = s;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000BB725 File Offset: 0x000B9925
		public override bool Contains(object o)
		{
			return this.s.Contains(o);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000BB733 File Offset: 0x000B9933
		public override void CopyTo(Array array, int index)
		{
			this.s.CopyTo(array, index);
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x000BB742 File Offset: 0x000B9942
		public override int Count
		{
			get
			{
				return this.s.Count;
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000BB74F File Offset: 0x000B994F
		public override IEnumerator GetEnumerator()
		{
			return this.s.GetEnumerator();
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x000BB75C File Offset: 0x000B995C
		public override bool IsEmpty
		{
			get
			{
				return this.s.IsEmpty;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x000BB769 File Offset: 0x000B9969
		public override bool IsFixedSize
		{
			get
			{
				return this.s.IsFixedSize;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x000BB776 File Offset: 0x000B9976
		public override bool IsSynchronized
		{
			get
			{
				return this.s.IsSynchronized;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x000BB783 File Offset: 0x000B9983
		public override object SyncRoot
		{
			get
			{
				return this.s.SyncRoot;
			}
		}

		// Token: 0x040016F8 RID: 5880
		private readonly ISet s;
	}
}
