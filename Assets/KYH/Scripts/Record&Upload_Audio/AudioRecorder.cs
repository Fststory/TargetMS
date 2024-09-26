using System;
using System.IO;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip clip;
    public AudioUploader audioUploader;
    string[] micList;
    private float recordingStartTime;
    private bool isRecording = false;

    void Awake()
    {
        micList = Microphone.devices;
    }

    private void Update()
    {
        // 숫자 5번 누르면 녹음 시작!
        if (Input.GetKeyDown(KeyCode.Alpha5) && !isRecording)
        {
            print("녹음 시작!");
            clip = Microphone.Start(micList[0], true, 10, 44100); // 최대 10초까지 녹음 가능
            recordingStartTime = Time.time; // 녹음 시작 시간 기록
            isRecording = true;
        }

        // 숫자 6번 누르면 녹음 종료!
        if (Input.GetKeyDown(KeyCode.Alpha6) && isRecording)
        {
            StopRecording();
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

    private void StopRecording()
    {
        if (clip != null && isRecording)
        {
            print("녹음 종료!");
            Microphone.End(micList[0]);

            // 녹음된 시간 계산
            float recordingDuration = Time.time - recordingStartTime;
            Debug.Log("녹음된 시간: " + recordingDuration + "초");

            // 클립 길이 조정
            int samples = (int)(recordingDuration * clip.frequency);
            AudioClip trimmedClip = AudioClip.Create(clip.name, 2 * samples, clip.channels, clip.frequency, false);

            float[] samplesData = new float[clip.samples];
            clip.GetData(samplesData, 0);

            // 필요한 만큼의 샘플만 설정
            trimmedClip.SetData(samplesData, 0);
            clip = trimmedClip; // 원래 클립을 조정된 클립으로 대체

            isRecording = false;
        }
    }

    public void SaveAudioClipToWAV(string filePath)
    {
        if (clip == null)
        {
            Debug.LogError("No AudioClip assigned.");
            return;
        }

        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        using (FileStream fs = File.Create(filePath))
        {
            WriteWAVHeader(fs, clip.channels, clip.frequency, clip.samples);
            ConvertAndWrite(fs, samples);
        }

        Debug.Log("AudioClip saved as WAV: " + filePath);
    }

    private void WriteWAVHeader(FileStream fileStream, int channels, int frequency, int sampleCount)
    {
        var samples = sampleCount * channels;
        var fileSize = samples + 36;

        fileStream.Write(new byte[] { 82, 73, 70, 70 }, 0, 4);
        fileStream.Write(BitConverter.GetBytes(fileSize), 0, 4);
        fileStream.Write(new byte[] { 87, 65, 86, 69 }, 0, 4);
        fileStream.Write(new byte[] { 102, 109, 116, 32 }, 0, 4);
        fileStream.Write(BitConverter.GetBytes(16), 0, 4);
        fileStream.Write(BitConverter.GetBytes(1), 0, 2);
        fileStream.Write(BitConverter.GetBytes(channels), 0, 2);
        fileStream.Write(BitConverter.GetBytes(frequency), 0, 4);
        fileStream.Write(BitConverter.GetBytes(frequency * channels * 2), 0, 4);
        fileStream.Write(BitConverter.GetBytes(channels * 2), 0, 2);
        fileStream.Write(BitConverter.GetBytes(16), 0, 2);
        fileStream.Write(new byte[] { 100, 97, 116, 97 }, 0, 4);
        fileStream.Write(BitConverter.GetBytes(samples), 0, 4);
    }

    private void ConvertAndWrite(FileStream fileStream, float[] samples)
    {
        Int16[] intData = new Int16[samples.Length];
        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * 32767);
        }
        Byte[] bytesData = new Byte[intData.Length * 2];
        Buffer.BlockCopy(intData, 0, bytesData, 0, bytesData.Length);
        fileStream.Write(bytesData, 0, bytesData.Length);
    }
}
