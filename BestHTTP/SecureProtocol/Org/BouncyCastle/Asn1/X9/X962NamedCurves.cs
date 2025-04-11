using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000664 RID: 1636
	public sealed class X962NamedCurves
	{
		// Token: 0x06003D1C RID: 15644 RVA: 0x00023EF4 File Offset: 0x000220F4
		private X962NamedCurves()
		{
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x00175ABE File Offset: 0x00173CBE
		private static void DefineCurve(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			X962NamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			X962NamedCurves.names.Add(oid, name);
			X962NamedCurves.curves.Add(oid, holder);
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x00175AEC File Offset: 0x00173CEC
		static X962NamedCurves()
		{
			X962NamedCurves.DefineCurve("prime192v1", X9ObjectIdentifiers.Prime192v1, X962NamedCurves.Prime192v1Holder.Instance);
			X962NamedCurves.DefineCurve("prime192v2", X9ObjectIdentifiers.Prime192v2, X962NamedCurves.Prime192v2Holder.Instance);
			X962NamedCurves.DefineCurve("prime192v3", X9ObjectIdentifiers.Prime192v3, X962NamedCurves.Prime192v3Holder.Instance);
			X962NamedCurves.DefineCurve("prime239v1", X9ObjectIdentifiers.Prime239v1, X962NamedCurves.Prime239v1Holder.Instance);
			X962NamedCurves.DefineCurve("prime239v2", X9ObjectIdentifiers.Prime239v2, X962NamedCurves.Prime239v2Holder.Instance);
			X962NamedCurves.DefineCurve("prime239v3", X9ObjectIdentifiers.Prime239v3, X962NamedCurves.Prime239v3Holder.Instance);
			X962NamedCurves.DefineCurve("prime256v1", X9ObjectIdentifiers.Prime256v1, X962NamedCurves.Prime256v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb163v1", X9ObjectIdentifiers.C2Pnb163v1, X962NamedCurves.C2pnb163v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb163v2", X9ObjectIdentifiers.C2Pnb163v2, X962NamedCurves.C2pnb163v2Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb163v3", X9ObjectIdentifiers.C2Pnb163v3, X962NamedCurves.C2pnb163v3Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb176w1", X9ObjectIdentifiers.C2Pnb176w1, X962NamedCurves.C2pnb176w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb191v1", X9ObjectIdentifiers.C2Tnb191v1, X962NamedCurves.C2tnb191v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb191v2", X9ObjectIdentifiers.C2Tnb191v2, X962NamedCurves.C2tnb191v2Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb191v3", X9ObjectIdentifiers.C2Tnb191v3, X962NamedCurves.C2tnb191v3Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb208w1", X9ObjectIdentifiers.C2Pnb208w1, X962NamedCurves.C2pnb208w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb239v1", X9ObjectIdentifiers.C2Tnb239v1, X962NamedCurves.C2tnb239v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb239v2", X9ObjectIdentifiers.C2Tnb239v2, X962NamedCurves.C2tnb239v2Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb239v3", X9ObjectIdentifiers.C2Tnb239v3, X962NamedCurves.C2tnb239v3Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb272w1", X9ObjectIdentifiers.C2Pnb272w1, X962NamedCurves.C2pnb272w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb304w1", X9ObjectIdentifiers.C2Pnb304w1, X962NamedCurves.C2pnb304w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb359v1", X9ObjectIdentifiers.C2Tnb359v1, X962NamedCurves.C2tnb359v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb368w1", X9ObjectIdentifiers.C2Pnb368w1, X962NamedCurves.C2pnb368w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb431r1", X9ObjectIdentifiers.C2Tnb431r1, X962NamedCurves.C2tnb431r1Holder.Instance);
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x00175CE4 File Offset: 0x00173EE4
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = X962NamedCurves.GetOid(name);
			if (oid != null)
			{
				return X962NamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x00175D04 File Offset: 0x00173F04
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)X962NamedCurves.curves[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x00175D2D File Offset: 0x00173F2D
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)X962NamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x00175D44 File Offset: 0x00173F44
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)X962NamedCurves.names[oid];
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x00175D56 File Offset: 0x00173F56
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(X962NamedCurves.names.Values);
			}
		}

		// Token: 0x040025E0 RID: 9696
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x040025E1 RID: 9697
		private static readonly IDictionary curves = Platform.CreateHashtable();

		// Token: 0x040025E2 RID: 9698
		private static readonly IDictionary names = Platform.CreateHashtable();

		// Token: 0x02000967 RID: 2407
		internal class Prime192v1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F15 RID: 20245 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Prime192v1Holder()
			{
			}

			// Token: 0x06004F16 RID: 20246 RVA: 0x001B7AF4 File Offset: 0x001B5CF4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("ffffffffffffffffffffffff99def836146bc9b1b4d22831", 16);
				BigInteger one = BigInteger.One;
				FpCurve fpCurve = new FpCurve(new BigInteger("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF", 16), new BigInteger("fffffffffffffffffffffffffffffffefffffffffffffffc", 16), new BigInteger("64210519e59c80e70fa7e9ab72243049feb8deecc146b9b1", 16), bigInteger, one);
				return new X9ECParameters(fpCurve, new X9ECPoint(fpCurve, Hex.Decode("03188da80eb03090f67cbf20eb43a18800f4ff0afd82ff1012")), bigInteger, one, Hex.Decode("3045AE6FC8422f64ED579528D38120EAE12196D5"));
			}

			// Token: 0x040035E1 RID: 13793
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime192v1Holder();
		}

		// Token: 0x02000968 RID: 2408
		internal class Prime192v2Holder : X9ECParametersHolder
		{
			// Token: 0x06004F18 RID: 20248 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Prime192v2Holder()
			{
			}

			// Token: 0x06004F19 RID: 20249 RVA: 0x001B7B6C File Offset: 0x001B5D6C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("fffffffffffffffffffffffe5fb1a724dc80418648d8dd31", 16);
				BigInteger one = BigInteger.One;
				FpCurve fpCurve = new FpCurve(new BigInteger("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF", 16), new BigInteger("fffffffffffffffffffffffffffffffefffffffffffffffc", 16), new BigInteger("cc22d6dfb95c6b25e49c0d6364a4e5980c393aa21668d953", 16), bigInteger, one);
				return new X9ECParameters(fpCurve, new X9ECPoint(fpCurve, Hex.Decode("03eea2bae7e1497842f2de7769cfe9c989c072ad696f48034a")), bigInteger, one, Hex.Decode("31a92ee2029fd10d901b113e990710f0d21ac6b6"));
			}

			// Token: 0x040035E2 RID: 13794
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime192v2Holder();
		}

		// Token: 0x02000969 RID: 2409
		internal class Prime192v3Holder : X9ECParametersHolder
		{
			// Token: 0x06004F1B RID: 20251 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Prime192v3Holder()
			{
			}

			// Token: 0x06004F1C RID: 20252 RVA: 0x001B7BE4 File Offset: 0x001B5DE4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("ffffffffffffffffffffffff7a62d031c83f4294f640ec13", 16);
				BigInteger one = BigInteger.One;
				FpCurve fpCurve = new FpCurve(new BigInteger("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF", 16), new BigInteger("fffffffffffffffffffffffffffffffefffffffffffffffc", 16), new BigInteger("22123dc2395a05caa7423daeccc94760a7d462256bd56916", 16), bigInteger, one);
				return new X9ECParameters(fpCurve, new X9ECPoint(fpCurve, Hex.Decode("027d29778100c65a1da1783716588dce2b8b4aee8e228f1896")), bigInteger, one, Hex.Decode("c469684435deb378c4b65ca9591e2a5763059a2e"));
			}

			// Token: 0x040035E3 RID: 13795
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime192v3Holder();
		}

		// Token: 0x0200096A RID: 2410
		internal class Prime239v1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F1E RID: 20254 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Prime239v1Holder()
			{
			}

			// Token: 0x06004F1F RID: 20255 RVA: 0x001B7C5C File Offset: 0x001B5E5C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("7fffffffffffffffffffffff7fffff9e5e9a9f5d9071fbd1522688909d0b", 16);
				BigInteger one = BigInteger.One;
				FpCurve fpCurve = new FpCurve(new BigInteger("883423532389192164791648750360308885314476597252960362792450860609699839"), new BigInteger("7fffffffffffffffffffffff7fffffffffff8000000000007ffffffffffc", 16), new BigInteger("6b016c3bdcf18941d0d654921475ca71a9db2fb27d1d37796185c2942c0a", 16), bigInteger, one);
				return new X9ECParameters(fpCurve, new X9ECPoint(fpCurve, Hex.Decode("020ffa963cdca8816ccc33b8642bedf905c3d358573d3f27fbbd3b3cb9aaaf")), bigInteger, one, Hex.Decode("e43bb460f0b80cc0c0b075798e948060f8321b7d"));
			}

			// Token: 0x040035E4 RID: 13796
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime239v1Holder();
		}

		// Token: 0x0200096B RID: 2411
		internal class Prime239v2Holder : X9ECParametersHolder
		{
			// Token: 0x06004F21 RID: 20257 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Prime239v2Holder()
			{
			}

			// Token: 0x06004F22 RID: 20258 RVA: 0x001B7CD4 File Offset: 0x001B5ED4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("7fffffffffffffffffffffff800000cfa7e8594377d414c03821bc582063", 16);
				BigInteger one = BigInteger.One;
				FpCurve fpCurve = new FpCurve(new BigInteger("883423532389192164791648750360308885314476597252960362792450860609699839"), new BigInteger("7fffffffffffffffffffffff7fffffffffff8000000000007ffffffffffc", 16), new BigInteger("617fab6832576cbbfed50d99f0249c3fee58b94ba0038c7ae84c8c832f2c", 16), bigInteger, one);
				return new X9ECParameters(fpCurve, new X9ECPoint(fpCurve, Hex.Decode("0238af09d98727705120c921bb5e9e26296a3cdcf2f35757a0eafd87b830e7")), bigInteger, one, Hex.Decode("e8b4011604095303ca3b8099982be09fcb9ae616"));
			}

			// Token: 0x040035E5 RID: 13797
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime239v2Holder();
		}

		// Token: 0x0200096C RID: 2412
		internal class Prime239v3Holder : X9ECParametersHolder
		{
			// Token: 0x06004F24 RID: 20260 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Prime239v3Holder()
			{
			}

			// Token: 0x06004F25 RID: 20261 RVA: 0x001B7D4C File Offset: 0x001B5F4C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("7fffffffffffffffffffffff7fffff975deb41b3a6057c3c432146526551", 16);
				BigInteger one = BigInteger.One;
				FpCurve fpCurve = new FpCurve(new BigInteger("883423532389192164791648750360308885314476597252960362792450860609699839"), new BigInteger("7fffffffffffffffffffffff7fffffffffff8000000000007ffffffffffc", 16), new BigInteger("255705fa2a306654b1f4cb03d6a750a30c250102d4988717d9ba15ab6d3e", 16), bigInteger, one);
				return new X9ECParameters(fpCurve, new X9ECPoint(fpCurve, Hex.Decode("036768ae8e18bb92cfcf005c949aa2c6d94853d0e660bbf854b1c9505fe95a")), bigInteger, one, Hex.Decode("7d7374168ffe3471b60a857686a19475d3bfa2ff"));
			}

			// Token: 0x040035E6 RID: 13798
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime239v3Holder();
		}

		// Token: 0x0200096D RID: 2413
		internal class Prime256v1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F27 RID: 20263 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Prime256v1Holder()
			{
			}

			// Token: 0x06004F28 RID: 20264 RVA: 0x001B7DC4 File Offset: 0x001B5FC4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("ffffffff00000000ffffffffffffffffbce6faada7179e84f3b9cac2fc632551", 16);
				BigInteger one = BigInteger.One;
				FpCurve fpCurve = new FpCurve(new BigInteger("115792089210356248762697446949407573530086143415290314195533631308867097853951"), new BigInteger("ffffffff00000001000000000000000000000000fffffffffffffffffffffffc", 16), new BigInteger("5ac635d8aa3a93e7b3ebbd55769886bc651d06b0cc53b0f63bce3c3e27d2604b", 16), bigInteger, one);
				return new X9ECParameters(fpCurve, new X9ECPoint(fpCurve, Hex.Decode("036b17d1f2e12c4247f8bce6e563a440f277037d812deb33a0f4a13945d898c296")), bigInteger, one, Hex.Decode("c49d360886e704936a6678e1139d26b7819f7e90"));
			}

			// Token: 0x040035E7 RID: 13799
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime256v1Holder();
		}

		// Token: 0x0200096E RID: 2414
		internal class C2pnb163v1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F2A RID: 20266 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2pnb163v1Holder()
			{
			}

			// Token: 0x06004F2B RID: 20267 RVA: 0x001B7E3C File Offset: 0x001B603C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("0400000000000000000001E60FC8821CC74DAEAFC1", 16);
				BigInteger two = BigInteger.Two;
				F2mCurve f2mCurve = new F2mCurve(163, 1, 2, 8, new BigInteger("072546B5435234A422E0789675F432C89435DE5242", 16), new BigInteger("00C9517D06D5240D3CFF38C74B20B6CD4D6F9DD4D9", 16), bigInteger, two);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("0307AF69989546103D79329FCC3D74880F33BBE803CB")), bigInteger, two, Hex.Decode("D2C0FB15760860DEF1EEF4D696E6768756151754"));
			}

			// Token: 0x040035E8 RID: 13800
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb163v1Holder();
		}

		// Token: 0x0200096F RID: 2415
		internal class C2pnb163v2Holder : X9ECParametersHolder
		{
			// Token: 0x06004F2D RID: 20269 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2pnb163v2Holder()
			{
			}

			// Token: 0x06004F2E RID: 20270 RVA: 0x001B7EB0 File Offset: 0x001B60B0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("03FFFFFFFFFFFFFFFFFFFDF64DE1151ADBB78F10A7", 16);
				BigInteger two = BigInteger.Two;
				F2mCurve f2mCurve = new F2mCurve(163, 1, 2, 8, new BigInteger("0108B39E77C4B108BED981ED0E890E117C511CF072", 16), new BigInteger("0667ACEB38AF4E488C407433FFAE4F1C811638DF20", 16), bigInteger, two);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("030024266E4EB5106D0A964D92C4860E2671DB9B6CC5")), bigInteger, two, null);
			}

			// Token: 0x040035E9 RID: 13801
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb163v2Holder();
		}

		// Token: 0x02000970 RID: 2416
		internal class C2pnb163v3Holder : X9ECParametersHolder
		{
			// Token: 0x06004F30 RID: 20272 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2pnb163v3Holder()
			{
			}

			// Token: 0x06004F31 RID: 20273 RVA: 0x001B7F1C File Offset: 0x001B611C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("03FFFFFFFFFFFFFFFFFFFE1AEE140F110AFF961309", 16);
				BigInteger two = BigInteger.Two;
				F2mCurve f2mCurve = new F2mCurve(163, 1, 2, 8, new BigInteger("07A526C63D3E25A256A007699F5447E32AE456B50E", 16), new BigInteger("03F7061798EB99E238FD6F1BF95B48FEEB4854252B", 16), bigInteger, two);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("0202F9F87B7C574D0BDECF8A22E6524775F98CDEBDCB")), bigInteger, two, null);
			}

			// Token: 0x040035EA RID: 13802
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb163v3Holder();
		}

		// Token: 0x02000971 RID: 2417
		internal class C2pnb176w1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F33 RID: 20275 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2pnb176w1Holder()
			{
			}

			// Token: 0x06004F34 RID: 20276 RVA: 0x001B7F88 File Offset: 0x001B6188
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("010092537397ECA4F6145799D62B0A19CE06FE26AD", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(65390L);
				F2mCurve f2mCurve = new F2mCurve(176, 1, 2, 43, new BigInteger("00E4E6DB2995065C407D9D39B8D0967B96704BA8E9C90B", 16), new BigInteger("005DDA470ABE6414DE8EC133AE28E9BBD7FCEC0AE0FFF2", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("038D16C2866798B600F9F08BB4A8E860F3298CE04A5798")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035EB RID: 13803
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb176w1Holder();
		}

		// Token: 0x02000972 RID: 2418
		internal class C2tnb191v1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F36 RID: 20278 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2tnb191v1Holder()
			{
			}

			// Token: 0x06004F37 RID: 20279 RVA: 0x001B7FFC File Offset: 0x001B61FC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("40000000000000000000000004A20E90C39067C893BBB9A5", 16);
				BigInteger two = BigInteger.Two;
				F2mCurve f2mCurve = new F2mCurve(191, 9, new BigInteger("2866537B676752636A68F56554E12640276B649EF7526267", 16), new BigInteger("2E45EF571F00786F67B0081B9495A3D95462F5DE0AA185EC", 16), bigInteger, two);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("0236B3DAF8A23206F9C4F299D7B21A9C369137F2C84AE1AA0D")), bigInteger, two, Hex.Decode("4E13CA542744D696E67687561517552F279A8C84"));
			}

			// Token: 0x040035EC RID: 13804
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb191v1Holder();
		}

		// Token: 0x02000973 RID: 2419
		internal class C2tnb191v2Holder : X9ECParametersHolder
		{
			// Token: 0x06004F39 RID: 20281 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2tnb191v2Holder()
			{
			}

			// Token: 0x06004F3A RID: 20282 RVA: 0x001B8070 File Offset: 0x001B6270
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("20000000000000000000000050508CB89F652824E06B8173", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				F2mCurve f2mCurve = new F2mCurve(191, 9, new BigInteger("401028774D7777C7B7666D1366EA432071274F89FF01E718", 16), new BigInteger("0620048D28BCBD03B6249C99182B7C8CD19700C362C46A01", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("023809B2B7CC1B28CC5A87926AAD83FD28789E81E2C9E3BF10")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035ED RID: 13805
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb191v2Holder();
		}

		// Token: 0x02000974 RID: 2420
		internal class C2tnb191v3Holder : X9ECParametersHolder
		{
			// Token: 0x06004F3C RID: 20284 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2tnb191v3Holder()
			{
			}

			// Token: 0x06004F3D RID: 20285 RVA: 0x001B80DC File Offset: 0x001B62DC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("155555555555555555555555610C0B196812BFB6288A3EA3", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(6L);
				F2mCurve f2mCurve = new F2mCurve(191, 9, new BigInteger("6C01074756099122221056911C77D77E77A777E7E7E77FCB", 16), new BigInteger("71FE1AF926CF847989EFEF8DB459F66394D90F32AD3F15E8", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("03375D4CE24FDE434489DE8746E71786015009E66E38A926DD")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035EE RID: 13806
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb191v3Holder();
		}

		// Token: 0x02000975 RID: 2421
		internal class C2pnb208w1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F3F RID: 20287 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2pnb208w1Holder()
			{
			}

			// Token: 0x06004F40 RID: 20288 RVA: 0x001B8148 File Offset: 0x001B6348
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("0101BAF95C9723C57B6C21DA2EFF2D5ED588BDD5717E212F9D", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(65096L);
				F2mCurve f2mCurve = new F2mCurve(208, 1, 2, 83, new BigInteger("0", 16), new BigInteger("00C8619ED45A62E6212E1160349E2BFA844439FAFC2A3FD1638F9E", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("0289FDFBE4ABE193DF9559ECF07AC0CE78554E2784EB8C1ED1A57A")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035EF RID: 13807
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb208w1Holder();
		}

		// Token: 0x02000976 RID: 2422
		internal class C2tnb239v1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F42 RID: 20290 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2tnb239v1Holder()
			{
			}

			// Token: 0x06004F43 RID: 20291 RVA: 0x001B81BC File Offset: 0x001B63BC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("2000000000000000000000000000000F4D42FFE1492A4993F1CAD666E447", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				F2mCurve f2mCurve = new F2mCurve(239, 36, new BigInteger("32010857077C5431123A46B808906756F543423E8D27877578125778AC76", 16), new BigInteger("790408F2EEDAF392B012EDEFB3392F30F4327C0CA3F31FC383C422AA8C16", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("0257927098FA932E7C0A96D3FD5B706EF7E5F5C156E16B7E7C86038552E91D")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035F0 RID: 13808
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb239v1Holder();
		}

		// Token: 0x02000977 RID: 2423
		internal class C2tnb239v2Holder : X9ECParametersHolder
		{
			// Token: 0x06004F45 RID: 20293 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2tnb239v2Holder()
			{
			}

			// Token: 0x06004F46 RID: 20294 RVA: 0x001B8228 File Offset: 0x001B6428
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("1555555555555555555555555555553C6F2885259C31E3FCDF154624522D", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(6L);
				F2mCurve f2mCurve = new F2mCurve(239, 36, new BigInteger("4230017757A767FAE42398569B746325D45313AF0766266479B75654E65F", 16), new BigInteger("5037EA654196CFF0CD82B2C14A2FCF2E3FF8775285B545722F03EACDB74B", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("0228F9D04E900069C8DC47A08534FE76D2B900B7D7EF31F5709F200C4CA205")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035F1 RID: 13809
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb239v2Holder();
		}

		// Token: 0x02000978 RID: 2424
		internal class C2tnb239v3Holder : X9ECParametersHolder
		{
			// Token: 0x06004F48 RID: 20296 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2tnb239v3Holder()
			{
			}

			// Token: 0x06004F49 RID: 20297 RVA: 0x001B8294 File Offset: 0x001B6494
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("0CCCCCCCCCCCCCCCCCCCCCCCCCCCCCAC4912D2D9DF903EF9888B8A0E4CFF", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(10L);
				F2mCurve f2mCurve = new F2mCurve(239, 36, new BigInteger("01238774666A67766D6676F778E676B66999176666E687666D8766C66A9F", 16), new BigInteger("6A941977BA9F6A435199ACFC51067ED587F519C5ECB541B8E44111DE1D40", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("0370F6E9D04D289C4E89913CE3530BFDE903977D42B146D539BF1BDE4E9C92")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035F2 RID: 13810
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb239v3Holder();
		}

		// Token: 0x02000979 RID: 2425
		internal class C2pnb272w1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F4B RID: 20299 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2pnb272w1Holder()
			{
			}

			// Token: 0x06004F4C RID: 20300 RVA: 0x001B8304 File Offset: 0x001B6504
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("0100FAF51354E0E39E4892DF6E319C72C8161603FA45AA7B998A167B8F1E629521", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(65286L);
				F2mCurve f2mCurve = new F2mCurve(272, 1, 3, 56, new BigInteger("0091A091F03B5FBA4AB2CCF49C4EDD220FB028712D42BE752B2C40094DBACDB586FB20", 16), new BigInteger("7167EFC92BB2E3CE7C8AAAFF34E12A9C557003D7C73A6FAF003F99F6CC8482E540F7", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("026108BABB2CEEBCF787058A056CBE0CFE622D7723A289E08A07AE13EF0D10D171DD8D")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035F3 RID: 13811
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb272w1Holder();
		}

		// Token: 0x0200097A RID: 2426
		internal class C2pnb304w1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F4E RID: 20302 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2pnb304w1Holder()
			{
			}

			// Token: 0x06004F4F RID: 20303 RVA: 0x001B8378 File Offset: 0x001B6578
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("0101D556572AABAC800101D556572AABAC8001022D5C91DD173F8FB561DA6899164443051D", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(65070L);
				F2mCurve f2mCurve = new F2mCurve(304, 1, 2, 11, new BigInteger("00FD0D693149A118F651E6DCE6802085377E5F882D1B510B44160074C1288078365A0396C8E681", 16), new BigInteger("00BDDB97E555A50A908E43B01C798EA5DAA6788F1EA2794EFCF57166B8C14039601E55827340BE", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("02197B07845E9BE2D96ADB0F5F3C7F2CFFBD7A3EB8B6FEC35C7FD67F26DDF6285A644F740A2614")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035F4 RID: 13812
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb304w1Holder();
		}

		// Token: 0x0200097B RID: 2427
		internal class C2tnb359v1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F51 RID: 20305 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2tnb359v1Holder()
			{
			}

			// Token: 0x06004F52 RID: 20306 RVA: 0x001B83EC File Offset: 0x001B65EC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("01AF286BCA1AF286BCA1AF286BCA1AF286BCA1AF286BC9FB8F6B85C556892C20A7EB964FE7719E74F490758D3B", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(76L);
				F2mCurve f2mCurve = new F2mCurve(359, 68, new BigInteger("5667676A654B20754F356EA92017D946567C46675556F19556A04616B567D223A5E05656FB549016A96656A557", 16), new BigInteger("2472E2D0197C49363F1FE7F5B6DB075D52B6947D135D8CA445805D39BC345626089687742B6329E70680231988", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("033C258EF3047767E7EDE0F1FDAA79DAEE3841366A132E163ACED4ED2401DF9C6BDCDE98E8E707C07A2239B1B097")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035F5 RID: 13813
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb359v1Holder();
		}

		// Token: 0x0200097C RID: 2428
		internal class C2pnb368w1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F54 RID: 20308 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2pnb368w1Holder()
			{
			}

			// Token: 0x06004F55 RID: 20309 RVA: 0x001B845C File Offset: 0x001B665C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("010090512DA9AF72B08349D98A5DD4C7B0532ECA51CE03E2D10F3B7AC579BD87E909AE40A6F131E9CFCE5BD967", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(65392L);
				F2mCurve f2mCurve = new F2mCurve(368, 1, 2, 85, new BigInteger("00E0D2EE25095206F5E2A4F9ED229F1F256E79A0E2B455970D8D0D865BD94778C576D62F0AB7519CCD2A1A906AE30D", 16), new BigInteger("00FC1217D4320A90452C760A58EDCD30C8DD069B3C34453837A34ED50CB54917E1C2112D84D164F444F8F74786046A", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("021085E2755381DCCCE3C1557AFA10C2F0C0C2825646C5B34A394CBCFA8BC16B22E7E789E927BE216F02E1FB136A5F")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035F6 RID: 13814
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb368w1Holder();
		}

		// Token: 0x0200097D RID: 2429
		internal class C2tnb431r1Holder : X9ECParametersHolder
		{
			// Token: 0x06004F57 RID: 20311 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private C2tnb431r1Holder()
			{
			}

			// Token: 0x06004F58 RID: 20312 RVA: 0x001B84D0 File Offset: 0x001B66D0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = new BigInteger("0340340340340340340340340340340340340340340340340340340323C313FAB50589703B5EC68D3587FEC60D161CC149C1AD4A91", 16);
				BigInteger bigInteger2 = BigInteger.ValueOf(10080L);
				F2mCurve f2mCurve = new F2mCurve(431, 120, new BigInteger("1A827EF00DD6FC0E234CAF046C6A5D8A85395B236CC4AD2CF32A0CADBDC9DDF620B0EB9906D0957F6C6FEACD615468DF104DE296CD8F", 16), new BigInteger("10D9B4A3D9047D8B154359ABFB1B7F5485B04CEB868237DDC9DEDA982A679A5A919B626D4E50A8DD731B107A9962381FB5D807BF2618", 16), bigInteger, bigInteger2);
				return new X9ECParameters(f2mCurve, new X9ECPoint(f2mCurve, Hex.Decode("02120FC05D3C67A99DE161D2F4092622FECA701BE4F50F4758714E8A87BBF2A658EF8C21E7C5EFE965361F6C2999C0C247B0DBD70CE6B7")), bigInteger, bigInteger2, null);
			}

			// Token: 0x040035F7 RID: 13815
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb431r1Holder();
		}
	}
}
