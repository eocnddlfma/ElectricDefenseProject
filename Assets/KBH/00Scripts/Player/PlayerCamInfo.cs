using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCamInfo
{
   private Agent _owner;
   public void Initialize(Agent owner)
   {
      _owner = owner;
      _virtualCam = GameObject.Find("PlayerVirtualCam").GetComponent<CinemachineVirtualCamera>();
      _camTransposer = _virtualCam.GetCinemachineComponent<CinemachineTransposer>();
   }

   [Header("Cam Control")]
   [SerializeField] private CinemachineVirtualCamera _virtualCam;
   [SerializeField] private CinemachineTransposer _camTransposer;
   [SerializeField] private Vector3 _viewCamOffset;
   [SerializeField] private Vector3 _UpgradeCamOffset;
   [SerializeField] private Vector2 onUpgradeDamping;

   public void SetCameraSetting(GameMode mode, Transform lookTrm)
   {
      switch (mode)
      {
         case GameMode.View:
            _virtualCam.Follow = lookTrm;
            _virtualCam.LookAt = lookTrm;
            _camTransposer.m_FollowOffset = _viewCamOffset;
            _camTransposer.m_XDamping = 0;
            _camTransposer.m_ZDamping = 0;
            break;
         case GameMode.Build:

            break;

         case GameMode.Upgrade:
            _virtualCam.Follow = lookTrm;
            _virtualCam.LookAt = lookTrm;
            _camTransposer.m_FollowOffset = _UpgradeCamOffset;
            _camTransposer.m_XDamping = onUpgradeDamping.x;
            _camTransposer.m_ZDamping = onUpgradeDamping.y;
            break;

         case GameMode.Shop:

            break;
      }
   }
}
