using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the amount of money the player currently has. Uses the Text component of its parent object for this.
/// </summary>
public class ShowCurrentMoney : MonoBehaviour, IMoneyChangedListener {

    Text moneyText;

    void Start()
    {
        moneyText = gameObject.GetComponent<Text>();
        CityController.Current.RegisterMoneyChangedListener(this);
        ((IMoneyChangedListener)this).onMoneyChanged();
    }

    public void onMoneyChanged()
    {
        moneyText.text = CityController.Current.CurrentMoney.ToString();
    }
}
