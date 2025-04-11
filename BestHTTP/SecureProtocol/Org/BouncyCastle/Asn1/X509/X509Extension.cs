using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006AD RID: 1709
	public class X509Extension
	{
		// Token: 0x06003F61 RID: 16225 RVA: 0x0017CB78 File Offset: 0x0017AD78
		public X509Extension(DerBoolean critical, Asn1OctetString value)
		{
			if (critical == null)
			{
				throw new ArgumentNullException("critical");
			}
			this.critical = critical.IsTrue;
			this.value = value;
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x0017CBA1 File Offset: 0x0017ADA1
		public X509Extension(bool critical, Asn1OctetString value)
		{
			this.critical = critical;
			this.value = value;
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x0017CBB7 File Offset: 0x0017ADB7
		public bool IsCritical
		{
			get
			{
				return this.critical;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x0017CBBF File Offset: 0x0017ADBF
		public Asn1OctetString Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0017CBC7 File Offset: 0x0017ADC7
		public Asn1Encodable GetParsedValue()
		{
			return X509Extension.ConvertValueToObject(this);
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0017CBD0 File Offset: 0x0017ADD0
		public override int GetHashCode()
		{
			int hashCode = this.Value.GetHashCode();
			if (!this.IsCritical)
			{
				return ~hashCode;
			}
			return hashCode;
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x0017CBF8 File Offset: 0x0017ADF8
		public override bool Equals(object obj)
		{
			X509Extension x509Extension = obj as X509Extension;
			return x509Extension != null && this.Value.Equals(x509Extension.Value) && this.IsCritical == x509Extension.IsCritical;
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x0017CC34 File Offset: 0x0017AE34
		public static Asn1Object ConvertValueToObject(X509Extension ext)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(ext.Value.GetOctets());
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("can't convert extension", innerException);
			}
			return result;
		}

		// Token: 0x04002726 RID: 10022
		internal bool critical;

		// Token: 0x04002727 RID: 10023
		internal Asn1OctetString value;
	}
}
