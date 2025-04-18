﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc7748;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032
{
	// Token: 0x0200031D RID: 797
	public abstract class Ed25519
	{
		// Token: 0x06001F0C RID: 7948 RVA: 0x000E9B08 File Offset: 0x000E7D08
		private static byte[] CalculateS(byte[] r, byte[] k, byte[] s)
		{
			uint[] array = new uint[16];
			Ed25519.DecodeScalar(r, 0, array);
			uint[] array2 = new uint[8];
			Ed25519.DecodeScalar(k, 0, array2);
			uint[] array3 = new uint[8];
			Ed25519.DecodeScalar(s, 0, array3);
			Nat256.MulAddTo(array2, array3, array);
			byte[] array4 = new byte[64];
			for (int i = 0; i < array.Length; i++)
			{
				Ed25519.Encode32(array[i], array4, i * 4);
			}
			return Ed25519.ReduceScalar(array4);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000E9B7A File Offset: 0x000E7D7A
		private static bool CheckContextVar(byte[] ctx, byte phflag)
		{
			return (ctx == null && phflag == 0) || (ctx != null && ctx.Length < 256);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000E9B94 File Offset: 0x000E7D94
		private static bool CheckPointVar(byte[] p)
		{
			uint[] array = new uint[8];
			Ed25519.Decode32(p, 0, array, 0, 8);
			array[7] &= 2147483647U;
			return !Nat256.Gte(array, Ed25519.P);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000E9BD0 File Offset: 0x000E7DD0
		private static bool CheckScalarVar(byte[] s)
		{
			uint[] array = new uint[8];
			Ed25519.DecodeScalar(s, 0, array);
			return !Nat256.Gte(array, Ed25519.L);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000E9BFA File Offset: 0x000E7DFA
		private static IDigest CreateDigest()
		{
			return new Sha512Digest();
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000E9C01 File Offset: 0x000E7E01
		public static IDigest CreatePrehash()
		{
			return Ed25519.CreateDigest();
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000E9C08 File Offset: 0x000E7E08
		private static uint Decode24(byte[] bs, int off)
		{
			return (uint)((int)bs[off] | (int)bs[++off] << 8 | (int)bs[++off] << 16);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000E9C24 File Offset: 0x000E7E24
		private static uint Decode32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] | (int)bs[++off] << 8 | (int)bs[++off] << 16 | (int)bs[++off] << 24);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000E9C4C File Offset: 0x000E7E4C
		private static void Decode32(byte[] bs, int bsOff, uint[] n, int nOff, int nLen)
		{
			for (int i = 0; i < nLen; i++)
			{
				n[nOff + i] = Ed25519.Decode32(bs, bsOff + i * 4);
			}
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000E9C78 File Offset: 0x000E7E78
		private static bool DecodePointVar(byte[] p, int pOff, bool negate, Ed25519.PointExt r)
		{
			byte[] array = Arrays.CopyOfRange(p, pOff, pOff + 32);
			if (!Ed25519.CheckPointVar(array))
			{
				return false;
			}
			int num = (array[31] & 128) >> 7;
			byte[] array2 = array;
			int num2 = 31;
			array2[num2] &= 127;
			X25519Field.Decode(array, 0, r.y);
			int[] array3 = X25519Field.Create();
			int[] array4 = X25519Field.Create();
			X25519Field.Sqr(r.y, array3);
			X25519Field.Mul(Ed25519.C_d, array3, array4);
			X25519Field.SubOne(array3);
			X25519Field.AddOne(array4);
			if (!X25519Field.SqrtRatioVar(array3, array4, r.x))
			{
				return false;
			}
			X25519Field.Normalize(r.x);
			if (num == 1 && X25519Field.IsZeroVar(r.x))
			{
				return false;
			}
			if (negate ^ num != (r.x[0] & 1))
			{
				X25519Field.Negate(r.x, r.x);
			}
			Ed25519.PointExtendXY(r);
			return true;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000E9D4F File Offset: 0x000E7F4F
		private static void DecodeScalar(byte[] k, int kOff, uint[] n)
		{
			Ed25519.Decode32(k, kOff, n, 0, 8);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000E9D5B File Offset: 0x000E7F5B
		private static void Dom2(IDigest d, byte phflag, byte[] ctx)
		{
			if (ctx != null)
			{
				d.BlockUpdate(Ed25519.Dom2Prefix, 0, Ed25519.Dom2Prefix.Length);
				d.Update(phflag);
				d.Update((byte)ctx.Length);
				d.BlockUpdate(ctx, 0, ctx.Length);
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000E9D8F File Offset: 0x000E7F8F
		private static void Encode24(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000E9DAF File Offset: 0x000E7FAF
		private static void Encode32(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000E9DDC File Offset: 0x000E7FDC
		private static void Encode56(ulong n, byte[] bs, int off)
		{
			Ed25519.Encode32((uint)n, bs, off);
			Ed25519.Encode24((uint)(n >> 32), bs, off + 4);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000E9DF8 File Offset: 0x000E7FF8
		private static void EncodePoint(Ed25519.PointAccum p, byte[] r, int rOff)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			X25519Field.Inv(p.z, array2);
			X25519Field.Mul(p.x, array2, array);
			X25519Field.Mul(p.y, array2, array2);
			X25519Field.Normalize(array);
			X25519Field.Normalize(array2);
			X25519Field.Encode(array2, r, rOff);
			int num = rOff + 32 - 1;
			r[num] |= (byte)((array[0] & 1) << 7);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000E9E64 File Offset: 0x000E8064
		public static void GeneratePrivateKey(SecureRandom random, byte[] k)
		{
			random.NextBytes(k);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000E9E70 File Offset: 0x000E8070
		public static void GeneratePublicKey(byte[] sk, int skOff, byte[] pk, int pkOff)
		{
			IDigest digest = Ed25519.CreateDigest();
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(sk, skOff, Ed25519.SecretKeySize);
			digest.DoFinal(array, 0);
			byte[] array2 = new byte[32];
			Ed25519.PruneScalar(array, 0, array2);
			Ed25519.ScalarMultBaseEncoded(array2, pk, pkOff);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000E9EBC File Offset: 0x000E80BC
		private static sbyte[] GetWnaf(uint[] n, int width)
		{
			uint[] array = new uint[16];
			uint num = 0U;
			int num2 = array.Length;
			int num3 = 8;
			while (--num3 >= 0)
			{
				uint num4 = n[num3];
				array[--num2] = (num4 >> 16 | num << 16);
				num = (array[--num2] = num4);
			}
			sbyte[] array2 = new sbyte[256];
			int num5 = 1 << width;
			uint num6 = (uint)(num5 - 1);
			uint num7 = (uint)num5 >> 1;
			uint num8 = 0U;
			int i = 0;
			int j = 0;
			while (j < array.Length)
			{
				uint num9 = array[j];
				while (i < 16)
				{
					uint num10 = num9 >> i;
					if ((num10 & 1U) == num8)
					{
						i++;
					}
					else
					{
						uint num11 = (num10 & num6) + num8;
						num8 = (num11 & num7);
						num11 -= num8 << 1;
						num8 >>= width - 1;
						array2[(j << 4) + i] = (sbyte)num11;
						i += width;
					}
				}
				j++;
				i -= 16;
			}
			return array2;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000E9FA8 File Offset: 0x000E81A8
		private static void ImplSign(IDigest d, byte[] h, byte[] s, byte[] pk, int pkOff, byte[] ctx, byte phflag, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			Ed25519.Dom2(d, phflag, ctx);
			d.BlockUpdate(h, 32, 32);
			d.BlockUpdate(m, mOff, mLen);
			d.DoFinal(h, 0);
			byte[] array = Ed25519.ReduceScalar(h);
			byte[] array2 = new byte[32];
			Ed25519.ScalarMultBaseEncoded(array, array2, 0);
			Ed25519.Dom2(d, phflag, ctx);
			d.BlockUpdate(array2, 0, 32);
			d.BlockUpdate(pk, pkOff, 32);
			d.BlockUpdate(m, mOff, mLen);
			d.DoFinal(h, 0);
			byte[] k = Ed25519.ReduceScalar(h);
			Array sourceArray = Ed25519.CalculateS(array, k, s);
			Array.Copy(array2, 0, sig, sigOff, 32);
			Array.Copy(sourceArray, 0, sig, sigOff + 32, 32);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000EA054 File Offset: 0x000E8254
		private static void ImplSign(byte[] sk, int skOff, byte[] ctx, byte phflag, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			if (!Ed25519.CheckContextVar(ctx, phflag))
			{
				throw new ArgumentException("ctx");
			}
			IDigest digest = Ed25519.CreateDigest();
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(sk, skOff, Ed25519.SecretKeySize);
			digest.DoFinal(array, 0);
			byte[] array2 = new byte[32];
			Ed25519.PruneScalar(array, 0, array2);
			byte[] array3 = new byte[32];
			Ed25519.ScalarMultBaseEncoded(array2, array3, 0);
			Ed25519.ImplSign(digest, array, array2, array3, 0, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000EA0D4 File Offset: 0x000E82D4
		private static void ImplSign(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] ctx, byte phflag, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			if (!Ed25519.CheckContextVar(ctx, phflag))
			{
				throw new ArgumentException("ctx");
			}
			IDigest digest = Ed25519.CreateDigest();
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(sk, skOff, Ed25519.SecretKeySize);
			digest.DoFinal(array, 0);
			byte[] array2 = new byte[32];
			Ed25519.PruneScalar(array, 0, array2);
			Ed25519.ImplSign(digest, array, array2, pk, pkOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000EA148 File Offset: 0x000E8348
		private static bool ImplVerify(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] ctx, byte phflag, byte[] m, int mOff, int mLen)
		{
			if (!Ed25519.CheckContextVar(ctx, phflag))
			{
				throw new ArgumentException("ctx");
			}
			byte[] array = Arrays.CopyOfRange(sig, sigOff, sigOff + 32);
			byte[] array2 = Arrays.CopyOfRange(sig, sigOff + 32, sigOff + Ed25519.SignatureSize);
			if (!Ed25519.CheckPointVar(array))
			{
				return false;
			}
			if (!Ed25519.CheckScalarVar(array2))
			{
				return false;
			}
			Ed25519.PointExt pointExt = new Ed25519.PointExt();
			if (!Ed25519.DecodePointVar(pk, pkOff, true, pointExt))
			{
				return false;
			}
			IDigest digest = Ed25519.CreateDigest();
			byte[] array3 = new byte[digest.GetDigestSize()];
			Ed25519.Dom2(digest, phflag, ctx);
			digest.BlockUpdate(array, 0, 32);
			digest.BlockUpdate(pk, pkOff, 32);
			digest.BlockUpdate(m, mOff, mLen);
			digest.DoFinal(array3, 0);
			byte[] k = Ed25519.ReduceScalar(array3);
			uint[] array4 = new uint[8];
			Ed25519.DecodeScalar(array2, 0, array4);
			uint[] array5 = new uint[8];
			Ed25519.DecodeScalar(k, 0, array5);
			Ed25519.PointAccum pointAccum = new Ed25519.PointAccum();
			Ed25519.ScalarMultStraussVar(array4, array5, pointExt, pointAccum);
			byte[] array6 = new byte[32];
			Ed25519.EncodePoint(pointAccum, array6, 0);
			return Arrays.AreEqual(array6, array);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000EA248 File Offset: 0x000E8448
		private static void PointAddVar(bool negate, Ed25519.PointExt p, Ed25519.PointAccum r)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] array4 = X25519Field.Create();
			int[] u = r.u;
			int[] array5 = X25519Field.Create();
			int[] array6 = X25519Field.Create();
			int[] v = r.v;
			int[] zm;
			int[] zp;
			int[] zm2;
			int[] array7;
			if (negate)
			{
				zm = array4;
				zp = array3;
				zm2 = array6;
				array7 = array5;
			}
			else
			{
				zm = array3;
				zp = array4;
				zm2 = array5;
				array7 = array6;
			}
			X25519Field.Apm(r.y, r.x, array2, array);
			X25519Field.Apm(p.y, p.x, zp, zm);
			X25519Field.Mul(array, array3, array);
			X25519Field.Mul(array2, array4, array2);
			X25519Field.Mul(r.u, r.v, array3);
			X25519Field.Mul(array3, p.t, array3);
			X25519Field.Mul(array3, Ed25519.C_d2, array3);
			X25519Field.Mul(r.z, p.z, array4);
			X25519Field.Add(array4, array4, array4);
			X25519Field.Apm(array2, array, v, u);
			X25519Field.Apm(array4, array3, array7, zm2);
			X25519Field.Carry(array7);
			X25519Field.Mul(u, array5, r.x);
			X25519Field.Mul(array6, v, r.y);
			X25519Field.Mul(array5, array6, r.z);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000EA374 File Offset: 0x000E8574
		private static void PointAddVar(bool negate, Ed25519.PointExt p, Ed25519.PointExt q, Ed25519.PointExt r)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] array4 = X25519Field.Create();
			int[] array5 = X25519Field.Create();
			int[] array6 = X25519Field.Create();
			int[] array7 = X25519Field.Create();
			int[] array8 = X25519Field.Create();
			int[] zm;
			int[] zp;
			int[] zm2;
			int[] array9;
			if (negate)
			{
				zm = array4;
				zp = array3;
				zm2 = array7;
				array9 = array6;
			}
			else
			{
				zm = array3;
				zp = array4;
				zm2 = array6;
				array9 = array7;
			}
			X25519Field.Apm(p.y, p.x, array2, array);
			X25519Field.Apm(q.y, q.x, zp, zm);
			X25519Field.Mul(array, array3, array);
			X25519Field.Mul(array2, array4, array2);
			X25519Field.Mul(p.t, q.t, array3);
			X25519Field.Mul(array3, Ed25519.C_d2, array3);
			X25519Field.Mul(p.z, q.z, array4);
			X25519Field.Add(array4, array4, array4);
			X25519Field.Apm(array2, array, array8, array5);
			X25519Field.Apm(array4, array3, array9, zm2);
			X25519Field.Carry(array9);
			X25519Field.Mul(array5, array6, r.x);
			X25519Field.Mul(array7, array8, r.y);
			X25519Field.Mul(array6, array7, r.z);
			X25519Field.Mul(array5, array8, r.t);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x000EA4A0 File Offset: 0x000E86A0
		private static void PointAddPrecomp(Ed25519.PointPrecomp p, Ed25519.PointAccum r)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] u = r.u;
			int[] array4 = X25519Field.Create();
			int[] array5 = X25519Field.Create();
			int[] v = r.v;
			X25519Field.Apm(r.y, r.x, array2, array);
			X25519Field.Mul(array, p.ymx_h, array);
			X25519Field.Mul(array2, p.ypx_h, array2);
			X25519Field.Mul(r.u, r.v, array3);
			X25519Field.Mul(array3, p.xyd, array3);
			X25519Field.Apm(array2, array, v, u);
			X25519Field.Apm(r.z, array3, array5, array4);
			X25519Field.Carry(array5);
			X25519Field.Mul(u, array4, r.x);
			X25519Field.Mul(array5, v, r.y);
			X25519Field.Mul(array4, array5, r.z);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000EA578 File Offset: 0x000E8778
		private static Ed25519.PointExt PointCopy(Ed25519.PointAccum p)
		{
			Ed25519.PointExt pointExt = new Ed25519.PointExt();
			X25519Field.Copy(p.x, 0, pointExt.x, 0);
			X25519Field.Copy(p.y, 0, pointExt.y, 0);
			X25519Field.Copy(p.z, 0, pointExt.z, 0);
			X25519Field.Mul(p.u, p.v, pointExt.t);
			return pointExt;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x000EA5DC File Offset: 0x000E87DC
		private static Ed25519.PointExt PointCopy(Ed25519.PointExt p)
		{
			Ed25519.PointExt pointExt = new Ed25519.PointExt();
			X25519Field.Copy(p.x, 0, pointExt.x, 0);
			X25519Field.Copy(p.y, 0, pointExt.y, 0);
			X25519Field.Copy(p.z, 0, pointExt.z, 0);
			X25519Field.Copy(p.t, 0, pointExt.t, 0);
			return pointExt;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000EA63C File Offset: 0x000E883C
		private static void PointDouble(Ed25519.PointAccum r)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] u = r.u;
			int[] array4 = X25519Field.Create();
			int[] array5 = X25519Field.Create();
			int[] v = r.v;
			X25519Field.Sqr(r.x, array);
			X25519Field.Sqr(r.y, array2);
			X25519Field.Sqr(r.z, array3);
			X25519Field.Add(array3, array3, array3);
			X25519Field.Apm(array, array2, v, array5);
			X25519Field.Add(r.x, r.y, u);
			X25519Field.Sqr(u, u);
			X25519Field.Sub(v, u, u);
			X25519Field.Add(array3, array5, array4);
			X25519Field.Carry(array4);
			X25519Field.Mul(u, array4, r.x);
			X25519Field.Mul(array5, v, r.y);
			X25519Field.Mul(array4, array5, r.z);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000EA70E File Offset: 0x000E890E
		private static void PointExtendXY(Ed25519.PointAccum p)
		{
			X25519Field.One(p.z);
			X25519Field.Copy(p.x, 0, p.u, 0);
			X25519Field.Copy(p.y, 0, p.v, 0);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000EA741 File Offset: 0x000E8941
		private static void PointExtendXY(Ed25519.PointExt p)
		{
			X25519Field.One(p.z);
			X25519Field.Mul(p.x, p.y, p.t);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000EA768 File Offset: 0x000E8968
		private static void PointLookup(int block, int index, Ed25519.PointPrecomp p)
		{
			int num = block * 8 * 3 * 10;
			for (int i = 0; i < 8; i++)
			{
				int mask = (i ^ index) - 1 >> 31;
				Nat.CMov(10, mask, Ed25519.precompBase, num, p.ypx_h, 0);
				num += 10;
				Nat.CMov(10, mask, Ed25519.precompBase, num, p.ymx_h, 0);
				num += 10;
				Nat.CMov(10, mask, Ed25519.precompBase, num, p.xyd, 0);
				num += 10;
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000EA7E4 File Offset: 0x000E89E4
		private static Ed25519.PointExt[] PointPrecompVar(Ed25519.PointExt p, int count)
		{
			Ed25519.PointExt pointExt = new Ed25519.PointExt();
			Ed25519.PointAddVar(false, p, p, pointExt);
			Ed25519.PointExt[] array = new Ed25519.PointExt[count];
			array[0] = Ed25519.PointCopy(p);
			for (int i = 1; i < count; i++)
			{
				Ed25519.PointAddVar(false, array[i - 1], pointExt, array[i] = new Ed25519.PointExt());
			}
			return array;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000EA834 File Offset: 0x000E8A34
		private static void PointSetNeutral(Ed25519.PointAccum p)
		{
			X25519Field.Zero(p.x);
			X25519Field.One(p.y);
			X25519Field.One(p.z);
			X25519Field.Zero(p.u);
			X25519Field.One(p.v);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000EA86D File Offset: 0x000E8A6D
		private static void PointSetNeutral(Ed25519.PointExt p)
		{
			X25519Field.Zero(p.x);
			X25519Field.One(p.y);
			X25519Field.One(p.z);
			X25519Field.Zero(p.t);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000EA89C File Offset: 0x000E8A9C
		public static void Precompute()
		{
			object obj = Ed25519.precompLock;
			lock (obj)
			{
				if (Ed25519.precompBase == null)
				{
					Ed25519.PointExt pointExt = new Ed25519.PointExt();
					X25519Field.Copy(Ed25519.B_x, 0, pointExt.x, 0);
					X25519Field.Copy(Ed25519.B_y, 0, pointExt.y, 0);
					Ed25519.PointExtendXY(pointExt);
					Ed25519.precompBaseTable = Ed25519.PointPrecompVar(pointExt, 32);
					Ed25519.PointAccum pointAccum = new Ed25519.PointAccum();
					X25519Field.Copy(Ed25519.B_x, 0, pointAccum.x, 0);
					X25519Field.Copy(Ed25519.B_y, 0, pointAccum.y, 0);
					Ed25519.PointExtendXY(pointAccum);
					Ed25519.precompBase = new int[1920];
					int num = 0;
					for (int i = 0; i < 8; i++)
					{
						Ed25519.PointExt[] array = new Ed25519.PointExt[4];
						Ed25519.PointExt pointExt2 = new Ed25519.PointExt();
						Ed25519.PointSetNeutral(pointExt2);
						for (int j = 0; j < 4; j++)
						{
							Ed25519.PointExt q = Ed25519.PointCopy(pointAccum);
							Ed25519.PointAddVar(true, pointExt2, q, pointExt2);
							Ed25519.PointDouble(pointAccum);
							array[j] = Ed25519.PointCopy(pointAccum);
							if (i + j != 10)
							{
								for (int k = 1; k < 8; k++)
								{
									Ed25519.PointDouble(pointAccum);
								}
							}
						}
						Ed25519.PointExt[] array2 = new Ed25519.PointExt[8];
						int num2 = 0;
						array2[num2++] = pointExt2;
						for (int l = 0; l < 3; l++)
						{
							int num3 = 1 << l;
							int m = 0;
							while (m < num3)
							{
								Ed25519.PointAddVar(false, array2[num2 - num3], array[l], array2[num2] = new Ed25519.PointExt());
								m++;
								num2++;
							}
						}
						for (int n = 0; n < 8; n++)
						{
							Ed25519.PointExt pointExt3 = array2[n];
							int[] array3 = X25519Field.Create();
							int[] array4 = X25519Field.Create();
							X25519Field.Add(pointExt3.z, pointExt3.z, array3);
							X25519Field.Inv(array3, array4);
							X25519Field.Mul(pointExt3.x, array4, array3);
							X25519Field.Mul(pointExt3.y, array4, array4);
							Ed25519.PointPrecomp pointPrecomp = new Ed25519.PointPrecomp();
							X25519Field.Apm(array4, array3, pointPrecomp.ypx_h, pointPrecomp.ymx_h);
							X25519Field.Mul(array3, array4, pointPrecomp.xyd);
							X25519Field.Mul(pointPrecomp.xyd, Ed25519.C_d4, pointPrecomp.xyd);
							X25519Field.Normalize(pointPrecomp.ypx_h);
							X25519Field.Normalize(pointPrecomp.ymx_h);
							X25519Field.Copy(pointPrecomp.ypx_h, 0, Ed25519.precompBase, num);
							num += 10;
							X25519Field.Copy(pointPrecomp.ymx_h, 0, Ed25519.precompBase, num);
							num += 10;
							X25519Field.Copy(pointPrecomp.xyd, 0, Ed25519.precompBase, num);
							num += 10;
						}
					}
				}
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000EAB68 File Offset: 0x000E8D68
		private static void PruneScalar(byte[] n, int nOff, byte[] r)
		{
			Array.Copy(n, nOff, r, 0, 32);
			int num = 0;
			r[num] &= 248;
			int num2 = 31;
			r[num2] &= 127;
			int num3 = 31;
			r[num3] |= 64;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000EABA4 File Offset: 0x000E8DA4
		private static byte[] ReduceScalar(byte[] n)
		{
			long num = (long)((ulong)Ed25519.Decode32(n, 0) & (ulong)-1);
			long num2 = (long)((ulong)((ulong)Ed25519.Decode24(n, 4) << 4) & (ulong)-1);
			long num3 = (long)((ulong)Ed25519.Decode32(n, 7) & (ulong)-1);
			long num4 = (long)((ulong)((ulong)Ed25519.Decode24(n, 11) << 4) & (ulong)-1);
			long num5 = (long)((ulong)Ed25519.Decode32(n, 14) & (ulong)-1);
			long num6 = (long)((ulong)((ulong)Ed25519.Decode24(n, 18) << 4) & (ulong)-1);
			long num7 = (long)((ulong)Ed25519.Decode32(n, 21) & (ulong)-1);
			long num8 = (long)((ulong)((ulong)Ed25519.Decode24(n, 25) << 4) & (ulong)-1);
			long num9 = (long)((ulong)Ed25519.Decode32(n, 28) & (ulong)-1);
			long num10 = (long)((ulong)((ulong)Ed25519.Decode24(n, 32) << 4) & (ulong)-1);
			long num11 = (long)((ulong)Ed25519.Decode32(n, 35) & (ulong)-1);
			long num12 = (long)((ulong)((ulong)Ed25519.Decode24(n, 39) << 4) & (ulong)-1);
			long num13 = (long)((ulong)Ed25519.Decode32(n, 42) & (ulong)-1);
			long num14 = (long)((ulong)((ulong)Ed25519.Decode24(n, 46) << 4) & (ulong)-1);
			long num15 = (long)((ulong)Ed25519.Decode32(n, 49) & (ulong)-1);
			long num16 = (long)((ulong)((ulong)Ed25519.Decode24(n, 53) << 4) & (ulong)-1);
			long num17 = (long)((ulong)Ed25519.Decode32(n, 56) & (ulong)-1);
			long num18 = (long)((ulong)((ulong)Ed25519.Decode24(n, 60) << 4) & (ulong)-1);
			long num19 = (long)((ulong)n[63] & 255UL);
			num10 -= num19 * -50998291L;
			num11 -= num19 * 19280294L;
			num12 -= num19 * 127719000L;
			num13 -= num19 * -6428113L;
			num14 -= num19 * 5343L;
			num18 += num17 >> 28;
			num17 &= 268435455L;
			num9 -= num18 * -50998291L;
			num10 -= num18 * 19280294L;
			num11 -= num18 * 127719000L;
			num12 -= num18 * -6428113L;
			num13 -= num18 * 5343L;
			num8 -= num17 * -50998291L;
			num9 -= num17 * 19280294L;
			num10 -= num17 * 127719000L;
			num11 -= num17 * -6428113L;
			num12 -= num17 * 5343L;
			num16 += num15 >> 28;
			num15 &= 268435455L;
			num7 -= num16 * -50998291L;
			num8 -= num16 * 19280294L;
			num9 -= num16 * 127719000L;
			num10 -= num16 * -6428113L;
			num11 -= num16 * 5343L;
			num6 -= num15 * -50998291L;
			num7 -= num15 * 19280294L;
			num8 -= num15 * 127719000L;
			num9 -= num15 * -6428113L;
			num10 -= num15 * 5343L;
			num14 += num13 >> 28;
			num13 &= 268435455L;
			num5 -= num14 * -50998291L;
			num6 -= num14 * 19280294L;
			num7 -= num14 * 127719000L;
			num8 -= num14 * -6428113L;
			num9 -= num14 * 5343L;
			num13 += num12 >> 28;
			num12 &= 268435455L;
			num4 -= num13 * -50998291L;
			num5 -= num13 * 19280294L;
			num6 -= num13 * 127719000L;
			num7 -= num13 * -6428113L;
			num8 -= num13 * 5343L;
			num12 += num11 >> 28;
			num11 &= 268435455L;
			num3 -= num12 * -50998291L;
			num4 -= num12 * 19280294L;
			num5 -= num12 * 127719000L;
			num6 -= num12 * -6428113L;
			num7 -= num12 * 5343L;
			num11 += num10 >> 28;
			num10 &= 268435455L;
			num2 -= num11 * -50998291L;
			num3 -= num11 * 19280294L;
			num4 -= num11 * 127719000L;
			num5 -= num11 * -6428113L;
			num6 -= num11 * 5343L;
			num9 += num8 >> 28;
			num8 &= 268435455L;
			num10 += num9 >> 28;
			num9 &= 268435455L;
			long num20 = num9 >> 27 & 1L;
			num10 += num20;
			num -= num10 * -50998291L;
			num2 -= num10 * 19280294L;
			num3 -= num10 * 127719000L;
			num4 -= num10 * -6428113L;
			num5 -= num10 * 5343L;
			num2 += num >> 28;
			num &= 268435455L;
			num3 += num2 >> 28;
			num2 &= 268435455L;
			num4 += num3 >> 28;
			num3 &= 268435455L;
			num5 += num4 >> 28;
			num4 &= 268435455L;
			num6 += num5 >> 28;
			num5 &= 268435455L;
			num7 += num6 >> 28;
			num6 &= 268435455L;
			num8 += num7 >> 28;
			num7 &= 268435455L;
			num9 += num8 >> 28;
			num8 &= 268435455L;
			num10 = num9 >> 28;
			num9 &= 268435455L;
			num10 -= num20;
			num += (num10 & -50998291L);
			num2 += (num10 & 19280294L);
			num3 += (num10 & 127719000L);
			num4 += (num10 & -6428113L);
			num5 += (num10 & 5343L);
			num2 += num >> 28;
			num &= 268435455L;
			num3 += num2 >> 28;
			num2 &= 268435455L;
			num4 += num3 >> 28;
			num3 &= 268435455L;
			num5 += num4 >> 28;
			num4 &= 268435455L;
			num6 += num5 >> 28;
			num5 &= 268435455L;
			num7 += num6 >> 28;
			num6 &= 268435455L;
			num8 += num7 >> 28;
			num7 &= 268435455L;
			num9 += num8 >> 28;
			num8 &= 268435455L;
			byte[] array = new byte[32];
			Ed25519.Encode56((ulong)(num | num2 << 28), array, 0);
			Ed25519.Encode56((ulong)(num3 | num4 << 28), array, 7);
			Ed25519.Encode56((ulong)(num5 | num6 << 28), array, 14);
			Ed25519.Encode56((ulong)(num7 | num8 << 28), array, 21);
			Ed25519.Encode32((uint)num9, array, 28);
			return array;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000EB200 File Offset: 0x000E9400
		private static void ScalarMultBase(byte[] k, Ed25519.PointAccum r)
		{
			Ed25519.Precompute();
			Ed25519.PointSetNeutral(r);
			uint[] array = new uint[8];
			Ed25519.DecodeScalar(k, 0, array);
			Nat.CAdd(8, (int)(~array[0] & 1U), array, Ed25519.L, array);
			Nat.ShiftDownBit(8, array, 1U);
			for (int i = 0; i < 8; i++)
			{
				array[i] = Interleave.Shuffle2(array[i]);
			}
			Ed25519.PointPrecomp pointPrecomp = new Ed25519.PointPrecomp();
			int num = 28;
			for (;;)
			{
				for (int j = 0; j < 8; j++)
				{
					uint num2 = array[j] >> num;
					int num3 = (int)(num2 >> 3 & 1U);
					int index = (int)((num2 ^ (uint)(-(uint)num3)) & 7U);
					Ed25519.PointLookup(j, index, pointPrecomp);
					X25519Field.CSwap(num3, pointPrecomp.ypx_h, pointPrecomp.ymx_h);
					X25519Field.CNegate(num3, pointPrecomp.xyd);
					Ed25519.PointAddPrecomp(pointPrecomp, r);
				}
				if ((num -= 4) < 0)
				{
					break;
				}
				Ed25519.PointDouble(r);
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000EB2D0 File Offset: 0x000E94D0
		private static void ScalarMultBaseEncoded(byte[] k, byte[] r, int rOff)
		{
			Ed25519.PointAccum pointAccum = new Ed25519.PointAccum();
			Ed25519.ScalarMultBase(k, pointAccum);
			Ed25519.EncodePoint(pointAccum, r, rOff);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000EB2F4 File Offset: 0x000E94F4
		internal static void ScalarMultBaseYZ(byte[] k, int kOff, int[] y, int[] z)
		{
			byte[] array = new byte[32];
			Ed25519.PruneScalar(k, kOff, array);
			Ed25519.PointAccum pointAccum = new Ed25519.PointAccum();
			Ed25519.ScalarMultBase(array, pointAccum);
			X25519Field.Copy(pointAccum.y, 0, y, 0);
			X25519Field.Copy(pointAccum.z, 0, z, 0);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000EB33C File Offset: 0x000E953C
		private static void ScalarMultStraussVar(uint[] nb, uint[] np, Ed25519.PointExt p, Ed25519.PointAccum r)
		{
			Ed25519.Precompute();
			int num = 5;
			sbyte[] wnaf = Ed25519.GetWnaf(nb, 7);
			sbyte[] wnaf2 = Ed25519.GetWnaf(np, num);
			Ed25519.PointExt[] array = Ed25519.PointPrecompVar(p, 1 << num - 2);
			Ed25519.PointSetNeutral(r);
			int num2 = 255;
			while (num2 > 0 && (wnaf[num2] | wnaf2[num2]) == 0)
			{
				num2--;
			}
			for (;;)
			{
				int num3 = (int)wnaf[num2];
				if (num3 != 0)
				{
					int num4 = num3 >> 31;
					int num5 = (num3 ^ num4) >> 1;
					Ed25519.PointAddVar(num4 != 0, Ed25519.precompBaseTable[num5], r);
				}
				int num6 = (int)wnaf2[num2];
				if (num6 != 0)
				{
					int num7 = num6 >> 31;
					int num8 = (num6 ^ num7) >> 1;
					Ed25519.PointAddVar(num7 != 0, array[num8], r);
				}
				if (--num2 < 0)
				{
					break;
				}
				Ed25519.PointDouble(r);
			}
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000EB3FC File Offset: 0x000E95FC
		public static void Sign(byte[] sk, int skOff, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			byte[] ctx = null;
			byte phflag = 0;
			Ed25519.ImplSign(sk, skOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000EB420 File Offset: 0x000E9620
		public static void Sign(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			byte[] ctx = null;
			byte phflag = 0;
			Ed25519.ImplSign(sk, skOff, pk, pkOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000EB448 File Offset: 0x000E9648
		public static void Sign(byte[] sk, int skOff, byte[] ctx, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			byte phflag = 0;
			Ed25519.ImplSign(sk, skOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000EB46C File Offset: 0x000E966C
		public static void Sign(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] ctx, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			byte phflag = 0;
			Ed25519.ImplSign(sk, skOff, pk, pkOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000EB494 File Offset: 0x000E9694
		public static void SignPrehash(byte[] sk, int skOff, byte[] ctx, byte[] ph, int phOff, byte[] sig, int sigOff)
		{
			byte phflag = 1;
			Ed25519.ImplSign(sk, skOff, ctx, phflag, ph, phOff, Ed25519.PrehashSize, sig, sigOff);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000EB4B8 File Offset: 0x000E96B8
		public static void SignPrehash(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] ctx, byte[] ph, int phOff, byte[] sig, int sigOff)
		{
			byte phflag = 1;
			Ed25519.ImplSign(sk, skOff, pk, pkOff, ctx, phflag, ph, phOff, Ed25519.PrehashSize, sig, sigOff);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000EB4E0 File Offset: 0x000E96E0
		public static void SignPrehash(byte[] sk, int skOff, byte[] ctx, IDigest ph, byte[] sig, int sigOff)
		{
			byte[] array = new byte[Ed25519.PrehashSize];
			if (Ed25519.PrehashSize != ph.DoFinal(array, 0))
			{
				throw new ArgumentException("ph");
			}
			byte phflag = 1;
			Ed25519.ImplSign(sk, skOff, ctx, phflag, array, 0, array.Length, sig, sigOff);
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000EB528 File Offset: 0x000E9728
		public static void SignPrehash(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] ctx, IDigest ph, byte[] sig, int sigOff)
		{
			byte[] array = new byte[Ed25519.PrehashSize];
			if (Ed25519.PrehashSize != ph.DoFinal(array, 0))
			{
				throw new ArgumentException("ph");
			}
			byte phflag = 1;
			Ed25519.ImplSign(sk, skOff, pk, pkOff, ctx, phflag, array, 0, array.Length, sig, sigOff);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000EB574 File Offset: 0x000E9774
		public static bool Verify(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] m, int mOff, int mLen)
		{
			byte[] ctx = null;
			byte phflag = 0;
			return Ed25519.ImplVerify(sig, sigOff, pk, pkOff, ctx, phflag, m, mOff, mLen);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000EB598 File Offset: 0x000E9798
		public static bool Verify(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] ctx, byte[] m, int mOff, int mLen)
		{
			byte phflag = 0;
			return Ed25519.ImplVerify(sig, sigOff, pk, pkOff, ctx, phflag, m, mOff, mLen);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000EB5BC File Offset: 0x000E97BC
		public static bool VerifyPrehash(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] ctx, byte[] ph, int phOff)
		{
			byte phflag = 1;
			return Ed25519.ImplVerify(sig, sigOff, pk, pkOff, ctx, phflag, ph, phOff, Ed25519.PrehashSize);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000EB5E0 File Offset: 0x000E97E0
		public static bool VerifyPrehash(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] ctx, IDigest ph)
		{
			byte[] array = new byte[Ed25519.PrehashSize];
			if (Ed25519.PrehashSize != ph.DoFinal(array, 0))
			{
				throw new ArgumentException("ph");
			}
			byte phflag = 1;
			return Ed25519.ImplVerify(sig, sigOff, pk, pkOff, ctx, phflag, array, 0, array.Length);
		}

		// Token: 0x04001848 RID: 6216
		private const long M28L = 268435455L;

		// Token: 0x04001849 RID: 6217
		private const long M32L = 4294967295L;

		// Token: 0x0400184A RID: 6218
		private const int PointBytes = 32;

		// Token: 0x0400184B RID: 6219
		private const int ScalarUints = 8;

		// Token: 0x0400184C RID: 6220
		private const int ScalarBytes = 32;

		// Token: 0x0400184D RID: 6221
		public static readonly int PrehashSize = 64;

		// Token: 0x0400184E RID: 6222
		public static readonly int PublicKeySize = 32;

		// Token: 0x0400184F RID: 6223
		public static readonly int SecretKeySize = 32;

		// Token: 0x04001850 RID: 6224
		public static readonly int SignatureSize = 64;

		// Token: 0x04001851 RID: 6225
		private static readonly byte[] Dom2Prefix = Strings.ToByteArray("SigEd25519 no Ed25519 collisions");

		// Token: 0x04001852 RID: 6226
		private static readonly uint[] P = new uint[]
		{
			4294967277U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			2147483647U
		};

		// Token: 0x04001853 RID: 6227
		private static readonly uint[] L = new uint[]
		{
			1559614445U,
			1477600026U,
			2734136534U,
			350157278U,
			0U,
			0U,
			0U,
			268435456U
		};

		// Token: 0x04001854 RID: 6228
		private const int L0 = -50998291;

		// Token: 0x04001855 RID: 6229
		private const int L1 = 19280294;

		// Token: 0x04001856 RID: 6230
		private const int L2 = 127719000;

		// Token: 0x04001857 RID: 6231
		private const int L3 = -6428113;

		// Token: 0x04001858 RID: 6232
		private const int L4 = 5343;

		// Token: 0x04001859 RID: 6233
		private static readonly int[] B_x = new int[]
		{
			52811034,
			25909283,
			8072341,
			50637101,
			13785486,
			30858332,
			20483199,
			20966410,
			43936626,
			4379245
		};

		// Token: 0x0400185A RID: 6234
		private static readonly int[] B_y = new int[]
		{
			40265304,
			26843545,
			6710886,
			53687091,
			13421772,
			40265318,
			26843545,
			6710886,
			53687091,
			13421772
		};

		// Token: 0x0400185B RID: 6235
		private static readonly int[] C_d = new int[]
		{
			56195235,
			47411844,
			25868126,
			40503822,
			57364,
			58321048,
			30416477,
			31930572,
			57760639,
			10749657
		};

		// Token: 0x0400185C RID: 6236
		private static readonly int[] C_d2 = new int[]
		{
			45281625,
			27714825,
			18181821,
			13898781,
			114729,
			49533232,
			60832955,
			30306712,
			48412415,
			4722099
		};

		// Token: 0x0400185D RID: 6237
		private static readonly int[] C_d4 = new int[]
		{
			23454386,
			55429651,
			2809210,
			27797563,
			229458,
			31957600,
			54557047,
			27058993,
			29715967,
			9444199
		};

		// Token: 0x0400185E RID: 6238
		private const int WnafWidthBase = 7;

		// Token: 0x0400185F RID: 6239
		private const int PrecompBlocks = 8;

		// Token: 0x04001860 RID: 6240
		private const int PrecompTeeth = 4;

		// Token: 0x04001861 RID: 6241
		private const int PrecompSpacing = 8;

		// Token: 0x04001862 RID: 6242
		private const int PrecompPoints = 8;

		// Token: 0x04001863 RID: 6243
		private const int PrecompMask = 7;

		// Token: 0x04001864 RID: 6244
		private static readonly object precompLock = new object();

		// Token: 0x04001865 RID: 6245
		private static Ed25519.PointExt[] precompBaseTable = null;

		// Token: 0x04001866 RID: 6246
		private static int[] precompBase = null;

		// Token: 0x020008EB RID: 2283
		public enum Algorithm
		{
			// Token: 0x0400343A RID: 13370
			Ed25519,
			// Token: 0x0400343B RID: 13371
			Ed25519ctx,
			// Token: 0x0400343C RID: 13372
			Ed25519ph
		}

		// Token: 0x020008EC RID: 2284
		private class PointAccum
		{
			// Token: 0x0400343D RID: 13373
			internal int[] x = X25519Field.Create();

			// Token: 0x0400343E RID: 13374
			internal int[] y = X25519Field.Create();

			// Token: 0x0400343F RID: 13375
			internal int[] z = X25519Field.Create();

			// Token: 0x04003440 RID: 13376
			internal int[] u = X25519Field.Create();

			// Token: 0x04003441 RID: 13377
			internal int[] v = X25519Field.Create();
		}

		// Token: 0x020008ED RID: 2285
		private class PointExt
		{
			// Token: 0x04003442 RID: 13378
			internal int[] x = X25519Field.Create();

			// Token: 0x04003443 RID: 13379
			internal int[] y = X25519Field.Create();

			// Token: 0x04003444 RID: 13380
			internal int[] z = X25519Field.Create();

			// Token: 0x04003445 RID: 13381
			internal int[] t = X25519Field.Create();
		}

		// Token: 0x020008EE RID: 2286
		private class PointPrecomp
		{
			// Token: 0x04003446 RID: 13382
			internal int[] ypx_h = X25519Field.Create();

			// Token: 0x04003447 RID: 13383
			internal int[] ymx_h = X25519Field.Create();

			// Token: 0x04003448 RID: 13384
			internal int[] xyd = X25519Field.Create();
		}
	}
}
