using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000617 RID: 1559
	public abstract class Asn1Sequence : Asn1Object, IEnumerable
	{
		// Token: 0x06003B10 RID: 15120 RVA: 0x00170364 File Offset: 0x0016E564
		public static Asn1Sequence GetInstance(object obj)
		{
			if (obj == null || obj is Asn1Sequence)
			{
				return (Asn1Sequence)obj;
			}
			if (obj is Asn1SequenceParser)
			{
				return Asn1Sequence.GetInstance(((Asn1SequenceParser)obj).ToAsn1Object());
			}
			if (obj is byte[])
			{
				try
				{
					return Asn1Sequence.GetInstance(Asn1Object.FromByteArray((byte[])obj));
				}
				catch (IOException ex)
				{
					throw new ArgumentException("failed to construct sequence from byte[]: " + ex.Message);
				}
			}
			if (obj is Asn1Encodable)
			{
				Asn1Object asn1Object = ((Asn1Encodable)obj).ToAsn1Object();
				if (asn1Object is Asn1Sequence)
				{
					return (Asn1Sequence)asn1Object;
				}
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x00170420 File Offset: 0x0016E620
		public static Asn1Sequence GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			Asn1Object @object = obj.GetObject();
			if (explicitly)
			{
				if (!obj.IsExplicit())
				{
					throw new ArgumentException("object implicit - explicit expected.");
				}
				return (Asn1Sequence)@object;
			}
			else if (obj.IsExplicit())
			{
				if (obj is BerTaggedObject)
				{
					return new BerSequence(@object);
				}
				return new DerSequence(@object);
			}
			else
			{
				if (@object is Asn1Sequence)
				{
					return (Asn1Sequence)@object;
				}
				throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
			}
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x00170498 File Offset: 0x0016E698
		protected internal Asn1Sequence(int capacity)
		{
			this.seq = Platform.CreateArrayList(capacity);
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x001704AC File Offset: 0x0016E6AC
		public virtual IEnumerator GetEnumerator()
		{
			return this.seq.GetEnumerator();
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x001704B9 File Offset: 0x0016E6B9
		[Obsolete("Use GetEnumerator() instead")]
		public IEnumerator GetObjects()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x001704C1 File Offset: 0x0016E6C1
		public virtual Asn1SequenceParser Parser
		{
			get
			{
				return new Asn1Sequence.Asn1SequenceParserImpl(this);
			}
		}

		// Token: 0x170007AF RID: 1967
		public virtual Asn1Encodable this[int index]
		{
			get
			{
				return (Asn1Encodable)this.seq[index];
			}
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x001704DC File Offset: 0x0016E6DC
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetObjectAt(int index)
		{
			return this[index];
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06003B18 RID: 15128 RVA: 0x001704E5 File Offset: 0x0016E6E5
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x001704ED File Offset: 0x0016E6ED
		public virtual int Count
		{
			get
			{
				return this.seq.Count;
			}
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x001704FC File Offset: 0x0016E6FC
		protected override int Asn1GetHashCode()
		{
			int num = this.Count;
			foreach (object obj in this)
			{
				num *= 17;
				if (obj == null)
				{
					num ^= DerNull.Instance.GetHashCode();
				}
				else
				{
					num ^= obj.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x0017056C File Offset: 0x0016E76C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1Sequence asn1Sequence = asn1Object as Asn1Sequence;
			if (asn1Sequence == null)
			{
				return false;
			}
			if (this.Count != asn1Sequence.Count)
			{
				return false;
			}
			IEnumerator enumerator = this.GetEnumerator();
			IEnumerator enumerator2 = asn1Sequence.GetEnumerator();
			while (enumerator.MoveNext() && enumerator2.MoveNext())
			{
				object obj = this.GetCurrent(enumerator).ToAsn1Object();
				Asn1Object obj2 = this.GetCurrent(enumerator2).ToAsn1Object();
				if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x001705DC File Offset: 0x0016E7DC
		private Asn1Encodable GetCurrent(IEnumerator e)
		{
			Asn1Encodable asn1Encodable = (Asn1Encodable)e.Current;
			if (asn1Encodable == null)
			{
				return DerNull.Instance;
			}
			return asn1Encodable;
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x001705FF File Offset: 0x0016E7FF
		protected internal void AddObject(Asn1Encodable obj)
		{
			this.seq.Add(obj);
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x0017060E File Offset: 0x0016E80E
		public override string ToString()
		{
			return CollectionUtilities.ToString(this.seq);
		}

		// Token: 0x04002560 RID: 9568
		private readonly IList seq;

		// Token: 0x02000963 RID: 2403
		private class Asn1SequenceParserImpl : Asn1SequenceParser, IAsn1Convertible
		{
			// Token: 0x06004F08 RID: 20232 RVA: 0x001B77F0 File Offset: 0x001B59F0
			public Asn1SequenceParserImpl(Asn1Sequence outer)
			{
				this.outer = outer;
				this.max = outer.Count;
			}

			// Token: 0x06004F09 RID: 20233 RVA: 0x001B780C File Offset: 0x001B5A0C
			public IAsn1Convertible ReadObject()
			{
				if (this.index == this.max)
				{
					return null;
				}
				Asn1Sequence asn1Sequence = this.outer;
				int num = this.index;
				this.index = num + 1;
				Asn1Encodable asn1Encodable = asn1Sequence[num];
				if (asn1Encodable is Asn1Sequence)
				{
					return ((Asn1Sequence)asn1Encodable).Parser;
				}
				if (asn1Encodable is Asn1Set)
				{
					return ((Asn1Set)asn1Encodable).Parser;
				}
				return asn1Encodable;
			}

			// Token: 0x06004F0A RID: 20234 RVA: 0x001B786F File Offset: 0x001B5A6F
			public Asn1Object ToAsn1Object()
			{
				return this.outer;
			}

			// Token: 0x040035D7 RID: 13783
			private readonly Asn1Sequence outer;

			// Token: 0x040035D8 RID: 13784
			private readonly int max;

			// Token: 0x040035D9 RID: 13785
			private int index;
		}
	}
}
