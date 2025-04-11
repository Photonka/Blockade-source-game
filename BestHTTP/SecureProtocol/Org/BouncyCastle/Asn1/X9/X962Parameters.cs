using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000665 RID: 1637
	public class X962Parameters : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003D24 RID: 15652 RVA: 0x00175D68 File Offset: 0x00173F68
		public static X962Parameters GetInstance(object obj)
		{
			if (obj == null || obj is X962Parameters)
			{
				return (X962Parameters)obj;
			}
			if (obj is Asn1Object)
			{
				return new X962Parameters((Asn1Object)obj);
			}
			if (obj is byte[])
			{
				try
				{
					return new X962Parameters(Asn1Object.FromByteArray((byte[])obj));
				}
				catch (Exception ex)
				{
					throw new ArgumentException("unable to parse encoded data: " + ex.Message, ex);
				}
			}
			throw new ArgumentException("unknown object in getInstance()");
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x00175DEC File Offset: 0x00173FEC
		public X962Parameters(X9ECParameters ecParameters)
		{
			this._params = ecParameters.ToAsn1Object();
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x00175E00 File Offset: 0x00174000
		public X962Parameters(DerObjectIdentifier namedCurve)
		{
			this._params = namedCurve;
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x00175E00 File Offset: 0x00174000
		public X962Parameters(Asn1Object obj)
		{
			this._params = obj;
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x00175E0F File Offset: 0x0017400F
		public bool IsNamedCurve
		{
			get
			{
				return this._params is DerObjectIdentifier;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x00175E1F File Offset: 0x0017401F
		public bool IsImplicitlyCA
		{
			get
			{
				return this._params is Asn1Null;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x00175E2F File Offset: 0x0017402F
		public Asn1Object Parameters
		{
			get
			{
				return this._params;
			}
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x00175E2F File Offset: 0x0017402F
		public override Asn1Object ToAsn1Object()
		{
			return this._params;
		}

		// Token: 0x040025E3 RID: 9699
		private readonly Asn1Object _params;
	}
}
