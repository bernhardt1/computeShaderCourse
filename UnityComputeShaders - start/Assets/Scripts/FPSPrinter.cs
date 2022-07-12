using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSPrinter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI averageFpsText;
    
    private float deltaTime;
    private float lastFps = 60;

    private float[] fpsSamples;
    private int index = 0;


    void Start()
    {

        fpsSamples = new float[60]; // 1 minute rolling average

        Invoke(nameof(SampleFPS), 1.0f);
    }

    void Update ()
    {
        if (Time.timeScale == 1f)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            if (fps > 60) fps = 60f;
            if (fps < 0) fps = 0f;
            
            lastFps = fps;

            fpsText.text = "FPS: " + (lastFps).ToString ();
        }
    }

    void SampleFPS()
    {

        if (Time.timeScale == 1f)
        {
            fpsSamples[index] = lastFps;
            index = (index + 1) % fpsSamples.Length;

            
            PrintFPSAverage();
        }

        Invoke(nameof(SampleFPS), 1.0f);
    }

    void PrintFPSAverage()
    {
        float sum = 0;
        int setCount = 1;

        foreach (float i in fpsSamples)
        {
            if ( i != 0) setCount++;
            sum += i;
        }
        if (setCount > fpsSamples.Length) setCount = fpsSamples.Length;

        int averageFPS = (int) (sum / setCount);

        averageFpsText.text = "AVG: " + (averageFPS).ToString ();
    }
}
