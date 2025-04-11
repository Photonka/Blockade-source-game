using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000730 RID: 1840
	public class CompleteRevocationRefs : Asn1Encodable
	{
		// Token: 0x060042D3 RID: 17107 RVA: 0x0018B0D8 File Offset: 0x001892D8
		public static CompleteRevocationRefs GetInstance(object obj)
		{
			if (obj == null || obj is CompleteRevocationRefs)
			{
				return (CompleteRevocationRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompleteRevocationRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CompleteRevocationRefs' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0018B128 File Offset: 0x00189328
		private CompleteRevocationRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				CrlOcspRef.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
			}
			this.crlOcspRefs = seq;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0018B19C File Offset: 0x0018939C
		public CompleteRevocationRefs(params CrlOcspRef[] crlOcspRefs)
		{
			if (crlOcspRefs == null)
			{
				throw new ArgumentNullException("crlOcspRefs");
			}
			this.crlOcspRefs = new DerSequence(crlOcspRefs);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0018B1CC File Offset: 0x001893CC
		public CompleteRevocationRefs(IEnumerable crlOcspRefs)
		{
			if (crlOcspRefs == null)
			{
				throw new ArgumentNullException("crlOcspRefs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(crlOcspRefs, typeof(CrlOcspRef)))
			{
				throw new ArgumentException("Must contain only 'CrlOcspRef' objects", "crlOcspRefs");
			}
			this.crlOcspRefs = new DerSequence(Asn1EncodableVector.FromEnumerable(crlOcspRefs));
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0018B220 File Offset: 0x00189420
		public CrlOcspRef[] GetCrlOcspRefs()
		{
			CrlOcspRef[] array = new CrlOcspRef[this.crlOcspRefs.Count];
			for (int i = 0; i < this.crlOcspRefs.Count; i++)
			{
				array[i] = CrlOcspRef.GetInstance(this.crlOcspRefs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0018B26E File Offset: 0x0018946E
		public override Asn1Object ToAsn1Object()
		{
			return this.crlOcspRefs;
		}

		// Token: 0x04002AEB RID: 10987
		private readonly Asn1Sequence crlOcspRefs;
	}
}
