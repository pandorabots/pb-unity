/*********************************************************************
 * The below code was originally created by Peter Olthof from Peer Play.
 * http://peerplay.nl/about/
 * Please consider supporting his Patreon https://www.patreon.com/peerplay
**********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightOnAudio : MonoBehaviour
{
    public int band;
    public float minIntensity, maxIntensity;
    Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        myLight.intensity = (AudioAnalyzer.audioBandBuffer[band] * (maxIntensity - minIntensity)) + minIntensity;
    }
}
