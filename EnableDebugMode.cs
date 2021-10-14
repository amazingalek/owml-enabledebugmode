using OWML.ModHelper;
using UnityEngine;
using System.Reflection;
using OWML.Common;
using OWML.Utils;
using UnityEngine.InputSystem;

namespace OWML.EnableDebugMode
{
	public class EnableDebugMode : ModBehaviour
	{
		private int _renderValue;
		private bool _isStarted;
		private PlayerSpawner _playerSpawner;

		public void Start()
		{
			ModHelper.Console.WriteLine($"In {nameof(EnableDebugMode)}!", MessageType.Debug);
			ModHelper.HarmonyHelper.EmptyMethod<DebugInputManager>("Awake");
			ModHelper.Events.Subscribe<PlayerSpawner>(Events.AfterAwake);
			ModHelper.Events.Event += OnEvent;
		}

		private void OnEvent(MonoBehaviour behaviour, Events ev)
		{
			if (behaviour is PlayerSpawner playerSpawner && ev == Events.AfterAwake)
			{
				_playerSpawner = playerSpawner;
				_isStarted = true;
			}
		}

		public void Update()
		{
			if (!_isStarted)
			{
				return;
			}

			if (Keyboard.current[Key.F1].wasPressedThisFrame)
			{
				CycleGUIMode();
			}

			HandleWarping();
		}

		private void HandleWarping()
		{
			if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.Comet);
			}
			if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.HourglassTwin_1);
			}
			if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.TimberHearth);
			}
			if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.BrittleHollow);
			}
			if (Keyboard.current[Key.Digit5].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.GasGiant);
			}
			if (Keyboard.current[Key.Digit6].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.DarkBramble);
			}
			if (Keyboard.current[Key.Digit0].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.Ship);
			}
			if (Keyboard.current[Key.Digit7].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.QuantumMoon);
			}
			if (Keyboard.current[Key.Digit9].wasPressedThisFrame)
			{
				WarpTo(SpawnLocation.LunarLookout);
			}
		}

		private void CycleGUIMode()
		{
			_renderValue++;
			if (_renderValue >= 8)
			{
				_renderValue = 0;
			}
			ModHelper.Console.WriteLine("Render value: " + _renderValue, MessageType.Debug);
			(typeof(GUIMode).GetAnyMember("_renderMode") as FieldInfo)?.SetValue(null, _renderValue);
		}

		private void WarpTo(SpawnLocation location)
		{
			ModHelper.Console.WriteLine($"Warping to {location}!", MessageType.Debug);
			_playerSpawner.DebugWarp(_playerSpawner.GetSpawnPoint(location));
		}
	}
}