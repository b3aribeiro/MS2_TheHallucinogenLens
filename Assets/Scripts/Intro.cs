using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    //Breathing
    public Animation _anim;
    public GameObject _gameobj;
    public TextMeshProUGUI _text;

    private float waitTime = 4f;
    private int roundCounter = 0;

    MenuController _menuController;

    // Start is called before the first frame update
    void Start()
    {

        _menuController = FindObjectOfType<MenuController>();

        _anim = gameObject.GetComponent<Animation>();

        _text = gameObject.GetComponent<TextMeshProUGUI>();

        StartCoroutine(Waiting(waitTime));

    }

    // Update is called once per frame
    void Update()
    {
        if (_anim.isPlaying)
        {
            print("isPlaying" + roundCounter);
            return;
        } else if(roundCounter == 0)
        {
            _text.text = "take a deep breath";
            print(_text.text);
            StartCoroutine(Waiting(waitTime));
            _anim.Play("fade");
            StartCoroutine(Waiting(waitTime));
            roundCounter++;
        } else  if(roundCounter == 1)
        {
            _text.text = "breathe out";
            _anim.Play("fade");
            StartCoroutine(Waiting(waitTime));
            roundCounter++;
        } else if(roundCounter == 2)
        {
            _text.text = "breathe in";
            _anim.Play("fade");
            StartCoroutine(Waiting(waitTime));
            roundCounter++;
        } else if(roundCounter == 3)
        {
            _text.text = "breathe out";
            _anim.Play("fade");
            StartCoroutine(Waiting(waitTime));
            roundCounter++;
        } else if(roundCounter == 4)
        {
            _text.text = "breathe in";
            _anim.Play("fade");
            StartCoroutine(Waiting(waitTime));
            roundCounter++;
        } else if(roundCounter == 5)
        {
            _text.text = "breathe out";
            _anim.Play("fade");
            StartCoroutine(Waiting(waitTime));
            roundCounter++;
        } else if(roundCounter == 6)
        {
            StartCoroutine(Waiting(waitTime));
            roundCounter++;
        }
    }

      private IEnumerator Waiting(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(roundCounter == 7) 
        {print("ops");
            _menuController.SimulationBegins();}
    }
}
