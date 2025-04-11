using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020006C4 RID: 1732
	public class MessageImprint : Asn1Encodable
	{
		// Token: 0x06004019 RID: 16409 RVA: 0x00180F5B File Offset: 0x0017F15B
		public static MessageImprint GetInstance(object o)
		{
			if (o == null || o is MessageImprint)
			{
				return (MessageImprint)o;
			}
			if (o is Asn1Sequence)
			{
				return new MessageImprint((Asn1Sequence)o);
			}
			throw new ArgumentException("Unknown object in 'MessageImprint' factory: " + Platform.GetTypeName(o));
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x00180F98 File Offset: 0x0017F198
		private MessageImprint(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.hashedMessage = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x00180FED File Offset: 0x0017F1ED
		public MessageImprint(AlgorithmIdentifier hashAlgorithm, byte[] hashedMessage)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.hashedMessage = hashedMessage;
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x00181003 File Offset: 0x0017F203
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x0018100B File Offset: 0x0017F20B
		public byte[] GetHashedMessage()
		{
			return this.hashedMessage;
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x00181013 File Offset: 0x0017F213
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				new DerOctetString(this.hashedMessage)
			});
		}

		// Token: 0x040027EF RID: 10223
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x040027F0 RID: 10224
		private readonly byte[] hashedMessage;
	}
}
