using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002A6 RID: 678
	public class PkixCertPathValidatorUtilities
	{
		// Token: 0x060018D5 RID: 6357 RVA: 0x000BE9B8 File Offset: 0x000BCBB8
		internal static TrustAnchor FindTrustAnchor(X509Certificate cert, ISet trustAnchors)
		{
			IEnumerator enumerator = trustAnchors.GetEnumerator();
			TrustAnchor trustAnchor = null;
			AsymmetricKeyParameter asymmetricKeyParameter = null;
			Exception ex = null;
			X509CertStoreSelector x509CertStoreSelector = new X509CertStoreSelector();
			try
			{
				x509CertStoreSelector.Subject = PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert);
				goto IL_BC;
			}
			catch (IOException innerException)
			{
				throw new Exception("Cannot set subject search criteria for trust anchor.", innerException);
			}
			IL_35:
			trustAnchor = (TrustAnchor)enumerator.Current;
			if (trustAnchor.TrustedCert != null)
			{
				if (x509CertStoreSelector.Match(trustAnchor.TrustedCert))
				{
					asymmetricKeyParameter = trustAnchor.TrustedCert.GetPublicKey();
				}
				else
				{
					trustAnchor = null;
				}
			}
			else
			{
				if (trustAnchor.CAName != null && trustAnchor.CAPublicKey != null)
				{
					try
					{
						X509Name issuerPrincipal = PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert);
						X509Name other = new X509Name(trustAnchor.CAName);
						if (issuerPrincipal.Equivalent(other, true))
						{
							asymmetricKeyParameter = trustAnchor.CAPublicKey;
						}
						else
						{
							trustAnchor = null;
						}
						goto IL_AB;
					}
					catch (InvalidParameterException)
					{
						trustAnchor = null;
						goto IL_AB;
					}
				}
				trustAnchor = null;
			}
			IL_AB:
			if (asymmetricKeyParameter != null)
			{
				try
				{
					cert.Verify(asymmetricKeyParameter);
				}
				catch (Exception ex)
				{
					trustAnchor = null;
				}
			}
			IL_BC:
			if (enumerator.MoveNext() && trustAnchor == null)
			{
				goto IL_35;
			}
			if (trustAnchor == null && ex != null)
			{
				throw new Exception("TrustAnchor found but certificate validation failed.", ex);
			}
			return trustAnchor;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000BEACC File Offset: 0x000BCCCC
		internal static bool IsIssuerTrustAnchor(X509Certificate cert, ISet trustAnchors)
		{
			bool result;
			try
			{
				result = (PkixCertPathValidatorUtilities.FindTrustAnchor(cert, trustAnchors) != null);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000BEAFC File Offset: 0x000BCCFC
		internal static void AddAdditionalStoresFromAltNames(X509Certificate cert, PkixParameters pkixParams)
		{
			if (cert.GetIssuerAlternativeNames() != null)
			{
				foreach (object obj in cert.GetIssuerAlternativeNames())
				{
					IList list = (IList)obj;
					if (list[0].Equals(6))
					{
						PkixCertPathValidatorUtilities.AddAdditionalStoreFromLocation((string)list[1], pkixParams);
					}
				}
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000BEB5C File Offset: 0x000BCD5C
		internal static DateTime GetValidDate(PkixParameters paramsPKIX)
		{
			DateTimeObject date = paramsPKIX.Date;
			if (date == null)
			{
				return DateTime.UtcNow;
			}
			return date.Value;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000BEB7F File Offset: 0x000BCD7F
		internal static X509Name GetIssuerPrincipal(object cert)
		{
			if (cert is X509Certificate)
			{
				return ((X509Certificate)cert).IssuerDN;
			}
			return ((IX509AttributeCertificate)cert).Issuer.GetPrincipals()[0];
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000BEBA7 File Offset: 0x000BCDA7
		internal static bool IsSelfIssued(X509Certificate cert)
		{
			return cert.SubjectDN.Equivalent(cert.IssuerDN, true);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000BEBBC File Offset: 0x000BCDBC
		internal static AlgorithmIdentifier GetAlgorithmIdentifier(AsymmetricKeyParameter key)
		{
			AlgorithmIdentifier algorithmID;
			try
			{
				algorithmID = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(key).AlgorithmID;
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Subject public key cannot be decoded.", cause);
			}
			return algorithmID;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000BEBF8 File Offset: 0x000BCDF8
		internal static bool IsAnyPolicy(ISet policySet)
		{
			return policySet == null || policySet.Contains(PkixCertPathValidatorUtilities.ANY_POLICY) || policySet.Count == 0;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000BEC18 File Offset: 0x000BCE18
		internal static void AddAdditionalStoreFromLocation(string location, PkixParameters pkixParams)
		{
			if (pkixParams.IsAdditionalLocationsEnabled)
			{
				try
				{
					if (Platform.StartsWith(location, "ldap://"))
					{
						location = location.Substring(7);
						int num = location.IndexOf('/');
						if (num != -1)
						{
							"ldap://" + location.Substring(0, num);
						}
						else
						{
							"ldap://" + location;
						}
						throw Platform.CreateNotImplementedException("LDAP cert/CRL stores");
					}
				}
				catch (Exception)
				{
					throw new Exception("Exception adding X.509 stores.");
				}
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000BEC9C File Offset: 0x000BCE9C
		private static BigInteger GetSerialNumber(object cert)
		{
			if (cert is X509Certificate)
			{
				return ((X509Certificate)cert).SerialNumber;
			}
			return ((X509V2AttributeCertificate)cert).SerialNumber;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000BECC0 File Offset: 0x000BCEC0
		internal static ISet GetQualifierSet(Asn1Sequence qualifiers)
		{
			ISet set = new HashSet();
			if (qualifiers == null)
			{
				return set;
			}
			foreach (object obj in qualifiers)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				try
				{
					set.Add(PolicyQualifierInfo.GetInstance(asn1Encodable.ToAsn1Object()));
				}
				catch (IOException cause)
				{
					throw new PkixCertPathValidatorException("Policy qualifier info cannot be decoded.", cause);
				}
			}
			return set;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000BED48 File Offset: 0x000BCF48
		internal static PkixPolicyNode RemovePolicyNode(PkixPolicyNode validPolicyTree, IList[] policyNodes, PkixPolicyNode _node)
		{
			PkixPolicyNode parent = _node.Parent;
			if (validPolicyTree == null)
			{
				return null;
			}
			if (parent == null)
			{
				for (int i = 0; i < policyNodes.Length; i++)
				{
					policyNodes[i] = Platform.CreateArrayList();
				}
				return null;
			}
			parent.RemoveChild(_node);
			PkixCertPathValidatorUtilities.RemovePolicyNodeRecurse(policyNodes, _node);
			return validPolicyTree;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000BED8C File Offset: 0x000BCF8C
		private static void RemovePolicyNodeRecurse(IList[] policyNodes, PkixPolicyNode _node)
		{
			policyNodes[_node.Depth].Remove(_node);
			if (_node.HasChildren)
			{
				foreach (object obj in _node.Children)
				{
					PkixPolicyNode node = (PkixPolicyNode)obj;
					PkixCertPathValidatorUtilities.RemovePolicyNodeRecurse(policyNodes, node);
				}
			}
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000BEDFC File Offset: 0x000BCFFC
		internal static void PrepareNextCertB1(int i, IList[] policyNodes, string id_p, IDictionary m_idp, X509Certificate cert)
		{
			bool flag = false;
			foreach (object obj in policyNodes[i])
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)obj;
				if (pkixPolicyNode.ValidPolicy.Equals(id_p))
				{
					flag = true;
					pkixPolicyNode.ExpectedPolicies = (ISet)m_idp[id_p];
					break;
				}
			}
			if (!flag)
			{
				foreach (object obj2 in policyNodes[i])
				{
					PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)obj2;
					if (PkixCertPathValidatorUtilities.ANY_POLICY.Equals(pkixPolicyNode2.ValidPolicy))
					{
						ISet policyQualifiers = null;
						Asn1Sequence asn1Sequence = null;
						try
						{
							asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(cert, X509Extensions.CertificatePolicies));
						}
						catch (Exception innerException)
						{
							throw new Exception("Certificate policies cannot be decoded.", innerException);
						}
						IEnumerator enumerator2 = asn1Sequence.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							PolicyInformation policyInformation = null;
							try
							{
								policyInformation = PolicyInformation.GetInstance(enumerator2.Current);
							}
							catch (Exception innerException2)
							{
								throw new Exception("Policy information cannot be decoded.", innerException2);
							}
							if (PkixCertPathValidatorUtilities.ANY_POLICY.Equals(policyInformation.PolicyIdentifier.Id))
							{
								try
								{
									policyQualifiers = PkixCertPathValidatorUtilities.GetQualifierSet(policyInformation.PolicyQualifiers);
									break;
								}
								catch (PkixCertPathValidatorException cause)
								{
									throw new PkixCertPathValidatorException("Policy qualifier info set could not be built.", cause);
								}
							}
						}
						bool critical = false;
						ISet criticalExtensionOids = cert.GetCriticalExtensionOids();
						if (criticalExtensionOids != null)
						{
							critical = criticalExtensionOids.Contains(X509Extensions.CertificatePolicies.Id);
						}
						PkixPolicyNode parent = pkixPolicyNode2.Parent;
						if (PkixCertPathValidatorUtilities.ANY_POLICY.Equals(parent.ValidPolicy))
						{
							PkixPolicyNode pkixPolicyNode3 = new PkixPolicyNode(Platform.CreateArrayList(), i, (ISet)m_idp[id_p], parent, policyQualifiers, id_p, critical);
							parent.AddChild(pkixPolicyNode3);
							policyNodes[i].Add(pkixPolicyNode3);
							return;
						}
						break;
					}
				}
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000BEFC0 File Offset: 0x000BD1C0
		internal static PkixPolicyNode PrepareNextCertB2(int i, IList[] policyNodes, string id_p, PkixPolicyNode validPolicyTree)
		{
			int num = 0;
			foreach (object obj in Platform.CreateArrayList(policyNodes[i]))
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)obj;
				if (pkixPolicyNode.ValidPolicy.Equals(id_p))
				{
					pkixPolicyNode.Parent.RemoveChild(pkixPolicyNode);
					policyNodes[i].RemoveAt(num);
					for (int j = i - 1; j >= 0; j--)
					{
						IList list = policyNodes[j];
						for (int k = 0; k < list.Count; k++)
						{
							PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)list[k];
							if (!pkixPolicyNode2.HasChildren)
							{
								validPolicyTree = PkixCertPathValidatorUtilities.RemovePolicyNode(validPolicyTree, policyNodes, pkixPolicyNode2);
								if (validPolicyTree == null)
								{
									break;
								}
							}
						}
					}
				}
				else
				{
					num++;
				}
			}
			return validPolicyTree;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000BF09C File Offset: 0x000BD29C
		internal static void GetCertStatus(DateTime validDate, X509Crl crl, object cert, CertStatus certStatus)
		{
			X509Crl x509Crl = null;
			try
			{
				x509Crl = new X509Crl(CertificateList.GetInstance((Asn1Sequence)Asn1Object.FromByteArray(crl.GetEncoded())));
			}
			catch (Exception innerException)
			{
				throw new Exception("Bouncy Castle X509Crl could not be created.", innerException);
			}
			X509CrlEntry revokedCertificate = x509Crl.GetRevokedCertificate(PkixCertPathValidatorUtilities.GetSerialNumber(cert));
			if (revokedCertificate == null)
			{
				return;
			}
			X509Name issuerPrincipal = PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert);
			if (issuerPrincipal.Equivalent(revokedCertificate.GetCertificateIssuer(), true) || issuerPrincipal.Equivalent(crl.IssuerDN, true))
			{
				DerEnumerated derEnumerated = null;
				if (revokedCertificate.HasExtensions)
				{
					try
					{
						derEnumerated = DerEnumerated.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(revokedCertificate, X509Extensions.ReasonCode));
					}
					catch (Exception innerException2)
					{
						throw new Exception("Reason code CRL entry extension could not be decoded.", innerException2);
					}
				}
				if (validDate.Ticks >= revokedCertificate.RevocationDate.Ticks || derEnumerated == null || derEnumerated.Value.TestBit(0) || derEnumerated.Value.TestBit(1) || derEnumerated.Value.TestBit(2) || derEnumerated.Value.TestBit(8))
				{
					if (derEnumerated != null)
					{
						certStatus.Status = derEnumerated.Value.SignValue;
					}
					else
					{
						certStatus.Status = 0;
					}
					certStatus.RevocationDate = new DateTimeObject(revokedCertificate.RevocationDate);
				}
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000BF1E0 File Offset: 0x000BD3E0
		internal static AsymmetricKeyParameter GetNextWorkingKey(IList certs, int index)
		{
			AsymmetricKeyParameter publicKey = ((X509Certificate)certs[index]).GetPublicKey();
			if (!(publicKey is DsaPublicKeyParameters))
			{
				return publicKey;
			}
			DsaPublicKeyParameters dsaPublicKeyParameters = (DsaPublicKeyParameters)publicKey;
			if (dsaPublicKeyParameters.Parameters != null)
			{
				return dsaPublicKeyParameters;
			}
			for (int i = index + 1; i < certs.Count; i++)
			{
				publicKey = ((X509Certificate)certs[i]).GetPublicKey();
				if (!(publicKey is DsaPublicKeyParameters))
				{
					throw new PkixCertPathValidatorException("DSA parameters cannot be inherited from previous certificate.");
				}
				DsaPublicKeyParameters dsaPublicKeyParameters2 = (DsaPublicKeyParameters)publicKey;
				if (dsaPublicKeyParameters2.Parameters != null)
				{
					DsaParameters parameters = dsaPublicKeyParameters2.Parameters;
					try
					{
						return new DsaPublicKeyParameters(dsaPublicKeyParameters.Y, parameters);
					}
					catch (Exception ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
			throw new PkixCertPathValidatorException("DSA parameters cannot be inherited from previous certificate.");
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x000BF2A4 File Offset: 0x000BD4A4
		internal static DateTime GetValidCertDateFromValidityModel(PkixParameters paramsPkix, PkixCertPath certPath, int index)
		{
			if (paramsPkix.ValidityModel != 1)
			{
				return PkixCertPathValidatorUtilities.GetValidDate(paramsPkix);
			}
			if (index <= 0)
			{
				return PkixCertPathValidatorUtilities.GetValidDate(paramsPkix);
			}
			if (index - 1 == 0)
			{
				DerGeneralizedTime derGeneralizedTime = null;
				try
				{
					derGeneralizedTime = DerGeneralizedTime.GetInstance(((X509Certificate)certPath.Certificates[index - 1]).GetExtensionValue(IsisMttObjectIdentifiers.IdIsisMttATDateOfCertGen));
				}
				catch (ArgumentException)
				{
					throw new Exception("Date of cert gen extension could not be read.");
				}
				if (derGeneralizedTime != null)
				{
					try
					{
						return derGeneralizedTime.ToDateTime();
					}
					catch (ArgumentException innerException)
					{
						throw new Exception("Date from date of cert gen extension could not be parsed.", innerException);
					}
				}
			}
			return ((X509Certificate)certPath.Certificates[index - 1]).NotBefore;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000BF354 File Offset: 0x000BD554
		internal static ICollection FindCertificates(X509CertStoreSelector certSelect, IList certStores)
		{
			ISet set = new HashSet();
			foreach (object obj in certStores)
			{
				IX509Store ix509Store = (IX509Store)obj;
				try
				{
					foreach (object obj2 in ix509Store.GetMatches(certSelect))
					{
						X509Certificate o = (X509Certificate)obj2;
						set.Add(o);
					}
				}
				catch (Exception innerException)
				{
					throw new Exception("Problem while picking certificates from X.509 store.", innerException);
				}
			}
			return set;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000BF418 File Offset: 0x000BD618
		internal static void GetCrlIssuersFromDistributionPoint(DistributionPoint dp, ICollection issuerPrincipals, X509CrlStoreSelector selector, PkixParameters pkixParams)
		{
			IList list = Platform.CreateArrayList();
			if (dp.CrlIssuer != null)
			{
				GeneralName[] names = dp.CrlIssuer.GetNames();
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].TagNo == 4)
					{
						try
						{
							list.Add(X509Name.GetInstance(names[i].Name.ToAsn1Object()));
						}
						catch (IOException innerException)
						{
							throw new Exception("CRL issuer information from distribution point cannot be decoded.", innerException);
						}
					}
				}
			}
			else
			{
				if (dp.DistributionPointName == null)
				{
					throw new Exception("CRL issuer is omitted from distribution point but no distributionPoint field present.");
				}
				foreach (object obj in issuerPrincipals)
				{
					list.Add((X509Name)obj);
				}
			}
			selector.Issuers = list;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000BF4D4 File Offset: 0x000BD6D4
		internal static ISet GetCompleteCrls(DistributionPoint dp, object cert, DateTime currentDate, PkixParameters paramsPKIX)
		{
			X509CrlStoreSelector x509CrlStoreSelector = new X509CrlStoreSelector();
			try
			{
				ISet set = new HashSet();
				if (cert is X509V2AttributeCertificate)
				{
					set.Add(((X509V2AttributeCertificate)cert).Issuer.GetPrincipals()[0]);
				}
				else
				{
					set.Add(PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert));
				}
				PkixCertPathValidatorUtilities.GetCrlIssuersFromDistributionPoint(dp, set, x509CrlStoreSelector, paramsPKIX);
			}
			catch (Exception innerException)
			{
				throw new Exception("Could not get issuer information from distribution point.", innerException);
			}
			if (cert is X509Certificate)
			{
				x509CrlStoreSelector.CertificateChecking = (X509Certificate)cert;
			}
			else if (cert is X509V2AttributeCertificate)
			{
				x509CrlStoreSelector.AttrCertChecking = (IX509AttributeCertificate)cert;
			}
			x509CrlStoreSelector.CompleteCrlEnabled = true;
			ISet set2 = PkixCertPathValidatorUtilities.CrlUtilities.FindCrls(x509CrlStoreSelector, paramsPKIX, currentDate);
			if (!set2.IsEmpty)
			{
				return set2;
			}
			if (cert is IX509AttributeCertificate)
			{
				IX509AttributeCertificate ix509AttributeCertificate = (IX509AttributeCertificate)cert;
				throw new Exception("No CRLs found for issuer \"" + ix509AttributeCertificate.Issuer.GetPrincipals()[0] + "\"");
			}
			X509Certificate x509Certificate = (X509Certificate)cert;
			throw new Exception("No CRLs found for issuer \"" + x509Certificate.IssuerDN + "\"");
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000BF5E0 File Offset: 0x000BD7E0
		internal static ISet GetDeltaCrls(DateTime currentDate, PkixParameters paramsPKIX, X509Crl completeCRL)
		{
			X509CrlStoreSelector x509CrlStoreSelector = new X509CrlStoreSelector();
			try
			{
				IList list = Platform.CreateArrayList();
				list.Add(completeCRL.IssuerDN);
				x509CrlStoreSelector.Issuers = list;
			}
			catch (IOException innerException)
			{
				throw new Exception("Cannot extract issuer from CRL.", innerException);
			}
			BigInteger bigInteger = null;
			try
			{
				Asn1Object extensionValue = PkixCertPathValidatorUtilities.GetExtensionValue(completeCRL, X509Extensions.CrlNumber);
				if (extensionValue != null)
				{
					bigInteger = DerInteger.GetInstance(extensionValue).PositiveValue;
				}
			}
			catch (Exception innerException2)
			{
				throw new Exception("CRL number extension could not be extracted from CRL.", innerException2);
			}
			byte[] issuingDistributionPoint = null;
			try
			{
				Asn1Object extensionValue2 = PkixCertPathValidatorUtilities.GetExtensionValue(completeCRL, X509Extensions.IssuingDistributionPoint);
				if (extensionValue2 != null)
				{
					issuingDistributionPoint = extensionValue2.GetDerEncoded();
				}
			}
			catch (Exception innerException3)
			{
				throw new Exception("Issuing distribution point extension value could not be read.", innerException3);
			}
			x509CrlStoreSelector.MinCrlNumber = ((bigInteger == null) ? null : bigInteger.Add(BigInteger.One));
			x509CrlStoreSelector.IssuingDistributionPoint = issuingDistributionPoint;
			x509CrlStoreSelector.IssuingDistributionPointEnabled = true;
			x509CrlStoreSelector.MaxBaseCrlNumber = bigInteger;
			IEnumerable enumerable = PkixCertPathValidatorUtilities.CrlUtilities.FindCrls(x509CrlStoreSelector, paramsPKIX, currentDate);
			ISet set = new HashSet();
			foreach (object obj in enumerable)
			{
				X509Crl x509Crl = (X509Crl)obj;
				if (PkixCertPathValidatorUtilities.isDeltaCrl(x509Crl))
				{
					set.Add(x509Crl);
				}
			}
			return set;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000BF73C File Offset: 0x000BD93C
		private static bool isDeltaCrl(X509Crl crl)
		{
			return crl.GetCriticalExtensionOids().Contains(X509Extensions.DeltaCrlIndicator.Id);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000BF754 File Offset: 0x000BD954
		internal static ICollection FindCertificates(X509AttrCertStoreSelector certSelect, IList certStores)
		{
			ISet set = new HashSet();
			foreach (object obj in certStores)
			{
				IX509Store ix509Store = (IX509Store)obj;
				try
				{
					foreach (object obj2 in ix509Store.GetMatches(certSelect))
					{
						X509V2AttributeCertificate o = (X509V2AttributeCertificate)obj2;
						set.Add(o);
					}
				}
				catch (Exception innerException)
				{
					throw new Exception("Problem while picking certificates from X.509 store.", innerException);
				}
			}
			return set;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000BF818 File Offset: 0x000BDA18
		internal static void AddAdditionalStoresFromCrlDistributionPoint(CrlDistPoint crldp, PkixParameters pkixParams)
		{
			if (crldp != null)
			{
				DistributionPoint[] array = null;
				try
				{
					array = crldp.GetDistributionPoints();
				}
				catch (Exception innerException)
				{
					throw new Exception("Distribution points could not be read.", innerException);
				}
				for (int i = 0; i < array.Length; i++)
				{
					DistributionPointName distributionPointName = array[i].DistributionPointName;
					if (distributionPointName != null && distributionPointName.PointType == 0)
					{
						GeneralName[] names = GeneralNames.GetInstance(distributionPointName.Name).GetNames();
						for (int j = 0; j < names.Length; j++)
						{
							if (names[j].TagNo == 6)
							{
								PkixCertPathValidatorUtilities.AddAdditionalStoreFromLocation(DerIA5String.GetInstance(names[j].Name).GetString(), pkixParams);
							}
						}
					}
				}
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000BF8C4 File Offset: 0x000BDAC4
		internal static bool ProcessCertD1i(int index, IList[] policyNodes, DerObjectIdentifier pOid, ISet pq)
		{
			IList list = policyNodes[index - 1];
			for (int i = 0; i < list.Count; i++)
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)list[i];
				if (pkixPolicyNode.ExpectedPolicies.Contains(pOid.Id))
				{
					ISet set = new HashSet();
					set.Add(pOid.Id);
					PkixPolicyNode pkixPolicyNode2 = new PkixPolicyNode(Platform.CreateArrayList(), index, set, pkixPolicyNode, pq, pOid.Id, false);
					pkixPolicyNode.AddChild(pkixPolicyNode2);
					policyNodes[index].Add(pkixPolicyNode2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x000BF948 File Offset: 0x000BDB48
		internal static void ProcessCertD1ii(int index, IList[] policyNodes, DerObjectIdentifier _poid, ISet _pq)
		{
			IList list = policyNodes[index - 1];
			for (int i = 0; i < list.Count; i++)
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)list[i];
				if (PkixCertPathValidatorUtilities.ANY_POLICY.Equals(pkixPolicyNode.ValidPolicy))
				{
					ISet set = new HashSet();
					set.Add(_poid.Id);
					PkixPolicyNode pkixPolicyNode2 = new PkixPolicyNode(Platform.CreateArrayList(), index, set, pkixPolicyNode, _pq, _poid.Id, false);
					pkixPolicyNode.AddChild(pkixPolicyNode2);
					policyNodes[index].Add(pkixPolicyNode2);
					return;
				}
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000BF9C8 File Offset: 0x000BDBC8
		internal static ICollection FindIssuerCerts(X509Certificate cert, PkixBuilderParameters pkixParams)
		{
			X509CertStoreSelector x509CertStoreSelector = new X509CertStoreSelector();
			ISet set = new HashSet();
			try
			{
				x509CertStoreSelector.Subject = cert.IssuerDN;
			}
			catch (IOException innerException)
			{
				throw new Exception("Subject criteria for certificate selector to find issuer certificate could not be set.", innerException);
			}
			try
			{
				set.AddAll(PkixCertPathValidatorUtilities.FindCertificates(x509CertStoreSelector, pkixParams.GetStores()));
				set.AddAll(PkixCertPathValidatorUtilities.FindCertificates(x509CertStoreSelector, pkixParams.GetAdditionalStores()));
			}
			catch (Exception innerException2)
			{
				throw new Exception("Issuer certificate cannot be searched.", innerException2);
			}
			return set;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000BFA50 File Offset: 0x000BDC50
		internal static Asn1Object GetExtensionValue(IX509Extension ext, DerObjectIdentifier oid)
		{
			Asn1OctetString extensionValue = ext.GetExtensionValue(oid);
			if (extensionValue == null)
			{
				return null;
			}
			return X509ExtensionUtilities.FromExtensionValue(extensionValue);
		}

		// Token: 0x04001740 RID: 5952
		private static readonly PkixCrlUtilities CrlUtilities = new PkixCrlUtilities();

		// Token: 0x04001741 RID: 5953
		internal static readonly string ANY_POLICY = "2.5.29.32.0";

		// Token: 0x04001742 RID: 5954
		internal static readonly string CRL_NUMBER = X509Extensions.CrlNumber.Id;

		// Token: 0x04001743 RID: 5955
		internal static readonly int KEY_CERT_SIGN = 5;

		// Token: 0x04001744 RID: 5956
		internal static readonly int CRL_SIGN = 6;

		// Token: 0x04001745 RID: 5957
		internal static readonly string[] crlReasons = new string[]
		{
			"unspecified",
			"keyCompromise",
			"cACompromise",
			"affiliationChanged",
			"superseded",
			"cessationOfOperation",
			"certificateHold",
			"unknown",
			"removeFromCRL",
			"privilegeWithdrawn",
			"aACompromise"
		};
	}
}
