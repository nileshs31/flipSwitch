using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;


public class ad : MonoBehaviour
{
    public Text adstatus;
    string App_ID = "ca-app-pub-2329342188923198~6447910859";

    string Banner_Ad_ID = "ca-app-pub-3940256099942544/6300978111";
    string Interstitial_Ad_ID = "ca-app-pub-3940256099942544/1033173712";
    string Video_Ad_ID = "ca-app-pub-3940256099942544/5224354917";

    private BannerView bannerView;
    private BannerView bannerView1;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    ///Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
        this.RequestInterstitial();
        this.RequestRewardBasedVideo();
        this.RequestBanner1();
    }
    public void RequestBanner()
    {
        AdSize adSize = new AdSize(250, 250);
        
        
            // Create a 320x50 banner at the top of the screen.
            this.bannerView = new BannerView(Banner_Ad_ID, AdSize.Banner, AdPosition.Bottom);
         
        
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }
    public void RequestBanner1()
    {
        AdSize adSize = new AdSize(250, 250);


        // Create a 320x50 banner at the top of the screen.
        this.bannerView1 = new BannerView(Banner_Ad_ID, AdSize.Banner, AdPosition.Top);


        // Called when an ad request has successfully loaded.
        this.bannerView1.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView1.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView1.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView1.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView1.LoadAd(request);

    }


    /*public void ShowBannerAd()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }*/


    public void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(Interstitial_Ad_ID);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }


    public void RequestRewardBasedVideo()
    {  

        this.rewardedAd = new RewardedAd(Video_Ad_ID);
        
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);


        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
 


    }

    public void ShowVideoBasedRewarded()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
            
        }
 
    }


    //EVENTS AND DELEGATES FOR ADS
     public void HandleOnAdLoaded(object sender, EventArgs args)
     {
            //adStatus.text = "Ad loaded";
        
        }

     public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
     {
         //adStatus.text = "Ad failed loaded";
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
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                           );
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             );
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
          

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }

}
