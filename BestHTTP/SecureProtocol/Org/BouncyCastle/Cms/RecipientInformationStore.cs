using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x02000604 RID: 1540
	public class RecipientInformationStore
	{
		// Token: 0x06003A94 RID: 14996 RVA: 0x0016E7F4 File Offset: 0x0016C9F4
		public RecipientInformationStore(ICollection recipientInfos)
		{
			foreach (object obj in recipientInfos)
			{
				RecipientInformation recipientInformation = (RecipientInformation)obj;
				RecipientID recipientID = recipientInformation.RecipientID;
				IList list = (IList)this.table[recipientID];
				if (list == null)
				{
					list = (this.table[recipientID] = Platform.CreateArrayList(1));
				}
				list.Add(recipientInformation);
			}
			this.all = Platform.CreateArrayList(recipientInfos);
		}

		// Token: 0x1700079A RID: 1946
		public RecipientInformation this[RecipientID selector]
		{
			get
			{
				return this.GetFirstRecipient(selector);
			}
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x0016E8A8 File Offset: 0x0016CAA8
		public RecipientInformation GetFirstRecipient(RecipientID selector)
		{
			IList list = (IList)this.table[selector];
			if (list != null)
			{
				return (RecipientInformation)list[0];
			}
			return null;
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06003A97 RID: 14999 RVA: 0x0016E8D8 File Offset: 0x0016CAD8
		public int Count
		{
			get
			{
				return this.all.Count;
			}
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x0016E8E5 File Offset: 0x0016CAE5
		public ICollection GetRecipients()
		{
			return Platform.CreateArrayList(this.all);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x0016E8F4 File Offset: 0x0016CAF4
		public ICollection GetRecipients(RecipientID selector)
		{
			IList list = (IList)this.table[selector];
			if (list != null)
			{
				return Platform.CreateArrayList(list);
			}
			return Platform.CreateArrayList();
		}

		// Token: 0x0400253C RID: 9532
		private readonly IList all;

		// Token: 0x0400253D RID: 9533
		private readonly IDictionary table = Platform.CreateHashtable();
	}
}
