using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x0200079B RID: 1947
	public class Challenge : Asn1Encodable
	{
		// Token: 0x060045DB RID: 17883 RVA: 0x00194618 File Offset: 0x00192818
		private Challenge(Asn1Sequence seq)
		{
			int index = 0;
			if (seq.Count == 3)
			{
				this.owf = AlgorithmIdentifier.GetInstance(seq[index++]);
			}
			this.witness = Asn1OctetString.GetInstance(seq[index++]);
			this.challenge = Asn1OctetString.GetInstance(seq[index]);
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x00194674 File Offset: 0x00192874
		public static Challenge GetInstance(object obj)
		{
			if (obj is Challenge)
			{
				return (Challenge)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Challenge((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060045DD RID: 17885 RVA: 0x001946B3 File Offset: 0x001928B3
		public virtual AlgorithmIdentifier Owf
		{
			get
			{
				return this.owf;
			}
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x001946BC File Offset: 0x001928BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.owf
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.witness
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.challenge
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C76 RID: 11382
		private readonly AlgorithmIdentifier owf;

		// Token: 0x04002C77 RID: 11383
		private readonly Asn1OctetString witness;

		// Token: 0x04002C78 RID: 11384
		private readonly Asn1OctetString challenge;
	}
}
