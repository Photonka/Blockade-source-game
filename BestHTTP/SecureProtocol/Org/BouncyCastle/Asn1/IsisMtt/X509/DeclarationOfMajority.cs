using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000712 RID: 1810
	public class DeclarationOfMajority : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600420E RID: 16910 RVA: 0x00187F6D File Offset: 0x0018616D
		public DeclarationOfMajority(int notYoungerThan)
		{
			this.declaration = new DerTaggedObject(false, 0, new DerInteger(notYoungerThan));
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x00187F88 File Offset: 0x00186188
		public DeclarationOfMajority(bool fullAge, string country)
		{
			if (country.Length > 2)
			{
				throw new ArgumentException("country can only be 2 characters");
			}
			DerPrintableString derPrintableString = new DerPrintableString(country, true);
			DerSequence obj;
			if (fullAge)
			{
				obj = new DerSequence(derPrintableString);
			}
			else
			{
				obj = new DerSequence(new Asn1Encodable[]
				{
					DerBoolean.False,
					derPrintableString
				});
			}
			this.declaration = new DerTaggedObject(false, 1, obj);
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x00187FE9 File Offset: 0x001861E9
		public DeclarationOfMajority(DerGeneralizedTime dateOfBirth)
		{
			this.declaration = new DerTaggedObject(false, 2, dateOfBirth);
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x00188000 File Offset: 0x00186200
		public static DeclarationOfMajority GetInstance(object obj)
		{
			if (obj == null || obj is DeclarationOfMajority)
			{
				return (DeclarationOfMajority)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new DeclarationOfMajority((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x0018804D File Offset: 0x0018624D
		private DeclarationOfMajority(Asn1TaggedObject o)
		{
			if (o.TagNo > 2)
			{
				throw new ArgumentException("Bad tag number: " + o.TagNo);
			}
			this.declaration = o;
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x00188080 File Offset: 0x00186280
		public override Asn1Object ToAsn1Object()
		{
			return this.declaration;
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x00188088 File Offset: 0x00186288
		public DeclarationOfMajority.Choice Type
		{
			get
			{
				return (DeclarationOfMajority.Choice)this.declaration.TagNo;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06004215 RID: 16917 RVA: 0x00188098 File Offset: 0x00186298
		public virtual int NotYoungerThan
		{
			get
			{
				if (this.declaration.TagNo == 0)
				{
					return DerInteger.GetInstance(this.declaration, false).Value.IntValue;
				}
				return -1;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x001880CC File Offset: 0x001862CC
		public virtual Asn1Sequence FullAgeAtCountry
		{
			get
			{
				DeclarationOfMajority.Choice tagNo = (DeclarationOfMajority.Choice)this.declaration.TagNo;
				if (tagNo == DeclarationOfMajority.Choice.FullAgeAtCountry)
				{
					return Asn1Sequence.GetInstance(this.declaration, false);
				}
				return null;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06004217 RID: 16919 RVA: 0x001880F8 File Offset: 0x001862F8
		public virtual DerGeneralizedTime DateOfBirth
		{
			get
			{
				DeclarationOfMajority.Choice tagNo = (DeclarationOfMajority.Choice)this.declaration.TagNo;
				if (tagNo == DeclarationOfMajority.Choice.DateOfBirth)
				{
					return DerGeneralizedTime.GetInstance(this.declaration, false);
				}
				return null;
			}
		}

		// Token: 0x04002A2D RID: 10797
		private readonly Asn1TaggedObject declaration;

		// Token: 0x020009AF RID: 2479
		public enum Choice
		{
			// Token: 0x04003662 RID: 13922
			NotYoungerThan,
			// Token: 0x04003663 RID: 13923
			FullAgeAtCountry,
			// Token: 0x04003664 RID: 13924
			DateOfBirth
		}
	}
}
