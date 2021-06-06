using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragonCurve : MonoBehaviour
{
    public int levels = 15;

    private LineRenderer line;
    private List<Vector3> dots = new List<Vector3>();

    private void Start() {
        line = GetComponent<LineRenderer>();

        Calculate();
    }

    private void Init() {
        dots.Clear();
        dots.Add(new Vector3(0f, 0f));
        dots.Add(new Vector3(0f, 1f));
    }

    private void Generate() {

        float inv = 1f;

        for (int i = 0; i < dots.Count - 1; i++) { 
            float x = (dots[i].x + dots[i + 1].x) / 2f + (inv * ((dots[i + 1].y - dots[i].y) / 2f));
            float y = (dots[i].y + dots[i + 1].y) / 2f - (inv * ((dots[i + 1].x - dots[i].x) / 2f));

            Vector2 pn = new Vector2(x, y);

            dots.Insert(i + 1, pn);
            inv *= -1f;
            i++;
        }
    }

    private void Draw() {
        line.positionCount = dots.Count;
        line.SetPositions(dots.ToArray());
    }

    public void Calculate() {
        Init();

        for (int i = 0; i < levels; i++) {
            Generate();
        }

        Draw();
    }
}
