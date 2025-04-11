using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000684 RID: 1668
	public class DsaParameter : Asn1Encodable
	{
		// Token: 0x06003E0C RID: 15884 RVA: 0x00178934 File Offset: 0x00176B34
		public static DsaParameter GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DsaParameter.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x00178942 File Offset: 0x00176B42
		public static DsaParameter GetInstance(object obj)
		{
			if (obj == null || obj is DsaParameter)
			{
				return (DsaParameter)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DsaParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DsaParameter: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x0017897F File Offset: 0x00176B7F
		public DsaParameter(BigInteger p, BigInteger q, BigInteger g)
		{
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.g = new DerInteger(g);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x001789AC File Offset: 0x00176BAC
		private DsaParameter(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.p = DerInteger.GetInstance(seq[0]);
			this.q = DerInteger.GetInstance(seq[1]);
			this.g = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06003E10 RID: 15888 RVA: 0x00178A1E File Offset: 0x00176C1E
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06003E11 RID: 15889 RVA: 0x00178A2B File Offset: 0x00176C2B
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x00178A38 File Offset: 0x00176C38
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x00178A45 File Offset: 0x00176C45
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.p,
				this.q,
				this.g
			});
		}

		// Token: 0x04002677 RID: 9847
		internal readonly DerInteger p;

		// Token: 0x04002678 RID: 9848
		internal readonly DerInteger q;

		// Token: 0x04002679 RID: 9849
		internal readonly DerInteger g;
	}
}
