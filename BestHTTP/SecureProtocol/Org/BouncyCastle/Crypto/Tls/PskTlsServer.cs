using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200042A RID: 1066
	public class PskTlsServer : AbstractTlsServer
	{
		// Token: 0x06002A6F RID: 10863 RVA: 0x001150B9 File Offset: 0x001132B9
		public PskTlsServer(TlsPskIdentityManager pskIdentityManager) : this(new DefaultTlsCipherFactory(), pskIdentityManager)
		{
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x001150C7 File Offset: 0x001132C7
		public PskTlsServer(TlsCipherFactory cipherFactory, TlsPskIdentityManager pskIdentityManager) : base(cipherFactory)
		{
			this.mPskIdentityManager = pskIdentityManager;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		protected virtual TlsEncryptionCredentials GetRsaEncryptionCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x001110CD File Offset: 0x0010F2CD
		protected virtual DHParameters GetDHParameters()
		{
			return DHStandardGroups.rfc7919_ffdhe2048;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x001150D7 File Offset: 0x001132D7
		protected override int[] GetCipherSuites()
		{
			return new int[]
			{
				49207,
				49205,
				178,
				144
			};
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x001150EC File Offset: 0x001132EC
		public override TlsCredentials GetCredentials()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			if (keyExchangeAlgorithm - 13 > 1)
			{
				if (keyExchangeAlgorithm == 15)
				{
					return this.GetRsaEncryptionCredentials();
				}
				if (keyExchangeAlgorithm != 24)
				{
					throw new TlsFatalAlert(80);
				}
			}
			return null;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x00115128 File Offset: 0x00113328
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			if (keyExchangeAlgorithm - 13 <= 2 || keyExchangeAlgorithm == 24)
			{
				return this.CreatePskKeyExchange(keyExchangeAlgorithm);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0011515C File Offset: 0x0011335C
		protected virtual TlsKeyExchange CreatePskKeyExchange(int keyExchange)
		{
			return new TlsPskKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, null, this.mPskIdentityManager, null, this.GetDHParameters(), this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x04001C71 RID: 7281
		protected TlsPskIdentityManager mPskIdentityManager;
	}
}
