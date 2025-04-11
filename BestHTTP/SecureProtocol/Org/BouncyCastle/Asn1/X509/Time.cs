using System;
using System.Globalization;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A3 RID: 1699
	public class Time : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003F00 RID: 16128 RVA: 0x0017BC1C File Offset: 0x00179E1C
		public static Time GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Time.GetInstance(obj.GetObject());
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x0017BC29 File Offset: 0x00179E29
		public Time(Asn1Object time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			if (!(time is DerUtcTime) && !(time is DerGeneralizedTime))
			{
				throw new ArgumentException("unknown object passed to Time");
			}
			this.time = time;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0017BC64 File Offset: 0x00179E64
		public Time(DateTime date)
		{
			string text = date.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + "Z";
			int num = int.Parse(text.Substring(0, 4));
			if (num < 1950 || num > 2049)
			{
				this.time = new DerGeneralizedTime(text);
				return;
			}
			this.time = new DerUtcTime(text.Substring(2));
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x0017BCD0 File Offset: 0x00179ED0
		public static Time GetInstance(object obj)
		{
			if (obj == null || obj is Time)
			{
				return (Time)obj;
			}
			if (obj is DerUtcTime)
			{
				return new Time((DerUtcTime)obj);
			}
			if (obj is DerGeneralizedTime)
			{
				return new Time((DerGeneralizedTime)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x0017BD31 File Offset: 0x00179F31
		public string GetTime()
		{
			if (this.time is DerUtcTime)
			{
				return ((DerUtcTime)this.time).AdjustedTimeString;
			}
			return ((DerGeneralizedTime)this.time).GetTime();
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0017BD64 File Offset: 0x00179F64
		public DateTime ToDateTime()
		{
			DateTime result;
			try
			{
				if (this.time is DerUtcTime)
				{
					result = ((DerUtcTime)this.time).ToAdjustedDateTime();
				}
				else
				{
					result = ((DerGeneralizedTime)this.time).ToDateTime();
				}
			}
			catch (FormatException ex)
			{
				throw new InvalidOperationException("invalid date string: " + ex.Message);
			}
			return result;
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0017BDCC File Offset: 0x00179FCC
		public override Asn1Object ToAsn1Object()
		{
			return this.time;
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0017BDD4 File Offset: 0x00179FD4
		public override string ToString()
		{
			return this.GetTime();
		}

		// Token: 0x040026F7 RID: 9975
		private readonly Asn1Object time;
	}
}
