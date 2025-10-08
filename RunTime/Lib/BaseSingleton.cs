using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _instance;

    public static T Instance {
        get {
            if (_instance == null) {
#if UNITY_2023_1_OR_NEWER
                _instance = FindFirstObjectByType<T>();
#else
                _instance = FindObjectOfType<T>();
#endif
                if (_instance == null) {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    public bool IsLoaded { get; private set; } = false;
    private readonly TaskCompletionSource<bool> _initTaskSource = new TaskCompletionSource<bool>();
    public Task InitCompleted => _initTaskSource.Task;

    protected virtual void Awake() {
        if (_instance == null) {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(InitCoroutine());
        }
        else if (_instance != this) {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator InitCoroutine() {
        yield return StartCoroutine(Init());

        IsLoaded = true;
        _initTaskSource.TrySetResult(true);         //코루틴 완료 시점에 Task도 완료 처리
    }

    protected virtual IEnumerator Init() {          //상속받은 매니저에서 비동기 호출 가능 하도록 구성
        yield return null;
    }

    // //비동기 사용 예시
    // IEnumerator Start() {
    //     yield return new WaitUntil(() => Manager.Instance.isLoaded);
    //     Debug.Log("사용 준비 완료(Coroutine)");
    // }
    
    // async void Start() {
    //     await Manager.Instance.InitCompleted;
    //     Debug.Log("사용 준비 완료(async)");
    // }
}
