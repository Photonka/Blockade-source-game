using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200073F RID: 1855
	public class RevocationValues : Asn1Encodable
	{
		// Token: 0x06004334 RID: 17204 RVA: 0x0018C70D File Offset: 0x0018A90D
		public static RevocationValues GetInstance(object obj)
		{
			if (obj == null || obj is RevocationValues)
			{
				return (RevocationValues)obj;
			}
			return new RevocationValues(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x0018C72C File Offset: 0x0018A92C
		private RevocationValues(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				Asn1Object @object = asn1TaggedObject.GetObject();
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)@object;
					foreach (object obj2 in asn1Sequence)
					{
						CertificateList.GetInstance(((Asn1Encodable)obj2).ToAsn1Object());
					}
					this.crlVals = asn1Sequence;
					break;
				}
				case 1:
				{
					Asn1Sequence asn1Sequence2 = (Asn1Sequence)@object;
					foreach (object obj3 in asn1Sequence2)
					{
						BasicOcspResponse.GetInstance(((Asn1Encodable)obj3).ToAsn1Object());
					}
					this.ocspVals = asn1Sequence2;
					break;
				}
				case 2:
					this.otherRevVals = OtherRevVals.GetInstance(@object);
					break;
				default:
					throw new ArgumentException("Illegal tag in RevocationValues", "seq");
				}
			}
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x0018C8E4 File Offset: 0x0018AAE4
		public RevocationValues(CertificateList[] crlVals, BasicOcspResponse[] ocspVals, OtherRevVals otherRevVals)
		{
			if (crlVals != null)
			{
				this.crlVals = new DerSequence(crlVals);
			}
			if (ocspVals != null)
			{
				this.ocspVals = new DerSequence(ocspVals);
			}
			this.otherRevVals = otherRevVals;
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x0018C920 File Offset: 0x0018AB20
		public RevocationValues(IEnumerable crlVals, IEnumerable ocspVals, OtherRevVals otherRevVals)
		{
			if (crlVals != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(crlVals, typeof(CertificateList)))
				{
					throw new ArgumentException("Must contain only 'CertificateList' objects", "crlVals");
				}
				this.crlVals = new DerSequence(Asn1EncodableVector.FromEnumerable(crlVals));
			}
			if (ocspVals != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(ocspVals, typeof(BasicOcspResponse)))
				{
					throw new ArgumentException("Must contain only 'BasicOcspResponse' objects", "ocspVals");
				}
				this.ocspVals = new DerSequence(Asn1EncodableVector.FromEnumerable(ocspVals));
			}
			this.otherRevVals = otherRevVals;
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x0018C9A8 File Offset: 0x0018ABA8
		public CertificateList[] GetCrlVals()
		{
			CertificateList[] array = new CertificateList[this.crlVals.Count];
			for (int i = 0; i < this.crlVals.Count; i++)
			{
				array[i] = CertificateList.GetInstance(this.crlVals[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x0018C9F8 File Offset: 0x0018ABF8
		public BasicOcspResponse[] GetOcspVals()
		{
			BasicOcspResponse[] array = new BasicOcspResponse[this.ocspVals.Count];
			for (int i = 0; i < this.ocspVals.Count; i++)
			{
				array[i] = BasicOcspResponse.GetInstance(this.ocspVals[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x0018CA46 File Offset: 0x0018AC46
		public OtherRevVals OtherRevVals
		{
			get
			{
				return this.otherRevVals;
			}
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x0018CA50 File Offset: 0x0018AC50
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.crlVals != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.crlVals)
				});
			}
			if (this.ocspVals != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.ocspVals)
				});
			}
			if (this.otherRevVals != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.otherRevVals.ToAsn1Object())
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B14 RID: 11028
		private readonly Asn1Sequence crlVals;

		// Token: 0x04002B15 RID: 11029
		private readonly Asn1Sequence ocspVals;

		// Token: 0x04002B16 RID: 11030
		private readonly OtherRevVals otherRevVals;
	}
}
