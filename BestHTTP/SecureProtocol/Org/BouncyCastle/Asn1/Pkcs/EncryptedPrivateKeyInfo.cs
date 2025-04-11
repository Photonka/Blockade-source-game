using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006DC RID: 1756
	public class EncryptedPrivateKeyInfo : Asn1Encodable
	{
		// Token: 0x060040B2 RID: 16562 RVA: 0x00183364 File Offset: 0x00181564
		private EncryptedPrivateKeyInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.algId = AlgorithmIdentifier.GetInstance(seq[0]);
			this.data = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x001833B4 File Offset: 0x001815B4
		public EncryptedPrivateKeyInfo(AlgorithmIdentifier algId, byte[] encoding)
		{
			this.algId = algId;
			this.data = new DerOctetString(encoding);
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x001833CF File Offset: 0x001815CF
		public static EncryptedPrivateKeyInfo GetInstance(object obj)
		{
			if (obj is EncryptedPrivateKeyInfo)
			{
				return (EncryptedPrivateKeyInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedPrivateKeyInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x0018340E File Offset: 0x0018160E
		public AlgorithmIdentifier EncryptionAlgorithm
		{
			get
			{
				return this.algId;
			}
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x00183416 File Offset: 0x00181616
		public byte[] GetEncryptedData()
		{
			return this.data.GetOctets();
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x00183423 File Offset: 0x00181623
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algId,
				this.data
			});
		}

		// Token: 0x04002885 RID: 10373
		private readonly AlgorithmIdentifier algId;

		// Token: 0x04002886 RID: 10374
		private readonly Asn1OctetString data;
	}
}
