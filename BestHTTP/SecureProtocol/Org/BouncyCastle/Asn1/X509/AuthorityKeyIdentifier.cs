using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000677 RID: 1655
	public class AuthorityKeyIdentifier : Asn1Encodable
	{
		// Token: 0x06003DAA RID: 15786 RVA: 0x0017772E File Offset: 0x0017592E
		public static AuthorityKeyIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AuthorityKeyIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x0017773C File Offset: 0x0017593C
		public static AuthorityKeyIdentifier GetInstance(object obj)
		{
			if (obj is AuthorityKeyIdentifier)
			{
				return (AuthorityKeyIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthorityKeyIdentifier((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return AuthorityKeyIdentifier.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x001777A0 File Offset: 0x001759A0
		protected internal AuthorityKeyIdentifier(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.keyidentifier = Asn1OctetString.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					this.certissuer = GeneralNames.GetInstance(asn1TaggedObject, false);
					break;
				case 2:
					this.certserno = DerInteger.GetInstance(asn1TaggedObject, false);
					break;
				default:
					throw new ArgumentException("illegal tag");
				}
			}
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00177848 File Offset: 0x00175A48
		public AuthorityKeyIdentifier(SubjectPublicKeyInfo spki)
		{
			Sha1Digest sha1Digest = new Sha1Digest();
			byte[] array = new byte[((IDigest)sha1Digest).GetDigestSize()];
			byte[] bytes = spki.PublicKeyData.GetBytes();
			((IDigest)sha1Digest).BlockUpdate(bytes, 0, bytes.Length);
			((IDigest)sha1Digest).DoFinal(array, 0);
			this.keyidentifier = new DerOctetString(array);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x00177898 File Offset: 0x00175A98
		public AuthorityKeyIdentifier(SubjectPublicKeyInfo spki, GeneralNames name, BigInteger serialNumber)
		{
			Sha1Digest sha1Digest = new Sha1Digest();
			byte[] array = new byte[((IDigest)sha1Digest).GetDigestSize()];
			byte[] bytes = spki.PublicKeyData.GetBytes();
			((IDigest)sha1Digest).BlockUpdate(bytes, 0, bytes.Length);
			((IDigest)sha1Digest).DoFinal(array, 0);
			this.keyidentifier = new DerOctetString(array);
			this.certissuer = name;
			this.certserno = new DerInteger(serialNumber);
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x001778FA File Offset: 0x00175AFA
		public AuthorityKeyIdentifier(GeneralNames name, BigInteger serialNumber)
		{
			this.keyidentifier = null;
			this.certissuer = GeneralNames.GetInstance(name.ToAsn1Object());
			this.certserno = new DerInteger(serialNumber);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x00177926 File Offset: 0x00175B26
		public AuthorityKeyIdentifier(byte[] keyIdentifier)
		{
			this.keyidentifier = new DerOctetString(keyIdentifier);
			this.certissuer = null;
			this.certserno = null;
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x00177948 File Offset: 0x00175B48
		public AuthorityKeyIdentifier(byte[] keyIdentifier, GeneralNames name, BigInteger serialNumber)
		{
			this.keyidentifier = new DerOctetString(keyIdentifier);
			this.certissuer = GeneralNames.GetInstance(name.ToAsn1Object());
			this.certserno = new DerInteger(serialNumber);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x00177979 File Offset: 0x00175B79
		public byte[] GetKeyIdentifier()
		{
			if (this.keyidentifier != null)
			{
				return this.keyidentifier.GetOctets();
			}
			return null;
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x00177990 File Offset: 0x00175B90
		public GeneralNames AuthorityCertIssuer
		{
			get
			{
				return this.certissuer;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06003DB4 RID: 15796 RVA: 0x00177998 File Offset: 0x00175B98
		public BigInteger AuthorityCertSerialNumber
		{
			get
			{
				if (this.certserno != null)
				{
					return this.certserno.Value;
				}
				return null;
			}
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x001779B0 File Offset: 0x00175BB0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.keyidentifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.keyidentifier)
				});
			}
			if (this.certissuer != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.certissuer)
				});
			}
			if (this.certserno != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.certserno)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x00177A3A File Offset: 0x00175C3A
		public override string ToString()
		{
			return "AuthorityKeyIdentifier: KeyID(" + this.keyidentifier.GetOctets() + ")";
		}

		// Token: 0x04002650 RID: 9808
		internal readonly Asn1OctetString keyidentifier;

		// Token: 0x04002651 RID: 9809
		internal readonly GeneralNames certissuer;

		// Token: 0x04002652 RID: 9810
		internal readonly DerInteger certserno;
	}
}
