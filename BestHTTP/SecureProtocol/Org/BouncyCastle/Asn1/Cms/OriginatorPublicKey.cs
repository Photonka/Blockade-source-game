using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000780 RID: 1920
	public class OriginatorPublicKey : Asn1Encodable
	{
		// Token: 0x0600450B RID: 17675 RVA: 0x001921B6 File Offset: 0x001903B6
		public OriginatorPublicKey(AlgorithmIdentifier algorithm, byte[] publicKey)
		{
			this.mAlgorithm = algorithm;
			this.mPublicKey = new DerBitString(publicKey);
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x001921D1 File Offset: 0x001903D1
		[Obsolete("Use 'GetInstance' instead")]
		public OriginatorPublicKey(Asn1Sequence seq)
		{
			this.mAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.mPublicKey = DerBitString.GetInstance(seq[1]);
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x001921FD File Offset: 0x001903FD
		public static OriginatorPublicKey GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OriginatorPublicKey.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x0019220B File Offset: 0x0019040B
		public static OriginatorPublicKey GetInstance(object obj)
		{
			if (obj == null || obj is OriginatorPublicKey)
			{
				return (OriginatorPublicKey)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OriginatorPublicKey(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("Invalid OriginatorPublicKey: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x00192248 File Offset: 0x00190448
		public AlgorithmIdentifier Algorithm
		{
			get
			{
				return this.mAlgorithm;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x00192250 File Offset: 0x00190450
		public DerBitString PublicKey
		{
			get
			{
				return this.mPublicKey;
			}
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x00192258 File Offset: 0x00190458
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.mAlgorithm,
				this.mPublicKey
			});
		}

		// Token: 0x04002C25 RID: 11301
		private readonly AlgorithmIdentifier mAlgorithm;

		// Token: 0x04002C26 RID: 11302
		private readonly DerBitString mPublicKey;
	}
}
