using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Net;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000686 RID: 1670
	public class GeneralName : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003E1F RID: 15903 RVA: 0x00178C8F File Offset: 0x00176E8F
		public GeneralName(X509Name directoryName)
		{
			this.obj = directoryName;
			this.tag = 4;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00178CA5 File Offset: 0x00176EA5
		public GeneralName(Asn1Object name, int tag)
		{
			this.obj = name;
			this.tag = tag;
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x00178CBB File Offset: 0x00176EBB
		public GeneralName(int tag, Asn1Encodable name)
		{
			this.obj = name;
			this.tag = tag;
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x00178CD4 File Offset: 0x00176ED4
		public GeneralName(int tag, string name)
		{
			this.tag = tag;
			if (tag == 1 || tag == 2 || tag == 6)
			{
				this.obj = new DerIA5String(name);
				return;
			}
			if (tag == 8)
			{
				this.obj = new DerObjectIdentifier(name);
				return;
			}
			if (tag == 4)
			{
				this.obj = new X509Name(name);
				return;
			}
			if (tag != 7)
			{
				throw new ArgumentException("can't process string for tag: " + tag, "tag");
			}
			byte[] array = this.toGeneralNameEncoding(name);
			if (array == null)
			{
				throw new ArgumentException("IP Address is invalid", "name");
			}
			this.obj = new DerOctetString(array);
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x00178D70 File Offset: 0x00176F70
		public static GeneralName GetInstance(object obj)
		{
			if (obj == null || obj is GeneralName)
			{
				return (GeneralName)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				int tagNo = asn1TaggedObject.TagNo;
				switch (tagNo)
				{
				case 0:
					return new GeneralName(tagNo, Asn1Sequence.GetInstance(asn1TaggedObject, false));
				case 1:
					return new GeneralName(tagNo, DerIA5String.GetInstance(asn1TaggedObject, false));
				case 2:
					return new GeneralName(tagNo, DerIA5String.GetInstance(asn1TaggedObject, false));
				case 3:
					throw new ArgumentException("unknown tag: " + tagNo);
				case 4:
					return new GeneralName(tagNo, X509Name.GetInstance(asn1TaggedObject, true));
				case 5:
					return new GeneralName(tagNo, Asn1Sequence.GetInstance(asn1TaggedObject, false));
				case 6:
					return new GeneralName(tagNo, DerIA5String.GetInstance(asn1TaggedObject, false));
				case 7:
					return new GeneralName(tagNo, Asn1OctetString.GetInstance(asn1TaggedObject, false));
				case 8:
					return new GeneralName(tagNo, DerObjectIdentifier.GetInstance(asn1TaggedObject, false));
				}
			}
			if (obj is byte[])
			{
				try
				{
					return GeneralName.GetInstance(Asn1Object.FromByteArray((byte[])obj));
				}
				catch (IOException)
				{
					throw new ArgumentException("unable to parse encoded general name");
				}
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x00178EB0 File Offset: 0x001770B0
		public static GeneralName GetInstance(Asn1TaggedObject tagObj, bool explicitly)
		{
			return GeneralName.GetInstance(Asn1TaggedObject.GetInstance(tagObj, true));
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06003E25 RID: 15909 RVA: 0x00178EBE File Offset: 0x001770BE
		public int TagNo
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x00178EC6 File Offset: 0x001770C6
		public Asn1Encodable Name
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x00178ED0 File Offset: 0x001770D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.tag);
			stringBuilder.Append(": ");
			switch (this.tag)
			{
			case 1:
			case 2:
			case 6:
				stringBuilder.Append(DerIA5String.GetInstance(this.obj).GetString());
				goto IL_8C;
			case 4:
				stringBuilder.Append(X509Name.GetInstance(this.obj).ToString());
				goto IL_8C;
			}
			stringBuilder.Append(this.obj.ToString());
			IL_8C:
			return stringBuilder.ToString();
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x00178F70 File Offset: 0x00177170
		private byte[] toGeneralNameEncoding(string ip)
		{
			if (BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv6WithNetmask(ip) || BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv6(ip))
			{
				int num = ip.IndexOf('/');
				if (num < 0)
				{
					byte[] array = new byte[16];
					int[] parsedIp = this.parseIPv6(ip);
					this.copyInts(parsedIp, array, 0);
					return array;
				}
				byte[] array2 = new byte[32];
				int[] parsedIp2 = this.parseIPv6(ip.Substring(0, num));
				this.copyInts(parsedIp2, array2, 0);
				string text = ip.Substring(num + 1);
				if (text.IndexOf(':') > 0)
				{
					parsedIp2 = this.parseIPv6(text);
				}
				else
				{
					parsedIp2 = this.parseMask(text);
				}
				this.copyInts(parsedIp2, array2, 16);
				return array2;
			}
			else
			{
				if (!BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv4WithNetmask(ip) && !BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv4(ip))
				{
					return null;
				}
				int num2 = ip.IndexOf('/');
				if (num2 < 0)
				{
					byte[] array3 = new byte[4];
					this.parseIPv4(ip, array3, 0);
					return array3;
				}
				byte[] array4 = new byte[8];
				this.parseIPv4(ip.Substring(0, num2), array4, 0);
				string text2 = ip.Substring(num2 + 1);
				if (text2.IndexOf('.') > 0)
				{
					this.parseIPv4(text2, array4, 4);
				}
				else
				{
					this.parseIPv4Mask(text2, array4, 4);
				}
				return array4;
			}
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x00179098 File Offset: 0x00177298
		private void parseIPv4Mask(string mask, byte[] addr, int offset)
		{
			int num = int.Parse(mask);
			for (int num2 = 0; num2 != num; num2++)
			{
				int num3 = num2 / 8 + offset;
				addr[num3] |= (byte)(1 << num2 % 8);
			}
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x001790D4 File Offset: 0x001772D4
		private void parseIPv4(string ip, byte[] addr, int offset)
		{
			foreach (string s in ip.Split(new char[]
			{
				'.',
				'/'
			}))
			{
				addr[offset++] = (byte)int.Parse(s);
			}
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x0017911C File Offset: 0x0017731C
		private int[] parseMask(string mask)
		{
			int[] array = new int[8];
			int num = int.Parse(mask);
			for (int num2 = 0; num2 != num; num2++)
			{
				array[num2 / 16] |= 1 << num2 % 16;
			}
			return array;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x0017915C File Offset: 0x0017735C
		private void copyInts(int[] parsedIp, byte[] addr, int offSet)
		{
			for (int num = 0; num != parsedIp.Length; num++)
			{
				addr[num * 2 + offSet] = (byte)(parsedIp[num] >> 8);
				addr[num * 2 + 1 + offSet] = (byte)parsedIp[num];
			}
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x00179194 File Offset: 0x00177394
		private int[] parseIPv6(string ip)
		{
			if (Platform.StartsWith(ip, "::"))
			{
				ip = ip.Substring(1);
			}
			else if (Platform.EndsWith(ip, "::"))
			{
				ip = ip.Substring(0, ip.Length - 1);
			}
			IEnumerator enumerator = ip.Split(new char[]
			{
				':'
			}).GetEnumerator();
			int num = 0;
			int[] array = new int[8];
			int num2 = -1;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = (string)obj;
				if (text.Length == 0)
				{
					num2 = num;
					array[num++] = 0;
				}
				else if (text.IndexOf('.') < 0)
				{
					array[num++] = int.Parse(text, NumberStyles.AllowHexSpecifier);
				}
				else
				{
					string[] array2 = text.Split(new char[]
					{
						'.'
					});
					array[num++] = (int.Parse(array2[0]) << 8 | int.Parse(array2[1]));
					array[num++] = (int.Parse(array2[2]) << 8 | int.Parse(array2[3]));
				}
			}
			if (num != array.Length)
			{
				Array.Copy(array, num2, array, array.Length - (num - num2), num - num2);
				for (int num3 = num2; num3 != array.Length - (num - num2); num3++)
				{
					array[num3] = 0;
				}
			}
			return array;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x001792CC File Offset: 0x001774CC
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(this.tag == 4, this.tag, this.obj);
		}

		// Token: 0x0400267C RID: 9852
		public const int OtherName = 0;

		// Token: 0x0400267D RID: 9853
		public const int Rfc822Name = 1;

		// Token: 0x0400267E RID: 9854
		public const int DnsName = 2;

		// Token: 0x0400267F RID: 9855
		public const int X400Address = 3;

		// Token: 0x04002680 RID: 9856
		public const int DirectoryName = 4;

		// Token: 0x04002681 RID: 9857
		public const int EdiPartyName = 5;

		// Token: 0x04002682 RID: 9858
		public const int UniformResourceIdentifier = 6;

		// Token: 0x04002683 RID: 9859
		public const int IPAddress = 7;

		// Token: 0x04002684 RID: 9860
		public const int RegisteredID = 8;

		// Token: 0x04002685 RID: 9861
		internal readonly Asn1Encodable obj;

		// Token: 0x04002686 RID: 9862
		internal readonly int tag;
	}
}
