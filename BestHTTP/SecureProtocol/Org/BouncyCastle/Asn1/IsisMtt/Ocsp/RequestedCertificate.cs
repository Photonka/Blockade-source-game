using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.Ocsp
{
	// Token: 0x02000719 RID: 1817
	public class RequestedCertificate : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004246 RID: 16966 RVA: 0x00188E7C File Offset: 0x0018707C
		public static RequestedCertificate GetInstance(object obj)
		{
			if (obj == null || obj is RequestedCertificate)
			{
				return (RequestedCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RequestedCertificate(X509CertificateStructure.GetInstance(obj));
			}
			if (obj is Asn1TaggedObject)
			{
				return new RequestedCertificate((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00188EDD File Offset: 0x001870DD
		public static RequestedCertificate GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			if (!isExplicit)
			{
				throw new ArgumentException("choice item must be explicitly tagged");
			}
			return RequestedCertificate.GetInstance(obj.GetObject());
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00188EF8 File Offset: 0x001870F8
		private RequestedCertificate(Asn1TaggedObject tagged)
		{
			RequestedCertificate.Choice tagNo = (RequestedCertificate.Choice)tagged.TagNo;
			if (tagNo == RequestedCertificate.Choice.PublicKeyCertificate)
			{
				this.publicKeyCert = Asn1OctetString.GetInstance(tagged, true).GetOctets();
				return;
			}
			if (tagNo == RequestedCertificate.Choice.AttributeCertificate)
			{
				this.attributeCert = Asn1OctetString.GetInstance(tagged, true).GetOctets();
				return;
			}
			throw new ArgumentException("unknown tag number: " + tagged.TagNo);
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x00188F59 File Offset: 0x00187159
		public RequestedCertificate(X509CertificateStructure certificate)
		{
			this.cert = certificate;
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x00188F68 File Offset: 0x00187168
		public RequestedCertificate(RequestedCertificate.Choice type, byte[] certificateOctets) : this(new DerTaggedObject((int)type, new DerOctetString(certificateOctets)))
		{
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x00188F7C File Offset: 0x0018717C
		public RequestedCertificate.Choice Type
		{
			get
			{
				if (this.cert != null)
				{
					return RequestedCertificate.Choice.Certificate;
				}
				if (this.publicKeyCert != null)
				{
					return RequestedCertificate.Choice.PublicKeyCertificate;
				}
				return RequestedCertificate.Choice.AttributeCertificate;
			}
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x00188F94 File Offset: 0x00187194
		public byte[] GetCertificateBytes()
		{
			if (this.cert != null)
			{
				try
				{
					return this.cert.GetEncoded();
				}
				catch (IOException arg)
				{
					throw new InvalidOperationException("can't decode certificate: " + arg);
				}
			}
			if (this.publicKeyCert != null)
			{
				return this.publicKeyCert;
			}
			return this.attributeCert;
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x00188FF0 File Offset: 0x001871F0
		public override Asn1Object ToAsn1Object()
		{
			if (this.publicKeyCert != null)
			{
				return new DerTaggedObject(0, new DerOctetString(this.publicKeyCert));
			}
			if (this.attributeCert != null)
			{
				return new DerTaggedObject(1, new DerOctetString(this.attributeCert));
			}
			return this.cert.ToAsn1Object();
		}

		// Token: 0x04002A54 RID: 10836
		private readonly X509CertificateStructure cert;

		// Token: 0x04002A55 RID: 10837
		private readonly byte[] publicKeyCert;

		// Token: 0x04002A56 RID: 10838
		private readonly byte[] attributeCert;

		// Token: 0x020009B0 RID: 2480
		public enum Choice
		{
			// Token: 0x04003666 RID: 13926
			Certificate = -1,
			// Token: 0x04003667 RID: 13927
			PublicKeyCertificate,
			// Token: 0x04003668 RID: 13928
			AttributeCertificate
		}
	}
}
