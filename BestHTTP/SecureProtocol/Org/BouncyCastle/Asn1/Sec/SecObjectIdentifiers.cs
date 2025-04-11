using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Sec
{
	// Token: 0x020006D2 RID: 1746
	public abstract class SecObjectIdentifiers
	{
		// Token: 0x0400283D RID: 10301
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier("1.3.132.0");

		// Token: 0x0400283E RID: 10302
		public static readonly DerObjectIdentifier SecT163k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".1");

		// Token: 0x0400283F RID: 10303
		public static readonly DerObjectIdentifier SecT163r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".2");

		// Token: 0x04002840 RID: 10304
		public static readonly DerObjectIdentifier SecT239k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".3");

		// Token: 0x04002841 RID: 10305
		public static readonly DerObjectIdentifier SecT113r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".4");

		// Token: 0x04002842 RID: 10306
		public static readonly DerObjectIdentifier SecT113r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".5");

		// Token: 0x04002843 RID: 10307
		public static readonly DerObjectIdentifier SecP112r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".6");

		// Token: 0x04002844 RID: 10308
		public static readonly DerObjectIdentifier SecP112r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".7");

		// Token: 0x04002845 RID: 10309
		public static readonly DerObjectIdentifier SecP160r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".8");

		// Token: 0x04002846 RID: 10310
		public static readonly DerObjectIdentifier SecP160k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".9");

		// Token: 0x04002847 RID: 10311
		public static readonly DerObjectIdentifier SecP256k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".10");

		// Token: 0x04002848 RID: 10312
		public static readonly DerObjectIdentifier SecT163r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".15");

		// Token: 0x04002849 RID: 10313
		public static readonly DerObjectIdentifier SecT283k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".16");

		// Token: 0x0400284A RID: 10314
		public static readonly DerObjectIdentifier SecT283r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".17");

		// Token: 0x0400284B RID: 10315
		public static readonly DerObjectIdentifier SecT131r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".22");

		// Token: 0x0400284C RID: 10316
		public static readonly DerObjectIdentifier SecT131r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".23");

		// Token: 0x0400284D RID: 10317
		public static readonly DerObjectIdentifier SecT193r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".24");

		// Token: 0x0400284E RID: 10318
		public static readonly DerObjectIdentifier SecT193r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".25");

		// Token: 0x0400284F RID: 10319
		public static readonly DerObjectIdentifier SecT233k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".26");

		// Token: 0x04002850 RID: 10320
		public static readonly DerObjectIdentifier SecT233r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".27");

		// Token: 0x04002851 RID: 10321
		public static readonly DerObjectIdentifier SecP128r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".28");

		// Token: 0x04002852 RID: 10322
		public static readonly DerObjectIdentifier SecP128r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".29");

		// Token: 0x04002853 RID: 10323
		public static readonly DerObjectIdentifier SecP160r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".30");

		// Token: 0x04002854 RID: 10324
		public static readonly DerObjectIdentifier SecP192k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".31");

		// Token: 0x04002855 RID: 10325
		public static readonly DerObjectIdentifier SecP224k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".32");

		// Token: 0x04002856 RID: 10326
		public static readonly DerObjectIdentifier SecP224r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".33");

		// Token: 0x04002857 RID: 10327
		public static readonly DerObjectIdentifier SecP384r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".34");

		// Token: 0x04002858 RID: 10328
		public static readonly DerObjectIdentifier SecP521r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".35");

		// Token: 0x04002859 RID: 10329
		public static readonly DerObjectIdentifier SecT409k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".36");

		// Token: 0x0400285A RID: 10330
		public static readonly DerObjectIdentifier SecT409r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".37");

		// Token: 0x0400285B RID: 10331
		public static readonly DerObjectIdentifier SecT571k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".38");

		// Token: 0x0400285C RID: 10332
		public static readonly DerObjectIdentifier SecT571r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".39");

		// Token: 0x0400285D RID: 10333
		public static readonly DerObjectIdentifier SecP192r1 = X9ObjectIdentifiers.Prime192v1;

		// Token: 0x0400285E RID: 10334
		public static readonly DerObjectIdentifier SecP256r1 = X9ObjectIdentifiers.Prime256v1;
	}
}
