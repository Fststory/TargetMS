//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using UnityEngine.Networking;

//public class STTManager : MonoBehaviourPunCallbacks
//{
//    private List<string> sttResults = new List<string>();

//    // STT 결과를 추가하는 RPC 함수
//    [PunRPC]
//    public void AddSTTResult(string sttResult)
//    {
//        sttResults.Add(sttResult);
//        Debug.Log("STT 결과 추가: " + sttResult);
//    }

//    // 모든 STT 결과를 AI에 전송하는 함수
//    public void SendAllResultsToAI()
//    {
//        string combinedText = string.Join(" ", sttResults);
//        StartCoroutine(SendTextToAI(combinedText));
//    }

//    private IEnumerator<UnityWebRequest> SendTextToAI(string text)
//    {
//        using (UnityWebRequest www = UnityWebRequest.Post("YOUR_AI_API_URL", text))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.LogError("AI 요청 실패: " + www.error);
//            }
//            else
//            {
//                string summaryResult = www.downloadHandler.text;
//                Debug.Log("AI 요약 결과: " + summaryResult);
//            }
//        }
//    }
//}
