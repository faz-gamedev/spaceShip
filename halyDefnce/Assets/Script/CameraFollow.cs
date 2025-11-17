using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;        
    [SerializeField] private GameObject cabin;        
    [SerializeField] private MeshRenderer shipMeshe;        
    [SerializeField] private Transform camPositionDefault;   
    [SerializeField] private Transform camPositionAlternative; 
    [SerializeField] private float positionSmoothTime = 9; 
    [SerializeField] private bool switchCam = false;

    private Transform cam;  


    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        if (switchCam==false)
        {
            Vector3 desiredPosition = camPositionDefault.position;

            Vector3 smoothedPosition = Vector3.Lerp(cam.position, desiredPosition, positionSmoothTime * Time.deltaTime);

            cam.position = smoothedPosition;
            cam.rotation = camPositionDefault.rotation;
        }
        else
        {
            cam.rotation= camPositionAlternative.rotation;
        }
   
    }
    private void Update()
    {
        if (switchCam)
        {

            cam.position=camPositionAlternative.position;
        }
    }
    public void SwitchCamera()
    {
        switchCam = !switchCam;
        cabin.SetActive(switchCam);
        if(shipMeshe!=null)
        shipMeshe.enabled = !switchCam;
    }
}
