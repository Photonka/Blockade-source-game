using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003D7 RID: 983
	public abstract class PbeParametersGenerator
	{
		// Token: 0x0600281A RID: 10266 RVA: 0x0010E2E5 File Offset: 0x0010C4E5
		public virtual void Init(byte[] password, byte[] salt, int iterationCount)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt");
			}
			this.mPassword = Arrays.Clone(password);
			this.mSalt = Arrays.Clone(salt);
			this.mIterationCount = iterationCount;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x0010E322 File Offset: 0x0010C522
		public virtual byte[] Password
		{
			get
			{
				return Arrays.Clone(this.mPassword);
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0010E32F File Offset: 0x0010C52F
		[Obsolete("Use 'Password' property")]
		public byte[] GetPassword()
		{
			return this.Password;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x0010E337 File Offset: 0x0010C537
		public virtual byte[] Salt
		{
			get
			{
				return Arrays.Clone(this.mSalt);
			}
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x0010E344 File Offset: 0x0010C544
		[Obsolete("Use 'Salt' property")]
		public byte[] GetSalt()
		{
			return this.Salt;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x0010E34C File Offset: 0x0010C54C
		public virtual int IterationCount
		{
			get
			{
				return this.mIterationCount;
			}
		}

		// Token: 0x06002820 RID: 10272
		[Obsolete("Use version with 'algorithm' parameter")]
		public abstract ICipherParameters GenerateDerivedParameters(int keySize);

		// Token: 0x06002821 RID: 10273
		public abstract ICipherParameters GenerateDerivedParameters(string algorithm, int keySize);

		// Token: 0x06002822 RID: 10274
		[Obsolete("Use version with 'algorithm' parameter")]
		public abstract ICipherParameters GenerateDerivedParameters(int keySize, int ivSize);

		// Token: 0x06002823 RID: 10275
		public abstract ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize);

		// Token: 0x06002824 RID: 10276
		public abstract ICipherParameters GenerateDerivedMacParameters(int keySize);

		// Token: 0x06002825 RID: 10277 RVA: 0x0010E354 File Offset: 0x0010C554
		public static byte[] Pkcs5PasswordToBytes(char[] password)
		{
			if (password == null)
			{
				return new byte[0];
			}
			return Strings.ToByteArray(password);
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0010E366 File Offset: 0x0010C566
		[Obsolete("Use version taking 'char[]' instead")]
		public static byte[] Pkcs5PasswordToBytes(string password)
		{
			if (password == null)
			{
				return new byte[0];
			}
			return Strings.ToByteArray(password);
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0010E378 File Offset: 0x0010C578
		public static byte[] Pkcs5PasswordToUtf8Bytes(char[] password)
		{
			if (password == null)
			{
				return new byte[0];
			}
			return Encoding.UTF8.GetBytes(password);
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0010E38F File Offset: 0x0010C58F
		[Obsolete("Use version taking 'char[]' instead")]
		public static byte[] Pkcs5PasswordToUtf8Bytes(string password)
		{
			if (password == null)
			{
				return new byte[0];
			}
			return Encoding.UTF8.GetBytes(password);
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0010E3A6 File Offset: 0x0010C5A6
		public static byte[] Pkcs12PasswordToBytes(char[] password)
		{
			return PbeParametersGenerator.Pkcs12PasswordToBytes(password, false);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x0010E3B0 File Offset: 0x0010C5B0
		public static byte[] Pkcs12PasswordToBytes(char[] password, bool wrongPkcs12Zero)
		{
			if (password == null || password.Length < 1)
			{
				return new byte[wrongPkcs12Zero ? 2 : 0];
			}
			byte[] array = new byte[(password.Length + 1) * 2];
			Encoding.BigEndianUnicode.GetBytes(password, 0, password.Length, array, 0);
			return array;
		}

		// Token: 0x040019CC RID: 6604
		protected byte[] mPassword;

		// Token: 0x040019CD RID: 6605
		protected byte[] mSalt;

		// Token: 0x040019CE RID: 6606
		protected int mIterationCount;
	}
}
