using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossLocking : MonoBehaviour
{
    public GameObject Self;
    public GameObject Gate;
    public GameObject Boss;
    public GameObject FakeBoss;
    public TMP_Text bossNameText;
    public TMP_Text warningText;

    public float textDisplayDuration = 3f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Gate.SetActive(true);
            StartCoroutine(ShowBossName());
        }
    }

    IEnumerator ShowBossName()
    {
        bossNameText.gameObject.SetActive(true);
        warningText.gameObject.SetActive(true);

        yield return new WaitForSeconds(textDisplayDuration);

        bossNameText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(false);

        FakeBoss.SetActive(false);
        Boss.SetActive(true);

        Self.SetActive(false);
    }
}
