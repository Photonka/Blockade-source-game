using System;
using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Encoders;
using BestHTTP.SignalRCore.Messages;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x0200019E RID: 414
	public class TestHubExample : MonoBehaviour
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x0009CE54 File Offset: 0x0009B054
		private void Start()
		{
			HubOptions hubOptions = new HubOptions();
			hubOptions.SkipNegotiation = false;
			this.hub = new HubConnection(this.URI, new JsonProtocol(new LitJsonEncoder()), hubOptions);
			this.hub.OnConnected += this.Hub_OnConnected;
			this.hub.OnError += this.Hub_OnError;
			this.hub.OnClosed += this.Hub_OnClosed;
			this.hub.OnMessage += this.Hub_OnMessage;
			this.hub.On<string>("Send", delegate(string arg)
			{
				this.uiText += string.Format(" On Send: {0}\n", arg);
			});
			this.hub.On<TestHubExample.Person>("Person", delegate(TestHubExample.Person person)
			{
				this.uiText += string.Format(" On Person: {0}\n", person);
			});
			this.hub.On<TestHubExample.Person, TestHubExample.Person>("TwoPersons", delegate(TestHubExample.Person person1, TestHubExample.Person person2)
			{
				this.uiText += string.Format(" On TwoPersons: {0}, {1}\n", person1, person2);
			});
			this.hub.StartConnect();
			this.uiText = "StartConnect called\n";
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0009CF50 File Offset: 0x0009B150
		private void OnDestroy()
		{
			if (this.hub != null)
			{
				this.hub.StartClose();
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0009CF65 File Offset: 0x0009B165
		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, true, delegate
			{
				this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, false, false, Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.Label(this.uiText, Array.Empty<GUILayoutOption>());
				GUILayout.EndVertical();
				GUILayout.EndScrollView();
			});
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0009CF80 File Offset: 0x0009B180
		private void Hub_OnConnected(HubConnection hub)
		{
			this.uiText += "Hub Connected\n";
			hub.Send("Send", new object[]
			{
				"my message"
			});
			hub.Invoke<string>("NoParam", Array.Empty<object>()).OnSuccess(delegate(string ret)
			{
				this.uiText += string.Format(" 'NoParam' returned: {0}\n", ret);
			});
			hub.Invoke<int>("Add", new object[]
			{
				10,
				20
			}).OnSuccess(delegate(int result)
			{
				this.uiText += string.Format(" 'Add(10, 20)' returned: {0}\n", result);
			}).OnError(delegate(Exception error)
			{
				this.uiText += string.Format(" 'Add(10, 20)' error: {0}\n", error);
			});
			hub.Invoke<TestHubExample.Person>("GetPerson", new object[]
			{
				"Mr. Smith",
				26
			}).OnSuccess(delegate(TestHubExample.Person result)
			{
				this.uiText += string.Format(" 'GetPerson(\"Mr. Smith\", 26)' returned: {0}\n", result);
			}).OnError(delegate(Exception error)
			{
				this.uiText += string.Format(" 'GetPerson(\"Mr. Smith\", 26)' error: {0}\n", error);
			});
			hub.Invoke<int>("SingleResultFailure", new object[]
			{
				10,
				20
			}).OnSuccess(delegate(int result)
			{
				this.uiText += string.Format(" 'SingleResultFailure(10, 20)' returned: {0}\n", result);
			}).OnError(delegate(Exception error)
			{
				this.uiText += string.Format(" 'SingleResultFailure(10, 20)' error: {0}\n", error);
			});
			hub.Invoke<int[]>("Batched", new object[]
			{
				10
			}).OnSuccess(delegate(int[] result)
			{
				this.uiText += string.Format(" 'Batched(10)' returned items: {0}\n", result.Length);
			}).OnError(delegate(Exception error)
			{
				this.uiText += string.Format(" 'Batched(10)' error: {0}\n", error);
			});
			hub.Stream<int>("ObservableCounter", new object[]
			{
				10,
				1000
			}).OnItem(delegate(StreamItemContainer<int> result)
			{
				this.uiText += string.Format(" 'ObservableCounter(10, 1000)' OnItem: {0}\n", result.LastAdded);
			}).OnSuccess(delegate(StreamItemContainer<int> result)
			{
				this.uiText += string.Format(" 'ObservableCounter(10, 1000)' OnSuccess. Final count: {0}\n", result.Items.Count);
			}).OnError(delegate(Exception error)
			{
				this.uiText += string.Format(" 'ObservableCounter(10, 1000)' error: {0}\n", error);
			});
			StreamItemContainer<int> value = hub.Stream<int>("ChannelCounter", new object[]
			{
				10,
				1000
			}).OnItem(delegate(StreamItemContainer<int> result)
			{
				this.uiText += string.Format(" 'ChannelCounter(10, 1000)' OnItem: {0}\n", result.LastAdded);
			}).OnSuccess(delegate(StreamItemContainer<int> result)
			{
				this.uiText += string.Format(" 'ChannelCounter(10, 1000)' OnSuccess. Final count: {0}\n", result.Items.Count);
			}).OnError(delegate(Exception error)
			{
				this.uiText += string.Format(" 'ChannelCounter(10, 1000)' error: {0}\n", error);
			}).value;
			hub.CancelStream<int>(value);
			hub.Stream<TestHubExample.Person>("GetRandomPersons", new object[]
			{
				20,
				2000
			}).OnItem(delegate(StreamItemContainer<TestHubExample.Person> result)
			{
				this.uiText += string.Format(" 'GetRandomPersons(20, 1000)' OnItem: {0}\n", result.LastAdded);
			}).OnSuccess(delegate(StreamItemContainer<TestHubExample.Person> result)
			{
				this.uiText += string.Format(" 'GetRandomPersons(20, 1000)' OnSuccess. Final count: {0}\n", result.Items.Count);
			});
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0006CF70 File Offset: 0x0006B170
		private bool Hub_OnMessage(HubConnection hub, Message message)
		{
			return true;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0009D202 File Offset: 0x0009B402
		private void Hub_OnClosed(HubConnection hub)
		{
			this.uiText += "Hub Closed\n";
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0009D21A File Offset: 0x0009B41A
		private void Hub_OnError(HubConnection hub, string error)
		{
			this.uiText = this.uiText + "Hub Error: " + error + "\n";
		}

		// Token: 0x040012BB RID: 4795
		private readonly Uri URI = new Uri(GUIHelper.BaseURL + "/TestHub");

		// Token: 0x040012BC RID: 4796
		private HubConnection hub;

		// Token: 0x040012BD RID: 4797
		private Vector2 scrollPos;

		// Token: 0x040012BE RID: 4798
		private string uiText;

		// Token: 0x020008C1 RID: 2241
		private sealed class Person
		{
			// Token: 0x17000C0A RID: 3082
			// (get) Token: 0x06004D2D RID: 19757 RVA: 0x001B0615 File Offset: 0x001AE815
			// (set) Token: 0x06004D2E RID: 19758 RVA: 0x001B061D File Offset: 0x001AE81D
			public string Name { get; set; }

			// Token: 0x17000C0B RID: 3083
			// (get) Token: 0x06004D2F RID: 19759 RVA: 0x001B0626 File Offset: 0x001AE826
			// (set) Token: 0x06004D30 RID: 19760 RVA: 0x001B062E File Offset: 0x001AE82E
			public long Age { get; set; }

			// Token: 0x06004D31 RID: 19761 RVA: 0x001B0638 File Offset: 0x001AE838
			public override string ToString()
			{
				return string.Format("[Person Name: '{0}', Age: {1}]", this.Name, this.Age.ToString());
			}
		}
	}
}
