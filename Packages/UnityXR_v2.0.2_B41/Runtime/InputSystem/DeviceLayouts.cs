﻿// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

#if UNITY_INPUT_SYSTEM
using UnityEngine.Scripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.PXR.Input
{
    /// <summary>
    /// A Pico Headset
    /// </summary>
    [Preserve]
    [InputControlLayout(displayName = "PicoXR HMD")]
    public class PXR_HMD : XRHMD
    {
        [Preserve]
        [InputControl]
        public ButtonControl userPresence { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "devicetrackingstate" })]
        public new IntegerControl trackingState { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "deviceistracked" })]
        public new ButtonControl isTracked { get; private set; }
        [Preserve]
        [InputControl]
        public new Vector3Control devicePosition { get; private set; }
        [Preserve]
        [InputControl]
        public new QuaternionControl deviceRotation { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control deviceAngularVelocity { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control deviceAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control deviceAngularAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public new Vector3Control leftEyePosition { get; private set; }
        [Preserve]
        [InputControl]
        public new QuaternionControl leftEyeRotation { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control leftEyeAngularVelocity { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control leftEyeAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control leftEyeAngularAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public new Vector3Control rightEyePosition { get; private set; }
        [Preserve]
        [InputControl]
        public new QuaternionControl rightEyeRotation { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control rightEyeAngularVelocity { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control rightEyeAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control rightEyeAngularAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public new Vector3Control centerEyePosition { get; private set; }
        [Preserve]
        [InputControl]
        public new QuaternionControl centerEyeRotation { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control centerEyeAngularVelocity { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control centerEyeAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control centerEyeAngularAcceleration { get; private set; }


        protected override void FinishSetup()
        {
            base.FinishSetup();

            userPresence = GetChildControl<ButtonControl>("userPresence");
            trackingState = GetChildControl<IntegerControl>("trackingState");
            isTracked = GetChildControl<ButtonControl>("isTracked");
            devicePosition = GetChildControl<Vector3Control>("devicePosition");
            deviceRotation = GetChildControl<QuaternionControl>("deviceRotation");
            deviceAngularVelocity = GetChildControl<Vector3Control>("deviceAngularVelocity");
            deviceAcceleration = GetChildControl<Vector3Control>("deviceAcceleration");
            deviceAngularAcceleration = GetChildControl<Vector3Control>("deviceAngularAcceleration");
            leftEyePosition = GetChildControl<Vector3Control>("leftEyePosition");
            leftEyeRotation = GetChildControl<QuaternionControl>("leftEyeRotation");
            leftEyeAngularVelocity = GetChildControl<Vector3Control>("leftEyeAngularVelocity");
            leftEyeAcceleration = GetChildControl<Vector3Control>("leftEyeAcceleration");
            leftEyeAngularAcceleration = GetChildControl<Vector3Control>("leftEyeAngularAcceleration");
            rightEyePosition = GetChildControl<Vector3Control>("rightEyePosition");
            rightEyeRotation = GetChildControl<QuaternionControl>("rightEyeRotation");
            rightEyeAngularVelocity = GetChildControl<Vector3Control>("rightEyeAngularVelocity");
            rightEyeAcceleration = GetChildControl<Vector3Control>("rightEyeAcceleration");
            rightEyeAngularAcceleration = GetChildControl<Vector3Control>("rightEyeAngularAcceleration");
            centerEyePosition = GetChildControl<Vector3Control>("centerEyePosition");
            centerEyeRotation = GetChildControl<QuaternionControl>("centerEyeRotation");
            centerEyeAngularVelocity = GetChildControl<Vector3Control>("centerEyeAngularVelocity");
            centerEyeAcceleration = GetChildControl<Vector3Control>("centerEyeAcceleration");
            centerEyeAngularAcceleration = GetChildControl<Vector3Control>("centerEyeAngularAcceleration");
        }
    }

    /// <summary>
    /// A Pico Controller
    /// </summary>
    [Preserve]
    [InputControlLayout(displayName = "PicoXR Controller", commonUsages = new[] { "LeftHand", "RightHand" })]
    public class PXR_Controller : XRControllerWithRumble
    {
        [Preserve]
        [InputControl(aliases = new[] { "Primary2DAxis", "Touchpad" })]
        public Vector2Control thumbstick { get; private set; }

        [Preserve]
        [InputControl]
        public AxisControl trigger { get; private set; }
        [Preserve]
        [InputControl]
        public AxisControl grip { get; private set; }

        [Preserve]
        [InputControl(aliases = new[] { "A", "X" })]
        public ButtonControl primaryButton { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "B", "Y" })]
        public ButtonControl secondaryButton { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "GripButton", "GripPress" })]
        public ButtonControl gripPressed { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "Primary2DAxisClick", "TouchpadPress" })]
        public ButtonControl thumbstickClicked { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "ATouch", "XTouch" })]
        public ButtonControl primaryTouched { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "BTouch", "YTouch" })]
        public ButtonControl secondaryTouched { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "TriggerTouch" })]
        public AxisControl triggerTouched { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "TriggerPress" })]
        public ButtonControl triggerPressed { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "ThumbRestTouch" })]
        public ButtonControl thumbstickTouched { get; private set; }

        [Preserve]
        [InputControl(aliases = new[] { "controllerTrackingState" })]
        public new IntegerControl trackingState { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "ControllerIsTracked" })]
        public new ButtonControl isTracked { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "controllerPosition" })]
        public new Vector3Control devicePosition { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "controllerRotation" })]
        public new QuaternionControl deviceRotation { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "controllerVelocity" })]
        public Vector3Control deviceVelocity { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "controllerAngularVelocity" })]
        public Vector3Control deviceAngularVelocity { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "controllerAcceleration" })]
        public Vector3Control deviceAcceleration { get; private set; }
        [Preserve]
        [InputControl(aliases = new[] { "controllerAngularAcceleration" })]
        public Vector3Control deviceAngularAcceleration { get; private set; }

        protected override void FinishSetup()
        {
            base.FinishSetup();

            thumbstick = GetChildControl<Vector2Control>("thumbstick");
            trigger = GetChildControl<AxisControl>("trigger");
            triggerTouched = GetChildControl<AxisControl>("triggerTouched");
            grip = GetChildControl<AxisControl>("grip");

            primaryButton = GetChildControl<ButtonControl>("primaryButton");
            secondaryButton = GetChildControl<ButtonControl>("secondaryButton");
            gripPressed = GetChildControl<ButtonControl>("gripPressed");
            thumbstickClicked = GetChildControl<ButtonControl>("thumbstickClicked");
            primaryTouched = GetChildControl<ButtonControl>("primaryTouched");
            secondaryTouched = GetChildControl<ButtonControl>("secondaryTouched");
            thumbstickTouched = GetChildControl<ButtonControl>("thumbstickTouched");
            triggerPressed = GetChildControl<ButtonControl>("triggerPressed");

            trackingState = GetChildControl<IntegerControl>("trackingState");
            isTracked = GetChildControl<ButtonControl>("isTracked");
            devicePosition = GetChildControl<Vector3Control>("devicePosition");
            deviceRotation = GetChildControl<QuaternionControl>("deviceRotation");
            deviceVelocity = GetChildControl<Vector3Control>("deviceVelocity");
            deviceAngularVelocity = GetChildControl<Vector3Control>("deviceAngularVelocity");
            deviceAcceleration = GetChildControl<Vector3Control>("deviceAcceleration");
            deviceAngularAcceleration = GetChildControl<Vector3Control>("deviceAngularAcceleration");
        }
    }
}
#endif