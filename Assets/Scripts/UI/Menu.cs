using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    public void platformerBtnClick() {
        SceneManager.LoadSceneAsync("Platformer");
    }

    public void flyingBtnClick() {

    }

    public void settingsBtnClick() {
        SceneManager.LoadSceneAsync("Settings");
    }

    public void editorsBtnClick() {
        SceneManager.LoadSceneAsync("RuntimeGraph");
    }

}
