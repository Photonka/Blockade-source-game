using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000436 RID: 1078
	public class SrpTlsClient : AbstractTlsClient
	{
		// Token: 0x06002ACD RID: 10957 RVA: 0x00115E6B File Offset: 0x0011406B
		public SrpTlsClient(byte[] identity, byte[] password) : this(new DefaultTlsCipherFactory(), new DefaultTlsSrpGroupVerifier(), identity, password)
		{
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x00115E7F File Offset: 0x0011407F
		public SrpTlsClient(TlsCipherFactory cipherFactory, byte[] identity, byte[] password) : this(cipherFactory, new DefaultTlsSrpGroupVerifier(), identity, password)
		{
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x00115E8F File Offset: 0x0011408F
		public SrpTlsClient(TlsCipherFactory cipherFactory, TlsSrpGroupVerifier groupVerifier, byte[] identity, byte[] password) : base(cipherFactory)
		{
			this.mGroupVerifier = groupVerifier;
			this.mIdentity = Arrays.Clone(identity);
			this.mPassword = Arrays.Clone(password);
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		protected virtual bool RequireSrpServerExtension
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x00115EB8 File Offset: 0x001140B8
		public override int[] GetCipherSuites()
		{
			return new int[]
			{
				49182
			};
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x00115EC8 File Offset: 0x001140C8
		public override IDictionary GetClientExtensions()
		{
			IDictionary dictionary = TlsExtensionsUtilities.EnsureExtensionsInitialised(base.GetClientExtensions());
			TlsSrpUtilities.AddSrpExtension(dictionary, this.mIdentity);
			return dictionary;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x00115EE1 File Offset: 0x001140E1
		public override void ProcessServerExtensions(IDictionary serverExtensions)
		{
			if (!TlsUtilities.HasExpectedEmptyExtensionData(serverExtensions, 12, 47) && this.RequireSrpServerExtension)
			{
				throw new TlsFatalAlert(47);
			}
			base.ProcessServerExtensions(serverExtensions);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x00115F08 File Offset: 0x00114108
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			if (keyExchangeAlgorithm - 21 <= 2)
			{
				return this.CreateSrpKeyExchange(keyExchangeAlgorithm);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		public override TlsAuthentication GetAuthentication()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00115F37 File Offset: 0x00114137
		protected virtual TlsKeyExchange CreateSrpKeyExchange(int keyExchange)
		{
			return new TlsSrpKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mGroupVerifier, this.mIdentity, this.mPassword);
		}

		// Token: 0x04001CB4 RID: 7348
		protected TlsSrpGroupVerifier mGroupVerifier;

		// Token: 0x04001CB5 RID: 7349
		protected byte[] mIdentity;

		// Token: 0x04001CB6 RID: 7350
		protected byte[] mPassword;
	}
}
