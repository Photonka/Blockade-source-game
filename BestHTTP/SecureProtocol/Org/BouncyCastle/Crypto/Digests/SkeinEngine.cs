using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020005A9 RID: 1449
	public class SkeinEngine : IMemoable
	{
		// Token: 0x06003802 RID: 14338 RVA: 0x0016339C File Offset: 0x0016159C
		static SkeinEngine()
		{
			SkeinEngine.InitialState(256, 128, new ulong[]
			{
				16217771249220022880UL,
				9817190399063458076UL,
				1155188648486244218UL,
				14769517481627992514UL
			});
			SkeinEngine.InitialState(256, 160, new ulong[]
			{
				1450197650740764312UL,
				3081844928540042640UL,
				15310647011875280446UL,
				3301952811952417661UL
			});
			SkeinEngine.InitialState(256, 224, new ulong[]
			{
				14270089230798940683UL,
				9758551101254474012UL,
				11082101768697755780UL,
				4056579644589979102UL
			});
			SkeinEngine.InitialState(256, 256, new ulong[]
			{
				18202890402666165321UL,
				3443677322885453875UL,
				12915131351309911055UL,
				7662005193972177513UL
			});
			SkeinEngine.InitialState(512, 128, new ulong[]
			{
				12158729379475595090UL,
				2204638249859346602UL,
				3502419045458743507UL,
				13617680570268287068UL,
				983504137758028059UL,
				1880512238245786339UL,
				11730851291495443074UL,
				7602827311880509485UL
			});
			SkeinEngine.InitialState(512, 160, new ulong[]
			{
				2934123928682216849UL,
				14047033351726823311UL,
				1684584802963255058UL,
				5744138295201861711UL,
				2444857010922934358UL,
				15638910433986703544UL,
				13325156239043941114UL,
				118355523173251694UL
			});
			SkeinEngine.InitialState(512, 224, new ulong[]
			{
				14758403053642543652UL,
				14674518637417806319UL,
				10145881904771976036UL,
				4146387520469897396UL,
				1106145742801415120UL,
				7455425944880474941UL,
				11095680972475339753UL,
				11397762726744039159UL
			});
			SkeinEngine.InitialState(512, 384, new ulong[]
			{
				11814849197074935647UL,
				12753905853581818532UL,
				11346781217370868990UL,
				15535391162178797018UL,
				2000907093792408677UL,
				9140007292425499655UL,
				6093301768906360022UL,
				2769176472213098488UL
			});
			SkeinEngine.InitialState(512, 512, new ulong[]
			{
				5261240102383538638UL,
				978932832955457283UL,
				10363226125605772238UL,
				11107378794354519217UL,
				6752626034097301424UL,
				16915020251879818228UL,
				11029617608758768931UL,
				12544957130904423475UL
			});
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x001634D3 File Offset: 0x001616D3
		private static void InitialState(int blockSize, int outputSize, ulong[] state)
		{
			SkeinEngine.INITIAL_STATES.Add(SkeinEngine.VariantIdentifier(blockSize / 8, outputSize / 8), state);
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x001634F0 File Offset: 0x001616F0
		private static int VariantIdentifier(int blockSizeBytes, int outputSizeBytes)
		{
			return outputSizeBytes << 16 | blockSizeBytes;
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x001634F8 File Offset: 0x001616F8
		public SkeinEngine(int blockSizeBits, int outputSizeBits)
		{
			if (outputSizeBits % 8 != 0)
			{
				throw new ArgumentException("Output size must be a multiple of 8 bits. :" + outputSizeBits);
			}
			this.outputSizeBytes = outputSizeBits / 8;
			this.threefish = new ThreefishEngine(blockSizeBits);
			this.ubi = new SkeinEngine.UBI(this, this.threefish.GetBlockSize());
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x0016355E File Offset: 0x0016175E
		public SkeinEngine(SkeinEngine engine) : this(engine.BlockSize * 8, engine.OutputSize * 8)
		{
			this.CopyIn(engine);
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x00163580 File Offset: 0x00161780
		private void CopyIn(SkeinEngine engine)
		{
			this.ubi.Reset(engine.ubi);
			this.chain = Arrays.Clone(engine.chain, this.chain);
			this.initialState = Arrays.Clone(engine.initialState, this.initialState);
			this.key = Arrays.Clone(engine.key, this.key);
			this.preMessageParameters = SkeinEngine.Clone(engine.preMessageParameters, this.preMessageParameters);
			this.postMessageParameters = SkeinEngine.Clone(engine.postMessageParameters, this.postMessageParameters);
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x00163611 File Offset: 0x00161811
		private static SkeinEngine.Parameter[] Clone(SkeinEngine.Parameter[] data, SkeinEngine.Parameter[] existing)
		{
			if (data == null)
			{
				return null;
			}
			if (existing == null || existing.Length != data.Length)
			{
				existing = new SkeinEngine.Parameter[data.Length];
			}
			Array.Copy(data, 0, existing, 0, existing.Length);
			return existing;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0016363A File Offset: 0x0016183A
		public IMemoable Copy()
		{
			return new SkeinEngine(this);
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x00163644 File Offset: 0x00161844
		public void Reset(IMemoable other)
		{
			SkeinEngine skeinEngine = (SkeinEngine)other;
			if (this.BlockSize != skeinEngine.BlockSize || this.outputSizeBytes != skeinEngine.outputSizeBytes)
			{
				throw new MemoableResetException("Incompatible parameters in provided SkeinEngine.");
			}
			this.CopyIn(skeinEngine);
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x00163686 File Offset: 0x00161886
		public int OutputSize
		{
			get
			{
				return this.outputSizeBytes;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x0016368E File Offset: 0x0016188E
		public int BlockSize
		{
			get
			{
				return this.threefish.GetBlockSize();
			}
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x0016369C File Offset: 0x0016189C
		public void Init(SkeinParameters parameters)
		{
			this.chain = null;
			this.key = null;
			this.preMessageParameters = null;
			this.postMessageParameters = null;
			if (parameters != null)
			{
				if (parameters.GetKey().Length < 16)
				{
					throw new ArgumentException("Skein key must be at least 128 bits.");
				}
				this.InitParams(parameters.GetParameters());
			}
			this.CreateInitialState();
			this.UbiInit(48);
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x001636FC File Offset: 0x001618FC
		private void InitParams(IDictionary parameters)
		{
			IEnumerator enumerator = parameters.Keys.GetEnumerator();
			IList list = Platform.CreateArrayList();
			IList list2 = Platform.CreateArrayList();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				int num = (int)obj;
				byte[] value = (byte[])parameters[num];
				if (num == 0)
				{
					this.key = value;
				}
				else if (num < 48)
				{
					list.Add(new SkeinEngine.Parameter(num, value));
				}
				else
				{
					list2.Add(new SkeinEngine.Parameter(num, value));
				}
			}
			this.preMessageParameters = new SkeinEngine.Parameter[list.Count];
			list.CopyTo(this.preMessageParameters, 0);
			Array.Sort<SkeinEngine.Parameter>(this.preMessageParameters);
			this.postMessageParameters = new SkeinEngine.Parameter[list2.Count];
			list2.CopyTo(this.postMessageParameters, 0);
			Array.Sort<SkeinEngine.Parameter>(this.postMessageParameters);
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x001637D0 File Offset: 0x001619D0
		private void CreateInitialState()
		{
			ulong[] array = (ulong[])SkeinEngine.INITIAL_STATES[SkeinEngine.VariantIdentifier(this.BlockSize, this.OutputSize)];
			if (this.key == null && array != null)
			{
				this.chain = Arrays.Clone(array);
			}
			else
			{
				this.chain = new ulong[this.BlockSize / 8];
				if (this.key != null)
				{
					this.UbiComplete(0, this.key);
				}
				this.UbiComplete(4, new SkeinEngine.Configuration((long)(this.outputSizeBytes * 8)).Bytes);
			}
			if (this.preMessageParameters != null)
			{
				for (int i = 0; i < this.preMessageParameters.Length; i++)
				{
					SkeinEngine.Parameter parameter = this.preMessageParameters[i];
					this.UbiComplete(parameter.Type, parameter.Value);
				}
			}
			this.initialState = Arrays.Clone(this.chain);
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x001638A5 File Offset: 0x00161AA5
		public void Reset()
		{
			Array.Copy(this.initialState, 0, this.chain, 0, this.chain.Length);
			this.UbiInit(48);
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x001638CA File Offset: 0x00161ACA
		private void UbiComplete(int type, byte[] value)
		{
			this.UbiInit(type);
			this.ubi.Update(value, 0, value.Length, this.chain);
			this.UbiFinal();
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x001638EF File Offset: 0x00161AEF
		private void UbiInit(int type)
		{
			this.ubi.Reset(type);
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x001638FD File Offset: 0x00161AFD
		private void UbiFinal()
		{
			this.ubi.DoFinal(this.chain);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x00163910 File Offset: 0x00161B10
		private void CheckInitialised()
		{
			if (this.ubi == null)
			{
				throw new ArgumentException("Skein engine is not initialised.");
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x00163925 File Offset: 0x00161B25
		public void Update(byte inByte)
		{
			this.singleByte[0] = inByte;
			this.Update(this.singleByte, 0, 1);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x0016393E File Offset: 0x00161B3E
		public void Update(byte[] inBytes, int inOff, int len)
		{
			this.CheckInitialised();
			this.ubi.Update(inBytes, inOff, len, this.chain);
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x0016395C File Offset: 0x00161B5C
		public int DoFinal(byte[] outBytes, int outOff)
		{
			this.CheckInitialised();
			if (outBytes.Length < outOff + this.outputSizeBytes)
			{
				throw new DataLengthException("Output buffer is too short to hold output");
			}
			this.UbiFinal();
			if (this.postMessageParameters != null)
			{
				for (int i = 0; i < this.postMessageParameters.Length; i++)
				{
					SkeinEngine.Parameter parameter = this.postMessageParameters[i];
					this.UbiComplete(parameter.Type, parameter.Value);
				}
			}
			int blockSize = this.BlockSize;
			int num = (this.outputSizeBytes + blockSize - 1) / blockSize;
			for (int j = 0; j < num; j++)
			{
				int outputBytes = Math.Min(blockSize, this.outputSizeBytes - j * blockSize);
				this.Output((ulong)((long)j), outBytes, outOff + j * blockSize, outputBytes);
			}
			this.Reset();
			return this.outputSizeBytes;
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x00163A18 File Offset: 0x00161C18
		private void Output(ulong outputSequence, byte[] outBytes, int outOff, int outputBytes)
		{
			byte[] array = new byte[8];
			ThreefishEngine.WordToBytes(outputSequence, array, 0);
			ulong[] array2 = new ulong[this.chain.Length];
			this.UbiInit(63);
			this.ubi.Update(array, 0, array.Length, array2);
			this.ubi.DoFinal(array2);
			int num = (outputBytes + 8 - 1) / 8;
			for (int i = 0; i < num; i++)
			{
				int num2 = Math.Min(8, outputBytes - i * 8);
				if (num2 == 8)
				{
					ThreefishEngine.WordToBytes(array2[i], outBytes, outOff + i * 8);
				}
				else
				{
					ThreefishEngine.WordToBytes(array2[i], array, 0);
					Array.Copy(array, 0, outBytes, outOff + i * 8, num2);
				}
			}
		}

		// Token: 0x04002381 RID: 9089
		public const int SKEIN_256 = 256;

		// Token: 0x04002382 RID: 9090
		public const int SKEIN_512 = 512;

		// Token: 0x04002383 RID: 9091
		public const int SKEIN_1024 = 1024;

		// Token: 0x04002384 RID: 9092
		private const int PARAM_TYPE_KEY = 0;

		// Token: 0x04002385 RID: 9093
		private const int PARAM_TYPE_CONFIG = 4;

		// Token: 0x04002386 RID: 9094
		private const int PARAM_TYPE_MESSAGE = 48;

		// Token: 0x04002387 RID: 9095
		private const int PARAM_TYPE_OUTPUT = 63;

		// Token: 0x04002388 RID: 9096
		private static readonly IDictionary INITIAL_STATES = Platform.CreateHashtable();

		// Token: 0x04002389 RID: 9097
		private readonly ThreefishEngine threefish;

		// Token: 0x0400238A RID: 9098
		private readonly int outputSizeBytes;

		// Token: 0x0400238B RID: 9099
		private ulong[] chain;

		// Token: 0x0400238C RID: 9100
		private ulong[] initialState;

		// Token: 0x0400238D RID: 9101
		private byte[] key;

		// Token: 0x0400238E RID: 9102
		private SkeinEngine.Parameter[] preMessageParameters;

		// Token: 0x0400238F RID: 9103
		private SkeinEngine.Parameter[] postMessageParameters;

		// Token: 0x04002390 RID: 9104
		private readonly SkeinEngine.UBI ubi;

		// Token: 0x04002391 RID: 9105
		private readonly byte[] singleByte = new byte[1];

		// Token: 0x02000954 RID: 2388
		private class Configuration
		{
			// Token: 0x06004EC7 RID: 20167 RVA: 0x001B6500 File Offset: 0x001B4700
			public Configuration(long outputSizeBits)
			{
				this.bytes[0] = 83;
				this.bytes[1] = 72;
				this.bytes[2] = 65;
				this.bytes[3] = 51;
				this.bytes[4] = 1;
				this.bytes[5] = 0;
				ThreefishEngine.WordToBytes((ulong)outputSizeBits, this.bytes, 8);
			}

			// Token: 0x17000C43 RID: 3139
			// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x001B6567 File Offset: 0x001B4767
			public byte[] Bytes
			{
				get
				{
					return this.bytes;
				}
			}

			// Token: 0x0400359D RID: 13725
			private byte[] bytes = new byte[32];
		}

		// Token: 0x02000955 RID: 2389
		public class Parameter
		{
			// Token: 0x06004EC9 RID: 20169 RVA: 0x001B656F File Offset: 0x001B476F
			public Parameter(int type, byte[] value)
			{
				this.type = type;
				this.value = value;
			}

			// Token: 0x17000C44 RID: 3140
			// (get) Token: 0x06004ECA RID: 20170 RVA: 0x001B6585 File Offset: 0x001B4785
			public int Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17000C45 RID: 3141
			// (get) Token: 0x06004ECB RID: 20171 RVA: 0x001B658D File Offset: 0x001B478D
			public byte[] Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x0400359E RID: 13726
			private int type;

			// Token: 0x0400359F RID: 13727
			private byte[] value;
		}

		// Token: 0x02000956 RID: 2390
		private class UbiTweak
		{
			// Token: 0x06004ECC RID: 20172 RVA: 0x001B6595 File Offset: 0x001B4795
			public UbiTweak()
			{
				this.Reset();
			}

			// Token: 0x06004ECD RID: 20173 RVA: 0x001B65AF File Offset: 0x001B47AF
			public void Reset(SkeinEngine.UbiTweak tweak)
			{
				this.tweak = Arrays.Clone(tweak.tweak, this.tweak);
				this.extendedPosition = tweak.extendedPosition;
			}

			// Token: 0x06004ECE RID: 20174 RVA: 0x001B65D4 File Offset: 0x001B47D4
			public void Reset()
			{
				this.tweak[0] = 0UL;
				this.tweak[1] = 0UL;
				this.extendedPosition = false;
				this.First = true;
			}

			// Token: 0x17000C46 RID: 3142
			// (get) Token: 0x06004ECF RID: 20175 RVA: 0x001B65F8 File Offset: 0x001B47F8
			// (set) Token: 0x06004ED0 RID: 20176 RVA: 0x001B660A File Offset: 0x001B480A
			public uint Type
			{
				get
				{
					return (uint)(this.tweak[1] >> 56 & 63UL);
				}
				set
				{
					this.tweak[1] = ((this.tweak[1] & 18446743798831644672UL) | ((ulong)value & 63UL) << 56);
				}
			}

			// Token: 0x17000C47 RID: 3143
			// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x001B6630 File Offset: 0x001B4830
			// (set) Token: 0x06004ED2 RID: 20178 RVA: 0x001B6648 File Offset: 0x001B4848
			public bool First
			{
				get
				{
					return (this.tweak[1] & 4611686018427387904UL) > 0UL;
				}
				set
				{
					if (value)
					{
						this.tweak[1] |= 4611686018427387904UL;
						return;
					}
					this.tweak[1] &= 13835058055282163711UL;
				}
			}

			// Token: 0x17000C48 RID: 3144
			// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x001B6680 File Offset: 0x001B4880
			// (set) Token: 0x06004ED4 RID: 20180 RVA: 0x001B6698 File Offset: 0x001B4898
			public bool Final
			{
				get
				{
					return (this.tweak[1] & 9223372036854775808UL) > 0UL;
				}
				set
				{
					if (value)
					{
						this.tweak[1] |= 9223372036854775808UL;
						return;
					}
					this.tweak[1] &= 9223372036854775807UL;
				}
			}

			// Token: 0x06004ED5 RID: 20181 RVA: 0x001B66D0 File Offset: 0x001B48D0
			public void AdvancePosition(int advance)
			{
				if (this.extendedPosition)
				{
					ulong[] array = new ulong[]
					{
						this.tweak[0] & (ulong)-1,
						this.tweak[0] >> 32 & (ulong)-1,
						this.tweak[1] & (ulong)-1
					};
					ulong num = (ulong)((long)advance);
					for (int i = 0; i < array.Length; i++)
					{
						num += array[i];
						array[i] = num;
						num >>= 32;
					}
					this.tweak[0] = ((array[1] & (ulong)-1) << 32 | (array[0] & (ulong)-1));
					this.tweak[1] = ((this.tweak[1] & 18446744069414584320UL) | (array[2] & (ulong)-1));
					return;
				}
				ulong num2 = this.tweak[0];
				num2 += (ulong)advance;
				this.tweak[0] = num2;
				if (num2 > 18446744069414584320UL)
				{
					this.extendedPosition = true;
				}
			}

			// Token: 0x06004ED6 RID: 20182 RVA: 0x001B67A0 File Offset: 0x001B49A0
			public ulong[] GetWords()
			{
				return this.tweak;
			}

			// Token: 0x06004ED7 RID: 20183 RVA: 0x001B67A8 File Offset: 0x001B49A8
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					this.Type,
					" first: ",
					this.First.ToString(),
					", final: ",
					this.Final.ToString()
				});
			}

			// Token: 0x040035A0 RID: 13728
			private const ulong LOW_RANGE = 18446744069414584320UL;

			// Token: 0x040035A1 RID: 13729
			private const ulong T1_FINAL = 9223372036854775808UL;

			// Token: 0x040035A2 RID: 13730
			private const ulong T1_FIRST = 4611686018427387904UL;

			// Token: 0x040035A3 RID: 13731
			private ulong[] tweak = new ulong[2];

			// Token: 0x040035A4 RID: 13732
			private bool extendedPosition;
		}

		// Token: 0x02000957 RID: 2391
		private class UBI
		{
			// Token: 0x06004ED8 RID: 20184 RVA: 0x001B6800 File Offset: 0x001B4A00
			public UBI(SkeinEngine engine, int blockSize)
			{
				this.engine = engine;
				this.currentBlock = new byte[blockSize];
				this.message = new ulong[this.currentBlock.Length / 8];
			}

			// Token: 0x06004ED9 RID: 20185 RVA: 0x001B683C File Offset: 0x001B4A3C
			public void Reset(SkeinEngine.UBI ubi)
			{
				this.currentBlock = Arrays.Clone(ubi.currentBlock, this.currentBlock);
				this.currentOffset = ubi.currentOffset;
				this.message = Arrays.Clone(ubi.message, this.message);
				this.tweak.Reset(ubi.tweak);
			}

			// Token: 0x06004EDA RID: 20186 RVA: 0x001B6894 File Offset: 0x001B4A94
			public void Reset(int type)
			{
				this.tweak.Reset();
				this.tweak.Type = (uint)type;
				this.currentOffset = 0;
			}

			// Token: 0x06004EDB RID: 20187 RVA: 0x001B68B4 File Offset: 0x001B4AB4
			public void Update(byte[] value, int offset, int len, ulong[] output)
			{
				int num = 0;
				while (len > num)
				{
					if (this.currentOffset == this.currentBlock.Length)
					{
						this.ProcessBlock(output);
						this.tweak.First = false;
						this.currentOffset = 0;
					}
					int num2 = Math.Min(len - num, this.currentBlock.Length - this.currentOffset);
					Array.Copy(value, offset + num, this.currentBlock, this.currentOffset, num2);
					num += num2;
					this.currentOffset += num2;
					this.tweak.AdvancePosition(num2);
				}
			}

			// Token: 0x06004EDC RID: 20188 RVA: 0x001B6940 File Offset: 0x001B4B40
			private void ProcessBlock(ulong[] output)
			{
				this.engine.threefish.Init(true, this.engine.chain, this.tweak.GetWords());
				for (int i = 0; i < this.message.Length; i++)
				{
					this.message[i] = ThreefishEngine.BytesToWord(this.currentBlock, i * 8);
				}
				this.engine.threefish.ProcessBlock(this.message, output);
				for (int j = 0; j < output.Length; j++)
				{
					output[j] ^= this.message[j];
				}
			}

			// Token: 0x06004EDD RID: 20189 RVA: 0x001B69D8 File Offset: 0x001B4BD8
			public void DoFinal(ulong[] output)
			{
				for (int i = this.currentOffset; i < this.currentBlock.Length; i++)
				{
					this.currentBlock[i] = 0;
				}
				this.tweak.Final = true;
				this.ProcessBlock(output);
			}

			// Token: 0x040035A5 RID: 13733
			private readonly SkeinEngine.UbiTweak tweak = new SkeinEngine.UbiTweak();

			// Token: 0x040035A6 RID: 13734
			private readonly SkeinEngine engine;

			// Token: 0x040035A7 RID: 13735
			private byte[] currentBlock;

			// Token: 0x040035A8 RID: 13736
			private int currentOffset;

			// Token: 0x040035A9 RID: 13737
			private ulong[] message;
		}
	}
}
