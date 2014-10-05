using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Lastoneout.GameInfrastructure.AiPLayer;

namespace Game.Lastoneout.Tests
{
    [TestClass]
    public class Skynet_should
    {
        SkynetPlayer _skynet;

        [TestInitialize]
        public void Init()
        {
            _skynet = new SkynetPlayer();
        }

        [TestMethod]
        public void keep_winning_state_for_input_2()
        {
            _skynet.GetMove(2).Should().Be(1);
        }

        [TestMethod]
        public void keep_winning_state_for_input_3()
        {
            _skynet.GetMove(3).Should().Be(2);
        }

        [TestMethod]
        public void keep_winning_state_for_input_4()
        {
            _skynet.GetMove(4).Should().Be(3);
        }

        [TestMethod]
        public void keep_winning_state_for_input_6()
        {
            _skynet.GetMove(6).Should().Be(1);
        }
        
        [TestMethod]
        public void keep_winning_state_for_input_20()
        {
            _skynet.GetMove(20).Should().Be(3);
        }


        [TestMethod]
        public void have_delay_less_than_1500ms()
        {
            _skynet.GetDelay().Should().BeLessThan(TimeSpan.FromMilliseconds(1500));
        }
    }
}
