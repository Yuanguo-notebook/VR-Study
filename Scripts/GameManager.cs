using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

//The Game Manager keeps track of which scenes to load, handles loading scenes, fading between scenes and also video playing/pausing

namespace Interactive360
{

    public class GameManager : MonoBehaviour
    {

        public static GameManager instance = null;

        Scene scene;
        VideoPlayer video;
        Animator anim;
        Image fadeImage;

        AsyncOperation operation;


        [Header("Scene Management")]
        public string[] scenesToLoad;
        public string activeScene;

        [Space]
        [Header("UI Settings")]

        public bool useFade;
        public GameObject fadeOverlay;
       


        //make sure that we only have a single instance of the game manager
        void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        
        //set the initial active scene
        void Start()
		{
			scene = SceneManager.GetActiveScene ();
			activeScene = scene.buildIndex + " - " + scene.name;


		}

		void Update(){
			fadeOverlay = GameObject.FindGameObjectWithTag ("Fade");
		}
			

        //Select scene is called from either the menu manager or hotspot manager, and is used to load the desired scene
        public void SelectScene(string sceneToLoad)
        {
            //if we want to use the fading between scenes, start the coroutine here
			if (useFade )
            {
                StartCoroutine(FadeOutAndIn(sceneToLoad));
            }
            //if we dont want to use fading, just load the next scene
            else
            {
                SceneManager.LoadScene(sceneToLoad);

            }
            //set the active scene to the next scene
            activeScene = sceneToLoad;
        }

        IEnumerator FadeOutAndIn(string sceneToLoad)
        {
            //get references to animatior and image component 
            anim = fadeOverlay.GetComponent<Animator>();
            fadeImage = fadeOverlay.GetComponent<Image>();

           

            //set FadeOut to true on the animator so our image will fade out
            anim.SetBool("FadeOut", true);

            //wait until the fade image is entirely black (alpha=1) then load next scene
            yield return new WaitUntil(() => fadeImage.color.a == 1);
            SceneManager.LoadScene(sceneToLoad);
            Scene scene = SceneManager.GetSceneByName(sceneToLoad);

			
            yield return new WaitUntil(() => scene.isLoaded);
            // grab video and wait until it is loaded and prepared before starting the fade out
			if (sceneToLoad.Contains ("Scene")) {
				video = FindObjectOfType<VideoPlayer>();

				yield return new WaitUntil(() => video.isPrepared);

			}
            
            //set FadeOUt to false on the animator so our image will fade back in 
            anim.SetBool("FadeOut", false);
            yield return new WaitUntil(() => fadeImage.color.a == 0);
          
            

        }

        //Find the video in the scene and pause it
        public void PauseVideo()
        {
            if (!video)
            {
                video = FindObjectOfType<VideoPlayer>();
            }
            video.Pause();
        }

        //Find the video in the scene and play it
        public void PlayVideo()
        {
            if (!video)
            {
                video = FindObjectOfType<VideoPlayer>();
            }
            video.Play();
        }
    }
}

