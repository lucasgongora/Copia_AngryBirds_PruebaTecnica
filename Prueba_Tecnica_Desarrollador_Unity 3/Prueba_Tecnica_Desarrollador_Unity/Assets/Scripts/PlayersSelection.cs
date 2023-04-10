using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSelection : MonoBehaviour
{
    public GameObject[] charactersPrefabs1;
    public GameObject[] charactersPrefabs2;
    public GameObject[] charactersPrefabs3;
    public int selectedCharacter1 = 0;
    public int selectedCharacter2 = 0;
    public int selectedCharacter3 = 0;

    private void Start()
    {
        charactersPrefabs1[selectedCharacter1].SetActive(true);
        charactersPrefabs2[selectedCharacter2].SetActive(true);
        charactersPrefabs3[selectedCharacter3].SetActive(true);
    }

    public void NextCharacter1()
    {
        charactersPrefabs1[selectedCharacter1].SetActive(false);
        selectedCharacter1 = (selectedCharacter1 + 1) % charactersPrefabs1.Length;
        charactersPrefabs1[selectedCharacter1].SetActive(true);
    }
    public void NextCharacter2()
    {
        charactersPrefabs2[selectedCharacter2].SetActive(false);
        selectedCharacter2 = (selectedCharacter2 + 1) % charactersPrefabs2.Length;
        charactersPrefabs2[selectedCharacter2].SetActive(true);
    }
    public void NextCharacter3()
    {
        charactersPrefabs3[selectedCharacter3].SetActive(false);
        selectedCharacter3 = (selectedCharacter3 + 1) % charactersPrefabs3.Length;
        charactersPrefabs3[selectedCharacter3].SetActive(true);
    }
    
    public void SetPlayerPrefsWithSelected()
    {
        PlayerPrefs.SetInt("selectedCharacter1", selectedCharacter1);
        PlayerPrefs.SetInt("selectedCharacter2", selectedCharacter2 + 5);
        PlayerPrefs.SetInt("selectedCharacter3", selectedCharacter3 + 10);
    }
}
