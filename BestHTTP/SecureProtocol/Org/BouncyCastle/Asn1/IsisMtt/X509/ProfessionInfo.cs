using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000716 RID: 1814
	public class ProfessionInfo : Asn1Encodable
	{
		// Token: 0x06004231 RID: 16945 RVA: 0x00188728 File Offset: 0x00186928
		public static ProfessionInfo GetInstance(object obj)
		{
			if (obj == null || obj is ProfessionInfo)
			{
				return (ProfessionInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ProfessionInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x00188778 File Offset: 0x00186978
		private ProfessionInfo(Asn1Sequence seq)
		{
			if (seq.Count > 5)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Bad tag number: " + asn1TaggedObject.TagNo);
				}
				this.namingAuthority = NamingAuthority.GetInstance(asn1TaggedObject, true);
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			this.professionItems = Asn1Sequence.GetInstance(asn1Encodable);
			if (enumerator.MoveNext())
			{
				asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is Asn1Sequence)
				{
					this.professionOids = Asn1Sequence.GetInstance(asn1Encodable);
				}
				else if (asn1Encodable is DerPrintableString)
				{
					this.registrationNumber = DerPrintableString.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is Asn1OctetString))
					{
						throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable));
					}
					this.addProfessionInfo = Asn1OctetString.GetInstance(asn1Encodable);
				}
			}
			if (enumerator.MoveNext())
			{
				asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is DerPrintableString)
				{
					this.registrationNumber = DerPrintableString.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is DerOctetString))
					{
						throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable));
					}
					this.addProfessionInfo = (DerOctetString)asn1Encodable;
				}
			}
			if (!enumerator.MoveNext())
			{
				return;
			}
			asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is DerOctetString)
			{
				this.addProfessionInfo = (DerOctetString)asn1Encodable;
				return;
			}
			throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable));
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x00188934 File Offset: 0x00186B34
		public ProfessionInfo(NamingAuthority namingAuthority, DirectoryString[] professionItems, DerObjectIdentifier[] professionOids, string registrationNumber, Asn1OctetString addProfessionInfo)
		{
			this.namingAuthority = namingAuthority;
			this.professionItems = new DerSequence(professionItems);
			if (professionOids != null)
			{
				this.professionOids = new DerSequence(professionOids);
			}
			this.registrationNumber = registrationNumber;
			this.addProfessionInfo = addProfessionInfo;
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x00188980 File Offset: 0x00186B80
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.namingAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.namingAuthority)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.professionItems
			});
			if (this.professionOids != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.professionOids
				});
			}
			if (this.registrationNumber != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerPrintableString(this.registrationNumber, true)
				});
			}
			if (this.addProfessionInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.addProfessionInfo
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x00188A34 File Offset: 0x00186C34
		public virtual Asn1OctetString AddProfessionInfo
		{
			get
			{
				return this.addProfessionInfo;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x00188A3C File Offset: 0x00186C3C
		public virtual NamingAuthority NamingAuthority
		{
			get
			{
				return this.namingAuthority;
			}
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x00188A44 File Offset: 0x00186C44
		public virtual DirectoryString[] GetProfessionItems()
		{
			DirectoryString[] array = new DirectoryString[this.professionItems.Count];
			for (int i = 0; i < this.professionItems.Count; i++)
			{
				array[i] = DirectoryString.GetInstance(this.professionItems[i]);
			}
			return array;
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x00188A90 File Offset: 0x00186C90
		public virtual DerObjectIdentifier[] GetProfessionOids()
		{
			if (this.professionOids == null)
			{
				return new DerObjectIdentifier[0];
			}
			DerObjectIdentifier[] array = new DerObjectIdentifier[this.professionOids.Count];
			for (int i = 0; i < this.professionOids.Count; i++)
			{
				array[i] = DerObjectIdentifier.GetInstance(this.professionOids[i]);
			}
			return array;
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x00188AE8 File Offset: 0x00186CE8
		public virtual string RegistrationNumber
		{
			get
			{
				return this.registrationNumber;
			}
		}

		// Token: 0x04002A39 RID: 10809
		public static readonly DerObjectIdentifier Rechtsanwltin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".1");

		// Token: 0x04002A3A RID: 10810
		public static readonly DerObjectIdentifier Rechtsanwalt = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".2");

		// Token: 0x04002A3B RID: 10811
		public static readonly DerObjectIdentifier Rechtsbeistand = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".3");

		// Token: 0x04002A3C RID: 10812
		public static readonly DerObjectIdentifier Steuerberaterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".4");

		// Token: 0x04002A3D RID: 10813
		public static readonly DerObjectIdentifier Steuerberater = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".5");

		// Token: 0x04002A3E RID: 10814
		public static readonly DerObjectIdentifier Steuerbevollmchtigte = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".6");

		// Token: 0x04002A3F RID: 10815
		public static readonly DerObjectIdentifier Steuerbevollmchtigter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".7");

		// Token: 0x04002A40 RID: 10816
		public static readonly DerObjectIdentifier Notarin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".8");

		// Token: 0x04002A41 RID: 10817
		public static readonly DerObjectIdentifier Notar = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".9");

		// Token: 0x04002A42 RID: 10818
		public static readonly DerObjectIdentifier Notarvertreterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".10");

		// Token: 0x04002A43 RID: 10819
		public static readonly DerObjectIdentifier Notarvertreter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".11");

		// Token: 0x04002A44 RID: 10820
		public static readonly DerObjectIdentifier Notariatsverwalterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".12");

		// Token: 0x04002A45 RID: 10821
		public static readonly DerObjectIdentifier Notariatsverwalter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".13");

		// Token: 0x04002A46 RID: 10822
		public static readonly DerObjectIdentifier Wirtschaftsprferin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".14");

		// Token: 0x04002A47 RID: 10823
		public static readonly DerObjectIdentifier Wirtschaftsprfer = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".15");

		// Token: 0x04002A48 RID: 10824
		public static readonly DerObjectIdentifier VereidigteBuchprferin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".16");

		// Token: 0x04002A49 RID: 10825
		public static readonly DerObjectIdentifier VereidigterBuchprfer = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".17");

		// Token: 0x04002A4A RID: 10826
		public static readonly DerObjectIdentifier Patentanwltin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".18");

		// Token: 0x04002A4B RID: 10827
		public static readonly DerObjectIdentifier Patentanwalt = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".19");

		// Token: 0x04002A4C RID: 10828
		private readonly NamingAuthority namingAuthority;

		// Token: 0x04002A4D RID: 10829
		private readonly Asn1Sequence professionItems;

		// Token: 0x04002A4E RID: 10830
		private readonly Asn1Sequence professionOids;

		// Token: 0x04002A4F RID: 10831
		private readonly string registrationNumber;

		// Token: 0x04002A50 RID: 10832
		private readonly Asn1OctetString addProfessionInfo;
	}
}
