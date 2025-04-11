using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.SignalRCore.Messages;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001DC RID: 476
	public sealed class JsonProtocol : IProtocol
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0006CF70 File Offset: 0x0006B170
		public TransferModes Type
		{
			get
			{
				return TransferModes.Text;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x000A5AA9 File Offset: 0x000A3CA9
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x000A5AB1 File Offset: 0x000A3CB1
		public IEncoder Encoder { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x000A5ABA File Offset: 0x000A3CBA
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x000A5AC2 File Offset: 0x000A3CC2
		public HubConnection Connection { get; set; }

		// Token: 0x060011DA RID: 4570 RVA: 0x000A5ACB File Offset: 0x000A3CCB
		public JsonProtocol(IEncoder encoder)
		{
			if (encoder == null)
			{
				throw new ArgumentNullException("encoder");
			}
			if (encoder.Name != "json")
			{
				throw new ArgumentException("Encoder must be a json encoder!");
			}
			this.Encoder = encoder;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000A5B08 File Offset: 0x000A3D08
		public void ParseMessages(string data, ref List<Message> messages)
		{
			int num = 0;
			int num2 = data.IndexOf('\u001e');
			if (num2 == -1)
			{
				throw new Exception("Missing separator!");
			}
			while (num2 != -1)
			{
				string text = data.Substring(num, num2 - num);
				Message item = this.Encoder.DecodeAs<Message>(text);
				messages.Add(item);
				num = num2 + 1;
				num2 = data.IndexOf('\u001e', num);
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00002B75 File Offset: 0x00000D75
		public void ParseMessages(byte[] data, ref List<Message> messages)
		{
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000A5B64 File Offset: 0x000A3D64
		public byte[] EncodeMessage(Message message)
		{
			string text = null;
			switch (message.type)
			{
			case MessageTypes.Invocation:
			case MessageTypes.StreamInvocation:
				text = this.Encoder.EncodeAsText<InvocationMessage>(new InvocationMessage
				{
					type = message.type,
					invocationId = message.invocationId,
					nonblocking = message.nonblocking,
					target = message.target,
					arguments = message.arguments
				});
				break;
			case MessageTypes.CancelInvocation:
				text = this.Encoder.EncodeAsText<CancelInvocationMessage>(new CancelInvocationMessage
				{
					invocationId = message.invocationId
				});
				break;
			case MessageTypes.Ping:
				text = this.Encoder.EncodeAsText<PingMessage>(default(PingMessage));
				break;
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return JsonProtocol.WithSeparator(text);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000A5C44 File Offset: 0x000A3E44
		public object[] GetRealArguments(Type[] argTypes, object[] arguments)
		{
			if (arguments == null || arguments.Length == 0)
			{
				return null;
			}
			if (argTypes.Length > arguments.Length)
			{
				throw new Exception(string.Format("argType.Length({0}) < arguments.length({1})", argTypes.Length, arguments.Length));
			}
			object[] array = new object[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
			{
				array[i] = this.ConvertTo(argTypes[i], arguments[i]);
			}
			return array;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000A5CAC File Offset: 0x000A3EAC
		public object ConvertTo(Type toType, object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (toType.IsPrimitive || toType.IsEnum)
			{
				return Convert.ChangeType(obj, toType);
			}
			if (toType == typeof(string))
			{
				return obj.ToString();
			}
			return this.Encoder.ConvertTo(toType, obj);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000A5CFC File Offset: 0x000A3EFC
		public static byte[] WithSeparator(string str)
		{
			int byteCount = Encoding.UTF8.GetByteCount(str);
			byte[] array = new byte[byteCount + 1];
			Encoding.UTF8.GetBytes(str, 0, str.Length, array, 0);
			array[byteCount] = 30;
			return array;
		}

		// Token: 0x040013EE RID: 5102
		public const char Separator = '\u001e';
	}
}
