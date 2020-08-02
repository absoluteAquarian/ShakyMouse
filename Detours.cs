using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace ShakyMouse{
	public static class Detours{
		public static void Load(){
			On.Terraria.GameInput.PlayerInput.MouseInput += PatchMouseInput;
		}

		private static void PatchMouseInput(On.Terraria.GameInput.PlayerInput.orig_MouseInput orig){
			bool mouseUsed = false;

			PlayerInput.MouseInfoOld = PlayerInput.MouseInfo;
			//Changes start here!
			ShakyConfig config = ModContent.GetInstance<ShakyConfig>();
			Vector2 vibe = config.DisableShake ? Vector2.Zero : ShakyMouse.GetVibration(config.ShakeFactor);

			PlayerInput.MouseInfo = Mouse.GetState();

			//Changes end here!
			PlayerInput.ScrollWheelValue += PlayerInput.MouseInfo.ScrollWheelValue;

			int mouseX = PlayerInput.MouseInfo.X + (int)vibe.X;
			int mouseY = PlayerInput.MouseInfo.Y + (int)vibe.Y;
			int mouseDeltaX = mouseX - PlayerInput.MouseInfoOld.X;
			int mouseDeltaY = mouseY - PlayerInput.MouseInfoOld.Y;

			if(mouseDeltaX != 0 || mouseDeltaY != 0 || PlayerInput.MouseInfo.ScrollWheelValue != PlayerInput.MouseInfoOld.ScrollWheelValue){
				PlayerInput.MouseX = mouseX;
				PlayerInput.MouseY = mouseY;
				mouseUsed = true;
			}

			PlayerInput.MouseKeys.Clear();

			if(Main.instance.IsActive){
				if(PlayerInput.MouseInfo.LeftButton == ButtonState.Pressed){
					PlayerInput.MouseKeys.Add("Mouse1");
					mouseUsed = true;
				}

				if(PlayerInput.MouseInfo.RightButton == ButtonState.Pressed){
					PlayerInput.MouseKeys.Add("Mouse2");
					mouseUsed = true;
				}

				if(PlayerInput.MouseInfo.MiddleButton == ButtonState.Pressed){
					PlayerInput.MouseKeys.Add("Mouse3");
					mouseUsed = true;
				}

				if(PlayerInput.MouseInfo.XButton1 == ButtonState.Pressed){
					PlayerInput.MouseKeys.Add("Mouse4");
					mouseUsed = true;
				}

				if(PlayerInput.MouseInfo.XButton2 == ButtonState.Pressed){
					PlayerInput.MouseKeys.Add("Mouse5");
					mouseUsed = true;
				}
			}

			if(mouseUsed){
				PlayerInput.CurrentInputMode = InputMode.Mouse;
				PlayerInput.Triggers.Current.UsedMovementKey = false;
			}
		}
	}
}
