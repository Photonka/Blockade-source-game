using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E8 RID: 1256
	public class SkeinParameters : ICipherParameters
	{
		// Token: 0x0600304A RID: 12362 RVA: 0x00129807 File Offset: 0x00127A07
		public SkeinParameters() : this(Platform.CreateHashtable())
		{
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x00129814 File Offset: 0x00127A14
		private SkeinParameters(IDictionary parameters)
		{
			this.parameters = parameters;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x00129823 File Offset: 0x00127A23
		public IDictionary GetParameters()
		{
			return this.parameters;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x0012982B File Offset: 0x00127A2B
		public byte[] GetKey()
		{
			return (byte[])this.parameters[0];
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x00129843 File Offset: 0x00127A43
		public byte[] GetPersonalisation()
		{
			return (byte[])this.parameters[8];
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x0012985B File Offset: 0x00127A5B
		public byte[] GetPublicKey()
		{
			return (byte[])this.parameters[12];
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x00129874 File Offset: 0x00127A74
		public byte[] GetKeyIdentifier()
		{
			return (byte[])this.parameters[16];
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0012988D File Offset: 0x00127A8D
		public byte[] GetNonce()
		{
			return (byte[])this.parameters[20];
		}

		// Token: 0x04001EF4 RID: 7924
		public const int PARAM_TYPE_KEY = 0;

		// Token: 0x04001EF5 RID: 7925
		public const int PARAM_TYPE_CONFIG = 4;

		// Token: 0x04001EF6 RID: 7926
		public const int PARAM_TYPE_PERSONALISATION = 8;

		// Token: 0x04001EF7 RID: 7927
		public const int PARAM_TYPE_PUBLIC_KEY = 12;

		// Token: 0x04001EF8 RID: 7928
		public const int PARAM_TYPE_KEY_IDENTIFIER = 16;

		// Token: 0x04001EF9 RID: 7929
		public const int PARAM_TYPE_NONCE = 20;

		// Token: 0x04001EFA RID: 7930
		public const int PARAM_TYPE_MESSAGE = 48;

		// Token: 0x04001EFB RID: 7931
		public const int PARAM_TYPE_OUTPUT = 63;

		// Token: 0x04001EFC RID: 7932
		private IDictionary parameters;

		// Token: 0x0200092E RID: 2350
		public class Builder
		{
			// Token: 0x06004E50 RID: 20048 RVA: 0x001B3920 File Offset: 0x001B1B20
			public Builder()
			{
			}

			// Token: 0x06004E51 RID: 20049 RVA: 0x001B3934 File Offset: 0x001B1B34
			public Builder(IDictionary paramsMap)
			{
				foreach (object obj in paramsMap.Keys)
				{
					int num = (int)obj;
					this.parameters.Add(num, paramsMap[num]);
				}
			}

			// Token: 0x06004E52 RID: 20050 RVA: 0x001B3994 File Offset: 0x001B1B94
			public Builder(SkeinParameters parameters)
			{
				foreach (object obj in parameters.parameters.Keys)
				{
					int num = (int)obj;
					this.parameters.Add(num, parameters.parameters[num]);
				}
			}

			// Token: 0x06004E53 RID: 20051 RVA: 0x001B39FC File Offset: 0x001B1BFC
			public SkeinParameters.Builder Set(int type, byte[] value)
			{
				if (value == null)
				{
					throw new ArgumentException("Parameter value must not be null.");
				}
				if (type != 0 && (type <= 4 || type >= 63 || type == 48))
				{
					throw new ArgumentException("Parameter types must be in the range 0,5..47,49..62.");
				}
				if (type == 4)
				{
					throw new ArgumentException("Parameter type " + 4 + " is reserved for internal use.");
				}
				this.parameters.Add(type, value);
				return this;
			}

			// Token: 0x06004E54 RID: 20052 RVA: 0x001B3A65 File Offset: 0x001B1C65
			public SkeinParameters.Builder SetKey(byte[] key)
			{
				return this.Set(0, key);
			}

			// Token: 0x06004E55 RID: 20053 RVA: 0x001B3A6F File Offset: 0x001B1C6F
			public SkeinParameters.Builder SetPersonalisation(byte[] personalisation)
			{
				return this.Set(8, personalisation);
			}

			// Token: 0x06004E56 RID: 20054 RVA: 0x001B3A7C File Offset: 0x001B1C7C
			public SkeinParameters.Builder SetPersonalisation(DateTime date, string emailAddress, string distinguisher)
			{
				SkeinParameters.Builder result;
				try
				{
					MemoryStream memoryStream = new MemoryStream();
					StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
					streamWriter.Write(date.ToString("YYYYMMDD", CultureInfo.InvariantCulture));
					streamWriter.Write(" ");
					streamWriter.Write(emailAddress);
					streamWriter.Write(" ");
					streamWriter.Write(distinguisher);
					Platform.Dispose(streamWriter);
					result = this.Set(8, memoryStream.ToArray());
				}
				catch (IOException innerException)
				{
					throw new InvalidOperationException("Byte I/O failed.", innerException);
				}
				return result;
			}

			// Token: 0x06004E57 RID: 20055 RVA: 0x001B3B08 File Offset: 0x001B1D08
			public SkeinParameters.Builder SetPublicKey(byte[] publicKey)
			{
				return this.Set(12, publicKey);
			}

			// Token: 0x06004E58 RID: 20056 RVA: 0x001B3B13 File Offset: 0x001B1D13
			public SkeinParameters.Builder SetKeyIdentifier(byte[] keyIdentifier)
			{
				return this.Set(16, keyIdentifier);
			}

			// Token: 0x06004E59 RID: 20057 RVA: 0x001B3B1E File Offset: 0x001B1D1E
			public SkeinParameters.Builder SetNonce(byte[] nonce)
			{
				return this.Set(20, nonce);
			}

			// Token: 0x06004E5A RID: 20058 RVA: 0x001B3B29 File Offset: 0x001B1D29
			public SkeinParameters Build()
			{
				return new SkeinParameters(this.parameters);
			}

			// Token: 0x04003506 RID: 13574
			private IDictionary parameters = Platform.CreateHashtable();
		}
	}
}
