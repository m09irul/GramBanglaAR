using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DentedPixel;


public class MainMenuManager : MonoBehaviour
{
    public Button startBtn;
    public MenuPikluManager menuPikluManager;
    public CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup.alpha = 1;
        LeanTween.alphaCanvas(canvasGroup, 0, 5f).setEase(LeanTweenType.easeInQuad);

        WalkToScene();
    }

    void WalkToScene()
    {
        LeanTween.moveX(menuPikluManager.gameObject, 0, 7).setOnComplete(() =>
        {
            startBtn.gameObject.SetActive(true);

            menuPikluManager.Hi();
        });

        
    }
 
    public void Jumpoff() 
    {
        CinemachineShake.instance.ShakeCamera(10, 0.2f, 4f);
        menuPikluManager.Jetpack.SetActive(true);
        menuPikluManager.FlyOff();
        LeanTween.rotateY(menuPikluManager.gameObject, 90, 1f).setOnComplete(()=> {
            LeanTween.moveLocalY(menuPikluManager.gameObject, 7, 3.5f).setEase(LeanTweenType.easeInCirc).setOnComplete(() =>
                LeanTween.alphaCanvas(canvasGroup, 1, 3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(()=>
                    SceneManager.LoadSceneAsync(1)
                ));
        });
    }


}
