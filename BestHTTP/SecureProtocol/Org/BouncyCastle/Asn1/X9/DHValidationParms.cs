using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000660 RID: 1632
	public class DHValidationParms : Asn1Encodable
	{
		// Token: 0x06003D03 RID: 15619 RVA: 0x0017561A File Offset: 0x0017381A
		public static DHValidationParms GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return DHValidationParms.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x00175628 File Offset: 0x00173828
		public static DHValidationParms GetInstance(object obj)
		{
			if (obj == null || obj is DHDomainParameters)
			{
				return (DHValidationParms)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DHValidationParms((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DHValidationParms: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x00175675 File Offset: 0x00173875
		public DHValidationParms(DerBitString seed, DerInteger pgenCounter)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			if (pgenCounter == null)
			{
				throw new ArgumentNullException("pgenCounter");
			}
			this.seed = seed;
			this.pgenCounter = pgenCounter;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x001756A8 File Offset: 0x001738A8
		private DHValidationParms(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.seed = DerBitString.GetInstance(seq[0]);
			this.pgenCounter = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06003D07 RID: 15623 RVA: 0x00175708 File Offset: 0x00173908
		public DerBitString Seed
		{
			get
			{
				return this.seed;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x00175710 File Offset: 0x00173910
		public DerInteger PgenCounter
		{
			get
			{
				return this.pgenCounter;
			}
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x00175718 File Offset: 0x00173918
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.seed,
				this.pgenCounter
			});
		}

		// Token: 0x040025D9 RID: 9689
		private readonly DerBitString seed;

		// Token: 0x040025DA RID: 9690
		private readonly DerInteger pgenCounter;
	}
}
