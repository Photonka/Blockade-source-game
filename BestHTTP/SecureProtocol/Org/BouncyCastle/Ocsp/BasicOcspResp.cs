using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002DD RID: 733
	public class BasicOcspResp : X509ExtensionBase
	{
		// Token: 0x06001B09 RID: 6921 RVA: 0x000D1A0A File Offset: 0x000CFC0A
		public BasicOcspResp(BasicOcspResponse resp)
		{
			this.resp = resp;
			this.data = resp.TbsResponseData;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000D1A28 File Offset: 0x000CFC28
		public byte[] GetTbsResponseData()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.data.GetDerEncoded();
			}
			catch (IOException e)
			{
				throw new OcspException("problem encoding tbsResponseData", e);
			}
			return derEncoded;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000D1A64 File Offset: 0x000CFC64
		public int Version
		{
			get
			{
				return this.data.Version.Value.IntValue + 1;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000D1A7D File Offset: 0x000CFC7D
		public RespID ResponderId
		{
			get
			{
				return new RespID(this.data.ResponderID);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000D1A8F File Offset: 0x000CFC8F
		public DateTime ProducedAt
		{
			get
			{
				return this.data.ProducedAt.ToDateTime();
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000D1AA4 File Offset: 0x000CFCA4
		public SingleResp[] Responses
		{
			get
			{
				Asn1Sequence responses = this.data.Responses;
				SingleResp[] array = new SingleResp[responses.Count];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = new SingleResp(SingleResponse.GetInstance(responses[num]));
				}
				return array;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000D1AEC File Offset: 0x000CFCEC
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.data.ResponseExtensions;
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000D1AF9 File Offset: 0x000CFCF9
		protected override X509Extensions GetX509Extensions()
		{
			return this.ResponseExtensions;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000D1B01 File Offset: 0x000CFD01
		public string SignatureAlgName
		{
			get
			{
				return OcspUtilities.GetAlgorithmName(this.resp.SignatureAlgorithm.Algorithm);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000D1B18 File Offset: 0x000CFD18
		public string SignatureAlgOid
		{
			get
			{
				return this.resp.SignatureAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000D1B2F File Offset: 0x000CFD2F
		[Obsolete("RespData class is no longer required as all functionality is available on this class")]
		public RespData GetResponseData()
		{
			return new RespData(this.data);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000D1B3C File Offset: 0x000CFD3C
		public byte[] GetSignature()
		{
			return this.resp.GetSignatureOctets();
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000D1B4C File Offset: 0x000CFD4C
		private IList GetCertList()
		{
			IList list = Platform.CreateArrayList();
			Asn1Sequence certs = this.resp.Certs;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					try
					{
						list.Add(new X509CertificateParser().ReadCertificate(asn1Encodable.GetEncoded()));
					}
					catch (IOException e)
					{
						throw new OcspException("can't re-encode certificate!", e);
					}
					catch (CertificateException e2)
					{
						throw new OcspException("can't re-encode certificate!", e2);
					}
				}
			}
			return list;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000D1C04 File Offset: 0x000CFE04
		public X509Certificate[] GetCerts()
		{
			IList certList = this.GetCertList();
			X509Certificate[] array = new X509Certificate[certList.Count];
			for (int i = 0; i < certList.Count; i++)
			{
				array[i] = (X509Certificate)certList[i];
			}
			return array;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000D1C48 File Offset: 0x000CFE48
		public IX509Store GetCertificates(string type)
		{
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

		// Token: 0x06001B18 RID: 6936 RVA: 0x000D1C94 File Offset: 0x000CFE94
		public bool Verify(AsymmetricKeyParameter publicKey)
		{
			bool result;
			try
			{
				ISigner signer = SignerUtilities.GetSigner(this.SignatureAlgName);
				signer.Init(false, publicKey);
				byte[] derEncoded = this.data.GetDerEncoded();
				signer.BlockUpdate(derEncoded, 0, derEncoded.Length);
				result = signer.VerifySignature(this.GetSignature());
			}
			catch (Exception ex)
			{
				throw new OcspException("exception processing sig: " + ex, ex);
			}
			return result;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000D1D00 File Offset: 0x000CFF00
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000D1D10 File Offset: 0x000CFF10
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BasicOcspResp basicOcspResp = obj as BasicOcspResp;
			return basicOcspResp != null && this.resp.Equals(basicOcspResp.resp);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000D1D40 File Offset: 0x000CFF40
		public override int GetHashCode()
		{
			return this.resp.GetHashCode();
		}

		// Token: 0x040017B9 RID: 6073
		private readonly BasicOcspResponse resp;

		// Token: 0x040017BA RID: 6074
		private readonly ResponseData data;
	}
}
