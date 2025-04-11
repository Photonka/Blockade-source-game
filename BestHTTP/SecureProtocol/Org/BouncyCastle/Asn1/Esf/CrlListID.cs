using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000732 RID: 1842
	public class CrlListID : Asn1Encodable
	{
		// Token: 0x060042E1 RID: 17121 RVA: 0x0018B420 File Offset: 0x00189620
		public static CrlListID GetInstance(object obj)
		{
			if (obj == null || obj is CrlListID)
			{
				return (CrlListID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlListID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlListID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x0018B470 File Offset: 0x00189670
		private CrlListID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crls = (Asn1Sequence)seq[0].ToAsn1Object();
			foreach (object obj in this.crls)
			{
				CrlValidatedID.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
			}
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x0018B520 File Offset: 0x00189720
		public CrlListID(params CrlValidatedID[] crls)
		{
			if (crls == null)
			{
				throw new ArgumentNullException("crls");
			}
			this.crls = new DerSequence(crls);
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x0018B550 File Offset: 0x00189750
		public CrlListID(IEnumerable crls)
		{
			if (crls == null)
			{
				throw new ArgumentNullException("crls");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(crls, typeof(CrlValidatedID)))
			{
				throw new ArgumentException("Must contain only 'CrlValidatedID' objects", "crls");
			}
			this.crls = new DerSequence(Asn1EncodableVector.FromEnumerable(crls));
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x0018B5A4 File Offset: 0x001897A4
		public CrlValidatedID[] GetCrls()
		{
			CrlValidatedID[] array = new CrlValidatedID[this.crls.Count];
			for (int i = 0; i < this.crls.Count; i++)
			{
				array[i] = CrlValidatedID.GetInstance(this.crls[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x0018B5F2 File Offset: 0x001897F2
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.crls);
		}

		// Token: 0x04002AEF RID: 10991
		private readonly Asn1Sequence crls;
	}
}
