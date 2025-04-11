using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000683 RID: 1667
	public class DistributionPointName : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003E02 RID: 15874 RVA: 0x00178780 File Offset: 0x00176980
		public static DistributionPointName GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DistributionPointName.GetInstance(Asn1TaggedObject.GetInstance(obj, true));
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x00178790 File Offset: 0x00176990
		public static DistributionPointName GetInstance(object obj)
		{
			if (obj == null || obj is DistributionPointName)
			{
				return (DistributionPointName)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new DistributionPointName((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x001787DD File Offset: 0x001769DD
		public DistributionPointName(int type, Asn1Encodable name)
		{
			this.type = type;
			this.name = name;
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x001787F3 File Offset: 0x001769F3
		public DistributionPointName(GeneralNames name) : this(0, name)
		{
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x001787FD File Offset: 0x001769FD
		public int PointType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06003E07 RID: 15879 RVA: 0x00178805 File Offset: 0x00176A05
		public Asn1Encodable Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x0017880D File Offset: 0x00176A0D
		public DistributionPointName(Asn1TaggedObject obj)
		{
			this.type = obj.TagNo;
			if (this.type == 0)
			{
				this.name = GeneralNames.GetInstance(obj, false);
				return;
			}
			this.name = Asn1Set.GetInstance(obj, false);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00178844 File Offset: 0x00176A44
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.type, this.name);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00178858 File Offset: 0x00176A58
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistributionPointName: [");
			stringBuilder.Append(newLine);
			if (this.type == 0)
			{
				this.appendObject(stringBuilder, newLine, "fullName", this.name.ToString());
			}
			else
			{
				this.appendObject(stringBuilder, newLine, "nameRelativeToCRLIssuer", this.name.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x001788DC File Offset: 0x00176ADC
		private void appendObject(StringBuilder buf, string sep, string name, string val)
		{
			string value = "    ";
			buf.Append(value);
			buf.Append(name);
			buf.Append(":");
			buf.Append(sep);
			buf.Append(value);
			buf.Append(value);
			buf.Append(val);
			buf.Append(sep);
		}

		// Token: 0x04002673 RID: 9843
		internal readonly Asn1Encodable name;

		// Token: 0x04002674 RID: 9844
		internal readonly int type;

		// Token: 0x04002675 RID: 9845
		public const int FullName = 0;

		// Token: 0x04002676 RID: 9846
		public const int NameRelativeToCrlIssuer = 1;
	}
}
