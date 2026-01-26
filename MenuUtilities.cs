using UnityEngine;

namespace MineMogulModMenu {
    public class MenuUtilities : MonoBehaviour {
        private static bool stylesInit = false;
        public static bool IsDragging {get; private set;}
        public static Vector2 DragOffset {get; private set;}
        public static GUIStyle WindowStyle {get; private set;}
        public static GUIStyle ButtonStyle {get; private set;}
        public static GUIStyle HeaderStyle {get; private set;}
        public static GUIStyle ToggleStyle {get; private set;}
        public static GUIStyle TextFieldStyle {get; private set;}
        public static GUIStyle TextAreaStyle {get; private set;}
        public static GUIStyle SliderStyle {get; private set;}
        public static GUIStyle SliderThumbStyle {get; private set;}
        private static Texture2D checkboxOff;
        private static Texture2D checkboxOn;
        public static void ResetStyles() {
            stylesInit = false;
        }
        public static void DragWindow(ref Rect windowRect) {
            Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            Rect titleRect = new Rect(windowRect.x, windowRect.y, windowRect.width, 30);

            if (Input.GetMouseButtonDown(0) && titleRect.Contains(mouse))
            {
                IsDragging = true;
                DragOffset = mouse - new Vector2(windowRect.x, windowRect.y);
            }

            if (Input.GetMouseButtonUp(0))
                IsDragging = false;

            if (IsDragging && Input.GetMouseButton(0))
            {
                windowRect.x = mouse.x - DragOffset.x;
                windowRect.y = mouse.y - DragOffset.y;
            }
        }
        private static Texture2D MakeTex(int w, int h, Color col)
        {
            Texture2D tex = new Texture2D(w, h);
            tex.hideFlags = HideFlags.HideAndDontSave;
            Color[] pix = new Color[w * h];
            for (int i = 0; i < pix.Length; i++) pix[i] = col;
            tex.SetPixels(pix);
            tex.Apply();
            return tex;
        }
        // Button functions
        public static bool Button(string label)
        {
            return GUILayout.Button(label, ButtonStyle);
        }

        public static bool Button(string label, float width)
        {
            return GUILayout.Button(label, ButtonStyle, GUILayout.Width(width));
        }

        public static bool Button(string label, float width, float height)
        {
            return GUILayout.Button(label, ButtonStyle, GUILayout.Width(width), GUILayout.Height(height));
        }

        // Toggle function
        public static bool Toggle(bool value, string label)
        {
            Rect rect = GUILayoutUtility.GetRect(200, 20);

            Texture2D box = value ? checkboxOn : checkboxOff;
            if (GUI.Button(new Rect(rect.x, rect.y, 16, 16), box, GUIStyle.none))
                value = !value;

            GUI.Label(new Rect(rect.x + 22, rect.y, rect.width - 22, 16), label, HeaderStyle);

            return value;
        }

        public static void Label(string text)
        {
            GUILayout.Label(text, HeaderStyle);
        }

        public static void Label(string text, float width)
        {
            GUILayout.Label(text, HeaderStyle, GUILayout.Width(width));
        }

        // TextField functions
        public static string TextField(string text)
        {
            return GUILayout.TextField(text, TextFieldStyle);
        }

        public static string TextField(string text, float width)
        {
            return GUILayout.TextField(text, TextFieldStyle, GUILayout.Width(width));
        }

        // TextArea functions
        public static string TextArea(string text, float height)
        {
            return GUILayout.TextArea(text, TextAreaStyle, GUILayout.Height(height));
        }

        public static string TextArea(string text, float width, float height)
        {
            return GUILayout.TextArea(text, TextAreaStyle, GUILayout.Width(width), GUILayout.Height(height));
        }

        // Slider functions
        public static float HorizontalSlider(float value, float min, float max)
        {
            return GUILayout.HorizontalSlider(value, min, max, SliderStyle, SliderThumbStyle);
        }

        public static float HorizontalSlider(float value, float min, float max, float width)
        {
            return GUILayout.HorizontalSlider(value, min, max, SliderStyle, SliderThumbStyle, GUILayout.Width(width));
        }

        public static float HorizontalSlider(string label, float value, float min, float max)
        {
            GUILayout.BeginHorizontal();
            value = GUILayout.HorizontalSlider(value, min, max, SliderStyle, SliderThumbStyle);
            string labelText = $"{label}: {value:F2}";
            GUILayout.Label(labelText, HeaderStyle, GUILayout.Width(140f));
            GUILayout.EndHorizontal();
            return value;
        }

