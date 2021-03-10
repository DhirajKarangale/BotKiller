using UnityEngine;
using System;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class RewardedVideoAd : MonoBehaviour
{
    private const string appId = "ca-app-pub-5695466976308828~7688222448";

    string rewardedId = "ca-app-pub-5695466976308828/6903245071";

    [SerializeField] Text adStatus;
    [SerializeField] GameObject adTextObject;
    [SerializeField] GameManager gameManager;

    private BannerView bannerView;
    private RewardBasedVideoAd rewardBasedVideo;

    private void Start()
    {
        if (bannerView != null) bannerView.Destroy();
        MobileAds.Initialize(appId);
        RequestRewardVideoAd();
        adTextObject.SetActive(false);
    }
      
        private void RequestRewardVideoAd()
        {
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardBasedVideo.LoadAd(request, rewardedId);

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        }

    public void ShowRewardVideoAd()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
        RequestRewardVideoAd();
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        adTextObject.SetActive(true);
        adStatus.text = "Ad failed to show , try again !";
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        adTextObject.SetActive(true);
        Debug.Log("Add closed");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        adTextObject.SetActive(false);
        gameManager.PlayerReSpwan();
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        adTextObject.SetActive(true);
        adStatus.text = "Ad Closed , try again !";
    }
}
