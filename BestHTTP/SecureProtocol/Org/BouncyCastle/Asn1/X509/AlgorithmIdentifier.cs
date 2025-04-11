using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200066F RID: 1647
	public class AlgorithmIdentifier : Asn1Encodable
	{
		// Token: 0x06003D69 RID: 15721 RVA: 0x00176DBC File Offset: 0x00174FBC
		public static AlgorithmIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AlgorithmIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x00176DCA File Offset: 0x00174FCA
		public static AlgorithmIdentifier GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is AlgorithmIdentifier)
			{
				return (AlgorithmIdentifier)obj;
			}
			return new AlgorithmIdentifier(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x00176DEB File Offset: 0x00174FEB
		public AlgorithmIdentifier(DerObjectIdentifier algorithm)
		{
			this.algorithm = algorithm;
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x00176DFA File Offset: 0x00174FFA
		[Obsolete("Use version taking a DerObjectIdentifier")]
		public AlgorithmIdentifier(string algorithm)
		{
			this.algorithm = new DerObjectIdentifier(algorithm);
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x00176E0E File Offset: 0x0017500E
		public AlgorithmIdentifier(DerObjectIdentifier algorithm, Asn1Encodable parameters)
		{
			this.algorithm = algorithm;
			this.parameters = parameters;
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x00176E24 File Offset: 0x00175024
		internal AlgorithmIdentifier(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.algorithm = DerObjectIdentifier.GetInstance(seq[0]);
			this.parameters = ((seq.Count < 2) ? null : seq[1]);
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x00176E8F File Offset: 0x0017508F
		public virtual DerObjectIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06003D70 RID: 15728 RVA: 0x00176E8F File Offset: 0x0017508F
		[Obsolete("Use 'Algorithm' property instead")]
		public virtual DerObjectIdentifier ObjectID
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x00176E97 File Offset: 0x00175097
		public virtual Asn1Encodable Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x00176EA0 File Offset: 0x001750A0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.algorithm
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.parameters
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400263A RID: 9786
		private readonly DerObjectIdentifier algorithm;

		// Token: 0x0400263B RID: 9787
		private readonly Asn1Encodable parameters;
	}
}
