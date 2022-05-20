using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensibilitySlider : MonoBehaviour
{
    private Slider slider;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        slider = this.GetComponent<Slider>();

        slider.value = PlayerPrefs.GetFloat("sensibility", 100f);
        player.transform.GetComponent<FirstPersonController>().AdjustSensibility(slider.value);

        if (slider != null)
        {
            slider.onValueChanged.AddListener(value => player.transform.GetComponent<FirstPersonController>().AdjustSensibility(slider.value));
        }
        else
        {
            Debug.LogError("erro");
        }
    }
}
