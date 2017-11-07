using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour {

	private IEnumerator Start () {
		while(!LocalizationManager.instance.FinishedLoading) {
            yield return null; //wait one frame
        }
        SceneManager.LoadScene("MainMenu");
	}
}
