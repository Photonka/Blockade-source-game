using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.GM
{
	// Token: 0x02000721 RID: 1825
	public sealed class GMNamedCurves
	{
		// Token: 0x06004271 RID: 17009 RVA: 0x00023EF4 File Offset: 0x000220F4
		private GMNamedCurves()
		{
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x00096BA2 File Offset: 0x00094DA2
		private static ECCurve ConfigureCurve(ECCurve curve)
		{
			return curve;
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x0011807A File Offset: 0x0011627A
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.Decode(hex));
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x00189831 File Offset: 0x00187A31
		private static void DefineCurve(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			GMNamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			GMNamedCurves.names.Add(oid, name);
			GMNamedCurves.curves.Add(oid, holder);
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x0018985C File Offset: 0x00187A5C
		static GMNamedCurves()
		{
			GMNamedCurves.DefineCurve("wapip192v1", GMObjectIdentifiers.wapip192v1, GMNamedCurves.WapiP192V1Holder.Instance);
			GMNamedCurves.DefineCurve("sm2p256v1", GMObjectIdentifiers.sm2p256v1, GMNamedCurves.SM2P256V1Holder.Instance);
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x001898B0 File Offset: 0x00187AB0
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = GMNamedCurves.GetOid(name);
			if (oid != null)
			{
				return GMNamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x001898D0 File Offset: 0x00187AD0
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)GMNamedCurves.curves[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x001898F9 File Offset: 0x00187AF9
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)GMNamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x00189910 File Offset: 0x00187B10
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)GMNamedCurves.names[oid];
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x00189922 File Offset: 0x00187B22
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(GMNamedCurves.names.Values);
			}
		}

		// Token: 0x04002A8A RID: 10890
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x04002A8B RID: 10891
		private static readonly IDictionary curves = Platform.CreateHashtable();

		// Token: 0x04002A8C RID: 10892
		private static readonly IDictionary names = Platform.CreateHashtable();

		// Token: 0x020009B1 RID: 2481
		internal class SM2P256V1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FE9 RID: 20457 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private SM2P256V1Holder()
			{
			}

			// Token: 0x06004FEA RID: 20458 RVA: 0x001B9E20 File Offset: 0x001B8020
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = GMNamedCurves.FromHex("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFF");
				BigInteger a = GMNamedCurves.FromHex("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFC");
				BigInteger b = GMNamedCurves.FromHex("28E9FA9E9D9F5E344D5A9E4BCF6509A7F39789F515AB8F92DDBCBD414D940E93");
				byte[] seed = null;
				BigInteger bigInteger = GMNamedCurves.FromHex("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFF7203DF6B21C6052B53BBF40939D54123");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = GMNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("0432C4AE2C1F1981195F9904466A39C9948FE30BBFF2660BE1715A4589334C74C7BC3736A2F4F6779C59BDCEE36B692153D0A9877CC62A474002DF32E52139F0A0"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x04003669 RID: 13929
			internal static readonly X9ECParametersHolder Instance = new GMNamedCurves.SM2P256V1Holder();
		}

		// Token: 0x020009B2 RID: 2482
		internal class WapiP192V1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FEC RID: 20460 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private WapiP192V1Holder()
			{
			}

			// Token: 0x06004FED RID: 20461 RVA: 0x001B9E9C File Offset: 0x001B809C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = GMNamedCurves.FromHex("BDB6F4FE3E8B1D9E0DA8C0D46F4C318CEFE4AFE3B6B8551F");
				BigInteger a = GMNamedCurves.FromHex("BB8E5E8FBC115E139FE6A814FE48AAA6F0ADA1AA5DF91985");
				BigInteger b = GMNamedCurves.FromHex("1854BEBDC31B21B7AEFC80AB0ECD10D5B1B3308E6DBF11C1");
				byte[] seed = null;
				BigInteger bigInteger = GMNamedCurves.FromHex("BDB6F4FE3E8B1D9E0DA8C0D40FC962195DFAE76F56564677");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = GMNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("044AD5F7048DE709AD51236DE65E4D4B482C836DC6E410664002BB3A02D4AAADACAE24817A4CA3A1B014B5270432DB27D2"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x0400366A RID: 13930
			internal static readonly X9ECParametersHolder Instance = new GMNamedCurves.WapiP192V1Holder();
		}
	}
}
