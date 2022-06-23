using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookDash.Managers
{
    public class MejaMakanManager : MonoBehaviour
    {
        public static MejaMakanManager instance;
        // Start is called before the first frame update

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("There is another MejaMakanManager");
                return;
            }
            instance = this;
        }
        private MejaMakan selectedMejaMakan;
        public ButtonUI _buttonUI;
        public void selectMejaMakan(MejaMakan mejaMakan, bool needMenu, bool serveFood)
        {
            /*if(selectedMejaMakan = mejaMakan)
            {

            }*/
            selectedMejaMakan = mejaMakan;
            _buttonUI.enableButton(mejaMakan, needMenu, serveFood);
            Debug.Log("Masuk");
        }
        public void deselectMejaMakan(bool isRange, bool needMenu, bool serveFood)
        {
            selectedMejaMakan = null;
            _buttonUI.hideButton(isRange, needMenu, serveFood);
        }
    }
}

