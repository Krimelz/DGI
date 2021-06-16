using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawField : MonoBehaviour {

	private Cell[] cells;
	private string path;

	private void Start() {
		path = Path.Combine(Application.dataPath, "1", "JSONFiles");
		cells = GetComponentsInChildren<Cell>();
	}

	public void Save() {
		SaveToFile(ReadFromField());
	}

	public void ClearField() {
		for (int i = 0; i < cells.Length; i++) {
			cells[i].Clear();
		}
	}

	public float[] ReadFromField() {
		float[] result = new float[cells.Length];

		for (int i = 0; i < result.Length; ++i) {
			if (cells[i].IsFill) {
				result[i] = 1f;
			} else {
				result[i] = -1f;
			}
		}

		return result;
	}

	private void SaveToFile(float[] vector) {
		Picture picture = new Picture(vector);

		string name = DateTime.Now.Ticks + ".json";
		string path = Path.Combine(this.path, name);
		string data = JsonUtility.ToJson(picture, true);

		Debug.Log("Saved to -> " + path);

		File.WriteAllText(path, data);
	}
}

[Serializable]
public class Picture {
	public float[] vector;

	public Picture(float[] vector) {
		this.vector = vector;
	}
}
