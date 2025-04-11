using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200069C RID: 1692
	public class SubjectPublicKeyInfo : Asn1Encodable
	{
		// Token: 0x06003EC4 RID: 16068 RVA: 0x0017B2D6 File Offset: 0x001794D6
		public static SubjectPublicKeyInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SubjectPublicKeyInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x0017B2E4 File Offset: 0x001794E4
		public static SubjectPublicKeyInfo GetInstance(object obj)
		{
			if (obj is SubjectPublicKeyInfo)
			{
				return (SubjectPublicKeyInfo)obj;
			}
			if (obj != null)
			{
				return new SubjectPublicKeyInfo(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x0017B305 File Offset: 0x00179505
		public SubjectPublicKeyInfo(AlgorithmIdentifier algID, Asn1Encodable publicKey)
		{
			this.keyData = new DerBitString(publicKey);
			this.algID = algID;
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x0017B320 File Offset: 0x00179520
		public SubjectPublicKeyInfo(AlgorithmIdentifier algID, byte[] publicKey)
		{
			this.keyData = new DerBitString(publicKey);
			this.algID = algID;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x0017B33C File Offset: 0x0017953C
		private SubjectPublicKeyInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.algID = AlgorithmIdentifier.GetInstance(seq[0]);
			this.keyData = DerBitString.GetInstance(seq[1]);
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x0017B39C File Offset: 0x0017959C
		public AlgorithmIdentifier AlgorithmID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x0017B3A4 File Offset: 0x001795A4
		public Asn1Object GetPublicKey()
		{
			return Asn1Object.FromByteArray(this.keyData.GetOctets());
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x0017B3B6 File Offset: 0x001795B6
		public DerBitString PublicKeyData
		{
			get
			{
				return this.keyData;
			}
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x0017B3BE File Offset: 0x001795BE
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algID,
				this.keyData
			});
		}

		// Token: 0x040026D9 RID: 9945
		private readonly AlgorithmIdentifier algID;

		// Token: 0x040026DA RID: 9946
		private readonly DerBitString keyData;
	}
}
