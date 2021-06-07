using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Caesar : MonoBehaviour {
    public string charsString;

    public InputField decode;
    public InputField encode;

    public GameObject charPrefab;
    public Transform originalContainer;
    public Transform changedContainer;

    private List<char> original = new List<char>();
    private List<char> changed = new List<char>();

    private List<Text> chars = new List<Text>();

    private bool isEncode;

    private void Start() {
        Init();
        SpawnChars();
    }

    private void Init () {
        original.AddRange(charsString.ToCharArray());
        changed.AddRange(charsString.ToCharArray());
    }

    private void SpawnChars() {
        for (int i = 0; i < original.Count; i++) {
            GameObject go = Instantiate(charPrefab, originalContainer);
            go.GetComponentInChildren<Text>().text = original[i].ToString();
        }

        for (int i = 0; i < changed.Count; i++) {
            GameObject go = Instantiate(charPrefab, changedContainer);
            Text text = go.GetComponentInChildren<Text>();
            text.text = original[i].ToString();
            chars.Add(text);
        }
    }

    public void UpdateChars() {
        for (int i = 0; i < changed.Count; i++) {
            chars[i].text = changed[i].ToString();
        }
    }

    private void Encode() {
        string text = encode.text;
        string encoded = string.Empty;

        for (int i = 0; i < text.Length; i++) {
            int index = original.IndexOf(text[i]);
            encoded += changed[index].ToString();
        }

        decode.text = encoded;
    }

    private void Decode() {
        string text = decode.text;
        string decoded = string.Empty;

        for (int i = 0; i < text.Length; i++) {
            int index = changed.IndexOf(text[i]);
            decoded += original[index].ToString();
        }

        encode.text = decoded;
    }

    public void SetEncode(bool value) {
        isEncode = value;
    }

    public void UpdateText() {
        if (isEncode) {
            Encode();
        } else {
            Decode();
        }
    }

    public void ToLeft() {
        char tmp = changed[0];

        for (int i = 0; i < changed.Count - 1; i++) {
            changed[i] = changed[i + 1];
        }

        changed[changed.Count - 1] = tmp;
    }

    public void ToRight() {
        char tmp = changed[changed.Count - 1];

        for (int i = changed.Count - 1; i > 0; i--) {
            changed[i] = changed[i - 1];
        }

        changed[0] = tmp;
    }
}
