using System;

namespace BestHTTP.Authentication
{
	// Token: 0x02000803 RID: 2051
	public sealed class Credentials
	{
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x001A5874 File Offset: 0x001A3A74
		// (set) Token: 0x0600497B RID: 18811 RVA: 0x001A587C File Offset: 0x001A3A7C
		public AuthenticationTypes Type { get; private set; }

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x001A5885 File Offset: 0x001A3A85
		// (set) Token: 0x0600497D RID: 18813 RVA: 0x001A588D File Offset: 0x001A3A8D
		public string UserName { get; private set; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600497E RID: 18814 RVA: 0x001A5896 File Offset: 0x001A3A96
		// (set) Token: 0x0600497F RID: 18815 RVA: 0x001A589E File Offset: 0x001A3A9E
		public string Password { get; private set; }

		// Token: 0x06004980 RID: 18816 RVA: 0x001A58A7 File Offset: 0x001A3AA7
		public Credentials(string userName, string password) : this(AuthenticationTypes.Unknown, userName, password)
		{
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x001A58B2 File Offset: 0x001A3AB2
		public Credentials(AuthenticationTypes type, string userName, string password)
		{
			this.Type = type;
			this.UserName = userName;
			this.Password = password;
		}
	}
}
