using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x0200071E RID: 1822
	public class LdsVersionInfo : Asn1Encodable
	{
		// Token: 0x06004267 RID: 16999 RVA: 0x0018955C File Offset: 0x0018775C
		public LdsVersionInfo(string ldsVersion, string unicodeVersion)
		{
			this.ldsVersion = new DerPrintableString(ldsVersion);
			this.unicodeVersion = new DerPrintableString(unicodeVersion);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x0018957C File Offset: 0x0018777C
		private LdsVersionInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("sequence wrong size for LDSVersionInfo", "seq");
			}
			this.ldsVersion = DerPrintableString.GetInstance(seq[0]);
			this.unicodeVersion = DerPrintableString.GetInstance(seq[1]);
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x001895CC File Offset: 0x001877CC
		public static LdsVersionInfo GetInstance(object obj)
		{
			if (obj is LdsVersionInfo)
			{
				return (LdsVersionInfo)obj;
			}
			if (obj != null)
			{
				return new LdsVersionInfo(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x001895ED File Offset: 0x001877ED
		public virtual string GetLdsVersion()
		{
			return this.ldsVersion.GetString();
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x001895FA File Offset: 0x001877FA
		public virtual string GetUnicodeVersion()
		{
			return this.unicodeVersion.GetString();
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x00189607 File Offset: 0x00187807
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.ldsVersion,
				this.unicodeVersion
			});
		}

		// Token: 0x04002A6A RID: 10858
		private DerPrintableString ldsVersion;

		// Token: 0x04002A6B RID: 10859
		private DerPrintableString unicodeVersion;
	}
}
