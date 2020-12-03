using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    float speed = 10.0f;
    bool TimeRunning;
    bool PillsPicked;

    public GameObject AddCountText;
    public GameObject MinusCountText;
    public GameObject TotalEnergyText;
    public GameObject EnergyCountText;
    public Animator PlayerAni;
    public int energyCount;
    

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb.GetComponent<Rigidbody>(); //Caling Rigidbody for collision checks for private
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Forward
        if (Input.GetKey(KeyCode.W) && TimeRunning == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            PlayerAni.SetBool("RunningBool", true);

        }
        //Movement Backwards
        if (Input.GetKey(KeyCode.S) && TimeRunning == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayerAni.SetBool("RunningBool", true);
        }
        //Movement Left
        if (Input.GetKey(KeyCode.A) && TimeRunning == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            PlayerAni.SetBool("RunningBool", true);
        }
        //Movement Right
        if (Input.GetKey(KeyCode.D) && TimeRunning == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            PlayerAni.SetBool("RunningBool", true);
        }

        if(Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.D) && TimeRunning == true)
        {
            PlayerAni.SetBool("RunningBool", false);
        }


        //Printing text onto the screen to display the following.
        EnergyCountText.GetComponent<Text>().text = "Energy Collected : " + energyCount; //Showing Energy Collected

        AddCountText.GetComponent<Text>().text = "Add Energy Left : " + GameManager.instance.AddCubeSize; //Total Add Cube 

        MinusCountText.GetComponent<Text>().text = "Minus Energy Left : " + GameManager.instance.MinusCubeSize;//Total Minus Cube

        TotalEnergyText.GetComponent<Text>().text = "Total Energy Left : " + GameManager.instance.TotalCubeSize;// Total Cube
        //If Energy falls below -1 or time reaches 0 you lose
        if (energyCount <= -1 || GameManager.instance.levelTime <= 0) 
        {
            TimeRunning = false;

            SceneManager.LoadScene("LoseScene");
        }
        //If Energy goes above 50 you win
        else if (energyCount >= 50)
        {
            SceneManager.LoadScene("WinScene");
        }
        //This is to check time has not ran out
        else
        {
            TimeRunning = true;
        }

        Pills();
    }

    private void OnCollisionEnter(Collision Player)
    {
        //if Player colide with addcube it adds time, energy collected and spawn more cubes.
        if (Player.gameObject.CompareTag("AddEnergy"))
        {
            energyCount += 5;

            GameManager.instance.AddTime();

            Destroy(Player.gameObject);
        }
        if (Player.gameObject.CompareTag("MinusEnergy"))
        {
            energyCount -= 25;
            Destroy(Player.gameObject);
        }
        if (Player.gameObject.CompareTag("Pills"))
        {
            PillsPicked = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pills"))
        {
            PillsPicked = false;
        }
    }

    public void Pills()
    {
        if (PillsPicked == true)
        {
            while (GameManager.instance.MinusCubeSize > 0)
            {
                Destroy(GameObject.FindGameObjectWithTag("MinusEnergy"));
                GameManager.instance.MinusCubeSize--;
            }
        }
        else
        {
            PillsPicked = false;
        }
    }
}
