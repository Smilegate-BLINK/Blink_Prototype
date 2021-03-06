using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Gamestrap
{
    [AddComponentMenu("UI/Gamestrap/Effects/Perspective")]
    public class PerspectiveEffect : GamestrapEffect
    {
        public float perspective = 0f;

        public override void ModifyVerticesWrapper(List<UIVertex> vertexList)
        {
            if (perspective != 0)
                ApplyPerspective(vertexList, 0, vertexList.Count);
        }

        public void ApplyPerspective(List<UIVertex> verts, int start, int end)
        {
            UIVertex vt;
            float bottomPos = verts.Min(t => t.position.y);
            float topPos = verts.Max(t => t.position.y);
            float height = topPos - bottomPos;

            float leftPos = verts.Min(t => t.position.x);
            float rightPos = verts.Max(t => t.position.x);
            float middleX = leftPos + (rightPos - leftPos) / 2f;
            for (int i = start; i < end; i++)
            {
                vt = verts[i];
                Vector3 v = vt.position;
                float percentage = Mathf.Lerp(perspective, 1, (vt.position.y - bottomPos) / height);
                float offset = (v.x - middleX) * percentage;
                v.x = middleX + offset;
                vt.position = v;
                verts[i] = vt;
            }
        }
    }
}
