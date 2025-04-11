using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000676 RID: 1654
	public class AuthorityInformationAccess : Asn1Encodable
	{
		// Token: 0x06003DA3 RID: 15779 RVA: 0x001775ED File Offset: 0x001757ED
		public static AuthorityInformationAccess GetInstance(object obj)
		{
			if (obj is AuthorityInformationAccess)
			{
				return (AuthorityInformationAccess)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new AuthorityInformationAccess(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x00177610 File Offset: 0x00175810
		private AuthorityInformationAccess(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("sequence may not be empty");
			}
			this.descriptions = new AccessDescription[seq.Count];
			for (int i = 0; i < seq.Count; i++)
			{
				this.descriptions[i] = AccessDescription.GetInstance(seq[i]);
			}
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x0017766D File Offset: 0x0017586D
		public AuthorityInformationAccess(AccessDescription description)
		{
			this.descriptions = new AccessDescription[]
			{
				description
			};
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x00177685 File Offset: 0x00175885
		public AuthorityInformationAccess(DerObjectIdentifier oid, GeneralName location) : this(new AccessDescription(oid, location))
		{
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00177694 File Offset: 0x00175894
		public AccessDescription[] GetAccessDescriptions()
		{
			return (AccessDescription[])this.descriptions.Clone();
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x001776A8 File Offset: 0x001758A8
		public override Asn1Object ToAsn1Object()
		{
			Asn1Encodable[] v = this.descriptions;
			return new DerSequence(v);
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x001776C4 File Offset: 0x001758C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("AuthorityInformationAccess:");
			stringBuilder.Append(newLine);
			foreach (AccessDescription value in this.descriptions)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(value);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400264F RID: 9807
		private readonly AccessDescription[] descriptions;
	}
}
