using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using GoogleMobileAds.Api;

public class AdmobController : MonoBehaviour
{
    private string appId="4234609";

    public void Awake(){

        Time.timeScale=1;
        
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestConfigurationAd();
        RequestBannerAd();
        Advertisement.Initialize(appId,false);
    }

    private InterstitialAd intersitional;
    private BannerView banner;
    private string appId_admob="ca-app-pub-4962234576866611~9475644858";
    private string intersitionalId="ca-app-pub-4962234576866611/6236427362";

    private string bannerId = "ca-app-pub-4962234576866611/7189082196";

     AdRequest AdRequestBuild(){
         return new AdRequest.Builder().Build();
     }


      void RequestConfigurationAd(){
          intersitional=new InterstitialAd(intersitionalId);
          AdRequest request=AdRequestBuild();
          intersitional.LoadAd(request);

          intersitional.OnAdLoaded+=this.HandleOnAdLoaded;
          intersitional.OnAdOpening+=this.HandleOnAdOpening;
          intersitional.OnAdClosed+=this.HandleOnAdClosed;

      }


      public bool showIntersitionalAd(){
        if(intersitional.IsLoaded()){
            intersitional.Show();

            return true;
        }

        return false;
      }

      private void OnDestroy()
      {
          DestroyIntersitional();

          intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
          intersitional.OnAdOpening-=this.HandleOnAdOpening;
          intersitional.OnAdClosed-=this.HandleOnAdClosed;

      }

      private void HandleOnAdClosed(object sender, EventArgs e)
      {          
        intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
        intersitional.OnAdOpening-=this.HandleOnAdOpening;
        intersitional.OnAdClosed-=this.HandleOnAdClosed;

        RequestConfigurationAd();
      }

      private void HandleOnAdOpening(object sender, EventArgs e)
    {
        
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        
    }

 

     public void DestroyIntersitional(){
         intersitional.Destroy();
     }


    public void RequestBannerAd(){
        banner=new BannerView(bannerId,AdSize.Banner,AdPosition.BottomRight);
        AdRequest request = AdRequestBannerBuild();
        banner.LoadAd(request);
    }

    public void DestroyBanner(){
        if(banner!=null){
            banner.Destroy();
        }
    }



    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }
}
