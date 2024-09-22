using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCheckMgr : MonoBehaviour
{
    // 정답 체크용 스크립트
    // 체크리스트 제출 후 정답을 판별해서 표시하자 (작성 내용 확인 창 UI 에서 사용)

    Proposal checklist = ProposalMgr.instance.proposal;

    // 필수 문항 10개에 대한 정답 체크 (bool)
    bool[] tORf = new bool[10];

    // 문항별 ox 이미지 (할당해줘야 됨!)
    public Image[] o = new Image[10];
    public Image[] x = new Image[10];

    public void AnswerCheck()
    {
        // 문항별 정답 체크
        if (checklist.analysis_type == "3/4C분석") tORf[0] = true;
        if (checklist.audience_type == "사내대상") tORf[1] = true;
        if (checklist.subject == "새로운 세대의 사용자 경험을 반영한 모바일 애플리케이션을 통해 시장 점유율을 높이고, 경쟁사 대비 차별화된 기능을 제공하기 위해") tORf[2] = true;
        if (checklist.project_type == "어플리케이션") tORf[3] = true;
        if (checklist.step == "컨셉제안") tORf[4] = true;
        if (checklist.plan == "2월~5월") tORf[5] = true;
        if (checklist.target == "20대 중반에서 30대 초반의 IT에 익숙한 직장인") tORf[6] = true;
        if (checklist.purpose == "사용자들이 더 쉽게 상품을 검색하고 구매할 수 있는 통합 쇼핑 플랫폼을 제공하여 사용자 편의성을 극대화하고, 기업 매출 성장을 목표로 함") tORf[7] = true;
        if (checklist.worker == "") tORf[8] = true;
        if (checklist.budget == "1억 2000만원") tORf[9] = true;

        // 문항별 O/X 에 따라 이미지 표시
        for(int i = 0; i < 10; i++)
        {
            o[i].gameObject.SetActive(tORf[i]);     // 정답이면 O 이미지 활성화
            x[i].gameObject.SetActive(!tORf[i]);    // 오답이면 X 이미지 활성화
        }
    }
}