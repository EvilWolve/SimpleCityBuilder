using UnityEngine;

namespace Board.Visual
{
	public class GameboardVisual : MonoBehaviour, IGameboardVisual
	{
		[SerializeField] BoxCollider boxCollider;
		[SerializeField] MeshRenderer meshRenderer;
		
		IGameboard gameboard;
		
		public void SetGameboard(IGameboard gameboard)
		{
			this.gameboard = gameboard;

			const float THICKNESS = 0.1f;
			
			this.boxCollider.center = new Vector3(this.gameboard.Width / 2f, THICKNESS / 2f, this.gameboard.Height / 2f);
			this.boxCollider.size = new Vector3(this.gameboard.Width, THICKNESS, this.gameboard.Height);

			this.meshRenderer.transform.position = new Vector3(this.gameboard.Width / 2f, 0f, this.gameboard.Height / 2f);
			this.meshRenderer.transform.localScale = new Vector3(this.gameboard.Width, 1f, this.gameboard.Height);
			this.meshRenderer.material.SetTextureScale("_MainTex", new Vector2(this.gameboard.Width, this.gameboard.Height));
		}
	}
}
