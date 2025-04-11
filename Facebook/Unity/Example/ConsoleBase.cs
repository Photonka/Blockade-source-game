using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Facebook.Unity.Example
{
	// Token: 0x0200013A RID: 314
	internal class ConsoleBase : MonoBehaviour
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0008BE2E File Offset: 0x0008A02E
		protected static int ButtonHeight
		{
			get
			{
				if (!Constants.IsMobile)
				{
					return 24;
				}
				return 60;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0008BE3C File Offset: 0x0008A03C
		protected static int MainWindowWidth
		{
			get
			{
				if (!Constants.IsMobile)
				{
					return 700;
				}
				return Screen.width - 30;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0008BE53 File Offset: 0x0008A053
		protected static int MainWindowFullWidth
		{
			get
			{
				if (!Constants.IsMobile)
				{
					return 760;
				}
				return Screen.width;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0008BE67 File Offset: 0x0008A067
		protected static int MarginFix
		{
			get
			{
				if (!Constants.IsMobile)
				{
					return 48;
				}
				return 0;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0008BE74 File Offset: 0x0008A074
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0008BE7B File Offset: 0x0008A07B
		protected static Stack<string> MenuStack
		{
			get
			{
				return ConsoleBase.menuStack;
			}
			set
			{
				ConsoleBase.menuStack = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0008BE83 File Offset: 0x0008A083
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x0008BE8B File Offset: 0x0008A08B
		protected string Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0008BE94 File Offset: 0x0008A094
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x0008BE9C File Offset: 0x0008A09C
		protected Texture2D LastResponseTexture { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0008BEA5 File Offset: 0x0008A0A5
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0008BEAD File Offset: 0x0008A0AD
		protected string LastResponse
		{
			get
			{
				return this.lastResponse;
			}
			set
			{
				this.lastResponse = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0008BEB6 File Offset: 0x0008A0B6
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0008BEBE File Offset: 0x0008A0BE
		protected Vector2 ScrollPosition
		{
			get
			{
				return this.scrollPosition;
			}
			set
			{
				this.scrollPosition = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0008BEC7 File Offset: 0x0008A0C7
		protected float ScaleFactor
		{
			get
			{
				if (this.scaleFactor == null)
				{
					this.scaleFactor = new float?(Screen.dpi / 160f);
				}
				return this.scaleFactor.Value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0008BEF7 File Offset: 0x0008A0F7
		protected int FontSize
		{
			get
			{
				return (int)Math.Round((double)(this.ScaleFactor * 16f));
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0008BF0C File Offset: 0x0008A10C
		protected GUIStyle TextStyle
		{
			get
			{
				if (this.textStyle == null)
				{
					this.textStyle = new GUIStyle(GUI.skin.textArea);
					this.textStyle.alignment = 0;
					this.textStyle.wordWrap = true;
					this.textStyle.padding = new RectOffset(10, 10, 10, 10);
					this.textStyle.stretchHeight = true;
					this.textStyle.stretchWidth = false;
					this.textStyle.fontSize = this.FontSize;
				}
				return this.textStyle;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0008BF95 File Offset: 0x0008A195
		protected GUIStyle ButtonStyle
		{
			get
			{
				if (this.buttonStyle == null)
				{
					this.buttonStyle = new GUIStyle(GUI.skin.button);
					this.buttonStyle.fontSize = this.FontSize;
				}
				return this.buttonStyle;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0008BFCB File Offset: 0x0008A1CB
		protected GUIStyle TextInputStyle
		{
			get
			{
				if (this.textInputStyle == null)
				{
					this.textInputStyle = new GUIStyle(GUI.skin.textField);
					this.textInputStyle.fontSize = this.FontSize;
				}
				return this.textInputStyle;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0008C001 File Offset: 0x0008A201
		protected GUIStyle LabelStyle
		{
			get
			{
				if (this.labelStyle == null)
				{
					this.labelStyle = new GUIStyle(GUI.skin.label);
					this.labelStyle.fontSize = this.FontSize;
				}
				return this.labelStyle;
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0008C037 File Offset: 0x0008A237
		protected virtual void Awake()
		{
			Application.targetFrameRate = 60;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0008C040 File Offset: 0x0008A240
		protected bool Button(string label)
		{
			return GUILayout.Button(label, this.ButtonStyle, new GUILayoutOption[]
			{
				GUILayout.MinHeight((float)ConsoleBase.ButtonHeight * this.ScaleFactor),
				GUILayout.MaxWidth((float)ConsoleBase.MainWindowWidth)
			});
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0008C078 File Offset: 0x0008A278
		protected void LabelAndTextField(string label, ref string text)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(label, this.LabelStyle, new GUILayoutOption[]
			{
				GUILayout.MaxWidth(200f * this.ScaleFactor)
			});
			text = GUILayout.TextField(text, this.TextInputStyle, new GUILayoutOption[]
			{
				GUILayout.MaxWidth((float)(ConsoleBase.MainWindowWidth - 150))
			});
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0006CF70 File Offset: 0x0006B170
		protected bool IsHorizontalLayout()
		{
			return true;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0008C0E3 File Offset: 0x0008A2E3
		protected void SwitchMenu(Type menuClass)
		{
			ConsoleBase.menuStack.Push(base.GetType().Name);
			SceneManager.LoadScene(menuClass.Name);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0008C105 File Offset: 0x0008A305
		protected void GoBack()
		{
			if (ConsoleBase.menuStack.Any<string>())
			{
				SceneManager.LoadScene(ConsoleBase.menuStack.Pop());
			}
		}

		// Token: 0x040010B4 RID: 4276
		private const int DpiScalingFactor = 160;

		// Token: 0x040010B5 RID: 4277
		private static Stack<string> menuStack = new Stack<string>();

		// Token: 0x040010B6 RID: 4278
		private string status = "Ready";

		// Token: 0x040010B7 RID: 4279
		private string lastResponse = string.Empty;

		// Token: 0x040010B8 RID: 4280
		private Vector2 scrollPosition = Vector2.zero;

		// Token: 0x040010B9 RID: 4281
		private float? scaleFactor;

		// Token: 0x040010BA RID: 4282
		private GUIStyle textStyle;

		// Token: 0x040010BB RID: 4283
		private GUIStyle buttonStyle;

		// Token: 0x040010BC RID: 4284
		private GUIStyle textInputStyle;

		// Token: 0x040010BD RID: 4285
		private GUIStyle labelStyle;
	}
}
