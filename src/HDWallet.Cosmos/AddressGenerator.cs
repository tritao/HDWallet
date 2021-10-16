using System;
using System.Linq;
using HDWallet.Core;

namespace HDWallet.Cosmos
{
    public class AddressGenerator : IAddressGenerator
    {
        string DefaultHRP { get; set; } = "cosmos";

        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return $"{GetBech32Address(pubKeyBytes, DefaultHRP)}";
        }

        public string GenerateAddress(byte[] pubKeyBytes, string hrp)
        {
            return $"{GetBech32Address(pubKeyBytes, hrp)}";
        }

        private string GetBech32Address(byte[] pubKeyBytes, string hrp)
        {
            var addr = addressFromPublicKey(pubKeyBytes);
            return Bech32Engine.Encode(hrp, addr);
        }

        private byte[] addressFromPublicKey(byte[] pubKeyBytes)
        {
            if(pubKeyBytes.Length == 65)
            {
                throw new NotImplementedException();
            }

            if(pubKeyBytes.Length == 33)
            {
                var sha256 =  NBitcoin.Crypto.Hashes.SHA256(pubKeyBytes);
                var ripesha = NBitcoin.Crypto.Hashes.RIPEMD160(sha256, sha256.Length);
                return ripesha;
            }

            throw new NotSupportedException();
        }

        public string GetBech32PublicKey(byte[] pubKeyBytes, string hrp)
        {
            var rawPubKey = pubKeyFromPublicKey(pubKeyBytes);
            return Bech32Engine.Encode(hrp, rawPubKey);
        }

        // See: https://github.com/tendermint/tendermint/blob/d419fffe18531317c28c29a292ad7d253f6cafdf/docs/spec/blockchain/encoding.md#public-key-cryptography
        const string BECH32_PUBKEY_DATA_PREFIX = "eb5ae98721";

        private byte[] pubKeyFromPublicKey(byte[] pubKeyBytes)
        {
            var buffer = HexStringToByteArray(BECH32_PUBKEY_DATA_PREFIX);
            var combined = buffer.Concat(pubKeyBytes).ToArray();
            return combined;
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}