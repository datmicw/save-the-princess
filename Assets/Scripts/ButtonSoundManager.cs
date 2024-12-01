using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioSource audioSource;  // Tham chiếu đến AudioSource
    public AudioClip clickSound;     // Tham chiếu đến âm thanh click

    // Hàm phát âm thanh khi nhấn nút
    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(clickSound);  // Phát âm thanh
    }
}
