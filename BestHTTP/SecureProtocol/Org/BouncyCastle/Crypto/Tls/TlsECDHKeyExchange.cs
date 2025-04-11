using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000452 RID: 1106
	public class TlsECDHKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x06002BA7 RID: 11175 RVA: 0x00119328 File Offset: 0x00117528
		public TlsECDHKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, int[] namedCurves, byte[] clientECPointFormats, byte[] serverECPointFormats) : base(keyExchange, supportedSignatureAlgorithms)
		{
			switch (keyExchange)
			{
			case 16:
			case 18:
			case 20:
				this.mTlsSigner = null;
				break;
			case 17:
				this.mTlsSigner = new TlsECDsaSigner();
				break;
			case 19:
				this.mTlsSigner = new TlsRsaSigner();
				break;
			default:
				throw new InvalidOperationException("unsupported key exchange algorithm");
			}
			this.mNamedCurves = namedCurves;
			this.mClientECPointFormats = clientECPointFormats;
			this.mServerECPointFormats = serverECPointFormats;
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x001193A1 File Offset: 0x001175A1
		public override void Init(TlsContext context)
		{
			base.Init(context);
			if (this.mTlsSigner != null)
			{
				this.mTlsSigner.Init(context);
			}
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x001193BE File Offset: 0x001175BE
		public override void SkipServerCredentials()
		{
			if (this.mKeyExchange != 20)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x001193D4 File Offset: 0x001175D4
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mKeyExchange == 20)
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
					this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey((ECPublicKeyParameters)this.mServerPublicKey);
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

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x001194A8 File Offset: 0x001176A8
		public override bool RequiresServerKeyExchange
		{
			get
			{
				int mKeyExchange = this.mKeyExchange;
				return mKeyExchange == 17 || mKeyExchange - 19 <= 1;
			}
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x001194CC File Offset: 0x001176CC
		public override byte[] GenerateServerKeyExchange()
		{
			if (!this.RequiresServerKeyExchange)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mNamedCurves, this.mClientECPointFormats, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x00119514 File Offset: 0x00117714
		public override void ProcessServerKeyExchange(Stream input)
		{
			if (!this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(10);
			}
			ECDomainParameters curve_params = TlsEccUtilities.ReadECParameters(this.mNamedCurves, this.mClientECPointFormats, input);
			byte[] encoding = TlsUtilities.ReadOpaque8(input);
			this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mClientECPointFormats, curve_params, encoding));
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x00119564 File Offset: 0x00117764
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			if (this.mKeyExchange == 20)
			{
				throw new TlsFatalAlert(40);
			}
			foreach (byte b in certificateRequest.CertificateTypes)
			{
				if (b - 1 > 1 && b - 64 > 2)
				{
					throw new TlsFatalAlert(47);
				}
			}
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x001195B1 File Offset: 0x001177B1
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			if (this.mKeyExchange == 20)
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

		// Token: 0x06002BB0 RID: 11184 RVA: 0x001195EA File Offset: 0x001177EA
		public override void GenerateClientKeyExchange(Stream output)
		{
			if (this.mAgreementCredentials == null)
			{
				this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralClientKeyExchange(this.mContext.SecureRandom, this.mServerECPointFormats, this.mECAgreePublicKey.Parameters, output);
			}
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0011961C File Offset: 0x0011781C
		public override void ProcessClientCertificate(Certificate clientCertificate)
		{
			if (this.mKeyExchange == 20)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x00119630 File Offset: 0x00117830
		public override void ProcessClientKeyExchange(Stream input)
		{
			if (this.mECAgreePublicKey != null)
			{
				return;
			}
			byte[] encoding = TlsUtilities.ReadOpaque8(input);
			ECDomainParameters parameters = this.mECAgreePrivateKey.Parameters;
			this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mServerECPointFormats, parameters, encoding));
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x00119671 File Offset: 0x00117871
		public override byte[] GeneratePremasterSecret()
		{
			if (this.mAgreementCredentials != null)
			{
				return this.mAgreementCredentials.GenerateAgreement(this.mECAgreePublicKey);
			}
			if (this.mECAgreePrivateKey != null)
			{
				return TlsEccUtilities.CalculateECDHBasicAgreement(this.mECAgreePublicKey, this.mECAgreePrivateKey);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x04001CFF RID: 7423
		protected TlsSigner mTlsSigner;

		// Token: 0x04001D00 RID: 7424
		protected int[] mNamedCurves;

		// Token: 0x04001D01 RID: 7425
		protected byte[] mClientECPointFormats;

		// Token: 0x04001D02 RID: 7426
		protected byte[] mServerECPointFormats;

		// Token: 0x04001D03 RID: 7427
		protected AsymmetricKeyParameter mServerPublicKey;

		// Token: 0x04001D04 RID: 7428
		protected TlsAgreementCredentials mAgreementCredentials;

		// Token: 0x04001D05 RID: 7429
		protected ECPrivateKeyParameters mECAgreePrivateKey;

		// Token: 0x04001D06 RID: 7430
		protected ECPublicKeyParameters mECAgreePublicKey;
	}
}
