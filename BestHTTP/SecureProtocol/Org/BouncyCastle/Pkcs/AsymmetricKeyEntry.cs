using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs
{
	// Token: 0x020002B0 RID: 688
	public class AsymmetricKeyEntry : Pkcs12Entry
	{
		// Token: 0x060019B3 RID: 6579 RVA: 0x000C57B5 File Offset: 0x000C39B5
		public AsymmetricKeyEntry(AsymmetricKeyParameter key) : base(Platform.CreateHashtable())
		{
			this.key = key;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000C57C9 File Offset: 0x000C39C9
		[Obsolete]
		public AsymmetricKeyEntry(AsymmetricKeyParameter key, Hashtable attributes) : base(attributes)
		{
			this.key = key;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000C57C9 File Offset: 0x000C39C9
		public AsymmetricKeyEntry(AsymmetricKeyParameter key, IDictionary attributes) : base(attributes)
		{
			this.key = key;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x000C57D9 File Offset: 0x000C39D9
		public AsymmetricKeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000C57E4 File Offset: 0x000C39E4
		public override bool Equals(object obj)
		{
			AsymmetricKeyEntry asymmetricKeyEntry = obj as AsymmetricKeyEntry;
			return asymmetricKeyEntry != null && this.key.Equals(asymmetricKeyEntry.key);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000C580E File Offset: 0x000C3A0E
		public override int GetHashCode()
		{
			return ~this.key.GetHashCode();
		}

		// Token: 0x0400177A RID: 6010
		private readonly AsymmetricKeyParameter key;
	}
}
