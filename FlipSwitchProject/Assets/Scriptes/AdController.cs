using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;


public class AdController : MonoBehaviour
{
    string appId = "ca-app-pub-9601525912442679~3459214383";
    
    string bannerId = "ca-app-pub-9601525912442679/4267848925";
    
    string interstitialId = "ca-app-pub-9601525912442679/6437898458";
    
    string rewardedVideoId = "ca-app-pub-9601525912442679/2296291703";

    public BannerView bannerView;
    public InterstitialAd interstitialView;
    public RewardedAd rewardedView;

    private void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        RequestInterstitial();
        RequestRewardBasedVideo();
    }
    void Start()
    {
        

    }

    #region Banner Ad
    public void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.BottomLeft);
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        HideBanner();

    }

    public void ShowBanner()
    {   
        this.bannerView.Show();
    }

    public void HideBanner()
    {
        this.bannerView.Hide();
    }
    public void DestroyBanner()
    {
        this.bannerView.Destroy();
    }

    public void RequestBannerBottom()
    {
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        HideBanner();

    }

    #endregion


    #region Interstitial Ad

    public void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        this.interstitialView = new InterstitialAd(interstitialId);

        // Called when an ad request has successfully loaded.
        this.interstitialView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialView.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitialView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialView.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitialView.IsLoaded())
        {
            this.interstitialView.Show();
        }
    }

    #endregion

    #region Rewarded Ad
    public void RequestRewardBasedVideo()
    {  

        this.rewardedView = new RewardedAd(rewardedVideoId);
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedView.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.rewardedView.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedView.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedView.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedView.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedView.OnAdClosed += HandleRewardedAdClosed;
 
    }

    public void ShowVideoBasedRewarded()
    {
        if (this.rewardedView.IsLoaded())
        {
            this.rewardedView.Show();
            
        }
 
    }
    #endregion

    #region EVENTS AND DELEGATES FOR ADS
    public void HandleOnAdLoaded(object sender, EventArgs args)
     {
        

     }

     public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
     {

     }

     public void HandleOnAdOpened(object sender, EventArgs args)
     {
         MonoBehaviour.print("HandleAdOpened event received");
     }

     public void HandleOnAdClosed(object sender, EventArgs args)
     {
         MonoBehaviour.print("HandleAdClosed event received");
     }

     public void HandleOnAdLeavingApplication(object sender, EventArgs args)
     {
         MonoBehaviour.print("HandleAdLeavingApplication event received");
     }


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdFailedToLoad event received with message: ");
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdFailedToShow event received with message: ");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");

        RequestRewardBasedVideo();

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        /*if (isGameScene)
        {
            canbutt.videoWatched();
        }   // show video watched
        */
    }

    #endregion

}
