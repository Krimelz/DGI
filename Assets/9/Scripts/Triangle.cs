using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour {

	[SerializeField]
	private GameObject triangle;
	[SerializeField]
	private Transform first;
	[SerializeField]
	private Transform second;
	[SerializeField]
	private Transform third;

	[SerializeField, Min(1)]
	private int level = 1;

	private void Start() {
		if (level > 1) {
			SpawnTriangles();
			Destroy(gameObject);
		}
	}

	private void SpawnTriangles() {
		GameObject triangle1 = Instantiate(triangle, first.position, Quaternion.identity);
		GameObject triangle2 = Instantiate(triangle, second.position, Quaternion.identity);
		GameObject triangle3 = Instantiate(triangle, third.position, Quaternion.identity);

		triangle1.GetComponent<Triangle>().SetLevel(level - 1);
		triangle2.GetComponent<Triangle>().SetLevel(level - 1);
		triangle3.GetComponent<Triangle>().SetLevel(level - 1);

		triangle1.transform.localScale *= 0.5f;
		triangle2.transform.localScale *= 0.5f;
		triangle3.transform.localScale *= 0.5f;
	}

	public void SetLevel(int level) {
		this.level = level;
	}
}
