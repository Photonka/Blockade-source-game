using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200067D RID: 1661
	public class CrlDistPoint : Asn1Encodable
	{
		// Token: 0x06003DDD RID: 15837 RVA: 0x0017809E File Offset: 0x0017629E
		public static CrlDistPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CrlDistPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x001780AC File Offset: 0x001762AC
		public static CrlDistPoint GetInstance(object obj)
		{
			if (obj is CrlDistPoint || obj == null)
			{
				return (CrlDistPoint)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlDistPoint((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x001780F9 File Offset: 0x001762F9
		private CrlDistPoint(Asn1Sequence seq)
		{
			this.seq = seq;
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x00178108 File Offset: 0x00176308
		public CrlDistPoint(DistributionPoint[] points)
		{
			this.seq = new DerSequence(points);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x0017812C File Offset: 0x0017632C
		public DistributionPoint[] GetDistributionPoints()
		{
			DistributionPoint[] array = new DistributionPoint[this.seq.Count];
			for (int num = 0; num != this.seq.Count; num++)
			{
				array[num] = DistributionPoint.GetInstance(this.seq[num]);
			}
			return array;
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x00178175 File Offset: 0x00176375
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x00178180 File Offset: 0x00176380
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("CRLDistPoint:");
			stringBuilder.Append(newLine);
			DistributionPoint[] distributionPoints = this.GetDistributionPoints();
			for (int num = 0; num != distributionPoints.Length; num++)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(distributionPoints[num]);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400265B RID: 9819
		internal readonly Asn1Sequence seq;
	}
}
