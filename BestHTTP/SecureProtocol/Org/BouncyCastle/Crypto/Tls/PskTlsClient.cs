using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000429 RID: 1065
	public class PskTlsClient : AbstractTlsClient
	{
		// Token: 0x06002A68 RID: 10856 RVA: 0x00114FFD File Offset: 0x001131FD
		public PskTlsClient(TlsPskIdentity pskIdentity) : this(new DefaultTlsCipherFactory(), pskIdentity)
		{
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x0011500B File Offset: 0x0011320B
		public PskTlsClient(TlsCipherFactory cipherFactory, TlsPskIdentity pskIdentity) : this(cipherFactory, new DefaultTlsDHVerifier(), pskIdentity)
		{
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x0011501A File Offset: 0x0011321A
		public PskTlsClient(TlsCipherFactory cipherFactory, TlsDHVerifier dhVerifier, TlsPskIdentity pskIdentity) : base(cipherFactory)
		{
			this.mDHVerifier = dhVerifier;
			this.mPskIdentity = pskIdentity;
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x00115031 File Offset: 0x00113231
		public override int[] GetCipherSuites()
		{
			return new int[]
			{
				49207,
				49205
			};
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x0011504C File Offset: 0x0011324C
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			if (keyExchangeAlgorithm - 13 <= 2 || keyExchangeAlgorithm == 24)
			{
				return this.CreatePskKeyExchange(keyExchangeAlgorithm);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		public override TlsAuthentication GetAuthentication()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x00115080 File Offset: 0x00113280
		protected virtual TlsKeyExchange CreatePskKeyExchange(int keyExchange)
		{
			return new TlsPskKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mPskIdentity, null, this.mDHVerifier, null, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x04001C6F RID: 7279
		protected TlsDHVerifier mDHVerifier;

		// Token: 0x04001C70 RID: 7280
		protected TlsPskIdentity mPskIdentity;
	}
}
