using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000680 RID: 1664
	public class DigestInfo : Asn1Encodable
	{
		// Token: 0x06003DEB RID: 15851 RVA: 0x001782D7 File Offset: 0x001764D7
		public static DigestInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DigestInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x001782E5 File Offset: 0x001764E5
		public static DigestInfo GetInstance(object obj)
		{
			if (obj is DigestInfo)
			{
				return (DigestInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DigestInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00178324 File Offset: 0x00176524
		public DigestInfo(AlgorithmIdentifier algID, byte[] digest)
		{
			this.digest = digest;
			this.algID = algID;
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x0017833C File Offset: 0x0017653C
		private DigestInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.algID = AlgorithmIdentifier.GetInstance(seq[0]);
			this.digest = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x00178391 File Offset: 0x00176591
		public AlgorithmIdentifier AlgorithmID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x00178399 File Offset: 0x00176599
		public byte[] GetDigest()
		{
			return this.digest;
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x001783A1 File Offset: 0x001765A1
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algID,
				new DerOctetString(this.digest)
			});
		}

		// Token: 0x04002667 RID: 9831
		private readonly byte[] digest;

		// Token: 0x04002668 RID: 9832
		private readonly AlgorithmIdentifier algID;
	}
}