        public static float HorizontalSlider(string label, float value, float min, float max, float sliderWidth)
        {
            GUILayout.BeginHorizontal();
            value = GUILayout.HorizontalSlider(value, min, max, SliderStyle, SliderThumbStyle, GUILayout.Width(sliderWidth));
            string labelText = $"{label}: {value:F2}";
            GUILayout.Label(labelText, HeaderStyle, GUILayout.Width(140f));
            GUILayout.EndHorizontal();
            return value;
        }

        public static float HorizontalSlider(string label, float value, float min, float max, string format)
        {
            GUILayout.BeginHorizontal();
            value = GUILayout.HorizontalSlider(value, min, max, SliderStyle, SliderThumbStyle);
            string labelText = $"{label}: {value.ToString(format)}";
            GUILayout.Label(labelText, HeaderStyle, GUILayout.Width(140f));
            GUILayout.EndHorizontal();
            return value;
        }

        public static float HorizontalSlider(string label, float value, float min, float max, float sliderWidth, string format)
        {
            GUILayout.BeginHorizontal();
            value = GUILayout.HorizontalSlider(value, min, max, SliderStyle, SliderThumbStyle, GUILayout.Width(sliderWidth));
            string labelText = $"{label}: {value.ToString(format)}";
            GUILayout.Label(labelText, HeaderStyle, GUILayout.Width(140f));
            GUILayout.EndHorizontal();
            return value;
        }

        // Box/Panel functions
        public static void BeginBox()
        {
            GUILayout.BeginVertical(WindowStyle);
        }

        public static void EndBox()
        {
            GUILayout.EndVertical();
        }

        // Horizontal/Vertical layout helpers
        public static void BeginHorizontal()
        {
            GUILayout.BeginHorizontal();
        }

        public static void EndHorizontal()
        {
            GUILayout.EndHorizontal();
        }

        public static void BeginVertical()
        {
            GUILayout.BeginVertical();
        }

        public static void EndVertical()
        {
            GUILayout.EndVertical();
        }

        // Spacing utilities
        public static void Space(float pixels)
        {
            GUILayout.Space(pixels);
        }

        public static void FlexibleSpace()
        {
            GUILayout.FlexibleSpace();
        }

