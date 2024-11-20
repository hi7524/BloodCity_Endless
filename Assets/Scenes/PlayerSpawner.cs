using UnityEngine;

// 플레이어 생성
public class PlayerSpawner : MonoBehaviour
{
    public string character;

    public GameObject SpaceMarine;
    public GameObject Beeper;
    public GameObject Baz;


    private void Awake()
    {
        SetCharacter(character);
    }

    private void SetCharacter(string tag)
    {
        if (character == "SpaceMarine")
        {
            Destroy(Beeper);
            Destroy(Baz);

        }
        else if (character == "Beeper")
        {
            Destroy(SpaceMarine);
            Destroy(Baz);
        }
        else if (character == "Baz")
        {
            Destroy(SpaceMarine);
            Destroy(Beeper);
        }
        else
        {
            Debug.Log("캐릭터 이름 오타난 것 같아요");
        }
    }
}
