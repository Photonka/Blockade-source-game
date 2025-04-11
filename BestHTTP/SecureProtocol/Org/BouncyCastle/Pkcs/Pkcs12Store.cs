using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs
{
	// Token: 0x020002B5 RID: 693
	public class Pkcs12Store
	{
		// Token: 0x060019DF RID: 6623 RVA: 0x000C6527 File Offset: 0x000C4727
		private static SubjectKeyIdentifier CreateSubjectKeyID(AsymmetricKeyParameter pubKey)
		{
			return new SubjectKeyIdentifier(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey));
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000C6534 File Offset: 0x000C4734
		internal Pkcs12Store(DerObjectIdentifier keyAlgorithm, DerObjectIdentifier certAlgorithm, bool useDerEncoding)
		{
			this.keyAlgorithm = keyAlgorithm;
			this.certAlgorithm = certAlgorithm;
			this.useDerEncoding = useDerEncoding;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000C6593 File Offset: 0x000C4793
		public Pkcs12Store() : this(PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc, PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc, false)
		{
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000C65A6 File Offset: 0x000C47A6
		public Pkcs12Store(Stream input, char[] password) : this()
		{
			this.Load(input, password);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000C65B8 File Offset: 0x000C47B8
		protected virtual void LoadKeyBag(PrivateKeyInfo privKeyInfo, Asn1Set bagAttributes)
		{
			AsymmetricKeyParameter key = PrivateKeyFactory.CreateKey(privKeyInfo);
			IDictionary dictionary = Platform.CreateHashtable();
			AsymmetricKeyEntry value = new AsymmetricKeyEntry(key, dictionary);
			string text = null;
			Asn1OctetString asn1OctetString = null;
			if (bagAttributes != null)
			{
				foreach (object obj in bagAttributes)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)obj;
					DerObjectIdentifier instance = DerObjectIdentifier.GetInstance(asn1Sequence[0]);
					Asn1Set instance2 = Asn1Set.GetInstance(asn1Sequence[1]);
					if (instance2.Count > 0)
					{
						Asn1Encodable asn1Encodable = instance2[0];
						if (dictionary.Contains(instance.Id))
						{
							if (!dictionary[instance.Id].Equals(asn1Encodable))
							{
								throw new IOException("attempt to add existing attribute with different value");
							}
						}
						else
						{
							dictionary.Add(instance.Id, asn1Encodable);
						}
						if (instance.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName))
						{
							text = ((DerBmpString)asn1Encodable).GetString();
							this.keys[text] = value;
						}
						else if (instance.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID))
						{
							asn1OctetString = (Asn1OctetString)asn1Encodable;
						}
					}
				}
			}
			if (asn1OctetString == null)
			{
				this.unmarkedKeyEntry = value;
				return;
			}
			string text2 = Hex.ToHexString(asn1OctetString.GetOctets());
			if (text == null)
			{
				this.keys[text2] = value;
				return;
			}
			this.localIds[text] = text2;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000C671C File Offset: 0x000C491C
		protected virtual void LoadPkcs8ShroudedKeyBag(EncryptedPrivateKeyInfo encPrivKeyInfo, Asn1Set bagAttributes, char[] password, bool wrongPkcs12Zero)
		{
			if (password != null)
			{
				PrivateKeyInfo privKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(password, wrongPkcs12Zero, encPrivKeyInfo);
				this.LoadKeyBag(privKeyInfo, bagAttributes);
			}
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000C6740 File Offset: 0x000C4940
		public void Load(Stream input, char[] password)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			Pfx pfx = new Pfx((Asn1Sequence)Asn1Object.FromStream(input));
			ContentInfo authSafe = pfx.AuthSafe;
			bool wrongPkcs12Zero = false;
			if (password != null && pfx.MacData != null)
			{
				MacData macData = pfx.MacData;
				DigestInfo mac = macData.Mac;
				AlgorithmIdentifier algorithmID = mac.AlgorithmID;
				byte[] salt = macData.GetSalt();
				int intValue = macData.IterationCount.IntValue;
				byte[] octets = ((Asn1OctetString)authSafe.Content).GetOctets();
				byte[] a = Pkcs12Store.CalculatePbeMac(algorithmID.Algorithm, salt, intValue, password, false, octets);
				byte[] digest = mac.GetDigest();
				if (!Arrays.ConstantTimeAreEqual(a, digest))
				{
					if (password.Length != 0)
					{
						throw new IOException("PKCS12 key store MAC invalid - wrong password or corrupted file.");
					}
					if (!Arrays.ConstantTimeAreEqual(Pkcs12Store.CalculatePbeMac(algorithmID.Algorithm, salt, intValue, password, true, octets), digest))
					{
						throw new IOException("PKCS12 key store MAC invalid - wrong password or corrupted file.");
					}
					wrongPkcs12Zero = true;
				}
			}
			this.keys.Clear();
			this.localIds.Clear();
			this.unmarkedKeyEntry = null;
			IList list = Platform.CreateArrayList();
			if (authSafe.ContentType.Equals(PkcsObjectIdentifiers.Data))
			{
				foreach (ContentInfo contentInfo2 in new AuthenticatedSafe((Asn1Sequence)Asn1Object.FromByteArray(((Asn1OctetString)authSafe.Content).GetOctets())).GetContentInfo())
				{
					DerObjectIdentifier contentType = contentInfo2.ContentType;
					byte[] array = null;
					if (contentType.Equals(PkcsObjectIdentifiers.Data))
					{
						array = ((Asn1OctetString)contentInfo2.Content).GetOctets();
					}
					else if (contentType.Equals(PkcsObjectIdentifiers.EncryptedData) && password != null)
					{
						EncryptedData instance = EncryptedData.GetInstance(contentInfo2.Content);
						array = Pkcs12Store.CryptPbeData(false, instance.EncryptionAlgorithm, password, wrongPkcs12Zero, instance.Content.GetOctets());
					}
					if (array != null)
					{
						foreach (object obj in ((Asn1Sequence)Asn1Object.FromByteArray(array)))
						{
							SafeBag safeBag = new SafeBag((Asn1Sequence)obj);
							if (safeBag.BagID.Equals(PkcsObjectIdentifiers.CertBag))
							{
								list.Add(safeBag);
							}
							else if (safeBag.BagID.Equals(PkcsObjectIdentifiers.Pkcs8ShroudedKeyBag))
							{
								this.LoadPkcs8ShroudedKeyBag(EncryptedPrivateKeyInfo.GetInstance(safeBag.BagValue), safeBag.BagAttributes, password, wrongPkcs12Zero);
							}
							else if (safeBag.BagID.Equals(PkcsObjectIdentifiers.KeyBag))
							{
								this.LoadKeyBag(PrivateKeyInfo.GetInstance(safeBag.BagValue), safeBag.BagAttributes);
							}
						}
					}
				}
			}
			this.certs.Clear();
			this.chainCerts.Clear();
			this.keyCerts.Clear();
			foreach (object obj2 in list)
			{
				SafeBag safeBag2 = (SafeBag)obj2;
				byte[] octets2 = ((Asn1OctetString)new CertBag((Asn1Sequence)safeBag2.BagValue).CertValue).GetOctets();
				X509Certificate x509Certificate = new X509CertificateParser().ReadCertificate(octets2);
				IDictionary dictionary = Platform.CreateHashtable();
				Asn1OctetString asn1OctetString = null;
				string text = null;
				if (safeBag2.BagAttributes != null)
				{
					foreach (object obj3 in safeBag2.BagAttributes)
					{
						Asn1Sequence asn1Sequence = (Asn1Sequence)obj3;
						DerObjectIdentifier instance2 = DerObjectIdentifier.GetInstance(asn1Sequence[0]);
						Asn1Set instance3 = Asn1Set.GetInstance(asn1Sequence[1]);
						if (instance3.Count > 0)
						{
							Asn1Encodable asn1Encodable = instance3[0];
							if (dictionary.Contains(instance2.Id))
							{
								if (!dictionary[instance2.Id].Equals(asn1Encodable))
								{
									throw new IOException("attempt to add existing attribute with different value");
								}
							}
							else
							{
								dictionary.Add(instance2.Id, asn1Encodable);
							}
							if (instance2.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName))
							{
								text = ((DerBmpString)asn1Encodable).GetString();
							}
							else if (instance2.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID))
							{
								asn1OctetString = (Asn1OctetString)asn1Encodable;
							}
						}
					}
				}
				Pkcs12Store.CertId certId = new Pkcs12Store.CertId(x509Certificate.GetPublicKey());
				X509CertificateEntry value = new X509CertificateEntry(x509Certificate, dictionary);
				this.chainCerts[certId] = value;
				if (this.unmarkedKeyEntry != null)
				{
					if (this.keyCerts.Count == 0)
					{
						string text2 = Hex.ToHexString(certId.Id);
						this.keyCerts[text2] = value;
						this.keys[text2] = this.unmarkedKeyEntry;
					}
					else
					{
						this.keys["unmarked"] = this.unmarkedKeyEntry;
					}
				}
				else
				{
					if (asn1OctetString != null)
					{
						string key = Hex.ToHexString(asn1OctetString.GetOctets());
						this.keyCerts[key] = value;
					}
					if (text != null)
					{
						this.certs[text] = value;
					}
				}
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000C6C74 File Offset: 0x000C4E74
		public AsymmetricKeyEntry GetKey(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return (AsymmetricKeyEntry)this.keys[alias];
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000C6C95 File Offset: 0x000C4E95
		public bool IsCertificateEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return this.certs[alias] != null && this.keys[alias] == null;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000C6CC4 File Offset: 0x000C4EC4
		public bool IsKeyEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return this.keys[alias] != null;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000C6CE4 File Offset: 0x000C4EE4
		private IDictionary GetAliasesTable()
		{
			IDictionary dictionary = Platform.CreateHashtable();
			foreach (object obj in this.certs.Keys)
			{
				string key = (string)obj;
				dictionary[key] = "cert";
			}
			foreach (object obj2 in this.keys.Keys)
			{
				string key2 = (string)obj2;
				if (dictionary[key2] == null)
				{
					dictionary[key2] = "key";
				}
			}
			return dictionary;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x000C6DAC File Offset: 0x000C4FAC
		public IEnumerable Aliases
		{
			get
			{
				return new EnumerableProxy(this.GetAliasesTable().Keys);
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000C6DBE File Offset: 0x000C4FBE
		public bool ContainsAlias(string alias)
		{
			return this.certs[alias] != null || this.keys[alias] != null;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000C6DE0 File Offset: 0x000C4FE0
		public X509CertificateEntry GetCertificate(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[alias];
			if (x509CertificateEntry == null)
			{
				string text = (string)this.localIds[alias];
				if (text != null)
				{
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[text];
				}
				else
				{
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[alias];
				}
			}
			return x509CertificateEntry;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000C6E4C File Offset: 0x000C504C
		public string GetCertificateAlias(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			foreach (object obj in this.certs)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (((X509CertificateEntry)dictionaryEntry.Value).Certificate.Equals(cert))
				{
					return (string)dictionaryEntry.Key;
				}
			}
			foreach (object obj2 in this.keyCerts)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				if (((X509CertificateEntry)dictionaryEntry2.Value).Certificate.Equals(cert))
				{
					return (string)dictionaryEntry2.Key;
				}
			}
			return null;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000C6F48 File Offset: 0x000C5148
		public X509CertificateEntry[] GetCertificateChain(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (!this.IsKeyEntry(alias))
			{
				return null;
			}
			X509CertificateEntry x509CertificateEntry = this.GetCertificate(alias);
			if (x509CertificateEntry != null)
			{
				IList list = Platform.CreateArrayList();
				while (x509CertificateEntry != null)
				{
					X509Certificate certificate = x509CertificateEntry.Certificate;
					X509CertificateEntry x509CertificateEntry2 = null;
					Asn1OctetString extensionValue = certificate.GetExtensionValue(X509Extensions.AuthorityKeyIdentifier);
					if (extensionValue != null)
					{
						AuthorityKeyIdentifier instance = AuthorityKeyIdentifier.GetInstance(Asn1Object.FromByteArray(extensionValue.GetOctets()));
						if (instance.GetKeyIdentifier() != null)
						{
							x509CertificateEntry2 = (X509CertificateEntry)this.chainCerts[new Pkcs12Store.CertId(instance.GetKeyIdentifier())];
						}
					}
					if (x509CertificateEntry2 == null)
					{
						X509Name issuerDN = certificate.IssuerDN;
						X509Name subjectDN = certificate.SubjectDN;
						if (!issuerDN.Equivalent(subjectDN))
						{
							foreach (object obj in this.chainCerts.Keys)
							{
								Pkcs12Store.CertId key = (Pkcs12Store.CertId)obj;
								X509CertificateEntry x509CertificateEntry3 = (X509CertificateEntry)this.chainCerts[key];
								X509Certificate certificate2 = x509CertificateEntry3.Certificate;
								if (certificate2.SubjectDN.Equivalent(issuerDN))
								{
									try
									{
										certificate.Verify(certificate2.GetPublicKey());
										x509CertificateEntry2 = x509CertificateEntry3;
										break;
									}
									catch (InvalidKeyException)
									{
									}
								}
							}
						}
					}
					list.Add(x509CertificateEntry);
					if (x509CertificateEntry2 != x509CertificateEntry)
					{
						x509CertificateEntry = x509CertificateEntry2;
					}
					else
					{
						x509CertificateEntry = null;
					}
				}
				X509CertificateEntry[] array = new X509CertificateEntry[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = (X509CertificateEntry)list[i];
				}
				return array;
			}
			return null;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000C70F4 File Offset: 0x000C52F4
		public void SetCertificateEntry(string alias, X509CertificateEntry certEntry)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (certEntry == null)
			{
				throw new ArgumentNullException("certEntry");
			}
			if (this.keys[alias] != null)
			{
				throw new ArgumentException("There is a key entry with the name " + alias + ".");
			}
			this.certs[alias] = certEntry;
			this.chainCerts[new Pkcs12Store.CertId(certEntry.Certificate.GetPublicKey())] = certEntry;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000C716C File Offset: 0x000C536C
		public void SetKeyEntry(string alias, AsymmetricKeyEntry keyEntry, X509CertificateEntry[] chain)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (keyEntry == null)
			{
				throw new ArgumentNullException("keyEntry");
			}
			if (keyEntry.Key.IsPrivate && chain == null)
			{
				throw new ArgumentException("No certificate chain for private key");
			}
			if (this.keys[alias] != null)
			{
				this.DeleteEntry(alias);
			}
			this.keys[alias] = keyEntry;
			this.certs[alias] = chain[0];
			for (int num = 0; num != chain.Length; num++)
			{
				this.chainCerts[new Pkcs12Store.CertId(chain[num].Certificate.GetPublicKey())] = chain[num];
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000C7210 File Offset: 0x000C5410
		public void DeleteEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			AsymmetricKeyEntry asymmetricKeyEntry = (AsymmetricKeyEntry)this.keys[alias];
			if (asymmetricKeyEntry != null)
			{
				this.keys.Remove(alias);
			}
			X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[alias];
			if (x509CertificateEntry != null)
			{
				this.certs.Remove(alias);
				this.chainCerts.Remove(new Pkcs12Store.CertId(x509CertificateEntry.Certificate.GetPublicKey()));
			}
			if (asymmetricKeyEntry != null)
			{
				string text = (string)this.localIds[alias];
				if (text != null)
				{
					this.localIds.Remove(alias);
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[text];
				}
				if (x509CertificateEntry != null)
				{
					this.keyCerts.Remove(text);
					this.chainCerts.Remove(new Pkcs12Store.CertId(x509CertificateEntry.Certificate.GetPublicKey()));
				}
			}
			if (x509CertificateEntry == null && asymmetricKeyEntry == null)
			{
				throw new ArgumentException("no such entry as " + alias);
			}
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000C7304 File Offset: 0x000C5504
		public bool IsEntryOfType(string alias, Type entryType)
		{
			if (entryType == typeof(X509CertificateEntry))
			{
				return this.IsCertificateEntry(alias);
			}
			return entryType == typeof(AsymmetricKeyEntry) && this.IsKeyEntry(alias) && this.GetCertificate(alias) != null;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000C7354 File Offset: 0x000C5554
		[Obsolete("Use 'Count' property instead")]
		public int Size()
		{
			return this.Count;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x000C735C File Offset: 0x000C555C
		public int Count
		{
			get
			{
				return this.GetAliasesTable().Count;
			}
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000C736C File Offset: 0x000C556C
		public void Save(Stream stream, char[] password, SecureRandom random)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this.keys.Keys)
			{
				string text = (string)obj;
				byte[] array = new byte[20];
				random.NextBytes(array);
				AsymmetricKeyEntry asymmetricKeyEntry = (AsymmetricKeyEntry)this.keys[text];
				DerObjectIdentifier oid;
				Asn1Encodable asn1Encodable;
				if (password == null)
				{
					oid = PkcsObjectIdentifiers.KeyBag;
					asn1Encodable = PrivateKeyInfoFactory.CreatePrivateKeyInfo(asymmetricKeyEntry.Key);
				}
				else
				{
					oid = PkcsObjectIdentifiers.Pkcs8ShroudedKeyBag;
					asn1Encodable = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(this.keyAlgorithm, password, array, 1024, asymmetricKeyEntry.Key);
				}
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
				foreach (object obj2 in asymmetricKeyEntry.BagAttributeKeys)
				{
					string text2 = (string)obj2;
					Asn1Encodable obj3 = asymmetricKeyEntry[text2];
					if (!text2.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
					{
						asn1EncodableVector2.Add(new Asn1Encodable[]
						{
							new DerSequence(new Asn1Encodable[]
							{
								new DerObjectIdentifier(text2),
								new DerSet(obj3)
							})
						});
					}
				}
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					new DerSequence(new Asn1Encodable[]
					{
						PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
						new DerSet(new DerBmpString(text))
					})
				});
				if (asymmetricKeyEntry[PkcsObjectIdentifiers.Pkcs9AtLocalKeyID] == null)
				{
					SubjectKeyIdentifier obj4 = Pkcs12Store.CreateSubjectKeyID(this.GetCertificate(text).Certificate.GetPublicKey());
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						new DerSequence(new Asn1Encodable[]
						{
							PkcsObjectIdentifiers.Pkcs9AtLocalKeyID,
							new DerSet(obj4)
						})
					});
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new SafeBag(oid, asn1Encodable.ToAsn1Object(), new DerSet(asn1EncodableVector2))
				});
			}
			byte[] derEncoded = new DerSequence(asn1EncodableVector).GetDerEncoded();
			ContentInfo contentInfo = new ContentInfo(PkcsObjectIdentifiers.Data, new BerOctetString(derEncoded));
			byte[] array2 = new byte[20];
			random.NextBytes(array2);
			Asn1EncodableVector asn1EncodableVector3 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			Pkcs12PbeParams pkcs12PbeParams = new Pkcs12PbeParams(array2, 1024);
			AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier(this.certAlgorithm, pkcs12PbeParams.ToAsn1Object());
			ISet set = new HashSet();
			foreach (object obj5 in this.keys.Keys)
			{
				string text3 = (string)obj5;
				X509CertificateEntry certificate = this.GetCertificate(text3);
				CertBag certBag = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(certificate.Certificate.GetEncoded()));
				Asn1EncodableVector asn1EncodableVector4 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
				foreach (object obj6 in certificate.BagAttributeKeys)
				{
					string text4 = (string)obj6;
					Asn1Encodable obj7 = certificate[text4];
					if (!text4.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
					{
						asn1EncodableVector4.Add(new Asn1Encodable[]
						{
							new DerSequence(new Asn1Encodable[]
							{
								new DerObjectIdentifier(text4),
								new DerSet(obj7)
							})
						});
					}
				}
				asn1EncodableVector4.Add(new Asn1Encodable[]
				{
					new DerSequence(new Asn1Encodable[]
					{
						PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
						new DerSet(new DerBmpString(text3))
					})
				});
				if (certificate[PkcsObjectIdentifiers.Pkcs9AtLocalKeyID] == null)
				{
					SubjectKeyIdentifier obj8 = Pkcs12Store.CreateSubjectKeyID(certificate.Certificate.GetPublicKey());
					asn1EncodableVector4.Add(new Asn1Encodable[]
					{
						new DerSequence(new Asn1Encodable[]
						{
							PkcsObjectIdentifiers.Pkcs9AtLocalKeyID,
							new DerSet(obj8)
						})
					});
				}
				asn1EncodableVector3.Add(new Asn1Encodable[]
				{
					new SafeBag(PkcsObjectIdentifiers.CertBag, certBag.ToAsn1Object(), new DerSet(asn1EncodableVector4))
				});
				set.Add(certificate.Certificate);
			}
			foreach (object obj9 in this.certs.Keys)
			{
				string text5 = (string)obj9;
				X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[text5];
				if (this.keys[text5] == null)
				{
					CertBag certBag2 = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(x509CertificateEntry.Certificate.GetEncoded()));
					Asn1EncodableVector asn1EncodableVector5 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
					foreach (object obj10 in x509CertificateEntry.BagAttributeKeys)
					{
						string text6 = (string)obj10;
						if (!text6.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID.Id))
						{
							Asn1Encodable obj11 = x509CertificateEntry[text6];
							if (!text6.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
							{
								asn1EncodableVector5.Add(new Asn1Encodable[]
								{
									new DerSequence(new Asn1Encodable[]
									{
										new DerObjectIdentifier(text6),
										new DerSet(obj11)
									})
								});
							}
						}
					}
					asn1EncodableVector5.Add(new Asn1Encodable[]
					{
						new DerSequence(new Asn1Encodable[]
						{
							PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
							new DerSet(new DerBmpString(text5))
						})
					});
					asn1EncodableVector3.Add(new Asn1Encodable[]
					{
						new SafeBag(PkcsObjectIdentifiers.CertBag, certBag2.ToAsn1Object(), new DerSet(asn1EncodableVector5))
					});
					set.Add(x509CertificateEntry.Certificate);
				}
			}
			foreach (object obj12 in this.chainCerts.Keys)
			{
				Pkcs12Store.CertId key = (Pkcs12Store.CertId)obj12;
				X509CertificateEntry x509CertificateEntry2 = (X509CertificateEntry)this.chainCerts[key];
				if (!set.Contains(x509CertificateEntry2.Certificate))
				{
					CertBag certBag3 = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(x509CertificateEntry2.Certificate.GetEncoded()));
					Asn1EncodableVector asn1EncodableVector6 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
					foreach (object obj13 in x509CertificateEntry2.BagAttributeKeys)
					{
						string text7 = (string)obj13;
						if (!text7.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID.Id))
						{
							asn1EncodableVector6.Add(new Asn1Encodable[]
							{
								new DerSequence(new Asn1Encodable[]
								{
									new DerObjectIdentifier(text7),
									new DerSet(x509CertificateEntry2[text7])
								})
							});
						}
					}
					asn1EncodableVector3.Add(new Asn1Encodable[]
					{
						new SafeBag(PkcsObjectIdentifiers.CertBag, certBag3.ToAsn1Object(), new DerSet(asn1EncodableVector6))
					});
				}
			}
			byte[] derEncoded2 = new DerSequence(asn1EncodableVector3).GetDerEncoded();
			ContentInfo contentInfo2;
			if (password == null)
			{
				contentInfo2 = new ContentInfo(PkcsObjectIdentifiers.Data, new BerOctetString(derEncoded2));
			}
			else
			{
				byte[] str = Pkcs12Store.CryptPbeData(true, algorithmIdentifier, password, false, derEncoded2);
				EncryptedData encryptedData = new EncryptedData(PkcsObjectIdentifiers.Data, algorithmIdentifier, new BerOctetString(str));
				contentInfo2 = new ContentInfo(PkcsObjectIdentifiers.EncryptedData, encryptedData.ToAsn1Object());
			}
			byte[] encoded = new AuthenticatedSafe(new ContentInfo[]
			{
				contentInfo,
				contentInfo2
			}).GetEncoded(this.useDerEncoding ? "DER" : "BER");
			ContentInfo contentInfo3 = new ContentInfo(PkcsObjectIdentifiers.Data, new BerOctetString(encoded));
			MacData macData = null;
			if (password != null)
			{
				byte[] array3 = new byte[20];
				random.NextBytes(array3);
				byte[] digest = Pkcs12Store.CalculatePbeMac(OiwObjectIdentifiers.IdSha1, array3, 1024, password, false, encoded);
				macData = new MacData(new DigestInfo(new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance), digest), array3, 1024);
			}
			Pfx obj14 = new Pfx(contentInfo3, macData);
			DerOutputStream derOutputStream;
			if (this.useDerEncoding)
			{
				derOutputStream = new DerOutputStream(stream);
			}
			else
			{
				derOutputStream = new BerOutputStream(stream);
			}
			derOutputStream.WriteObject(obj14);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000C7C6C File Offset: 0x000C5E6C
		internal static byte[] CalculatePbeMac(DerObjectIdentifier oid, byte[] salt, int itCount, char[] password, bool wrongPkcs12Zero, byte[] data)
		{
			Asn1Encodable pbeParameters = PbeUtilities.GenerateAlgorithmParameters(oid, salt, itCount);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(oid, password, wrongPkcs12Zero, pbeParameters);
			IMac mac = (IMac)PbeUtilities.CreateEngine(oid);
			mac.Init(parameters);
			return MacUtilities.DoFinal(mac, data);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000C7CA8 File Offset: 0x000C5EA8
		private static byte[] CryptPbeData(bool forEncryption, AlgorithmIdentifier algId, char[] password, bool wrongPkcs12Zero, byte[] data)
		{
			IBufferedCipher bufferedCipher = PbeUtilities.CreateEngine(algId.Algorithm) as IBufferedCipher;
			if (bufferedCipher == null)
			{
				throw new Exception("Unknown encryption algorithm: " + algId.Algorithm);
			}
			Pkcs12PbeParams instance = Pkcs12PbeParams.GetInstance(algId.Parameters);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(algId.Algorithm, password, wrongPkcs12Zero, instance);
			bufferedCipher.Init(forEncryption, parameters);
			return bufferedCipher.DoFinal(data);
		}

		// Token: 0x04001781 RID: 6017
		private readonly Pkcs12Store.IgnoresCaseHashtable keys = new Pkcs12Store.IgnoresCaseHashtable();

		// Token: 0x04001782 RID: 6018
		private readonly IDictionary localIds = Platform.CreateHashtable();

		// Token: 0x04001783 RID: 6019
		private readonly Pkcs12Store.IgnoresCaseHashtable certs = new Pkcs12Store.IgnoresCaseHashtable();

		// Token: 0x04001784 RID: 6020
		private readonly IDictionary chainCerts = Platform.CreateHashtable();

		// Token: 0x04001785 RID: 6021
		private readonly IDictionary keyCerts = Platform.CreateHashtable();

		// Token: 0x04001786 RID: 6022
		private readonly DerObjectIdentifier keyAlgorithm;

		// Token: 0x04001787 RID: 6023
		private readonly DerObjectIdentifier certAlgorithm;

		// Token: 0x04001788 RID: 6024
		private readonly bool useDerEncoding;

		// Token: 0x04001789 RID: 6025
		private AsymmetricKeyEntry unmarkedKeyEntry;

		// Token: 0x0400178A RID: 6026
		private const int MinIterations = 1024;

		// Token: 0x0400178B RID: 6027
		private const int SaltSize = 20;

		// Token: 0x020008D9 RID: 2265
		internal class CertId
		{
			// Token: 0x06004D63 RID: 19811 RVA: 0x001B0A34 File Offset: 0x001AEC34
			internal CertId(AsymmetricKeyParameter pubKey)
			{
				this.id = Pkcs12Store.CreateSubjectKeyID(pubKey).GetKeyIdentifier();
			}

			// Token: 0x06004D64 RID: 19812 RVA: 0x001B0A4D File Offset: 0x001AEC4D
			internal CertId(byte[] id)
			{
				this.id = id;
			}

			// Token: 0x17000C0D RID: 3085
			// (get) Token: 0x06004D65 RID: 19813 RVA: 0x001B0A5C File Offset: 0x001AEC5C
			internal byte[] Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x06004D66 RID: 19814 RVA: 0x001B0A64 File Offset: 0x001AEC64
			public override int GetHashCode()
			{
				return Arrays.GetHashCode(this.id);
			}

			// Token: 0x06004D67 RID: 19815 RVA: 0x001B0A74 File Offset: 0x001AEC74
			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				Pkcs12Store.CertId certId = obj as Pkcs12Store.CertId;
				return certId != null && Arrays.AreEqual(this.id, certId.id);
			}

			// Token: 0x04003381 RID: 13185
			private readonly byte[] id;
		}

		// Token: 0x020008DA RID: 2266
		private class IgnoresCaseHashtable : IEnumerable
		{
			// Token: 0x06004D68 RID: 19816 RVA: 0x001B0AA4 File Offset: 0x001AECA4
			public void Clear()
			{
				this.orig.Clear();
				this.keys.Clear();
			}

			// Token: 0x06004D69 RID: 19817 RVA: 0x001B0ABC File Offset: 0x001AECBC
			public IEnumerator GetEnumerator()
			{
				return this.orig.GetEnumerator();
			}

			// Token: 0x17000C0E RID: 3086
			// (get) Token: 0x06004D6A RID: 19818 RVA: 0x001B0AC9 File Offset: 0x001AECC9
			public ICollection Keys
			{
				get
				{
					return this.orig.Keys;
				}
			}

			// Token: 0x06004D6B RID: 19819 RVA: 0x001B0AD8 File Offset: 0x001AECD8
			public object Remove(string alias)
			{
				string key = Platform.ToUpperInvariant(alias);
				string text = (string)this.keys[key];
				if (text == null)
				{
					return null;
				}
				this.keys.Remove(key);
				object result = this.orig[text];
				this.orig.Remove(text);
				return result;
			}

			// Token: 0x17000C0F RID: 3087
			public object this[string alias]
			{
				get
				{
					string key = Platform.ToUpperInvariant(alias);
					string text = (string)this.keys[key];
					if (text == null)
					{
						return null;
					}
					return this.orig[text];
				}
				set
				{
					string key = Platform.ToUpperInvariant(alias);
					string text = (string)this.keys[key];
					if (text != null)
					{
						this.orig.Remove(text);
					}
					this.keys[key] = alias;
					this.orig[alias] = value;
				}
			}

			// Token: 0x17000C10 RID: 3088
			// (get) Token: 0x06004D6E RID: 19822 RVA: 0x001B0BAF File Offset: 0x001AEDAF
			public ICollection Values
			{
				get
				{
					return this.orig.Values;
				}
			}

			// Token: 0x04003382 RID: 13186
			private readonly IDictionary orig = Platform.CreateHashtable();

			// Token: 0x04003383 RID: 13187
			private readonly IDictionary keys = Platform.CreateHashtable();
		}
	}
}
