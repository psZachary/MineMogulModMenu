using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MineMogulModMenu
{
    public enum DrawType
    {
        BoundingBox
    }
    public struct DrawingEntry(DrawType drawType, Renderer[] renderers, Color color)
    {
        public DrawType DrawType = drawType;
        public Renderer[] Renderers = renderers;
        public Color Color = color;
    }
    public class DrawingManager : MonoBehaviour
    {
        public static List<DrawingEntry> NextDrawEntries = [];
        private Material mat;

        void Awake()
        {
            mat = new Material(Shader.Find("Hidden/Internal-Colored"));
            mat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
        }

        void DrawBounds(Bounds b)
        {
            var min = b.min;
            var max = b.max;
            // Bottom
            GL.Vertex3(min.x, min.y, min.z); GL.Vertex3(max.x, min.y, min.z);
            GL.Vertex3(max.x, min.y, min.z); GL.Vertex3(max.x, min.y, max.z);
            GL.Vertex3(max.x, min.y, max.z); GL.Vertex3(min.x, min.y, max.z);
            GL.Vertex3(min.x, min.y, max.z); GL.Vertex3(min.x, min.y, min.z);

            // Top
            GL.Vertex3(min.x, max.y, min.z); GL.Vertex3(max.x, max.y, min.z);
            GL.Vertex3(max.x, max.y, min.z); GL.Vertex3(max.x, max.y, max.z);
            GL.Vertex3(max.x, max.y, max.z); GL.Vertex3(min.x, max.y, max.z);
            GL.Vertex3(min.x, max.y, max.z); GL.Vertex3(min.x, max.y, min.z);

            // Verticals
            GL.Vertex3(min.x, min.y, min.z); GL.Vertex3(min.x, max.y, min.z);
            GL.Vertex3(max.x, min.y, min.z); GL.Vertex3(max.x, max.y, min.z);
            GL.Vertex3(max.x, min.y, max.z); GL.Vertex3(max.x, max.y, max.z);
            GL.Vertex3(min.x, min.y, max.z); GL.Vertex3(min.x, max.y, max.z);
        }

        void OnRenderObject()
        {

            // Only render for the main camera to avoid duplicate rendering
            Camera currentCamera = Camera.current;
            if (currentCamera == null || currentCamera != Camera.main)
                return;

            if (Config.Instance.Settings.HighlightThroughWalls)
                mat.SetPass(0);

            GL.PushMatrix();
            GL.Begin(GL.LINES);

            foreach (DrawingEntry entry in NextDrawEntries)
            {
                if (entry.Renderers.Count() <= 0)
                    continue;

                GL.Color(entry.Color);
                if (entry.DrawType == DrawType.BoundingBox)
                {
                    foreach (Renderer r in entry.Renderers)
                    {
                        if (r)
                        {
                            DrawBounds(r.bounds);
                        }
                    }
                }
            }
            NextDrawEntries.Clear();

            GL.End();
            GL.PopMatrix();
        }
    }
}