using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TextMeshProUGUI playerHpText;
    [SerializeField] Slider hpBarSlider;

    void Update()
    {
        transform.position = playerController.transform.position;
        playerHpText.text = (playerController.CurrentPlayerHp).ToString();
        CheckHp();
    }

    public void CheckHp() //*HP °»½Å
    {
        hpBarSlider.value = (float)playerController.CurrentPlayerHp / (float)playerController.MaxPlayerHp;
    }
}
