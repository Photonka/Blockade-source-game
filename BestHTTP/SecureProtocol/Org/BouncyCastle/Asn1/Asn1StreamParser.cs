using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200061B RID: 1563
	public class Asn1StreamParser
	{
		// Token: 0x06003B32 RID: 15154 RVA: 0x00170A1F File Offset: 0x0016EC1F
		public Asn1StreamParser(Stream inStream) : this(inStream, Asn1InputStream.FindLimit(inStream))
		{
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x00170A2E File Offset: 0x0016EC2E
		public Asn1StreamParser(Stream inStream, int limit)
		{
			if (!inStream.CanRead)
			{
				throw new ArgumentException("Expected stream to be readable", "inStream");
			}
			this._in = inStream;
			this._limit = limit;
			this.tmpBuffers = new byte[16][];
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x00170A69 File Offset: 0x0016EC69
		public Asn1StreamParser(byte[] encoding) : this(new MemoryStream(encoding, false), encoding.Length)
		{
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x00170A7C File Offset: 0x0016EC7C
		internal IAsn1Convertible ReadIndef(int tagValue)
		{
			if (tagValue <= 8)
			{
				if (tagValue == 4)
				{
					return new BerOctetStringParser(this);
				}
				if (tagValue == 8)
				{
					return new DerExternalParser(this);
				}
			}
			else
			{
				if (tagValue == 16)
				{
					return new BerSequenceParser(this);
				}
				if (tagValue == 17)
				{
					return new BerSetParser(this);
				}
			}
			throw new Asn1Exception("unknown BER object encountered: 0x" + tagValue.ToString("X"));
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x00170ADC File Offset: 0x0016ECDC
		internal IAsn1Convertible ReadImplicit(bool constructed, int tag)
		{
			if (!(this._in is IndefiniteLengthInputStream))
			{
				if (constructed)
				{
					if (tag == 4)
					{
						return new BerOctetStringParser(this);
					}
					if (tag == 16)
					{
						return new DerSequenceParser(this);
					}
					if (tag == 17)
					{
						return new DerSetParser(this);
					}
				}
				else
				{
					if (tag == 4)
					{
						return new DerOctetStringParser((DefiniteLengthInputStream)this._in);
					}
					if (tag == 16)
					{
						throw new Asn1Exception("sets must use constructed encoding (see X.690 8.11.1/8.12.1)");
					}
					if (tag == 17)
					{
						throw new Asn1Exception("sequences must use constructed encoding (see X.690 8.9.1/8.10.1)");
					}
				}
				throw new Asn1Exception("implicit tagging not implemented");
			}
			if (!constructed)
			{
				throw new IOException("indefinite length primitive encoding encountered");
			}
			return this.ReadIndef(tag);
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x00170B74 File Offset: 0x0016ED74
		internal Asn1Object ReadTaggedObject(bool constructed, int tag)
		{
			if (!constructed)
			{
				DefiniteLengthInputStream definiteLengthInputStream = (DefiniteLengthInputStream)this._in;
				return new DerTaggedObject(false, tag, new DerOctetString(definiteLengthInputStream.ToArray()));
			}
			Asn1EncodableVector asn1EncodableVector = this.ReadVector();
			if (this._in is IndefiniteLengthInputStream)
			{
				if (asn1EncodableVector.Count != 1)
				{
					return new BerTaggedObject(false, tag, BerSequence.FromVector(asn1EncodableVector));
				}
				return new BerTaggedObject(true, tag, asn1EncodableVector[0]);
			}
			else
			{
				if (asn1EncodableVector.Count != 1)
				{
					return new DerTaggedObject(false, tag, DerSequence.FromVector(asn1EncodableVector));
				}
				return new DerTaggedObject(true, tag, asn1EncodableVector[0]);
			}
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x00170C04 File Offset: 0x0016EE04
		public virtual IAsn1Convertible ReadObject()
		{
			int num = this._in.ReadByte();
			if (num == -1)
			{
				return null;
			}
			this.Set00Check(false);
			int num2 = Asn1InputStream.ReadTagNumber(this._in, num);
			bool flag = (num & 32) != 0;
			int num3 = Asn1InputStream.ReadLength(this._in, this._limit);
			if (num3 < 0)
			{
				if (!flag)
				{
					throw new IOException("indefinite length primitive encoding encountered");
				}
				Asn1StreamParser asn1StreamParser = new Asn1StreamParser(new IndefiniteLengthInputStream(this._in, this._limit), this._limit);
				if ((num & 64) != 0)
				{
					return new BerApplicationSpecificParser(num2, asn1StreamParser);
				}
				if ((num & 128) != 0)
				{
					return new BerTaggedObjectParser(true, num2, asn1StreamParser);
				}
				return asn1StreamParser.ReadIndef(num2);
			}
			else
			{
				DefiniteLengthInputStream definiteLengthInputStream = new DefiniteLengthInputStream(this._in, num3);
				if ((num & 64) != 0)
				{
					return new DerApplicationSpecific(flag, num2, definiteLengthInputStream.ToArray());
				}
				if ((num & 128) != 0)
				{
					return new BerTaggedObjectParser(flag, num2, new Asn1StreamParser(definiteLengthInputStream));
				}
				if (flag)
				{
					if (num2 <= 8)
					{
						if (num2 == 4)
						{
							return new BerOctetStringParser(new Asn1StreamParser(definiteLengthInputStream));
						}
						if (num2 == 8)
						{
							return new DerExternalParser(new Asn1StreamParser(definiteLengthInputStream));
						}
					}
					else
					{
						if (num2 == 16)
						{
							return new DerSequenceParser(new Asn1StreamParser(definiteLengthInputStream));
						}
						if (num2 == 17)
						{
							return new DerSetParser(new Asn1StreamParser(definiteLengthInputStream));
						}
					}
					throw new IOException("unknown tag " + num2 + " encountered");
				}
				if (num2 == 4)
				{
					return new DerOctetStringParser(definiteLengthInputStream);
				}
				IAsn1Convertible result;
				try
				{
					result = Asn1InputStream.CreatePrimitiveDerObject(num2, definiteLengthInputStream, this.tmpBuffers);
				}
				catch (ArgumentException exception)
				{
					throw new Asn1Exception("corrupted stream detected", exception);
				}
				return result;
			}
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x00170D94 File Offset: 0x0016EF94
		private void Set00Check(bool enabled)
		{
			if (this._in is IndefiniteLengthInputStream)
			{
				((IndefiniteLengthInputStream)this._in).SetEofOn00(enabled);
			}
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x00170DB4 File Offset: 0x0016EFB4
		internal Asn1EncodableVector ReadVector()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			IAsn1Convertible asn1Convertible;
			while ((asn1Convertible = this.ReadObject()) != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Convertible.ToAsn1Object()
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x04002562 RID: 9570
		private readonly Stream _in;

		// Token: 0x04002563 RID: 9571
		private readonly int _limit;

		// Token: 0x04002564 RID: 9572
		private readonly byte[][] tmpBuffers;
	}
}
