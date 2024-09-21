using Photon.Voice.Unity; // Photon Voice Unity 네임스페이스를 사용합니다.
using System.Collections.Generic; // 제네릭 컬렉션을 사용하기 위한 네임스페이스입니다.
using System; // 기본적인 시스템 기능을 사용하기 위한 네임스페이스입니다.
using System.IO; // 파일 및 스트림 작업을 위한 네임스페이스입니다.
using UnityEngine; // Unity 엔진의 기본 기능을 사용하기 위한 네임스페이스입니다.
using System.Collections; // 코루틴을 사용하기 위한 네임스페이스입니다.
using UnityEngine.Networking; // Unity의 네트워킹 기능을 사용하기 위한 네임스페이스입니다.
using System.Text; // 문자열 인코딩을 위한 네임스페이스 추가


public class RealtimeVoiceRecorder : MonoBehaviour // MonoBehaviour를 상속받는 RealtimeVoiceRecorder 클래스 정의
{
    public AudioOutCapture audioOutCapture; // 음성 출력을 캡처하기 위한 AudioOutCapture 컴포넌트를 public으로 선언

    private Dictionary<int, MemoryStream> userAudioStreams = new Dictionary<int, MemoryStream>(); // 화자 ID와 메모리 스트림을 매핑하는 딕셔너리 생성

    public string serverUrl;
    public int voiceId;

    void Start() // Start 메서드: 스크립트가 시작될 때 호출
    {
        voiceId = GenerateUniqueVoiceId();
        // 음성 데이터 캡처 이벤트 등록
        audioOutCapture.OnAudioFrame += OnAudioFrameCaptured; // OnAudioFrameCaptured 메서드를 이벤트에 등록
    }

