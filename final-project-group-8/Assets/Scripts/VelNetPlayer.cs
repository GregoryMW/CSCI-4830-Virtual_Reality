using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe;
using VelNet;
using System.IO;
using System;

public class VelNetPlayer : NetworkSerializedObjectStream
{
    public OVRCameraRig myPlayer;
    public Transform head, leftHand, rightHand;
    public GameObject avatar;
    string avatarURL = "default";
    public GameObject defaultAvatarPrefab;

    protected override void ReceiveState(BinaryReader binaryReader)
    {
        try
        {
            string url = binaryReader.ReadString();
            if (url != avatarURL)
            {
                loadAvatar(url);
            }
        }
        catch (Exception e)
        {
            return;
        }

        if (head == null)
        {
            return;
        }

        updateAvatarHead(binaryReader.ReadVector3(), binaryReader.ReadQuaternion());

        leftHand.position = binaryReader.ReadVector3();
        leftHand.rotation = binaryReader.ReadQuaternion();

        rightHand.position = binaryReader.ReadVector3();
        rightHand.rotation = binaryReader.ReadQuaternion();

        transform.position = binaryReader.ReadVector3();
        transform.rotation = binaryReader.ReadQuaternion();
    }

    protected override void SendState(BinaryWriter binaryWriter)
    {
        if (avatar == null)
        {
            return;
        }
        binaryWriter.Write(avatarURL);
        binaryWriter.Write(head.position);
        binaryWriter.Write(head.rotation);
        binaryWriter.Write(leftHand.position);
        binaryWriter.Write(leftHand.rotation);
        binaryWriter.Write(rightHand.position);
        binaryWriter.Write(rightHand.rotation);
        binaryWriter.Write(transform.position);
        binaryWriter.Write(transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void loadAvatar(string url)
    {

        if (url != null)
        {
            avatarURL = url;
        }

        if (avatarURL == "default")
        {
            GameObject newAvatar = GameObject.Instantiate(defaultAvatarPrefab);
            changeAvatar(newAvatar);
        }
        else
        {
            var avatarLoader = new AvatarLoader();
            avatarLoader.OnCompleted += (_, args) =>
            {

                changeAvatar(args.Avatar);

            };
            avatarLoader.LoadAvatar(avatarURL);
        }

    }

    void changeAvatar(GameObject rpmAvatar)
    {
        if (avatar)
        {
            Destroy(avatar.gameObject);
        }
        avatar = rpmAvatar;
        rightHand = rpmAvatar.transform.FindChildRecursive("RightHand");
        leftHand = rpmAvatar.transform.FindChildRecursive("LeftHand");
        head = rpmAvatar.transform.FindChildRecursive("Head");
    }
    private void OnDestroy()
    {
        if (avatar)
        {
            Destroy(avatar.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayer == null || avatar == null)
        {
            return;
        }

        if (networkObject.IsMine)
        {
            updateAvatarHead(myPlayer.centerEyeAnchor.position, myPlayer.centerEyeAnchor.rotation);

            leftHand.position = myPlayer.leftControllerAnchor.position;
            leftHand.rotation = myPlayer.leftControllerAnchor.rotation;

            rightHand.position = myPlayer.rightControllerAnchor.position;
            rightHand.rotation = myPlayer.rightControllerAnchor.rotation;

            transform.position = myPlayer.transform.position;
            transform.rotation = myPlayer.transform.rotation;
        }
    }

    public void updateAvatarHead(Vector3 targetPosition, Quaternion targetRotation)
    {

        Vector3 headOffset = head.position - avatar.transform.position;
        avatar.transform.position = targetPosition - headOffset;

        Vector3 f = targetRotation * Vector3.forward;
        float headTilt = Mathf.Asin(f.y) * Mathf.Rad2Deg;
        head.localRotation = Quaternion.Euler(-headTilt, 0, 0);
        f.y = 0;
        f.Normalize();
        avatar.transform.forward = f;
    }
}
