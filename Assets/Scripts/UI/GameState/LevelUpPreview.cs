using UnityEngine;
using UnityEngine.UI;

public class LevelUpPreview : MonoBehaviour
{
    public static LevelUpPreview Instance { get; private set; }

    public Image Cha;
    public Button[] upgradeCards;
    public Text[] DesText;

    public string selectState;

    public GameObject select;

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
        DesText[0].text = PlayerState.Instance.maxHealth.ToString();
        if (PlayerState.Instance.currentIndex != 1) { DesText[1].text = PlayerState.Instance.restorePerSec.ToString("F1"); }
        else if (PlayerState.Instance.currentIndex == 1) { DesText[1].text = "<color=red>-</color>"; };
        DesText[2].text = PlayerState.Instance.defense.ToString("F1");
        DesText[3].text = PlayerState.Instance.speed.ToString("F1");
        DesText[4].text = PlayerState.Instance.attackDamage.ToString("F1");
        DesText[5].text = PlayerState.Instance.attackRange.ToString("F1");
        DesText[6].text = PlayerState.Instance.abilityHaste.ToString("F1");
        DesText[7].text = PlayerState.Instance.magnetism.ToString("F1");
        DesText[8].text = PlayerState.Instance.curse.ToString();

        for (int i = 0; i < DesText.Length; i++) { DesText[i].color = Color.white; }

        switch (PlayerState.Instance.currentIndex) 
        { 
            case 0:
                Cha.sprite = Resources.Load<Sprite>("States/player0");
                break;
            case 1:
                Cha.sprite = Resources.Load<Sprite>("States/player1");
                break;
            case 2:
                Cha.sprite = Resources.Load<Sprite>("States/player2");
                break;
        }

    }

    /* 스탯 표시 */
    public void TextUpdate(int num)
    {
        select.SetActive(true);

        ResetState();

        selectState = upgradeCards[num].GetComponent<Image>().sprite.name;

        switch (selectState)
        {
            case "surviveStats": survive();
                break;
            case "strengthStats": strength();
                break;
            case "intellectStats": intellect();
                break;
            case "SurStr": survive(); strength();
                break;
            case "StrInt": strength(); intellect();
                break;
            case "IntSur": intellect(); survive();
                break;
        }
    }

    private void survive()
    {
        UpdateStat(0, 3);          // maxHealth
        if (PlayerState.Instance.currentIndex != 1) { UpdateStat(1, 0.1f); }; // restorePerSec
        UpdateStat(2, 0.5f);       // defense
        UpdateStatPercentage(3, 0.01f); // speed
    }

    private void strength()
    {
        UpdateStatPercentage(4, 0.3f); // attackDamage
        UpdateStatPercentage(5, 0.005f); // attackRange
    }

    private void intellect()
    {
        UpdateStatPercentage(6, 0.5f); // abilityHaste
        UpdateStat(7, 3);            // magnetism
    }

    // 스탯 증가 표시
    private void UpdateStat(int index, float increment)
    {
        float.TryParse(DesText[index].text, out float value);
        value += increment;
        DesText[index].color = Color.green;
        DesText[index].text = value.ToString();
    }

    // 비율 증가 표시
    private void UpdateStatPercentage(int index, float percentage)
    {
        float.TryParse(DesText[index].text, out float value);
        value += value * percentage;
        DesText[index].color = Color.green;
        DesText[index].text = value.ToString("F1");
    }


    /* 진짜 스탯 업그레이드 */
    public void StateSelect()
    {
        switch (selectState) 
        {
            case "surviveStats": surviveStats();
                break;
            case "strengthStats": strengthStats();
                break;
            case "intellectStats": intellectStats();
                break;
            case "SurStr": surviveStats(); strengthStats();
                break;
            case "StrInt": strengthStats(); intellectStats();
                break;
            case "IntSur": intellectStats(); surviveStats();
                break;
        }
        ResetState();
        select.SetActive(false);
        UIManager.Instance.ToggleWindow(UIManager.Instance.levelUpWindow); // 레벨업 창 토글
    }

    private void surviveStats()
    {
        PlayerState.Instance.maxHealth += 3;
        if (PlayerState.Instance.currentIndex != 1) { PlayerState.Instance.restorePerSec += 0.1f; }
        PlayerState.Instance.defense += 0.5f;
        PlayerState.Instance.speed += PlayerState.Instance.speed * 0.01f;
    }

    private void strengthStats()
    {
        PlayerState.Instance.attackDamage += PlayerState.Instance.attackDamage * 0.3f;
        PlayerState.Instance.attackRange += PlayerState.Instance.attackRange * 0.005f;
    }

    private void intellectStats()
    {
        PlayerState.Instance.abilityHaste += PlayerState.Instance.abilityHaste * 0.5f;
        PlayerState.Instance.magnetism += 3;
    }
}