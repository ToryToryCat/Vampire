
---

## 📂 Scripts

### ✅ Datas
- **`ItemData.cs`**: 아이템 정보를 담는 `ScriptableObject`.  
  - 아이템 종류, 레벨별 수치, 기본 능력치, 투사체/아이콘 등을 정의.

---

### ✅ Game
- **`AchiveManager.cs`**: 업적 조건을 체크하고, 캐릭터 해금을 처리.
- **`AudioManager.cs`**: BGM 및 효과음을 관리하는 싱글턴.
- **`Bullet.cs`**: 투사체의 이동, 데미지 처리, 관통 및 비활성화 처리.
- **`Character.cs`**: 플레이어 공통 속성(속도 배율 등) 제공.
- **`Enemy.cs`**: 적의 AI 이동, 피격, 사망 처리.
- **`Follow.cs`**: UI가 플레이어를 따라오게 위치 동기화.
- **`GameManager.cs`**: 게임 시간, 경험치, 승리/패배 조건 제어.
- **`Gear.cs`**: 장비를 통해 플레이어 및 무기 스탯 보정.
- **`Hand.cs`**: 손 오브젝트의 방향 및 위치 조정.
- **`Player.cs`**: 입력을 받아 이동, 충돌 시 체력 감소 및 사망 처리.
- **`PoolManager.cs`**: 오브젝트 풀링 관리.
- **`Repostion.cs`**: 영역 밖으로 나간 지형/적 재배치.
- **`Scanner.cs`**: 주변 적 탐지 및 타겟 선택.
- **`Spawner.cs`**: 적 생성 및 `SpawnData` 구조체 정의.
- **`Weapon.cs`**: 무기 회전, 발사, 레벨업, 총알 생성 로직.

---

### ✅ UI
- **`HUD.cs`**: HUD 정보(레벨, 처치 수, 시간, 체력 등) 갱신.
- **`GameResult.cs`**: 승리/패배 화면 UI 처리.
- **`Item.cs`**: 아이템 선택 UI와 업그레이드 로직 관리.
- **`LevelUp.cs`**: 레벨업 UI 생성 및 무작위 아이템 선택.

---

### ✅ Utility
> ⚠️ 현재 설명 누락됨. 사용 중인 스크립트가 있다면 여기에 추가하는 것이 좋음.  
예시:
- **`Singleton<T>.cs`**: 싱글턴 패턴의 베이스 클래스.
- **`ExtensionMethods.cs`**: 확장 메서드 모음 등.

---

## 📝 참고
- 프로젝트는 `GameManager` 중심으로 전체 흐름을 제어하며,  
  대부분의 기능은 `PoolManager`, `AudioManager`, `Spawner` 등 전용 매니저 스크립트와 상호작용합니다.
