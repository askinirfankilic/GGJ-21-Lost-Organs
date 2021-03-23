using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public bool gameOver = false;

    public Text scoreText;
    public Transform canvas;

    AudioSource au;

    int score = 0;

    void Start()
    {
        gm = this;
        au = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        au.Stop();
        canvas.GetChild(0).gameObject.SetActive(false);
        canvas.GetChild(2).gameObject.SetActive(true);
        canvas.GetChild(2).GetChild(0).GetComponent<Text>().text = score.ToString();
    }

    public void EarnPoint()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void StartUI()
    {
        canvas.GetChild(1).GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(FadeIn());
        au.Play();
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1);
        canvas.GetChild(1).gameObject.SetActive(false);
    }
}
