using System.Collections;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.XR.Management;
using VELConnect;
using VelNet;
using TMPro;


public class NetworkGameManager : MonoBehaviour
{
    [SerializeField] OVRCameraRig myPlayer;
    [SerializeField] VelNetPlayer localPlayer;
    [SerializeField] GameObject ovrRig;
    [SerializeField] TMP_Text codeText;
    public bool showCode = false;

    IEnumerator startNetworking()
    {
        bool hasInitialState = false;
        bool isJoined = false;
        VELConnectManager.OnInitialState += (s) =>
        {
            hasInitialState = true;
        };

        VelNetManager.OnLoggedIn += () =>
        {
            VelNetManager.Join("Chess 4830 Greg & Gabe");
        };
        VelNetManager.OnJoinedRoom += (roomname) =>
        {
            NetworkObject player = VelNetManager.NetworkInstantiate("Player");
            localPlayer = player.GetComponent<VelNetPlayer>();
            localPlayer.myPlayer = myPlayer;
            isJoined = true;
        };
        yield return new WaitUntil(() => { return hasInitialState && isJoined; });
        string avatar_url = VELConnectManager.GetDeviceData("avatar_url");
        localPlayer.loadAvatar(avatar_url);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < XRGeneralSettings.Instance.Manager.activeLoaders.Count; i++)
        {
            XRLoader loader = XRGeneralSettings.Instance.Manager.activeLoaders[i];
            if (loader.GetType() == typeof(OculusLoader))
            {
                ovrRig.SetActive(true);
            }
        }
        StartCoroutine(startNetworking());
    }

    // Update is called once per frame
    void Update()
    {
        if(!showCode)
        {
            codeText.text = "";
        }
        else
        {
            codeText.text = "" + VELConnectManager.PairingCode;
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            if (showCode)
                showCode = false;
            else
                showCode = true;
        }
    }
}
