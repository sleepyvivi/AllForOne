﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class CreatePlayer2 : MonoBehaviour
{
    public InputField nameText;
    private BasePlayer newPlayer;
    public string PlayerName;

    public GameObject inputField;
    public string curPlayerName;
    public GameObject charName;

    public Text strengthText;
    public Text healthText;
    public Text speedText;
    public Text defenseText;

    private int pointsToSpend = 90;
    public Text pointsText;

    public Animator animator;

    public float playTime = 1.5f;
    public float pauseTime = 1.5f;
    public AudioSource melee;
    public AudioSource ocarina;
    public AudioSource theme;

    public GameObject canvas;
    public GameObject canvas2;
    public GameObject placer;
    public GameObject placer2;

    public int state = 1;

    public Button place;

    public Button strengthMinusButton;
    public Button healthMinusButton;
    public Button speedMinusButton;
    public Button defenseMinusButton;

    public Camera cam;
    public Camera unitCam;

    public float time = 10f;
    public Text timeLeft;
    public bool playing = false;

    void Start()
    {
        newPlayer = new BasePlayer();
        strengthText.text = 1.ToString();
        healthText.text = 1.ToString();
        speedText.text = 1.ToString();
        defenseText.text = 1.ToString();

        pointsText.text = 90.ToString();

        placer.SetActive(false);
    }

    void Update()
    {
        curPlayerName = inputField.GetComponent<Text>().text;
        charName.GetComponent<TextMeshProUGUI>().text = curPlayerName;

        if (pointsToSpend == 0)
        {
            Debug.Log("boohoo");
            if (place.enabled)
            {
                //canvas.SetActive(false);
            }
        }

        if (playing == true)
        {
            time -= Time.deltaTime;
            timeLeft.text = (time).ToString();
            if (time < 0)
            {
                time = 10;
                playing = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Player 2")
                {
                    //Debug.Log("CLICKED PLAYER 2 UNIT");
                    cam.enabled = false;
                    //unitCam.enabled = true;
                    hit.collider.GetComponent<ThirdPersonCharacterControl>().ControlUnit();
                    StartCoroutine(SwitchTime());
                    playing = true;
                }
            }
        }
    }

    public void CreateNewPlayer()
    {
        GameInfo.PlayerName = nameText.text;

        GameInfo.Strength = newPlayer.Strength;
        GameInfo.Health = newPlayer.Health;
        GameInfo.Speed = newPlayer.Speed;
        GameInfo.Defense = newPlayer.Defense;

        SaveInfo.SaveAll();

        //SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void UpdateUI()
    {
        strengthText.text = newPlayer.Strength.ToString();
        healthText.text = newPlayer.Health.ToString();
        speedText.text = newPlayer.Speed.ToString();
        defenseText.text = newPlayer.Defense.ToString();
        pointsText.text = pointsToSpend.ToString();
    }

    public void SetStrength(int amount)
    {
        if (amount > 0 && pointsToSpend > 0)
        {
            newPlayer.Strength += amount;
            pointsToSpend -= 2;
            UpdateUI();

        }

        else if (amount < 1)
        {
            newPlayer.Strength += amount;
            pointsToSpend += 2;
            UpdateUI();
        }

        if (newPlayer.Strength >= 1)
        {
            strengthMinusButton.gameObject.SetActive(true);
        }

        if (newPlayer.Strength < 1)
        {
            strengthMinusButton.gameObject.SetActive(false);
        }
    }

    public void SetHealth(int amount)
    {
        if (amount > 0 && pointsToSpend > 0)
        {
            newPlayer.Health += amount;
            pointsToSpend -= 3;
            UpdateUI();

        }

        else if (amount < 1)
        {
            newPlayer.Health += amount;
            pointsToSpend += 3;
            UpdateUI();
        }

        if (newPlayer.Health >= 1)
        {
            healthMinusButton.gameObject.SetActive(true);
        }

        if (newPlayer.Health < 1)
        {
            healthMinusButton.gameObject.SetActive(false);
        }
    }

    public void SetSpeed(int amount)
    {
        if (amount > 0 && pointsToSpend > 0)
        {
            newPlayer.Speed += amount;
            pointsToSpend -= 3;
            UpdateUI();

        }

        else if (amount < 1)
        {
            newPlayer.Speed += amount;
            pointsToSpend += 3;
            UpdateUI();
        }

        if (newPlayer.Speed >= 1)
        {
            speedMinusButton.gameObject.SetActive(true);
        }

        if (newPlayer.Speed < 1)
        {
            speedMinusButton.gameObject.SetActive(false);
        }
    }

    public void SetDefense(int amount)
    {
        if (amount > 0 && pointsToSpend > 0)
        {
            newPlayer.Defense += amount;
            pointsToSpend -= 2;
            UpdateUI();

        }

        else if (amount < 1)
        {
            newPlayer.Defense += amount;
            pointsToSpend += 2;
            UpdateUI();
        }

        if (newPlayer.Defense >= 1)
        {
            defenseMinusButton.gameObject.SetActive(true);
        }

        if (newPlayer.Defense < 1)
        {
            defenseMinusButton.gameObject.SetActive(false);
        }
    }

    public void LoadStuff()
    {
        LoadInfo.LoadAll();
        //SceneManager.LoadScene(0);

        canvas2.SetActive(false);
        placer.SetActive(false);
        placer2.SetActive(true);

        newPlayer.Strength = 1;
        strengthText.text = newPlayer.Strength.ToString();
        newPlayer.Health = 1;
        healthText.text = newPlayer.Health.ToString();
        newPlayer.Speed = 1;
        speedText.text = newPlayer.Speed.ToString();
        newPlayer.Defense = 1;
        defenseText.text = newPlayer.Defense.ToString();
    }

    public void Animations()
    {
        animator.SetTrigger("onBuyPress");
        //melee.Stop();
        //ocarina.Play();   
        StartCoroutine("SwitchSound");
    }

    IEnumerator SwitchSound()
    {
        melee.Pause();
        ocarina.Play();
        yield return new WaitForSeconds(1.5f);
        melee.UnPause();
        yield return null;
    }

    public void SwitchTurn()
    {
        canvas2.SetActive(false);

    }

    public void EndPlacing()
    {
        if (pointsToSpend == 0)
        {
            canvas2.SetActive(false);
        }
    }

    IEnumerator SwitchTime()
    {
        yield return new WaitForSeconds(10f);
        cam.enabled = true;
        yield return null;
    }

    void SwitchSong()
    {
        melee.Stop();
        theme.Play();
    }
}
