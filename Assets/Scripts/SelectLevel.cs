using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			RestartLevel();
		}
	}

	public void RestartLevel()
	{
		Basket.Instance.Fail(false);
	}

	public void NextLevel()
	{
		int numberOfScenes = SceneManager.sceneCountInBuildSettings;
		int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;

		if(currentSceneNumber < numberOfScenes-1)
		{
			SceneManager.LoadScene(currentSceneNumber+1);
		}
	}
}
