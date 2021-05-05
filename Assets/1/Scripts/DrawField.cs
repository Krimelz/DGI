using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DrawField : MonoBehaviour
{
    public Text resultText;
    public Cell[] cells;

    private string path;
    private List<Picture> pictures = new List<Picture>();

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "1", "JSONFiles");
        cells = GetComponentsInChildren<Cell>();
    }

    public void Save()
    {
        SaveToFile(ReadFromField(), 0.1f);
    }

    public void Check()
    {
        LoadFiles();

        float[,] w = CalcW(pictures.ToArray());
        float[] y = ReadFromField();

        y = Multiply(w, y);
        y = Activate(y);

        float r = float.MaxValue;

        for (int i = 0; i < pictures.Count; ++i)
        {
            r = Mathf.Min(CalcDist(pictures[i].vector, y), r);
        }

        if (Mathf.Abs(r - 16f) <= 0.1f)
        {
            resultText.text = "YES = " + r.ToString();
        }
        else
        {
            resultText.text = "NO = " + r.ToString();
        }

        pictures.Clear();
    }

    private float[] ReadFromField()
    {
        float[] result = new float[cells.Length];

        for (int i = 0; i < result.Length; ++i)
        {
            if (cells[i].IsFill)
            {
                result[i] = 1f;
            }
            else
            {
                result[i] = -1f;
            }
        }

        return result;
    }

    private void LoadFiles()
    {
        string[] files = Directory.GetFiles(path, "*.json");

        foreach (string file in files)
        {
            string data = File.ReadAllText(file);
            pictures.Add(JsonUtility.FromJson<Picture>(data));
        }
    }

    private void SaveToFile(float[] vector, float weight)
    {
        Picture picture = new Picture();
        picture.weight = weight;
        picture.vector = vector;

        string path = Path.Combine(this.path, DateTime.Now.Ticks + ".json");
        string data = JsonUtility.ToJson(picture, true);

        Debug.Log("Saved to -> " + path);

        File.WriteAllText(path, data);
    }

    private float[,] CalcW(Picture[] pictures)
    {
        float[,] w = new float[10, 10];

        for (int i = 0; i < pictures.Length; ++i)
        {
            float[,] tmp = Multiply(pictures[i].vector, pictures[i].vector);
            tmp = Multiply(tmp, pictures[i].weight);
            Add(ref w, tmp);
        }

        return w;
    }

    private float[,] Multiply(float[] t, float[] v)
    {
        float[,] result = new float[v.Length, v.Length];

        for (int i = 0; i < t.Length; ++i)
        {
            for (int j = 0; j < v.Length; ++j)
            {
                result[i, j] = t[i] * v[j];
            }
        }

        return result;
    }

    private float[,] Multiply(float[,] matrix, float w)
    {
        float[,] result = matrix;

        for (int i = 0; i < matrix.GetLength(0); ++i)
        {
            for (int j = 0; j < matrix.GetLength(1); ++j)
            {
                matrix[i, j] *= w;
            }
        }

        return result;
    }

    private float[] Multiply(float[,] w, float[] v)
    {
        float[] result = new float[v.Length];

        for (int i = 0; i < result.Length; ++i)
        {
            result[i] = 0f;
        }

        for (int i = 0; i < w.GetLength(0); ++i)
        {
            for (int j = 0; j < w.GetLength(1); ++j)
            {
                result[i] += w[i, j] * v[j];
            }
        }

        return result;
    }

    private void Add(ref float[,] firstMatrix, float[,] secondMatrix)
    {
        for (int i = 0; i < firstMatrix.GetLength(0); ++i)
        {
            for (int j = 0; j < firstMatrix.GetLength(1); ++j)
            {
                firstMatrix[i, j] += secondMatrix[i, j];
            }
        }
    }

    private float[] Activate(float[] vector)
    {
        float[] result = vector;

        for (int i = 0; i < vector.Length; ++i)
        {
            result[i] = Sign(vector[i]);
        }

        return result;
    }

    private float Sign(float x)
    {
        if (x >= 0f)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }

    private float CalcDist(float[] x, float[] y)
    {
        float result = 0f;

        for (int i = 0; i < y.Length; ++i)
        {
            result += Mathf.Pow(x[i] - y[i], 2f);
        }

        result = Mathf.Sqrt(result);

        Debug.Log(result);

        return result;
    }

}

[Serializable]
public class Picture
{
    public float weight;
    public float[] vector;
}
