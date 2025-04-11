using System;
using System.Globalization;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000650 RID: 1616
	public class DerUtcTime : Asn1Object
	{
		// Token: 0x06003CAC RID: 15532 RVA: 0x00174957 File Offset: 0x00172B57
		public static DerUtcTime GetInstance(object obj)
		{
			if (obj == null || obj is DerUtcTime)
			{
				return (DerUtcTime)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x00174980 File Offset: 0x00172B80
		public static DerUtcTime GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerUtcTime)
			{
				return DerUtcTime.GetInstance(@object);
			}
			return new DerUtcTime(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x001749B8 File Offset: 0x00172BB8
		public DerUtcTime(string time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			this.time = time;
			try
			{
				this.ToDateTime();
			}
			catch (FormatException ex)
			{
				throw new ArgumentException("invalid date string: " + ex.Message);
			}
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x00174A10 File Offset: 0x00172C10
		public DerUtcTime(DateTime time)
		{
			this.time = time.ToString("yyMMddHHmmss", CultureInfo.InvariantCulture) + "Z";
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x00174A39 File Offset: 0x00172C39
		internal DerUtcTime(byte[] bytes)
		{
			this.time = Strings.FromAsciiByteArray(bytes);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x00174A4D File Offset: 0x00172C4D
		public DateTime ToDateTime()
		{
			return this.ParseDateString(this.TimeString, "yyMMddHHmmss'GMT'zzz");
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x00174A60 File Offset: 0x00172C60
		public DateTime ToAdjustedDateTime()
		{
			return this.ParseDateString(this.AdjustedTimeString, "yyyyMMddHHmmss'GMT'zzz");
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x00174A74 File Offset: 0x00172C74
		private DateTime ParseDateString(string dateStr, string formatStr)
		{
			return DateTime.ParseExact(dateStr, formatStr, DateTimeFormatInfo.InvariantInfo).ToUniversalTime();
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x00174A98 File Offset: 0x00172C98
		public string TimeString
		{
			get
			{
				if (this.time.IndexOf('-') < 0 && this.time.IndexOf('+') < 0)
				{
					if (this.time.Length == 11)
					{
						return this.time.Substring(0, 10) + "00GMT+00:00";
					}
					return this.time.Substring(0, 12) + "GMT+00:00";
				}
				else
				{
					int num = this.time.IndexOf('-');
					if (num < 0)
					{
						num = this.time.IndexOf('+');
					}
					string text = this.time;
					if (num == this.time.Length - 3)
					{
						text += "00";
					}
					if (num == 10)
					{
						return string.Concat(new string[]
						{
							text.Substring(0, 10),
							"00GMT",
							text.Substring(10, 3),
							":",
							text.Substring(13, 2)
						});
					}
					return string.Concat(new string[]
					{
						text.Substring(0, 12),
						"GMT",
						text.Substring(12, 3),
						":",
						text.Substring(15, 2)
					});
				}
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x00174BCD File Offset: 0x00172DCD
		[Obsolete("Use 'AdjustedTimeString' property instead")]
		public string AdjustedTime
		{
			get
			{
				return this.AdjustedTimeString;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x00174BD8 File Offset: 0x00172DD8
		public string AdjustedTimeString
		{
			get
			{
				string timeString = this.TimeString;
				return ((timeString[0] < '5') ? "20" : "19") + timeString;
			}
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x00174C09 File Offset: 0x00172E09
		private byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.time);
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x00174C16 File Offset: 0x00172E16
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(23, this.GetOctets());
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x00174C28 File Offset: 0x00172E28
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUtcTime derUtcTime = asn1Object as DerUtcTime;
			return derUtcTime != null && this.time.Equals(derUtcTime.time);
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x00174C52 File Offset: 0x00172E52
		protected override int Asn1GetHashCode()
		{
			return this.time.GetHashCode();
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x00174C5F File Offset: 0x00172E5F
		public override string ToString()
		{
			return this.time;
		}

		// Token: 0x040025C7 RID: 9671
		private readonly string time;
	}
}
