using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200046D RID: 1133
	internal class TlsSessionImpl : TlsSession
	{
		// Token: 0x06002CB7 RID: 11447 RVA: 0x0011CB1C File Offset: 0x0011AD1C
		internal TlsSessionImpl(byte[] sessionID, SessionParameters sessionParameters)
		{
			if (sessionID == null)
			{
				throw new ArgumentNullException("sessionID");
			}
			if (sessionID.Length > 32)
			{
				throw new ArgumentException("cannot be longer than 32 bytes", "sessionID");
			}
			this.mSessionID = Arrays.Clone(sessionID);
			this.mSessionParameters = sessionParameters;
			this.mResumable = (sessionID.Length != 0 && sessionParameters != null && sessionParameters.IsExtendedMasterSecret);
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x0011CB80 File Offset: 0x0011AD80
		public virtual SessionParameters ExportSessionParameters()
		{
			SessionParameters result;
			lock (this)
			{
				result = ((this.mSessionParameters == null) ? null : this.mSessionParameters.Copy());
			}
			return result;
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x0011CBD0 File Offset: 0x0011ADD0
		public virtual byte[] SessionID
		{
			get
			{
				byte[] result;
				lock (this)
				{
					result = this.mSessionID;
				}
				return result;
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x0011CC10 File Offset: 0x0011AE10
		public virtual void Invalidate()
		{
			lock (this)
			{
				this.mResumable = false;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x0011CC4C File Offset: 0x0011AE4C
		public virtual bool IsResumable
		{
			get
			{
				bool result;
				lock (this)
				{
					result = this.mResumable;
				}
				return result;
			}
		}

		// Token: 0x04001D5F RID: 7519
		internal readonly byte[] mSessionID;

		// Token: 0x04001D60 RID: 7520
		internal readonly SessionParameters mSessionParameters;

		// Token: 0x04001D61 RID: 7521
		internal bool mResumable;
	}
}
