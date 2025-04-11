using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Endo;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Sec
{
	// Token: 0x020006D1 RID: 1745
	public sealed class SecNamedCurves
	{
		// Token: 0x0600406F RID: 16495 RVA: 0x00023EF4 File Offset: 0x000220F4
		private SecNamedCurves()
		{
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x00096BA2 File Offset: 0x00094DA2
		private static ECCurve ConfigureCurve(ECCurve curve)
		{
			return curve;
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x0015398A File Offset: 0x00151B8A
		private static ECCurve ConfigureCurveGlv(ECCurve c, GlvTypeBParameters p)
		{
			return c.Configure().SetEndomorphism(new GlvTypeBEndomorphism(c, p)).Create();
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x0011807A File Offset: 0x0011627A
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.Decode(hex));
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x001822CC File Offset: 0x001804CC
		private static void DefineCurve(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			SecNamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			SecNamedCurves.names.Add(oid, name);
			SecNamedCurves.curves.Add(oid, holder);
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x001822F8 File Offset: 0x001804F8
		static SecNamedCurves()
		{
			SecNamedCurves.DefineCurve("secp112r1", SecObjectIdentifiers.SecP112r1, SecNamedCurves.Secp112r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp112r2", SecObjectIdentifiers.SecP112r2, SecNamedCurves.Secp112r2Holder.Instance);
			SecNamedCurves.DefineCurve("secp128r1", SecObjectIdentifiers.SecP128r1, SecNamedCurves.Secp128r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp128r2", SecObjectIdentifiers.SecP128r2, SecNamedCurves.Secp128r2Holder.Instance);
			SecNamedCurves.DefineCurve("secp160k1", SecObjectIdentifiers.SecP160k1, SecNamedCurves.Secp160k1Holder.Instance);
			SecNamedCurves.DefineCurve("secp160r1", SecObjectIdentifiers.SecP160r1, SecNamedCurves.Secp160r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp160r2", SecObjectIdentifiers.SecP160r2, SecNamedCurves.Secp160r2Holder.Instance);
			SecNamedCurves.DefineCurve("secp192k1", SecObjectIdentifiers.SecP192k1, SecNamedCurves.Secp192k1Holder.Instance);
			SecNamedCurves.DefineCurve("secp192r1", SecObjectIdentifiers.SecP192r1, SecNamedCurves.Secp192r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp224k1", SecObjectIdentifiers.SecP224k1, SecNamedCurves.Secp224k1Holder.Instance);
			SecNamedCurves.DefineCurve("secp224r1", SecObjectIdentifiers.SecP224r1, SecNamedCurves.Secp224r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp256k1", SecObjectIdentifiers.SecP256k1, SecNamedCurves.Secp256k1Holder.Instance);
			SecNamedCurves.DefineCurve("secp256r1", SecObjectIdentifiers.SecP256r1, SecNamedCurves.Secp256r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp384r1", SecObjectIdentifiers.SecP384r1, SecNamedCurves.Secp384r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp521r1", SecObjectIdentifiers.SecP521r1, SecNamedCurves.Secp521r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect113r1", SecObjectIdentifiers.SecT113r1, SecNamedCurves.Sect113r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect113r2", SecObjectIdentifiers.SecT113r2, SecNamedCurves.Sect113r2Holder.Instance);
			SecNamedCurves.DefineCurve("sect131r1", SecObjectIdentifiers.SecT131r1, SecNamedCurves.Sect131r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect131r2", SecObjectIdentifiers.SecT131r2, SecNamedCurves.Sect131r2Holder.Instance);
			SecNamedCurves.DefineCurve("sect163k1", SecObjectIdentifiers.SecT163k1, SecNamedCurves.Sect163k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect163r1", SecObjectIdentifiers.SecT163r1, SecNamedCurves.Sect163r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect163r2", SecObjectIdentifiers.SecT163r2, SecNamedCurves.Sect163r2Holder.Instance);
			SecNamedCurves.DefineCurve("sect193r1", SecObjectIdentifiers.SecT193r1, SecNamedCurves.Sect193r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect193r2", SecObjectIdentifiers.SecT193r2, SecNamedCurves.Sect193r2Holder.Instance);
			SecNamedCurves.DefineCurve("sect233k1", SecObjectIdentifiers.SecT233k1, SecNamedCurves.Sect233k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect233r1", SecObjectIdentifiers.SecT233r1, SecNamedCurves.Sect233r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect239k1", SecObjectIdentifiers.SecT239k1, SecNamedCurves.Sect239k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect283k1", SecObjectIdentifiers.SecT283k1, SecNamedCurves.Sect283k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect283r1", SecObjectIdentifiers.SecT283r1, SecNamedCurves.Sect283r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect409k1", SecObjectIdentifiers.SecT409k1, SecNamedCurves.Sect409k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect409r1", SecObjectIdentifiers.SecT409r1, SecNamedCurves.Sect409r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect571k1", SecObjectIdentifiers.SecT571k1, SecNamedCurves.Sect571k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect571r1", SecObjectIdentifiers.SecT571r1, SecNamedCurves.Sect571r1Holder.Instance);
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x001825B8 File Offset: 0x001807B8
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = SecNamedCurves.GetOid(name);
			if (oid != null)
			{
				return SecNamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x001825D8 File Offset: 0x001807D8
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)SecNamedCurves.curves[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x00182601 File Offset: 0x00180801
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)SecNamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x00182618 File Offset: 0x00180818
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)SecNamedCurves.names[oid];
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x0018262A File Offset: 0x0018082A
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(SecNamedCurves.names.Values);
			}
		}

		// Token: 0x0400283A RID: 10298
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x0400283B RID: 10299
		private static readonly IDictionary curves = Platform.CreateHashtable();

		// Token: 0x0400283C RID: 10300
		private static readonly IDictionary names = Platform.CreateHashtable();

		// Token: 0x0200098E RID: 2446
		internal class Secp112r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F86 RID: 20358 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp112r1Holder()
			{
			}

			// Token: 0x06004F87 RID: 20359 RVA: 0x001B8C2C File Offset: 0x001B6E2C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("DB7C2ABF62E35E668076BEAD208B");
				BigInteger a = SecNamedCurves.FromHex("DB7C2ABF62E35E668076BEAD2088");
				BigInteger b = SecNamedCurves.FromHex("659EF8BA043916EEDE8911702B22");
				byte[] seed = Hex.Decode("00F50B028E4D696E676875615175290472783FB1");
				BigInteger bigInteger = SecNamedCurves.FromHex("DB7C2ABF62E35E7628DFAC6561C5");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("0409487239995A5EE76B55F9C2F098A89CE5AF8724C0A23E0E0FF77500"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x0400360A RID: 13834
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp112r1Holder();
		}

		// Token: 0x0200098F RID: 2447
		internal class Secp112r2Holder : X9ECParametersHolder
		{
			// Token: 0x06004F89 RID: 20361 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp112r2Holder()
			{
			}

			// Token: 0x06004F8A RID: 20362 RVA: 0x001B8CB0 File Offset: 0x001B6EB0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("DB7C2ABF62E35E668076BEAD208B");
				BigInteger a = SecNamedCurves.FromHex("6127C24C05F38A0AAAF65C0EF02C");
				BigInteger b = SecNamedCurves.FromHex("51DEF1815DB5ED74FCC34C85D709");
				byte[] seed = Hex.Decode("002757A1114D696E6768756151755316C05E0BD4");
				BigInteger bigInteger = SecNamedCurves.FromHex("36DF0AAFD8B8D7597CA10520D04B");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, bigInteger2));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("044BA30AB5E892B4E1649DD0928643ADCD46F5882E3747DEF36E956E97"));
				return new X9ECParameters(eccurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400360B RID: 13835
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp112r2Holder();
		}

		// Token: 0x02000990 RID: 2448
		internal class Secp128r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F8C RID: 20364 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp128r1Holder()
			{
			}

			// Token: 0x06004F8D RID: 20365 RVA: 0x001B8D34 File Offset: 0x001B6F34
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("E87579C11079F43DD824993C2CEE5ED3");
				byte[] seed = Hex.Decode("000E0D4D696E6768756151750CC03A4473D03679");
				BigInteger bigInteger = SecNamedCurves.FromHex("FFFFFFFE0000000075A30D1B9038A115");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("04161FF7528B899B2D0C28607CA52C5B86CF5AC8395BAFEB13C02DA292DDED7A83"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x0400360C RID: 13836
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp128r1Holder();
		}

		// Token: 0x02000991 RID: 2449
		internal class Secp128r2Holder : X9ECParametersHolder
		{
			// Token: 0x06004F8F RID: 20367 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp128r2Holder()
			{
			}

			// Token: 0x06004F90 RID: 20368 RVA: 0x001B8DB8 File Offset: 0x001B6FB8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("D6031998D1B3BBFEBF59CC9BBFF9AEE1");
				BigInteger b = SecNamedCurves.FromHex("5EEEFCA380D02919DC2C6558BB6D8A5D");
				byte[] seed = Hex.Decode("004D696E67687561517512D8F03431FCE63B88F4");
				BigInteger bigInteger = SecNamedCurves.FromHex("3FFFFFFF7FFFFFFFBE0024720613B5A3");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, bigInteger2));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("047B6AA5D85E572983E6FB32A7CDEBC14027B6916A894D3AEE7106FE805FC34B44"));
				return new X9ECParameters(eccurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400360D RID: 13837
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp128r2Holder();
		}

		// Token: 0x02000992 RID: 2450
		internal class Secp160k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F92 RID: 20370 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp160k1Holder()
			{
			}

			// Token: 0x06004F93 RID: 20371 RVA: 0x001B8E3C File Offset: 0x001B703C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC73");
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(7L);
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("0100000000000000000001B8FA16DFAB9ACA16B6B3");
				BigInteger one = BigInteger.One;
				GlvTypeBParameters p = new GlvTypeBParameters(new BigInteger("9ba48cba5ebcb9b6bd33b92830b2a2e0e192f10a", 16), new BigInteger("c39c6c3b3a36d7701b9c71a1f5804ae5d0003f4", 16), new BigInteger[]
				{
					new BigInteger("9162fbe73984472a0a9e", 16),
					new BigInteger("-96341f1138933bc2f505", 16)
				}, new BigInteger[]
				{
					new BigInteger("127971af8721782ecffa3", 16),
					new BigInteger("9162fbe73984472a0a9e", 16)
				}, new BigInteger("9162fbe73984472a0a9d0590", 16), new BigInteger("96341f1138933bc2f503fd44", 16), 176);
				ECCurve eccurve = SecNamedCurves.ConfigureCurveGlv(new FpCurve(q, zero, b, bigInteger, one), p);
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("043B4C382CE37AA192A4019E763036F4F5DD4D7EBB938CF935318FDCED6BC28286531733C3F03C4FEE"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x0400360E RID: 13838
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp160k1Holder();
		}

		// Token: 0x02000993 RID: 2451
		internal class Secp160r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F95 RID: 20373 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp160r1Holder()
			{
			}

			// Token: 0x06004F96 RID: 20374 RVA: 0x001B8F38 File Offset: 0x001B7138
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("1C97BEFC54BD7A8B65ACF89F81D4D4ADC565FA45");
				byte[] seed = Hex.Decode("1053CDE42C14D696E67687561517533BF3F83345");
				BigInteger bigInteger = SecNamedCurves.FromHex("0100000000000000000001F4C8F927AED3CA752257");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("044A96B5688EF573284664698968C38BB913CBFC8223A628553168947D59DCC912042351377AC5FB32"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x0400360F RID: 13839
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp160r1Holder();
		}

		// Token: 0x02000994 RID: 2452
		internal class Secp160r2Holder : X9ECParametersHolder
		{
			// Token: 0x06004F98 RID: 20376 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp160r2Holder()
			{
			}

			// Token: 0x06004F99 RID: 20377 RVA: 0x001B8FBC File Offset: 0x001B71BC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC73");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC70");
				BigInteger b = SecNamedCurves.FromHex("B4E134D3FB59EB8BAB57274904664D5AF50388BA");
				byte[] seed = Hex.Decode("B99B99B099B323E02709A4D696E6768756151751");
				BigInteger bigInteger = SecNamedCurves.FromHex("0100000000000000000000351EE786A818F3A1A16B");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("0452DCB034293A117E1F4FF11B30F7199D3144CE6DFEAFFEF2E331F296E071FA0DF9982CFEA7D43F2E"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003610 RID: 13840
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp160r2Holder();
		}

		// Token: 0x02000995 RID: 2453
		internal class Secp192k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F9B RID: 20379 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp192k1Holder()
			{
			}

			// Token: 0x06004F9C RID: 20380 RVA: 0x001B9040 File Offset: 0x001B7240
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFEE37");
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(3L);
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFE26F2FC170F69466A74DEFD8D");
				BigInteger one = BigInteger.One;
				GlvTypeBParameters p = new GlvTypeBParameters(new BigInteger("bb85691939b869c1d087f601554b96b80cb4f55b35f433c2", 16), new BigInteger("3d84f26c12238d7b4f3d516613c1759033b1a5800175d0b1", 16), new BigInteger[]
				{
					new BigInteger("71169be7330b3038edb025f1", 16),
					new BigInteger("-b3fb3400dec5c4adceb8655c", 16)
				}, new BigInteger[]
				{
					new BigInteger("12511cfe811d0f4e6bc688b4d", 16),
					new BigInteger("71169be7330b3038edb025f1", 16)
				}, new BigInteger("71169be7330b3038edb025f1d0f9", 16), new BigInteger("b3fb3400dec5c4adceb8655d4c94", 16), 208);
				ECCurve eccurve = SecNamedCurves.ConfigureCurveGlv(new FpCurve(q, zero, b, bigInteger, one), p);
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("04DB4FF10EC057E9AE26B07D0280B7F4341DA5D1B1EAE06C7D9B2F2F6D9C5628A7844163D015BE86344082AA88D95E2F9D"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003611 RID: 13841
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp192k1Holder();
		}

		// Token: 0x02000996 RID: 2454
		internal class Secp192r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F9E RID: 20382 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp192r1Holder()
			{
			}

			// Token: 0x06004F9F RID: 20383 RVA: 0x001B913C File Offset: 0x001B733C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("64210519E59C80E70FA7E9AB72243049FEB8DEECC146B9B1");
				byte[] seed = Hex.Decode("3045AE6FC8422F64ED579528D38120EAE12196D5");
				BigInteger bigInteger = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFF99DEF836146BC9B1B4D22831");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("04188DA80EB03090F67CBF20EB43A18800F4FF0AFD82FF101207192B95FFC8DA78631011ED6B24CDD573F977A11E794811"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003612 RID: 13842
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp192r1Holder();
		}

		// Token: 0x02000997 RID: 2455
		internal class Secp224k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FA1 RID: 20385 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp224k1Holder()
			{
			}

			// Token: 0x06004FA2 RID: 20386 RVA: 0x001B91C0 File Offset: 0x001B73C0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFE56D");
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(5L);
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("010000000000000000000000000001DCE8D2EC6184CAF0A971769FB1F7");
				BigInteger one = BigInteger.One;
				GlvTypeBParameters p = new GlvTypeBParameters(new BigInteger("fe0e87005b4e83761908c5131d552a850b3f58b749c37cf5b84d6768", 16), new BigInteger("60dcd2104c4cbc0be6eeefc2bdd610739ec34e317f9b33046c9e4788", 16), new BigInteger[]
				{
					new BigInteger("6b8cf07d4ca75c88957d9d670591", 16),
					new BigInteger("-b8adf1378a6eb73409fa6c9c637d", 16)
				}, new BigInteger[]
				{
					new BigInteger("1243ae1b4d71613bc9f780a03690e", 16),
					new BigInteger("6b8cf07d4ca75c88957d9d670591", 16)
				}, new BigInteger("6b8cf07d4ca75c88957d9d67059037a4", 16), new BigInteger("b8adf1378a6eb73409fa6c9c637ba7f5", 16), 240);
				ECCurve eccurve = SecNamedCurves.ConfigureCurveGlv(new FpCurve(q, zero, b, bigInteger, one), p);
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("04A1455B334DF099DF30FC28A169A467E9E47075A90F7E650EB6B7A45C7E089FED7FBA344282CAFBD6F7E319F7C0B0BD59E2CA4BDB556D61A5"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003613 RID: 13843
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp224k1Holder();
		}

		// Token: 0x02000998 RID: 2456
		internal class Secp224r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FA4 RID: 20388 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp224r1Holder()
			{
			}

			// Token: 0x06004FA5 RID: 20389 RVA: 0x001B92BC File Offset: 0x001B74BC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000000000000000000000001");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFE");
				BigInteger b = SecNamedCurves.FromHex("B4050A850C04B3ABF54132565044B0B7D7BFD8BA270B39432355FFB4");
				byte[] seed = Hex.Decode("BD71344799D5C7FCDC45B59FA3B9AB8F6A948BC5");
				BigInteger bigInteger = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFF16A2E0B8F03E13DD29455C5C2A3D");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("04B70E0CBD6BB4BF7F321390B94A03C1D356C21122343280D6115C1D21BD376388B5F723FB4C22DFE6CD4375A05A07476444D5819985007E34"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003614 RID: 13844
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp224r1Holder();
		}

		// Token: 0x02000999 RID: 2457
		internal class Secp256k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FA7 RID: 20391 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp256k1Holder()
			{
			}

			// Token: 0x06004FA8 RID: 20392 RVA: 0x001B9340 File Offset: 0x001B7540
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F");
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(7L);
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141");
				BigInteger one = BigInteger.One;
				GlvTypeBParameters p = new GlvTypeBParameters(new BigInteger("7ae96a2b657c07106e64479eac3434e99cf0497512f58995c1396c28719501ee", 16), new BigInteger("5363ad4cc05c30e0a5261c028812645a122e22ea20816678df02967c1b23bd72", 16), new BigInteger[]
				{
					new BigInteger("3086d221a7d46bcde86c90e49284eb15", 16),
					new BigInteger("-e4437ed6010e88286f547fa90abfe4c3", 16)
				}, new BigInteger[]
				{
					new BigInteger("114ca50f7a8e2f3f657c1108d9d44cfd8", 16),
					new BigInteger("3086d221a7d46bcde86c90e49284eb15", 16)
				}, new BigInteger("3086d221a7d46bcde86c90e49284eb153dab", 16), new BigInteger("e4437ed6010e88286f547fa90abfe4c42212", 16), 272);
				ECCurve eccurve = SecNamedCurves.ConfigureCurveGlv(new FpCurve(q, zero, b, bigInteger, one), p);
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("0479BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B8"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003615 RID: 13845
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp256k1Holder();
		}

		// Token: 0x0200099A RID: 2458
		internal class Secp256r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FAA RID: 20394 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp256r1Holder()
			{
			}

			// Token: 0x06004FAB RID: 20395 RVA: 0x001B943C File Offset: 0x001B763C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("5AC635D8AA3A93E7B3EBBD55769886BC651D06B0CC53B0F63BCE3C3E27D2604B");
				byte[] seed = Hex.Decode("C49D360886E704936A6678E1139D26B7819F7E90");
				BigInteger bigInteger = SecNamedCurves.FromHex("FFFFFFFF00000000FFFFFFFFFFFFFFFFBCE6FAADA7179E84F3B9CAC2FC632551");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("046B17D1F2E12C4247F8BCE6E563A440F277037D812DEB33A0F4A13945D898C2964FE342E2FE1A7F9B8EE7EB4A7C0F9E162BCE33576B315ECECBB6406837BF51F5"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003616 RID: 13846
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp256r1Holder();
		}

		// Token: 0x0200099B RID: 2459
		internal class Secp384r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FAD RID: 20397 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp384r1Holder()
			{
			}

			// Token: 0x06004FAE RID: 20398 RVA: 0x001B94C0 File Offset: 0x001B76C0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("B3312FA7E23EE7E4988E056BE3F82D19181D9C6EFE8141120314088F5013875AC656398D8A2ED19D2A85C8EDD3EC2AEF");
				byte[] seed = Hex.Decode("A335926AA319A27A1D00896A6773A4827ACDAC73");
				BigInteger bigInteger = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC7634D81F4372DDF581A0DB248B0A77AECEC196ACCC52973");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("04AA87CA22BE8B05378EB1C71EF320AD746E1D3B628BA79B9859F741E082542A385502F25DBF55296C3A545E3872760AB73617DE4A96262C6F5D9E98BF9292DC29F8F41DBD289A147CE9DA3113B5F0B8C00A60B1CE1D7E819D7A431D7C90EA0E5F"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003617 RID: 13847
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp384r1Holder();
		}

		// Token: 0x0200099C RID: 2460
		internal class Secp521r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FB0 RID: 20400 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Secp521r1Holder()
			{
			}

			// Token: 0x06004FB1 RID: 20401 RVA: 0x001B9544 File Offset: 0x001B7744
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("0051953EB9618E1C9A1F929A21A0B68540EEA2DA725B99B315F3B8B489918EF109E156193951EC7E937B1652C0BD3BB1BF073573DF883D2C34F1EF451FD46B503F00");
				byte[] seed = Hex.Decode("D09E8800291CB85396CC6717393284AAA0DA64BA");
				BigInteger bigInteger = SecNamedCurves.FromHex("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA51868783BF2F966B7FCC0148F709A5D03BB5C9B8899C47AEBB6FB71E91386409");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = SecNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("0400C6858E06B70404E9CD9E3ECB662395B4429C648139053FB521F828AF606B4D3DBAA14B5E77EFE75928FE1DC127A2FFA8DE3348B3C1856A429BF97E7E31C2E5BD66011839296A789A3BC0045C8A5FB42C7D1BD998F54449579B446817AFBD17273E662C97EE72995EF42640C550B9013FAD0761353C7086A272C24088BE94769FD16650"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003618 RID: 13848
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp521r1Holder();
		}

		// Token: 0x0200099D RID: 2461
		internal class Sect113r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FB3 RID: 20403 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect113r1Holder()
			{
			}

			// Token: 0x06004FB4 RID: 20404 RVA: 0x001B95C8 File Offset: 0x001B77C8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("003088250CA6E7C7FE649CE85820F7");
				BigInteger b = SecNamedCurves.FromHex("00E8BEE4D3E2260744188BE0E9C723");
				byte[] seed = Hex.Decode("10E723AB14D696E6768756151756FEBF8FCB49A9");
				BigInteger bigInteger = SecNamedCurves.FromHex("0100000000000000D9CCEC8A39E56F");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(113, 9, a, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("04009D73616F35F4AB1407D73562C10F00A52830277958EE84D1315ED31886"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003619 RID: 13849
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect113r1Holder();

			// Token: 0x0400361A RID: 13850
			private const int m = 113;

			// Token: 0x0400361B RID: 13851
			private const int k = 9;
		}

		// Token: 0x0200099E RID: 2462
		internal class Sect113r2Holder : X9ECParametersHolder
		{
			// Token: 0x06004FB6 RID: 20406 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect113r2Holder()
			{
			}

			// Token: 0x06004FB7 RID: 20407 RVA: 0x001B9644 File Offset: 0x001B7844
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("00689918DBEC7E5A0DD6DFC0AA55C7");
				BigInteger b = SecNamedCurves.FromHex("0095E9A9EC9B297BD4BF36E059184F");
				byte[] seed = Hex.Decode("10C0FB15760860DEF1EEF4D696E676875615175D");
				BigInteger bigInteger = SecNamedCurves.FromHex("010000000000000108789B2496AF93");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(113, 9, a, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("0401A57A6A7B26CA5EF52FCDB816479700B3ADC94ED1FE674C06E695BABA1D"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400361C RID: 13852
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect113r2Holder();

			// Token: 0x0400361D RID: 13853
			private const int m = 113;

			// Token: 0x0400361E RID: 13854
			private const int k = 9;
		}

		// Token: 0x0200099F RID: 2463
		internal class Sect131r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FB9 RID: 20409 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect131r1Holder()
			{
			}

			// Token: 0x06004FBA RID: 20410 RVA: 0x001B96C0 File Offset: 0x001B78C0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("07A11B09A76B562144418FF3FF8C2570B8");
				BigInteger b = SecNamedCurves.FromHex("0217C05610884B63B9C6C7291678F9D341");
				byte[] seed = Hex.Decode("4D696E676875615175985BD3ADBADA21B43A97E2");
				BigInteger bigInteger = SecNamedCurves.FromHex("0400000000000000023123953A9464B54D");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(131, 2, 3, 8, a, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("040081BAF91FDF9833C40F9C181343638399078C6E7EA38C001F73C8134B1B4EF9E150"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400361F RID: 13855
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect131r1Holder();

			// Token: 0x04003620 RID: 13856
			private const int m = 131;

			// Token: 0x04003621 RID: 13857
			private const int k1 = 2;

			// Token: 0x04003622 RID: 13858
			private const int k2 = 3;

			// Token: 0x04003623 RID: 13859
			private const int k3 = 8;
		}

		// Token: 0x020009A0 RID: 2464
		internal class Sect131r2Holder : X9ECParametersHolder
		{
			// Token: 0x06004FBC RID: 20412 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect131r2Holder()
			{
			}

			// Token: 0x06004FBD RID: 20413 RVA: 0x001B9740 File Offset: 0x001B7940
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("03E5A88919D7CAFCBF415F07C2176573B2");
				BigInteger b = SecNamedCurves.FromHex("04B8266A46C55657AC734CE38F018F2192");
				byte[] seed = Hex.Decode("985BD3ADBAD4D696E676875615175A21B43A97E3");
				BigInteger bigInteger = SecNamedCurves.FromHex("0400000000000000016954A233049BA98F");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(131, 2, 3, 8, a, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("040356DCD8F2F95031AD652D23951BB366A80648F06D867940A5366D9E265DE9EB240F"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003624 RID: 13860
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect131r2Holder();

			// Token: 0x04003625 RID: 13861
			private const int m = 131;

			// Token: 0x04003626 RID: 13862
			private const int k1 = 2;

			// Token: 0x04003627 RID: 13863
			private const int k2 = 3;

			// Token: 0x04003628 RID: 13864
			private const int k3 = 8;
		}

		// Token: 0x020009A1 RID: 2465
		internal class Sect163k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FBF RID: 20415 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect163k1Holder()
			{
			}

			// Token: 0x06004FC0 RID: 20416 RVA: 0x001B97C0 File Offset: 0x001B79C0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger one = BigInteger.One;
				BigInteger one2 = BigInteger.One;
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("04000000000000000000020108A2E0CC0D99F8A5EF");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(163, 3, 6, 7, one, one2, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("0402FE13C0537BBC11ACAA07D793DE4E6D5E5C94EEE80289070FB05D38FF58321F2E800536D538CCDAA3D9"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003629 RID: 13865
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect163k1Holder();

			// Token: 0x0400362A RID: 13866
			private const int m = 163;

			// Token: 0x0400362B RID: 13867
			private const int k1 = 3;

			// Token: 0x0400362C RID: 13868
			private const int k2 = 6;

			// Token: 0x0400362D RID: 13869
			private const int k3 = 7;
		}

		// Token: 0x020009A2 RID: 2466
		internal class Sect163r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FC2 RID: 20418 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect163r1Holder()
			{
			}

			// Token: 0x06004FC3 RID: 20419 RVA: 0x001B982C File Offset: 0x001B7A2C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("07B6882CAAEFA84F9554FF8428BD88E246D2782AE2");
				BigInteger b = SecNamedCurves.FromHex("0713612DCDDCB40AAB946BDA29CA91F73AF958AFD9");
				byte[] seed = Hex.Decode("24B7B137C8A14D696E6768756151756FD0DA2E5C");
				BigInteger bigInteger = SecNamedCurves.FromHex("03FFFFFFFFFFFFFFFFFFFF48AAB689C29CA710279B");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(163, 3, 6, 7, a, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("040369979697AB43897789566789567F787A7876A65400435EDB42EFAFB2989D51FEFCE3C80988F41FF883"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400362E RID: 13870
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect163r1Holder();

			// Token: 0x0400362F RID: 13871
			private const int m = 163;

			// Token: 0x04003630 RID: 13872
			private const int k1 = 3;

			// Token: 0x04003631 RID: 13873
			private const int k2 = 6;

			// Token: 0x04003632 RID: 13874
			private const int k3 = 7;
		}

		// Token: 0x020009A3 RID: 2467
		internal class Sect163r2Holder : X9ECParametersHolder
		{
			// Token: 0x06004FC5 RID: 20421 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect163r2Holder()
			{
			}

			// Token: 0x06004FC6 RID: 20422 RVA: 0x001B98AC File Offset: 0x001B7AAC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger one = BigInteger.One;
				BigInteger b = SecNamedCurves.FromHex("020A601907B8C953CA1481EB10512F78744A3205FD");
				byte[] seed = Hex.Decode("85E25BFE5C86226CDB12016F7553F9D0E693A268");
				BigInteger bigInteger = SecNamedCurves.FromHex("040000000000000000000292FE77E70C12A4234C33");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(163, 3, 6, 7, one, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("0403F0EBA16286A2D57EA0991168D4994637E8343E3600D51FBC6C71A0094FA2CDD545B11C5C0C797324F1"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003633 RID: 13875
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect163r2Holder();

			// Token: 0x04003634 RID: 13876
			private const int m = 163;

			// Token: 0x04003635 RID: 13877
			private const int k1 = 3;

			// Token: 0x04003636 RID: 13878
			private const int k2 = 6;

			// Token: 0x04003637 RID: 13879
			private const int k3 = 7;
		}

		// Token: 0x020009A4 RID: 2468
		internal class Sect193r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FC8 RID: 20424 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect193r1Holder()
			{
			}

			// Token: 0x06004FC9 RID: 20425 RVA: 0x001B9924 File Offset: 0x001B7B24
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("0017858FEB7A98975169E171F77B4087DE098AC8A911DF7B01");
				BigInteger b = SecNamedCurves.FromHex("00FDFB49BFE6C3A89FACADAA7A1E5BBC7CC1C2E5D831478814");
				byte[] seed = Hex.Decode("103FAEC74D696E676875615175777FC5B191EF30");
				BigInteger bigInteger = SecNamedCurves.FromHex("01000000000000000000000000C7F34A778F443ACC920EBA49");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(193, 15, a, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("0401F481BC5F0FF84A74AD6CDF6FDEF4BF6179625372D8C0C5E10025E399F2903712CCF3EA9E3A1AD17FB0B3201B6AF7CE1B05"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003638 RID: 13880
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect193r1Holder();

			// Token: 0x04003639 RID: 13881
			private const int m = 193;

			// Token: 0x0400363A RID: 13882
			private const int k = 15;
		}

		// Token: 0x020009A5 RID: 2469
		internal class Sect193r2Holder : X9ECParametersHolder
		{
			// Token: 0x06004FCB RID: 20427 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect193r2Holder()
			{
			}

			// Token: 0x06004FCC RID: 20428 RVA: 0x001B99A0 File Offset: 0x001B7BA0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("0163F35A5137C2CE3EA6ED8667190B0BC43ECD69977702709B");
				BigInteger b = SecNamedCurves.FromHex("00C9BB9E8927D4D64C377E2AB2856A5B16E3EFB7F61D4316AE");
				byte[] seed = Hex.Decode("10B7B4D696E676875615175137C8A16FD0DA2211");
				BigInteger bigInteger = SecNamedCurves.FromHex("010000000000000000000000015AAB561B005413CCD4EE99D5");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(193, 15, a, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("0400D9B67D192E0367C803F39E1A7E82CA14A651350AAE617E8F01CE94335607C304AC29E7DEFBD9CA01F596F927224CDECF6C"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400363B RID: 13883
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect193r2Holder();

			// Token: 0x0400363C RID: 13884
			private const int m = 193;

			// Token: 0x0400363D RID: 13885
			private const int k = 15;
		}

		// Token: 0x020009A6 RID: 2470
		internal class Sect233k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FCE RID: 20430 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect233k1Holder()
			{
			}

			// Token: 0x06004FCF RID: 20431 RVA: 0x001B9A1C File Offset: 0x001B7C1C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger one = BigInteger.One;
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("8000000000000000000000000000069D5BB915BCD46EFB1AD5F173ABDF");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				F2mCurve f2mCurve = new F2mCurve(233, 74, zero, one, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("04017232BA853A7E731AF129F22FF4149563A419C26BF50A4C9D6EEFAD612601DB537DECE819B7F70F555A67C427A8CD9BF18AEB9B56E0C11056FAE6A3"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400363E RID: 13886
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect233k1Holder();

			// Token: 0x0400363F RID: 13887
			private const int m = 233;

			// Token: 0x04003640 RID: 13888
			private const int k = 74;
		}

		// Token: 0x020009A7 RID: 2471
		internal class Sect233r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FD1 RID: 20433 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect233r1Holder()
			{
			}

			// Token: 0x06004FD2 RID: 20434 RVA: 0x001B9A88 File Offset: 0x001B7C88
			protected override X9ECParameters CreateParameters()
			{
				BigInteger one = BigInteger.One;
				BigInteger b = SecNamedCurves.FromHex("0066647EDE6C332C7F8C0923BB58213B333B20E9CE4281FE115F7D8F90AD");
				byte[] seed = Hex.Decode("74D59FF07F6B413D0EA14B344B20A2DB049B50C3");
				BigInteger bigInteger = SecNamedCurves.FromHex("01000000000000000000000000000013E974E72F8A6922031D2603CFE0D7");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(233, 74, one, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("0400FAC9DFCBAC8313BB2139F1BB755FEF65BC391F8B36F8F8EB7371FD558B01006A08A41903350678E58528BEBF8A0BEFF867A7CA36716F7E01F81052"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003641 RID: 13889
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect233r1Holder();

			// Token: 0x04003642 RID: 13890
			private const int m = 233;

			// Token: 0x04003643 RID: 13891
			private const int k = 74;
		}

		// Token: 0x020009A8 RID: 2472
		internal class Sect239k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FD4 RID: 20436 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect239k1Holder()
			{
			}

			// Token: 0x06004FD5 RID: 20437 RVA: 0x001B9B00 File Offset: 0x001B7D00
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger one = BigInteger.One;
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("2000000000000000000000000000005A79FEC67CB6E91F1C1DA800E478A5");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				F2mCurve f2mCurve = new F2mCurve(239, 158, zero, one, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("0429A0B6A887A983E9730988A68727A8B2D126C44CC2CC7B2A6555193035DC76310804F12E549BDB011C103089E73510ACB275FC312A5DC6B76553F0CA"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003644 RID: 13892
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect239k1Holder();

			// Token: 0x04003645 RID: 13893
			private const int m = 239;

			// Token: 0x04003646 RID: 13894
			private const int k = 158;
		}

		// Token: 0x020009A9 RID: 2473
		internal class Sect283k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FD7 RID: 20439 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect283k1Holder()
			{
			}

			// Token: 0x06004FD8 RID: 20440 RVA: 0x001B9B6C File Offset: 0x001B7D6C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger one = BigInteger.One;
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE9AE2ED07577265DFF7F94451E061E163C61");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				F2mCurve f2mCurve = new F2mCurve(283, 5, 7, 12, zero, one, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("040503213F78CA44883F1A3B8162F188E553CD265F23C1567A16876913B0C2AC245849283601CCDA380F1C9E318D90F95D07E5426FE87E45C0E8184698E45962364E34116177DD2259"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003647 RID: 13895
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect283k1Holder();

			// Token: 0x04003648 RID: 13896
			private const int m = 283;

			// Token: 0x04003649 RID: 13897
			private const int k1 = 5;

			// Token: 0x0400364A RID: 13898
			private const int k2 = 7;

			// Token: 0x0400364B RID: 13899
			private const int k3 = 12;
		}

		// Token: 0x020009AA RID: 2474
		internal class Sect283r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FDA RID: 20442 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect283r1Holder()
			{
			}

			// Token: 0x06004FDB RID: 20443 RVA: 0x001B9BD8 File Offset: 0x001B7DD8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger one = BigInteger.One;
				BigInteger b = SecNamedCurves.FromHex("027B680AC8B8596DA5A4AF8A19A0303FCA97FD7645309FA2A581485AF6263E313B79A2F5");
				byte[] seed = Hex.Decode("77E2B07370EB0F832A6DD5B62DFC88CD06BB84BE");
				BigInteger bigInteger = SecNamedCurves.FromHex("03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEF90399660FC938A90165B042A7CEFADB307");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(283, 5, 7, 12, one, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("0405F939258DB7DD90E1934F8C70B0DFEC2EED25B8557EAC9C80E2E198F8CDBECD86B1205303676854FE24141CB98FE6D4B20D02B4516FF702350EDDB0826779C813F0DF45BE8112F4"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400364C RID: 13900
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect283r1Holder();

			// Token: 0x0400364D RID: 13901
			private const int m = 283;

			// Token: 0x0400364E RID: 13902
			private const int k1 = 5;

			// Token: 0x0400364F RID: 13903
			private const int k2 = 7;

			// Token: 0x04003650 RID: 13904
			private const int k3 = 12;
		}

		// Token: 0x020009AB RID: 2475
		internal class Sect409k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FDD RID: 20445 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect409k1Holder()
			{
			}

			// Token: 0x06004FDE RID: 20446 RVA: 0x001B9C54 File Offset: 0x001B7E54
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger one = BigInteger.One;
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE5F83B2D4EA20400EC4557D5ED3E3E7CA5B4B5C83B8E01E5FCF");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				F2mCurve f2mCurve = new F2mCurve(409, 87, zero, one, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("040060F05F658F49C1AD3AB1890F7184210EFD0987E307C84C27ACCFB8F9F67CC2C460189EB5AAAA62EE222EB1B35540CFE902374601E369050B7C4E42ACBA1DACBF04299C3460782F918EA427E6325165E9EA10E3DA5F6C42E9C55215AA9CA27A5863EC48D8E0286B"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003651 RID: 13905
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect409k1Holder();

			// Token: 0x04003652 RID: 13906
			private const int m = 409;

			// Token: 0x04003653 RID: 13907
			private const int k = 87;
		}

		// Token: 0x020009AC RID: 2476
		internal class Sect409r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FE0 RID: 20448 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect409r1Holder()
			{
			}

			// Token: 0x06004FE1 RID: 20449 RVA: 0x001B9CC0 File Offset: 0x001B7EC0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger one = BigInteger.One;
				BigInteger b = SecNamedCurves.FromHex("0021A5C2C8EE9FEB5C4B9A753B7B476B7FD6422EF1F3DD674761FA99D6AC27C8A9A197B272822F6CD57A55AA4F50AE317B13545F");
				byte[] seed = Hex.Decode("4099B5A457F9D69F79213D094C4BCD4D4262210B");
				BigInteger bigInteger = SecNamedCurves.FromHex("010000000000000000000000000000000000000000000000000001E2AAD6A612F33307BE5FA47C3C9E052F838164CD37D9A21173");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(409, 87, one, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("04015D4860D088DDB3496B0C6064756260441CDE4AF1771D4DB01FFE5B34E59703DC255A868A1180515603AEAB60794E54BB7996A70061B1CFAB6BE5F32BBFA78324ED106A7636B9C5A7BD198D0158AA4F5488D08F38514F1FDF4B4F40D2181B3681C364BA0273C706"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003654 RID: 13908
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect409r1Holder();

			// Token: 0x04003655 RID: 13909
			private const int m = 409;

			// Token: 0x04003656 RID: 13910
			private const int k = 87;
		}

		// Token: 0x020009AD RID: 2477
		internal class Sect571k1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FE3 RID: 20451 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect571k1Holder()
			{
			}

			// Token: 0x06004FE4 RID: 20452 RVA: 0x001B9D38 File Offset: 0x001B7F38
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger one = BigInteger.One;
				byte[] seed = null;
				BigInteger bigInteger = SecNamedCurves.FromHex("020000000000000000000000000000000000000000000000000000000000000000000000131850E1F19A63E4B391A8DB917F4138B630D84BE5D639381E91DEB45CFE778F637C1001");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				F2mCurve f2mCurve = new F2mCurve(571, 2, 5, 10, zero, one, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("04026EB7A859923FBC82189631F8103FE4AC9CA2970012D5D46024804801841CA44370958493B205E647DA304DB4CEB08CBBD1BA39494776FB988B47174DCA88C7E2945283A01C89720349DC807F4FBF374F4AEADE3BCA95314DD58CEC9F307A54FFC61EFC006D8A2C9D4979C0AC44AEA74FBEBBB9F772AEDCB620B01A7BA7AF1B320430C8591984F601CD4C143EF1C7A3"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x04003657 RID: 13911
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect571k1Holder();

			// Token: 0x04003658 RID: 13912
			private const int m = 571;

			// Token: 0x04003659 RID: 13913
			private const int k1 = 2;

			// Token: 0x0400365A RID: 13914
			private const int k2 = 5;

			// Token: 0x0400365B RID: 13915
			private const int k3 = 10;
		}

		// Token: 0x020009AE RID: 2478
		internal class Sect571r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FE6 RID: 20454 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Sect571r1Holder()
			{
			}

			// Token: 0x06004FE7 RID: 20455 RVA: 0x001B9DA4 File Offset: 0x001B7FA4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger one = BigInteger.One;
				BigInteger b = SecNamedCurves.FromHex("02F40E7E2221F295DE297117B7F3D62F5C6A97FFCB8CEFF1CD6BA8CE4A9A18AD84FFABBD8EFA59332BE7AD6756A66E294AFD185A78FF12AA520E4DE739BACA0C7FFEFF7F2955727A");
				byte[] seed = Hex.Decode("2AA058F73A0E33AB486B0F610410C53A7F132310");
				BigInteger bigInteger = SecNamedCurves.FromHex("03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE661CE18FF55987308059B186823851EC7DD9CA1161DE93D5174D66E8382E9BB2FE84E47");
				BigInteger bigInteger2 = BigInteger.ValueOf(2L);
				F2mCurve f2mCurve = new F2mCurve(571, 2, 5, 10, one, b, bigInteger, bigInteger2);
				X9ECPoint g = new X9ECPoint(f2mCurve, Hex.Decode("040303001D34B856296C16C0D40D3CD7750A93D1D2955FA80AA5F40FC8DB7B2ABDBDE53950F4C0D293CDD711A35B67FB1499AE60038614F1394ABFA3B4C850D927E1E7769C8EEC2D19037BF27342DA639B6DCCFFFEB73D69D78C6C27A6009CBBCA1980F8533921E8A684423E43BAB08A576291AF8F461BB2A8B3531D2F0485C19B16E2F1516E23DD3C1A4827AF1B8AC15B"));
				return new X9ECParameters(f2mCurve, g, bigInteger, bigInteger2, seed);
			}

			// Token: 0x0400365C RID: 13916
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect571r1Holder();

			// Token: 0x0400365D RID: 13917
			private const int m = 571;

			// Token: 0x0400365E RID: 13918
			private const int k1 = 2;

			// Token: 0x0400365F RID: 13919
			private const int k2 = 5;

			// Token: 0x04003660 RID: 13920
			private const int k3 = 10;
		}
	}
}
