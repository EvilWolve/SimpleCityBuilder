using NUnit.Framework;
using UnityEngine;
using Configuration.Board;

namespace ValidationTests
{
	public class GameboardConfigurationValidationTests
	{
		GameboardConfiguration gameboardConfiguration;
		
		[SetUp]
		public void Setup()
		{
			this.gameboardConfiguration = Resources.Load<GameboardConfiguration>(GameboardConfiguration.RESOURCE_LOCATION);
		}

		[Test]
		public void ValidateGameboardConfigurationLocation()
		{
			Assert.IsNotNull(this.gameboardConfiguration);
		}

		[Test]
		public void ValidateGameboardConfigurationContents()
		{
			Assert.IsTrue(this.gameboardConfiguration.dimensions.x > 0 && this.gameboardConfiguration.dimensions.y > 0);
			Assert.IsNotNull(this.gameboardConfiguration.prefab);
		}
	}
}