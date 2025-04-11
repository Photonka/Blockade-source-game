using System;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.Messages;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x02000197 RID: 407
	internal class DemoHub : Hub
	{
		// Token: 0x06000F02 RID: 3842 RVA: 0x0009B768 File Offset: 0x00099968
		public DemoHub() : base("demo")
		{
			base.On("invoke", new OnMethodCallCallbackDelegate(this.Invoke));
			base.On("signal", new OnMethodCallCallbackDelegate(this.Signal));
			base.On("groupAdded", new OnMethodCallCallbackDelegate(this.GroupAdded));
			base.On("fromArbitraryCode", new OnMethodCallCallbackDelegate(this.FromArbitraryCode));
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0009B8A4 File Offset: 0x00099AA4
		public void ReportProgress(string arg)
		{
			base.Call("reportProgress", new OnMethodResultDelegate(this.OnLongRunningJob_Done), null, new OnMethodProgressDelegate(this.OnLongRunningJob_Progress), new object[]
			{
				arg
			});
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0009B8E0 File Offset: 0x00099AE0
		public void OnLongRunningJob_Progress(Hub hub, ClientMessage originialMessage, ProgressMessage progress)
		{
			this.longRunningJobProgress = (float)progress.Progress;
			this.longRunningJobStatus = progress.Progress.ToString() + "%";
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0009B918 File Offset: 0x00099B18
		public void OnLongRunningJob_Done(Hub hub, ClientMessage originalMessage, ResultMessage result)
		{
			this.longRunningJobStatus = result.ReturnValue.ToString();
			this.MultipleCalls();
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0009B931 File Offset: 0x00099B31
		public void MultipleCalls()
		{
			base.Call("multipleCalls", Array.Empty<object>());
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0009B944 File Offset: 0x00099B44
		public void DynamicTask()
		{
			base.Call("dynamicTask", new OnMethodResultDelegate(this.OnDynamicTask_Done), new OnMethodFailedDelegate(this.OnDynamicTask_Failed), Array.Empty<object>());
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0009B96F File Offset: 0x00099B6F
		private void OnDynamicTask_Failed(Hub hub, ClientMessage originalMessage, FailureMessage result)
		{
			this.dynamicTaskResult = string.Format("The dynamic task failed :( {0}", result.ErrorMessage);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0009B987 File Offset: 0x00099B87
		private void OnDynamicTask_Done(Hub hub, ClientMessage originalMessage, ResultMessage result)
		{
			this.dynamicTaskResult = string.Format("The dynamic task! {0}", result.ReturnValue);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0009B99F File Offset: 0x00099B9F
		public void AddToGroups()
		{
			base.Call("addToGroups", Array.Empty<object>());
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0009B9B2 File Offset: 0x00099BB2
		public void GetValue()
		{
			base.Call("getValue", delegate(Hub hub, ClientMessage msg, ResultMessage result)
			{
				this.genericTaskResult = string.Format("The value is {0} after 5 seconds", result.ReturnValue);
			}, Array.Empty<object>());
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0009B9D1 File Offset: 0x00099BD1
		public void TaskWithException()
		{
			base.Call("taskWithException", null, delegate(Hub hub, ClientMessage msg, FailureMessage error)
			{
				this.taskWithExceptionResult = string.Format("Error: {0}", error.ErrorMessage);
			}, Array.Empty<object>());
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0009B9F1 File Offset: 0x00099BF1
		public void GenericTaskWithException()
		{
			base.Call("genericTaskWithException", null, delegate(Hub hub, ClientMessage msg, FailureMessage error)
			{
				this.genericTaskWithExceptionResult = string.Format("Error: {0}", error.ErrorMessage);
			}, Array.Empty<object>());
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0009BA11 File Offset: 0x00099C11
		public void SynchronousException()
		{
			base.Call("synchronousException", null, delegate(Hub hub, ClientMessage msg, FailureMessage error)
			{
				this.synchronousExceptionResult = string.Format("Error: {0}", error.ErrorMessage);
			}, Array.Empty<object>());
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0009BA31 File Offset: 0x00099C31
		public void PassingDynamicComplex(object person)
		{
			base.Call("passingDynamicComplex", delegate(Hub hub, ClientMessage msg, ResultMessage result)
			{
				this.invokingHubMethodWithDynamicResult = string.Format("The person's age is {0}", result.ReturnValue);
			}, new object[]
			{
				person
			});
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0009BA55 File Offset: 0x00099C55
		public void SimpleArray(int[] array)
		{
			base.Call("simpleArray", delegate(Hub hub, ClientMessage msg, ResultMessage result)
			{
				this.simpleArrayResult = "Simple array works!";
			}, new object[]
			{
				array
			});
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0009BA79 File Offset: 0x00099C79
		public void ComplexType(object person)
		{
			base.Call("complexType", delegate(Hub hub, ClientMessage msg, ResultMessage result)
			{
				this.complexTypeResult = string.Format("Complex Type -> {0}", ((IHub)this).Connection.JsonEncoder.Encode(base.State["person"]));
			}, new object[]
			{
				person
			});
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0009BA9D File Offset: 0x00099C9D
		public void ComplexArray(object[] complexArray)
		{
			base.Call("ComplexArray", delegate(Hub hub, ClientMessage msg, ResultMessage result)
			{
				this.complexArrayResult = "Complex Array Works!";
			}, new object[]
			{
				complexArray
			});
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0009BAC1 File Offset: 0x00099CC1
		public void Overload()
		{
			base.Call("Overload", new OnMethodResultDelegate(this.OnVoidOverload_Done), Array.Empty<object>());
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0009BAE0 File Offset: 0x00099CE0
		private void OnVoidOverload_Done(Hub hub, ClientMessage originalMessage, ResultMessage result)
		{
			this.voidOverloadResult = "Void Overload called";
			this.Overload(101);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0009BAF5 File Offset: 0x00099CF5
		public void Overload(int number)
		{
			base.Call("Overload", new OnMethodResultDelegate(this.OnIntOverload_Done), new object[]
			{
				number
			});
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0009BB1E File Offset: 0x00099D1E
		private void OnIntOverload_Done(Hub hub, ClientMessage originalMessage, ResultMessage result)
		{
			this.intOverloadResult = string.Format("Overload with return value called => {0}", result.ReturnValue.ToString());
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0009BB3B File Offset: 0x00099D3B
		public void ReadStateValue()
		{
			base.Call("readStateValue", delegate(Hub hub, ClientMessage msg, ResultMessage result)
			{
				this.readStateResult = string.Format("Read some state! => {0}", result.ReturnValue);
			}, Array.Empty<object>());
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0009BB5A File Offset: 0x00099D5A
		public void PlainTask()
		{
			base.Call("plainTask", delegate(Hub hub, ClientMessage msg, ResultMessage result)
			{
				this.plainTaskResult = "Plain Task Result";
			}, Array.Empty<object>());
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0009BB79 File Offset: 0x00099D79
		public void GenericTaskWithContinueWith()
		{
			base.Call("genericTaskWithContinueWith", delegate(Hub hub, ClientMessage msg, ResultMessage result)
			{
				this.genericTaskWithContinueWithResult = result.ReturnValue.ToString();
			}, Array.Empty<object>());
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0009BB98 File Offset: 0x00099D98
		private void FromArbitraryCode(Hub hub, MethodCallMessage methodCall)
		{
			this.fromArbitraryCodeResult = (methodCall.Arguments[0] as string);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0009BBAD File Offset: 0x00099DAD
		private void GroupAdded(Hub hub, MethodCallMessage methodCall)
		{
			if (!string.IsNullOrEmpty(this.groupAddedResult))
			{
				this.groupAddedResult = "Group Already Added!";
				return;
			}
			this.groupAddedResult = "Group Added!";
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0009BBD3 File Offset: 0x00099DD3
		private void Signal(Hub hub, MethodCallMessage methodCall)
		{
			this.dynamicTaskResult = string.Format("The dynamic task! {0}", methodCall.Arguments[0]);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0009BBED File Offset: 0x00099DED
		private void Invoke(Hub hub, MethodCallMessage methodCall)
		{
			this.invokeResults.Add(string.Format("{0} client state index -> {1}", methodCall.Arguments[0], base.State["index"]));
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0009BC1C File Offset: 0x00099E1C
		public void Draw()
		{
			GUILayout.Label("Arbitrary Code", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(string.Format("Sending {0} from arbitrary code without the hub itself!", this.fromArbitraryCodeResult), Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Group Added", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.groupAddedResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Dynamic Task", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.dynamicTaskResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Report Progress", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.Label(this.longRunningJobStatus, Array.Empty<GUILayoutOption>());
			GUILayout.HorizontalSlider(this.longRunningJobProgress, 0f, 100f, Array.Empty<GUILayoutOption>());
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Generic Task", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.genericTaskResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Task With Exception", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.taskWithExceptionResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Generic Task With Exception", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.genericTaskWithExceptionResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Synchronous Exception", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.synchronousExceptionResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Invoking hub method with dynamic", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.invokingHubMethodWithDynamicResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Simple Array", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.simpleArrayResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Complex Type", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.complexTypeResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Complex Array", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.complexArrayResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Overloads", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.Label(this.voidOverloadResult, Array.Empty<GUILayoutOption>());
			GUILayout.Label(this.intOverloadResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Read State Value", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.readStateResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Plain Task", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.plainTaskResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Generic Task With ContinueWith", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.Label(this.genericTaskWithContinueWithResult, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.Label("Message Pump", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			this.invokeResults.Draw((float)(Screen.width - 40), 270f);
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
		}

		// Token: 0x04001291 RID: 4753
		private float longRunningJobProgress;

		// Token: 0x04001292 RID: 4754
		private string longRunningJobStatus = "Not Started!";

		// Token: 0x04001293 RID: 4755
		private string fromArbitraryCodeResult = string.Empty;

		// Token: 0x04001294 RID: 4756
		private string groupAddedResult = string.Empty;

		// Token: 0x04001295 RID: 4757
		private string dynamicTaskResult = string.Empty;

		// Token: 0x04001296 RID: 4758
		private string genericTaskResult = string.Empty;

		// Token: 0x04001297 RID: 4759
		private string taskWithExceptionResult = string.Empty;

		// Token: 0x04001298 RID: 4760
		private string genericTaskWithExceptionResult = string.Empty;

		// Token: 0x04001299 RID: 4761
		private string synchronousExceptionResult = string.Empty;

		// Token: 0x0400129A RID: 4762
		private string invokingHubMethodWithDynamicResult = string.Empty;

		// Token: 0x0400129B RID: 4763
		private string simpleArrayResult = string.Empty;

		// Token: 0x0400129C RID: 4764
		private string complexTypeResult = string.Empty;

		// Token: 0x0400129D RID: 4765
		private string complexArrayResult = string.Empty;

		// Token: 0x0400129E RID: 4766
		private string voidOverloadResult = string.Empty;

		// Token: 0x0400129F RID: 4767
		private string intOverloadResult = string.Empty;

		// Token: 0x040012A0 RID: 4768
		private string readStateResult = string.Empty;

		// Token: 0x040012A1 RID: 4769
		private string plainTaskResult = string.Empty;

		// Token: 0x040012A2 RID: 4770
		private string genericTaskWithContinueWithResult = string.Empty;

		// Token: 0x040012A3 RID: 4771
		private GUIMessageList invokeResults = new GUIMessageList();
	}
}
