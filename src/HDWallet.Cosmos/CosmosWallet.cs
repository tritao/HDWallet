using System;
using System.Linq;
using System.Text;
using HDWallet.Core;
using HDWallet.Secp256k1;
using NBitcoin;
using NBitcoin.DataEncoders;

namespace HDWallet.Cosmos
{
    public class CosmosWallet : Wallet, IWallet
    {
        public CosmosWallet() { }

        public CosmosWallet(string privateKey) : base(privateKey) { }

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public string GetAddress(string hrp)
        {
            return new AddressGenerator().GenerateAddress(PublicKey.ToBytes(), hrp);
        }

        public string GetPublicKey(string hrp)
        {
            return new AddressGenerator().GetBech32PublicKey(PublicKey.ToBytes(), hrp);
        }
    }
}