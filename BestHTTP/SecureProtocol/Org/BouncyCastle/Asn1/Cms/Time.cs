using System;
using System.Globalization;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200078E RID: 1934
	public class Time : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004583 RID: 17795 RVA: 0x0019368C File Offset: 0x0019188C
		public static Time GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Time.GetInstance(obj.GetObject());
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x00193699 File Offset: 0x00191899
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

		// Token: 0x06004585 RID: 17797 RVA: 0x001936D4 File Offset: 0x001918D4
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

		// Token: 0x06004586 RID: 17798 RVA: 0x00193740 File Offset: 0x00191940
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

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x001937A1 File Offset: 0x001919A1
		public string TimeString
		{
			get
			{
				if (this.time is DerUtcTime)
				{
					return ((DerUtcTime)this.time).AdjustedTimeString;
				}
				return ((DerGeneralizedTime)this.time).GetTime();
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x001937D4 File Offset: 0x001919D4
		public DateTime Date
		{
			get
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
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x0019383C File Offset: 0x00191A3C
		public override Asn1Object ToAsn1Object()
		{
			return this.time;
		}

		// Token: 0x04002C53 RID: 11347
		private readonly Asn1Object time;
	}
}
