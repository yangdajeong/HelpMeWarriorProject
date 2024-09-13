using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossMonsterHPBar : MonoBehaviour
{
    [SerializeField] Boss monsterBoss;
    [SerializeField] Slider bossHpBarSlider;

    void Update()
    {
        if (monsterBoss != null)
        {
            transform.position = monsterBoss.transform.position + new Vector3(0, 6f, 0);
            CheckBossHp();
        }
    }

    public void CheckBossHp() //*HP °»½Å
    {
        bossHpBarSlider.value = (float)monsterBoss.CurrentHp / (float)monsterBoss.MaxHp;
    }
}
