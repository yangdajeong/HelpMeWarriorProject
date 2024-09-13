using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    [SerializeField] float destination;
    [SerializeField] float currentPosition = 0;
    [SerializeField] Slider naviSlider;

    void Update()
    {
        if (PlayerController.speed == 0)
            return;

        currentPosition += Time.deltaTime;

        naviSlider.value = Mathf.Clamp01(currentPosition / destination);
    }
}
