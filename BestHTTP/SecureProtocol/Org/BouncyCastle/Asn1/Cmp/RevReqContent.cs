using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007BA RID: 1978
	public class RevReqContent : Asn1Encodable
	{
		// Token: 0x060046B1 RID: 18097 RVA: 0x00196921 File Offset: 0x00194B21
		private RevReqContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x00196930 File Offset: 0x00194B30
		public static RevReqContent GetInstance(object obj)
		{
			if (obj is RevReqContent)
			{
				return (RevReqContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevReqContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x00196970 File Offset: 0x00194B70
		public RevReqContent(params RevDetails[] revDetails)
		{
			this.content = new DerSequence(revDetails);
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x00196994 File Offset: 0x00194B94
		public virtual RevDetails[] ToRevDetailsArray()
		{
			RevDetails[] array = new RevDetails[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = RevDetails.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x001969D5 File Offset: 0x00194BD5
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04002D29 RID: 11561
		private readonly Asn1Sequence content;
	}
}