        // Separator line
        public static void Separator()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        }
        private static Texture2D MakeCheckbox(int w, int h, Color bgColor, bool isChecked)
        {
            Texture2D tex = new Texture2D(w, h);
            tex.hideFlags = HideFlags.HideAndDontSave;
            Color border = new Color(0.4f, 0.2f, 0.6f, 1f);
            Color check = Color.white;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                        tex.SetPixel(x, y, border);
                    else
                        tex.SetPixel(x, y, bgColor);
                }
            }

            if (isChecked)
            {
                for (int i = 3; i < w - 3; i++)
                {
                    tex.SetPixel(i, i, check);
                    tex.SetPixel(i, i - 1, check);
                    tex.SetPixel(w - 1 - i, i, check);
                    tex.SetPixel(w - 1 - i, i - 1, check);
                }
            }

            tex.Apply();
            return tex;
        }
        public static void InitStyles()
        {
            if (stylesInit) return;

            checkboxOff = MakeCheckbox(16, 16, new Color(0.18f, 0.18f, 0.18f, 1f), false);
            checkboxOn = MakeCheckbox(16, 16, new Color(0.5f, 0.2f, 0.7f, 1f), true);

            Font usingFont = Font.CreateDynamicFontFromOSFont("Consolas", 16);

            WindowStyle = new GUIStyle(GUI.skin.window);
            WindowStyle.normal.background = MakeTex(1, 1, new Color(0.1f, 0.1f, 0.1f, 0.98f));
            WindowStyle.onNormal.background = WindowStyle.normal.background;
            WindowStyle.normal.textColor = new Color(0.757f, 0.6f, 1f);
            WindowStyle.font = usingFont;
            WindowStyle.fontSize = 16;
            WindowStyle.fontStyle = FontStyle.Bold;
            WindowStyle.padding = new RectOffset(10, 10, 10, 10);
            WindowStyle.contentOffset = Vector2.zero;
            WindowStyle.alignment = TextAnchor.UpperCenter;

            ButtonStyle = new GUIStyle(GUI.skin.button);
            ButtonStyle.normal.background = MakeTex(1, 1, new Color(0.18f, 0.18f, 0.18f, 1f));
            ButtonStyle.hover.background = MakeTex(1, 1, new Color(0.25f, 0.15f, 0.35f, 1f));
            ButtonStyle.active.background = MakeTex(1, 1, new Color(0.5f, 0.2f, 0.7f, 1f));
            ButtonStyle.normal.textColor = Color.white;
            ButtonStyle.hover.textColor = new Color(0.9f, 0.6f, 1f);
            ButtonStyle.active.textColor = Color.white;
            ButtonStyle.fontSize = 12;
            ButtonStyle.fontStyle = FontStyle.Bold;
            ButtonStyle.padding = new RectOffset(10, 10, 6, 6);
            ButtonStyle.font = usingFont;

            HeaderStyle = new GUIStyle(GUI.skin.label);
            HeaderStyle.normal.textColor = new Color(1f, 1f, 1f);
            HeaderStyle.fontSize = 12;
            HeaderStyle.alignment = TextAnchor.MiddleLeft;
            HeaderStyle.font = usingFont;

            ToggleStyle = new GUIStyle(GUI.skin.toggle);
            ToggleStyle.normal.background = checkboxOff;
            ToggleStyle.onNormal.background = checkboxOn;
            ToggleStyle.hover.background = checkboxOff;
            ToggleStyle.onHover.background = checkboxOn;
            ToggleStyle.active.background = checkboxOff;
            ToggleStyle.onActive.background = checkboxOn;
            ToggleStyle.normal.textColor = Color.white;
            ToggleStyle.onNormal.textColor = new Color(0.9f, 0.6f, 1f);
            ToggleStyle.hover.textColor = new Color(0.9f, 0.6f, 1f);
            ToggleStyle.onHover.textColor = new Color(0.9f, 0.6f, 1f);
            ToggleStyle.fontSize = 14;
            ToggleStyle.fixedWidth = 16;
            ToggleStyle.fixedHeight = 16;
            ToggleStyle.margin = new RectOffset(4, 4, 4, 4);

            TextFieldStyle = new GUIStyle(GUI.skin.textField);
            TextFieldStyle.normal.background = MakeTex(1, 1, new Color(0.15f, 0.15f, 0.15f, 1f));
            TextFieldStyle.hover.background = MakeTex(1, 1, new Color(0.2f, 0.12f, 0.28f, 1f));
            TextFieldStyle.focused.background = MakeTex(1, 1, new Color(0.25f, 0.15f, 0.35f, 1f));
            TextFieldStyle.normal.textColor = Color.white;
            TextFieldStyle.hover.textColor = new Color(0.9f, 0.6f, 1f);
            TextFieldStyle.focused.textColor = new Color(0.9f, 0.6f, 1f);
            TextFieldStyle.fontSize = 12;
            TextFieldStyle.font = usingFont;
            TextFieldStyle.padding = new RectOffset(5, 5, 4, 4);
            TextFieldStyle.border = new RectOffset(2, 2, 2, 2);

            TextAreaStyle = new GUIStyle(GUI.skin.textArea);
            TextAreaStyle.normal.background = MakeTex(1, 1, new Color(0.15f, 0.15f, 0.15f, 1f));
            TextAreaStyle.hover.background = MakeTex(1, 1, new Color(0.2f, 0.12f, 0.28f, 1f));
            TextAreaStyle.focused.background = MakeTex(1, 1, new Color(0.25f, 0.15f, 0.35f, 1f));
            TextAreaStyle.normal.textColor = Color.white;
            TextAreaStyle.hover.textColor = new Color(0.9f, 0.6f, 1f);
            TextAreaStyle.focused.textColor = new Color(0.9f, 0.6f, 1f);
            TextAreaStyle.fontSize = 12;
            TextAreaStyle.font = usingFont;
            TextAreaStyle.padding = new RectOffset(5, 5, 4, 4);
            TextAreaStyle.border = new RectOffset(2, 2, 2, 2);
            TextAreaStyle.wordWrap = true;

            SliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
            SliderStyle.normal.background = MakeTex(1, 1, new Color(0.18f, 0.18f, 0.18f, 1f));
            SliderStyle.hover.background = MakeTex(1, 1, new Color(0.2f, 0.12f, 0.28f, 1f));
            SliderStyle.active.background = MakeTex(1, 1, new Color(0.25f, 0.15f, 0.35f, 1f));
            SliderStyle.fixedHeight = 4;
            SliderStyle.margin = new RectOffset(4, 4, 8, 8);

            SliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
            SliderThumbStyle.normal.background = MakeTex(1, 1, new Color(0.5f, 0.2f, 0.7f, 1f));
            SliderThumbStyle.hover.background = MakeTex(1, 1, new Color(0.6f, 0.3f, 0.8f, 1f));
            SliderThumbStyle.active.background = MakeTex(1, 1, new Color(0.7f, 0.4f, 0.9f, 1f));
            SliderThumbStyle.fixedWidth = 12;
            SliderThumbStyle.fixedHeight = 12;

            stylesInit = true;
        }
    }
}