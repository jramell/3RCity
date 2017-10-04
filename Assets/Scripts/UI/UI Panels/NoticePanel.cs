using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePanel : MonoBehaviour
{
    /// <summary>
    /// UI Text elemts that have the message that's going to be displayed.
    /// </summary>
    [SerializeField]
    private Text messageText;

    /// <summary>
    /// 
    /// </summary>
    [Range(1,10)]
    [Tooltip("Seconds that the notice is going to be displayed.")]
    public float noticeDuration;

    public void DisplayNotice(string noticeText = "")
    {
        messageText.text = noticeText;
        gameObject.SetActive(true);
        StartCoroutine(DisplayRoutine());
    }

    /// <summary>
    /// Displays the panel for a fixed amount of seconds, then hides it. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisplayRoutine()
    {      
        yield return new WaitForSeconds(noticeDuration);
        gameObject.SetActive(false);
    }
}
