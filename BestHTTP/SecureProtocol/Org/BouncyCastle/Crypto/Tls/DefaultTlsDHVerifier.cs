using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000401 RID: 1025
	public class DefaultTlsDHVerifier : TlsDHVerifier
	{
		// Token: 0x0600297B RID: 10619 RVA: 0x00110E5C File Offset: 0x0010F05C
		private static void AddDefaultGroup(DHParameters dhParameters)
		{
			DefaultTlsDHVerifier.DefaultGroups.Add(dhParameters);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x00110E6C File Offset: 0x0010F06C
		static DefaultTlsDHVerifier()
		{
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe2048);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe3072);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe4096);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe6144);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe8192);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_1536);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_2048);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_3072);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_4096);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_6144);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_8192);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x00110EFB File Offset: 0x0010F0FB
		public DefaultTlsDHVerifier() : this(DefaultTlsDHVerifier.DefaultMinimumPrimeBits)
		{
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x00110F08 File Offset: 0x0010F108
		public DefaultTlsDHVerifier(int minimumPrimeBits) : this(DefaultTlsDHVerifier.DefaultGroups, minimumPrimeBits)
		{
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x00110F16 File Offset: 0x0010F116
		public DefaultTlsDHVerifier(IList groups, int minimumPrimeBits)
		{
			this.mGroups = groups;
			this.mMinimumPrimeBits = minimumPrimeBits;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x00110F2C File Offset: 0x0010F12C
		public virtual bool Accept(DHParameters dhParameters)
		{
			return this.CheckMinimumPrimeBits(dhParameters) && this.CheckGroup(dhParameters);
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x00110F40 File Offset: 0x0010F140
		public virtual int MinimumPrimeBits
		{
			get
			{
				return this.mMinimumPrimeBits;
			}
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x00110F48 File Offset: 0x0010F148
		protected virtual bool AreGroupsEqual(DHParameters a, DHParameters b)
		{
			return a == b || (this.AreParametersEqual(a.P, b.P) && this.AreParametersEqual(a.G, b.G));
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x00110F78 File Offset: 0x0010F178
		protected virtual bool AreParametersEqual(BigInteger a, BigInteger b)
		{
			return a == b || a.Equals(b);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x00110F88 File Offset: 0x0010F188
		protected virtual bool CheckGroup(DHParameters dhParameters)
		{
			foreach (object obj in this.mGroups)
			{
				DHParameters b = (DHParameters)obj;
				if (this.AreGroupsEqual(dhParameters, b))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00110FEC File Offset: 0x0010F1EC
		protected virtual bool CheckMinimumPrimeBits(DHParameters dhParameters)
		{
			return dhParameters.P.BitLength >= this.MinimumPrimeBits;
		}

		// Token: 0x04001B70 RID: 7024
		public static readonly int DefaultMinimumPrimeBits = 2048;

		// Token: 0x04001B71 RID: 7025
		protected static readonly IList DefaultGroups = Platform.CreateArrayList();

		// Token: 0x04001B72 RID: 7026
		protected readonly IList mGroups;

		// Token: 0x04001B73 RID: 7027
		protected readonly int mMinimumPrimeBits;
	}
}
