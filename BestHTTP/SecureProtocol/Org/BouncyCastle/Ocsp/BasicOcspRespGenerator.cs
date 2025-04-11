using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002DE RID: 734
	public class BasicOcspRespGenerator
	{
		// Token: 0x06001B1C RID: 6940 RVA: 0x000D1D4D File Offset: 0x000CFF4D
		public BasicOcspRespGenerator(RespID responderID)
		{
			this.responderID = responderID;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000D1D67 File Offset: 0x000CFF67
		public BasicOcspRespGenerator(AsymmetricKeyParameter publicKey)
		{
			this.responderID = new RespID(publicKey);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000D1D86 File Offset: 0x000CFF86
		public void AddResponse(CertificateID certID, CertificateStatus certStatus)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, null));
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000D1DA1 File Offset: 0x000CFFA1
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, singleExtensions));
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000D1DBC File Offset: 0x000CFFBC
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, DateTime nextUpdate, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, nextUpdate, singleExtensions));
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000D1DD9 File Offset: 0x000CFFD9
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, DateTime thisUpdate, DateTime nextUpdate, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, thisUpdate, nextUpdate, singleExtensions));
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000D1DF3 File Offset: 0x000CFFF3
		public void SetResponseExtensions(X509Extensions responseExtensions)
		{
			this.responseExtensions = responseExtensions;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000D1DFC File Offset: 0x000CFFFC
		private BasicOcspResp GenerateResponse(ISignatureFactory signatureCalculator, X509Certificate[] chain, DateTime producedAt)
		{
			DerObjectIdentifier algorithm = ((AlgorithmIdentifier)signatureCalculator.AlgorithmDetails).Algorithm;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this.list)
			{
				BasicOcspRespGenerator.ResponseObject responseObject = (BasicOcspRespGenerator.ResponseObject)obj;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						responseObject.ToResponse()
					});
				}
				catch (Exception e)
				{
					throw new OcspException("exception creating Request", e);
				}
			}
			ResponseData responseData = new ResponseData(this.responderID.ToAsn1Object(), new DerGeneralizedTime(producedAt), new DerSequence(asn1EncodableVector), this.responseExtensions);
			DerBitString signature = null;
			try
			{
				IStreamCalculator streamCalculator = signatureCalculator.CreateCalculator();
				byte[] derEncoded = responseData.GetDerEncoded();
				streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
				Platform.Dispose(streamCalculator.Stream);
				signature = new DerBitString(((IBlockResult)streamCalculator.GetResult()).Collect());
			}
			catch (Exception ex)
			{
				throw new OcspException("exception processing TBSRequest: " + ex, ex);
			}
			AlgorithmIdentifier sigAlgID = OcspUtilities.GetSigAlgID(algorithm);
			DerSequence certs = null;
			if (chain != null && chain.Length != 0)
			{
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
				try
				{
					for (int num = 0; num != chain.Length; num++)
					{
						asn1EncodableVector2.Add(new Asn1Encodable[]
						{
							X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(chain[num].GetEncoded()))
						});
					}
				}
				catch (IOException e2)
				{
					throw new OcspException("error processing certs", e2);
				}
				catch (CertificateEncodingException e3)
				{
					throw new OcspException("error encoding certs", e3);
				}
				certs = new DerSequence(asn1EncodableVector2);
			}
			return new BasicOcspResp(new BasicOcspResponse(responseData, sigAlgID, signature, certs));
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000D1FD4 File Offset: 0x000D01D4
		public BasicOcspResp Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, DateTime thisUpdate)
		{
			return this.Generate(signingAlgorithm, privateKey, chain, thisUpdate, null);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000D1FE2 File Offset: 0x000D01E2
		public BasicOcspResp Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, DateTime producedAt, SecureRandom random)
		{
			if (signingAlgorithm == null)
			{
				throw new ArgumentException("no signing algorithm specified");
			}
			return this.GenerateResponse(new Asn1SignatureFactory(signingAlgorithm, privateKey, random), chain, producedAt);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000D2004 File Offset: 0x000D0204
		public BasicOcspResp Generate(ISignatureFactory signatureCalculatorFactory, X509Certificate[] chain, DateTime producedAt)
		{
			if (signatureCalculatorFactory == null)
			{
				throw new ArgumentException("no signature calculator specified");
			}
			return this.GenerateResponse(signatureCalculatorFactory, chain, producedAt);
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x000D201D File Offset: 0x000D021D
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return OcspUtilities.AlgNames;
			}
		}

		// Token: 0x040017BB RID: 6075
		private readonly IList list = Platform.CreateArrayList();

		// Token: 0x040017BC RID: 6076
		private X509Extensions responseExtensions;

		// Token: 0x040017BD RID: 6077
		private RespID responderID;

		// Token: 0x020008E3 RID: 2275
		private class ResponseObject
		{
			// Token: 0x06004D75 RID: 19829 RVA: 0x001B0C52 File Offset: 0x001AEE52
			public ResponseObject(CertificateID certId, CertificateStatus certStatus, DateTime thisUpdate, X509Extensions extensions) : this(certId, certStatus, new DerGeneralizedTime(thisUpdate), null, extensions)
			{
			}

			// Token: 0x06004D76 RID: 19830 RVA: 0x001B0C65 File Offset: 0x001AEE65
			public ResponseObject(CertificateID certId, CertificateStatus certStatus, DateTime thisUpdate, DateTime nextUpdate, X509Extensions extensions) : this(certId, certStatus, new DerGeneralizedTime(thisUpdate), new DerGeneralizedTime(nextUpdate), extensions)
			{
			}

			// Token: 0x06004D77 RID: 19831 RVA: 0x001B0C80 File Offset: 0x001AEE80
			private ResponseObject(CertificateID certId, CertificateStatus certStatus, DerGeneralizedTime thisUpdate, DerGeneralizedTime nextUpdate, X509Extensions extensions)
			{
				this.certId = certId;
				if (certStatus == null)
				{
					this.certStatus = new CertStatus();
				}
				else if (certStatus is UnknownStatus)
				{
					this.certStatus = new CertStatus(2, DerNull.Instance);
				}
				else
				{
					RevokedStatus revokedStatus = (RevokedStatus)certStatus;
					CrlReason revocationReason = revokedStatus.HasRevocationReason ? new CrlReason(revokedStatus.RevocationReason) : null;
					this.certStatus = new CertStatus(new RevokedInfo(new DerGeneralizedTime(revokedStatus.RevocationTime), revocationReason));
				}
				this.thisUpdate = thisUpdate;
				this.nextUpdate = nextUpdate;
				this.extensions = extensions;
			}

			// Token: 0x06004D78 RID: 19832 RVA: 0x001B0D16 File Offset: 0x001AEF16
			public SingleResponse ToResponse()
			{
				return new SingleResponse(this.certId.ToAsn1Object(), this.certStatus, this.thisUpdate, this.nextUpdate, this.extensions);
			}

			// Token: 0x04003420 RID: 13344
			internal CertificateID certId;

			// Token: 0x04003421 RID: 13345
			internal CertStatus certStatus;

			// Token: 0x04003422 RID: 13346
			internal DerGeneralizedTime thisUpdate;

			// Token: 0x04003423 RID: 13347
			internal DerGeneralizedTime nextUpdate;

			// Token: 0x04003424 RID: 13348
			internal X509Extensions extensions;
		}
	}
}
