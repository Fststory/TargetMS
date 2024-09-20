using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView_Script : MonoBehaviour // 결과 이벤트창이 들어 갈 곳이다
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }




    // 캔버스에 패널 두개를 만든다
    // 왼쪽에는 문제 1 ~ n개 까지 있고
    // 오른쪽에도 문제 1 ~ n개 풀었느지 안풀었는지 확인하는 진행도 바를 만든다.
    // 시간 관계상 풀었는지 안풀었는지 2개의 표시만 넣는다.
    // 진행도 바는 1. 버튼, 2. 체크 이미지를 넣고 
    // 버튼을 누르면 해당하는 문제의 패널을 연다.

    // 왼쪽 패널 안에 문제 1개당 각각 새로운 버튼을 만든다.
    // 버튼을 누르면 각각 버튼 안에 할당된 패널이 나오고
    // 그 패널 안에는 1. 문제 tmp_text 와 2. 여러 답변이 들어있는 버튼을 만들어 둔다.
    // 이후 각 문제와 답변의 기획팀에서 주는 문서와 답변을 넣고 필요에 따라 갯수를 가감한다.
    // 만약, currentpanel 이 true 일때 다른 문제버튼을 누르면 현재 currentpeanel 를 fasle로 하고 새로운 panel를 currentpanel로 한다.

    // 만약, 아무 문제안의 답변 버튼을 누르면 해당 문제의 번호를 풀었다는 체크를 하고 오른쪽에도 적용한다.


}
