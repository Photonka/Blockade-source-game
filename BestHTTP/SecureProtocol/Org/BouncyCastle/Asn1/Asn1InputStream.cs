using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000610 RID: 1552
	public class Asn1InputStream : FilterStream
	{
		// Token: 0x06003AE7 RID: 15079 RVA: 0x0016FB6C File Offset: 0x0016DD6C
		internal static int FindLimit(Stream input)
		{
			if (input is LimitedInputStream)
			{
				return ((LimitedInputStream)input).GetRemaining();
			}
			if (input is MemoryStream)
			{
				MemoryStream memoryStream = (MemoryStream)input;
				return (int)(memoryStream.Length - memoryStream.Position);
			}
			return int.MaxValue;
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x0016FBB0 File Offset: 0x0016DDB0
		public Asn1InputStream(Stream inputStream) : this(inputStream, Asn1InputStream.FindLimit(inputStream))
		{
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x0016FBBF File Offset: 0x0016DDBF
		public Asn1InputStream(Stream inputStream, int limit) : base(inputStream)
		{
			this.limit = limit;
			this.tmpBuffers = new byte[16][];
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x0016FBDC File Offset: 0x0016DDDC
		public Asn1InputStream(byte[] input) : this(new MemoryStream(input, false), input.Length)
		{
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x0016FBF0 File Offset: 0x0016DDF0
		private Asn1Object BuildObject(int tag, int tagNo, int length)
		{
			bool flag = (tag & 32) != 0;
			DefiniteLengthInputStream definiteLengthInputStream = new DefiniteLengthInputStream(this.s, length);
			if ((tag & 64) != 0)
			{
				return new DerApplicationSpecific(flag, tagNo, definiteLengthInputStream.ToArray());
			}
			if ((tag & 128) != 0)
			{
				return new Asn1StreamParser(definiteLengthInputStream).ReadTaggedObject(flag, tagNo);
			}
			if (flag)
			{
				if (tagNo <= 8)
				{
					if (tagNo == 4)
					{
						return new BerOctetString(this.BuildDerEncodableVector(definiteLengthInputStream));
					}
					if (tagNo == 8)
					{
						return new DerExternal(this.BuildDerEncodableVector(definiteLengthInputStream));
					}
				}
				else
				{
					if (tagNo == 16)
					{
						return this.CreateDerSequence(definiteLengthInputStream);
					}
					if (tagNo == 17)
					{
						return this.CreateDerSet(definiteLengthInputStream);
					}
				}
				throw new IOException("unknown tag " + tagNo + " encountered");
			}
			return Asn1InputStream.CreatePrimitiveDerObject(tagNo, definiteLengthInputStream, this.tmpBuffers);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x0016FCAC File Offset: 0x0016DEAC
		internal Asn1EncodableVector BuildEncodableVector()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			Asn1Object asn1Object;
			while ((asn1Object = this.ReadObject()) != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Object
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x0016FCE1 File Offset: 0x0016DEE1
		internal virtual Asn1EncodableVector BuildDerEncodableVector(DefiniteLengthInputStream dIn)
		{
			return new Asn1InputStream(dIn).BuildEncodableVector();
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x0016FCEE File Offset: 0x0016DEEE
		internal virtual DerSequence CreateDerSequence(DefiniteLengthInputStream dIn)
		{
			return DerSequence.FromVector(this.BuildDerEncodableVector(dIn));
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x0016FCFC File Offset: 0x0016DEFC
		internal virtual DerSet CreateDerSet(DefiniteLengthInputStream dIn)
		{
			return DerSet.FromVector(this.BuildDerEncodableVector(dIn), false);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x0016FD0C File Offset: 0x0016DF0C
		public Asn1Object ReadObject()
		{
			int num = this.ReadByte();
			if (num <= 0)
			{
				if (num == 0)
				{
					throw new IOException("unexpected end-of-contents marker");
				}
				return null;
			}
			else
			{
				int num2 = Asn1InputStream.ReadTagNumber(this.s, num);
				bool flag = (num & 32) != 0;
				int num3 = Asn1InputStream.ReadLength(this.s, this.limit);
				if (num3 >= 0)
				{
					Asn1Object result;
					try
					{
						result = this.BuildObject(num, num2, num3);
					}
					catch (ArgumentException exception)
					{
						throw new Asn1Exception("corrupted stream detected", exception);
					}
					return result;
				}
				if (!flag)
				{
					throw new IOException("indefinite length primitive encoding encountered");
				}
				Asn1StreamParser parser = new Asn1StreamParser(new IndefiniteLengthInputStream(this.s, this.limit), this.limit);
				if ((num & 64) != 0)
				{
					return new BerApplicationSpecificParser(num2, parser).ToAsn1Object();
				}
				if ((num & 128) != 0)
				{
					return new BerTaggedObjectParser(true, num2, parser).ToAsn1Object();
				}
				if (num2 <= 8)
				{
					if (num2 == 4)
					{
						return new BerOctetStringParser(parser).ToAsn1Object();
					}
					if (num2 == 8)
					{
						return new DerExternalParser(parser).ToAsn1Object();
					}
				}
				else
				{
					if (num2 == 16)
					{
						return new BerSequenceParser(parser).ToAsn1Object();
					}
					if (num2 == 17)
					{
						return new BerSetParser(parser).ToAsn1Object();
					}
				}
				throw new IOException("unknown BER object encountered");
			}
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x0016FE44 File Offset: 0x0016E044
		internal static int ReadTagNumber(Stream s, int tag)
		{
			int num = tag & 31;
			if (num == 31)
			{
				num = 0;
				int num2 = s.ReadByte();
				if ((num2 & 127) == 0)
				{
					throw new IOException("Corrupted stream - invalid high tag number found");
				}
				while (num2 >= 0 && (num2 & 128) != 0)
				{
					num |= (num2 & 127);
					num <<= 7;
					num2 = s.ReadByte();
				}
				if (num2 < 0)
				{
					throw new EndOfStreamException("EOF found inside tag value.");
				}
				num |= (num2 & 127);
			}
			return num;
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x0016FEAC File Offset: 0x0016E0AC
		internal static int ReadLength(Stream s, int limit)
		{
			int num = s.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException("EOF found when length expected");
			}
			if (num == 128)
			{
				return -1;
			}
			if (num > 127)
			{
				int num2 = num & 127;
				if (num2 > 4)
				{
					throw new IOException("DER length more than 4 bytes: " + num2);
				}
				num = 0;
				for (int i = 0; i < num2; i++)
				{
					int num3 = s.ReadByte();
					if (num3 < 0)
					{
						throw new EndOfStreamException("EOF found reading length");
					}
					num = (num << 8) + num3;
				}
				if (num < 0)
				{
					throw new IOException("Corrupted stream - negative length found");
				}
				if (num >= limit)
				{
					throw new IOException("Corrupted stream - out of bounds length found");
				}
			}
			return num;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x0016FF48 File Offset: 0x0016E148
		internal static byte[] GetBuffer(DefiniteLengthInputStream defIn, byte[][] tmpBuffers)
		{
			int remaining = defIn.GetRemaining();
			if (remaining >= tmpBuffers.Length)
			{
				return defIn.ToArray();
			}
			byte[] array = tmpBuffers[remaining];
			if (array == null)
			{
				array = (tmpBuffers[remaining] = new byte[remaining]);
			}
			defIn.ReadAllIntoByteArray(array);
			return array;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x0016FF88 File Offset: 0x0016E188
		internal static Asn1Object CreatePrimitiveDerObject(int tagNo, DefiniteLengthInputStream defIn, byte[][] tmpBuffers)
		{
			if (tagNo == 1)
			{
				return DerBoolean.FromOctetString(Asn1InputStream.GetBuffer(defIn, tmpBuffers));
			}
			if (tagNo == 6)
			{
				return DerObjectIdentifier.FromOctetString(Asn1InputStream.GetBuffer(defIn, tmpBuffers));
			}
			if (tagNo != 10)
			{
				byte[] array = defIn.ToArray();
				switch (tagNo)
				{
				case 2:
					return new DerInteger(array);
				case 3:
					return DerBitString.FromAsn1Octets(array);
				case 4:
					return new DerOctetString(array);
				case 5:
					return DerNull.Instance;
				case 12:
					return new DerUtf8String(array);
				case 18:
					return new DerNumericString(array);
				case 19:
					return new DerPrintableString(array);
				case 20:
					return new DerT61String(array);
				case 21:
					return new DerVideotexString(array);
				case 22:
					return new DerIA5String(array);
				case 23:
					return new DerUtcTime(array);
				case 24:
					return new DerGeneralizedTime(array);
				case 25:
					return new DerGraphicString(array);
				case 26:
					return new DerVisibleString(array);
				case 27:
					return new DerGeneralString(array);
				case 28:
					return new DerUniversalString(array);
				case 30:
					return new DerBmpString(array);
				}
				throw new IOException("unknown tag " + tagNo + " encountered");
			}
			return DerEnumerated.FromOctetString(Asn1InputStream.GetBuffer(defIn, tmpBuffers));
		}

		// Token: 0x0400255D RID: 9565
		private readonly int limit;

		// Token: 0x0400255E RID: 9566
		private readonly byte[][] tmpBuffers;
	}
}
