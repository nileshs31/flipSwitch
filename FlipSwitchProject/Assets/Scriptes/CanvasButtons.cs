using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject VolumeOffButton;
    public GameObject VolumeOnButton;
    public GameObject pauseGameScreen;
    public GameObject ThankingScreen;
    public Text payOrnNotEnough;
    public GameObject GameOverScreen;
    public AdController adcon;
    private void Awake()
    {
        adcon = GameObject.FindGameObjectWithTag("Ad").GetComponent<AdController>();
    }
    void Start()
    { 
        payOrnNotEnough.text = "(Pay 20 Coins)";

        if (AudioListener.volume == 0f)
        {
            VolumeOffButton.SetActive(false);
            VolumeOnButton.SetActive(true);
        }
        else
        {
            VolumeOffButton.SetActive(true);
            VolumeOnButton.SetActive(false);
        }
    }
    public void pauseGame()
    {
        if (adcon.bannerView == null)
        {
            adcon.RequestBanner();
        }
        adcon.ShowBanner();
        Time.timeScale = 0;
        pauseGameScreen.SetActive(true);
        
    }


    public void unpauseGame()
    {
        if (adcon.bannerView != null)
        {
            adcon.HideBanner();
        }
        Time.timeScale = 1;
        pauseGameScreen.SetActive(false);
    }

    public void VolOn()
    {
        VolumeOffButton.SetActive(true);
        VolumeOnButton.SetActive(false);
        AudioListener.volume = 1f;


    }
    public void VolOff()
    {
        VolumeOffButton.SetActive(false);
        VolumeOnButton.SetActive(true);
        AudioListener.volume = 0f;

    }

    public void continuePlay()
    {

        if (PlayerPrefs.GetInt("NumOfCoins", 0)>=20)
        {
            PlayerPrefs.SetInt("NumOfCoins", PlayerPrefs.GetInt("NumOfCoins")-20);
            PlayerPrefs.SetInt("Score", GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            payOrnNotEnough.text = "(Not Enough Coins)";
            payOrnNotEnough.color = Color.red;
        }
    }
    public void Removee()
    {
        adcon.ShowVideoBasedRewarded();
        Time.timeScale = 0;
    }

    public void videoWatched()
    {
        GameOverScreen.SetActive(false);
        ThankingScreen.SetActive(true);
        Time.timeScale = 0;
    }

    //New continue for rewarded----------------------------------------------------------------------------------------------------------------------------------------------------------
    public void continuePlay2()

    {
        PlayerPrefs.SetInt("Score", GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        //hide
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene("HomeScreen");
    }
    public void Update()
    {
        if(ThankingScreen.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }
}
