using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200068C RID: 1676
	public class IssuingDistributionPoint : Asn1Encodable
	{
		// Token: 0x06003E5A RID: 15962 RVA: 0x00179D2A File Offset: 0x00177F2A
		public static IssuingDistributionPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return IssuingDistributionPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00179D38 File Offset: 0x00177F38
		public static IssuingDistributionPoint GetInstance(object obj)
		{
			if (obj == null || obj is IssuingDistributionPoint)
			{
				return (IssuingDistributionPoint)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuingDistributionPoint((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x00179D88 File Offset: 0x00177F88
		public IssuingDistributionPoint(DistributionPointName distributionPoint, bool onlyContainsUserCerts, bool onlyContainsCACerts, ReasonFlags onlySomeReasons, bool indirectCRL, bool onlyContainsAttributeCerts)
		{
			this._distributionPoint = distributionPoint;
			this._indirectCRL = indirectCRL;
			this._onlyContainsAttributeCerts = onlyContainsAttributeCerts;
			this._onlyContainsCACerts = onlyContainsCACerts;
			this._onlyContainsUserCerts = onlyContainsUserCerts;
			this._onlySomeReasons = onlySomeReasons;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (distributionPoint != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, distributionPoint)
				});
			}
			if (onlyContainsUserCerts)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, DerBoolean.True)
				});
			}
			if (onlyContainsCACerts)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, DerBoolean.True)
				});
			}
			if (onlySomeReasons != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 3, onlySomeReasons)
				});
			}
			if (indirectCRL)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 4, DerBoolean.True)
				});
			}
			if (onlyContainsAttributeCerts)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 5, DerBoolean.True)
				});
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x00179E90 File Offset: 0x00178090
		private IssuingDistributionPoint(Asn1Sequence seq)
		{
			this.seq = seq;
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this._distributionPoint = DistributionPointName.GetInstance(instance, true);
					break;
				case 1:
					this._onlyContainsUserCerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 2:
					this._onlyContainsCACerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 3:
					this._onlySomeReasons = new ReasonFlags(DerBitString.GetInstance(instance, false));
					break;
				case 4:
					this._indirectCRL = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 5:
					this._onlyContainsAttributeCerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				default:
					throw new ArgumentException("unknown tag in IssuingDistributionPoint");
				}
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06003E5E RID: 15966 RVA: 0x00179F73 File Offset: 0x00178173
		public bool OnlyContainsUserCerts
		{
			get
			{
				return this._onlyContainsUserCerts;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06003E5F RID: 15967 RVA: 0x00179F7B File Offset: 0x0017817B
		public bool OnlyContainsCACerts
		{
			get
			{
				return this._onlyContainsCACerts;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x00179F83 File Offset: 0x00178183
		public bool IsIndirectCrl
		{
			get
			{
				return this._indirectCRL;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06003E61 RID: 15969 RVA: 0x00179F8B File Offset: 0x0017818B
		public bool OnlyContainsAttributeCerts
		{
			get
			{
				return this._onlyContainsAttributeCerts;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06003E62 RID: 15970 RVA: 0x00179F93 File Offset: 0x00178193
		public DistributionPointName DistributionPoint
		{
			get
			{
				return this._distributionPoint;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06003E63 RID: 15971 RVA: 0x00179F9B File Offset: 0x0017819B
		public ReasonFlags OnlySomeReasons
		{
			get
			{
				return this._onlySomeReasons;
			}
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x00179FA3 File Offset: 0x001781A3
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x00179FAC File Offset: 0x001781AC
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IssuingDistributionPoint: [");
			stringBuilder.Append(newLine);
			if (this._distributionPoint != null)
			{
				this.appendObject(stringBuilder, newLine, "distributionPoint", this._distributionPoint.ToString());
			}
			if (this._onlyContainsUserCerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsUserCerts", this._onlyContainsUserCerts.ToString());
			}
			if (this._onlyContainsCACerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsCACerts", this._onlyContainsCACerts.ToString());
			}
			if (this._onlySomeReasons != null)
			{
				this.appendObject(stringBuilder, newLine, "onlySomeReasons", this._onlySomeReasons.ToString());
			}
			if (this._onlyContainsAttributeCerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsAttributeCerts", this._onlyContainsAttributeCerts.ToString());
			}
			if (this._indirectCRL)
			{
				this.appendObject(stringBuilder, newLine, "indirectCRL", this._indirectCRL.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x0017A0C0 File Offset: 0x001782C0
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

		// Token: 0x04002698 RID: 9880
		private readonly DistributionPointName _distributionPoint;

		// Token: 0x04002699 RID: 9881
		private readonly bool _onlyContainsUserCerts;

		// Token: 0x0400269A RID: 9882
		private readonly bool _onlyContainsCACerts;

		// Token: 0x0400269B RID: 9883
		private readonly ReasonFlags _onlySomeReasons;

		// Token: 0x0400269C RID: 9884
		private readonly bool _indirectCRL;

		// Token: 0x0400269D RID: 9885
		private readonly bool _onlyContainsAttributeCerts;

		// Token: 0x0400269E RID: 9886
		private readonly Asn1Sequence seq;
	}
}
