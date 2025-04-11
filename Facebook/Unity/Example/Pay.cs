using System;
using UnityEngine;

namespace Facebook.Unity.Example
{
	// Token: 0x02000145 RID: 325
	internal class Pay : MenuBase
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x0008D2D5 File Offset: 0x0008B4D5
		protected override void GetGui()
		{
			base.LabelAndTextField("Product: ", ref this.payProduct);
			if (base.Button("Call Pay"))
			{
				this.CallFBPay();
			}
			GUILayout.Space(10f);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0008D308 File Offset: 0x0008B508
		private void CallFBPay()
		{
			FB.Canvas.Pay(this.payProduct, "purchaseitem", 1, null, null, null, null, null, new FacebookDelegate<IPayResult>(base.HandleResult));
		}

		// Token: 0x040010D9 RID: 4313
		private string payProduct = string.Empty;
	}
}
