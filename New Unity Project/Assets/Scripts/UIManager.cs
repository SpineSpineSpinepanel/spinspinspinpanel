using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public UISprite sprTitle = null;
    public UISprite sprStartButton = null;

    public AnimationCurve curve_MainUIAnim = null;

    public void StartButtonAction()
    {
        float screenY = 720f * 0.5f;
        float animDuration = 0.5f;

        float titlePosY = sprTitle.transform.localPosition.y;
        float buttonPosY = sprStartButton.transform.localPosition.y;

        DOTween.To(() => titlePosY, newTitleY => titlePosY = newTitleY, screenY + (sprTitle.height * 0.5f), animDuration).SetEase(curve_MainUIAnim).OnUpdate(() =>
             {
                 sprTitle.transform.localPosition = new Vector2(0, titlePosY);
             }).OnComplete(() =>
             {
                 sprTitle.gameObject.SetActive(false);
                 DOTween.To(() => buttonPosY, newButtonY => buttonPosY = newButtonY, -screenY - (sprStartButton.height * 0.5f), animDuration).SetEase(curve_MainUIAnim).OnUpdate(() =>
                 {
                     sprStartButton.transform.localPosition = new Vector2(0, buttonPosY);
                 }).OnComplete(() =>
                 {
                     sprStartButton.gameObject.SetActive(false);
                     Debug.Log("Game Start.");
                 });
             });
    }
}
