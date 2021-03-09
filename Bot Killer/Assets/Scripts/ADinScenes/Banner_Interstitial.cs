using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Banner_Interstitial : MonoBehaviour
{
    private const string appId = "ca-app-pub-8003338007949621~1667607563";

    private const string bannerId = "ca-app-pub-3940256099942544/6300978111";
    private const string interstatialId = "ca-app-pub-3940256099942544/1033173712";

    private BannerView bannerView;
    private InterstitialAd interstitial;

    //public Text adStatus;

    private void Start()
    {
        MobileAds.Initialize(appId);
        RequestInterstitial();
        if ((SceneManager.GetActiveScene().buildIndex == 0) || (SceneManager.GetActiveScene().buildIndex == 1) || (SceneManager.GetActiveScene().buildIndex == 2))
        {
            RequestBanner();
            ShowBannerAd();
        }
        else
        {
            if(this.bannerView != null)
            {
                bannerView.Destroy();
            }
        }
        Invoke("ShowInterstatialAd", 5f);
    }


    private void RequestBanner()
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
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }

    private void ShowBannerAd()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(interstatialId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    private void ShowInterstatialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }



    //Events
    private void HandleOnAdLoaded(object sender, EventArgs args)
    {
       Debug.Log( "Ad Loaded");
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Ad Failed to Loaded");
    }

    private void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    private void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

}
