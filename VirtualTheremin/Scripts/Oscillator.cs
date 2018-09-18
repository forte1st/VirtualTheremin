using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualTheremin {
    public class Oscillator : MonoBehaviour {

        public GameObject volumeHand;       // 左手
        public GameObject pitchHand;        // 右手
        public GameObject volumeAntenna;
        public GameObject pitchAntenna;
        public GameObject marker;

        [Range(0.1f, 50)]
        public float volumeSensitivity;     // 高いほどアンテナ付近での音量変化が大きくなります (デフォルト: 6)
        [Range(0.1f, 50)]
        public float pitchSensitivity;      // 高いほどアンテナ付近でのピッチ変化が大きくなります (デフォルト: 10)
        [Range(0, 10)]
        public float pitchMax;              // ピッチの最大値 (デフォルト: 4)
        [Range(0, 10)]
        public float pitchMin;              // ピッチの最小値 (デフォルト: 0.25)
        public bool markerIsVisible;        // 音階目盛の表示

        private AudioSource audioSource;
        private float volume;
        private float volumeHandDistance;
        private float pitch;
        private float pitchHandDistance;

        private readonly int markerRange = 24;                   // 目盛の表示範囲
        private readonly int[] blackKeys = { 1, 4, 6, 9, 11 };     // ラを0とした時の黒鍵の音階
        private GameObject[] markers;
        private readonly Vector3 blackKeyScale = new Vector3(1, 0.5f, 1);
        private readonly float frequencyRatio = Mathf.Pow(2, 1.0f / 12.0f);     //隣り合う音の周波数比

        void Start() {
            audioSource = gameObject.GetComponent<AudioSource>();

            // 目盛の生成
            markers = new GameObject[markerRange * 2 + 1];
            for (int i = 0; i < markers.Length; i++) {
                markers[i] = Instantiate(marker, transform);
                foreach (int j in blackKeys) {

                    // ドの色を変更
                    if (i % 12 == 3) {
                        markers[i].GetComponent<Renderer>().material.color = Color.red;
                        break;
                    }

                    // 黒鍵の色と長さを変更
                    if (i % 12 == j) {
                        markers[i].GetComponent<Renderer>().material.color = Color.black;
                        markers[i].transform.localScale = Vector3.Scale(markers[i].transform.localScale, blackKeyScale);
                        break;
                    }
                }
            }
        }

        void Update() {
            // 音量
            volumeHandDistance = Mathf.Max(volumeHand.transform.position.y - volumeAntenna.transform.position.y, 0);
            volume = 1 - Mathf.Exp(-volumeHandDistance * volumeSensitivity);
            audioSource.volume = volume;

            // ピッチ
            pitchHandDistance = HorizontalDistance(pitchHand.transform.position, pitchAntenna.transform.position);
            pitch = Mathf.Exp(-pitchHandDistance * pitchSensitivity) * (pitchMax - pitchMin) + pitchMin;
            audioSource.pitch = pitch;

            // 目盛
            for (int i = 0; i < markers.Length; i++) {
                float a = (Mathf.Pow(frequencyRatio, i - markerRange) - pitchMin) / (pitchMax - pitchMin);
                float distance;

                if (a > 0) {
                    distance = -(1 / pitchSensitivity) * Mathf.Log(a);
                } else {
                    distance = 0;
                }

                if (distance > 0) {
                    markers[i].transform.localPosition = pitchAntenna.transform.localPosition + Vector3.left * distance;
                } else {
                    markers[i].transform.localPosition = pitchAntenna.transform.localPosition;
                }

                markers[i].SetActive(markerIsVisible);
            }
        }

        //水平方向の距離を求める
        float HorizontalDistance(Vector3 v1, Vector3 v2) {
            return Vector2.Distance(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z));
        }
    }
}
