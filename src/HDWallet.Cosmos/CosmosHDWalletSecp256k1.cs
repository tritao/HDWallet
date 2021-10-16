using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Cosmos
{
    public class CosmosHDWalletSecp256k1 : HDWallet<CosmosWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Cosmos);

        public CosmosHDWalletSecp256k1(string words, string seedPassword = "") : base(words, seedPassword, _path) { }

        public CosmosHDWalletSecp256k1(CoinType coinType, string words, string seedPassword = "")
            : base(words, seedPassword, Purpose.Create(PurposeNumber.BIP44).Coin(coinType)) { }

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<CosmosWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<CosmosWallet> accountHDWallet = new AccountHDWallet<CosmosWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }
}