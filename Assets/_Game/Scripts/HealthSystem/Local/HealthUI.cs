// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using ReplenishableSystem;
// using TMPro;
//
// public class HealthUI : ReplenishableUI
// {
//     [SerializeField] TMP_Text healthValueText;
//
//     public override void UpdateUI()
//     {
//         base.UpdateUI();
//
//         healthValueText.SetText(((int)Replenishable.CurrentValue).ToString());
//     }
//
//     [ContextMenu("Set Health Height")]
//     public void SetHealthHeight(float height)
//     {
//         var backgroundRect = backgroundBar.rectTransform;
//         var replenishableRect = replenishableBar.rectTransform;
//         backgroundRect.sizeDelta = new Vector2(backgroundRect.rect.width, height);
//         replenishableRect.sizeDelta = new Vector2(replenishableRect.rect.width, height);
//     }
//     public void SetHealthWidth(float width)
//     {
//         var backgroundRect = backgroundBar.rectTransform;
//         var replenishableRect = replenishableBar.rectTransform;
//         backgroundRect.sizeDelta = new Vector2(width, backgroundRect.rect.height);
//         replenishableRect.sizeDelta = new Vector2(width, backgroundRect.rect.height);
//     }
//     public void MoveCanvasToPosition(Vector3 position)
//     {
//         Debug.Log("Inside move canvas to position. Position: " + position);
//         RectTransform rectTransform = transform.parent.parent.GetComponent<RectTransform>();
//         var oldRotation = rectTransform.rotation;
//         rectTransform.position = new Vector3(position.x, position.y, rectTransform.position.z);
//         rectTransform.rotation = oldRotation;
//     }
//
//     public void ToggleHealthUI(bool value)
//     {
//         healthValueText.gameObject.SetActive(value);
//         backgroundBar.gameObject.SetActive(value);
//         replenishableBar.gameObject.SetActive(value);
//     }
// }
