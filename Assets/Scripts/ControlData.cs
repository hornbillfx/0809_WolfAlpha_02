using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlData : MonoBehaviour
{

    public float BaseSpeed ;
    public float TargetSpeed ;
    public float MaxRunForce ;
     public float m_JumpForce;                          // Amount of force added when the player jumps.
    public float playerGravityScale;
    public float terminalVelocity;
    public float walljumpAmplitudeLeft;
    public float walljumpForceLeft;
    public float WallSlideGravity;
    public AudioControl Ac;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Multiplayer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
    }
}
