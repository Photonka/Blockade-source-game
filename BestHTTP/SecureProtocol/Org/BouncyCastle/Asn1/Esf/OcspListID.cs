using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000737 RID: 1847
	public class OcspListID : Asn1Encodable
	{
		// Token: 0x060042FD RID: 17149 RVA: 0x0018BB08 File Offset: 0x00189D08
		public static OcspListID GetInstance(object obj)
		{
			if (obj == null || obj is OcspListID)
			{
				return (OcspListID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspListID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspListID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x0018BB58 File Offset: 0x00189D58
		private OcspListID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspResponses = (Asn1Sequence)seq[0].ToAsn1Object();
			foreach (object obj in this.ocspResponses)
			{
				OcspResponsesID.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
			}
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x0018BC08 File Offset: 0x00189E08
		public OcspListID(params OcspResponsesID[] ocspResponses)
		{
			if (ocspResponses == null)
			{
				throw new ArgumentNullException("ocspResponses");
			}
			this.ocspResponses = new DerSequence(ocspResponses);
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x0018BC38 File Offset: 0x00189E38
		public OcspListID(IEnumerable ocspResponses)
		{
			if (ocspResponses == null)
			{
				throw new ArgumentNullException("ocspResponses");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(ocspResponses, typeof(OcspResponsesID)))
			{
				throw new ArgumentException("Must contain only 'OcspResponsesID' objects", "ocspResponses");
			}
			this.ocspResponses = new DerSequence(Asn1EncodableVector.FromEnumerable(ocspResponses));
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x0018BC8C File Offset: 0x00189E8C
		public OcspResponsesID[] GetOcspResponses()
		{
			OcspResponsesID[] array = new OcspResponsesID[this.ocspResponses.Count];
			for (int i = 0; i < this.ocspResponses.Count; i++)
			{
				array[i] = OcspResponsesID.GetInstance(this.ocspResponses[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x0018BCDA File Offset: 0x00189EDA
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.ocspResponses);
		}

		// Token: 0x04002B05 RID: 11013
		private readonly Asn1Sequence ocspResponses;
	}
}
