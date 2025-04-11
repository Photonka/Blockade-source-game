using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Sec;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.Pem;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.OpenSsl
{
	// Token: 0x020002BD RID: 701
	public class PemReader : PemReader
	{
		// Token: 0x06001A15 RID: 6677 RVA: 0x000C8874 File Offset: 0x000C6A74
		public PemReader(TextReader reader) : this(reader, null)
		{
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000C887E File Offset: 0x000C6A7E
		public PemReader(TextReader reader, IPasswordFinder pFinder) : base(reader)
		{
			this.pFinder = pFinder;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000C8890 File Offset: 0x000C6A90
		public object ReadObject()
		{
			PemObject pemObject = base.ReadPemObject();
			if (pemObject == null)
			{
				return null;
			}
			if (Platform.EndsWith(pemObject.Type, "PRIVATE KEY"))
			{
				return this.ReadPrivateKey(pemObject);
			}
			string type = pemObject.Type;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
			if (num > 1592636440U)
			{
				if (num <= 2201027889U)
				{
					if (num != 1951606473U)
					{
						if (num != 2201027889U)
						{
							goto IL_1A8;
						}
						if (!(type == "CERTIFICATE REQUEST"))
						{
							goto IL_1A8;
						}
					}
					else
					{
						if (!(type == "RSA PUBLIC KEY"))
						{
							goto IL_1A8;
						}
						return this.ReadRsaPublicKey(pemObject);
					}
				}
				else if (num != 2369105439U)
				{
					if (num != 2389505242U)
					{
						if (num != 4194211595U)
						{
							goto IL_1A8;
						}
						if (!(type == "NEW CERTIFICATE REQUEST"))
						{
							goto IL_1A8;
						}
					}
					else
					{
						if (!(type == "CMS"))
						{
							goto IL_1A8;
						}
						goto IL_190;
					}
				}
				else
				{
					if (!(type == "PUBLIC KEY"))
					{
						goto IL_1A8;
					}
					return this.ReadPublicKey(pemObject);
				}
				return this.ReadCertificateRequest(pemObject);
			}
			if (num <= 479209360U)
			{
				if (num != 368575810U)
				{
					if (num != 479209360U)
					{
						goto IL_1A8;
					}
					if (!(type == "CERTIFICATE"))
					{
						goto IL_1A8;
					}
				}
				else if (!(type == "X509 CERTIFICATE"))
				{
					goto IL_1A8;
				}
				return this.ReadCertificate(pemObject);
			}
			if (num != 1514293038U)
			{
				if (num != 1517936775U)
				{
					if (num != 1592636440U)
					{
						goto IL_1A8;
					}
					if (!(type == "ATTRIBUTE CERTIFICATE"))
					{
						goto IL_1A8;
					}
					return this.ReadAttributeCertificate(pemObject);
				}
				else if (!(type == "PKCS7"))
				{
					goto IL_1A8;
				}
			}
			else
			{
				if (!(type == "X509 CRL"))
				{
					goto IL_1A8;
				}
				return this.ReadCrl(pemObject);
			}
			IL_190:
			return this.ReadPkcs7(pemObject);
			IL_1A8:
			throw new IOException("unrecognised object: " + pemObject.Type);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000C8A5C File Offset: 0x000C6C5C
		private AsymmetricKeyParameter ReadRsaPublicKey(PemObject pemObject)
		{
			RsaPublicKeyStructure instance = RsaPublicKeyStructure.GetInstance(Asn1Object.FromByteArray(pemObject.Content));
			return new RsaKeyParameters(false, instance.Modulus, instance.PublicExponent);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000C8A8C File Offset: 0x000C6C8C
		private AsymmetricKeyParameter ReadPublicKey(PemObject pemObject)
		{
			return PublicKeyFactory.CreateKey(pemObject.Content);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000C8A9C File Offset: 0x000C6C9C
		private X509Certificate ReadCertificate(PemObject pemObject)
		{
			X509Certificate result;
			try
			{
				result = new X509CertificateParser().ReadCertificate(pemObject.Content);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000C8AE4 File Offset: 0x000C6CE4
		private X509Crl ReadCrl(PemObject pemObject)
		{
			X509Crl result;
			try
			{
				result = new X509CrlParser().ReadCrl(pemObject.Content);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000C8B2C File Offset: 0x000C6D2C
		private Pkcs10CertificationRequest ReadCertificateRequest(PemObject pemObject)
		{
			Pkcs10CertificationRequest result;
			try
			{
				result = new Pkcs10CertificationRequest(pemObject.Content);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000C8B70 File Offset: 0x000C6D70
		private IX509AttributeCertificate ReadAttributeCertificate(PemObject pemObject)
		{
			return new X509V2AttributeCertificate(pemObject.Content);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000C8B80 File Offset: 0x000C6D80
		private BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.ContentInfo ReadPkcs7(PemObject pemObject)
		{
			BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.ContentInfo instance;
			try
			{
				instance = BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.ContentInfo.GetInstance(Asn1Object.FromByteArray(pemObject.Content));
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing PKCS7 object: " + ex.ToString());
			}
			return instance;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000C8BC8 File Offset: 0x000C6DC8
		private object ReadPrivateKey(PemObject pemObject)
		{
			string text = pemObject.Type.Substring(0, pemObject.Type.Length - "PRIVATE KEY".Length).Trim();
			byte[] array = pemObject.Content;
			IDictionary dictionary = Platform.CreateHashtable();
			foreach (object obj in pemObject.Headers)
			{
				PemHeader pemHeader = (PemHeader)obj;
				dictionary[pemHeader.Name] = pemHeader.Value;
			}
			if ((string)dictionary["Proc-Type"] == "4,ENCRYPTED")
			{
				if (this.pFinder == null)
				{
					throw new PasswordException("No password finder specified, but a password is required");
				}
				char[] password = this.pFinder.GetPassword();
				if (password == null)
				{
					throw new PasswordException("Password is null, but a password is required");
				}
				string[] array2 = ((string)dictionary["DEK-Info"]).Split(new char[]
				{
					','
				});
				string dekAlgName = array2[0].Trim();
				byte[] iv = Hex.Decode(array2[1].Trim());
				array = PemUtilities.Crypt(false, array, password, dekAlgName, iv);
			}
			object result;
			try
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(array);
				AsymmetricKeyParameter asymmetricKeyParameter;
				AsymmetricKeyParameter publicParameter;
				if (!(text == "RSA"))
				{
					if (!(text == "DSA"))
					{
						if (!(text == "EC"))
						{
							if (!(text == "ENCRYPTED"))
							{
								if (text != null)
								{
									if (text.Length == 0)
									{
										return PrivateKeyFactory.CreateKey(PrivateKeyInfo.GetInstance(instance));
									}
								}
								throw new ArgumentException("Unknown key type: " + text, "type");
							}
							char[] password2 = this.pFinder.GetPassword();
							if (password2 == null)
							{
								throw new PasswordException("Password is null, but a password is required");
							}
							return PrivateKeyFactory.DecryptKey(password2, EncryptedPrivateKeyInfo.GetInstance(instance));
						}
						else
						{
							ECPrivateKeyStructure instance2 = ECPrivateKeyStructure.GetInstance(instance);
							AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier(X9ObjectIdentifiers.IdECPublicKey, instance2.GetParameters());
							asymmetricKeyParameter = PrivateKeyFactory.CreateKey(new PrivateKeyInfo(algorithmIdentifier, instance2.ToAsn1Object()));
							DerBitString publicKey = instance2.GetPublicKey();
							if (publicKey != null)
							{
								publicParameter = PublicKeyFactory.CreateKey(new SubjectPublicKeyInfo(algorithmIdentifier, publicKey.GetBytes()));
							}
							else
							{
								publicParameter = ECKeyPairGenerator.GetCorrespondingPublicKey((ECPrivateKeyParameters)asymmetricKeyParameter);
							}
						}
					}
					else
					{
						if (instance.Count != 6)
						{
							throw new PemException("malformed sequence in DSA private key");
						}
						DerInteger derInteger = (DerInteger)instance[1];
						DerInteger derInteger2 = (DerInteger)instance[2];
						DerInteger derInteger3 = (DerInteger)instance[3];
						DerInteger derInteger4 = (DerInteger)instance[4];
						DerInteger derInteger5 = (DerInteger)instance[5];
						DsaParameters parameters = new DsaParameters(derInteger.Value, derInteger2.Value, derInteger3.Value);
						asymmetricKeyParameter = new DsaPrivateKeyParameters(derInteger5.Value, parameters);
						publicParameter = new DsaPublicKeyParameters(derInteger4.Value, parameters);
					}
				}
				else
				{
					if (instance.Count != 9)
					{
						throw new PemException("malformed sequence in RSA private key");
					}
					RsaPrivateKeyStructure instance3 = RsaPrivateKeyStructure.GetInstance(instance);
					publicParameter = new RsaKeyParameters(false, instance3.Modulus, instance3.PublicExponent);
					asymmetricKeyParameter = new RsaPrivateCrtKeyParameters(instance3.Modulus, instance3.PublicExponent, instance3.PrivateExponent, instance3.Prime1, instance3.Prime2, instance3.Exponent1, instance3.Exponent2, instance3.Coefficient);
				}
				result = new AsymmetricCipherKeyPair(publicParameter, asymmetricKeyParameter);
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new PemException("problem creating " + text + " private key: " + ex2.ToString());
			}
			return result;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000C8F88 File Offset: 0x000C7188
		private static X9ECParameters GetCurveParameters(string name)
		{
			X9ECParameters byName = CustomNamedCurves.GetByName(name);
			if (byName == null)
			{
				byName = ECNamedCurveTable.GetByName(name);
			}
			if (byName == null)
			{
				throw new Exception("unknown curve name: " + name);
			}
			return byName;
		}

		// Token: 0x04001794 RID: 6036
		private readonly IPasswordFinder pFinder;
	}
}
