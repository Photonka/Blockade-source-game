using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E9 RID: 1513
	public class CmsSignedDataStreamGenerator : CmsSignedGenerator
	{
		// Token: 0x060039DA RID: 14810 RVA: 0x0016B361 File Offset: 0x00169561
		public CmsSignedDataStreamGenerator()
		{
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x0016B395 File Offset: 0x00169595
		public CmsSignedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x0016B3CA File Offset: 0x001695CA
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x0016B3D3 File Offset: 0x001695D3
		public void AddDigests(params string[] digestOids)
		{
			this.AddDigests(digestOids);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x0016B3DC File Offset: 0x001695DC
		public void AddDigests(IEnumerable digestOids)
		{
			foreach (object obj in digestOids)
			{
				string digestOid = (string)obj;
				this.ConfigureDigest(digestOid);
			}
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x0016B430 File Offset: 0x00169630
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid)
		{
			this.AddSigner(privateKey, cert, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x0016B441 File Offset: 0x00169641
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid)
		{
			this.AddSigner(privateKey, cert, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x0016B454 File Offset: 0x00169654
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x0016B46D File Offset: 0x0016966D
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x0016B488 File Offset: 0x00169688
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.AddSigner(privateKey, cert, CmsSignedDataStreamGenerator.Helper.GetEncOid(privateKey, digestOid), digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x0016B4A3 File Offset: 0x001696A3
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.DoAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), encryptionOid, digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x0016B4B9 File Offset: 0x001696B9
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid)
		{
			this.AddSigner(privateKey, subjectKeyID, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x0016B4CA File Offset: 0x001696CA
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOid, string digestOid)
		{
			this.AddSigner(privateKey, subjectKeyID, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x0016B4DD File Offset: 0x001696DD
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, subjectKeyID, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x0016B4F6 File Offset: 0x001696F6
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.AddSigner(privateKey, subjectKeyID, CmsSignedDataStreamGenerator.Helper.GetEncOid(privateKey, digestOid), digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x0016B511 File Offset: 0x00169711
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOid, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.DoAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), encryptionOid, digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x0016B528 File Offset: 0x00169728
		private void DoAddSigner(AsymmetricKeyParameter privateKey, SignerIdentifier signerIdentifier, string encryptionOid, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.ConfigureDigest(digestOid);
			CmsSignedDataStreamGenerator.SignerInfoGeneratorImpl signerInf = new CmsSignedDataStreamGenerator.SignerInfoGeneratorImpl(this, privateKey, signerIdentifier, digestOid, encryptionOid, signedAttrGenerator, unsignedAttrGenerator);
			this._signerInfs.Add(new CmsSignedDataStreamGenerator.DigestAndSignerInfoGeneratorHolder(signerInf, digestOid));
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x0016B561 File Offset: 0x00169761
		internal override void AddSignerCallback(SignerInformation si)
		{
			this.RegisterDigestOid(si.DigestAlgorithmID.Algorithm.Id);
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x0016B579 File Offset: 0x00169779
		public Stream Open(Stream outStream)
		{
			return this.Open(outStream, false);
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x0016B583 File Offset: 0x00169783
		public Stream Open(Stream outStream, bool encapsulate)
		{
			return this.Open(outStream, CmsSignedGenerator.Data, encapsulate);
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x0016B592 File Offset: 0x00169792
		public Stream Open(Stream outStream, bool encapsulate, Stream dataOutputStream)
		{
			return this.Open(outStream, CmsSignedGenerator.Data, encapsulate, dataOutputStream);
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x0016B5A2 File Offset: 0x001697A2
		public Stream Open(Stream outStream, string signedContentType, bool encapsulate)
		{
			return this.Open(outStream, signedContentType, encapsulate, null);
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x0016B5B0 File Offset: 0x001697B0
		public Stream Open(Stream outStream, string signedContentType, bool encapsulate, Stream dataOutputStream)
		{
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			if (!outStream.CanWrite)
			{
				throw new ArgumentException("Expected writeable stream", "outStream");
			}
			if (dataOutputStream != null && !dataOutputStream.CanWrite)
			{
				throw new ArgumentException("Expected writeable stream", "dataOutputStream");
			}
			this._messageDigestsLocked = true;
			BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
			berSequenceGenerator.AddObject(CmsObjectIdentifiers.SignedData);
			BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
			DerObjectIdentifier derObjectIdentifier = (signedContentType == null) ? null : new DerObjectIdentifier(signedContentType);
			berSequenceGenerator2.AddObject(this.CalculateVersion(derObjectIdentifier));
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this._messageDigestOids)
			{
				string identifier = (string)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new AlgorithmIdentifier(new DerObjectIdentifier(identifier), DerNull.Instance)
				});
			}
			byte[] encoded = new DerSet(asn1EncodableVector).GetEncoded();
			berSequenceGenerator2.GetRawOutputStream().Write(encoded, 0, encoded.Length);
			BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(berSequenceGenerator2.GetRawOutputStream());
			berSequenceGenerator3.AddObject(derObjectIdentifier);
			Stream s = encapsulate ? CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, true, this._bufferSize) : null;
			Stream safeTeeOutputStream = CmsSignedDataStreamGenerator.GetSafeTeeOutputStream(dataOutputStream, s);
			Stream outStream2 = CmsSignedDataStreamGenerator.AttachDigestsToOutputStream(this._messageDigests.Values, safeTeeOutputStream);
			return new CmsSignedDataStreamGenerator.CmsSignedDataOutputStream(this, outStream2, signedContentType, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x0016B738 File Offset: 0x00169938
		private void RegisterDigestOid(string digestOid)
		{
			if (this._messageDigestsLocked)
			{
				if (!this._messageDigestOids.Contains(digestOid))
				{
					throw new InvalidOperationException("Cannot register new digest OIDs after the data stream is opened");
				}
			}
			else
			{
				this._messageDigestOids.Add(digestOid);
			}
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x0016B768 File Offset: 0x00169968
		private void ConfigureDigest(string digestOid)
		{
			this.RegisterDigestOid(digestOid);
			string digestAlgName = CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(digestOid);
			if ((IDigest)this._messageDigests[digestAlgName] == null)
			{
				if (this._messageDigestsLocked)
				{
					throw new InvalidOperationException("Cannot configure new digests after the data stream is opened");
				}
				IDigest digestInstance = CmsSignedDataStreamGenerator.Helper.GetDigestInstance(digestAlgName);
				this._messageDigests[digestAlgName] = digestInstance;
			}
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x0016B7CC File Offset: 0x001699CC
		internal void Generate(Stream outStream, string eContentType, bool encapsulate, Stream dataOutputStream, CmsProcessable content)
		{
			Stream stream = this.Open(outStream, eContentType, encapsulate, dataOutputStream);
			if (content != null)
			{
				content.Write(stream);
			}
			Platform.Dispose(stream);
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x0016B7F8 File Offset: 0x001699F8
		private DerInteger CalculateVersion(DerObjectIdentifier contentOid)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (this._certs != null)
			{
				foreach (object obj in this._certs)
				{
					if (obj is Asn1TaggedObject)
					{
						Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
						if (asn1TaggedObject.TagNo == 1)
						{
							flag3 = true;
						}
						else if (asn1TaggedObject.TagNo == 2)
						{
							flag4 = true;
						}
						else if (asn1TaggedObject.TagNo == 3)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag)
			{
				return new DerInteger(5);
			}
			if (this._crls != null)
			{
				using (IEnumerator enumerator = this._crls.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current is Asn1TaggedObject)
						{
							flag2 = true;
							break;
						}
					}
				}
			}
			if (flag2)
			{
				return new DerInteger(5);
			}
			if (flag4)
			{
				return new DerInteger(4);
			}
			if (flag3 || !CmsObjectIdentifiers.Data.Equals(contentOid) || this.CheckForVersion3(this._signers))
			{
				return new DerInteger(3);
			}
			return new DerInteger(1);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x0016B938 File Offset: 0x00169B38
		private bool CheckForVersion3(IList signerInfos)
		{
			using (IEnumerator enumerator = signerInfos.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (SignerInfo.GetInstance(((SignerInformation)enumerator.Current).ToSignerInfo()).Version.Value.IntValue == 3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x0016B9A8 File Offset: 0x00169BA8
		private static Stream AttachDigestsToOutputStream(ICollection digests, Stream s)
		{
			Stream stream = s;
			foreach (object obj in digests)
			{
				IDigest digest = (IDigest)obj;
				stream = CmsSignedDataStreamGenerator.GetSafeTeeOutputStream(stream, new DigestSink(digest));
			}
			return stream;
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x0016BA08 File Offset: 0x00169C08
		private static Stream GetSafeOutputStream(Stream s)
		{
			if (s == null)
			{
				return new NullOutputStream();
			}
			return s;
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x0016BA14 File Offset: 0x00169C14
		private static Stream GetSafeTeeOutputStream(Stream s1, Stream s2)
		{
			if (s1 == null)
			{
				return CmsSignedDataStreamGenerator.GetSafeOutputStream(s2);
			}
			if (s2 == null)
			{
				return CmsSignedDataStreamGenerator.GetSafeOutputStream(s1);
			}
			return new TeeOutputStream(s1, s2);
		}

		// Token: 0x040024E9 RID: 9449
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x040024EA RID: 9450
		private readonly IList _signerInfs = Platform.CreateArrayList();

		// Token: 0x040024EB RID: 9451
		private readonly ISet _messageDigestOids = new HashSet();

		// Token: 0x040024EC RID: 9452
		private readonly IDictionary _messageDigests = Platform.CreateHashtable();

		// Token: 0x040024ED RID: 9453
		private readonly IDictionary _messageHashes = Platform.CreateHashtable();

		// Token: 0x040024EE RID: 9454
		private bool _messageDigestsLocked;

		// Token: 0x040024EF RID: 9455
		private int _bufferSize;

		// Token: 0x0200095F RID: 2399
		private class DigestAndSignerInfoGeneratorHolder
		{
			// Token: 0x06004EFC RID: 20220 RVA: 0x001B719F File Offset: 0x001B539F
			internal DigestAndSignerInfoGeneratorHolder(ISignerInfoGenerator signerInf, string digestOID)
			{
				this.signerInf = signerInf;
				this.digestOID = digestOID;
			}

			// Token: 0x17000C52 RID: 3154
			// (get) Token: 0x06004EFD RID: 20221 RVA: 0x001B71B5 File Offset: 0x001B53B5
			internal AlgorithmIdentifier DigestAlgorithm
			{
				get
				{
					return new AlgorithmIdentifier(new DerObjectIdentifier(this.digestOID), DerNull.Instance);
				}
			}

			// Token: 0x040035C7 RID: 13767
			internal readonly ISignerInfoGenerator signerInf;

			// Token: 0x040035C8 RID: 13768
			internal readonly string digestOID;
		}

		// Token: 0x02000960 RID: 2400
		private class SignerInfoGeneratorImpl : ISignerInfoGenerator
		{
			// Token: 0x06004EFE RID: 20222 RVA: 0x001B71CC File Offset: 0x001B53CC
			internal SignerInfoGeneratorImpl(CmsSignedDataStreamGenerator outer, AsymmetricKeyParameter key, SignerIdentifier signerIdentifier, string digestOID, string encOID, CmsAttributeTableGenerator sAttr, CmsAttributeTableGenerator unsAttr)
			{
				this.outer = outer;
				this._signerIdentifier = signerIdentifier;
				this._digestOID = digestOID;
				this._encOID = encOID;
				this._sAttr = sAttr;
				this._unsAttr = unsAttr;
				this._encName = CmsSignedDataStreamGenerator.Helper.GetEncryptionAlgName(this._encOID);
				string algorithm = CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(this._digestOID) + "with" + this._encName;
				if (this._sAttr != null)
				{
					this._sig = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance(algorithm);
				}
				else if (this._encName.Equals("RSA"))
				{
					this._sig = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance("RSA");
				}
				else
				{
					if (!this._encName.Equals("DSA"))
					{
						throw new SignatureException("algorithm: " + this._encName + " not supported in base signatures.");
					}
					this._sig = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance("NONEwithDSA");
				}
				this._sig.Init(true, new ParametersWithRandom(key, outer.rand));
			}

			// Token: 0x06004EFF RID: 20223 RVA: 0x001B72E4 File Offset: 0x001B54E4
			public SignerInfo Generate(DerObjectIdentifier contentType, AlgorithmIdentifier digestAlgorithm, byte[] calculatedDigest)
			{
				SignerInfo result;
				try
				{
					string algorithm = CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(this._digestOID) + "with" + this._encName;
					byte[] array = calculatedDigest;
					Asn1Set asn1Set = null;
					if (this._sAttr != null)
					{
						IDictionary baseParameters = this.outer.GetBaseParameters(contentType, digestAlgorithm, calculatedDigest);
						BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable attributeTable = this._sAttr.GetAttributes(baseParameters);
						if (contentType == null && attributeTable != null && attributeTable[CmsAttributes.ContentType] != null)
						{
							IDictionary dictionary = attributeTable.ToDictionary();
							dictionary.Remove(CmsAttributes.ContentType);
							attributeTable = new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(dictionary);
						}
						asn1Set = this.outer.GetAttributeSet(attributeTable);
						array = asn1Set.GetEncoded("DER");
					}
					else if (this._encName.Equals("RSA"))
					{
						array = new DigestInfo(digestAlgorithm, calculatedDigest).GetEncoded("DER");
					}
					this._sig.BlockUpdate(array, 0, array.Length);
					byte[] array2 = this._sig.GenerateSignature();
					Asn1Set unauthenticatedAttributes = null;
					if (this._unsAttr != null)
					{
						IDictionary baseParameters2 = this.outer.GetBaseParameters(contentType, digestAlgorithm, calculatedDigest);
						baseParameters2[CmsAttributeTableParameter.Signature] = array2.Clone();
						BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this._unsAttr.GetAttributes(baseParameters2);
						unauthenticatedAttributes = this.outer.GetAttributeSet(attributes);
					}
					Asn1Encodable defaultX509Parameters = SignerUtilities.GetDefaultX509Parameters(algorithm);
					AlgorithmIdentifier encAlgorithmIdentifier = CmsSignedDataStreamGenerator.Helper.GetEncAlgorithmIdentifier(new DerObjectIdentifier(this._encOID), defaultX509Parameters);
					result = new SignerInfo(this._signerIdentifier, digestAlgorithm, asn1Set, encAlgorithmIdentifier, new DerOctetString(array2), unauthenticatedAttributes);
				}
				catch (IOException e)
				{
					throw new CmsStreamException("encoding error.", e);
				}
				catch (SignatureException e2)
				{
					throw new CmsStreamException("error creating signature.", e2);
				}
				return result;
			}

			// Token: 0x040035C9 RID: 13769
			private readonly CmsSignedDataStreamGenerator outer;

			// Token: 0x040035CA RID: 13770
			private readonly SignerIdentifier _signerIdentifier;

			// Token: 0x040035CB RID: 13771
			private readonly string _digestOID;

			// Token: 0x040035CC RID: 13772
			private readonly string _encOID;

			// Token: 0x040035CD RID: 13773
			private readonly CmsAttributeTableGenerator _sAttr;

			// Token: 0x040035CE RID: 13774
			private readonly CmsAttributeTableGenerator _unsAttr;

			// Token: 0x040035CF RID: 13775
			private readonly string _encName;

			// Token: 0x040035D0 RID: 13776
			private readonly ISigner _sig;
		}

		// Token: 0x02000961 RID: 2401
		private class CmsSignedDataOutputStream : BaseOutputStream
		{
			// Token: 0x06004F00 RID: 20224 RVA: 0x001B74A0 File Offset: 0x001B56A0
			public CmsSignedDataOutputStream(CmsSignedDataStreamGenerator outer, Stream outStream, string contentOID, BerSequenceGenerator sGen, BerSequenceGenerator sigGen, BerSequenceGenerator eiGen)
			{
				this.outer = outer;
				this._out = outStream;
				this._contentOID = new DerObjectIdentifier(contentOID);
				this._sGen = sGen;
				this._sigGen = sigGen;
				this._eiGen = eiGen;
			}

			// Token: 0x06004F01 RID: 20225 RVA: 0x001B74DA File Offset: 0x001B56DA
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x06004F02 RID: 20226 RVA: 0x001B74E8 File Offset: 0x001B56E8
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x06004F03 RID: 20227 RVA: 0x001B74F8 File Offset: 0x001B56F8
			public override void Close()
			{
				this.DoClose();
				base.Close();
			}

			// Token: 0x06004F04 RID: 20228 RVA: 0x001B7508 File Offset: 0x001B5708
			private void DoClose()
			{
				Platform.Dispose(this._out);
				this._eiGen.Close();
				this.outer._digests.Clear();
				if (this.outer._certs.Count > 0)
				{
					Asn1Set obj = this.outer.UseDerForCerts ? CmsUtilities.CreateDerSetFromList(this.outer._certs) : CmsUtilities.CreateBerSetFromList(this.outer._certs);
					CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new BerTaggedObject(false, 0, obj));
				}
				if (this.outer._crls.Count > 0)
				{
					Asn1Set obj2 = this.outer.UseDerForCrls ? CmsUtilities.CreateDerSetFromList(this.outer._crls) : CmsUtilities.CreateBerSetFromList(this.outer._crls);
					CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new BerTaggedObject(false, 1, obj2));
				}
				foreach (object obj3 in this.outer._messageDigests)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					this.outer._messageHashes.Add(dictionaryEntry.Key, DigestUtilities.DoFinal((IDigest)dictionaryEntry.Value));
				}
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
				foreach (object obj4 in this.outer._signerInfs)
				{
					CmsSignedDataStreamGenerator.DigestAndSignerInfoGeneratorHolder digestAndSignerInfoGeneratorHolder = (CmsSignedDataStreamGenerator.DigestAndSignerInfoGeneratorHolder)obj4;
					AlgorithmIdentifier digestAlgorithm = digestAndSignerInfoGeneratorHolder.DigestAlgorithm;
					byte[] array = (byte[])this.outer._messageHashes[CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(digestAndSignerInfoGeneratorHolder.digestOID)];
					this.outer._digests[digestAndSignerInfoGeneratorHolder.digestOID] = array.Clone();
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						digestAndSignerInfoGeneratorHolder.signerInf.Generate(this._contentOID, digestAlgorithm, array)
					});
				}
				foreach (object obj5 in this.outer._signers)
				{
					SignerInformation signerInformation = (SignerInformation)obj5;
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						signerInformation.ToSignerInfo()
					});
				}
				CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new DerSet(asn1EncodableVector));
				this._sigGen.Close();
				this._sGen.Close();
			}

			// Token: 0x06004F05 RID: 20229 RVA: 0x001B77BC File Offset: 0x001B59BC
			private static void WriteToGenerator(Asn1Generator ag, Asn1Encodable ae)
			{
				byte[] encoded = ae.GetEncoded();
				ag.GetRawOutputStream().Write(encoded, 0, encoded.Length);
			}

			// Token: 0x040035D1 RID: 13777
			private readonly CmsSignedDataStreamGenerator outer;

			// Token: 0x040035D2 RID: 13778
			private Stream _out;

			// Token: 0x040035D3 RID: 13779
			private DerObjectIdentifier _contentOID;

			// Token: 0x040035D4 RID: 13780
			private BerSequenceGenerator _sGen;

			// Token: 0x040035D5 RID: 13781
			private BerSequenceGenerator _sigGen;

			// Token: 0x040035D6 RID: 13782
			private BerSequenceGenerator _eiGen;
		}
	}
}
