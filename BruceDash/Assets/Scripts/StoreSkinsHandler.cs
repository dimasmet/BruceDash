using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Products
{
    public SkinProduct[] products;
}

[System.Serializable]
public class SkinProduct
{
    public int numberProduct;
    public int priceProduct;
    public bool isBuyed;
}

public class StoreSkinsHandler : MonoBehaviour
{
    public static StoreSkinsHandler Instance;

    public enum StateProductButton
    {
        ActiveProduct,
        BuyedProduct,
        NoBuyedProduct
    }

    [SerializeField] private Products _products;
    [SerializeField] private Sprite[] _skinsSprites;

    [SerializeField] private ShopButton[] _shopButtons;

    [SerializeField] private Text _balanceText;

    private ShopButton _currentItemActive;
    private SkinProduct _currentProductActive;

    [SerializeField] private Text _infoNoCountStars;

    [SerializeField] private ChipPlayer chipPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        string jsonSaveStore = PlayerPrefs.GetString("JsonProductGameStore");
        if (jsonSaveStore != "")
        {
            _products = JsonUtility.FromJson<Products>(jsonSaveStore);
        }

        for (int i = 0; i < _shopButtons.Length; i++)
        {
            _shopButtons[i].InitButton(_products.products[i], _skinsSprites[i]);
        }

        SetNewProductActive(_shopButtons[0], 0);
    }

    private void Start()
    {
        GameMain.BalancePlayer = new BalancePlayer(_balanceText);
    }

    public void TapOnProductStore(ShopButton shopButton, int numberProduct)
    {
        if (_products.products[numberProduct].isBuyed)
        {
            SetNewProductActive(shopButton, numberProduct);
        }
        else
        {
            bool isCheckBalance = GameMain.BalancePlayer.CheckBalance(_products.products[numberProduct].priceProduct);

            if (isCheckBalance)
            {
                GameMain.BalancePlayer.DiscreaseBalance(_products.products[numberProduct].priceProduct);
                _products.products[numberProduct].isBuyed = true;

                shopButton.ChangeStatusProduct(StateProductButton.BuyedProduct);

                SetNewProductActive(shopButton, numberProduct);

                SaveBuyedProducts();
            }
            else
            {
                StartCoroutine(ShowInforamtionNoCouBalance());
            }
        }
    }

    private void SetNewProductActive(ShopButton shopButton, int numberProduct)
    {
        if (_currentItemActive != null)
        {
            _currentItemActive.ChangeStatusProduct(StateProductButton.BuyedProduct);
        }

        _currentItemActive = shopButton;
        _currentItemActive.ChangeStatusProduct(StateProductButton.ActiveProduct);

        _currentProductActive = _products.products[numberProduct];

        chipPlayer.SetSkin(_currentProductActive.numberProduct);
    }

    private void SaveBuyedProducts()
    {
        string json = JsonUtility.ToJson(_products);
        PlayerPrefs.SetString("JsonProductGameStore", json);
    }

    private IEnumerator ShowInforamtionNoCouBalance()
    {
        _infoNoCountStars.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _infoNoCountStars.gameObject.SetActive(false);
    }
}
