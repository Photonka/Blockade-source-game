using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200060E RID: 1550
	[Serializable]
	public class Asn1Exception : IOException
	{
		// Token: 0x06003ADF RID: 15071 RVA: 0x000B979A File Offset: 0x000B799A
		public Asn1Exception()
		{
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000B97A2 File Offset: 0x000B79A2
		public Asn1Exception(string message) : base(message)
		{
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000B97AB File Offset: 0x000B79AB
		public Asn1Exception(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
