using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200044B RID: 1099
	public class TlsDHKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x06002B42 RID: 11074 RVA: 0x00117D15 File Offset: 0x00115F15
		[Obsolete("Use constructor that takes a TlsDHVerifier")]
		public TlsDHKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, DHParameters dhParameters) : this(keyExchange, supportedSignatureAlgorithms, new DefaultTlsDHVerifier(), dhParameters)
		{
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x00117D28 File Offset: 0x00115F28
		public TlsDHKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsDHVerifier dhVerifier, DHParameters dhParameters) : base(keyExchange, supportedSignatureAlgorithms)
		{
			switch (keyExchange)
			{
			case 3:
				this.mTlsSigner = new TlsDssSigner();
				goto IL_64;
			case 5:
				this.mTlsSigner = new TlsRsaSigner();
				goto IL_64;
			case 7:
			case 9:
			case 11:
				this.mTlsSigner = null;
				goto IL_64;
			}
			throw new InvalidOperationException("unsupported key exchange algorithm");
			IL_64:
			this.mDHVerifier = dhVerifier;
			this.mDHParameters = dhParameters;
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x00117DA8 File Offset: 0x00115FA8
		public override void Init(TlsContext context)
		{
			base.Init(context);
			if (this.mTlsSigner != null)
			{
				this.mTlsSigner.Init(context);
			}
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x00117DC5 File Offset: 0x00115FC5
		public override void SkipServerCredentials()
		{
			if (this.mKeyExchange != 11)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x00117DDC File Offset: 0x00115FDC
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mKeyExchange == 11)
			{
				throw new TlsFatalAlert(10);
			}
			if (serverCertificate.IsEmpty)
			{
				throw new TlsFatalAlert(42);
			}
			X509CertificateStructure certificateAt = serverCertificate.GetCertificateAt(0);
			SubjectPublicKeyInfo subjectPublicKeyInfo = certificateAt.SubjectPublicKeyInfo;
			try
			{
				this.mServerPublicKey = PublicKeyFactory.CreateKey(subjectPublicKeyInfo);
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(43, alertCause);
			}
			if (this.mTlsSigner == null)
			{
				try
				{
					this.mDHAgreePublicKey = (DHPublicKeyParameters)this.mServerPublicKey;
					this.mDHParameters = this.mDHAgreePublicKey.Parameters;
				}
				catch (InvalidCastException alertCause2)
				{
					throw new TlsFatalAlert(46, alertCause2);
				}
				TlsUtilities.ValidateKeyUsage(certificateAt, 8);
			}
			else
			{
				if (!this.mTlsSigner.IsValidPublicKey(this.mServerPublicKey))
				{
					throw new TlsFatalAlert(46);
				}
				TlsUtilities.ValidateKeyUsage(certificateAt, 128);
			}
			base.ProcessServerCertificate(serverCertificate);
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x00117EBC File Offset: 0x001160BC
		public override bool RequiresServerKeyExchange
		{
			get
			{
				int mKeyExchange = this.mKeyExchange;
				return mKeyExchange == 3 || mKeyExchange == 5 || mKeyExchange == 11;
			}
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x00117EE0 File Offset: 0x001160E0
		public override byte[] GenerateServerKeyExchange()
		{
			if (!this.RequiresServerKeyExchange)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mDHParameters, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x00117F20 File Offset: 0x00116120
		public override void ProcessServerKeyExchange(Stream input)
		{
			if (!this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(10);
			}
			this.mDHParameters = TlsDHUtilities.ReceiveDHParameters(this.mDHVerifier, input);
			this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input), this.mDHParameters);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x00117F5C File Offset: 0x0011615C
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			if (this.mKeyExchange == 11)
			{
				throw new TlsFatalAlert(40);
			}
			foreach (byte b in certificateRequest.CertificateTypes)
			{
				if (b - 1 > 3 && b != 64)
				{
					throw new TlsFatalAlert(47);
				}
			}
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x00117FA7 File Offset: 0x001161A7
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			if (this.mKeyExchange == 11)
			{
				throw new TlsFatalAlert(80);
			}
			if (clientCredentials is TlsAgreementCredentials)
			{
				this.mAgreementCredentials = (TlsAgreementCredentials)clientCredentials;
				return;
			}
			if (!(clientCredentials is TlsSignerCredentials))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x00117FE0 File Offset: 0x001161E0
		public override void GenerateClientKeyExchange(Stream output)
		{
			if (this.mAgreementCredentials == null)
			{
				this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralClientKeyExchange(this.mContext.SecureRandom, this.mDHParameters, output);
			}
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x00118007 File Offset: 0x00116207
		public override void ProcessClientCertificate(Certificate clientCertificate)
		{
			if (this.mKeyExchange == 11)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0011801B File Offset: 0x0011621B
		public override void ProcessClientKeyExchange(Stream input)
		{
			if (this.mDHAgreePublicKey != null)
			{
				return;
			}
			this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input), this.mDHParameters);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0011803D File Offset: 0x0011623D
		public override byte[] GeneratePremasterSecret()
		{
			if (this.mAgreementCredentials != null)
			{
				return this.mAgreementCredentials.GenerateAgreement(this.mDHAgreePublicKey);
			}
			if (this.mDHAgreePrivateKey != null)
			{
				return TlsDHUtilities.CalculateDHBasicAgreement(this.mDHAgreePublicKey, this.mDHAgreePrivateKey);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x04001CEB RID: 7403
		protected TlsSigner mTlsSigner;

		// Token: 0x04001CEC RID: 7404
		protected TlsDHVerifier mDHVerifier;

		// Token: 0x04001CED RID: 7405
		protected DHParameters mDHParameters;

		// Token: 0x04001CEE RID: 7406
		protected AsymmetricKeyParameter mServerPublicKey;

		// Token: 0x04001CEF RID: 7407
		protected TlsAgreementCredentials mAgreementCredentials;

		// Token: 0x04001CF0 RID: 7408
		protected DHPrivateKeyParameters mDHAgreePrivateKey;

		// Token: 0x04001CF1 RID: 7409
		protected DHPublicKeyParameters mDHAgreePublicKey;
	}
}
