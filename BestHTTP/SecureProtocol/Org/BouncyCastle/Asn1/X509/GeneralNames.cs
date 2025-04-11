using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000687 RID: 1671
	public class GeneralNames : Asn1Encodable
	{
		// Token: 0x06003E2F RID: 15919 RVA: 0x001792E8 File Offset: 0x001774E8
		public static GeneralNames GetInstance(object obj)
		{
			if (obj == null || obj is GeneralNames)
			{
				return (GeneralNames)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new GeneralNames((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x00179335 File Offset: 0x00177535
		public static GeneralNames GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return GeneralNames.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x00179343 File Offset: 0x00177543
		public GeneralNames(GeneralName name)
		{
			this.names = new GeneralName[]
			{
				name
			};
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x0017935B File Offset: 0x0017755B
		public GeneralNames(GeneralName[] names)
		{
			this.names = (GeneralName[])names.Clone();
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x00179374 File Offset: 0x00177574
		private GeneralNames(Asn1Sequence seq)
		{
			this.names = new GeneralName[seq.Count];
			for (int num = 0; num != seq.Count; num++)
			{
				this.names[num] = GeneralName.GetInstance(seq[num]);
			}
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x001793BD File Offset: 0x001775BD
		public GeneralName[] GetNames()
		{
			return (GeneralName[])this.names.Clone();
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x001793D0 File Offset: 0x001775D0
		public override Asn1Object ToAsn1Object()
		{
			Asn1Encodable[] v = this.names;
			return new DerSequence(v);
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x001793EC File Offset: 0x001775EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("GeneralNames:");
			stringBuilder.Append(newLine);
			foreach (GeneralName value in this.names)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(value);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002687 RID: 9863
		private readonly GeneralName[] names;
	}
}
