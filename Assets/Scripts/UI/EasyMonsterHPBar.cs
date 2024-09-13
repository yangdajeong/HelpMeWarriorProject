using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EasyMonsterHPBar : MonoBehaviour
{
    [SerializeField] MonsterEasy monsterEasy;
    [SerializeField] TextMeshProUGUI playerHpText;
    [SerializeField] Slider hpBarSlider;

    void Update()
    {
        transform.position = monsterEasy.transform.position + new Vector3(0, 1f, 0); ;
        playerHpText.text = (monsterEasy.CurrentHp).ToString();
        CheckHp();
    }

    public void CheckHp() //*HP °»½Å
    {
        hpBarSlider.value = (float)monsterEasy.CurrentHp / (float)monsterEasy.MaxHp;
    }
}
