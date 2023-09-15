using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

// Used for the Hat selection logic
public class PlayerConfigurator : MonoBehaviour
{
    [SerializeField]
    private Transform m_HatAnchor;

    [SerializeField]
    private AssetReferenceGameObject m_hatReference;
    private AsyncOperationHandle<GameObject> m_HatLoadOpHandle;

    void Start()
    {           
        SetHat(m_hatReference);
    }
    public void SetHat(AssetReference hatReference)
    {
        if (!hatReference.RuntimeKeyIsValid()) return;

        m_HatLoadOpHandle = hatReference.LoadAssetAsync<GameObject>();
        m_HatLoadOpHandle.Completed += OnHatLoadComplete;
    }

    private void OnHatLoadComplete(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            Instantiate(asyncOperationHandle.Result, m_HatAnchor);
        else 
            Debug.Log($"AsyncOperationHandle Status: {asyncOperationHandle.Status}");
    }

    private void OnDisable()
    {
        m_HatLoadOpHandle.Completed -= OnHatLoadComplete;
    }
}
