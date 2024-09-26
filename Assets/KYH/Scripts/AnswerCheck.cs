using UnityEngine;
using UnityEngine.UI;

public class AnswerCheckMgr : MonoBehaviour
{
    // 정답 체크용 스크립트
    // 체크리스트 제출 후 정답을 판별해서 표시하자 (작성 내용 확인 창 UI 에서 사용)

    // 필수 문항 5개에 대한 정답 체크 (bool)
    public bool[] tORf = new bool[5];

    // 다 맞추면
    //bool allAnswer;

    // 문항별 ox 이미지 (할당해줘야 됨!)
    public Image[] o = new Image[5];
    public Image[] x = new Image[5];
       

    // 정답 체크 ( 체크 & O/X 이미지 표시)
    // 3단계로 넘어가는 부분에서 호출하면 되는 함수
    // 3단계 캔버스 켜질 때 AnswerCheckMgr 오브젝트가 활성화되므로 Start() 에서 호출
    public void AnswerCheck()
    {
        Proposal checklist = ProposalMgr.instance.proposal;     // 너무 길어서 변수에 담음

        // 문항 순서
        // 1.유형 2.읽는 이 3.타겟 4.기대효과 5.예상 리스크

        // 문항별 정답 체크
        if (checklist.project_type == "프로그램 개발") tORf[0] = true;
        if (checklist.audience_type == "내부 개발자") tORf[1] = true;
        if (checklist.target == "유아") tORf[2] = true;
        if (checklist.expected_outcome == "우주관련 콘텐츠 관심도 상승") tORf[3] = true;
        if (checklist.potential_risk == "적은 예산으로 인한 고용에 대한 문제 발생") tORf[4] = true;

        // 문항별 O/X 에 따라 이미지 표시
        for (int i = 0; i < 5; i++)
        {
            o[i].gameObject.SetActive(tORf[i]);     // 정답이면 O 이미지 활성화
            x[i].gameObject.SetActive(!tORf[i]);    // 오답이면 X 이미지 활성화
        }

        // 5 문제 다 맞추면 allAnswer (모두 정답 처리 => 기획서 작성 요청 가능할 때 사용하거나 하면 될 듯)
        //if (tORf[0]&& tORf[1] && tORf[2] && tORf[3] && tORf[4])
        //{
        //    allAnswer = true;
        //}
    }
}