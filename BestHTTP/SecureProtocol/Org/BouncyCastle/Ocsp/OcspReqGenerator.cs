using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002E3 RID: 739
	public class OcspReqGenerator
	{
		// Token: 0x06001B4B RID: 6987 RVA: 0x000D25A9 File Offset: 0x000D07A9
		public void AddRequest(CertificateID certId)
		{
			this.list.Add(new OcspReqGenerator.RequestObject(certId, null));
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000D25BE File Offset: 0x000D07BE
		public void AddRequest(CertificateID certId, X509Extensions singleRequestExtensions)
		{
			this.list.Add(new OcspReqGenerator.RequestObject(certId, singleRequestExtensions));
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x000D25D4 File Offset: 0x000D07D4
		public void SetRequestorName(X509Name requestorName)
		{
			try
			{
				this.requestorName = new GeneralName(4, requestorName);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("cannot encode principal", innerException);
			}
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000D2610 File Offset: 0x000D0810
		public void SetRequestorName(GeneralName requestorName)
		{
			this.requestorName = requestorName;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000D2619 File Offset: 0x000D0819
		public void SetRequestExtensions(X509Extensions requestExtensions)
		{
			this.requestExtensions = requestExtensions;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000D2624 File Offset: 0x000D0824
		private OcspReq GenerateRequest(DerObjectIdentifier signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, SecureRandom random)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this.list)
			{
				OcspReqGenerator.RequestObject requestObject = (OcspReqGenerator.RequestObject)obj;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						requestObject.ToRequest()
					});
				}
				catch (Exception e)
				{
					throw new OcspException("exception creating Request", e);
				}
			}
			TbsRequest tbsRequest = new TbsRequest(this.requestorName, new DerSequence(asn1EncodableVector), this.requestExtensions);
			ISigner signer = null;
			Signature optionalSignature = null;
			if (signingAlgorithm != null)
			{
				if (this.requestorName == null)
				{
					throw new OcspException("requestorName must be specified if request is signed.");
				}
				try
				{
					signer = SignerUtilities.GetSigner(signingAlgorithm.Id);
					if (random != null)
					{
						signer.Init(true, new ParametersWithRandom(privateKey, random));
					}
					else
					{
						signer.Init(true, privateKey);
					}
				}
				catch (Exception ex)
				{
					throw new OcspException("exception creating signature: " + ex, ex);
				}
				DerBitString signatureValue = null;
				try
				{
					byte[] encoded = tbsRequest.GetEncoded();
					signer.BlockUpdate(encoded, 0, encoded.Length);
					signatureValue = new DerBitString(signer.GenerateSignature());
				}
				catch (Exception ex2)
				{
					throw new OcspException("exception processing TBSRequest: " + ex2, ex2);
				}
				AlgorithmIdentifier signatureAlgorithm = new AlgorithmIdentifier(signingAlgorithm, DerNull.Instance);
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
					optionalSignature = new Signature(signatureAlgorithm, signatureValue, new DerSequence(asn1EncodableVector2));
				}
				else
				{
					optionalSignature = new Signature(signatureAlgorithm, signatureValue);
				}
			}
			return new OcspReq(new OcspRequest(tbsRequest, optionalSignature));
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000D2840 File Offset: 0x000D0A40
		public OcspReq Generate()
		{
			return this.GenerateRequest(null, null, null, null);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000D284C File Offset: 0x000D0A4C
		public OcspReq Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain)
		{
			return this.Generate(signingAlgorithm, privateKey, chain, null);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000D2858 File Offset: 0x000D0A58
		public OcspReq Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, SecureRandom random)
		{
			if (signingAlgorithm == null)
			{
				throw new ArgumentException("no signing algorithm specified");
			}
			OcspReq result;
			try
			{
				DerObjectIdentifier algorithmOid = OcspUtilities.GetAlgorithmOid(signingAlgorithm);
				result = this.GenerateRequest(algorithmOid, privateKey, chain, random);
			}
			catch (ArgumentException)
			{
				throw new ArgumentException("unknown signing algorithm specified: " + signingAlgorithm);
			}
			return result;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x000D201D File Offset: 0x000D021D
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return OcspUtilities.AlgNames;
			}
		}

		// Token: 0x040017C2 RID: 6082
		private IList list = Platform.CreateArrayList();

		// Token: 0x040017C3 RID: 6083
		private GeneralName requestorName;

		// Token: 0x040017C4 RID: 6084
		private X509Extensions requestExtensions;

		// Token: 0x020008E4 RID: 2276
		private class RequestObject
		{
			// Token: 0x06004D79 RID: 19833 RVA: 0x001B0D40 File Offset: 0x001AEF40
			public RequestObject(CertificateID certId, X509Extensions extensions)
			{
				this.certId = certId;
				this.extensions = extensions;
			}

			// Token: 0x06004D7A RID: 19834 RVA: 0x001B0D56 File Offset: 0x001AEF56
			public Request ToRequest()
			{
				return new Request(this.certId.ToAsn1Object(), this.extensions);
			}

			// Token: 0x04003425 RID: 13349
			internal CertificateID certId;

			// Token: 0x04003426 RID: 13350
			internal X509Extensions extensions;
		}
	}
}
