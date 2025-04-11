using System;
using System.Collections;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x02000243 RID: 579
	public class X509CollectionStoreParameters : IX509StoreParameters
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x000B0F14 File Offset: 0x000AF114
		public X509CollectionStoreParameters(ICollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.collection = Platform.CreateArrayList(collection);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000B0F36 File Offset: 0x000AF136
		public ICollection GetCollection()
		{
			return Platform.CreateArrayList(this.collection);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000B0F43 File Offset: 0x000AF143
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("X509CollectionStoreParameters: [\n");
			stringBuilder.Append("  collection: " + this.collection + "\n");
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400153B RID: 5435
		private readonly IList collection;
	}
}
