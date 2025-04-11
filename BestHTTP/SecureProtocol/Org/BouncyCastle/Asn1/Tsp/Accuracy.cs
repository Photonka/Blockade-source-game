using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020006C3 RID: 1731
	public class Accuracy : Asn1Encodable
	{
		// Token: 0x06004012 RID: 16402 RVA: 0x00180CF0 File Offset: 0x0017EEF0
		public Accuracy(DerInteger seconds, DerInteger millis, DerInteger micros)
		{
			if (millis != null && (millis.Value.IntValue < 1 || millis.Value.IntValue > 999))
			{
				throw new ArgumentException("Invalid millis field : not in (1..999)");
			}
			if (micros != null && (micros.Value.IntValue < 1 || micros.Value.IntValue > 999))
			{
				throw new ArgumentException("Invalid micros field : not in (1..999)");
			}
			this.seconds = seconds;
			this.millis = millis;
			this.micros = micros;
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x00180D74 File Offset: 0x0017EF74
		private Accuracy(Asn1Sequence seq)
		{
			for (int i = 0; i < seq.Count; i++)
			{
				if (seq[i] is DerInteger)
				{
					this.seconds = (DerInteger)seq[i];
				}
				else if (seq[i] is DerTaggedObject)
				{
					DerTaggedObject derTaggedObject = (DerTaggedObject)seq[i];
					int tagNo = derTaggedObject.TagNo;
					if (tagNo != 0)
					{
						if (tagNo != 1)
						{
							throw new ArgumentException("Invalig tag number");
						}
						this.micros = DerInteger.GetInstance(derTaggedObject, false);
						if (this.micros.Value.IntValue < 1 || this.micros.Value.IntValue > 999)
						{
							throw new ArgumentException("Invalid micros field : not in (1..999).");
						}
					}
					else
					{
						this.millis = DerInteger.GetInstance(derTaggedObject, false);
						if (this.millis.Value.IntValue < 1 || this.millis.Value.IntValue > 999)
						{
							throw new ArgumentException("Invalid millis field : not in (1..999).");
						}
					}
				}
			}
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x00180E83 File Offset: 0x0017F083
		public static Accuracy GetInstance(object o)
		{
			if (o == null || o is Accuracy)
			{
				return (Accuracy)o;
			}
			if (o is Asn1Sequence)
			{
				return new Accuracy((Asn1Sequence)o);
			}
			throw new ArgumentException("Unknown object in 'Accuracy' factory: " + Platform.GetTypeName(o));
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x00180EC0 File Offset: 0x0017F0C0
		public DerInteger Seconds
		{
			get
			{
				return this.seconds;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x00180EC8 File Offset: 0x0017F0C8
		public DerInteger Millis
		{
			get
			{
				return this.millis;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06004017 RID: 16407 RVA: 0x00180ED0 File Offset: 0x0017F0D0
		public DerInteger Micros
		{
			get
			{
				return this.micros;
			}
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x00180ED8 File Offset: 0x0017F0D8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.seconds != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.seconds
				});
			}
			if (this.millis != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.millis)
				});
			}
			if (this.micros != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.micros)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040027E8 RID: 10216
		private readonly DerInteger seconds;

		// Token: 0x040027E9 RID: 10217
		private readonly DerInteger millis;

		// Token: 0x040027EA RID: 10218
		private readonly DerInteger micros;

		// Token: 0x040027EB RID: 10219
		protected const int MinMillis = 1;

		// Token: 0x040027EC RID: 10220
		protected const int MaxMillis = 999;

		// Token: 0x040027ED RID: 10221
		protected const int MinMicros = 1;

		// Token: 0x040027EE RID: 10222
		protected const int MaxMicros = 999;
	}
}
