using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HardMonsterHPBar : MonoBehaviour
{
    [SerializeField] MonsterHard monsterHard;
    [SerializeField] Slider hpBarSlider;

    void Update()
    {
        transform.position = monsterHard.transform.position + new Vector3(0, 1f, 0);
        CheckHp();
    }

    public void CheckHp() //*HP °»½Å
    {
        hpBarSlider.value = (float)monsterHard.CurrentHp / (float)monsterHard.MaxHp;
    }
}
