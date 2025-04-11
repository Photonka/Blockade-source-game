using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x0200060A RID: 1546
	public class SignerInformationStore
	{
		// Token: 0x06003AC7 RID: 15047 RVA: 0x0016F7E0 File Offset: 0x0016D9E0
		public SignerInformationStore(SignerInformation signerInfo)
		{
			this.all = Platform.CreateArrayList(1);
			this.all.Add(signerInfo);
			SignerID signerID = signerInfo.SignerID;
			this.table[signerID] = this.all;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x0016F830 File Offset: 0x0016DA30
		public SignerInformationStore(ICollection signerInfos)
		{
			foreach (object obj in signerInfos)
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				SignerID signerID = signerInformation.SignerID;
				IList list = (IList)this.table[signerID];
				if (list == null)
				{
					list = (this.table[signerID] = Platform.CreateArrayList(1));
				}
				list.Add(signerInformation);
			}
			this.all = Platform.CreateArrayList(signerInfos);
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x0016F8D8 File Offset: 0x0016DAD8
		public SignerInformation GetFirstSigner(SignerID selector)
		{
			IList list = (IList)this.table[selector];
			if (list != null)
			{
				return (SignerInformation)list[0];
			}
			return null;
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x0016F908 File Offset: 0x0016DB08
		public int Count
		{
			get
			{
				return this.all.Count;
			}
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x0016F915 File Offset: 0x0016DB15
		public ICollection GetSigners()
		{
			return Platform.CreateArrayList(this.all);
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x0016F924 File Offset: 0x0016DB24
		public ICollection GetSigners(SignerID selector)
		{
			IList list = (IList)this.table[selector];
			if (list != null)
			{
				return Platform.CreateArrayList(list);
			}
			return Platform.CreateArrayList();
		}

		// Token: 0x04002556 RID: 9558
		private readonly IList all;

		// Token: 0x04002557 RID: 9559
		private readonly IDictionary table = Platform.CreateHashtable();
	}
}
