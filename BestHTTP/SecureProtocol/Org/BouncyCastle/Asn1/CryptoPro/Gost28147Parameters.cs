using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x0200074A RID: 1866
	public class Gost28147Parameters : Asn1Encodable
	{
		// Token: 0x06004378 RID: 17272 RVA: 0x0018DE66 File Offset: 0x0018C066
		public static Gost28147Parameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost28147Parameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x0018DE74 File Offset: 0x0018C074
		public static Gost28147Parameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost28147Parameters)
			{
				return (Gost28147Parameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Gost28147Parameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + Platform.GetTypeName(obj));
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x0018DEB4 File Offset: 0x0018C0B4
		private Gost28147Parameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = Asn1OctetString.GetInstance(seq[0]);
			this.paramSet = DerObjectIdentifier.GetInstance(seq[1]);
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x0018DF04 File Offset: 0x0018C104
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.paramSet
			});
		}

		// Token: 0x04002B5D RID: 11101
		private readonly Asn1OctetString iv;

		// Token: 0x04002B5E RID: 11102
		private readonly DerObjectIdentifier paramSet;
	}
}
