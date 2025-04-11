using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Operators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E7 RID: 1511
	public class CmsSignedDataGenerator : CmsSignedGenerator
	{
		// Token: 0x060039B4 RID: 14772 RVA: 0x0016A910 File Offset: 0x00168B10
		public CmsSignedDataGenerator()
		{
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x0016A923 File Offset: 0x00168B23
		public CmsSignedDataGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x0016A937 File Offset: 0x00168B37
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID)
		{
			this.AddSigner(privateKey, cert, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x0016A94E File Offset: 0x00168B4E
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), encryptionOID, digestOID, new DefaultSignedAttributeTableGenerator(), null, null);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x0016A967 File Offset: 0x00168B67
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID)
		{
			this.AddSigner(privateKey, subjectKeyID, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID);
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x0016A97E File Offset: 0x00168B7E
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), encryptionOID, digestOID, new DefaultSignedAttributeTableGenerator(), null, null);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x0016A997 File Offset: 0x00168B97
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID, signedAttr, unsignedAttr);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x0016A9B2 File Offset: 0x00168BB2
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), encryptionOID, digestOID, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr), signedAttr);
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x0016A9D4 File Offset: 0x00168BD4
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, subjectKeyID, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID, signedAttr, unsignedAttr);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x0016A9EF File Offset: 0x00168BEF
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), encryptionOID, digestOID, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr), signedAttr);
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x0016AA11 File Offset: 0x00168C11
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.AddSigner(privateKey, cert, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID, signedAttrGen, unsignedAttrGen);
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x0016AA2C File Offset: 0x00168C2C
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), encryptionOID, digestOID, signedAttrGen, unsignedAttrGen, null);
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x0016AA43 File Offset: 0x00168C43
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.AddSigner(privateKey, subjectKeyID, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID, signedAttrGen, unsignedAttrGen);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x0016AA5E File Offset: 0x00168C5E
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), encryptionOID, digestOID, signedAttrGen, unsignedAttrGen, null);
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x0016AA75 File Offset: 0x00168C75
		public void AddSignerInfoGenerator(SignerInfoGenerator signerInfoGenerator)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, signerInfoGenerator.contentSigner, signerInfoGenerator.sigId, signerInfoGenerator.signedGen, signerInfoGenerator.unsignedGen, null));
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x0016AAA4 File Offset: 0x00168CA4
		private void doAddSigner(AsymmetricKeyParameter privateKey, SignerIdentifier signerIdentifier, string encryptionOID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, privateKey, signerIdentifier, digestOID, encryptionOID, signedAttrGen, unsignedAttrGen, baseSignedTable));
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x0016AACE File Offset: 0x00168CCE
		public CmsSignedData Generate(CmsProcessable content)
		{
			return this.Generate(content, false);
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x0016AAD8 File Offset: 0x00168CD8
		public CmsSignedData Generate(string signedContentType, CmsProcessable content, bool encapsulate)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			this._digests.Clear();
			foreach (object obj in this._signers)
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					CmsSignedDataGenerator.Helper.FixAlgID(signerInformation.DigestAlgorithmID)
				});
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					signerInformation.ToSignerInfo()
				});
			}
			DerObjectIdentifier contentType = (signedContentType == null) ? null : new DerObjectIdentifier(signedContentType);
			foreach (object obj2 in this.signerInfs)
			{
				CmsSignedDataGenerator.SignerInf signerInf = (CmsSignedDataGenerator.SignerInf)obj2;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						signerInf.DigestAlgorithmID
					});
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						signerInf.ToSignerInfo(contentType, content, this.rand)
					});
				}
				catch (IOException e)
				{
					throw new CmsException("encoding error.", e);
				}
				catch (InvalidKeyException e2)
				{
					throw new CmsException("key inappropriate for signature.", e2);
				}
				catch (SignatureException e3)
				{
					throw new CmsException("error creating signature.", e3);
				}
				catch (CertificateEncodingException e4)
				{
					throw new CmsException("error creating sid.", e4);
				}
			}
			Asn1Set certificates = null;
			if (this._certs.Count != 0)
			{
				certificates = (base.UseDerForCerts ? CmsUtilities.CreateDerSetFromList(this._certs) : CmsUtilities.CreateBerSetFromList(this._certs));
			}
			Asn1Set crls = null;
			if (this._crls.Count != 0)
			{
				crls = (base.UseDerForCrls ? CmsUtilities.CreateDerSetFromList(this._crls) : CmsUtilities.CreateBerSetFromList(this._crls));
			}
			Asn1OctetString content2 = null;
			if (encapsulate)
			{
				MemoryStream memoryStream = new MemoryStream();
				if (content != null)
				{
					try
					{
						content.Write(memoryStream);
					}
					catch (IOException e5)
					{
						throw new CmsException("encapsulation error.", e5);
					}
				}
				content2 = new BerOctetString(memoryStream.ToArray());
			}
			ContentInfo contentInfo = new ContentInfo(contentType, content2);
			SignedData content3 = new SignedData(new DerSet(asn1EncodableVector), contentInfo, certificates, crls, new DerSet(asn1EncodableVector2));
			ContentInfo sigData = new ContentInfo(CmsObjectIdentifiers.SignedData, content3);
			return new CmsSignedData(content, sigData);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x0016AD68 File Offset: 0x00168F68
		public CmsSignedData Generate(CmsProcessable content, bool encapsulate)
		{
			return this.Generate(CmsSignedGenerator.Data, content, encapsulate);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x0016AD77 File Offset: 0x00168F77
		public SignerInformationStore GenerateCounterSigners(SignerInformation signer)
		{
			return this.Generate(null, new CmsProcessableByteArray(signer.GetSignature()), false).GetSignerInfos();
		}

		// Token: 0x040024DA RID: 9434
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x040024DB RID: 9435
		private readonly IList signerInfs = Platform.CreateArrayList();

		// Token: 0x0200095E RID: 2398
		private class SignerInf
		{
			// Token: 0x06004EF6 RID: 20214 RVA: 0x001B6E90 File Offset: 0x001B5090
			internal SignerInf(CmsSignedGenerator outer, AsymmetricKeyParameter key, SignerIdentifier signerIdentifier, string digestOID, string encOID, CmsAttributeTableGenerator sAttr, CmsAttributeTableGenerator unsAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable)
			{
				string algorithm = CmsSignedDataGenerator.Helper.GetDigestAlgName(digestOID) + "with" + CmsSignedDataGenerator.Helper.GetEncryptionAlgName(encOID);
				this.outer = outer;
				this.sigCalc = new Asn1SignatureFactory(algorithm, key);
				this.signerIdentifier = signerIdentifier;
				this.digestOID = digestOID;
				this.encOID = encOID;
				this.sAttr = sAttr;
				this.unsAttr = unsAttr;
				this.baseSignedTable = baseSignedTable;
			}

			// Token: 0x06004EF7 RID: 20215 RVA: 0x001B6F0C File Offset: 0x001B510C
			internal SignerInf(CmsSignedGenerator outer, ISignatureFactory sigCalc, SignerIdentifier signerIdentifier, CmsAttributeTableGenerator sAttr, CmsAttributeTableGenerator unsAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable)
			{
				this.outer = outer;
				this.sigCalc = sigCalc;
				this.signerIdentifier = signerIdentifier;
				this.digestOID = new DefaultDigestAlgorithmIdentifierFinder().find((AlgorithmIdentifier)sigCalc.AlgorithmDetails).Algorithm.Id;
				this.encOID = ((AlgorithmIdentifier)sigCalc.AlgorithmDetails).Algorithm.Id;
				this.sAttr = sAttr;
				this.unsAttr = unsAttr;
				this.baseSignedTable = baseSignedTable;
			}

			// Token: 0x17000C4F RID: 3151
			// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x001B6F8C File Offset: 0x001B518C
			internal AlgorithmIdentifier DigestAlgorithmID
			{
				get
				{
					return new AlgorithmIdentifier(new DerObjectIdentifier(this.digestOID), DerNull.Instance);
				}
			}

			// Token: 0x17000C50 RID: 3152
			// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x001B6FA3 File Offset: 0x001B51A3
			internal CmsAttributeTableGenerator SignedAttributes
			{
				get
				{
					return this.sAttr;
				}
			}

			// Token: 0x17000C51 RID: 3153
			// (get) Token: 0x06004EFA RID: 20218 RVA: 0x001B6FAB File Offset: 0x001B51AB
			internal CmsAttributeTableGenerator UnsignedAttributes
			{
				get
				{
					return this.unsAttr;
				}
			}

			// Token: 0x06004EFB RID: 20219 RVA: 0x001B6FB4 File Offset: 0x001B51B4
			internal SignerInfo ToSignerInfo(DerObjectIdentifier contentType, CmsProcessable content, SecureRandom random)
			{
				AlgorithmIdentifier digestAlgorithmID = this.DigestAlgorithmID;
				string digestAlgName = CmsSignedDataGenerator.Helper.GetDigestAlgName(this.digestOID);
				string algorithm = digestAlgName + "with" + CmsSignedDataGenerator.Helper.GetEncryptionAlgName(this.encOID);
				byte[] array;
				if (this.outer._digests.Contains(this.digestOID))
				{
					array = (byte[])this.outer._digests[this.digestOID];
				}
				else
				{
					IDigest digestInstance = CmsSignedDataGenerator.Helper.GetDigestInstance(digestAlgName);
					if (content != null)
					{
						content.Write(new DigestSink(digestInstance));
					}
					array = DigestUtilities.DoFinal(digestInstance);
					this.outer._digests.Add(this.digestOID, array.Clone());
				}
				IStreamCalculator streamCalculator = this.sigCalc.CreateCalculator();
				Stream stream = new BufferedStream(streamCalculator.Stream);
				Asn1Set asn1Set = null;
				if (this.sAttr != null)
				{
					IDictionary baseParameters = this.outer.GetBaseParameters(contentType, digestAlgorithmID, array);
					BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable attributeTable = this.sAttr.GetAttributes(baseParameters);
					if (contentType == null && attributeTable != null && attributeTable[CmsAttributes.ContentType] != null)
					{
						IDictionary dictionary = attributeTable.ToDictionary();
						dictionary.Remove(CmsAttributes.ContentType);
						attributeTable = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(dictionary);
					}
					asn1Set = this.outer.GetAttributeSet(attributeTable);
					new DerOutputStream(stream).WriteObject(asn1Set);
				}
				else if (content != null)
				{
					content.Write(stream);
				}
				Platform.Dispose(stream);
				byte[] array2 = ((IBlockResult)streamCalculator.GetResult()).Collect();
				Asn1Set unauthenticatedAttributes = null;
				if (this.unsAttr != null)
				{
					IDictionary baseParameters2 = this.outer.GetBaseParameters(contentType, digestAlgorithmID, array);
					baseParameters2[CmsAttributeTableParameter.Signature] = array2.Clone();
					BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this.unsAttr.GetAttributes(baseParameters2);
					unauthenticatedAttributes = this.outer.GetAttributeSet(attributes);
				}
				Asn1Encodable defaultX509Parameters = SignerUtilities.GetDefaultX509Parameters(algorithm);
				AlgorithmIdentifier encAlgorithmIdentifier = CmsSignedDataGenerator.Helper.GetEncAlgorithmIdentifier(new DerObjectIdentifier(this.encOID), defaultX509Parameters);
				return new SignerInfo(this.signerIdentifier, digestAlgorithmID, asn1Set, encAlgorithmIdentifier, new DerOctetString(array2), unauthenticatedAttributes);
			}

			// Token: 0x040035BF RID: 13759
			private readonly CmsSignedGenerator outer;

			// Token: 0x040035C0 RID: 13760
			private readonly ISignatureFactory sigCalc;

			// Token: 0x040035C1 RID: 13761
			private readonly SignerIdentifier signerIdentifier;

			// Token: 0x040035C2 RID: 13762
			private readonly string digestOID;

			// Token: 0x040035C3 RID: 13763
			private readonly string encOID;

			// Token: 0x040035C4 RID: 13764
			private readonly CmsAttributeTableGenerator sAttr;

			// Token: 0x040035C5 RID: 13765
			private readonly CmsAttributeTableGenerator unsAttr;

			// Token: 0x040035C6 RID: 13766
			private readonly BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable;
		}
	}
}
