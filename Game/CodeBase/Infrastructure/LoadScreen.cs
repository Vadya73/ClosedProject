using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class LoadScreen : MonoBehaviour
    {
        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        public void LoadScene(int sceneIndex)
        {
            Show();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            asyncLoad.allowSceneActivation = false;

            asyncLoad.completed += OnSceneLoaded;
            StartCoroutine(WaitForLoad(asyncLoad));
        }

        private IEnumerator WaitForLoad(AsyncOperation asyncLoad)
        {
            while (!asyncLoad.isDone)
            {
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                Debug.Log($"Loading progress: {progress * 100}%");
            
                if (asyncLoad.progress >= 0.9f)
                    asyncLoad.allowSceneActivation = true;
            
                yield return null;
            }
        }

        private void OnSceneLoaded(AsyncOperation obj)
        {
            Hide();
        }
    }
}