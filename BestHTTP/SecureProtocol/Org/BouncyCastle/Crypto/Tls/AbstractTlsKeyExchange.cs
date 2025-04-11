using System;
using System.Collections;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E0 RID: 992
	public abstract class AbstractTlsKeyExchange : TlsKeyExchange
	{
		// Token: 0x06002891 RID: 10385 RVA: 0x0010EEBF File Offset: 0x0010D0BF
		protected AbstractTlsKeyExchange(int keyExchange, IList supportedSignatureAlgorithms)
		{
			this.mKeyExchange = keyExchange;
			this.mSupportedSignatureAlgorithms = supportedSignatureAlgorithms;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x0010EED8 File Offset: 0x0010D0D8
		protected virtual DigitallySigned ParseSignature(Stream input)
		{
			DigitallySigned digitallySigned = DigitallySigned.Parse(this.mContext, input);
			SignatureAndHashAlgorithm algorithm = digitallySigned.Algorithm;
			if (algorithm != null)
			{
				TlsUtilities.VerifySupportedSignatureAlgorithm(this.mSupportedSignatureAlgorithms, algorithm);
			}
			return digitallySigned;
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x0010EF08 File Offset: 0x0010D108
		public virtual void Init(TlsContext context)
		{
			this.mContext = context;
			ProtocolVersion clientVersion = context.ClientVersion;
			if (TlsUtilities.IsSignatureAlgorithmsExtensionAllowed(clientVersion))
			{
				if (this.mSupportedSignatureAlgorithms == null)
				{
					switch (this.mKeyExchange)
					{
					case 1:
					case 5:
					case 9:
					case 15:
					case 18:
					case 19:
					case 23:
						this.mSupportedSignatureAlgorithms = TlsUtilities.GetDefaultRsaSignatureAlgorithms();
						return;
					case 3:
					case 7:
					case 22:
						this.mSupportedSignatureAlgorithms = TlsUtilities.GetDefaultDssSignatureAlgorithms();
						return;
					case 13:
					case 14:
					case 21:
					case 24:
						return;
					case 16:
					case 17:
						this.mSupportedSignatureAlgorithms = TlsUtilities.GetDefaultECDsaSignatureAlgorithms();
						return;
					}
					throw new InvalidOperationException("unsupported key exchange algorithm");
				}
			}
			else if (this.mSupportedSignatureAlgorithms != null)
			{
				throw new InvalidOperationException("supported_signature_algorithms not allowed for " + clientVersion);
			}
		}

		// Token: 0x06002894 RID: 10388
		public abstract void SkipServerCredentials();

		// Token: 0x06002895 RID: 10389 RVA: 0x0010EFF2 File Offset: 0x0010D1F2
		public virtual void ProcessServerCertificate(Certificate serverCertificate)
		{
			IList list = this.mSupportedSignatureAlgorithms;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0010EFFB File Offset: 0x0010D1FB
		public virtual void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			this.ProcessServerCertificate(serverCredentials.Certificate);
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool RequiresServerKeyExchange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0010F009 File Offset: 0x0010D209
		public virtual byte[] GenerateServerKeyExchange()
		{
			if (this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(80);
			}
			return null;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0010F01C File Offset: 0x0010D21C
		public virtual void SkipServerKeyExchange()
		{
			if (this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0010F02E File Offset: 0x0010D22E
		public virtual void ProcessServerKeyExchange(Stream input)
		{
			if (!this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x0600289B RID: 10395
		public abstract void ValidateCertificateRequest(CertificateRequest certificateRequest);

		// Token: 0x0600289C RID: 10396 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void SkipClientCredentials()
		{
		}

		// Token: 0x0600289D RID: 10397
		public abstract void ProcessClientCredentials(TlsCredentials clientCredentials);

		// Token: 0x0600289E RID: 10398 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void ProcessClientCertificate(Certificate clientCertificate)
		{
		}

		// Token: 0x0600289F RID: 10399
		public abstract void GenerateClientKeyExchange(Stream output);

		// Token: 0x060028A0 RID: 10400 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		public virtual void ProcessClientKeyExchange(Stream input)
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060028A1 RID: 10401
		public abstract byte[] GeneratePremasterSecret();

		// Token: 0x040019E2 RID: 6626
		protected readonly int mKeyExchange;

		// Token: 0x040019E3 RID: 6627
		protected IList mSupportedSignatureAlgorithms;

		// Token: 0x040019E4 RID: 6628
		protected TlsContext mContext;
	}
}
