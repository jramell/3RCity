using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour{

    UI.State currentState;
    static UIController currentController;
    List<IUIStateChangedListener> uiStateListeners;

    void Start() {
        uiStateListeners = new List<IUIStateChangedListener>();
    }

    public static UIController Current {
        get {
            if (currentController == null) {
                currentController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
            }
            return currentController;
        }
    }

    public void RegisterUIStateChangedListener(IUIStateChangedListener listener) {
        uiStateListeners.Add(listener);
    }

    public UI.State State {
        get { return currentState; }
        set {
            currentState = value;
            foreach (IUIStateChangedListener listener in uiStateListeners) {
                listener.onUIStateChanged();
            }
        }
    }
}
