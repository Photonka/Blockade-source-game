using System;
using System.Globalization;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000639 RID: 1593
	public class DerGeneralizedTime : Asn1Object
	{
		// Token: 0x06003BF5 RID: 15349 RVA: 0x00172C80 File Offset: 0x00170E80
		public static DerGeneralizedTime GetInstance(object obj)
		{
			if (obj == null || obj is DerGeneralizedTime)
			{
				return (DerGeneralizedTime)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x00172CB0 File Offset: 0x00170EB0
		public static DerGeneralizedTime GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerGeneralizedTime)
			{
				return DerGeneralizedTime.GetInstance(@object);
			}
			return new DerGeneralizedTime(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x00172CE8 File Offset: 0x00170EE8
		public DerGeneralizedTime(string time)
		{
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

		// Token: 0x06003BF8 RID: 15352 RVA: 0x00172D34 File Offset: 0x00170F34
		public DerGeneralizedTime(DateTime time)
		{
			this.time = time.ToString("yyyyMMddHHmmss\\Z");
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x00172D4E File Offset: 0x00170F4E
		internal DerGeneralizedTime(byte[] bytes)
		{
			this.time = Strings.FromAsciiByteArray(bytes);
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x00172D62 File Offset: 0x00170F62
		public string TimeString
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x00172D6C File Offset: 0x00170F6C
		public string GetTime()
		{
			if (this.time[this.time.Length - 1] == 'Z')
			{
				return this.time.Substring(0, this.time.Length - 1) + "GMT+00:00";
			}
			int num = this.time.Length - 5;
			char c = this.time[num];
			if (c == '-' || c == '+')
			{
				return string.Concat(new string[]
				{
					this.time.Substring(0, num),
					"GMT",
					this.time.Substring(num, 3),
					":",
					this.time.Substring(num + 3)
				});
			}
			num = this.time.Length - 3;
			c = this.time[num];
			if (c == '-' || c == '+')
			{
				return this.time.Substring(0, num) + "GMT" + this.time.Substring(num) + ":00";
			}
			return this.time + this.CalculateGmtOffset();
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x00172E8C File Offset: 0x0017108C
		private string CalculateGmtOffset()
		{
			char c = '+';
			DateTime dateTime = this.ToDateTime();
			TimeSpan timeSpan = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
			if (timeSpan.CompareTo(TimeSpan.Zero) < 0)
			{
				c = '-';
				timeSpan = timeSpan.Duration();
			}
			int hours = timeSpan.Hours;
			int minutes = timeSpan.Minutes;
			return string.Concat(new string[]
			{
				"GMT",
				c.ToString(),
				DerGeneralizedTime.Convert(hours),
				":",
				DerGeneralizedTime.Convert(minutes)
			});
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x00172F12 File Offset: 0x00171112
		private static string Convert(int time)
		{
			if (time < 10)
			{
				return "0" + time;
			}
			return time.ToString();
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x00172F34 File Offset: 0x00171134
		public DateTime ToDateTime()
		{
			string text = this.time;
			bool makeUniversal = false;
			string format;
			if (Platform.EndsWith(text, "Z"))
			{
				if (this.HasFractionalSeconds)
				{
					int count = text.Length - text.IndexOf('.') - 2;
					format = "yyyyMMddHHmmss." + this.FString(count) + "\\Z";
				}
				else
				{
					format = "yyyyMMddHHmmss\\Z";
				}
			}
			else if (this.time.IndexOf('-') > 0 || this.time.IndexOf('+') > 0)
			{
				text = this.GetTime();
				makeUniversal = true;
				if (this.HasFractionalSeconds)
				{
					int count2 = Platform.IndexOf(text, "GMT") - 1 - text.IndexOf('.');
					format = "yyyyMMddHHmmss." + this.FString(count2) + "'GMT'zzz";
				}
				else
				{
					format = "yyyyMMddHHmmss'GMT'zzz";
				}
			}
			else if (this.HasFractionalSeconds)
			{
				int count3 = text.Length - 1 - text.IndexOf('.');
				format = "yyyyMMddHHmmss." + this.FString(count3);
			}
			else
			{
				format = "yyyyMMddHHmmss";
			}
			return this.ParseDateString(text, format, makeUniversal);
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x00173044 File Offset: 0x00171244
		private string FString(int count)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append('f');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x00173074 File Offset: 0x00171274
		private DateTime ParseDateString(string s, string format, bool makeUniversal)
		{
			DateTimeStyles dateTimeStyles = DateTimeStyles.None;
			if (Platform.EndsWith(format, "Z"))
			{
				try
				{
					dateTimeStyles = (DateTimeStyles)Enums.GetEnumValue(typeof(DateTimeStyles), "AssumeUniversal");
				}
				catch (Exception)
				{
				}
				dateTimeStyles |= DateTimeStyles.AdjustToUniversal;
			}
			DateTime result = DateTime.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, dateTimeStyles);
			if (!makeUniversal)
			{
				return result;
			}
			return result.ToUniversalTime();
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06003C01 RID: 15361 RVA: 0x001730E0 File Offset: 0x001712E0
		private bool HasFractionalSeconds
		{
			get
			{
				return this.time.IndexOf('.') == 14;
			}
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x001730F3 File Offset: 0x001712F3
		private byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.time);
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x00173100 File Offset: 0x00171300
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(24, this.GetOctets());
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x00173110 File Offset: 0x00171310
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerGeneralizedTime derGeneralizedTime = asn1Object as DerGeneralizedTime;
			return derGeneralizedTime != null && this.time.Equals(derGeneralizedTime.time);
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x0017313A File Offset: 0x0017133A
		protected override int Asn1GetHashCode()
		{
			return this.time.GetHashCode();
		}

		// Token: 0x040025AC RID: 9644
		private readonly string time;
	}
}