    int GenerateUniqueVoiceId()
    {
        return Guid.NewGuid().GetHashCode(); // 랜덤한 고유 ID 생성
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("서버에 전송!");
            StartCoroutine(UploadAudioToServer(voiceId));
        }
    }

    void OnAudioFrameCaptured(float[] frame, int channels) // 음성 프레임이 캡처될 때 호출되는 메서드
    {
        Debug.Log("Audio frame captured!");

        //voiceId = channels; // channels를 voiceId로 사용하려면 다른 방법이 필요합니다.

        if (!userAudioStreams.ContainsKey(voiceId)) // 딕셔너리에 해당 화자 ID가 없으면
        {
            // 새로운 화자의 경우, 메모리 스트림 생성
            userAudioStreams[voiceId] = new MemoryStream(); // 메모리 스트림을 생성하여 딕셔너리에 추가
            print("딕셔너리에 화자 ID 추가!");
        }

        // 화자별로 음성 데이터를 저장
        byte[] byteArray = new byte[frame.Length * 4];  // float 배열을 byte 배열로 변환하기 위한 크기 설정
        Buffer.BlockCopy(frame, 0, byteArray, 0, byteArray.Length); // float 배열의 데이터를 byte 배열로 복사
        userAudioStreams[voiceId].Write(byteArray, 0, byteArray.Length); // 메모리 스트림에 byte 배열을 기록
    }

    void OnDestroy() // 객체가 파괴될 때 호출되는 메서드
    {
        foreach (var stream in userAudioStreams.Values) // 모든 메모리 스트림에 대해
        {
            stream.Close();  // 모든 메모리 스트림을 닫음
        }
        audioOutCapture.OnAudioFrame -= OnAudioFrameCaptured; // 이벤트에서 메서드를 제거
    }

    // 서버에 음성 데이터를 전송하는 코루틴
    public IEnumerator UploadAudioToServer(int voiceId) // 특정 화자 ID에 대한 음성을 서버에 업로드하는 코루틴
    {
        if (!userAudioStreams.ContainsKey(voiceId)) // 해당 화자 ID가 없으면
        {
            print("코루틴 종료");
            yield break; // 코루틴 종료
        }

        SaveAudioToWavFile(voiceId);

        MemoryStream audioStream = userAudioStreams[voiceId]; // 해당 화자 ID에 대한 메모리 스트림 가져오기
        audioStream.Seek(0, SeekOrigin.Begin);  // 스트림의 시작점으로 이동

        // HTTP POST 요청을 위한 폼 데이터 생성
        WWWForm form = new WWWForm(); // 새로운 폼 데이터 생성
        form.AddBinaryData("audio_file", audioStream.ToArray(), $"user_{voiceId}_voice.wav", "audio/wav"); // 음성 데이터를 이진 형식으로 추가
        form.AddField("id", voiceId.ToString());  // 화자 구분을 위한 voiceId 추가

        // 서버에 POST 요청 보내기
        using (UnityWebRequest www = UnityWebRequest.Post(serverUrl, form)) // 서버에 POST 요청 생성
        {
            yield return www.SendWebRequest(); // 요청을 보내고 응답을 기다림

            if (www.result != UnityWebRequest.Result.Success) // 요청이 실패하면
            {
                Debug.LogError("Upload failed: " + www.error); // 오류 로그 출력
            }
            else // 요청이 성공하면
            {
                Debug.Log("Upload successful: " + www.downloadHandler.text); // 성공 로그 출력
            }
        }
    }

    // WAV 파일로 저장하는 메서드
    public void SaveAudioToWavFile(int voiceId)
    {
        if (!userAudioStreams.ContainsKey(voiceId)) // 해당 화자 ID가 없으면
        {
            Debug.LogError("No audio stream found for voiceId: " + voiceId);
            return;
        }

        MemoryStream audioStream = userAudioStreams[voiceId]; // 해당 화자 ID에 대한 메모리 스트림 가져오기
        audioStream.Seek(0, SeekOrigin.Begin); // 스트림의 시작점으로 이동

        // 파일 경로 설정
        string filePath = Path.Combine(Application.dataPath, $"user_{voiceId}_voice.wav");

        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            // WAV 헤더 작성
            int sampleCount = (int)(audioStream.Length / 4); // 샘플 수 계산
            WriteWAVHeader(fileStream, 1, 44100, sampleCount); // 채널 수, 샘플 레이트, 샘플 수

            // 메모리 스트림의 내용을 PCM 형식으로 변환하여 파일에 작성
            byte[] byteArray = audioStream.ToArray();
            Int16[] intData = new Int16[byteArray.Length / 2];
            Buffer.BlockCopy(byteArray, 0, intData, 0, byteArray.Length); // 변환된 데이터 복사

            Byte[] bytesData = new Byte[intData.Length * 2];
            Buffer.BlockCopy(intData, 0, bytesData, 0, bytesData.Length); // 변환된 데이터 복사
            fileStream.Write(bytesData, 0, bytesData.Length); // 파일에 작성
        }

        Debug.Log("Audio saved to: " + filePath);
    }


    // WAV 파일 헤더 작성
    private void WriteWAVHeader(FileStream fileStream, int channels, int frequency, int sampleCount)
    {
        var samples = sampleCount * channels;
        var fileSize = samples * 2 + 36; // 2는 16비트 PCM의 바이트 수

        fileStream.Write(new byte[] { 82, 73, 70, 70 }, 0, 4); // "RIFF"
        fileStream.Write(BitConverter.GetBytes(fileSize), 0, 4);
        fileStream.Write(new byte[] { 87, 65, 86, 69 }, 0, 4); // "WAVE"
        fileStream.Write(new byte[] { 102, 109, 116, 32 }, 0, 4); // "fmt "
        fileStream.Write(BitConverter.GetBytes(16), 0, 4); // Subchunk1Size
        fileStream.Write(BitConverter.GetBytes((short)1), 0, 2); // AudioFormat
        fileStream.Write(BitConverter.GetBytes((short)channels), 0, 2); // NumChannels
        fileStream.Write(BitConverter.GetBytes(frequency), 0, 4); // SampleRate
        fileStream.Write(BitConverter.GetBytes(frequency * channels * 2), 0, 4); // ByteRate
        fileStream.Write(BitConverter.GetBytes((short)(channels * 2)), 0, 2); // BlockAlign
        fileStream.Write(BitConverter.GetBytes((short)16), 0, 2); // BitsPerSample
        fileStream.Write(new byte[] { 100, 97, 116, 97 }, 0, 4); // "data"
        fileStream.Write(BitConverter.GetBytes(samples), 0, 4); // Subchunk2Size
    }
}