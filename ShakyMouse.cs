using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShakyMouse{
	public class ShakyMouse : Mod{
		public static Vector2 GetVibration(float shakeFactor){
			//Not in a world; don't attempt to use a player
			if(Main.gameMenu)
				return Vector2.Zero;

			//Mouse vibing scales with player velocity
			//Scale is linear and caps at 60mph (12 velocity)
			//If the player isn't moving, then no vibing happens
			float playerSpeed = Main.LocalPlayer.velocity.Length();
			
			if(playerSpeed == 0)
				return Vector2.Zero;

			float speedFactor = playerSpeed >= 12 ? 1f : playerSpeed / 12;

			//Full vibe moves the cursor within a 10-tile box centered on the actual position
			//Resulting position can't go outside of the screen though
			Vector2 vibe;
			float randFactor = 5 * 16f;
			float rand() => Main.rand.NextFloat(-randFactor, randFactor);
			do{
				vibe = new Vector2(rand(), rand()) * speedFactor * shakeFactor;
			}while(vibe.X < 0 || vibe.X > Main.screenWidth || vibe.Y < 0 || vibe.Y > Main.screenHeight);

			return vibe;
		}

		public override void Load(){
			Detours.Load();
		}
	}
}