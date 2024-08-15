using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Button _thisButton;

    [SerializeField] private GameObject _statusActive;
    [SerializeField] private Image _imageProduct;
    [SerializeField] private Text _priceText;

    private SkinProduct product;

    private void Awake()
    {
        _thisButton.onClick.AddListener(() =>
        {
            StoreSkinsHandler.Instance.TapOnProductStore(this, product.numberProduct);
        });
    }

    public void InitButton(SkinProduct skinProduct, Sprite productImage)
    {
        product = skinProduct;

        _priceText.text = skinProduct.priceProduct.ToString();
        _imageProduct.sprite = productImage;

        if (skinProduct.isBuyed)
        {
            ChangeStatusProduct(StoreSkinsHandler.StateProductButton.BuyedProduct);
        }
        else
        {
            ChangeStatusProduct(StoreSkinsHandler.StateProductButton.NoBuyedProduct);
        }
    }

    public void ChangeStatusProduct(StoreSkinsHandler.StateProductButton state)
    {
        switch (state)
        {
            case StoreSkinsHandler.StateProductButton.ActiveProduct:
                _statusActive.SetActive(true);
                break;
            case StoreSkinsHandler.StateProductButton.BuyedProduct:
                _priceText.transform.parent.gameObject.SetActive(false);
                _statusActive.SetActive(false);
                break;
            case StoreSkinsHandler.StateProductButton.NoBuyedProduct:
                _priceText.transform.parent.gameObject.SetActive(true);
                break;
        }
    }

}
