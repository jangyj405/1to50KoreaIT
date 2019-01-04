using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using BackEnd;

public class CJooIAP : MonoBehaviour, IStoreListener
{
	private IStoreController controller = null;
	private IExtensionProvider provider = null;
	public static readonly string[] productIDs = { "dia010", "dia033", "dia055", "dia120", "dia180", "dia300" };

	[SerializeField]
	private GameObject disableTouchPanel = null;

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.controller = controller;
		this.provider = extensions;	
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		//todo Display UI
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		throw new System.NotImplementedException();
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
        //BackendReturnObject validateBro = Backend.Receipt.IsValidateGooglePurchase(args.purchasedProduct.receipt, args.purchasedProduct.definition.id);
        //Debug.Log(validateBro);
        //bool isValidated = validateBro.IsSuccess();
        //if(!isValidated)
        //{
        //    disableTouchPanel.SetActive(false);
        //    return PurchaseProcessingResult.Complete;
        //}
		// A consumable product has been purchased by this user.
		bool isContains = false;
		for (int i = 0; i < productIDs.Length; i++)
		{
			if (productIDs[i].Equals(args.purchasedProduct.definition.id, StringComparison.Ordinal))
			{
				isContains = true;
				break;
			}
		}

		if (isContains)
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			Backend.TBC.ChargeTBC(args.purchasedProduct.receipt, args.purchasedProduct.definition.id);
		}
		else
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed. 
		disableTouchPanel.SetActive(false);
		return PurchaseProcessingResult.Complete;
	}

	// Use this for initialization
	void Start ()
	{
		if(controller == null)
		{
			InitializePurchasing();
		}
	}
	public void InitializePurchasing()
	{
		
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		for (int i = 0; i < productIDs.Length; i++)
		{
			builder.AddProduct(productIDs[i], ProductType.Consumable);
		}
		UnityPurchasing.Initialize(this, builder);
	}

	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return controller != null && provider != null;
	}

	public void BuyProductID(string productId)
	{
		disableTouchPanel.SetActive(true);
		// If Purchasing has been initialized ...
		if (IsInitialized())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = controller.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				controller.InitiatePurchase(product);
			}
			// Otherwise ...
			else
			{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				disableTouchPanel.SetActive(false);
			}
		}
		// Otherwise ...
		else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");
			disableTouchPanel.SetActive(false);
		}
	}
}
