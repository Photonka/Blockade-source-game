using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x02000242 RID: 578
	internal class X509CollectionStore : IX509Store
	{
		// Token: 0x06001581 RID: 5505 RVA: 0x000B0E8B File Offset: 0x000AF08B
		internal X509CollectionStore(ICollection collection)
		{
			this._local = Platform.CreateArrayList(collection);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000B0EA0 File Offset: 0x000AF0A0
		public ICollection GetMatches(IX509Selector selector)
		{
			if (selector == null)
			{
				return Platform.CreateArrayList(this._local);
			}
			IList list = Platform.CreateArrayList();
			foreach (object obj in this._local)
			{
				if (selector.Match(obj))
				{
					list.Add(obj);
				}
			}
			return list;
		}

		// Token: 0x0400153A RID: 5434
		private ICollection _local;
	}
}
