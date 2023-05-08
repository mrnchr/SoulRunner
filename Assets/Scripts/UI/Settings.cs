using Rewired.UI.ControlMapper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    [SerializeField]
    private ControlMapper _controlMapper;

    public void backBtnClick() {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void controlsBtnClick() {
        _controlMapper.Open();
        
    }


}
