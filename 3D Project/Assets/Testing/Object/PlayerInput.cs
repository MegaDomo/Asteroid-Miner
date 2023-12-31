//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Testing/Object/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""b2b843fd-3f5a-4a86-98cf-93786df30687"",
            ""actions"": [
                {
                    ""name"": ""MoveAction"",
                    ""type"": ""Value"",
                    ""id"": ""a9e4922a-cfb4-436b-8205-3f7586d7a985"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""00596bbf-a70f-4605-9bd9-b4096f2e6a0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToolHoldAction"",
                    ""type"": ""Button"",
                    ""id"": ""cd52deda-979c-47f7-97f9-6802373255cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToolToggle"",
                    ""type"": ""Button"",
                    ""id"": ""9d1bfce1-e613-4fd9-8920-4ecb9eed776a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""306fc6e0-9246-4295-81c8-57ce1f5f7cc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TransferItem"",
                    ""type"": ""Button"",
                    ""id"": ""83c4fc13-69df-4766-affa-0ee0105ac1d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""836208d0-b46f-47f7-9e4b-af61376bac35"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""3ca69da5-76da-4bbe-84dd-ddaf61a2cfae"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""1435711a-9b75-4d4f-9134-d2c422fa036c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""079a4a80-99a8-45b4-870a-c64906555cbf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""83c9371b-7f01-4b1f-8449-a84aae6d351c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Forward"",
                    ""id"": ""431c0469-5453-4579-8655-3fd17fefea65"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Backward"",
                    ""id"": ""bec49e08-38b7-4cbd-8cdc-b07c31ee1ef3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b3f46905-b09f-4202-8cec-f98753051a3a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""add6f903-fb00-4582-b2c6-f5b56c0dd120"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToolHoldAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eb00c8a-6e5a-4ad2-b8a6-b6766c8fd111"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToolToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04cd0908-67a3-4e13-85e3-6710abfdb4b1"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ShiftClick"",
                    ""id"": ""8352fb90-ee1a-49e2-8a2e-aab51c548627"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TransferItem"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""e41e72bf-dce3-45d0-b530-ad3f8ab96062"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TransferItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""fc5d87c0-048c-447b-804f-3897703af2cf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TransferItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Ship"",
            ""id"": ""575190a3-669e-40a1-8d59-2276ff0a18ef"",
            ""actions"": [
                {
                    ""name"": ""MoveAction"",
                    ""type"": ""Value"",
                    ""id"": ""4959efd3-46f5-4e31-84fe-f143c46b5058"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ExitInteractable"",
                    ""type"": ""Button"",
                    ""id"": ""b97f0808-f0d6-4c8c-a3cf-3db01abae988"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FreeLook"",
                    ""type"": ""Button"",
                    ""id"": ""bf1fb08c-ca95-4a54-9670-98d08123ff2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleTool"",
                    ""type"": ""Button"",
                    ""id"": ""a36e45e2-50d4-415f-bf15-b17b5b6fae90"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""325cb9de-94bd-432d-b3bc-1a27a3c79d46"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""ea73bc44-b50c-47e7-9a9a-587cf7484f9a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""124b7876-6558-4c85-9c3f-6d31cd0ce94c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""88b71e9e-d2ab-4cc2-909b-6987ea23f6a8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""046644a8-ec3c-4d0c-82e3-9ceeafab61e4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Forward"",
                    ""id"": ""ade64c8a-b2ec-4525-97bf-09b100cb5831"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Backward"",
                    ""id"": ""1067df0c-758f-40c0-8778-8dfa3a842e98"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4e00fb82-48e6-4f85-93be-6fe9d6cdd943"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitInteractable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba39c003-1af2-4488-a592-230626e79fd4"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d97f6d1-7c59-49df-ae4c-c09ee533fbbe"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleTool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Crane"",
            ""id"": ""d1cef1cb-42d2-43f1-a9c6-68b3998079f3"",
            ""actions"": [
                {
                    ""name"": ""MoveAction"",
                    ""type"": ""Value"",
                    ""id"": ""58e1e7bc-667f-4521-9617-74b8e4ae234b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ExitInteractable"",
                    ""type"": ""Button"",
                    ""id"": ""47245fbd-06fe-4c7a-8e4d-975b1701acdc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9adbaae8-416a-4bc9-961b-23b85de1cc3b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitInteractable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""dee0c406-2ca5-4aa9-901d-1a04e796cf85"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3c61563c-b957-4cfe-bed3-85c00a8552f9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""03826437-82af-4cac-b3f9-50f3a95f7d4d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9f29ec1a-4223-4474-bc28-a1b889286fac"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b08d3c90-d646-4c8a-ae1d-37e6c3175f41"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MoveAction = m_Player.FindAction("MoveAction", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_ToolHoldAction = m_Player.FindAction("ToolHoldAction", throwIfNotFound: true);
        m_Player_ToolToggle = m_Player.FindAction("ToolToggle", throwIfNotFound: true);
        m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
        m_Player_TransferItem = m_Player.FindAction("TransferItem", throwIfNotFound: true);
        // Ship
        m_Ship = asset.FindActionMap("Ship", throwIfNotFound: true);
        m_Ship_MoveAction = m_Ship.FindAction("MoveAction", throwIfNotFound: true);
        m_Ship_ExitInteractable = m_Ship.FindAction("ExitInteractable", throwIfNotFound: true);
        m_Ship_FreeLook = m_Ship.FindAction("FreeLook", throwIfNotFound: true);
        m_Ship_ToggleTool = m_Ship.FindAction("ToggleTool", throwIfNotFound: true);
        // Crane
        m_Crane = asset.FindActionMap("Crane", throwIfNotFound: true);
        m_Crane_MoveAction = m_Crane.FindAction("MoveAction", throwIfNotFound: true);
        m_Crane_ExitInteractable = m_Crane.FindAction("ExitInteractable", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MoveAction;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_ToolHoldAction;
    private readonly InputAction m_Player_ToolToggle;
    private readonly InputAction m_Player_Inventory;
    private readonly InputAction m_Player_TransferItem;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveAction => m_Wrapper.m_Player_MoveAction;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @ToolHoldAction => m_Wrapper.m_Player_ToolHoldAction;
        public InputAction @ToolToggle => m_Wrapper.m_Player_ToolToggle;
        public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
        public InputAction @TransferItem => m_Wrapper.m_Player_TransferItem;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MoveAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveAction;
                @MoveAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveAction;
                @MoveAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveAction;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @ToolHoldAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToolHoldAction;
                @ToolHoldAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToolHoldAction;
                @ToolHoldAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToolHoldAction;
                @ToolToggle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToolToggle;
                @ToolToggle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToolToggle;
                @ToolToggle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToolToggle;
                @Inventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @TransferItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTransferItem;
                @TransferItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTransferItem;
                @TransferItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTransferItem;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveAction.started += instance.OnMoveAction;
                @MoveAction.performed += instance.OnMoveAction;
                @MoveAction.canceled += instance.OnMoveAction;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @ToolHoldAction.started += instance.OnToolHoldAction;
                @ToolHoldAction.performed += instance.OnToolHoldAction;
                @ToolHoldAction.canceled += instance.OnToolHoldAction;
                @ToolToggle.started += instance.OnToolToggle;
                @ToolToggle.performed += instance.OnToolToggle;
                @ToolToggle.canceled += instance.OnToolToggle;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @TransferItem.started += instance.OnTransferItem;
                @TransferItem.performed += instance.OnTransferItem;
                @TransferItem.canceled += instance.OnTransferItem;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Ship
    private readonly InputActionMap m_Ship;
    private IShipActions m_ShipActionsCallbackInterface;
    private readonly InputAction m_Ship_MoveAction;
    private readonly InputAction m_Ship_ExitInteractable;
    private readonly InputAction m_Ship_FreeLook;
    private readonly InputAction m_Ship_ToggleTool;
    public struct ShipActions
    {
        private @PlayerInput m_Wrapper;
        public ShipActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveAction => m_Wrapper.m_Ship_MoveAction;
        public InputAction @ExitInteractable => m_Wrapper.m_Ship_ExitInteractable;
        public InputAction @FreeLook => m_Wrapper.m_Ship_FreeLook;
        public InputAction @ToggleTool => m_Wrapper.m_Ship_ToggleTool;
        public InputActionMap Get() { return m_Wrapper.m_Ship; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipActions set) { return set.Get(); }
        public void SetCallbacks(IShipActions instance)
        {
            if (m_Wrapper.m_ShipActionsCallbackInterface != null)
            {
                @MoveAction.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnMoveAction;
                @MoveAction.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnMoveAction;
                @MoveAction.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnMoveAction;
                @ExitInteractable.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnExitInteractable;
                @ExitInteractable.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnExitInteractable;
                @ExitInteractable.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnExitInteractable;
                @FreeLook.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnFreeLook;
                @FreeLook.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnFreeLook;
                @FreeLook.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnFreeLook;
                @ToggleTool.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnToggleTool;
                @ToggleTool.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnToggleTool;
                @ToggleTool.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnToggleTool;
            }
            m_Wrapper.m_ShipActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveAction.started += instance.OnMoveAction;
                @MoveAction.performed += instance.OnMoveAction;
                @MoveAction.canceled += instance.OnMoveAction;
                @ExitInteractable.started += instance.OnExitInteractable;
                @ExitInteractable.performed += instance.OnExitInteractable;
                @ExitInteractable.canceled += instance.OnExitInteractable;
                @FreeLook.started += instance.OnFreeLook;
                @FreeLook.performed += instance.OnFreeLook;
                @FreeLook.canceled += instance.OnFreeLook;
                @ToggleTool.started += instance.OnToggleTool;
                @ToggleTool.performed += instance.OnToggleTool;
                @ToggleTool.canceled += instance.OnToggleTool;
            }
        }
    }
    public ShipActions @Ship => new ShipActions(this);

    // Crane
    private readonly InputActionMap m_Crane;
    private ICraneActions m_CraneActionsCallbackInterface;
    private readonly InputAction m_Crane_MoveAction;
    private readonly InputAction m_Crane_ExitInteractable;
    public struct CraneActions
    {
        private @PlayerInput m_Wrapper;
        public CraneActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveAction => m_Wrapper.m_Crane_MoveAction;
        public InputAction @ExitInteractable => m_Wrapper.m_Crane_ExitInteractable;
        public InputActionMap Get() { return m_Wrapper.m_Crane; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CraneActions set) { return set.Get(); }
        public void SetCallbacks(ICraneActions instance)
        {
            if (m_Wrapper.m_CraneActionsCallbackInterface != null)
            {
                @MoveAction.started -= m_Wrapper.m_CraneActionsCallbackInterface.OnMoveAction;
                @MoveAction.performed -= m_Wrapper.m_CraneActionsCallbackInterface.OnMoveAction;
                @MoveAction.canceled -= m_Wrapper.m_CraneActionsCallbackInterface.OnMoveAction;
                @ExitInteractable.started -= m_Wrapper.m_CraneActionsCallbackInterface.OnExitInteractable;
                @ExitInteractable.performed -= m_Wrapper.m_CraneActionsCallbackInterface.OnExitInteractable;
                @ExitInteractable.canceled -= m_Wrapper.m_CraneActionsCallbackInterface.OnExitInteractable;
            }
            m_Wrapper.m_CraneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveAction.started += instance.OnMoveAction;
                @MoveAction.performed += instance.OnMoveAction;
                @MoveAction.canceled += instance.OnMoveAction;
                @ExitInteractable.started += instance.OnExitInteractable;
                @ExitInteractable.performed += instance.OnExitInteractable;
                @ExitInteractable.canceled += instance.OnExitInteractable;
            }
        }
    }
    public CraneActions @Crane => new CraneActions(this);
    public interface IPlayerActions
    {
        void OnMoveAction(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnToolHoldAction(InputAction.CallbackContext context);
        void OnToolToggle(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnTransferItem(InputAction.CallbackContext context);
    }
    public interface IShipActions
    {
        void OnMoveAction(InputAction.CallbackContext context);
        void OnExitInteractable(InputAction.CallbackContext context);
        void OnFreeLook(InputAction.CallbackContext context);
        void OnToggleTool(InputAction.CallbackContext context);
    }
    public interface ICraneActions
    {
        void OnMoveAction(InputAction.CallbackContext context);
        void OnExitInteractable(InputAction.CallbackContext context);
    }
}
