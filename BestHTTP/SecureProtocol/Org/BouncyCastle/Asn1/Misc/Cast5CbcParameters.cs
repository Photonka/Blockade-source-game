using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000706 RID: 1798
	public class Cast5CbcParameters : Asn1Encodable
	{
		// Token: 0x060041E2 RID: 16866 RVA: 0x0018732D File Offset: 0x0018552D
		public static Cast5CbcParameters GetInstance(object o)
		{
			if (o is Cast5CbcParameters)
			{
				return (Cast5CbcParameters)o;
			}
			if (o is Asn1Sequence)
			{
				return new Cast5CbcParameters((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in Cast5CbcParameters factory");
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x0018735C File Offset: 0x0018555C
		public Cast5CbcParameters(byte[] iv, int keyLength)
		{
			this.iv = new DerOctetString(iv);
			this.keyLength = new DerInteger(keyLength);
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x0018737C File Offset: 0x0018557C
		private Cast5CbcParameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = (Asn1OctetString)seq[0];
			this.keyLength = (DerInteger)seq[1];
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x001873CC File Offset: 0x001855CC
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv.GetOctets());
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x001873DE File Offset: 0x001855DE
		public int KeyLength
		{
			get
			{
				return this.keyLength.Value.IntValue;
			}
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x001873F0 File Offset: 0x001855F0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.keyLength
			});
		}

		// Token: 0x040029DA RID: 10714
		private readonly DerInteger keyLength;

		// Token: 0x040029DB RID: 10715
		private readonly Asn1OctetString iv;
	}
}
