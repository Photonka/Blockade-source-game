using System;
using System.IO;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000641 RID: 1601
	public class DerObjectIdentifier : Asn1Object
	{
		// Token: 0x06003C43 RID: 15427 RVA: 0x001738D8 File Offset: 0x00171AD8
		public static DerObjectIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is DerObjectIdentifier)
			{
				return (DerObjectIdentifier)obj;
			}
			if (obj is byte[])
			{
				return DerObjectIdentifier.FromOctetString((byte[])obj);
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x00173928 File Offset: 0x00171B28
		public static DerObjectIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			Asn1Object @object = obj.GetObject();
			if (explicitly || @object is DerObjectIdentifier)
			{
				return DerObjectIdentifier.GetInstance(@object);
			}
			return DerObjectIdentifier.FromOctetString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x0017395E File Offset: 0x00171B5E
		public DerObjectIdentifier(string identifier)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			if (!DerObjectIdentifier.IsValidIdentifier(identifier))
			{
				throw new FormatException("string " + identifier + " not an OID");
			}
			this.identifier = identifier;
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x0017399C File Offset: 0x00171B9C
		internal DerObjectIdentifier(DerObjectIdentifier oid, string branchID)
		{
			if (!DerObjectIdentifier.IsValidBranchID(branchID, 0))
			{
				throw new ArgumentException("string " + branchID + " not a valid OID branch", "branchID");
			}
			this.identifier = oid.Id + "." + branchID;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x001739EA File Offset: 0x00171BEA
		public string Id
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x001739F2 File Offset: 0x00171BF2
		public virtual DerObjectIdentifier Branch(string branchID)
		{
			return new DerObjectIdentifier(this, branchID);
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x001739FC File Offset: 0x00171BFC
		public virtual bool On(DerObjectIdentifier stem)
		{
			string id = this.Id;
			string id2 = stem.Id;
			return id.Length > id2.Length && id[id2.Length] == '.' && Platform.StartsWith(id, id2);
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x00173A3E File Offset: 0x00171C3E
		internal DerObjectIdentifier(byte[] bytes)
		{
			this.identifier = DerObjectIdentifier.MakeOidStringFromBytes(bytes);
			this.body = Arrays.Clone(bytes);
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x00173A60 File Offset: 0x00171C60
		private void WriteField(Stream outputStream, long fieldValue)
		{
			byte[] array = new byte[9];
			int num = 8;
			array[num] = (byte)(fieldValue & 127L);
			while (fieldValue >= 128L)
			{
				fieldValue >>= 7;
				array[--num] = (byte)((fieldValue & 127L) | 128L);
			}
			outputStream.Write(array, num, 9 - num);
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x00173AB0 File Offset: 0x00171CB0
		private void WriteField(Stream outputStream, BigInteger fieldValue)
		{
			int num = (fieldValue.BitLength + 6) / 7;
			if (num == 0)
			{
				outputStream.WriteByte(0);
				return;
			}
			BigInteger bigInteger = fieldValue;
			byte[] array = new byte[num];
			for (int i = num - 1; i >= 0; i--)
			{
				array[i] = (byte)((bigInteger.IntValue & 127) | 128);
				bigInteger = bigInteger.ShiftRight(7);
			}
			byte[] array2 = array;
			int num2 = num - 1;
			array2[num2] &= 127;
			outputStream.Write(array, 0, array.Length);
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x00173B20 File Offset: 0x00171D20
		private void DoOutput(MemoryStream bOut)
		{
			OidTokenizer oidTokenizer = new OidTokenizer(this.identifier);
			string text = oidTokenizer.NextToken();
			int num = int.Parse(text) * 40;
			text = oidTokenizer.NextToken();
			if (text.Length <= 18)
			{
				this.WriteField(bOut, (long)num + long.Parse(text));
			}
			else
			{
				this.WriteField(bOut, new BigInteger(text).Add(BigInteger.ValueOf((long)num)));
			}
			while (oidTokenizer.HasMoreTokens)
			{
				text = oidTokenizer.NextToken();
				if (text.Length <= 18)
				{
					this.WriteField(bOut, long.Parse(text));
				}
				else
				{
					this.WriteField(bOut, new BigInteger(text));
				}
			}
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x00173BC0 File Offset: 0x00171DC0
		internal byte[] GetBody()
		{
			lock (this)
			{
				if (this.body == null)
				{
					MemoryStream memoryStream = new MemoryStream();
					this.DoOutput(memoryStream);
					this.body = memoryStream.ToArray();
				}
			}
			return this.body;
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x00173C1C File Offset: 0x00171E1C
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(6, this.GetBody());
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x00173C2B File Offset: 0x00171E2B
		protected override int Asn1GetHashCode()
		{
			return this.identifier.GetHashCode();
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x00173C38 File Offset: 0x00171E38
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerObjectIdentifier derObjectIdentifier = asn1Object as DerObjectIdentifier;
			return derObjectIdentifier != null && this.identifier.Equals(derObjectIdentifier.identifier);
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x001739EA File Offset: 0x00171BEA
		public override string ToString()
		{
			return this.identifier;
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x00173C64 File Offset: 0x00171E64
		private static bool IsValidBranchID(string branchID, int start)
		{
			bool flag = false;
			int num = branchID.Length;
			while (--num >= start)
			{
				char c = branchID[num];
				if ('0' <= c && c <= '9')
				{
					flag = true;
				}
				else
				{
					if (c != '.')
					{
						return false;
					}
					if (!flag)
					{
						return false;
					}
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x00173CAC File Offset: 0x00171EAC
		private static bool IsValidIdentifier(string identifier)
		{
			if (identifier.Length < 3 || identifier[1] != '.')
			{
				return false;
			}
			char c = identifier[0];
			return c >= '0' && c <= '2' && DerObjectIdentifier.IsValidBranchID(identifier, 2);
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x00173CEC File Offset: 0x00171EEC
		private static string MakeOidStringFromBytes(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			long num = 0L;
			BigInteger bigInteger = null;
			bool flag = true;
			for (int num2 = 0; num2 != bytes.Length; num2++)
			{
				int num3 = (int)bytes[num2];
				if (num <= 72057594037927808L)
				{
					num += (long)(num3 & 127);
					if ((num3 & 128) == 0)
					{
						if (flag)
						{
							if (num < 40L)
							{
								stringBuilder.Append('0');
							}
							else if (num < 80L)
							{
								stringBuilder.Append('1');
								num -= 40L;
							}
							else
							{
								stringBuilder.Append('2');
								num -= 80L;
							}
							flag = false;
						}
						stringBuilder.Append('.');
						stringBuilder.Append(num);
						num = 0L;
					}
					else
					{
						num <<= 7;
					}
				}
				else
				{
					if (bigInteger == null)
					{
						bigInteger = BigInteger.ValueOf(num);
					}
					bigInteger = bigInteger.Or(BigInteger.ValueOf((long)(num3 & 127)));
					if ((num3 & 128) == 0)
					{
						if (flag)
						{
							stringBuilder.Append('2');
							bigInteger = bigInteger.Subtract(BigInteger.ValueOf(80L));
							flag = false;
						}
						stringBuilder.Append('.');
						stringBuilder.Append(bigInteger);
						bigInteger = null;
						num = 0L;
					}
					else
					{
						bigInteger = bigInteger.ShiftLeft(7);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x00173E04 File Offset: 0x00172004
		internal static DerObjectIdentifier FromOctetString(byte[] enc)
		{
			int num = Arrays.GetHashCode(enc) & 1023;
			DerObjectIdentifier[] obj = DerObjectIdentifier.cache;
			DerObjectIdentifier result;
			lock (obj)
			{
				DerObjectIdentifier derObjectIdentifier = DerObjectIdentifier.cache[num];
				if (derObjectIdentifier != null && Arrays.AreEqual(enc, derObjectIdentifier.GetBody()))
				{
					result = derObjectIdentifier;
				}
				else
				{
					result = (DerObjectIdentifier.cache[num] = new DerObjectIdentifier(enc));
				}
			}
			return result;
		}

		// Token: 0x040025B8 RID: 9656
		private readonly string identifier;

		// Token: 0x040025B9 RID: 9657
		private byte[] body;

		// Token: 0x040025BA RID: 9658
		private const long LONG_LIMIT = 72057594037927808L;

		// Token: 0x040025BB RID: 9659
		private static readonly DerObjectIdentifier[] cache = new DerObjectIdentifier[1024];
	}
}
