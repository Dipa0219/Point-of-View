using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

public class MultiPlatformManager : MonoBehaviour
{
    public enum Phase
    {
        P1,
        P2,
        P3,
        P4
    }
            
            
    [SerializeField] private GameObject Platform_A;
    [SerializeField] private GameObject Platform_B;
    [SerializeField] private GameObject Platform_C;
    [SerializeField] private GameObject Platform_D;
    [SerializeField] private GameObject Platform_E;
    //[SerializeField] private GameObject Platform_F; -> useless2
    [SerializeField] private GameObject Platform_G;
    [SerializeField] private GameObject Platform_H;
    
    [SerializeField] private GameObject Platform_Useless_1;
    [SerializeField] private GameObject Platform_Useless_2;
    
    [SerializeField] private Transform[] alternative_waypoints;
    [SerializeField] private Transform[] waypoints_to_be_substituted;
    
    private Phase currentPhase;
    //[SerializeField] private Dictionary<GameObject, List<Phase>> platformPhases = new Dictionary<GameObject, List<Phase>>();
    
   
    // Start is called before the first frame update
    void Start()
    {
        currentPhase = Phase.P1;
        changePhase(Phase.P1);
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            changePhase(Phase.P1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            changePhase(Phase.P2);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            changePhase(Phase.P3);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            changePhase(Phase.P4);
        }
        */
    }


    // ReSharper disable Unity.PerformanceAnalysis
    public void changePhase(Phase newPhase)
    {
        /*if (currentPhase == newPhase)
        {
            return;
        }*/
        
        //1 A B C U1
        //2 A D G U1
        //3 D' E U2
        //4 D' H U2
        
        
        currentPhase = newPhase;
        print("Phase: " + newPhase);
        switch (newPhase)
        {
            case Phase.P1:
                
                Platform_A.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_B.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_C.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_Useless_1.GetComponent<MovingPlatformUnbound>().StartMoving();
                
                Platform_D.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_E.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_G.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_H.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_Useless_2.GetComponent<MovingPlatformUnbound>().StopMoving();

                //Platform_A.GetComponent()
                /*
                Platform_A.SetActive(true);
                Platform_B.SetActive(true);
                Platform_C.SetActive(true);
                Platform_D.SetActive(true);
                Platform_E.SetActive(true);
                Platform_G.SetActive(true);
                Platform_H.SetActive(true);
                Platform_Useless_1.SetActive(false);
                Platform_Useless_2.SetActive(false);
                */
                break;
            case Phase.P2:
                Platform_A.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_D.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_G.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_Useless_1.GetComponent<MovingPlatformUnbound>().StartMoving();
                
                Platform_B.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_C.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_E.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_H.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_Useless_2.GetComponent<MovingPlatformUnbound>().StopMoving();
                /*
                Platform_A.SetActive(false);
                Platform_B.SetActive(false);
                Platform_C.SetActive(false);
                Platform_D.SetActive(false);
                Platform_E.SetActive(false);
                Platform_G.SetActive(false);
                Platform_H.SetActive(false);
                Platform_Useless_1.SetActive(true);
                Platform_Useless_2.SetActive(true);
                */
                break;
            case Phase.P3:

                Platform_D.GetComponent<MovingPlatformUnbound>().SwitchWaypoints(waypoints_to_be_substituted, alternative_waypoints);
                
                Platform_D.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_E.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_Useless_2.GetComponent<MovingPlatformUnbound>().StartMoving();
                
                Platform_A.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_B.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_C.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_G.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_H.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_Useless_1.GetComponent<MovingPlatformUnbound>().StopMoving();
                /*
                Platform_A.SetActive(true);
                Platform_B.SetActive(true);
                Platform_C.SetActive(true);
                Platform_D.SetActive(true);
                Platform_E.SetActive(true);
                Platform_G.SetActive(true);
                Platform_H.SetActive(true);
                Platform_Useless_1.SetActive(false);
                Platform_Useless_2.SetActive(false);
                */
                break;
            case Phase.P4:

                Platform_D.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_H.GetComponent<MovingPlatformUnbound>().StartMoving();
                Platform_Useless_2.GetComponent<MovingPlatformUnbound>().StartMoving();
                
                Platform_A.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_B.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_C.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_E.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_G.GetComponent<MovingPlatformUnbound>().StopMoving();
                Platform_Useless_1.GetComponent<MovingPlatformUnbound>().StopMoving();
                
                /*
                Platform_A.SetActive(false);
                Platform_B.SetActive(false);
                Platform_C.SetActive(false);
                Platform_D.SetActive(false);
                Platform_E.SetActive(false);
                Platform_G.SetActive(false);
                Platform_H.SetActive(false);
                Platform_Useless_1.SetActive(true);
                Platform_Useless_2.SetActive(true);
                */
                break;
            default:
                break;
        }
    }
}
