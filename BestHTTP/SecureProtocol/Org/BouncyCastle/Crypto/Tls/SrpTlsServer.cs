using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000437 RID: 1079
	public class SrpTlsServer : AbstractTlsServer
	{
		// Token: 0x06002AD7 RID: 10967 RVA: 0x00115F57 File Offset: 0x00114157
		public SrpTlsServer(TlsSrpIdentityManager srpIdentityManager) : this(new DefaultTlsCipherFactory(), srpIdentityManager)
		{
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00115F65 File Offset: 0x00114165
		public SrpTlsServer(TlsCipherFactory cipherFactory, TlsSrpIdentityManager srpIdentityManager) : base(cipherFactory)
		{
			this.mSrpIdentityManager = srpIdentityManager;
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		protected virtual TlsSignerCredentials GetDsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		protected virtual TlsSignerCredentials GetRsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x00115F75 File Offset: 0x00114175
		protected override int[] GetCipherSuites()
		{
			return new int[]
			{
				49186,
				49183,
				49185,
				49182,
				49184,
				49181
			};
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x00115F88 File Offset: 0x00114188
		public override void ProcessClientExtensions(IDictionary clientExtensions)
		{
			base.ProcessClientExtensions(clientExtensions);
			this.mSrpIdentity = TlsSrpUtilities.GetSrpExtension(clientExtensions);
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x00115F9D File Offset: 0x0011419D
		public override int GetSelectedCipherSuite()
		{
			int selectedCipherSuite = base.GetSelectedCipherSuite();
			if (TlsSrpUtilities.IsSrpCipherSuite(selectedCipherSuite))
			{
				if (this.mSrpIdentity != null)
				{
					this.mLoginParameters = this.mSrpIdentityManager.GetLoginParameters(this.mSrpIdentity);
				}
				if (this.mLoginParameters == null)
				{
					throw new TlsFatalAlert(115);
				}
			}
			return selectedCipherSuite;
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x00115FDC File Offset: 0x001141DC
		public override TlsCredentials GetCredentials()
		{
			switch (TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite))
			{
			case 21:
				return null;
			case 22:
				return this.GetDsaSignerCredentials();
			case 23:
				return this.GetRsaSignerCredentials();
			default:
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x00116024 File Offset: 0x00114224
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			if (keyExchangeAlgorithm - 21 <= 2)
			{
				return this.CreateSrpKeyExchange(keyExchangeAlgorithm);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x00116053 File Offset: 0x00114253
		protected virtual TlsKeyExchange CreateSrpKeyExchange(int keyExchange)
		{
			return new TlsSrpKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mSrpIdentity, this.mLoginParameters);
		}

		// Token: 0x04001CB7 RID: 7351
		protected TlsSrpIdentityManager mSrpIdentityManager;

		// Token: 0x04001CB8 RID: 7352
		protected byte[] mSrpIdentity;

		// Token: 0x04001CB9 RID: 7353
		protected TlsSrpLoginParameters mLoginParameters;
	}
}
