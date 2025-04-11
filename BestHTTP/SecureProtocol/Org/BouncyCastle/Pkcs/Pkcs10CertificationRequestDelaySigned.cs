using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs
{
	// Token: 0x020002B3 RID: 691
	public class Pkcs10CertificationRequestDelaySigned : Pkcs10CertificationRequest
	{
		// Token: 0x060019CF RID: 6607 RVA: 0x000C62BD File Offset: 0x000C44BD
		protected Pkcs10CertificationRequestDelaySigned()
		{
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000C62C5 File Offset: 0x000C44C5
		public Pkcs10CertificationRequestDelaySigned(byte[] encoded) : base(encoded)
		{
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000C62CE File Offset: 0x000C44CE
		public Pkcs10CertificationRequestDelaySigned(Asn1Sequence seq) : base(seq)
		{
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000C62D7 File Offset: 0x000C44D7
		public Pkcs10CertificationRequestDelaySigned(Stream input) : base(input)
		{
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000C62E0 File Offset: 0x000C44E0
		public Pkcs10CertificationRequestDelaySigned(string signatureAlgorithm, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes, AsymmetricKeyParameter signingKey) : base(signatureAlgorithm, subject, publicKey, attributes, signingKey)
		{
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000C62F0 File Offset: 0x000C44F0
		public Pkcs10CertificationRequestDelaySigned(string signatureAlgorithm, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes)
		{
			if (signatureAlgorithm == null)
			{
				throw new ArgumentNullException("signatureAlgorithm");
			}
			if (subject == null)
			{
				throw new ArgumentNullException("subject");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("expected public key", "publicKey");
			}
			string text = Platform.ToUpperInvariant(signatureAlgorithm);
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)Pkcs10CertificationRequest.algorithms[text];
			if (derObjectIdentifier == null)
			{
				try
				{
					derObjectIdentifier = new DerObjectIdentifier(text);
				}
				catch (Exception innerException)
				{
					throw new ArgumentException("Unknown signature type requested", innerException);
				}
			}
			if (Pkcs10CertificationRequest.noParams.Contains(derObjectIdentifier))
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier);
			}
			else if (Pkcs10CertificationRequest.exParams.Contains(text))
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier, (Asn1Encodable)Pkcs10CertificationRequest.exParams[text]);
			}
			else
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier, DerNull.Instance);
			}
			SubjectPublicKeyInfo pkInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			this.reqInfo = new CertificationRequestInfo(subject, pkInfo, attributes);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000C63F4 File Offset: 0x000C45F4
		public byte[] GetDataToSign()
		{
			return this.reqInfo.GetDerEncoded();
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000C6401 File Offset: 0x000C4601
		public void SignRequest(byte[] signedData)
		{
			this.sigBits = new DerBitString(signedData);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000C640F File Offset: 0x000C460F
		public void SignRequest(DerBitString signedData)
		{
			this.sigBits = signedData;
		}
	}
}
