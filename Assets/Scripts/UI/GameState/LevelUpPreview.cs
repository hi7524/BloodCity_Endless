using UnityEngine;
using UnityEngine.UI;

public class LevelUpPreview : MonoBehaviour
{
    public static LevelUpPreview Instance { get; private set; }

    public Button[] upgradeCards;
    public Text[] DesText;

    public int selectState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        DesText[0].text = PlayerState.Instance.maxHealth.ToString("F1");
        DesText[1].text = PlayerState.Instance.restorePerSec.ToString("F1");
        DesText[2].text = PlayerState.Instance.defense.ToString("F1");
        DesText[3].text = PlayerState.Instance.speed.ToString("F1");
        DesText[4].text = PlayerState.Instance.attackDamage.ToString("F1");
        DesText[5].text = PlayerState.Instance.attackRange.ToString("F1");
        DesText[6].text = PlayerState.Instance.abilityHaste.ToString("F1");
        DesText[7].text = PlayerState.Instance.magnetism.ToString("F1");
        DesText[8].text = PlayerState.Instance.curse.ToString("F1");

        for (int i = 0; i < DesText.Length; i++) { DesText[i].color = Color.white; }
    }

    public void TextUpdate(int num)
    {
        ResetState();

        string stateName = upgradeCards[num].GetComponent<Image>().sprite.name;

        switch (stateName)
        {
            case "maxHealth":
                selectState = 0;
                float.TryParse(DesText[0].text, out float maxHealthValue);
                maxHealthValue += 3;
                DesText[0].color = Color.green;
                DesText[0].text = maxHealthValue.ToString();
                break;
            case "restorePerSec":
                selectState = 1;
                float.TryParse(DesText[1].text, out float restorePerSecValue);
                restorePerSecValue += 0.1f;
                DesText[1].color = Color.green;
                DesText[1].text = restorePerSecValue.ToString();
                break;
            case "defense":
                selectState = 2;
                float.TryParse(DesText[2].text, out float defenseValue);
                defenseValue += 0.5f;
                DesText[2].color = Color.green;
                DesText[2].text = defenseValue.ToString();
                break;
            case "speed":
                selectState = 3;
                float.TryParse(DesText[3].text, out float speedValue);
                speedValue += speedValue * 0.01f;
                DesText[3].color = Color.green;
                DesText[3].text = speedValue.ToString("F1");
                break;
            case "attackDamage":
                selectState = 4;
                float.TryParse(DesText[4].text, out float attackDamageValue);
                attackDamageValue += attackDamageValue * 0.03f;
                DesText[4].color = Color.green;
                DesText[4].text = attackDamageValue.ToString("F1");
                break;
            case "attackRange":
                selectState = 5;
                float.TryParse(DesText[5].text, out float attackRangeValue);
                attackRangeValue += attackRangeValue * 0.005f;
                DesText[5].color = Color.green;
                DesText[5].text = attackRangeValue.ToString("F1");
                break;
            case "abilityHaste":
                selectState = 6;
                float.TryParse(DesText[6].text, out float abilityHasteValue);
                abilityHasteValue += abilityHasteValue * 0.05f;
                DesText[6].color = Color.green;
                DesText[6].text = abilityHasteValue.ToString("F1");
                break;
            case "magnetism":
                selectState = 7;
                float.TryParse(DesText[7].text, out float magnetismValue);
                magnetismValue += 3;
                DesText[7].color = Color.green;
                DesText[7].text = magnetismValue.ToString();
                break;
        }
    }

    public void StateSelect()
    {
        // 스탯 업그레이드
        switch (selectState) 
        {
            case 0:
                PlayerState.Instance.maxHealth += 3;
                break;
            case 1:
                PlayerState.Instance.restorePerSec += 0.1f;
                break;
            case 2:
                PlayerState.Instance.defense += 0.5f;
                break;
            case 3:
                PlayerState.Instance.speed += PlayerState.Instance.speed * 0.01f;
                break;
            case 4:
                PlayerState.Instance.attackDamage += PlayerState.Instance.attackDamage * 0.3f;
                break;
            case 5:
                PlayerState.Instance.attackRange += PlayerState.Instance.attackRange * 0.005f;
                break;
            case 6:
                PlayerState.Instance.abilityHaste += PlayerState.Instance.abilityHaste * 0.5f;
                break;
            case 7:
                PlayerState.Instance.magnetism += 3;
                break;
        }
        UIManager.Instance.ToggleWindow(UIManager.Instance.levelUpWindow); // 레벨업 창 토글
        ResetState();
    }
}
