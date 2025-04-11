using System;
using BestHTTP.SignalR;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.JsonEncoders;
using BestHTTP.SignalR.Messages;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000195 RID: 405
	public sealed class DemoHubSample : MonoBehaviour
	{
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0009B37C File Offset: 0x0009957C
		private void Start()
		{
			this.demoHub = new DemoHub();
			this.typedDemoHub = new TypedDemoHub();
			this.vbDemoHub = new Hub("vbdemo");
			this.signalRConnection = new Connection(this.URI, new Hub[]
			{
				this.demoHub,
				this.typedDemoHub,
				this.vbDemoHub
			});
			this.signalRConnection.JsonEncoder = new LitJsonEncoder();
			this.signalRConnection.OnConnected += delegate(Connection connection)
			{
				var <>f__AnonymousType = new
				{
					Name = "Foo",
					Age = 20,
					Address = new
					{
						Street = "One Microsoft Way",
						Zip = "98052"
					}
				};
				this.demoHub.AddToGroups();
				this.demoHub.GetValue();
				this.demoHub.TaskWithException();
				this.demoHub.GenericTaskWithException();
				this.demoHub.SynchronousException();
				this.demoHub.DynamicTask();
				this.demoHub.PassingDynamicComplex(<>f__AnonymousType);
				this.demoHub.SimpleArray(new int[]
				{
					5,
					5,
					6
				});
				this.demoHub.ComplexType(<>f__AnonymousType);
				this.demoHub.ComplexArray(new object[]
				{
					<>f__AnonymousType,
					<>f__AnonymousType,
					<>f__AnonymousType
				});
				this.demoHub.ReportProgress("Long running job!");
				this.demoHub.Overload();
				this.demoHub.State["name"] = "Testing state!";
				this.demoHub.ReadStateValue();
				this.demoHub.PlainTask();
				this.demoHub.GenericTaskWithContinueWith();
				this.typedDemoHub.Echo("Typed echo callback");
				this.vbDemoHub.Call("readStateValue", delegate(Hub hub, ClientMessage msg, ResultMessage result)
				{
					this.vbReadStateResult = string.Format("Read some state from VB.NET! => {0}", (result.ReturnValue == null) ? "undefined" : result.ReturnValue.ToString());
				}, Array.Empty<object>());
			};
			this.signalRConnection.Open();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0009B413 File Offset: 0x00099613
		private void OnDestroy()
		{
			this.signalRConnection.Close();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0009B420 File Offset: 0x00099620
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, false, false, Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				this.demoHub.Draw();
				this.typedDemoHub.Draw();
				GUILayout.Label("Read State Value", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Space(20f);
				GUILayout.Label(this.vbReadStateResult, Array.Empty<GUILayoutOption>());
				GUILayout.EndHorizontal();
				GUILayout.Space(10f);
				GUILayout.EndVertical();
				GUILayout.EndScrollView();
			});
		}

		// Token: 0x04001288 RID: 4744
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/signalr");

		// Token: 0x04001289 RID: 4745
		private Connection signalRConnection;

		// Token: 0x0400128A RID: 4746
		private DemoHub demoHub;

		// Token: 0x0400128B RID: 4747
		private TypedDemoHub typedDemoHub;

		// Token: 0x0400128C RID: 4748
		private Hub vbDemoHub;

		// Token: 0x0400128D RID: 4749
		private string vbReadStateResult = string.Empty;

		// Token: 0x0400128E RID: 4750
		private Vector2 scrollPos;
	}
}
