using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002E2 RID: 738
	public class OcspReq : X509ExtensionBase
	{
		// Token: 0x06001B39 RID: 6969 RVA: 0x000D2204 File Offset: 0x000D0404
		public OcspReq(OcspRequest req)
		{
			this.req = req;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000D2213 File Offset: 0x000D0413
		public OcspReq(byte[] req) : this(new Asn1InputStream(req))
		{
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000D2221 File Offset: 0x000D0421
		public OcspReq(Stream inStr) : this(new Asn1InputStream(inStr))
		{
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000D2230 File Offset: 0x000D0430
		private OcspReq(Asn1InputStream aIn)
		{
			try
			{
				this.req = OcspRequest.GetInstance(aIn.ReadObject());
			}
			catch (ArgumentException ex)
			{
				throw new IOException("malformed request: " + ex.Message);
			}
			catch (InvalidCastException ex2)
			{
				throw new IOException("malformed request: " + ex2.Message);
			}
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000D22A0 File Offset: 0x000D04A0
		public byte[] GetTbsRequest()
		{
			byte[] encoded;
			try
			{
				encoded = this.req.TbsRequest.GetEncoded();
			}
			catch (IOException e)
			{
				throw new OcspException("problem encoding tbsRequest", e);
			}
			return encoded;
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000D22E0 File Offset: 0x000D04E0
		public int Version
		{
			get
			{
				return this.req.TbsRequest.Version.Value.IntValue + 1;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x000D22FE File Offset: 0x000D04FE
		public GeneralName RequestorName
		{
			get
			{
				return GeneralName.GetInstance(this.req.TbsRequest.RequestorName);
			}
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000D2318 File Offset: 0x000D0518
		public Req[] GetRequestList()
		{
			Asn1Sequence requestList = this.req.TbsRequest.RequestList;
			Req[] array = new Req[requestList.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = new Req(Request.GetInstance(requestList[num]));
			}
			return array;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x000D2365 File Offset: 0x000D0565
		public X509Extensions RequestExtensions
		{
			get
			{
				return X509Extensions.GetInstance(this.req.TbsRequest.RequestExtensions);
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000D237C File Offset: 0x000D057C
		protected override X509Extensions GetX509Extensions()
		{
			return this.RequestExtensions;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x000D2384 File Offset: 0x000D0584
		public string SignatureAlgOid
		{
			get
			{
				if (!this.IsSigned)
				{
					return null;
				}
				return this.req.OptionalSignature.SignatureAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000D23AA File Offset: 0x000D05AA
		public byte[] GetSignature()
		{
			if (!this.IsSigned)
			{
				return null;
			}
			return this.req.OptionalSignature.GetSignatureOctets();
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000D23C8 File Offset: 0x000D05C8
		private IList GetCertList()
		{
			IList list = Platform.CreateArrayList();
			Asn1Sequence certs = this.req.OptionalSignature.Certs;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					try
					{
						list.Add(new X509CertificateParser().ReadCertificate(asn1Encodable.GetEncoded()));
					}
					catch (Exception e)
					{
						throw new OcspException("can't re-encode certificate!", e);
					}
				}
			}
			return list;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000D2468 File Offset: 0x000D0668
		public X509Certificate[] GetCerts()
		{
			if (!this.IsSigned)
			{
				return null;
			}
			IList certList = this.GetCertList();
			X509Certificate[] array = new X509Certificate[certList.Count];
			for (int i = 0; i < certList.Count; i++)
			{
				array[i] = (X509Certificate)certList[i];
			}
			return array;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000D24B4 File Offset: 0x000D06B4
		public IX509Store GetCertificates(string type)
		{
			if (!this.IsSigned)
			{
				return null;
			}
			IX509Store result;
			try
			{
				result = X509StoreFactory.Create("Certificate/" + type, new X509CollectionStoreParameters(this.GetCertList()));
			}
			catch (Exception e)
			{
				throw new OcspException("can't setup the CertStore", e);
			}
			return result;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x000D2508 File Offset: 0x000D0708
		public bool IsSigned
		{
			get
			{
				return this.req.OptionalSignature != null;
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000D2518 File Offset: 0x000D0718
		public bool Verify(AsymmetricKeyParameter publicKey)
		{
			if (!this.IsSigned)
			{
				throw new OcspException("attempt to Verify signature on unsigned object");
			}
			bool result;
			try
			{
				ISigner signer = SignerUtilities.GetSigner(this.SignatureAlgOid);
				signer.Init(false, publicKey);
				byte[] encoded = this.req.TbsRequest.GetEncoded();
				signer.BlockUpdate(encoded, 0, encoded.Length);
				result = signer.VerifySignature(this.GetSignature());
			}
			catch (Exception ex)
			{
				throw new OcspException("exception processing sig: " + ex, ex);
			}
			return result;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000D259C File Offset: 0x000D079C
		public byte[] GetEncoded()
		{
			return this.req.GetEncoded();
		}

		// Token: 0x040017C1 RID: 6081
		private OcspRequest req;
	}
}
