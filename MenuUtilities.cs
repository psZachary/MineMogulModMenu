using UnityEngine;

namespace MineMogulModMenu {
    public class MenuUtilities : MonoBehaviour {
        public static bool IsDragging {get; private set;}
        public static Vector2 DragOffset {get; private set;}
        public static GUIStyle WindowStyle {get; private set;}
        public static GUIStyle ButtonStyle {get; private set;}
        public static GUIStyle SelectedButtonStyle {get; private set;}
        public static GUIStyle HeaderStyle {get; private set;}
        public static GUIStyle ToggleStyle {get; private set;}
        public static GUIStyle ToggleLabelStyle {get; private set;}
        public static GUIStyle TextFieldStyle {get; private set;}
        public static GUIStyle TextAreaStyle {get; private set;}
        public static GUIStyle SliderStyle {get; private set;}
        public static GUIStyle SliderThumbStyle {get; private set;}
        public static GUIStyle SelectionTableStyle {get; private set;}
        public static GUIStyle SelectionTableSelectedStyle {get; private set;}
        public static GUIStyle SelectionTableBorderStyle {get; private set;}
        public static GUIStyle SelectionTableLabelStyle {get; private set;}
        public static GUIStyle SeparatorStyle {get; private set;}
        private static Texture2D checkboxOffTexture;
        private static Texture2D checkboxOnTexture;
        private static bool stylesInit = false;

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

        public static bool Button(string label, bool selected)
        {
            GUIStyle style = selected ? SelectedButtonStyle : ButtonStyle;
            return GUILayout.Button(label, style);
        }

        public static bool Button(string label, float width)
        {
            return GUILayout.Button(label, ButtonStyle, GUILayout.Width(width));
        }

        public static bool Button(string label, float width, bool selected)
        {
            GUIStyle style = selected ? SelectedButtonStyle : ButtonStyle;
            return GUILayout.Button(label, style, GUILayout.Width(width));
        }

        public static bool Button(string label, float width, float height)
        {
            return GUILayout.Button(label, ButtonStyle, GUILayout.Width(width), GUILayout.Height(height));
        }

