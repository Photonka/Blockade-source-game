using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x02000284 RID: 644
	public interface ISet : ICollection, IEnumerable
	{
		// Token: 0x060017B4 RID: 6068
		void Add(object o);

		// Token: 0x060017B5 RID: 6069
		void AddAll(IEnumerable e);

		// Token: 0x060017B6 RID: 6070
		void Clear();

		// Token: 0x060017B7 RID: 6071
		bool Contains(object o);

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060017B8 RID: 6072
		bool IsEmpty { get; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060017B9 RID: 6073
		bool IsFixedSize { get; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060017BA RID: 6074
		bool IsReadOnly { get; }

		// Token: 0x060017BB RID: 6075
		void Remove(object o);

		// Token: 0x060017BC RID: 6076
		void RemoveAll(IEnumerable e);
	}
}
