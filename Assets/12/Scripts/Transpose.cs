using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transpose : MonoBehaviour {
    public InputField encode;
    public InputField decode;
    public InputField length;

    private int len = 1;

    public void SetLength() {
        len = int.Parse(length.text);
    }

    // TODO
    public void Encode() {
        string text = encode.text;
        string encoded = string.Empty;
        int strlen = text.Length;

        for (int i = 0; i < strlen; i++) {
            int index = i * len % strlen;
            encoded += text[index + i * len / strlen];
        }

        decode.text = encoded;
    }
}
