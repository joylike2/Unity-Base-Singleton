# BaseSingleton for Unity (v1.0.0)  
　
　
## ✅ 소개  
　
Unity 엔진을 기반으로  **BaseSingleton** 을 상속 받은 클래스를 싱글톤으로 만들어주는 클래스입니다.
**비동기 초기화(Task + Coroutine)** 을 지원하도로 싱글톤 기반 클래스로, 게임 매니저 및 시스템 관리 객체 구성에 이상적입니다.

　
　
　
## ⭐ 주요 특징
- 싱글톤 패턴: **FindObjectOfType** 또는 **FindFirstObjectByType** (Unity 2023 이상) 기반 자동 인스턴스 할당
- 코루틴 기반 비동기 초기화: **Init()** 을  Coroutine으로 override 가능
- Task 기반 초기화 완료 감지: **InitCompleted** Task로 초기화 완료 확인 가능 awit 지원
- DontDestroyOnLoad 자동 적용: 씬 전환 간 객체 유지 보장
- 중복 인스턴스 방지: 이미 존재 하는 경우 자동 제거 처리


　
　
　
## 📌 설치 방법
- 순서
　  
	Unity Package Manager 를 통해 가져올 수 있습니다.
	1. **Package Manager** 열기
	2. **Install package from git URL…** 선택 
	3. **`https://github.com/joylike2/Unity-Base-Singleton.git`** 입력 후 설치
　	     
<img src="https://github.com/joylike2/Unity-SaveLoad-FileManager/blob/main/Documentation~/Img_PackageManager1.png?raw=true" width="480px">


```none
https://github.com/joylike2/Unity-Base-Singleton.git
```

　
　
　
## 📌 사용 방법
- **BaseSingleton** 을 상속받아 자동으로 싱글톤으로 생성합니다.

　
　- 비동기 초기화
```csharp
//Init() 코루틴을 통해 비동기 초기화 과정을 구현할 수 있습니다.
public class GameManager : BaseSingleton<GameManager> {
	protected override Ienumerator Init() {
		yield return new WaitForSeconds(1f);
		Debug.Log("초기화 완료");
	}
}
```
　
　- Coroutine 사용
```csharp
StartCoroutine(AfterInit());

IEnumerator AfterInit() {
	yield return new WaitUntil(() => GameManager.Instance.IsLoaded);
	Debug.Log("GameManager 초기화 완료");
}
```
　
　- Task 사용
```csharp
await GameManager.Instance.InitCompleted;
Debug.Log("GameManager 초기화 완료");
```


　
　
　
## 지원 환경
- Unity 엔진 기반 프로젝트


　
　
 　
## 🎉 라이선스
This package is licensed under The MIT License (MIT)

Copyright © 2025 joylike2 (https://github.com/joylike2)

[https://github.com/joylike2/Unity-Base-Singleton](https://github.com/joylike2/Unity-Base-Singleton)    
　

See full copyrights in LICENSE.md inside repository
　
