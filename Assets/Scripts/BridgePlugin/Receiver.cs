using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Receiver : MonoBehaviour
{
    [SerializeField] GameEvent OnRecieveCloseApp;
    [SerializeField] GameEvent OnRecieveStartApp;
    [SerializeField] BoolValue generateCSVFile;
    int[] settings=new int[10];
    [SerializeField] StringVariable sesionId;

    bool canEnterStartIntent = true;
    bool canEnterCloseIntent = true;

    public void OnReceiveCloseApp(string messageFromNative)
    {
        if (canEnterCloseIntent && (SceneManager.GetActiveScene().name != "SystemLobby"))
        {
            canEnterCloseIntent = false;
            Debug.Log("OnRecieveCloseApp(string messageFromNative)");
            CloseAppClass closeAppInstant = JsonUtility.FromJson<CloseAppClass>(messageFromNative);
            OnRecieveCloseApp.Raise();
            generateCSVFile.Value = closeAppInstant.generateCsvReport;
        }
    }

    public void OnReceiveStartIntent(string messageFromNative)
    {
        if (canEnterStartIntent && (SceneManager.GetActiveScene().name == "SystemLobby"))//SystemLobby
        {
            canEnterStartIntent = false;
            Debug.Log("OnReceiveStartIntent(string messageFromNative)");
            StartAppClass startAppInstant = JsonUtility.FromJson<StartAppClass>(messageFromNative);
            OnRecieveStartApp.Raise();
            settings = startAppInstant.settings;
            sesionId.Value = startAppInstant.sessionId;
            if (GetComponent<MappingChoices>() != null) GetComponent<MappingChoices>().Mapper(settings);
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("settings [" + i + "]=" + settings[i]);
            }
            Debug.Log("sesion id =" + sesionId.Value);
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SystemLobby") Debug.Log(SceneManager.GetActiveScene().name);
    }
}
