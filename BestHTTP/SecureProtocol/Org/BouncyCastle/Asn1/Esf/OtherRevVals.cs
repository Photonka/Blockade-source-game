using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200073D RID: 1853
	public class OtherRevVals : Asn1Encodable
	{
		// Token: 0x06004325 RID: 17189 RVA: 0x0018C338 File Offset: 0x0018A538
		public static OtherRevVals GetInstance(object obj)
		{
			if (obj == null || obj is OtherRevVals)
			{
				return (OtherRevVals)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherRevVals((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherRevVals' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x0018C388 File Offset: 0x0018A588
		private OtherRevVals(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherRevValType = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.otherRevVals = seq[1].ToAsn1Object();
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x0018C3FB File Offset: 0x0018A5FB
		public OtherRevVals(DerObjectIdentifier otherRevValType, Asn1Encodable otherRevVals)
		{
			if (otherRevValType == null)
			{
				throw new ArgumentNullException("otherRevValType");
			}
			if (otherRevVals == null)
			{
				throw new ArgumentNullException("otherRevVals");
			}
			this.otherRevValType = otherRevValType;
			this.otherRevVals = otherRevVals.ToAsn1Object();
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x0018C432 File Offset: 0x0018A632
		public DerObjectIdentifier OtherRevValType
		{
			get
			{
				return this.otherRevValType;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x0018C43A File Offset: 0x0018A63A
		public Asn1Object OtherRevValsObject
		{
			get
			{
				return this.otherRevVals;
			}
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x0018C442 File Offset: 0x0018A642
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.otherRevValType,
				this.otherRevVals
			});
		}

		// Token: 0x04002B10 RID: 11024
		private readonly DerObjectIdentifier otherRevValType;

		// Token: 0x04002B11 RID: 11025
		private readonly Asn1Object otherRevVals;
	}
}
