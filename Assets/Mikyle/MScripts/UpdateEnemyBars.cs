using UnityEngine;
using UnityEngine.UI;

public class UpdateEnemyBars : MonoBehaviour
{
    [SerializeField] EnemyCharacter enemyCharacter;
    [SerializeField] Slider hpSlider;

    private 

    void Start()
    {
        enemyCharacter = GetComponent<EnemyCharacter>();
        hpSlider = this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Slider>();

        hpSlider.maxValue = enemyCharacter.enemyHealth;
        hpSlider.value = hpSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpSlider.value > enemyCharacter.enemyHealth)
            hpSlider.value-= 0.5f;
    }
}
