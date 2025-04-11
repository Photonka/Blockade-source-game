using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002A8 RID: 680
	public class PkixNameConstraintValidator
	{
		// Token: 0x060018F9 RID: 6393 RVA: 0x000BFD3C File Offset: 0x000BDF3C
		private static bool WithinDNSubtree(Asn1Sequence dns, Asn1Sequence subtree)
		{
			if (subtree.Count < 1)
			{
				return false;
			}
			if (subtree.Count > dns.Count)
			{
				return false;
			}
			for (int i = subtree.Count - 1; i >= 0; i--)
			{
				if (!subtree[i].Equals(dns[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000BFD8F File Offset: 0x000BDF8F
		public void CheckPermittedDN(Asn1Sequence dns)
		{
			this.CheckPermittedDN(this.permittedSubtreesDN, dns);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000BFD9E File Offset: 0x000BDF9E
		public void CheckExcludedDN(Asn1Sequence dns)
		{
			this.CheckExcludedDN(this.excludedSubtreesDN, dns);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000BFDB0 File Offset: 0x000BDFB0
		private void CheckPermittedDN(ISet permitted, Asn1Sequence dns)
		{
			if (permitted == null)
			{
				return;
			}
			if (permitted.Count == 0 && dns.Count == 0)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				Asn1Sequence subtree = (Asn1Sequence)obj;
				if (PkixNameConstraintValidator.WithinDNSubtree(dns, subtree))
				{
					return;
				}
			}
			throw new PkixNameConstraintValidatorException("Subject distinguished name is not from a permitted subtree");
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000BFE04 File Offset: 0x000BE004
		private void CheckExcludedDN(ISet excluded, Asn1Sequence dns)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				Asn1Sequence subtree = (Asn1Sequence)obj;
				if (PkixNameConstraintValidator.WithinDNSubtree(dns, subtree))
				{
					throw new PkixNameConstraintValidatorException("Subject distinguished name is from an excluded subtree");
				}
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000BFE4C File Offset: 0x000BE04C
		private ISet IntersectDN(ISet permitted, ISet dns)
		{
			ISet set = new HashSet();
			foreach (object obj in dns)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(((GeneralSubtree)obj).Base.Name.ToAsn1Object());
				if (permitted == null)
				{
					if (instance != null)
					{
						set.Add(instance);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						Asn1Sequence asn1Sequence = (Asn1Sequence)obj2;
						if (PkixNameConstraintValidator.WithinDNSubtree(instance, asn1Sequence))
						{
							set.Add(instance);
						}
						else if (PkixNameConstraintValidator.WithinDNSubtree(asn1Sequence, instance))
						{
							set.Add(asn1Sequence);
						}
					}
				}
			}
			return set;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000BFEE4 File Offset: 0x000BE0E4
		private ISet UnionDN(ISet excluded, Asn1Sequence dn)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)obj;
					if (PkixNameConstraintValidator.WithinDNSubtree(dn, asn1Sequence))
					{
						set.Add(asn1Sequence);
					}
					else if (PkixNameConstraintValidator.WithinDNSubtree(asn1Sequence, dn))
					{
						set.Add(dn);
					}
					else
					{
						set.Add(asn1Sequence);
						set.Add(dn);
					}
				}
				return set;
			}
			if (dn == null)
			{
				return excluded;
			}
			excluded.Add(dn);
			return excluded;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000BFF60 File Offset: 0x000BE160
		private ISet IntersectEmail(ISet permitted, ISet emails)
		{
			ISet set = new HashSet();
			foreach (object obj in emails)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string email = (string)obj2;
						this.intersectEmail(text, email, set);
					}
				}
			}
			return set;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000BFFD4 File Offset: 0x000BE1D4
		private ISet UnionEmail(ISet excluded, string email)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string email2 = (string)obj;
					this.unionEmail(email2, email, set);
				}
				return set;
			}
			if (email == null)
			{
				return excluded;
			}
			excluded.Add(email);
			return excluded;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000C0024 File Offset: 0x000BE224
		private ISet IntersectIP(ISet permitted, ISet ips)
		{
			ISet set = new HashSet();
			foreach (object obj in ips)
			{
				byte[] octets = Asn1OctetString.GetInstance(((GeneralSubtree)obj).Base.Name).GetOctets();
				if (permitted == null)
				{
					if (octets != null)
					{
						set.Add(octets);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						byte[] ipWithSubmask = (byte[])obj2;
						set.AddAll(this.IntersectIPRange(ipWithSubmask, octets));
					}
				}
			}
			return set;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000C00A8 File Offset: 0x000BE2A8
		private ISet UnionIP(ISet excluded, byte[] ip)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					byte[] ipWithSubmask = (byte[])obj;
					set.AddAll(this.UnionIPRange(ipWithSubmask, ip));
				}
				return set;
			}
			if (ip == null)
			{
				return excluded;
			}
			excluded.Add(ip);
			return excluded;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000C0100 File Offset: 0x000BE300
		private ISet UnionIPRange(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			ISet set = new HashSet();
			if (Arrays.AreEqual(ipWithSubmask1, ipWithSubmask2))
			{
				set.Add(ipWithSubmask1);
			}
			else
			{
				set.Add(ipWithSubmask1);
				set.Add(ipWithSubmask2);
			}
			return set;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000C0134 File Offset: 0x000BE334
		private ISet IntersectIPRange(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			if (ipWithSubmask1.Length != ipWithSubmask2.Length)
			{
				return new HashSet();
			}
			byte[][] array = this.ExtractIPsAndSubnetMasks(ipWithSubmask1, ipWithSubmask2);
			byte[] ip = array[0];
			byte[] array2 = array[1];
			byte[] ip2 = array[2];
			byte[] array3 = array[3];
			byte[][] array4 = this.MinMaxIPs(ip, array2, ip2, array3);
			byte[] ip3 = PkixNameConstraintValidator.Min(array4[1], array4[3]);
			if (PkixNameConstraintValidator.CompareTo(PkixNameConstraintValidator.Max(array4[0], array4[2]), ip3) == 1)
			{
				return new HashSet();
			}
			byte[] ip4 = PkixNameConstraintValidator.Or(array4[0], array4[2]);
			byte[] subnetMask = PkixNameConstraintValidator.Or(array2, array3);
			return new HashSet
			{
				this.IpWithSubnetMask(ip4, subnetMask)
			};
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000C01CC File Offset: 0x000BE3CC
		private byte[] IpWithSubnetMask(byte[] ip, byte[] subnetMask)
		{
			int num = ip.Length;
			byte[] array = new byte[num * 2];
			Array.Copy(ip, 0, array, 0, num);
			Array.Copy(subnetMask, 0, array, num, num);
			return array;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000C01FC File Offset: 0x000BE3FC
		private byte[][] ExtractIPsAndSubnetMasks(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			int num = ipWithSubmask1.Length / 2;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			Array.Copy(ipWithSubmask1, 0, array, 0, num);
			Array.Copy(ipWithSubmask1, num, array2, 0, num);
			byte[] array3 = new byte[num];
			byte[] array4 = new byte[num];
			Array.Copy(ipWithSubmask2, 0, array3, 0, num);
			Array.Copy(ipWithSubmask2, num, array4, 0, num);
			return new byte[][]
			{
				array,
				array2,
				array3,
				array4
			};
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000C026C File Offset: 0x000BE46C
		private byte[][] MinMaxIPs(byte[] ip1, byte[] subnetmask1, byte[] ip2, byte[] subnetmask2)
		{
			int num = ip1.Length;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			byte[] array3 = new byte[num];
			byte[] array4 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (ip1[i] & subnetmask1[i]);
				array2[i] = ((ip1[i] & subnetmask1[i]) | ~subnetmask1[i]);
				array3[i] = (ip2[i] & subnetmask2[i]);
				array4[i] = ((ip2[i] & subnetmask2[i]) | ~subnetmask2[i]);
			}
			return new byte[][]
			{
				array,
				array2,
				array3,
				array4
			};
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000C030C File Offset: 0x000BE50C
		private void CheckPermittedEmail(ISet permitted, string email)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string constraint = (string)obj;
				if (this.EmailIsConstrained(email, constraint))
				{
					return;
				}
			}
			if (email.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("Subject email address is not from a permitted subtree.");
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000C0360 File Offset: 0x000BE560
		private void CheckExcludedEmail(ISet excluded, string email)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				string constraint = (string)obj;
				if (this.EmailIsConstrained(email, constraint))
				{
					throw new PkixNameConstraintValidatorException("Email address is from an excluded subtree.");
				}
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000C03A8 File Offset: 0x000BE5A8
		private void CheckPermittedIP(ISet permitted, byte[] ip)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				byte[] constraint = (byte[])obj;
				if (this.IsIPConstrained(ip, constraint))
				{
					return;
				}
			}
			if (ip.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("IP is not from a permitted subtree.");
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000C03F8 File Offset: 0x000BE5F8
		private void checkExcludedIP(ISet excluded, byte[] ip)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				byte[] constraint = (byte[])obj;
				if (this.IsIPConstrained(ip, constraint))
				{
					throw new PkixNameConstraintValidatorException("IP is from an excluded subtree.");
				}
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000C0440 File Offset: 0x000BE640
		private bool IsIPConstrained(byte[] ip, byte[] constraint)
		{
			int num = ip.Length;
			if (num != constraint.Length / 2)
			{
				return false;
			}
			byte[] array = new byte[num];
			Array.Copy(constraint, num, array, 0, num);
			byte[] array2 = new byte[num];
			byte[] array3 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (constraint[i] & array[i]);
				array3[i] = (ip[i] & array[i]);
			}
			return Arrays.AreEqual(array2, array3);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000C04B0 File Offset: 0x000BE6B0
		private bool EmailIsConstrained(string email, string constraint)
		{
			string text = email.Substring(email.IndexOf('@') + 1);
			if (constraint.IndexOf('@') != -1)
			{
				if (Platform.ToUpperInvariant(email).Equals(Platform.ToUpperInvariant(constraint)))
				{
					return true;
				}
			}
			else if (!constraint[0].Equals('.'))
			{
				if (Platform.ToUpperInvariant(text).Equals(Platform.ToUpperInvariant(constraint)))
				{
					return true;
				}
			}
			else if (this.WithinDomain(text, constraint))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000C0524 File Offset: 0x000BE724
		private bool WithinDomain(string testDomain, string domain)
		{
			string text = domain;
			if (Platform.StartsWith(text, "."))
			{
				text = text.Substring(1);
			}
			string[] array = text.Split(new char[]
			{
				'.'
			});
			string[] array2 = testDomain.Split(new char[]
			{
				'.'
			});
			if (array2.Length <= array.Length)
			{
				return false;
			}
			int num = array2.Length - array.Length;
			for (int i = -1; i < array.Length; i++)
			{
				if (i == -1)
				{
					if (array2[i + num].Equals(""))
					{
						return false;
					}
				}
				else if (!Platform.EqualsIgnoreCase(array2[i + num], array[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000C05C0 File Offset: 0x000BE7C0
		private void CheckPermittedDNS(ISet permitted, string dns)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string text = (string)obj;
				if (this.WithinDomain(dns, text) || Platform.ToUpperInvariant(dns).Equals(Platform.ToUpperInvariant(text)))
				{
					return;
				}
			}
			if (dns.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("DNS is not from a permitted subtree.");
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000C0628 File Offset: 0x000BE828
		private void checkExcludedDNS(ISet excluded, string dns)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				string text = (string)obj;
				if (this.WithinDomain(dns, text) || Platform.EqualsIgnoreCase(dns, text))
				{
					throw new PkixNameConstraintValidatorException("DNS is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000C067C File Offset: 0x000BE87C
		private void unionEmail(string email1, string email2, ISet union)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (Platform.EqualsIgnoreCase(email1, email2))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (Platform.EqualsIgnoreCase(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (Platform.StartsWith(email1, "."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(email1, email2) || Platform.EqualsIgnoreCase(email1, email2))
					{
						union.Add(email2);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				if (Platform.EqualsIgnoreCase(email2.Substring(email1.IndexOf('@') + 1), email1))
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else if (Platform.StartsWith(email2, "."))
			{
				if (this.WithinDomain(email1, email2))
				{
					union.Add(email2);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else
			{
				if (Platform.EqualsIgnoreCase(email1, email2))
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000C0860 File Offset: 0x000BEA60
		private void unionURI(string email1, string email2, ISet union)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (Platform.EqualsIgnoreCase(email1, email2))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (Platform.EqualsIgnoreCase(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (Platform.StartsWith(email1, "."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(email1, email2) || Platform.EqualsIgnoreCase(email1, email2))
					{
						union.Add(email2);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				if (Platform.EqualsIgnoreCase(email2.Substring(email1.IndexOf('@') + 1), email1))
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else if (Platform.StartsWith(email2, "."))
			{
				if (this.WithinDomain(email1, email2))
				{
					union.Add(email2);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else
			{
				if (Platform.EqualsIgnoreCase(email1, email2))
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000C0A44 File Offset: 0x000BEC44
		private ISet intersectDNS(ISet permitted, ISet dnss)
		{
			ISet set = new HashSet();
			foreach (object obj in dnss)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string text2 = (string)obj2;
						if (this.WithinDomain(text2, text))
						{
							set.Add(text2);
						}
						else if (this.WithinDomain(text, text2))
						{
							set.Add(text);
						}
					}
				}
			}
			return set;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000C0AD4 File Offset: 0x000BECD4
		protected ISet unionDNS(ISet excluded, string dns)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string text = (string)obj;
					if (this.WithinDomain(text, dns))
					{
						set.Add(dns);
					}
					else if (this.WithinDomain(dns, text))
					{
						set.Add(text);
					}
					else
					{
						set.Add(text);
						set.Add(dns);
					}
				}
				return set;
			}
			if (dns == null)
			{
				return excluded;
			}
			excluded.Add(dns);
			return excluded;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000C0B50 File Offset: 0x000BED50
		private void intersectEmail(string email1, string email2, ISet intersect)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (Platform.EqualsIgnoreCase(email1, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(text, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (Platform.EqualsIgnoreCase(text, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (Platform.StartsWith(email1, "."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(email1, email2) || Platform.EqualsIgnoreCase(email1, email2))
					{
						intersect.Add(email1);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (this.WithinDomain(email2, email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				if (Platform.EqualsIgnoreCase(email2.Substring(email2.IndexOf('@') + 1), email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (Platform.StartsWith(email2, "."))
			{
				if (this.WithinDomain(email1, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (Platform.EqualsIgnoreCase(email1, email2))
			{
				intersect.Add(email1);
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000C0CB4 File Offset: 0x000BEEB4
		private void checkExcludedURI(ISet excluded, string uri)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				string constraint = (string)obj;
				if (this.IsUriConstrained(uri, constraint))
				{
					throw new PkixNameConstraintValidatorException("URI is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000C0CFC File Offset: 0x000BEEFC
		private ISet intersectURI(ISet permitted, ISet uris)
		{
			ISet set = new HashSet();
			foreach (object obj in uris)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string email = (string)obj2;
						this.intersectURI(email, text, set);
					}
				}
			}
			return set;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000C0D70 File Offset: 0x000BEF70
		private ISet unionURI(ISet excluded, string uri)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string email = (string)obj;
					this.unionURI(email, uri, set);
				}
				return set;
			}
			if (uri == null)
			{
				return excluded;
			}
			excluded.Add(uri);
			return excluded;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000C0DC0 File Offset: 0x000BEFC0
		private void intersectURI(string email1, string email2, ISet intersect)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (Platform.EqualsIgnoreCase(email1, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(text, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (Platform.EqualsIgnoreCase(text, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (Platform.StartsWith(email1, "."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(email1, email2) || Platform.EqualsIgnoreCase(email1, email2))
					{
						intersect.Add(email1);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (this.WithinDomain(email2, email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				if (Platform.EqualsIgnoreCase(email2.Substring(email2.IndexOf('@') + 1), email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (Platform.StartsWith(email2, "."))
			{
				if (this.WithinDomain(email1, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (Platform.EqualsIgnoreCase(email1, email2))
			{
				intersect.Add(email1);
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000C0F24 File Offset: 0x000BF124
		private void CheckPermittedURI(ISet permitted, string uri)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string constraint = (string)obj;
				if (this.IsUriConstrained(uri, constraint))
				{
					return;
				}
			}
			if (uri.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("URI is not from a permitted subtree.");
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000C0F78 File Offset: 0x000BF178
		private bool IsUriConstrained(string uri, string constraint)
		{
			string text = PkixNameConstraintValidator.ExtractHostFromURL(uri);
			if (!Platform.StartsWith(constraint, "."))
			{
				if (Platform.EqualsIgnoreCase(text, constraint))
				{
					return true;
				}
			}
			else if (this.WithinDomain(text, constraint))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000C0FB4 File Offset: 0x000BF1B4
		private static string ExtractHostFromURL(string url)
		{
			string text = url.Substring(url.IndexOf(':') + 1);
			int num = Platform.IndexOf(text, "//");
			if (num != -1)
			{
				text = text.Substring(num + 2);
			}
			if (text.LastIndexOf(':') != -1)
			{
				text = text.Substring(0, text.LastIndexOf(':'));
			}
			text = text.Substring(text.IndexOf(':') + 1);
			text = text.Substring(text.IndexOf('@') + 1);
			if (text.IndexOf('/') != -1)
			{
				text = text.Substring(0, text.IndexOf('/'));
			}
			return text;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000C1048 File Offset: 0x000BF248
		public void checkPermitted(GeneralName name)
		{
			switch (name.TagNo)
			{
			case 1:
				this.CheckPermittedEmail(this.permittedSubtreesEmail, this.ExtractNameAsString(name));
				return;
			case 2:
				this.CheckPermittedDNS(this.permittedSubtreesDNS, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.CheckPermittedDN(Asn1Sequence.GetInstance(name.Name.ToAsn1Object()));
				return;
			case 6:
				this.CheckPermittedURI(this.permittedSubtreesURI, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 7:
			{
				byte[] octets = Asn1OctetString.GetInstance(name.Name).GetOctets();
				this.CheckPermittedIP(this.permittedSubtreesIP, octets);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000C1104 File Offset: 0x000BF304
		public void checkExcluded(GeneralName name)
		{
			switch (name.TagNo)
			{
			case 1:
				this.CheckExcludedEmail(this.excludedSubtreesEmail, this.ExtractNameAsString(name));
				return;
			case 2:
				this.checkExcludedDNS(this.excludedSubtreesDNS, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.CheckExcludedDN(Asn1Sequence.GetInstance(name.Name.ToAsn1Object()));
				return;
			case 6:
				this.checkExcludedURI(this.excludedSubtreesURI, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 7:
			{
				byte[] octets = Asn1OctetString.GetInstance(name.Name).GetOctets();
				this.checkExcludedIP(this.excludedSubtreesIP, octets);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000C11C0 File Offset: 0x000BF3C0
		public void IntersectPermittedSubtree(Asn1Sequence permitted)
		{
			IDictionary dictionary = Platform.CreateHashtable();
			foreach (object obj in permitted)
			{
				GeneralSubtree instance = GeneralSubtree.GetInstance(obj);
				int tagNo = instance.Base.TagNo;
				if (dictionary[tagNo] == null)
				{
					dictionary[tagNo] = new HashSet();
				}
				((ISet)dictionary[tagNo]).Add(instance);
			}
			foreach (object obj2 in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				switch ((int)dictionaryEntry.Key)
				{
				case 1:
					this.permittedSubtreesEmail = this.IntersectEmail(this.permittedSubtreesEmail, (ISet)dictionaryEntry.Value);
					break;
				case 2:
					this.permittedSubtreesDNS = this.intersectDNS(this.permittedSubtreesDNS, (ISet)dictionaryEntry.Value);
					break;
				case 4:
					this.permittedSubtreesDN = this.IntersectDN(this.permittedSubtreesDN, (ISet)dictionaryEntry.Value);
					break;
				case 6:
					this.permittedSubtreesURI = this.intersectURI(this.permittedSubtreesURI, (ISet)dictionaryEntry.Value);
					break;
				case 7:
					this.permittedSubtreesIP = this.IntersectIP(this.permittedSubtreesIP, (ISet)dictionaryEntry.Value);
					break;
				}
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x000C132F File Offset: 0x000BF52F
		private string ExtractNameAsString(GeneralName name)
		{
			return DerIA5String.GetInstance(name.Name).GetString();
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000C1344 File Offset: 0x000BF544
		public void IntersectEmptyPermittedSubtree(int nameType)
		{
			switch (nameType)
			{
			case 1:
				this.permittedSubtreesEmail = new HashSet();
				return;
			case 2:
				this.permittedSubtreesDNS = new HashSet();
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.permittedSubtreesDN = new HashSet();
				return;
			case 6:
				this.permittedSubtreesURI = new HashSet();
				return;
			case 7:
				this.permittedSubtreesIP = new HashSet();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000C13B4 File Offset: 0x000BF5B4
		public void AddExcludedSubtree(GeneralSubtree subtree)
		{
			GeneralName @base = subtree.Base;
			switch (@base.TagNo)
			{
			case 1:
				this.excludedSubtreesEmail = this.UnionEmail(this.excludedSubtreesEmail, this.ExtractNameAsString(@base));
				return;
			case 2:
				this.excludedSubtreesDNS = this.unionDNS(this.excludedSubtreesDNS, this.ExtractNameAsString(@base));
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.excludedSubtreesDN = this.UnionDN(this.excludedSubtreesDN, (Asn1Sequence)@base.Name.ToAsn1Object());
				return;
			case 6:
				this.excludedSubtreesURI = this.unionURI(this.excludedSubtreesURI, this.ExtractNameAsString(@base));
				return;
			case 7:
				this.excludedSubtreesIP = this.UnionIP(this.excludedSubtreesIP, Asn1OctetString.GetInstance(@base.Name).GetOctets());
				break;
			default:
				return;
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000C1488 File Offset: 0x000BF688
		private static byte[] Max(byte[] ip1, byte[] ip2)
		{
			for (int i = 0; i < ip1.Length; i++)
			{
				if (((int)ip1[i] & 65535) > ((int)ip2[i] & 65535))
				{
					return ip1;
				}
			}
			return ip2;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000C14BC File Offset: 0x000BF6BC
		private static byte[] Min(byte[] ip1, byte[] ip2)
		{
			for (int i = 0; i < ip1.Length; i++)
			{
				if (((int)ip1[i] & 65535) < ((int)ip2[i] & 65535))
				{
					return ip1;
				}
			}
			return ip2;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000C14EE File Offset: 0x000BF6EE
		private static int CompareTo(byte[] ip1, byte[] ip2)
		{
			if (Arrays.AreEqual(ip1, ip2))
			{
				return 0;
			}
			if (Arrays.AreEqual(PkixNameConstraintValidator.Max(ip1, ip2), ip1))
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000C1510 File Offset: 0x000BF710
		private static byte[] Or(byte[] ip1, byte[] ip2)
		{
			byte[] array = new byte[ip1.Length];
			for (int i = 0; i < ip1.Length; i++)
			{
				array[i] = (ip1[i] | ip2[i]);
			}
			return array;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000C1540 File Offset: 0x000BF740
		[Obsolete("Use GetHashCode instead")]
		public int HashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000C1548 File Offset: 0x000BF748
		public override int GetHashCode()
		{
			return this.HashCollection(this.excludedSubtreesDN) + this.HashCollection(this.excludedSubtreesDNS) + this.HashCollection(this.excludedSubtreesEmail) + this.HashCollection(this.excludedSubtreesIP) + this.HashCollection(this.excludedSubtreesURI) + this.HashCollection(this.permittedSubtreesDN) + this.HashCollection(this.permittedSubtreesDNS) + this.HashCollection(this.permittedSubtreesEmail) + this.HashCollection(this.permittedSubtreesIP) + this.HashCollection(this.permittedSubtreesURI);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000C15D8 File Offset: 0x000BF7D8
		private int HashCollection(ICollection coll)
		{
			if (coll == null)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in coll)
			{
				if (obj is byte[])
				{
					num += Arrays.GetHashCode((byte[])obj);
				}
				else
				{
					num += obj.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000C1628 File Offset: 0x000BF828
		public override bool Equals(object o)
		{
			if (!(o is PkixNameConstraintValidator))
			{
				return false;
			}
			PkixNameConstraintValidator pkixNameConstraintValidator = (PkixNameConstraintValidator)o;
			return this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesDN, this.excludedSubtreesDN) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesDNS, this.excludedSubtreesDNS) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesEmail, this.excludedSubtreesEmail) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesIP, this.excludedSubtreesIP) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesURI, this.excludedSubtreesURI) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesDN, this.permittedSubtreesDN) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesDNS, this.permittedSubtreesDNS) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesEmail, this.permittedSubtreesEmail) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesIP, this.permittedSubtreesIP) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesURI, this.permittedSubtreesURI);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x000C1718 File Offset: 0x000BF918
		private bool CollectionsAreEqual(ICollection coll1, ICollection coll2)
		{
			if (coll1 == coll2)
			{
				return true;
			}
			if (coll1 == null || coll2 == null)
			{
				return false;
			}
			if (coll1.Count != coll2.Count)
			{
				return false;
			}
			foreach (object o in coll1)
			{
				IEnumerator enumerator2 = coll2.GetEnumerator();
				bool flag = false;
				while (enumerator2.MoveNext())
				{
					object o2 = enumerator2.Current;
					if (this.SpecialEquals(o, o2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x000C178B File Offset: 0x000BF98B
		private bool SpecialEquals(object o1, object o2)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			if (o1 is byte[] && o2 is byte[])
			{
				return Arrays.AreEqual((byte[])o1, (byte[])o2);
			}
			return o1.Equals(o2);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x000C17C4 File Offset: 0x000BF9C4
		private string StringifyIP(byte[] ip)
		{
			string text = "";
			for (int i = 0; i < ip.Length / 2; i++)
			{
				text = text + (int)(ip[i] & byte.MaxValue) + ".";
			}
			text = text.Substring(0, text.Length - 1);
			text += "/";
			for (int j = ip.Length / 2; j < ip.Length; j++)
			{
				text = text + (int)(ip[j] & byte.MaxValue) + ".";
			}
			return text.Substring(0, text.Length - 1);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x000C185C File Offset: 0x000BFA5C
		private string StringifyIPCollection(ISet ips)
		{
			string text = "";
			text += "[";
			foreach (object obj in ips)
			{
				text = text + this.StringifyIP((byte[])obj) + ",";
			}
			if (text.Length > 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text + "]";
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x000C18D0 File Offset: 0x000BFAD0
		public override string ToString()
		{
			string text = "";
			text += "permitted:\n";
			if (this.permittedSubtreesDN != null)
			{
				text += "DN:\n";
				text = text + this.permittedSubtreesDN.ToString() + "\n";
			}
			if (this.permittedSubtreesDNS != null)
			{
				text += "DNS:\n";
				text = text + this.permittedSubtreesDNS.ToString() + "\n";
			}
			if (this.permittedSubtreesEmail != null)
			{
				text += "Email:\n";
				text = text + this.permittedSubtreesEmail.ToString() + "\n";
			}
			if (this.permittedSubtreesURI != null)
			{
				text += "URI:\n";
				text = text + this.permittedSubtreesURI.ToString() + "\n";
			}
			if (this.permittedSubtreesIP != null)
			{
				text += "IP:\n";
				text = text + this.StringifyIPCollection(this.permittedSubtreesIP) + "\n";
			}
			text += "excluded:\n";
			if (!this.excludedSubtreesDN.IsEmpty)
			{
				text += "DN:\n";
				text = text + this.excludedSubtreesDN.ToString() + "\n";
			}
			if (!this.excludedSubtreesDNS.IsEmpty)
			{
				text += "DNS:\n";
				text = text + this.excludedSubtreesDNS.ToString() + "\n";
			}
			if (!this.excludedSubtreesEmail.IsEmpty)
			{
				text += "Email:\n";
				text = text + this.excludedSubtreesEmail.ToString() + "\n";
			}
			if (!this.excludedSubtreesURI.IsEmpty)
			{
				text += "URI:\n";
				text = text + this.excludedSubtreesURI.ToString() + "\n";
			}
			if (!this.excludedSubtreesIP.IsEmpty)
			{
				text += "IP:\n";
				text = text + this.StringifyIPCollection(this.excludedSubtreesIP) + "\n";
			}
			return text;
		}

		// Token: 0x04001746 RID: 5958
		private ISet excludedSubtreesDN = new HashSet();

		// Token: 0x04001747 RID: 5959
		private ISet excludedSubtreesDNS = new HashSet();

		// Token: 0x04001748 RID: 5960
		private ISet excludedSubtreesEmail = new HashSet();

		// Token: 0x04001749 RID: 5961
		private ISet excludedSubtreesURI = new HashSet();

		// Token: 0x0400174A RID: 5962
		private ISet excludedSubtreesIP = new HashSet();

		// Token: 0x0400174B RID: 5963
		private ISet permittedSubtreesDN;

		// Token: 0x0400174C RID: 5964
		private ISet permittedSubtreesDNS;

		// Token: 0x0400174D RID: 5965
		private ISet permittedSubtreesEmail;

		// Token: 0x0400174E RID: 5966
		private ISet permittedSubtreesURI;

		// Token: 0x0400174F RID: 5967
		private ISet permittedSubtreesIP;
	}
}
