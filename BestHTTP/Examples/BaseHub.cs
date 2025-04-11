using System;
using System.Collections.Generic;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.Messages;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000192 RID: 402
	internal class BaseHub : Hub
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x0009A944 File Offset: 0x00098B44
		public BaseHub(string name, string title) : base(name)
		{
			this.Title = title;
			base.On("joined", new OnMethodCallCallbackDelegate(this.Joined));
			base.On("rejoined", new OnMethodCallCallbackDelegate(this.Rejoined));
			base.On("left", new OnMethodCallCallbackDelegate(this.Left));
			base.On("invoked", new OnMethodCallCallbackDelegate(this.Invoked));
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0009A9C8 File Offset: 0x00098BC8
		private void Joined(Hub hub, MethodCallMessage methodCall)
		{
			Dictionary<string, object> dictionary = methodCall.Arguments[2] as Dictionary<string, object>;
			this.messages.Add(string.Format("{0} joined at {1}\n\tIsAuthenticated: {2} IsAdmin: {3} UserName: {4}", new object[]
			{
				methodCall.Arguments[0],
				methodCall.Arguments[1],
				dictionary["IsAuthenticated"],
				dictionary["IsAdmin"],
				dictionary["UserName"]
			}));
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0009AA3E File Offset: 0x00098C3E
		private void Rejoined(Hub hub, MethodCallMessage methodCall)
		{
			this.messages.Add(string.Format("{0} reconnected at {1}", methodCall.Arguments[0], methodCall.Arguments[1]));
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0009AA65 File Offset: 0x00098C65
		private void Left(Hub hub, MethodCallMessage methodCall)
		{
			this.messages.Add(string.Format("{0} left at {1}", methodCall.Arguments[0], methodCall.Arguments[1]));
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0009AA8C File Offset: 0x00098C8C
		private void Invoked(Hub hub, MethodCallMessage methodCall)
		{
			this.messages.Add(string.Format("{0} invoked hub method at {1}", methodCall.Arguments[0], methodCall.Arguments[1]));
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0009AAB3 File Offset: 0x00098CB3
		public void InvokedFromClient()
		{
			base.Call("invokedFromClient", new OnMethodResultDelegate(this.OnInvoked), new OnMethodFailedDelegate(this.OnInvokeFailed), Array.Empty<object>());
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0009AADE File Offset: 0x00098CDE
		private void OnInvoked(Hub hub, ClientMessage originalMessage, ResultMessage result)
		{
			Debug.Log(hub.Name + " invokedFromClient success!");
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0009AAF5 File Offset: 0x00098CF5
		private void OnInvokeFailed(Hub hub, ClientMessage originalMessage, FailureMessage result)
		{
			Debug.LogWarning(hub.Name + " " + result.ErrorMessage);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0009AB14 File Offset: 0x00098D14
		public void Draw()
		{
			GUILayout.Label(this.Title, Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			this.messages.Draw((float)(Screen.width - 20), 100f);
			GUILayout.EndHorizontal();
		}

		// Token: 0x0400127C RID: 4732
		private string Title;

		// Token: 0x0400127D RID: 4733
		private GUIMessageList messages = new GUIMessageList();
	}
}