        public static bool Button(string label, float width, float height, bool selected)
        {
            GUIStyle style = selected ? SelectedButtonStyle : ButtonStyle;
            return GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        // Toggle function
        public static bool Toggle(bool value, string label)
        {
            Rect rect = GUILayoutUtility.GetRect(200, 20);

            Texture2D box = value ? checkboxOnTexture : checkboxOffTexture;
            if (GUI.Button(new Rect(rect.x, rect.y, 20, 20), box, GUIStyle.none))
                value = !value;

            GUI.Label(new Rect(rect.x + 26, rect.y, rect.width - 26, 20), label, ToggleLabelStyle);

            return value;
        }

        public static void Toggle(ref bool value, string label) {
            value = Toggle(value, label);
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

        // SelectionTable functions
        public static int SelectionTable(string label, string[] options, int selectedIndex)
        {
            return SelectionTable(label, options, selectedIndex, 150f, 10f);
        }

        public static int SelectionTable(string label, string[] options, int selectedIndex, float height)
        {
            return SelectionTable(label, options, selectedIndex, height, 10f);
        }

        public static int SelectionTable(string label, string[] options, int selectedIndex, float height, float padding)
        {
            // Draw label
            GUILayout.Label(label, SelectionTableLabelStyle);
            GUILayout.Space(2);

            // Begin border box
            GUILayout.BeginVertical(SelectionTableBorderStyle);
            GUILayout.Space(padding);

            // Begin inner content area with padding
            GUILayout.BeginHorizontal();
            GUILayout.Space(padding);
            GUILayout.BeginVertical(GUILayout.Height(height), GUILayout.ExpandWidth(true));

            // Draw selection options
            for (int i = 0; i < options.Length; i++)
            {
                GUIStyle style = (i == selectedIndex) ? SelectionTableSelectedStyle : SelectionTableStyle;

                if (GUILayout.Button(options[i], style, GUILayout.ExpandWidth(true)))
                {
                    selectedIndex = i;
                }
            }

            // End inner content area with padding
            GUILayout.EndVertical();
            GUILayout.Space(padding);
            GUILayout.EndHorizontal();

            GUILayout.Space(padding);
            GUILayout.EndVertical();

            return selectedIndex;
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

        // Separator line functions
        public static void Separator()
        {
            GUILayout.Box("", SeparatorStyle, GUILayout.ExpandWidth(true), GUILayout.Height(2));
        }

        public static void Separator(float height)
        {
            GUILayout.Box("", SeparatorStyle, GUILayout.ExpandWidth(true), GUILayout.Height(height));
        }

        public static void Separator(float height, float spacing)
        {
            GUILayout.Space(spacing);
            GUILayout.Box("", SeparatorStyle, GUILayout.ExpandWidth(true), GUILayout.Height(height));
            GUILayout.Space(spacing);
        }
        private static Texture2D MakeCheckbox(int w, int h, Color bgColor)
        {
            Texture2D tex = new Texture2D(w, h);
            tex.hideFlags = HideFlags.HideAndDontSave;
            Color border = new Color(0.4f, 0.2f, 0.6f, 1f);

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

            tex.Apply();
            return tex;
        }
        public static void InitStyles()
        {
            if (stylesInit) return;

            checkboxOffTexture = MakeCheckbox(20, 20, new Color(0.18f, 0.18f, 0.18f, 1f));
            checkboxOnTexture = MakeCheckbox(20, 20, new Color(0.5f, 0.2f, 0.7f, 1f));

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

            SelectedButtonStyle = new GUIStyle(ButtonStyle);
            SelectedButtonStyle.normal.background = MakeTex(1, 1, new Color(0.5f, 0.2f, 0.7f, 1f));
            SelectedButtonStyle.hover.background = MakeTex(1, 1, new Color(0.6f, 0.3f, 0.8f, 1f));
            SelectedButtonStyle.active.background = MakeTex(1, 1, new Color(0.7f, 0.4f, 0.9f, 1f));
            SelectedButtonStyle.normal.textColor = Color.white;
            SelectedButtonStyle.hover.textColor = Color.white;
            SelectedButtonStyle.active.textColor = Color.white;

            HeaderStyle = new GUIStyle(GUI.skin.label);
            HeaderStyle.normal.textColor = new Color(1f, 1f, 1f);
            HeaderStyle.fontSize = 12;
            HeaderStyle.alignment = TextAnchor.MiddleLeft;
            HeaderStyle.font = usingFont;

            ToggleStyle = new GUIStyle(GUI.skin.toggle);
            ToggleStyle.normal.background = checkboxOffTexture;
            ToggleStyle.onNormal.background = checkboxOnTexture;
            ToggleStyle.hover.background = checkboxOffTexture;
            ToggleStyle.onHover.background = checkboxOnTexture;
            ToggleStyle.active.background = checkboxOffTexture;
            ToggleStyle.onActive.background = checkboxOnTexture;
            ToggleStyle.normal.textColor = Color.white;
            ToggleStyle.onNormal.textColor = new Color(0.9f, 0.6f, 1f);
            ToggleStyle.hover.textColor = new Color(0.9f, 0.6f, 1f);
            ToggleStyle.onHover.textColor = new Color(0.9f, 0.6f, 1f);
            ToggleStyle.fontSize = 16;
            ToggleStyle.fixedWidth = 20;
            ToggleStyle.fixedHeight = 20;
            ToggleStyle.margin = new RectOffset(4, 4, 4, 4);

            ToggleLabelStyle = new GUIStyle(HeaderStyle);
            ToggleLabelStyle.fontSize = 14;
            ToggleLabelStyle.alignment = TextAnchor.MiddleLeft;

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
            SliderStyle.fixedHeight = 8;
            SliderStyle.margin = new RectOffset(4, 4, 8, 8);

            SliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
            SliderThumbStyle.normal.background = MakeTex(1, 1, new Color(0.5f, 0.2f, 0.7f, 1f));
            SliderThumbStyle.hover.background = MakeTex(1, 1, new Color(0.6f, 0.3f, 0.8f, 1f));
            SliderThumbStyle.active.background = MakeTex(1, 1, new Color(0.7f, 0.4f, 0.9f, 1f));
            SliderThumbStyle.fixedWidth = 8;
            SliderThumbStyle.fixedHeight = 6;

            SelectionTableStyle = new GUIStyle(GUI.skin.button);
            SelectionTableStyle.normal.background = MakeTex(1, 1, new Color(0.15f, 0.15f, 0.15f, 1f));
            SelectionTableStyle.hover.background = MakeTex(1, 1, new Color(0.2f, 0.12f, 0.28f, 1f));
            SelectionTableStyle.active.background = MakeTex(1, 1, new Color(0.25f, 0.15f, 0.35f, 1f));
            SelectionTableStyle.normal.textColor = Color.white;
            SelectionTableStyle.hover.textColor = new Color(0.9f, 0.6f, 1f);
            SelectionTableStyle.active.textColor = Color.white;
            SelectionTableStyle.fontSize = 12;
            SelectionTableStyle.fontStyle = FontStyle.Normal;
            SelectionTableStyle.padding = new RectOffset(8, 8, 6, 6);
            SelectionTableStyle.font = usingFont;
            SelectionTableStyle.alignment = TextAnchor.MiddleLeft;

            SelectionTableSelectedStyle = new GUIStyle(SelectionTableStyle);
            SelectionTableSelectedStyle.normal.background = MakeTex(1, 1, new Color(0.5f, 0.2f, 0.7f, 1f));
            SelectionTableSelectedStyle.hover.background = MakeTex(1, 1, new Color(0.6f, 0.3f, 0.8f, 1f));
            SelectionTableSelectedStyle.active.background = MakeTex(1, 1, new Color(0.7f, 0.4f, 0.9f, 1f));
            SelectionTableSelectedStyle.normal.textColor = Color.white;
            SelectionTableSelectedStyle.hover.textColor = Color.white;
            SelectionTableSelectedStyle.active.textColor = Color.white;
            SelectionTableSelectedStyle.fontStyle = FontStyle.Bold;

            SelectionTableBorderStyle = new GUIStyle();
            SelectionTableBorderStyle.normal.background = MakeTex(1, 1, new Color(0.18f, 0.18f, 0.18f, 1f));
            SelectionTableBorderStyle.border = new RectOffset(2, 2, 2, 2);
            SelectionTableBorderStyle.padding = new RectOffset(0, 0, 0, 0);

            SelectionTableLabelStyle = new GUIStyle(HeaderStyle);
            SelectionTableLabelStyle.fontSize = 14;

            SeparatorStyle = new GUIStyle();
            SeparatorStyle.normal.background = MakeTex(1, 1, new Color(0.18f, 0.18f, 0.18f, 1f));
            SeparatorStyle.margin = new RectOffset(0, 0, 0, 0);
            SeparatorStyle.padding = new RectOffset(0, 0, 0, 0);

            stylesInit = true;
        }
    }
}