using System;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.Messages;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000196 RID: 406
	internal class TypedDemoHub : Hub
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0009B669 File Offset: 0x00099869
		public TypedDemoHub() : base("typeddemohub")
		{
			base.On("Echo", new OnMethodCallCallbackDelegate(this.Echo));
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0009B6A3 File Offset: 0x000998A3
		private void Echo(Hub hub, MethodCallMessage methodCall)
		{
			this.typedEchoClientResult = string.Format("{0} #{1} triggered!", methodCall.Arguments[0], methodCall.Arguments[1]);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0009B6C5 File Offset: 0x000998C5
		public void Echo(string msg)
		{
			base.Call("echo", new OnMethodResultDelegate(this.OnEcho_Done), new object[]
			{
				msg
			});
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0009B6E9 File Offset: 0x000998E9
		private void OnEcho_Done(Hub hub, ClientMessage originalMessage, ResultMessage result)
		{
			this.typedEchoResult = "TypedDemoHub.Echo(string message) invoked!";
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0009B6F8 File Offset: 0x000998F8
		public void Draw()
		{
			GUILayout.Label("Typed callback", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.Label(this.typedEchoResult, Array.Empty<GUILayoutOption>());
			GUILayout.Label(this.typedEchoClientResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
		}

		// Token: 0x0400128F RID: 4751
		private string typedEchoResult = string.Empty;

		// Token: 0x04001290 RID: 4752
		private string typedEchoClientResult = string.Empty;
	}
}
