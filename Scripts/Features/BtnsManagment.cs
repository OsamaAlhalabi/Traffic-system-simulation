using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnsManagment : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool playerCarActivated = false;
    public static bool steeringOnRun = false;
    public GameObject pauseMenuUI;
    public Camera mainCamera;
    public GameObject setting;
    public GameObject exit;
    public GameObject restart;
    public GameObject VarsMenu;
    public GameObject drive;
    public GameObject steer;
    public GameObject FrMove;
    public GameObject carPFtoDrive;
    public GameObject viewManagerObj;
    private Color OrgColor;
    private Vector3 cameraLoc;
    private Vector3 cameraRot;
    private GameObject playerCar2;
    private Vector3 mapCamPos;
    private Vector3 mapCamRot;

    public void HideBtns()
    {
        setting.SetActive(false);
        VarsMenu.SetActive(false);
    }
    public void ShowBtns()
    {
        setting.SetActive(true);
        VarsMenu.SetActive(true);
    }
    public void Pause()
    {
       pauseMenuUI.SetActive(true);
       Time.timeScale = 0.0f;
       gameIsPaused = true;
       HideBtns();
       
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        ShowBtns();
        Time.timeScale = 1f;
        gameIsPaused = false;
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }
    public void Drive()
    {
    
        if (!playerCarActivated)
        {
            carPFtoDrive.SetActive(true);
            playerCarActivated = true;
            mainCamera.GetComponent<CamMouseOrbit>().Activate(true);
            mainCamera.GetComponent<CameraManagment>().enabled = false;
            drive.GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            carPFtoDrive.SetActive(false) ;
            playerCarActivated = false ;
            mainCamera.GetComponent<CamMouseOrbit>().Activate(false) ;
            mainCamera.GetComponent<CameraManagment>().enabled = true;
            drive.GetComponentInChildren<Text>().color = OrgColor;
            Map(false);
        }
        
        Resume();
            
    }
    public void Steer()
    {
        if (!steeringOnRun)
        {
            steeringOnRun = true;
            Map(true);
            viewManagerObj.GetComponent<SteeringBtn>().enabled = true;
            mainCamera.GetComponent<CameraManagment>().enabled = false;
            steer.GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            steeringOnRun = false;
            Map(false);
            viewManagerObj.GetComponent<SteeringBtn>().enabled = false;
            mainCamera.GetComponent<CameraManagment>().enabled = true;
            steer.GetComponentInChildren<Text>().color = OrgColor;
        }
      
    }

    public void FreelyCamera()
    {
        if (mainCamera.GetComponent<CameraFreeFly>().Activated())
        {
            FrMove.GetComponentInChildren<Text>().color = OrgColor;
            mainCamera.GetComponent<CameraFreeFly>().Activate(false);
            mainCamera.GetComponent<CameraManagment>().enabled = false;
        }
        else
        {
            FrMove.GetComponentInChildren<Text>().color = Color.red;
            mainCamera.GetComponent<CameraManagment>().enabled = true;
            mainCamera.GetComponent<CameraFreeFly>().Activate(true);
        }
          
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Map(bool activate)
    {
        if (activate)
        {
            mainCamera.transform.position = mapCamPos;
            mainCamera.transform.eulerAngles = mapCamRot;
        }
        else
        {
            mainCamera.transform.position = cameraLoc;
            mainCamera.transform.eulerAngles = cameraRot;
        }
    }
    public void Start()
    {
        OrgColor = drive.GetComponentInChildren<Text>().color;
        cameraLoc = mainCamera.transform.position;
        cameraRot = mainCamera.transform.eulerAngles;
        mapCamPos = new Vector3(75.0f, 900.0f, 0.0f);
        mapCamRot = new Vector3(90.0f, 0.0f, 0.0f);

    }
    public void Update()
    {
 
            if (playerCarActivated)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Mouse is down");

                    RaycastHit hitInfo = new RaycastHit();
                    bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                    if (hit)
                    {
                        if (hitInfo.transform.gameObject.tag == "Car")
                        {
                            hitInfo.transform.gameObject.GetComponent<CarAIController>().enabled = false;
                            hitInfo.transform.gameObject.AddComponent<CarControl>();
                            Camera.main.GetComponent<CamMouseOrbit>().SetCarToDrive(hitInfo.transform.gameObject);
                        }
                    }
                }
            }
            
        

    }
}
