using System;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TankCode.Test
{
    public class TestRelayConnectionUI : MonoBehaviour
    {
        [SerializeField] private Button hostBtn;
        [SerializeField] private Button clientBtn;
        [SerializeField] private TMP_InputField joinCodeInputField;

        private async void Awake()
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            hostBtn.onClick.RemoveAllListeners();
            hostBtn.onClick.AddListener(HandleHostClick);

            clientBtn.onClick.RemoveAllListeners();
            clientBtn.onClick.AddListener(HandleClientClick);
        }

        private void HandleHostClick()
        {
            StartHostRelay();
        }

        private void HandleClientClick()
        {
            StartClientRelay(joinCodeInputField.text);
        }

        private async void StartHostRelay()
        {
            try
            {
                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);
                string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                Debug.Log($"Relay Join Code: {joinCode}");

                var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
                transport.SetHostRelayData(
                    allocation.RelayServer.IpV4,
                    (ushort)allocation.RelayServer.Port,
                    allocation.AllocationIdBytes,
                    allocation.Key,
                    allocation.ConnectionData
                );
                NetworkManager.Singleton.StartHost();
            }
            catch (Exception e)
            {
                Debug.LogError($"Relay Host Error: {e.Message}");
            }
        }

        private async void StartClientRelay(string joinCode)
        {
            try
            {
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
                transport.SetClientRelayData(
                    joinAllocation.RelayServer.IpV4,
                    (ushort)joinAllocation.RelayServer.Port,
                    joinAllocation.AllocationIdBytes,
                    joinAllocation.Key,
                    joinAllocation.ConnectionData,
                    joinAllocation.HostConnectionData
                );
                NetworkManager.Singleton.StartClient();
            }
            catch (Exception e)
            {
                Debug.LogError($"Relay Client Error: {e.Message}");
            }
        }
    }
}