using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000682 RID: 1666
	public class DistributionPoint : Asn1Encodable
	{
		// Token: 0x06003DF8 RID: 15864 RVA: 0x001784F3 File Offset: 0x001766F3
		public static DistributionPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DistributionPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x00178501 File Offset: 0x00176701
		public static DistributionPoint GetInstance(object obj)
		{
			if (obj == null || obj is DistributionPoint)
			{
				return (DistributionPoint)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DistributionPoint((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DistributionPoint: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x00178540 File Offset: 0x00176740
		private DistributionPoint(Asn1Sequence seq)
		{
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this.distributionPoint = DistributionPointName.GetInstance(instance, true);
					break;
				case 1:
					this.reasons = new ReasonFlags(DerBitString.GetInstance(instance, false));
					break;
				case 2:
					this.cRLIssuer = GeneralNames.GetInstance(instance, false);
					break;
				}
			}
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x001785BC File Offset: 0x001767BC
		public DistributionPoint(DistributionPointName distributionPointName, ReasonFlags reasons, GeneralNames crlIssuer)
		{
			this.distributionPoint = distributionPointName;
			this.reasons = reasons;
			this.cRLIssuer = crlIssuer;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x001785D9 File Offset: 0x001767D9
		public DistributionPointName DistributionPointName
		{
			get
			{
				return this.distributionPoint;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x001785E1 File Offset: 0x001767E1
		public ReasonFlags Reasons
		{
			get
			{
				return this.reasons;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x001785E9 File Offset: 0x001767E9
		public GeneralNames CrlIssuer
		{
			get
			{
				return this.cRLIssuer;
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x001785F4 File Offset: 0x001767F4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.distributionPoint != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.distributionPoint)
				});
			}
			if (this.reasons != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.reasons)
				});
			}
			if (this.cRLIssuer != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.cRLIssuer)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x00178680 File Offset: 0x00176880
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistributionPoint: [");
			stringBuilder.Append(newLine);
			if (this.distributionPoint != null)
			{
				this.appendObject(stringBuilder, newLine, "distributionPoint", this.distributionPoint.ToString());
			}
			if (this.reasons != null)
			{
				this.appendObject(stringBuilder, newLine, "reasons", this.reasons.ToString());
			}
			if (this.cRLIssuer != null)
			{
				this.appendObject(stringBuilder, newLine, "cRLIssuer", this.cRLIssuer.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x00178728 File Offset: 0x00176928
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

		// Token: 0x04002670 RID: 9840
		internal readonly DistributionPointName distributionPoint;

		// Token: 0x04002671 RID: 9841
		internal readonly ReasonFlags reasons;

		// Token: 0x04002672 RID: 9842
		internal readonly GeneralNames cRLIssuer;
	}
}
