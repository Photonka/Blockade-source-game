using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E5 RID: 997
	public abstract class AlertDescription
	{
		// Token: 0x060028D7 RID: 10455 RVA: 0x0010F480 File Offset: 0x0010D680
		public static string GetName(byte alertDescription)
		{
			if (alertDescription <= 70)
			{
				if (alertDescription <= 22)
				{
					if (alertDescription == 0)
					{
						return "close_notify";
					}
					if (alertDescription == 10)
					{
						return "unexpected_message";
					}
					switch (alertDescription)
					{
					case 20:
						return "bad_record_mac";
					case 21:
						return "decryption_failed";
					case 22:
						return "record_overflow";
					}
				}
				else
				{
					switch (alertDescription)
					{
					case 30:
						return "decompression_failure";
					case 31:
					case 32:
					case 33:
					case 34:
					case 35:
					case 36:
					case 37:
					case 38:
					case 39:
						break;
					case 40:
						return "handshake_failure";
					case 41:
						return "no_certificate";
					case 42:
						return "bad_certificate";
					case 43:
						return "unsupported_certificate";
					case 44:
						return "certificate_revoked";
					case 45:
						return "certificate_expired";
					case 46:
						return "certificate_unknown";
					case 47:
						return "illegal_parameter";
					case 48:
						return "unknown_ca";
					case 49:
						return "access_denied";
					case 50:
						return "decode_error";
					case 51:
						return "decrypt_error";
					default:
						if (alertDescription == 60)
						{
							return "export_restriction";
						}
						if (alertDescription == 70)
						{
							return "protocol_version";
						}
						break;
					}
				}
			}
			else if (alertDescription <= 86)
			{
				if (alertDescription == 71)
				{
					return "insufficient_security";
				}
				if (alertDescription == 80)
				{
					return "internal_error";
				}
				if (alertDescription == 86)
				{
					return "inappropriate_fallback";
				}
			}
			else
			{
				if (alertDescription == 90)
				{
					return "user_canceled";
				}
				if (alertDescription == 100)
				{
					return "no_renegotiation";
				}
				switch (alertDescription)
				{
				case 110:
					return "unsupported_extension";
				case 111:
					return "certificate_unobtainable";
				case 112:
					return "unrecognized_name";
				case 113:
					return "bad_certificate_status_response";
				case 114:
					return "bad_certificate_hash_value";
				case 115:
					return "unknown_psk_identity";
				}
			}
			return "UNKNOWN";
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0010F64F File Offset: 0x0010D84F
		public static string GetText(byte alertDescription)
		{
			return string.Concat(new object[]
			{
				AlertDescription.GetName(alertDescription),
				"(",
				alertDescription,
				")"
			});
		}

		// Token: 0x040019F8 RID: 6648
		public const byte close_notify = 0;

		// Token: 0x040019F9 RID: 6649
		public const byte unexpected_message = 10;

		// Token: 0x040019FA RID: 6650
		public const byte bad_record_mac = 20;

		// Token: 0x040019FB RID: 6651
		public const byte decryption_failed = 21;

		// Token: 0x040019FC RID: 6652
		public const byte record_overflow = 22;

		// Token: 0x040019FD RID: 6653
		public const byte decompression_failure = 30;

		// Token: 0x040019FE RID: 6654
		public const byte handshake_failure = 40;

		// Token: 0x040019FF RID: 6655
		public const byte no_certificate = 41;

		// Token: 0x04001A00 RID: 6656
		public const byte bad_certificate = 42;

		// Token: 0x04001A01 RID: 6657
		public const byte unsupported_certificate = 43;

		// Token: 0x04001A02 RID: 6658
		public const byte certificate_revoked = 44;

		// Token: 0x04001A03 RID: 6659
		public const byte certificate_expired = 45;

		// Token: 0x04001A04 RID: 6660
		public const byte certificate_unknown = 46;

		// Token: 0x04001A05 RID: 6661
		public const byte illegal_parameter = 47;

		// Token: 0x04001A06 RID: 6662
		public const byte unknown_ca = 48;

		// Token: 0x04001A07 RID: 6663
		public const byte access_denied = 49;

		// Token: 0x04001A08 RID: 6664
		public const byte decode_error = 50;

		// Token: 0x04001A09 RID: 6665
		public const byte decrypt_error = 51;

		// Token: 0x04001A0A RID: 6666
		public const byte export_restriction = 60;

		// Token: 0x04001A0B RID: 6667
		public const byte protocol_version = 70;

		// Token: 0x04001A0C RID: 6668
		public const byte insufficient_security = 71;

		// Token: 0x04001A0D RID: 6669
		public const byte internal_error = 80;

		// Token: 0x04001A0E RID: 6670
		public const byte user_canceled = 90;

		// Token: 0x04001A0F RID: 6671
		public const byte no_renegotiation = 100;

		// Token: 0x04001A10 RID: 6672
		public const byte unsupported_extension = 110;

		// Token: 0x04001A11 RID: 6673
		public const byte certificate_unobtainable = 111;

		// Token: 0x04001A12 RID: 6674
		public const byte unrecognized_name = 112;

		// Token: 0x04001A13 RID: 6675
		public const byte bad_certificate_status_response = 113;

		// Token: 0x04001A14 RID: 6676
		public const byte bad_certificate_hash_value = 114;

		// Token: 0x04001A15 RID: 6677
		public const byte unknown_psk_identity = 115;

		// Token: 0x04001A16 RID: 6678
		public const byte inappropriate_fallback = 86;
	}
}
