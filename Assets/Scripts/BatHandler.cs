﻿using HTC.UnityPlugin.Vive;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    public class BatHandler : MonoBehaviour
    {
        private static BatHandler instance = null;

        public static BatHandler Instance { get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<BatHandler>();
                }
                return instance;
            } 
        }

        [SerializeField]
        private bool rightHandIsOn = true;
        [SerializeField]
        private GameObject leftHand;
        [SerializeField]
        private GameObject rightHand;
        [SerializeField]
        private GameObject leftHandObj;
        [SerializeField]
        private GameObject rightHandObj;
        [SerializeField]
        private GameObject batObj;

        [SerializeField]
        private ControllerButton switchHandsBtn;

        private VivePoseTracker _pt;
#if UNITY_EDITOR
        [SerializeField] private List<Offset> offsets = new List<Offset>();
#endif

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(this);
            }

            _pt = this.gameObject.GetComponent<VivePoseTracker>();

        }

        public ViveRoleProperty BatPose {
            get {
                return _pt.viveRole;

            }
        }

        private ViveRoleProperty goProperty;

        // Start is called before the first frame update
        void Start()
        {
            
            batObj.SetActive(false);
            EventManager.StartListening("GAMESTART", StartGame);
            EventManager.StartListening("GAMEEND", EndGame);
        }

        private void StartGame()
        {
            if (rightHandIsOn)
            {
                SetRightHand();
            }
            else
            {
                SetLeftHand();
            }


            /*
            if (!GameManager.instance.IsOculusGo)
            {
            */
                ViveInput.AddPress(HandRole.LeftHand, switchHandsBtn, LeftTrigger);
                ViveInput.AddPress(HandRole.RightHand, switchHandsBtn, RightTrigger);
            /*
            }
            else
            {
                goProperty = ViveRoleProperty.New(DeviceRole.Device1);
                _pt.viveRole.roleValue = goProperty.roleValue;
                goProperty.onRoleChanged += GoProperty_onRoleChanged;
            }
            */
        }

        private void EndGame()
        {/*
            if (!GameManager.instance.IsOculusGo)
            {*/
                ViveInput.RemovePress(HandRole.LeftHand, switchHandsBtn, LeftTrigger);
                ViveInput.RemovePress(HandRole.RightHand, switchHandsBtn, RightTrigger);
            //}
            batObj.SetActive(false);
            leftHandObj.SetActive(true);
            rightHandObj.SetActive(true);
        }

        private void GoProperty_onRoleChanged()
        {
            _pt.viveRole.roleValue = goProperty.roleValue;
        }

        private void RightTrigger(ViveInputVirtualButton.OutputEventArgs arg0)
        {
            RightTrigger();
        }

        private void LeftTrigger(ViveInputVirtualButton.OutputEventArgs arg0)
        {
            LeftTrigger();
        }

        private void SetRightHand()
        {
            _pt.viveRole.SetEx(HandRole.RightHand);
            rightHandObj.SetActive(false);
            leftHandObj.SetActive(true);
            batObj.SetActive(true);
        }
        private void SetLeftHand()
        {
            _pt.viveRole.SetEx(HandRole.LeftHand);
            rightHandObj.SetActive(true);
            leftHandObj.SetActive(false);
            batObj.SetActive(true);
        }

        public void RightTrigger()
        {
            if (!rightHandIsOn)
            {
                SetRightHand();
                rightHandIsOn = true;
            }
        }

        public void LeftTrigger()
        {
            if (rightHandIsOn)
            {
                SetLeftHand();
                rightHandIsOn = false;
            }
        }
    }

}