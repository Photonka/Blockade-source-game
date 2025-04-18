﻿using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006AC RID: 1708
	public class X509DefaultEntryConverter : X509NameEntryConverter
	{
		// Token: 0x06003F5F RID: 16223 RVA: 0x0017CA84 File Offset: 0x0017AC84
		public override Asn1Object GetConvertedValue(DerObjectIdentifier oid, string value)
		{
			if (value.Length != 0 && value[0] == '#')
			{
				try
				{
					return base.ConvertHexEncoded(value, 1);
				}
				catch (IOException)
				{
					throw new Exception("can't recode value for oid " + oid.Id);
				}
			}
			if (value.Length != 0 && value[0] == '\\')
			{
				value = value.Substring(1);
			}
			if (oid.Equals(X509Name.EmailAddress) || oid.Equals(X509Name.DC))
			{
				return new DerIA5String(value);
			}
			if (oid.Equals(X509Name.DateOfBirth))
			{
				return new DerGeneralizedTime(value);
			}
			if (oid.Equals(X509Name.C) || oid.Equals(X509Name.SerialNumber) || oid.Equals(X509Name.DnQualifier) || oid.Equals(X509Name.TelephoneNumber))
			{
				return new DerPrintableString(value);
			}
			return new DerUtf8String(value);
		}
	}
}
