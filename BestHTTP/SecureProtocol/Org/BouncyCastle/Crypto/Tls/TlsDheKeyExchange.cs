using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200044A RID: 1098
	public class TlsDheKeyExchange : TlsDHKeyExchange
	{
		// Token: 0x06002B3C RID: 11068 RVA: 0x00117B46 File Offset: 0x00115D46
		[Obsolete("Use constructor that takes a TlsDHVerifier")]
		public TlsDheKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, DHParameters dhParameters) : this(keyExchange, supportedSignatureAlgorithms, new DefaultTlsDHVerifier(), dhParameters)
		{
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00117B56 File Offset: 0x00115D56
		public TlsDheKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsDHVerifier dhVerifier, DHParameters dhParameters) : base(keyExchange, supportedSignatureAlgorithms, dhVerifier, dhParameters)
		{
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00117B63 File Offset: 0x00115D63
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (!(serverCredentials is TlsSignerCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsSignerCredentials)serverCredentials;
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x00117B90 File Offset: 0x00115D90
		public override byte[] GenerateServerKeyExchange()
		{
			if (this.mDHParameters == null)
			{
				throw new TlsFatalAlert(80);
			}
			DigestInputBuffer digestInputBuffer = new DigestInputBuffer();
			this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mDHParameters, digestInputBuffer);
			SignatureAndHashAlgorithm signatureAndHashAlgorithm = TlsUtilities.GetSignatureAndHashAlgorithm(this.mContext, this.mServerCredentials);
			IDigest digest = TlsUtilities.CreateHash(signatureAndHashAlgorithm);
			SecurityParameters securityParameters = this.mContext.SecurityParameters;
			digest.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
			digest.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
			digestInputBuffer.UpdateDigest(digest);
			byte[] hash = DigestUtilities.DoFinal(digest);
			byte[] signature = this.mServerCredentials.GenerateCertificateSignature(hash);
			new DigitallySigned(signatureAndHashAlgorithm, signature).Encode(digestInputBuffer);
			return digestInputBuffer.ToArray();
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x00117C50 File Offset: 0x00115E50
		public override void ProcessServerKeyExchange(Stream input)
		{
			SecurityParameters securityParameters = this.mContext.SecurityParameters;
			SignerInputBuffer signerInputBuffer = new SignerInputBuffer();
			Stream input2 = new TeeInputStream(input, signerInputBuffer);
			this.mDHParameters = TlsDHUtilities.ReceiveDHParameters(this.mDHVerifier, input2);
			this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input2), this.mDHParameters);
			DigitallySigned digitallySigned = this.ParseSignature(input);
			ISigner signer = this.InitVerifyer(this.mTlsSigner, digitallySigned.Algorithm, securityParameters);
			signerInputBuffer.UpdateSigner(signer);
			if (!signer.VerifySignature(digitallySigned.Signature))
			{
				throw new TlsFatalAlert(51);
			}
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x00117CDC File Offset: 0x00115EDC
		protected virtual ISigner InitVerifyer(TlsSigner tlsSigner, SignatureAndHashAlgorithm algorithm, SecurityParameters securityParameters)
		{
			ISigner signer = tlsSigner.CreateVerifyer(algorithm, this.mServerPublicKey);
			signer.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
			signer.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
			return signer;
		}

		// Token: 0x04001CEA RID: 7402
		protected TlsSignerCredentials mServerCredentials;
	}
}
