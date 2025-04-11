using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007AC RID: 1964
	public class PkiMessage : Asn1Encodable
	{
		// Token: 0x0600465B RID: 18011 RVA: 0x00195B24 File Offset: 0x00193D24
		private PkiMessage(Asn1Sequence seq)
		{
			this.header = PkiHeader.GetInstance(seq[0]);
			this.body = PkiBody.GetInstance(seq[1]);
			for (int i = 2; i < seq.Count; i++)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i].ToAsn1Object();
				if (asn1TaggedObject.TagNo == 0)
				{
					this.protection = DerBitString.GetInstance(asn1TaggedObject, true);
				}
				else
				{
					this.extraCerts = Asn1Sequence.GetInstance(asn1TaggedObject, true);
				}
			}
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x00195BA2 File Offset: 0x00193DA2
		public static PkiMessage GetInstance(object obj)
		{
			if (obj is PkiMessage)
			{
				return (PkiMessage)obj;
			}
			if (obj != null)
			{
				return new PkiMessage(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x00195BC4 File Offset: 0x00193DC4
		public PkiMessage(PkiHeader header, PkiBody body, DerBitString protection, CmpCertificate[] extraCerts)
		{
			this.header = header;
			this.body = body;
			this.protection = protection;
			if (extraCerts != null)
			{
				this.extraCerts = new DerSequence(extraCerts);
			}
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x00195BFF File Offset: 0x00193DFF
		public PkiMessage(PkiHeader header, PkiBody body, DerBitString protection) : this(header, body, protection, null)
		{
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x00195C0B File Offset: 0x00193E0B
		public PkiMessage(PkiHeader header, PkiBody body) : this(header, body, null, null)
		{
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06004660 RID: 18016 RVA: 0x00195C17 File Offset: 0x00193E17
		public virtual PkiHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06004661 RID: 18017 RVA: 0x00195C1F File Offset: 0x00193E1F
		public virtual PkiBody Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06004662 RID: 18018 RVA: 0x00195C27 File Offset: 0x00193E27
		public virtual DerBitString Protection
		{
			get
			{
				return this.protection;
			}
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x00195C30 File Offset: 0x00193E30
		public virtual CmpCertificate[] GetExtraCerts()
		{
			if (this.extraCerts == null)
			{
				return null;
			}
			CmpCertificate[] array = new CmpCertificate[this.extraCerts.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = CmpCertificate.GetInstance(this.extraCerts[i]);
			}
			return array;
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x00195C7B File Offset: 0x00193E7B
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector v = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.header,
				this.body
			});
			PkiMessage.AddOptional(v, 0, this.protection);
			PkiMessage.AddOptional(v, 1, this.extraCerts);
			return new DerSequence(v);
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x00195891 File Offset: 0x00193A91
		private static void AddOptional(Asn1EncodableVector v, int tagNo, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, tagNo, obj)
				});
			}
		}

		// Token: 0x04002CFC RID: 11516
		private readonly PkiHeader header;

		// Token: 0x04002CFD RID: 11517
		private readonly PkiBody body;

		// Token: 0x04002CFE RID: 11518
		private readonly DerBitString protection;

		// Token: 0x04002CFF RID: 11519
		private readonly Asn1Sequence extraCerts;
	}
}
