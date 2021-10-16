using System;
using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.Cosmos.Tests
{
    public class GenerateWallet
    {

        [Test]
        public void ShouldGenerateWalletFromMnemonic()
        {
            IHDWallet<CosmosWallet> cosmosHDWallet = new CosmosHDWalletSecp256k1(CoinType.Terra, "social laugh lecture orchard mind spend soap window hidden donor machine moment sketch own desk omit tag hold gaze name parade fly main obtain");
            var account0 = cosmosHDWallet.GetAccount(0);
            CosmosWallet wallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("02b6465822d1fe2b27a10cbfd65f3e577517605b3df02152fa31e1f4994844a619", wallet0.PublicKey.ToHex());
            Assert.AreEqual("b13443590343bdce4043f8442dce3831e758a0949d9fc5cf03d5cfc4058b1452", wallet0.PrivateKey.ToHex());
            Assert.AreEqual("terra1av8hsx3wt8xnw9ksxtjvlywsa6yl4ghw2lth7x",wallet0.GetAddress("terra") );
            var accountPublicKey = wallet0.GetPublicKey("terrapub");
        }
    }
}