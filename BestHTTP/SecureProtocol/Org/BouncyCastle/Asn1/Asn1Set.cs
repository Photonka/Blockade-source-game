using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000619 RID: 1561
	public abstract class Asn1Set : Asn1Object, IEnumerable
	{
		// Token: 0x06003B20 RID: 15136 RVA: 0x0017061C File Offset: 0x0016E81C
		public static Asn1Set GetInstance(object obj)
		{
			if (obj == null || obj is Asn1Set)
			{
				return (Asn1Set)obj;
			}
			if (obj is Asn1SetParser)
			{
				return Asn1Set.GetInstance(((Asn1SetParser)obj).ToAsn1Object());
			}
			if (obj is byte[])
			{
				try
				{
					return Asn1Set.GetInstance(Asn1Object.FromByteArray((byte[])obj));
				}
				catch (IOException ex)
				{
					throw new ArgumentException("failed to construct set from byte[]: " + ex.Message);
				}
			}
			if (obj is Asn1Encodable)
			{
				Asn1Object asn1Object = ((Asn1Encodable)obj).ToAsn1Object();
				if (asn1Object is Asn1Set)
				{
					return (Asn1Set)asn1Object;
				}
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x001706D8 File Offset: 0x0016E8D8
		public static Asn1Set GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			Asn1Object @object = obj.GetObject();
			if (explicitly)
			{
				if (!obj.IsExplicit())
				{
					throw new ArgumentException("object implicit - explicit expected.");
				}
				return (Asn1Set)@object;
			}
			else
			{
				if (obj.IsExplicit())
				{
					return new DerSet(@object);
				}
				if (@object is Asn1Set)
				{
					return (Asn1Set)@object;
				}
				if (@object is Asn1Sequence)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
					foreach (object obj2 in ((Asn1Sequence)@object))
					{
						Asn1Encodable asn1Encodable = (Asn1Encodable)obj2;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							asn1Encodable
						});
					}
					return new DerSet(asn1EncodableVector, false);
				}
				throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
			}
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x001707B4 File Offset: 0x0016E9B4
		protected internal Asn1Set(int capacity)
		{
			this._set = Platform.CreateArrayList(capacity);
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x001707C8 File Offset: 0x0016E9C8
		public virtual IEnumerator GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x001707D5 File Offset: 0x0016E9D5
		[Obsolete("Use GetEnumerator() instead")]
		public IEnumerator GetObjects()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170007B2 RID: 1970
		public virtual Asn1Encodable this[int index]
		{
			get
			{
				return (Asn1Encodable)this._set[index];
			}
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x001707F0 File Offset: 0x0016E9F0
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetObjectAt(int index)
		{
			return this[index];
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x001707F9 File Offset: 0x0016E9F9
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x00170801 File Offset: 0x0016EA01
		public virtual int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x00170810 File Offset: 0x0016EA10
		public virtual Asn1Encodable[] ToArray()
		{
			Asn1Encodable[] array = new Asn1Encodable[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = this[i];
			}
			return array;
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x00170845 File Offset: 0x0016EA45
		public Asn1SetParser Parser
		{
			get
			{
				return new Asn1Set.Asn1SetParserImpl(this);
			}
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x00170850 File Offset: 0x0016EA50
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

		// Token: 0x06003B2C RID: 15148 RVA: 0x001708C0 File Offset: 0x0016EAC0
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1Set asn1Set = asn1Object as Asn1Set;
			if (asn1Set == null)
			{
				return false;
			}
			if (this.Count != asn1Set.Count)
			{
				return false;
			}
			IEnumerator enumerator = this.GetEnumerator();
			IEnumerator enumerator2 = asn1Set.GetEnumerator();
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

		// Token: 0x06003B2D RID: 15149 RVA: 0x00170930 File Offset: 0x0016EB30
		private Asn1Encodable GetCurrent(IEnumerator e)
		{
			Asn1Encodable asn1Encodable = (Asn1Encodable)e.Current;
			if (asn1Encodable == null)
			{
				return DerNull.Instance;
			}
			return asn1Encodable;
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x00170954 File Offset: 0x0016EB54
		protected internal void Sort()
		{
			if (this._set.Count < 2)
			{
				return;
			}
			Asn1Encodable[] array = new Asn1Encodable[this._set.Count];
			byte[][] array2 = new byte[this._set.Count][];
			for (int i = 0; i < this._set.Count; i++)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)this._set[i];
				array[i] = asn1Encodable;
				array2[i] = asn1Encodable.GetEncoded("DER");
			}
			Array.Sort(array2, array, new Asn1Set.DerComparer());
			for (int j = 0; j < this._set.Count; j++)
			{
				this._set[j] = array[j];
			}
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x00170A03 File Offset: 0x0016EC03
		protected internal void AddObject(Asn1Encodable obj)
		{
			this._set.Add(obj);
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x00170A12 File Offset: 0x0016EC12
		public override string ToString()
		{
			return CollectionUtilities.ToString(this._set);
		}

		// Token: 0x04002561 RID: 9569
		private readonly IList _set;

		// Token: 0x02000964 RID: 2404
		private class Asn1SetParserImpl : Asn1SetParser, IAsn1Convertible
		{
			// Token: 0x06004F0B RID: 20235 RVA: 0x001B7877 File Offset: 0x001B5A77
			public Asn1SetParserImpl(Asn1Set outer)
			{
				this.outer = outer;
				this.max = outer.Count;
			}

			// Token: 0x06004F0C RID: 20236 RVA: 0x001B7894 File Offset: 0x001B5A94
			public IAsn1Convertible ReadObject()
			{
				if (this.index == this.max)
				{
					return null;
				}
				Asn1Set asn1Set = this.outer;
				int num = this.index;
				this.index = num + 1;
				Asn1Encodable asn1Encodable = asn1Set[num];
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

			// Token: 0x06004F0D RID: 20237 RVA: 0x001B78F7 File Offset: 0x001B5AF7
			public virtual Asn1Object ToAsn1Object()
			{
				return this.outer;
			}

			// Token: 0x040035DA RID: 13786
			private readonly Asn1Set outer;

			// Token: 0x040035DB RID: 13787
			private readonly int max;

			// Token: 0x040035DC RID: 13788
			private int index;
		}

		// Token: 0x02000965 RID: 2405
		private class DerComparer : IComparer
		{
			// Token: 0x06004F0E RID: 20238 RVA: 0x001B7900 File Offset: 0x001B5B00
			public int Compare(object x, object y)
			{
				byte[] array = (byte[])x;
				byte[] array2 = (byte[])y;
				int num = Math.Min(array.Length, array2.Length);
				int num2 = 0;
				while (num2 != num)
				{
					byte b = array[num2];
					byte b2 = array2[num2];
					if (b != b2)
					{
						if (b >= b2)
						{
							return 1;
						}
						return -1;
					}
					else
					{
						num2++;
					}
				}
				if (array.Length > array2.Length)
				{
					if (!this.AllZeroesFrom(array, num))
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (array.Length >= array2.Length)
					{
						return 0;
					}
					if (!this.AllZeroesFrom(array2, num))
					{
						return -1;
					}
					return 0;
				}
			}

			// Token: 0x06004F0F RID: 20239 RVA: 0x001B797A File Offset: 0x001B5B7A
			private bool AllZeroesFrom(byte[] bs, int pos)
			{
				while (pos < bs.Length)
				{
					if (bs[pos++] != 0)
					{
						return false;
					}
				}
				return true;
			}
		}
	}
}
