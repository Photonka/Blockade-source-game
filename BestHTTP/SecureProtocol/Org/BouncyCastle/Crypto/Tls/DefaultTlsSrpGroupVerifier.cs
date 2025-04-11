using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Srp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000405 RID: 1029
	public class DefaultTlsSrpGroupVerifier : TlsSrpGroupVerifier
	{
		// Token: 0x0600299D RID: 10653 RVA: 0x00111400 File Offset: 0x0010F600
		static DefaultTlsSrpGroupVerifier()
		{
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_1024);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_1536);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_2048);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_3072);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_4096);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_6144);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_8192);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00111487 File Offset: 0x0010F687
		public DefaultTlsSrpGroupVerifier() : this(DefaultTlsSrpGroupVerifier.DefaultGroups)
		{
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x00111494 File Offset: 0x0010F694
		public DefaultTlsSrpGroupVerifier(IList groups)
		{
			this.mGroups = groups;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x001114A4 File Offset: 0x0010F6A4
		public virtual bool Accept(Srp6GroupParameters group)
		{
			foreach (object obj in this.mGroups)
			{
				Srp6GroupParameters b = (Srp6GroupParameters)obj;
				if (this.AreGroupsEqual(group, b))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00111508 File Offset: 0x0010F708
		protected virtual bool AreGroupsEqual(Srp6GroupParameters a, Srp6GroupParameters b)
		{
			return a == b || (this.AreParametersEqual(a.N, b.N) && this.AreParametersEqual(a.G, b.G));
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00110F78 File Offset: 0x0010F178
		protected virtual bool AreParametersEqual(BigInteger a, BigInteger b)
		{
			return a == b || a.Equals(b);
		}

		// Token: 0x04001B7C RID: 7036
		protected static readonly IList DefaultGroups = Platform.CreateArrayList();

		// Token: 0x04001B7D RID: 7037
		protected readonly IList mGroups;
	}
}
