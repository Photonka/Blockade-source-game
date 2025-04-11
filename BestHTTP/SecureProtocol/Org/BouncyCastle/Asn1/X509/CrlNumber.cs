using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200067E RID: 1662
	public class CrlNumber : DerInteger
	{
		// Token: 0x06003DE4 RID: 15844 RVA: 0x001781E6 File Offset: 0x001763E6
		public CrlNumber(BigInteger number) : base(number)
		{
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x001781EF File Offset: 0x001763EF
		public BigInteger Number
		{
			get
			{
				return base.PositiveValue;
			}
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x001781F7 File Offset: 0x001763F7
		public override string ToString()
		{
			return "CRLNumber: " + this.Number;
		}
	}
}
