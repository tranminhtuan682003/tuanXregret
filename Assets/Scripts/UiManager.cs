using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject PlayMode;
    private bool isPlay;
    void Start()
    {
    }
    void Update()
    {
    }

    public void PlayGame()
    {
        isPlay = !isPlay;
        PlayMode.SetActive(isPlay);
    }
}
