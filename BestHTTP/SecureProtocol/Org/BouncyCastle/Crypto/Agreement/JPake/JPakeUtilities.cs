using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x020005C7 RID: 1479
	public abstract class JPakeUtilities
	{
		// Token: 0x060038E7 RID: 14567 RVA: 0x00168208 File Offset: 0x00166408
		public static BigInteger GenerateX1(BigInteger q, SecureRandom random)
		{
			BigInteger zero = JPakeUtilities.Zero;
			BigInteger max = q.Subtract(JPakeUtilities.One);
			return BigIntegers.CreateRandomInRange(zero, max, random);
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x00168230 File Offset: 0x00166430
		public static BigInteger GenerateX2(BigInteger q, SecureRandom random)
		{
			BigInteger one = JPakeUtilities.One;
			BigInteger max = q.Subtract(JPakeUtilities.One);
			return BigIntegers.CreateRandomInRange(one, max, random);
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x00168255 File Offset: 0x00166455
		public static BigInteger CalculateS(char[] password)
		{
			return new BigInteger(Encoding.UTF8.GetBytes(password));
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x0013530B File Offset: 0x0013350B
		public static BigInteger CalculateGx(BigInteger p, BigInteger g, BigInteger x)
		{
			return g.ModPow(x, p);
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x00168267 File Offset: 0x00166467
		public static BigInteger CalculateGA(BigInteger p, BigInteger gx1, BigInteger gx3, BigInteger gx4)
		{
			return gx1.Multiply(gx3).Multiply(gx4).Mod(p);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x0016827C File Offset: 0x0016647C
		public static BigInteger CalculateX2s(BigInteger q, BigInteger x2, BigInteger s)
		{
			return x2.Multiply(s).Mod(q);
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x0016828B File Offset: 0x0016648B
		public static BigInteger CalculateA(BigInteger p, BigInteger q, BigInteger gA, BigInteger x2s)
		{
			return gA.ModPow(x2s, p);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x00168298 File Offset: 0x00166498
		public static BigInteger[] CalculateZeroKnowledgeProof(BigInteger p, BigInteger q, BigInteger g, BigInteger gx, BigInteger x, string participantId, IDigest digest, SecureRandom random)
		{
			BigInteger zero = JPakeUtilities.Zero;
			BigInteger max = q.Subtract(JPakeUtilities.One);
			BigInteger bigInteger = BigIntegers.CreateRandomInRange(zero, max, random);
			BigInteger bigInteger2 = g.ModPow(bigInteger, p);
			BigInteger val = JPakeUtilities.CalculateHashForZeroKnowledgeProof(g, bigInteger2, gx, participantId, digest);
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger.Subtract(x.Multiply(val)).Mod(q)
			};
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x001682F6 File Offset: 0x001664F6
		private static BigInteger CalculateHashForZeroKnowledgeProof(BigInteger g, BigInteger gr, BigInteger gx, string participantId, IDigest digest)
		{
			digest.Reset();
			JPakeUtilities.UpdateDigestIncludingSize(digest, g);
			JPakeUtilities.UpdateDigestIncludingSize(digest, gr);
			JPakeUtilities.UpdateDigestIncludingSize(digest, gx);
			JPakeUtilities.UpdateDigestIncludingSize(digest, participantId);
			return new BigInteger(DigestUtilities.DoFinal(digest));
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x0016832B File Offset: 0x0016652B
		public static void ValidateGx4(BigInteger gx4)
		{
			if (gx4.Equals(JPakeUtilities.One))
			{
				throw new CryptoException("g^x validation failed.  g^x should not be 1.");
			}
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x00168345 File Offset: 0x00166545
		public static void ValidateGa(BigInteger ga)
		{
			if (ga.Equals(JPakeUtilities.One))
			{
				throw new CryptoException("ga is equal to 1.  It should not be.  The chances of this happening are on the order of 2^160 for a 160-bit q.  Try again.");
			}
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x00168360 File Offset: 0x00166560
		public static void ValidateZeroKnowledgeProof(BigInteger p, BigInteger q, BigInteger g, BigInteger gx, BigInteger[] zeroKnowledgeProof, string participantId, IDigest digest)
		{
			BigInteger bigInteger = zeroKnowledgeProof[0];
			BigInteger e = zeroKnowledgeProof[1];
			BigInteger e2 = JPakeUtilities.CalculateHashForZeroKnowledgeProof(g, bigInteger, gx, participantId, digest);
			if (gx.CompareTo(JPakeUtilities.Zero) != 1 || gx.CompareTo(p) != -1 || gx.ModPow(q, p).CompareTo(JPakeUtilities.One) != 0 || g.ModPow(e, p).Multiply(gx.ModPow(e2, p)).Mod(p).CompareTo(bigInteger) != 0)
			{
				throw new CryptoException("Zero-knowledge proof validation failed");
			}
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x001683DE File Offset: 0x001665DE
		public static BigInteger CalculateKeyingMaterial(BigInteger p, BigInteger q, BigInteger gx4, BigInteger x2, BigInteger s, BigInteger B)
		{
			return gx4.ModPow(x2.Multiply(s).Negate().Mod(q), p).Multiply(B).ModPow(x2, p);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x00168408 File Offset: 0x00166608
		public static void ValidateParticipantIdsDiffer(string participantId1, string participantId2)
		{
			if (participantId1.Equals(participantId2))
			{
				throw new CryptoException("Both participants are using the same participantId (" + participantId1 + "). This is not allowed. Each participant must use a unique participantId.");
			}
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x00168429 File Offset: 0x00166629
		public static void ValidateParticipantIdsEqual(string expectedParticipantId, string actualParticipantId)
		{
			if (!expectedParticipantId.Equals(actualParticipantId))
			{
				throw new CryptoException(string.Concat(new string[]
				{
					"Received payload from incorrect partner (",
					actualParticipantId,
					"). Expected to receive payload from ",
					expectedParticipantId,
					"."
				}));
			}
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x00168465 File Offset: 0x00166665
		public static void ValidateNotNull(object obj, string description)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(description);
			}
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x00168474 File Offset: 0x00166674
		public static BigInteger CalculateMacTag(string participantId, string partnerParticipantId, BigInteger gx1, BigInteger gx2, BigInteger gx3, BigInteger gx4, BigInteger keyingMaterial, IDigest digest)
		{
			byte[] array = JPakeUtilities.CalculateMacKey(keyingMaterial, digest);
			HMac hmac = new HMac(digest);
			hmac.Init(new KeyParameter(array));
			Arrays.Fill(array, 0);
			JPakeUtilities.UpdateMac(hmac, "KC_1_U");
			JPakeUtilities.UpdateMac(hmac, participantId);
			JPakeUtilities.UpdateMac(hmac, partnerParticipantId);
			JPakeUtilities.UpdateMac(hmac, gx1);
			JPakeUtilities.UpdateMac(hmac, gx2);
			JPakeUtilities.UpdateMac(hmac, gx3);
			JPakeUtilities.UpdateMac(hmac, gx4);
			return new BigInteger(MacUtilities.DoFinal(hmac));
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x001684E6 File Offset: 0x001666E6
		private static byte[] CalculateMacKey(BigInteger keyingMaterial, IDigest digest)
		{
			digest.Reset();
			JPakeUtilities.UpdateDigest(digest, keyingMaterial);
			JPakeUtilities.UpdateDigest(digest, "JPAKE_KC");
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x00168506 File Offset: 0x00166706
		public static void ValidateMacTag(string participantId, string partnerParticipantId, BigInteger gx1, BigInteger gx2, BigInteger gx3, BigInteger gx4, BigInteger keyingMaterial, IDigest digest, BigInteger partnerMacTag)
		{
			if (!JPakeUtilities.CalculateMacTag(partnerParticipantId, participantId, gx3, gx4, gx1, gx2, keyingMaterial, digest).Equals(partnerMacTag))
			{
				throw new CryptoException("Partner MacTag validation failed. Therefore, the password, MAC, or digest algorithm of each participant does not match.");
			}
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x0016852D File Offset: 0x0016672D
		private static void UpdateDigest(IDigest digest, BigInteger bigInteger)
		{
			JPakeUtilities.UpdateDigest(digest, BigIntegers.AsUnsignedByteArray(bigInteger));
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x0016853B File Offset: 0x0016673B
		private static void UpdateDigest(IDigest digest, string str)
		{
			JPakeUtilities.UpdateDigest(digest, Encoding.UTF8.GetBytes(str));
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x0016854E File Offset: 0x0016674E
		private static void UpdateDigest(IDigest digest, byte[] bytes)
		{
			digest.BlockUpdate(bytes, 0, bytes.Length);
			Arrays.Fill(bytes, 0);
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x00168562 File Offset: 0x00166762
		private static void UpdateDigestIncludingSize(IDigest digest, BigInteger bigInteger)
		{
			JPakeUtilities.UpdateDigestIncludingSize(digest, BigIntegers.AsUnsignedByteArray(bigInteger));
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x00168570 File Offset: 0x00166770
		private static void UpdateDigestIncludingSize(IDigest digest, string str)
		{
			JPakeUtilities.UpdateDigestIncludingSize(digest, Encoding.UTF8.GetBytes(str));
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x00168583 File Offset: 0x00166783
		private static void UpdateDigestIncludingSize(IDigest digest, byte[] bytes)
		{
			digest.BlockUpdate(JPakeUtilities.IntToByteArray(bytes.Length), 0, 4);
			digest.BlockUpdate(bytes, 0, bytes.Length);
			Arrays.Fill(bytes, 0);
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x001685A7 File Offset: 0x001667A7
		private static void UpdateMac(IMac mac, BigInteger bigInteger)
		{
			JPakeUtilities.UpdateMac(mac, BigIntegers.AsUnsignedByteArray(bigInteger));
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x001685B5 File Offset: 0x001667B5
		private static void UpdateMac(IMac mac, string str)
		{
			JPakeUtilities.UpdateMac(mac, Encoding.UTF8.GetBytes(str));
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x001685C8 File Offset: 0x001667C8
		private static void UpdateMac(IMac mac, byte[] bytes)
		{
			mac.BlockUpdate(bytes, 0, bytes.Length);
			Arrays.Fill(bytes, 0);
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x001685DC File Offset: 0x001667DC
		private static byte[] IntToByteArray(int value)
		{
			return Pack.UInt32_To_BE((uint)value);
		}

		// Token: 0x04002474 RID: 9332
		public static readonly BigInteger Zero = BigInteger.Zero;

		// Token: 0x04002475 RID: 9333
		public static readonly BigInteger One = BigInteger.One;
	}
}
