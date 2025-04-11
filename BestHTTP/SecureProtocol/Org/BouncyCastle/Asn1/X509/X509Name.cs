using System;
using System.Collections;
using System.IO;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006B0 RID: 1712
	public class X509Name : Asn1Encodable
	{
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x0017D5DF File Offset: 0x0017B7DF
		// (set) Token: 0x06003F84 RID: 16260 RVA: 0x0017D5E8 File Offset: 0x0017B7E8
		public static bool DefaultReverse
		{
			get
			{
				return X509Name.defaultReverse[0];
			}
			set
			{
				X509Name.defaultReverse[0] = value;
			}
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x0017D5F4 File Offset: 0x0017B7F4
		static X509Name()
		{
			X509Name.DefaultSymbols.Add(X509Name.C, "C");
			X509Name.DefaultSymbols.Add(X509Name.O, "O");
			X509Name.DefaultSymbols.Add(X509Name.T, "T");
			X509Name.DefaultSymbols.Add(X509Name.OU, "OU");
			X509Name.DefaultSymbols.Add(X509Name.CN, "CN");
			X509Name.DefaultSymbols.Add(X509Name.L, "L");
			X509Name.DefaultSymbols.Add(X509Name.ST, "ST");
			X509Name.DefaultSymbols.Add(X509Name.SerialNumber, "SERIALNUMBER");
			X509Name.DefaultSymbols.Add(X509Name.EmailAddress, "E");
			X509Name.DefaultSymbols.Add(X509Name.DC, "DC");
			X509Name.DefaultSymbols.Add(X509Name.UID, "UID");
			X509Name.DefaultSymbols.Add(X509Name.Street, "STREET");
			X509Name.DefaultSymbols.Add(X509Name.Surname, "SURNAME");
			X509Name.DefaultSymbols.Add(X509Name.GivenName, "GIVENNAME");
			X509Name.DefaultSymbols.Add(X509Name.Initials, "INITIALS");
			X509Name.DefaultSymbols.Add(X509Name.Generation, "GENERATION");
			X509Name.DefaultSymbols.Add(X509Name.UnstructuredAddress, "unstructuredAddress");
			X509Name.DefaultSymbols.Add(X509Name.UnstructuredName, "unstructuredName");
			X509Name.DefaultSymbols.Add(X509Name.UniqueIdentifier, "UniqueIdentifier");
			X509Name.DefaultSymbols.Add(X509Name.DnQualifier, "DN");
			X509Name.DefaultSymbols.Add(X509Name.Pseudonym, "Pseudonym");
			X509Name.DefaultSymbols.Add(X509Name.PostalAddress, "PostalAddress");
			X509Name.DefaultSymbols.Add(X509Name.NameAtBirth, "NameAtBirth");
			X509Name.DefaultSymbols.Add(X509Name.CountryOfCitizenship, "CountryOfCitizenship");
			X509Name.DefaultSymbols.Add(X509Name.CountryOfResidence, "CountryOfResidence");
			X509Name.DefaultSymbols.Add(X509Name.Gender, "Gender");
			X509Name.DefaultSymbols.Add(X509Name.PlaceOfBirth, "PlaceOfBirth");
			X509Name.DefaultSymbols.Add(X509Name.DateOfBirth, "DateOfBirth");
			X509Name.DefaultSymbols.Add(X509Name.PostalCode, "PostalCode");
			X509Name.DefaultSymbols.Add(X509Name.BusinessCategory, "BusinessCategory");
			X509Name.DefaultSymbols.Add(X509Name.TelephoneNumber, "TelephoneNumber");
			X509Name.RFC2253Symbols.Add(X509Name.C, "C");
			X509Name.RFC2253Symbols.Add(X509Name.O, "O");
			X509Name.RFC2253Symbols.Add(X509Name.OU, "OU");
			X509Name.RFC2253Symbols.Add(X509Name.CN, "CN");
			X509Name.RFC2253Symbols.Add(X509Name.L, "L");
			X509Name.RFC2253Symbols.Add(X509Name.ST, "ST");
			X509Name.RFC2253Symbols.Add(X509Name.Street, "STREET");
			X509Name.RFC2253Symbols.Add(X509Name.DC, "DC");
			X509Name.RFC2253Symbols.Add(X509Name.UID, "UID");
			X509Name.RFC1779Symbols.Add(X509Name.C, "C");
			X509Name.RFC1779Symbols.Add(X509Name.O, "O");
			X509Name.RFC1779Symbols.Add(X509Name.OU, "OU");
			X509Name.RFC1779Symbols.Add(X509Name.CN, "CN");
			X509Name.RFC1779Symbols.Add(X509Name.L, "L");
			X509Name.RFC1779Symbols.Add(X509Name.ST, "ST");
			X509Name.RFC1779Symbols.Add(X509Name.Street, "STREET");
			X509Name.DefaultLookup.Add("c", X509Name.C);
			X509Name.DefaultLookup.Add("o", X509Name.O);
			X509Name.DefaultLookup.Add("t", X509Name.T);
			X509Name.DefaultLookup.Add("ou", X509Name.OU);
			X509Name.DefaultLookup.Add("cn", X509Name.CN);
			X509Name.DefaultLookup.Add("l", X509Name.L);
			X509Name.DefaultLookup.Add("st", X509Name.ST);
			X509Name.DefaultLookup.Add("serialnumber", X509Name.SerialNumber);
			X509Name.DefaultLookup.Add("street", X509Name.Street);
			X509Name.DefaultLookup.Add("emailaddress", X509Name.E);
			X509Name.DefaultLookup.Add("dc", X509Name.DC);
			X509Name.DefaultLookup.Add("e", X509Name.E);
			X509Name.DefaultLookup.Add("uid", X509Name.UID);
			X509Name.DefaultLookup.Add("surname", X509Name.Surname);
			X509Name.DefaultLookup.Add("givenname", X509Name.GivenName);
			X509Name.DefaultLookup.Add("initials", X509Name.Initials);
			X509Name.DefaultLookup.Add("generation", X509Name.Generation);
			X509Name.DefaultLookup.Add("unstructuredaddress", X509Name.UnstructuredAddress);
			X509Name.DefaultLookup.Add("unstructuredname", X509Name.UnstructuredName);
			X509Name.DefaultLookup.Add("uniqueidentifier", X509Name.UniqueIdentifier);
			X509Name.DefaultLookup.Add("dn", X509Name.DnQualifier);
			X509Name.DefaultLookup.Add("pseudonym", X509Name.Pseudonym);
			X509Name.DefaultLookup.Add("postaladdress", X509Name.PostalAddress);
			X509Name.DefaultLookup.Add("nameofbirth", X509Name.NameAtBirth);
			X509Name.DefaultLookup.Add("countryofcitizenship", X509Name.CountryOfCitizenship);
			X509Name.DefaultLookup.Add("countryofresidence", X509Name.CountryOfResidence);
			X509Name.DefaultLookup.Add("gender", X509Name.Gender);
			X509Name.DefaultLookup.Add("placeofbirth", X509Name.PlaceOfBirth);
			X509Name.DefaultLookup.Add("dateofbirth", X509Name.DateOfBirth);
			X509Name.DefaultLookup.Add("postalcode", X509Name.PostalCode);
			X509Name.DefaultLookup.Add("businesscategory", X509Name.BusinessCategory);
			X509Name.DefaultLookup.Add("telephonenumber", X509Name.TelephoneNumber);
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x0017DE4A File Offset: 0x0017C04A
		public static X509Name GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509Name.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x0017DE58 File Offset: 0x0017C058
		public static X509Name GetInstance(object obj)
		{
			if (obj == null || obj is X509Name)
			{
				return (X509Name)obj;
			}
			if (obj != null)
			{
				return new X509Name(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("null object in factory", "obj");
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x0017DE8A File Offset: 0x0017C08A
		protected X509Name()
		{
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x0017DEB4 File Offset: 0x0017C0B4
		protected X509Name(Asn1Sequence seq)
		{
			this.seq = seq;
			foreach (object obj in seq)
			{
				Asn1Set instance = Asn1Set.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
				for (int i = 0; i < instance.Count; i++)
				{
					Asn1Sequence instance2 = Asn1Sequence.GetInstance(instance[i].ToAsn1Object());
					if (instance2.Count != 2)
					{
						throw new ArgumentException("badly sized pair");
					}
					this.ordering.Add(DerObjectIdentifier.GetInstance(instance2[0].ToAsn1Object()));
					Asn1Object asn1Object = instance2[1].ToAsn1Object();
					if (asn1Object is IAsn1String && !(asn1Object is DerUniversalString))
					{
						string text = ((IAsn1String)asn1Object).GetString();
						if (Platform.StartsWith(text, "#"))
						{
							text = "\\" + text;
						}
						this.values.Add(text);
					}
					else
					{
						this.values.Add("#" + Hex.ToHexString(asn1Object.GetEncoded()));
					}
					this.added.Add(i != 0);
				}
			}
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x0017E03C File Offset: 0x0017C23C
		public X509Name(IList ordering, IDictionary attributes) : this(ordering, attributes, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x0017E04C File Offset: 0x0017C24C
		public X509Name(IList ordering, IDictionary attributes, X509NameEntryConverter converter)
		{
			this.converter = converter;
			foreach (object obj in ordering)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
				object obj2 = attributes[derObjectIdentifier];
				if (obj2 == null)
				{
					throw new ArgumentException("No attribute for object id - " + derObjectIdentifier + " - passed to distinguished name");
				}
				this.ordering.Add(derObjectIdentifier);
				this.added.Add(false);
				this.values.Add(obj2);
			}
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x0017E114 File Offset: 0x0017C314
		public X509Name(IList oids, IList values) : this(oids, values, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x0017E124 File Offset: 0x0017C324
		public X509Name(IList oids, IList values, X509NameEntryConverter converter)
		{
			this.converter = converter;
			if (oids.Count != values.Count)
			{
				throw new ArgumentException("'oids' must be same length as 'values'.");
			}
			for (int i = 0; i < oids.Count; i++)
			{
				this.ordering.Add(oids[i]);
				this.values.Add(values[i]);
				this.added.Add(false);
			}
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x0017E1C1 File Offset: 0x0017C3C1
		public X509Name(string dirName) : this(X509Name.DefaultReverse, X509Name.DefaultLookup, dirName)
		{
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x0017E1D4 File Offset: 0x0017C3D4
		public X509Name(string dirName, X509NameEntryConverter converter) : this(X509Name.DefaultReverse, X509Name.DefaultLookup, dirName, converter)
		{
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x0017E1E8 File Offset: 0x0017C3E8
		public X509Name(bool reverse, string dirName) : this(reverse, X509Name.DefaultLookup, dirName)
		{
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x0017E1F7 File Offset: 0x0017C3F7
		public X509Name(bool reverse, string dirName, X509NameEntryConverter converter) : this(reverse, X509Name.DefaultLookup, dirName, converter)
		{
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x0017E207 File Offset: 0x0017C407
		public X509Name(bool reverse, IDictionary lookUp, string dirName) : this(reverse, lookUp, dirName, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x0017E218 File Offset: 0x0017C418
		private DerObjectIdentifier DecodeOid(string name, IDictionary lookUp)
		{
			if (Platform.StartsWith(Platform.ToUpperInvariant(name), "OID."))
			{
				return new DerObjectIdentifier(name.Substring(4));
			}
			if (name[0] >= '0' && name[0] <= '9')
			{
				return new DerObjectIdentifier(name);
			}
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)lookUp[Platform.ToLowerInvariant(name)];
			if (derObjectIdentifier == null)
			{
				throw new ArgumentException("Unknown object id - " + name + " - passed to distinguished name");
			}
			return derObjectIdentifier;
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x0017E28C File Offset: 0x0017C48C
		public X509Name(bool reverse, IDictionary lookUp, string dirName, X509NameEntryConverter converter)
		{
			this.converter = converter;
			X509NameTokenizer x509NameTokenizer = new X509NameTokenizer(dirName);
			while (x509NameTokenizer.HasMoreTokens())
			{
				string text = x509NameTokenizer.NextToken();
				int num = text.IndexOf('=');
				if (num == -1)
				{
					throw new ArgumentException("badly formated directory string");
				}
				string name = text.Substring(0, num);
				string text2 = text.Substring(num + 1);
				DerObjectIdentifier value = this.DecodeOid(name, lookUp);
				if (text2.IndexOf('+') > 0)
				{
					X509NameTokenizer x509NameTokenizer2 = new X509NameTokenizer(text2, '+');
					string value2 = x509NameTokenizer2.NextToken();
					this.ordering.Add(value);
					this.values.Add(value2);
					this.added.Add(false);
					while (x509NameTokenizer2.HasMoreTokens())
					{
						string text3 = x509NameTokenizer2.NextToken();
						int num2 = text3.IndexOf('=');
						string name2 = text3.Substring(0, num2);
						string value3 = text3.Substring(num2 + 1);
						this.ordering.Add(this.DecodeOid(name2, lookUp));
						this.values.Add(value3);
						this.added.Add(true);
					}
				}
				else
				{
					this.ordering.Add(value);
					this.values.Add(text2);
					this.added.Add(false);
				}
			}
			if (reverse)
			{
				IList list = Platform.CreateArrayList();
				IList list2 = Platform.CreateArrayList();
				IList list3 = Platform.CreateArrayList();
				int num3 = 1;
				for (int i = 0; i < this.ordering.Count; i++)
				{
					if (!(bool)this.added[i])
					{
						num3 = 0;
					}
					int index = num3++;
					list.Insert(index, this.ordering[i]);
					list2.Insert(index, this.values[i]);
					list3.Insert(index, this.added[i]);
				}
				this.ordering = list;
				this.values = list2;
				this.added = list3;
			}
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x0017E4B0 File Offset: 0x0017C6B0
		public IList GetOidList()
		{
			return Platform.CreateArrayList(this.ordering);
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x0017E4BD File Offset: 0x0017C6BD
		public IList GetValueList()
		{
			return this.GetValueList(null);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x0017E4C8 File Offset: 0x0017C6C8
		public IList GetValueList(DerObjectIdentifier oid)
		{
			IList list = Platform.CreateArrayList();
			for (int num = 0; num != this.values.Count; num++)
			{
				if (oid == null || oid.Equals(this.ordering[num]))
				{
					string text = (string)this.values[num];
					if (Platform.StartsWith(text, "\\#"))
					{
						text = text.Substring(1);
					}
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x0017E538 File Offset: 0x0017C738
		public override Asn1Object ToAsn1Object()
		{
			if (this.seq == null)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
				DerObjectIdentifier derObjectIdentifier = null;
				for (int num = 0; num != this.ordering.Count; num++)
				{
					DerObjectIdentifier derObjectIdentifier2 = (DerObjectIdentifier)this.ordering[num];
					string value = (string)this.values[num];
					if (derObjectIdentifier != null && !(bool)this.added[num])
					{
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							new DerSet(asn1EncodableVector2)
						});
						asn1EncodableVector2 = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
					}
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						new DerSequence(new Asn1Encodable[]
						{
							derObjectIdentifier2,
							this.converter.GetConvertedValue(derObjectIdentifier2, value)
						})
					});
					derObjectIdentifier = derObjectIdentifier2;
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSet(asn1EncodableVector2)
				});
				this.seq = new DerSequence(asn1EncodableVector);
			}
			return this.seq;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x0017E63C File Offset: 0x0017C83C
		public bool Equivalent(X509Name other, bool inOrder)
		{
			if (!inOrder)
			{
				return this.Equivalent(other);
			}
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			int count = this.ordering.Count;
			if (count != other.ordering.Count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				object obj = (DerObjectIdentifier)this.ordering[i];
				DerObjectIdentifier obj2 = (DerObjectIdentifier)other.ordering[i];
				if (!obj.Equals(obj2))
				{
					return false;
				}
				string s = (string)this.values[i];
				string s2 = (string)other.values[i];
				if (!X509Name.equivalentStrings(s, s2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x0017E6E4 File Offset: 0x0017C8E4
		public bool Equivalent(X509Name other)
		{
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			int count = this.ordering.Count;
			if (count != other.ordering.Count)
			{
				return false;
			}
			bool[] array = new bool[count];
			int num;
			int num2;
			int num3;
			if (this.ordering[0].Equals(other.ordering[0]))
			{
				num = 0;
				num2 = count;
				num3 = 1;
			}
			else
			{
				num = count - 1;
				num2 = -1;
				num3 = -1;
			}
			for (int num4 = num; num4 != num2; num4 += num3)
			{
				bool flag = false;
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)this.ordering[num4];
				string s = (string)this.values[num4];
				for (int i = 0; i < count; i++)
				{
					if (!array[i])
					{
						DerObjectIdentifier obj = (DerObjectIdentifier)other.ordering[i];
						if (derObjectIdentifier.Equals(obj))
						{
							string s2 = (string)other.values[i];
							if (X509Name.equivalentStrings(s, s2))
							{
								array[i] = true;
								flag = true;
								break;
							}
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x0017E7F8 File Offset: 0x0017C9F8
		private static bool equivalentStrings(string s1, string s2)
		{
			string text = X509Name.canonicalize(s1);
			string text2 = X509Name.canonicalize(s2);
			if (!text.Equals(text2))
			{
				text = X509Name.stripInternalSpaces(text);
				text2 = X509Name.stripInternalSpaces(text2);
				if (!text.Equals(text2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x0017E838 File Offset: 0x0017CA38
		private static string canonicalize(string s)
		{
			string text = Platform.ToLowerInvariant(s).Trim();
			if (Platform.StartsWith(text, "#"))
			{
				Asn1Object asn1Object = X509Name.decodeObject(text);
				if (asn1Object is IAsn1String)
				{
					text = Platform.ToLowerInvariant(((IAsn1String)asn1Object).GetString()).Trim();
				}
			}
			return text;
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x0017E884 File Offset: 0x0017CA84
		private static Asn1Object decodeObject(string v)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(Hex.Decode(v.Substring(1)));
			}
			catch (IOException ex)
			{
				throw new InvalidOperationException("unknown encoding in name: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x0017E8D0 File Offset: 0x0017CAD0
		private static string stripInternalSpaces(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (str.Length != 0)
			{
				char c = str[0];
				stringBuilder.Append(c);
				for (int i = 1; i < str.Length; i++)
				{
					char c2 = str[i];
					if (c != ' ' || c2 != ' ')
					{
						stringBuilder.Append(c2);
					}
					c = c2;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x0017E930 File Offset: 0x0017CB30
		private void AppendValue(StringBuilder buf, IDictionary oidSymbols, DerObjectIdentifier oid, string val)
		{
			string text = (string)oidSymbols[oid];
			if (text != null)
			{
				buf.Append(text);
			}
			else
			{
				buf.Append(oid.Id);
			}
			buf.Append('=');
			int num = buf.Length;
			buf.Append(val);
			int num2 = buf.Length;
			if (Platform.StartsWith(val, "\\#"))
			{
				num += 2;
			}
			while (num != num2)
			{
				if (buf[num] == ',' || buf[num] == '"' || buf[num] == '\\' || buf[num] == '+' || buf[num] == '=' || buf[num] == '<' || buf[num] == '>' || buf[num] == ';')
				{
					buf.Insert(num++, "\\");
					num2++;
				}
				num++;
			}
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x0017EA10 File Offset: 0x0017CC10
		public string ToString(bool reverse, IDictionary oidSymbols)
		{
			ArrayList arrayList = new ArrayList();
			StringBuilder stringBuilder = null;
			for (int i = 0; i < this.ordering.Count; i++)
			{
				if ((bool)this.added[i])
				{
					stringBuilder.Append('+');
					this.AppendValue(stringBuilder, oidSymbols, (DerObjectIdentifier)this.ordering[i], (string)this.values[i]);
				}
				else
				{
					stringBuilder = new StringBuilder();
					this.AppendValue(stringBuilder, oidSymbols, (DerObjectIdentifier)this.ordering[i], (string)this.values[i]);
					arrayList.Add(stringBuilder);
				}
			}
			if (reverse)
			{
				arrayList.Reverse();
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (arrayList.Count > 0)
			{
				stringBuilder2.Append(arrayList[0].ToString());
				for (int j = 1; j < arrayList.Count; j++)
				{
					stringBuilder2.Append(',');
					stringBuilder2.Append(arrayList[j].ToString());
				}
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x0017EB24 File Offset: 0x0017CD24
		public override string ToString()
		{
			return this.ToString(X509Name.DefaultReverse, X509Name.DefaultSymbols);
		}

		// Token: 0x0400274C RID: 10060
		public static readonly DerObjectIdentifier C = new DerObjectIdentifier("2.5.4.6");

		// Token: 0x0400274D RID: 10061
		public static readonly DerObjectIdentifier O = new DerObjectIdentifier("2.5.4.10");

		// Token: 0x0400274E RID: 10062
		public static readonly DerObjectIdentifier OU = new DerObjectIdentifier("2.5.4.11");

		// Token: 0x0400274F RID: 10063
		public static readonly DerObjectIdentifier T = new DerObjectIdentifier("2.5.4.12");

		// Token: 0x04002750 RID: 10064
		public static readonly DerObjectIdentifier CN = new DerObjectIdentifier("2.5.4.3");

		// Token: 0x04002751 RID: 10065
		public static readonly DerObjectIdentifier Street = new DerObjectIdentifier("2.5.4.9");

		// Token: 0x04002752 RID: 10066
		public static readonly DerObjectIdentifier SerialNumber = new DerObjectIdentifier("2.5.4.5");

		// Token: 0x04002753 RID: 10067
		public static readonly DerObjectIdentifier L = new DerObjectIdentifier("2.5.4.7");

		// Token: 0x04002754 RID: 10068
		public static readonly DerObjectIdentifier ST = new DerObjectIdentifier("2.5.4.8");

		// Token: 0x04002755 RID: 10069
		public static readonly DerObjectIdentifier Surname = new DerObjectIdentifier("2.5.4.4");

		// Token: 0x04002756 RID: 10070
		public static readonly DerObjectIdentifier GivenName = new DerObjectIdentifier("2.5.4.42");

		// Token: 0x04002757 RID: 10071
		public static readonly DerObjectIdentifier Initials = new DerObjectIdentifier("2.5.4.43");

		// Token: 0x04002758 RID: 10072
		public static readonly DerObjectIdentifier Generation = new DerObjectIdentifier("2.5.4.44");

		// Token: 0x04002759 RID: 10073
		public static readonly DerObjectIdentifier UniqueIdentifier = new DerObjectIdentifier("2.5.4.45");

		// Token: 0x0400275A RID: 10074
		public static readonly DerObjectIdentifier BusinessCategory = new DerObjectIdentifier("2.5.4.15");

		// Token: 0x0400275B RID: 10075
		public static readonly DerObjectIdentifier PostalCode = new DerObjectIdentifier("2.5.4.17");

		// Token: 0x0400275C RID: 10076
		public static readonly DerObjectIdentifier DnQualifier = new DerObjectIdentifier("2.5.4.46");

		// Token: 0x0400275D RID: 10077
		public static readonly DerObjectIdentifier Pseudonym = new DerObjectIdentifier("2.5.4.65");

		// Token: 0x0400275E RID: 10078
		public static readonly DerObjectIdentifier DateOfBirth = new DerObjectIdentifier("1.3.6.1.5.5.7.9.1");

		// Token: 0x0400275F RID: 10079
		public static readonly DerObjectIdentifier PlaceOfBirth = new DerObjectIdentifier("1.3.6.1.5.5.7.9.2");

		// Token: 0x04002760 RID: 10080
		public static readonly DerObjectIdentifier Gender = new DerObjectIdentifier("1.3.6.1.5.5.7.9.3");

		// Token: 0x04002761 RID: 10081
		public static readonly DerObjectIdentifier CountryOfCitizenship = new DerObjectIdentifier("1.3.6.1.5.5.7.9.4");

		// Token: 0x04002762 RID: 10082
		public static readonly DerObjectIdentifier CountryOfResidence = new DerObjectIdentifier("1.3.6.1.5.5.7.9.5");

		// Token: 0x04002763 RID: 10083
		public static readonly DerObjectIdentifier NameAtBirth = new DerObjectIdentifier("1.3.36.8.3.14");

		// Token: 0x04002764 RID: 10084
		public static readonly DerObjectIdentifier PostalAddress = new DerObjectIdentifier("2.5.4.16");

		// Token: 0x04002765 RID: 10085
		public static readonly DerObjectIdentifier DmdName = new DerObjectIdentifier("2.5.4.54");

		// Token: 0x04002766 RID: 10086
		public static readonly DerObjectIdentifier TelephoneNumber = X509ObjectIdentifiers.id_at_telephoneNumber;

		// Token: 0x04002767 RID: 10087
		public static readonly DerObjectIdentifier OrganizationIdentifier = X509ObjectIdentifiers.id_at_organizationIdentifier;

		// Token: 0x04002768 RID: 10088
		public static readonly DerObjectIdentifier Name = X509ObjectIdentifiers.id_at_name;

		// Token: 0x04002769 RID: 10089
		public static readonly DerObjectIdentifier EmailAddress = PkcsObjectIdentifiers.Pkcs9AtEmailAddress;

		// Token: 0x0400276A RID: 10090
		public static readonly DerObjectIdentifier UnstructuredName = PkcsObjectIdentifiers.Pkcs9AtUnstructuredName;

		// Token: 0x0400276B RID: 10091
		public static readonly DerObjectIdentifier UnstructuredAddress = PkcsObjectIdentifiers.Pkcs9AtUnstructuredAddress;

		// Token: 0x0400276C RID: 10092
		public static readonly DerObjectIdentifier E = X509Name.EmailAddress;

		// Token: 0x0400276D RID: 10093
		public static readonly DerObjectIdentifier DC = new DerObjectIdentifier("0.9.2342.19200300.100.1.25");

		// Token: 0x0400276E RID: 10094
		public static readonly DerObjectIdentifier UID = new DerObjectIdentifier("0.9.2342.19200300.100.1.1");

		// Token: 0x0400276F RID: 10095
		private static readonly bool[] defaultReverse = new bool[1];

		// Token: 0x04002770 RID: 10096
		public static readonly Hashtable DefaultSymbols = new Hashtable();

		// Token: 0x04002771 RID: 10097
		public static readonly Hashtable RFC2253Symbols = new Hashtable();

		// Token: 0x04002772 RID: 10098
		public static readonly Hashtable RFC1779Symbols = new Hashtable();

		// Token: 0x04002773 RID: 10099
		public static readonly Hashtable DefaultLookup = new Hashtable();

		// Token: 0x04002774 RID: 10100
		private readonly IList ordering = Platform.CreateArrayList();

		// Token: 0x04002775 RID: 10101
		private readonly X509NameEntryConverter converter;

		// Token: 0x04002776 RID: 10102
		private IList values = Platform.CreateArrayList();

		// Token: 0x04002777 RID: 10103
		private IList added = Platform.CreateArrayList();

		// Token: 0x04002778 RID: 10104
		private Asn1Sequence seq;
	}
}
