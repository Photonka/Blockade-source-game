using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x0200071B RID: 1819
	public class DataGroupHash : Asn1Encodable
	{
		// Token: 0x06004255 RID: 16981 RVA: 0x0018918D File Offset: 0x0018738D
		public static DataGroupHash GetInstance(object obj)
		{
			if (obj is DataGroupHash)
			{
				return (DataGroupHash)obj;
			}
			if (obj != null)
			{
				return new DataGroupHash(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x001891B0 File Offset: 0x001873B0
		private DataGroupHash(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.dataGroupNumber = DerInteger.GetInstance(seq[0]);
			this.dataGroupHashValue = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x00189200 File Offset: 0x00187400
		public DataGroupHash(int dataGroupNumber, Asn1OctetString dataGroupHashValue)
		{
			this.dataGroupNumber = new DerInteger(dataGroupNumber);
			this.dataGroupHashValue = dataGroupHashValue;
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06004258 RID: 16984 RVA: 0x0018921B File Offset: 0x0018741B
		public int DataGroupNumber
		{
			get
			{
				return this.dataGroupNumber.Value.IntValue;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x0018922D File Offset: 0x0018742D
		public Asn1OctetString DataGroupHashValue
		{
			get
			{
				return this.dataGroupHashValue;
			}
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x00189235 File Offset: 0x00187435
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.dataGroupNumber,
				this.dataGroupHashValue
			});
		}

		// Token: 0x04002A59 RID: 10841
		private readonly DerInteger dataGroupNumber;

		// Token: 0x04002A5A RID: 10842
		private readonly Asn1OctetString dataGroupHashValue;
	}
}
