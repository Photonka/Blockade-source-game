using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007AF RID: 1967
	public class PkiStatusEncodable : Asn1Encodable
	{
		// Token: 0x0600466B RID: 18027 RVA: 0x00195D75 File Offset: 0x00193F75
		private PkiStatusEncodable(PkiStatus status) : this(new DerInteger((int)status))
		{
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x00195D83 File Offset: 0x00193F83
		private PkiStatusEncodable(DerInteger status)
		{
			this.status = status;
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x00195D92 File Offset: 0x00193F92
		public static PkiStatusEncodable GetInstance(object obj)
		{
			if (obj is PkiStatusEncodable)
			{
				return (PkiStatusEncodable)obj;
			}
			if (obj is DerInteger)
			{
				return new PkiStatusEncodable((DerInteger)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x00195DD1 File Offset: 0x00193FD1
		public virtual BigInteger Value
		{
			get
			{
				return this.status.Value;
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x00195DDE File Offset: 0x00193FDE
		public override Asn1Object ToAsn1Object()
		{
			return this.status;
		}

		// Token: 0x04002D09 RID: 11529
		public static readonly PkiStatusEncodable granted = new PkiStatusEncodable(PkiStatus.Granted);

		// Token: 0x04002D0A RID: 11530
		public static readonly PkiStatusEncodable grantedWithMods = new PkiStatusEncodable(PkiStatus.GrantedWithMods);

		// Token: 0x04002D0B RID: 11531
		public static readonly PkiStatusEncodable rejection = new PkiStatusEncodable(PkiStatus.Rejection);

		// Token: 0x04002D0C RID: 11532
		public static readonly PkiStatusEncodable waiting = new PkiStatusEncodable(PkiStatus.Waiting);

		// Token: 0x04002D0D RID: 11533
		public static readonly PkiStatusEncodable revocationWarning = new PkiStatusEncodable(PkiStatus.RevocationWarning);

		// Token: 0x04002D0E RID: 11534
		public static readonly PkiStatusEncodable revocationNotification = new PkiStatusEncodable(PkiStatus.RevocationNotification);

		// Token: 0x04002D0F RID: 11535
		public static readonly PkiStatusEncodable keyUpdateWaiting = new PkiStatusEncodable(PkiStatus.KeyUpdateWarning);

		// Token: 0x04002D10 RID: 11536
		private readonly DerInteger status;
	}
}
