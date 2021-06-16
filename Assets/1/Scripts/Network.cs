using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Network : MonoBehaviour {

	[SerializeField]
	private float bound = 4f;
	[SerializeField]
	private DrawField field;
	[SerializeField]
	private Text resultText;

	private string path;
	private List<Picture> xs = new List<Picture>();
	private float weight;

	private void Start() {
		path = Path.Combine(Application.dataPath, "1", "JSONFiles");
	}

	public void Check() {
		LoadFiles();
		Calculate();
	}

	private void Calculate() {
		weight = 1f / xs.Count;

		float[,] w = new float[100, 100];
		foreach (var x in xs) {
			w = Sum(Multi(Multi(x.vector, x.vector), weight), w);
		}

		float[] y = new float[100];
		y = field.ReadFromField();

		float[] y0 = new float[100];
		y0 = Multi(w, y);
		y0 = Norm(y0);

		float[] r = new float[xs.Count];

		for (int i = 0; i < xs.Count; i++) {
			r[i] = Dist(xs[i].vector, y0);
		}

		float rMin = Mathf.Min(r);

		if (rMin <= bound) {
			resultText.text = "YES";
		} else {
			resultText.text = "NO";
		}

		Debug.Log(rMin);
	}

	private void LoadFiles() {
		string[] files = Directory.GetFiles(path, "*.json");

		foreach (string file in files) {
			string data = File.ReadAllText(file);
			xs.Add(JsonUtility.FromJson<Picture>(data));
		}
	}

	private float[,] Multi(float[] col, float[] row) {
		float[,] matrix = new float[col.Length, row.Length];

		for (int i = 0; i < col.Length; i++) {
			for (int j = 0; j < row.Length; j++) {
				matrix[i, j] = col[i] * row[j];
			}
		}

		return matrix;
	}

	private float[,] Multi(float[,] matrix, float num) {
		float[,] multi = new float[matrix.GetLength(0), matrix.GetLength(1)];
		Array.Copy(matrix, multi, matrix.GetLength(0) * matrix.GetLength(1));

		for (int i = 0; i < matrix.GetLength(0); i++) {
			for (int j = 0; j < matrix.GetLength(1); j++) {
				multi[i, j] *= num;
			}
		}

		return multi;
	}

	private float[] Multi(float[,] matrix, float[] vector) {
		float[] multi = new float[matrix.GetLength(0)];

		for (int i = 0; i < matrix.GetLength(0); i++) {
			for (int j = 0; j < matrix.GetLength(1); j++) {
				multi[i] += matrix[i, j] * vector[j];
			}
		}

		return multi;
	}

	private float[,] Sum(float[,] matrix1, float[,] matrix2) {
		float[,] summed = new float[matrix1.GetLength(0), matrix1.GetLength(1)];

		for (int i = 0; i < matrix1.GetLength(0); i++) {
			for (int j = 0; j < matrix1.GetLength(1); j++) {
				summed[i, j] = matrix1[i, j] + matrix2[i, j];
			}
		}

		return summed;
	}

	private float Dist(float[] vector1, float[] vector2) {
		float result = 0f;

		for (int i = 0; i < vector1.Length; i++) {
			result += Mathf.Pow(vector1[i] - vector2[i], 2f);
		}

		result = Mathf.Sqrt(result);

		return result;
	}

	private float[] Norm(float[] vector) {
		float[] norm = new float[vector.Length];
		Array.Copy(vector, norm, vector.Length);

		float invertLen = 1f / Len(vector);

		for (int i = 0; i < norm.Length; i++) {
			
			if (norm[i] >= 0) {
				norm[i] = 1f;
			} else {
				norm[i] = -1f;
			}
			

			//norm[i] = norm[i] * invertLen;
		}

		return norm;
	}

	private float Len(float[] vector) {
		float result = 0f;

		for (int i = 0; i < vector.Length; i++) {
			result += Mathf.Pow(vector[i], 2f);
		}

		result = Mathf.Sqrt(result);

		return result;
	}
}
