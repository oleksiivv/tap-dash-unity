using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using GoogleMobileAds.Api;

public class PlayerUI : ScenesManager
{
    public GameObject pausePanel;
    public GameObject deathPanel;

    public GameObject winPanel;

    public static int addCnt=1;

#if UNITY_IOS
    private string appId="4234608";
#else
    private string appId="4234609";
#endif

    private bool adsAlreadyShowed=false;

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

        adsAlreadyShowed=false;
        

        canMove=true;
    }

    public static bool canMove=true;

    public void pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);

        canMove=false;
        if(addCnt%2==0){
            if(Advertisement.IsReady()){
                adsAlreadyShowed=true;
                Advertisement.Show("Interstitial_Android");
            }
            
        }
        addCnt++;
    }
    public void resume(){
        Time.timeScale=1;
        pausePanel.SetActive(false);

        Invoke(nameof(resetCanMove), 0.2f);
    }

    void resetCanMove(){
        canMove=true;
    }
    
    public void restart(){
        openScene(Application.loadedLevel);
    }
    public void next(){
        openScene(Application.loadedLevel+1);
    }

    public void showDeathPanel(){
        deathPanel.SetActive(true);
        if(addCnt%2==0){
            if(!showIntersitionalAd()){
                if(Advertisement.IsReady()){
                    adsAlreadyShowed=true;
                    Advertisement.Show("Interstitial_Android");
                }
            }
            
        }
        addCnt++;
    }
    public void showWinPanel(){
        winPanel.SetActive(true);
        if(addCnt%2==0){
            if(!showIntersitionalAd()){
                if(Advertisement.IsReady()){
                    adsAlreadyShowed=true;
                    Advertisement.Show("Interstitial_Android");
                }
            }
            
        }
        addCnt++;
    }


    private InterstitialAd intersitional;
    private BannerView banner;

#if UNITY_IOS
    private string appId_admob="ca-app-pub-4962234576866611~7942157909";
    private string intersitionalId="ca-app-pub-4962234576866611/7750586215";

    private string bannerId = "ca-app-pub-4962234576866611/1376749551";
#else
    private string appId_admob="ca-app-pub-4962234576866611~9475644858";
    private string intersitionalId="ca-app-pub-4962234576866611/6236427362";

    private string bannerId = "ca-app-pub-4962234576866611/7189082196";
#endif

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
        if(adsAlreadyShowed)return true;

        if(intersitional.IsLoaded()){
            adsAlreadyShowed=true;
            intersitional.Show();

            return true;
        }

        return false;
      }

      private void OnDestroy()
      {
        adsAlreadyShowed=true;

          DestroyIntersitional();

          intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
          intersitional.OnAdOpening-=this.HandleOnAdOpening;
          intersitional.OnAdClosed-=this.HandleOnAdClosed;

      }

      private void HandleOnAdClosed(object sender, EventArgs e)
      {
        adsAlreadyShowed=true;
          
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
