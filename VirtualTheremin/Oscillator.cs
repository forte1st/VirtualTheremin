using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualTheremin {
    public class Oscillator : MonoBehaviour {

        public GameObject volumeHand;       // 左手のGameObjectをアタッチ (Oculus TouchならLeftHandAnchor)
        public GameObject pitchHand;        // 右手のGameObjectをアタッチ (Oculus TouchならRightHandAnchor)
        public GameObject volumeAntenna;
        public GameObject pitchAntenna;
        public float volumeSensitivity;     // 高いほどアンテナ付近での音量変化が大きくなります (デフォルト: 5)
        public float pitchSensitivity;      // 高いほどアンテナ付近でのピッチ変化が大きくなります (デフォルト: 10)
        public float pitchMax;              // ピッチの最大値 (デフォルト: 4)
        public float pitchMin;              // ピッチの最小値 (デフォルト: 0.25)

        private AudioSource audioSource;
        private float volume;
        private float volumeHandDistance;
        private float pitch;
        private float pitchHandDistance;

        void Start() {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        void Update() {
            volumeHandDistance = Mathf.Max(volumeHand.transform.position.y - volumeAntenna.transform.position.y, 0);
            volume = 1 - Mathf.Exp(-volumeHandDistance * volumeSensitivity);
            audioSource.volume = volume;

            pitchHandDistance = HorizontalDistance(pitchHand.transform.position, pitchAntenna.transform.position);
            pitch = Mathf.Exp(-pitchHandDistance * pitchSensitivity) * (pitchMax - pitchMin) + pitchMin;
            audioSource.pitch = pitch;
        }

        float HorizontalDistance(Vector3 v1, Vector3 v2) {
            return Vector2.Distance(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z));
        }
    }
}