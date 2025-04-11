using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000707 RID: 1799
	public class IdeaCbcPar : Asn1Encodable
	{
		// Token: 0x060041E8 RID: 16872 RVA: 0x0018740F File Offset: 0x0018560F
		public static IdeaCbcPar GetInstance(object o)
		{
			if (o is IdeaCbcPar)
			{
				return (IdeaCbcPar)o;
			}
			if (o is Asn1Sequence)
			{
				return new IdeaCbcPar((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in IDEACBCPar factory");
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x0018743E File Offset: 0x0018563E
		public IdeaCbcPar(byte[] iv)
		{
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x00187452 File Offset: 0x00185652
		private IdeaCbcPar(Asn1Sequence seq)
		{
			if (seq.Count == 1)
			{
				this.iv = (Asn1OctetString)seq[0];
			}
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x00187475 File Offset: 0x00185675
		public byte[] GetIV()
		{
			if (this.iv != null)
			{
				return this.iv.GetOctets();
			}
			return null;
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x0018748C File Offset: 0x0018568C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.iv != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.iv
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040029DC RID: 10716
		internal Asn1OctetString iv;
	}
}
