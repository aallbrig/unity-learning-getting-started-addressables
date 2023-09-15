using UnityEngine;

// Used for the Hat selection logic
public class PlayerConfigurator : MonoBehaviour
{
    [SerializeField]
    private Transform m_HatAnchor;

    private ResourceRequest m_HatLoadingRequest;

    void Start()
    {           
        SetHat(string.Format("Hat{0:00}", GameManager.s_ActiveHat));
    }

    public void SetHat(string hatKey)
    {
        m_HatLoadingRequest = Resources.LoadAsync(hatKey);
        m_HatLoadingRequest.completed += OnHatLoaded;
    }

    private void OnHatLoaded(AsyncOperation asyncOperation)
    {
        Instantiate(m_HatLoadingRequest.asset as GameObject, m_HatAnchor, false);
    }

    private void OnDisable()
    {
        if (m_HatLoadingRequest != null)
            m_HatLoadingRequest.completed -= OnHatLoaded;
    }
}
