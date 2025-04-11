using System;
using System.Threading;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003DD RID: 989
	internal abstract class AbstractTlsContext : TlsContext
	{
		// Token: 0x0600287D RID: 10365 RVA: 0x0010ECF0 File Offset: 0x0010CEF0
		private static long NextCounterValue()
		{
			return Interlocked.Increment(ref AbstractTlsContext.counter);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x0010ECFC File Offset: 0x0010CEFC
		internal AbstractTlsContext(SecureRandom secureRandom, SecurityParameters securityParameters)
		{
			IDigest digest = TlsUtilities.CreateHash(4);
			byte[] array = new byte[digest.GetDigestSize()];
			secureRandom.NextBytes(array);
			this.mNonceRandom = new DigestRandomGenerator(digest);
			this.mNonceRandom.AddSeedMaterial(AbstractTlsContext.NextCounterValue());
			this.mNonceRandom.AddSeedMaterial(Times.NanoTime());
			this.mNonceRandom.AddSeedMaterial(array);
			this.mSecureRandom = secureRandom;
			this.mSecurityParameters = securityParameters;
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x0010ED6F File Offset: 0x0010CF6F
		public virtual IRandomGenerator NonceRandomGenerator
		{
			get
			{
				return this.mNonceRandom;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x0010ED77 File Offset: 0x0010CF77
		public virtual SecureRandom SecureRandom
		{
			get
			{
				return this.mSecureRandom;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x0010ED7F File Offset: 0x0010CF7F
		public virtual SecurityParameters SecurityParameters
		{
			get
			{
				return this.mSecurityParameters;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06002882 RID: 10370
		public abstract bool IsServer { get; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x0010ED87 File Offset: 0x0010CF87
		public virtual ProtocolVersion ClientVersion
		{
			get
			{
				return this.mClientVersion;
			}
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0010ED8F File Offset: 0x0010CF8F
		internal virtual void SetClientVersion(ProtocolVersion clientVersion)
		{
			this.mClientVersion = clientVersion;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x0010ED98 File Offset: 0x0010CF98
		public virtual ProtocolVersion ServerVersion
		{
			get
			{
				return this.mServerVersion;
			}
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0010EDA0 File Offset: 0x0010CFA0
		internal virtual void SetServerVersion(ProtocolVersion serverVersion)
		{
			this.mServerVersion = serverVersion;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x0010EDA9 File Offset: 0x0010CFA9
		public virtual TlsSession ResumableSession
		{
			get
			{
				return this.mSession;
			}
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0010EDB1 File Offset: 0x0010CFB1
		internal virtual void SetResumableSession(TlsSession session)
		{
			this.mSession = session;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x0010EDBA File Offset: 0x0010CFBA
		// (set) Token: 0x0600288A RID: 10378 RVA: 0x0010EDC2 File Offset: 0x0010CFC2
		public virtual object UserObject
		{
			get
			{
				return this.mUserObject;
			}
			set
			{
				this.mUserObject = value;
			}
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0010EDCC File Offset: 0x0010CFCC
		public virtual byte[] ExportKeyingMaterial(string asciiLabel, byte[] context_value, int length)
		{
			if (context_value != null && !TlsUtilities.IsValidUint16(context_value.Length))
			{
				throw new ArgumentException("must have length less than 2^16 (or be null)", "context_value");
			}
			SecurityParameters securityParameters = this.SecurityParameters;
			if (!securityParameters.IsExtendedMasterSecret)
			{
				throw new InvalidOperationException("cannot export keying material without extended_master_secret");
			}
			byte[] clientRandom = securityParameters.ClientRandom;
			byte[] serverRandom = securityParameters.ServerRandom;
			int num = clientRandom.Length + serverRandom.Length;
			if (context_value != null)
			{
				num += 2 + context_value.Length;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			Array.Copy(clientRandom, 0, array, num2, clientRandom.Length);
			num2 += clientRandom.Length;
			Array.Copy(serverRandom, 0, array, num2, serverRandom.Length);
			num2 += serverRandom.Length;
			if (context_value != null)
			{
				TlsUtilities.WriteUint16(context_value.Length, array, num2);
				num2 += 2;
				Array.Copy(context_value, 0, array, num2, context_value.Length);
				num2 += context_value.Length;
			}
			if (num2 != num)
			{
				throw new InvalidOperationException("error in calculation of seed for export");
			}
			return TlsUtilities.PRF(this, securityParameters.MasterSecret, asciiLabel, array, length);
		}

		// Token: 0x040019DA RID: 6618
		private static long counter = Times.NanoTime();

		// Token: 0x040019DB RID: 6619
		private readonly IRandomGenerator mNonceRandom;

		// Token: 0x040019DC RID: 6620
		private readonly SecureRandom mSecureRandom;

		// Token: 0x040019DD RID: 6621
		private readonly SecurityParameters mSecurityParameters;

		// Token: 0x040019DE RID: 6622
		private ProtocolVersion mClientVersion;

		// Token: 0x040019DF RID: 6623
		private ProtocolVersion mServerVersion;

		// Token: 0x040019E0 RID: 6624
		private TlsSession mSession;

		// Token: 0x040019E1 RID: 6625
		private object mUserObject;
	}
}
