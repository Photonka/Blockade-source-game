using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200065E RID: 1630
	public class DHDomainParameters : Asn1Encodable
	{
		// Token: 0x06003CF3 RID: 15603 RVA: 0x00175367 File Offset: 0x00173567
		public static DHDomainParameters GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return DHDomainParameters.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x00175378 File Offset: 0x00173578
		public static DHDomainParameters GetInstance(object obj)
		{
			if (obj == null || obj is DHDomainParameters)
			{
				return (DHDomainParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DHDomainParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DHDomainParameters: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x001753C8 File Offset: 0x001735C8
		public DHDomainParameters(DerInteger p, DerInteger g, DerInteger q, DerInteger j, DHValidationParms validationParms)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			this.p = p;
			this.g = g;
			this.q = q;
			this.j = j;
			this.validationParms = validationParms;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x0017542C File Offset: 0x0017362C
		private DHDomainParameters(Asn1Sequence seq)
		{
			if (seq.Count < 3 || seq.Count > 5)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			IEnumerator enumerator = seq.GetEnumerator();
			this.p = DerInteger.GetInstance(DHDomainParameters.GetNext(enumerator));
			this.g = DerInteger.GetInstance(DHDomainParameters.GetNext(enumerator));
			this.q = DerInteger.GetInstance(DHDomainParameters.GetNext(enumerator));
			Asn1Encodable next = DHDomainParameters.GetNext(enumerator);
			if (next != null && next is DerInteger)
			{
				this.j = DerInteger.GetInstance(next);
				next = DHDomainParameters.GetNext(enumerator);
			}
			if (next != null)
			{
				this.validationParms = DHValidationParms.GetInstance(next.ToAsn1Object());
			}
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x001754E4 File Offset: 0x001736E4
		private static Asn1Encodable GetNext(IEnumerator e)
		{
			if (!e.MoveNext())
			{
				return null;
			}
			return (Asn1Encodable)e.Current;
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x001754FB File Offset: 0x001736FB
		public DerInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x00175503 File Offset: 0x00173703
		public DerInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x0017550B File Offset: 0x0017370B
		public DerInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x00175513 File Offset: 0x00173713
		public DerInteger J
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x0017551B File Offset: 0x0017371B
		public DHValidationParms ValidationParms
		{
			get
			{
				return this.validationParms;
			}
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x00175524 File Offset: 0x00173724
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.p,
				this.g,
				this.q
			});
			if (this.j != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.j
				});
			}
			if (this.validationParms != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.validationParms
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040025D3 RID: 9683
		private readonly DerInteger p;

		// Token: 0x040025D4 RID: 9684
		private readonly DerInteger g;

		// Token: 0x040025D5 RID: 9685
		private readonly DerInteger q;

		// Token: 0x040025D6 RID: 9686
		private readonly DerInteger j;

		// Token: 0x040025D7 RID: 9687
		private readonly DHValidationParms validationParms;
	}
}
