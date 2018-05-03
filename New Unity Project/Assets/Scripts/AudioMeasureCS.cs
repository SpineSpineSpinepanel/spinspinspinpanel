using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMeasureCS : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject objCircleBg;

    public float RmsValue;
    public float DbValue;
    public float PitchValue;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    private float _visualModiFier = 0.15f;
    private float _smoothSpeed = 1f;
    private float _visualScale = 0f;

    void Start()
    {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
    }

    void Update()
    {
        AnalyzeSound();
        updateVisual();
    }

    void updateVisual()
    {
        int spectrumIndex = 0;
        float sum = 0f;
        for (int i = 0; i < QSamples; ++i)
        {
            sum += _spectrum[spectrumIndex];
            spectrumIndex++;
        }
        float Scale = sum * _visualModiFier;

        _visualScale -= Time.smoothDeltaTime * _smoothSpeed;

        if (_visualScale < Scale)
            _visualScale = Scale;

        objCircleBg.transform.localScale = Vector3.one + Vector3.one * _visualScale;
    }

    void AnalyzeSound()
    {
        //audioSource.GetOutputData(_samples, 0); // fill array with samples
        //int i;
        //float sum = 0;
        //for (i = 0; i < QSamples; i++)
        //{
        //    sum += _samples[i] * _samples[i]; // sum squared samples
        //}
        //RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        //DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
        //if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
        //                                    // get sound spectrum

        audioSource.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (int i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency
    }
}
