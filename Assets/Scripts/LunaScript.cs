using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class LunaScript : MonoBehaviour
{
    private float MovementSpeed = 2f;
    public TMP_Text ScoreText;
    public GameObject RobotPrefab;
    public GameObject CrystalPrefab;
    private int CrystalSpawnHeight = 3;
    private int RobotSpawnHeight = 5;
    private Rigidbody rb;
    private int points = 0;
    private System.Random random = new System.Random();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        InvokeRepeating(nameof(spawnCrystal), 2f, 2f);
        InvokeRepeating(nameof(spawnRobot), 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //float z_movement = rb.axis.z;
        //Debug.Log("z_movement is " + z_movement);
        //float x_movement = Input.GetAxis("x");
        //Debug.Log("x_movement is " + z_movement);
        //float y_movement = Input.GetAxis("y");
        //Debug.Log("y_movement is " + z_movement);

        float z_movement = Input.GetAxis("Vertical");
        float x_movement = Input.GetAxis("Horizontal");

        if (Input.GetButton("Vertical"))
        {
            rb.linearVelocity =  new Vector3(transform.position.x, transform.position.y, (transform.position.z * MovementSpeed * z_movement));
        }
        if (Input.GetButton("Horizontal"))
        {
            rb.linearVelocity = new Vector3(transform.position.x * MovementSpeed * x_movement, transform.position.y, transform.position.z);
        }
    }

    void spawnCrystal()
    {
        GameObject SpawnedCrystal = Instantiate(CrystalPrefab, new Vector3(transform.position.x + random.Next(-7, 7), transform.position.y + CrystalSpawnHeight, transform.position.z + random.Next(-7, 7)), quaternion.identity);
        Destroy(SpawnedCrystal, 4f);
    }

    void spawnRobot()
    {
        GameObject SpawnedRobot = Instantiate(RobotPrefab, new Vector3(transform.position.x + random.Next(-7, 7), transform.position.y + RobotSpawnHeight, transform.position.z + random.Next(-7, 7)), quaternion.identity);
        Destroy(SpawnedRobot, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            points += 50;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            points -= 100;
        }

        ScoreText.SetText("Score: " + points);
        if (points < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
