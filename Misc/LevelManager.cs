using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour
{
    private Scene _currentScene = default(Scene);
    private bool _isLoading = false;

    // With { get; private set; } I can publicly get Instance, but cannot set it, only from inside this class can it be managed.
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    private GameObject player;
    private CharacterController cc;
    public Footsteps footStepsScript;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        player = GameObject.FindWithTag("Player");
        cc = player.GetComponent<CharacterController>();
    }

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        ResetPlayerWhenMenuLoaded();
    }

    private void ResetPlayerWhenMenuLoaded()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            // Reset player
            PlayerSpawnPoint.enteredHomeForFirstTime = false;
            player.transform.localPosition = new Vector3(185.009f, 0f, 75.0009f);
            player.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);

            // Prevent from walking around when menu is loaded
            cc.enabled = false;
            footStepsScript.audioSrc.volume = 0;
        }
        else
        {
            cc.enabled = true;
        }
    }

    public static void EnableScene(int index)
    {
        _instance.DoEnableScene(index);
    }

    private void DoEnableScene(int index)
    {
        // Prevent loading multiple scenes at once.
        if (!_isLoading)
        {
            _isLoading = true;
            StartCoroutine(EnableSceneCoroutine(index));
        }
    }

    IEnumerator EnableSceneCoroutine(int index)
    {
        // Get the next scene and load it is necessary.
        var nextScene = SceneManager.GetSceneByBuildIndex(index);

        // This will be invalid if the scene is not yet loaded.
        var isFirstLoad = !nextScene.IsValid();

        if (isFirstLoad)
        {
            // Load the scene in.
            yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

            // Now that the scene is loaded we need to get a valid scene object.
            nextScene = SceneManager.GetSceneByBuildIndex(index);
        }

        // If we already have a scene active, disable its roots.
        if (_currentScene.IsValid())
        {
            SetSceneRootsActive(_currentScene, false);
        }

        // Update scene reference.
        _currentScene = nextScene;

        // Enable the new scene's roots.
        SetSceneRootsActive(_currentScene, true);

        _isLoading = false;
    }

    List<GameObject> _rootObjects = new List<GameObject>();
    private void SetSceneRootsActive(Scene scene, bool isActive)
    {
        if (!scene.IsValid())
        {
            throw new InvalidOperationException($"{scene.name} is not valid");
        }
        scene.GetRootGameObjects(_rootObjects);

        // Delay setting objects active to prevent scene error
        StartCoroutine(DelaySettingActive(isActive));
    }

    private IEnumerator DelaySettingActive(bool isActive)
    {
        yield return new WaitForSeconds(0.1f);
        foreach (var root in _rootObjects)
        {
            root.SetActive(isActive);
        }
    }
}