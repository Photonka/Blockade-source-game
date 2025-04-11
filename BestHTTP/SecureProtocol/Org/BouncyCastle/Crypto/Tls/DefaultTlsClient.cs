using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000400 RID: 1024
	public abstract class DefaultTlsClient : AbstractTlsClient
	{
		// Token: 0x06002971 RID: 10609 RVA: 0x00110D05 File Offset: 0x0010EF05
		public DefaultTlsClient() : this(new DefaultTlsCipherFactory())
		{
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x00110D12 File Offset: 0x0010EF12
		public DefaultTlsClient(TlsCipherFactory cipherFactory) : this(cipherFactory, new DefaultTlsDHVerifier())
		{
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x00110D20 File Offset: 0x0010EF20
		public DefaultTlsClient(TlsCipherFactory cipherFactory, TlsDHVerifier dhVerifier) : base(cipherFactory)
		{
			this.mDHVerifier = dhVerifier;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x00110D30 File Offset: 0x0010EF30
		public override int[] GetCipherSuites()
		{
			return new int[]
			{
				49195,
				49187,
				49161,
				49199,
				49191,
				49171,
				156,
				60,
				47
			};
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x00110D44 File Offset: 0x0010EF44
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			switch (keyExchangeAlgorithm)
			{
			case 1:
				return this.CreateRsaKeyExchange();
			case 3:
			case 5:
				return this.CreateDheKeyExchange(keyExchangeAlgorithm);
			case 7:
			case 9:
			case 11:
				return this.CreateDHKeyExchange(keyExchangeAlgorithm);
			case 16:
			case 18:
			case 20:
				return this.CreateECDHKeyExchange(keyExchangeAlgorithm);
			case 17:
			case 19:
				return this.CreateECDheKeyExchange(keyExchangeAlgorithm);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00110DE5 File Offset: 0x0010EFE5
		protected virtual TlsKeyExchange CreateDHKeyExchange(int keyExchange)
		{
			return new TlsDHKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mDHVerifier, null);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x00110DFA File Offset: 0x0010EFFA
		protected virtual TlsKeyExchange CreateDheKeyExchange(int keyExchange)
		{
			return new TlsDheKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mDHVerifier, null);
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x00110E0F File Offset: 0x0010F00F
		protected virtual TlsKeyExchange CreateECDHKeyExchange(int keyExchange)
		{
			return new TlsECDHKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x00110E2F File Offset: 0x0010F02F
		protected virtual TlsKeyExchange CreateECDheKeyExchange(int keyExchange)
		{
			return new TlsECDheKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x00110E4F File Offset: 0x0010F04F
		protected virtual TlsKeyExchange CreateRsaKeyExchange()
		{
			return new TlsRsaKeyExchange(this.mSupportedSignatureAlgorithms);
		}

		// Token: 0x04001B6F RID: 7023
		protected TlsDHVerifier mDHVerifier;
	}
}
