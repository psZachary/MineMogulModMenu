using System.Collections.Generic;
using UnityEngine;

namespace MineMogulModMenu {
    public struct BoundingBoxEntry {
        public Renderer Target;
        public Object Object;
        public int IdentifierGroup;
        public bool Enabled;
        public Color DrawColor;
        public BoundingBoxEntry() {
            IdentifierGroup = 0;
            Object = null;
            Target = null;
            Enabled = true;
            DrawColor = Color.white;
        }
    }
    public class BoundingBoxManager : MonoBehaviour
    {
        public static List<BoundingBoxEntry> BoundingBoxEntries = new List<BoundingBoxEntry>();
        private Material mat;

        void Awake()
        {
            mat = new Material(Shader.Find("Hidden/Internal-Colored"));
            mat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
        }
        public static void TryAddBoundingBoxEntry(BoundingBoxEntry newEntry) {
            bool hasObjectEntry = BoundingBoxEntries.Exists(entry => entry.Object == newEntry.Object);
            bool hasRendererEntry = BoundingBoxEntries.Exists(entry => entry.Target == newEntry.Target);
            if (!hasObjectEntry && !hasRendererEntry)
                BoundingBoxEntries.Add(newEntry);
        }
        public static void DisableBoundingBoxByObject(Object obj) {
            int index = BoundingBoxEntries.FindIndex(entry => entry.Object == obj);
            if (index >= 0) {
                var entry = BoundingBoxEntries[index];
                entry.Enabled = false;
                BoundingBoxEntries[index] = entry;
            }
        }

        public static void EnableBoundingBoxByObject(Object obj) {
            int index = BoundingBoxEntries.FindIndex(entry => entry.Object == obj);
            if (index >= 0) {
                var entry = BoundingBoxEntries[index];
                entry.Enabled = true;
                BoundingBoxEntries[index] = entry;
            }
        }
        public static void DisableBoundingBoxByRenderer(Renderer renderer) {
            int index = BoundingBoxEntries.FindIndex(entry => entry.Target == renderer);
            if (index >= 0) {
                var entry = BoundingBoxEntries[index];
                entry.Enabled = false;
                BoundingBoxEntries[index] = entry;
            }
        }
        public static void EnableBoundingBoxByRenderer(Renderer renderer) {
            int index = BoundingBoxEntries.FindIndex(entry => entry.Target == renderer);
            if (index >= 0) {
                var entry = BoundingBoxEntries[index];
                entry.Enabled = true;
                BoundingBoxEntries[index] = entry;
            }
        }

        public static void DisableAllBoundingBoxesExceptObject(Object obj) {
            int index = BoundingBoxEntries.FindIndex(entry => entry.Object == obj);
            for (int i = 0; i < BoundingBoxEntries.Count; i++) {
                if (i == index) continue;

                var entry = BoundingBoxEntries[i]; 
                entry.Enabled = false;
                BoundingBoxEntries[i] = entry;
            }
        }

        public static void DisableAllBoundingBoxesByIdentifierGroup(int idGroup) {
            for (int i = 0; i < BoundingBoxEntries.Count; i++) {
                if (BoundingBoxEntries[i].IdentifierGroup == idGroup) {
                    var entry = BoundingBoxEntries[i];
                    entry.Enabled = false;
                    BoundingBoxEntries[i] = entry;
                }
            }
        }

        public static void EnableAllBoundingBoxesByIdentifierGroup(int idGroup) {
            for (int i = 0; i < BoundingBoxEntries.Count; i++) {
                if (BoundingBoxEntries[i].IdentifierGroup == idGroup) {
                    var entry = BoundingBoxEntries[i];
                    entry.Enabled = true;
                    BoundingBoxEntries[i] = entry;
                }
            }
        }

        public static BoundingBoxEntry? GetBoundingBoxEntryByRenderer(Renderer renderer) {
            int index = BoundingBoxEntries.FindIndex(entry => entry.Target == renderer);
            return index >= 0 ? BoundingBoxEntries[index] : null;
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

            mat.SetPass(0);
            GL.PushMatrix();
            GL.Begin(GL.LINES);

            foreach (BoundingBoxEntry entry in BoundingBoxEntries)
                if (entry.Target != null && entry.Enabled == true && entry.Object != null) {
                    GL.Color(entry.DrawColor);
                    DrawBounds(entry.Target.bounds);
                }

            GL.End();
            GL.PopMatrix();
        }
    }
}