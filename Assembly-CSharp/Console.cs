using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class Console : MonoBehaviour
{
	// Token: 0x0600011E RID: 286 RVA: 0x00017EDB File Offset: 0x000160DB
	private void OnEnable()
	{
		Application.RegisterLogCallback(new Application.LogCallback(this.HandleLog));
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00017EEE File Offset: 0x000160EE
	private void OnDisable()
	{
		Application.RegisterLogCallback(null);
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00017EF6 File Offset: 0x000160F6
	private void Update()
	{
		if (Input.GetKeyDown(this.toggleKey))
		{
			this.show = !this.show;
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00017F14 File Offset: 0x00016114
	private void OnGUI()
	{
		if (!this.show)
		{
			return;
		}
		GUI.depth = -99;
		this.windowRect = new Rect(0f, 0f, (float)Screen.width, (float)(Screen.height / 2));
		GUI.DrawTexture(this.windowRect, this._t);
		GUI.skin.horizontalScrollbar.normal.background = null;
		GUI.skin.horizontalScrollbarThumb.normal.background = null;
		this.scrollPosition = GUI.BeginScrollView(new Rect(this.windowRect.x + 5f, this.windowRect.y + 5f, this.windowRect.width - 10f, this.windowRect.height - 25f - 10f), this.scrollPosition, new Rect(0f, 0f, (float)(Screen.width - 15), (float)(this.logs.Count * 25)));
		int num = 0;
		for (int i = 0; i < this.logs.Count; i++)
		{
			global::Console.Log log = this.logs[i];
			if (!this.collapse || i <= 0 || !(log.message == this.logs[i - 1].message))
			{
				this.gui_style.normal.textColor = global::Console.logTypeColors[log.type];
				GUIContent guicontent = new GUIContent(log.message);
				float num2 = this.gui_style.CalcHeight(guicontent, (float)(Screen.width - 15));
				GUI.TextArea(new Rect(0f, (float)num, (float)(Screen.width - 15), num2), log.message, this.gui_style);
				num += (int)num2 + 10;
			}
		}
		GUI.EndScrollView();
		GUI.contentColor = Color.white;
		Rect rect;
		rect..ctor(this.windowRect.x + 5f, this.windowRect.yMax - 25f, this.windowRect.width - 10f - 100f, 20f);
		Rect rect2;
		rect2..ctor(this.windowRect.xMax - 100f, this.windowRect.yMax - 25f, 100f, 20f);
		Vector2 vector;
		vector..ctor(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		if (rect.Contains(vector))
		{
			GUI.DrawTexture(rect, this._t_h);
		}
		else
		{
			GUI.DrawTexture(rect, this._t);
		}
		TextAnchor alignment = this.gui_style.alignment;
		this.gui_style.alignment = 4;
		if (GUI.Button(rect, this.clearLabel, this.gui_style))
		{
			this.logs.Clear();
		}
		this.gui_style.alignment = alignment;
		this.collapse = GUI.Toggle(rect2, this.collapse, this.collapseLabel);
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00018220 File Offset: 0x00016420
	private void HandleLog(string message, string stackTrace, LogType type)
	{
		this.logs.Add(new global::Console.Log
		{
			message = message,
			stackTrace = stackTrace,
			type = type
		});
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0001825C File Offset: 0x0001645C
	private void Awake()
	{
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[0];
		this.gui_style.fontSize = 16;
		this.gui_style.normal.textColor = new Color(255f, 255f, 255f, 255f);
		this.gui_style.alignment = 3;
		this.gui_style.margin.bottom = 15;
		this._t = GUI3.GetTexture2D(Color.black, 70f);
		this._t_h = GUI3.GetTexture2D(Color.white, 70f);
	}

	// Token: 0x0400013B RID: 315
	public KeyCode toggleKey = 96;

	// Token: 0x0400013C RID: 316
	private List<global::Console.Log> logs = new List<global::Console.Log>();

	// Token: 0x0400013D RID: 317
	private Vector2 scrollPosition;

	// Token: 0x0400013E RID: 318
	private bool show;

	// Token: 0x0400013F RID: 319
	private bool collapse;

	// Token: 0x04000140 RID: 320
	private GUIStyle gui_style;

	// Token: 0x04000141 RID: 321
	private Texture2D _t;

	// Token: 0x04000142 RID: 322
	private Texture2D _t_h;

	// Token: 0x04000143 RID: 323
	private static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
	{
		{
			1,
			Color.white
		},
		{
			0,
			Color.red
		},
		{
			4,
			Color.red
		},
		{
			3,
			Color.white
		},
		{
			2,
			Color.yellow
		}
	};

	// Token: 0x04000144 RID: 324
	private const int margin = 20;

	// Token: 0x04000145 RID: 325
	private Rect windowRect = new Rect(0f, 0f, (float)Screen.width, (float)(Screen.height / 2));

	// Token: 0x04000146 RID: 326
	private Rect titleBarRect = new Rect(0f, 0f, 10000f, 20f);

	// Token: 0x04000147 RID: 327
	private GUIContent clearLabel = new GUIContent("Очиска", "Очистить содержимое консоли.");

	// Token: 0x04000148 RID: 328
	private GUIContent collapseLabel = new GUIContent("Свернуть", "Свернуть повторяющиеся строки.");

	// Token: 0x0200082E RID: 2094
	private struct Log
	{
		// Token: 0x04003145 RID: 12613
		public string message;

		// Token: 0x04003146 RID: 12614
		public string stackTrace;

		// Token: 0x04003147 RID: 12615
		public LogType type;
	}
}
