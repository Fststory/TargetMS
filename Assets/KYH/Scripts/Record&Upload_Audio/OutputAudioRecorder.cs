using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OutputAudioRecorder : MonoBehaviour
{
    private AudioClip clip;

    public AudioUploader audioUploader;

    string[] micList;
    void Awake()
    {
        micList = Microphone.devices;
    }

    private void Update()   // 녹음 시작, 종료 시점에 마이크 오브젝트 led 조절해도 될 듯
    {
        // 숫자 5번 누르면 녹음 시작!
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            print("녹음 시작!");
            clip = Microphone.Start(micList[0], true, 30, 44100);   // 녹음하는 오디오 클립이 clip 에 담김
        }
        // 숫자 6번 누르면 녹음 종료!
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            print("녹음 종료!");
            Microphone.End(micList[0]);
        }
        // 숫자 7번 누르면 wav파일로 저장!
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            print("WAV 저장 처리 시작!");
            SaveAudioClipToWAV(Application.dataPath + "/test.wav");
        }
        // 숫자 8번 누르면 서버에 보내기
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            print("서버에 WAV 보냄!");
            audioUploader.UploadAudioFile(Application.dataPath + "/test.wav");
        }
    }

    public void SaveAudioClipToWAV(string filePath)
    {
        if (clip == null)
        {
            Debug.LogError("No AudioClip assigned.");
            return;
        }
                
        float[] samples = new float[clip.samples];  // 녹음한 클립의 데이터 배열
        clip.GetData(samples, 0);   // AudioClip의 오디오 데이터 가져오기

        using (FileStream fs = File.Create(filePath))   // filePath에 만들어지는 파일의 FileStream 을 fs 에 반환
        {
            WriteWAVHeader(fs, clip.channels, clip.frequency, clip.samples);    // WAV 파일 헤더 작성
            ConvertAndWrite(fs, samples);    // 오디오 데이터 변환 및 작성
        }// using 이 끝나면 fs 도 닫힘

        Debug.Log("AudioClip saved as WAV: " + filePath);
    }

    // WAV 파일 헤더 작성
    private void WriteWAVHeader(FileStream fileStream, int channels, int frequency, int sampleCount)
    {
        var samples = sampleCount * channels;   // 총 샘플 수
        var fileSize = samples + 36;    // 파일 크기 계산

        fileStream.Write(new byte[] { 82, 73, 70, 70 }, 0, 4); // "RIFF" 헤더
        fileStream.Write(BitConverter.GetBytes(fileSize), 0, 4);
        fileStream.Write(new byte[] { 87, 65, 86, 69 }, 0, 4); // "WAVE" 헤더
        fileStream.Write(new byte[] { 102, 109, 116, 32 }, 0, 4); // "fmt " 헤더
        fileStream.Write(BitConverter.GetBytes(16), 0, 4); // 16
        fileStream.Write(BitConverter.GetBytes(1), 0, 2); // 오디오 포맷 (PCM)
        fileStream.Write(BitConverter.GetBytes(channels), 0, 2); // 채널 수
        fileStream.Write(BitConverter.GetBytes(frequency), 0, 4); // 샘플 레이트
        fileStream.Write(BitConverter.GetBytes(frequency * channels * 2), 0, 4); // 바이트 레이트
        fileStream.Write(BitConverter.GetBytes(channels * 2), 0, 2); // 블록 크기
        fileStream.Write(BitConverter.GetBytes(16), 0, 2); // 비트 레이트
        fileStream.Write(new byte[] { 100, 97, 116, 97 }, 0, 4); // "data" 헤더
        fileStream.Write(BitConverter.GetBytes(samples), 0, 4);
    }

    // 오디오 데이터 변환 및 작성
    private void ConvertAndWrite(FileStream fileStream, float[] samples)
    {
        Int16[] intData = new Int16[samples.Length];    // int16 배열 생성
        // float -> Int16 변환
        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * 32767);   // 샘플 값을 int16 으로 변환
        }
        // Int16 데이터 작성
        Byte[] bytesData = new Byte[intData.Length * 2];    // Byte 배열 생성
        Buffer.BlockCopy(intData, 0, bytesData, 0, bytesData.Length);   // 변환된 데이터 복사
        fileStream.Write(bytesData, 0, bytesData.Length);   // 파일에 작성
    }
}
