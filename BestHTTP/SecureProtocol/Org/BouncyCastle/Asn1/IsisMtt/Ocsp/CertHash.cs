using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.Ocsp
{
	// Token: 0x02000718 RID: 1816
	public class CertHash : Asn1Encodable
	{
		// Token: 0x06004240 RID: 16960 RVA: 0x00188D50 File Offset: 0x00186F50
		public static CertHash GetInstance(object obj)
		{
			if (obj == null || obj is CertHash)
			{
				return (CertHash)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertHash((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x00188DA0 File Offset: 0x00186FA0
		private CertHash(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.certificateHash = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x00188E00 File Offset: 0x00187000
		public CertHash(AlgorithmIdentifier hashAlgorithm, byte[] certificateHash)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (certificateHash == null)
			{
				throw new ArgumentNullException("certificateHash");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.certificateHash = (byte[])certificateHash.Clone();
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x00188E3C File Offset: 0x0018703C
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06004244 RID: 16964 RVA: 0x00188E44 File Offset: 0x00187044
		public byte[] CertificateHash
		{
			get
			{
				return (byte[])this.certificateHash.Clone();
			}
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x00188E56 File Offset: 0x00187056
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				new DerOctetString(this.certificateHash)
			});
		}

		// Token: 0x04002A52 RID: 10834
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04002A53 RID: 10835
		private readonly byte[] certificateHash;
	}
}
