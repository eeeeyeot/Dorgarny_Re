using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTextEffect : MonoBehaviour {
    public Text taptext;

    private Color color;
    private float alpha;
    

    void Start () {
        color = taptext.color;
        alpha = 0.02f;
	}
	
	void Update () {
        TextEffect();
    }

    private void TextEffect()
    {
        if (color.a < 0.05f || color.a >= 1.0f)
        {
            alpha *= -1;
        }

        color.a += alpha;
        taptext.color = color;
    }
}
