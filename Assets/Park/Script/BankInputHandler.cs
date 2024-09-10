using UnityEngine;
using TMPro;

public class BankInputHandler : MonoBehaviour
{
    public TMP_InputField inputField1;  // InputField 연결
    public TMP_InputField inputField2;  // InputField 연결
    public TMP_InputField inputField3;  // InputField 연결
    public float fundValue;           // 입력된 값을 저장할 변수
    public float savingValue;
    public float stockValue;

    // 버튼 클릭 시 호출될 메서드
    public void OnSubmitButtonClicked()
    {
        // InputField에서 텍스트 값을 가져와서 float로 변환
        if (float.TryParse(inputField1.text, out fundValue))
        {
            Debug.Log("Fund Value: " + fundValue);
            // 여기서 fundValue를 펀드 관련 로직에 사용할 수 있음
        }
        
        if (float.TryParse(inputField2.text, out savingValue))
        {
            Debug.Log("Fund Value: " + savingValue);
            // 여기서 fundValue를 펀드 관련 로직에 사용할 수 있음
        }

        if (float.TryParse(inputField3.text, out stockValue))
        {
            Debug.Log("Fund Value: " + stockValue);
            // 여기서 fundValue를 펀드 관련 로직에 사용할 수 있음
        }

        
    }
    
    
}